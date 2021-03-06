﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;

namespace WeChat.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WeChatController : ControllerBase
    {
        private static readonly Dictionary<string, (WXLogic wx, IDisposable stream)> WeChatCache = new Dictionary<string, (WXLogic, IDisposable)>();
        private static HubConnection connection;

        [HttpGet]
        [ActionNameAttribute("AccessToken")]
        public async Task<IActionResult> AccessTokenAsync()
        {
            if (connection == null)
            {
                connection = new HubConnectionBuilder().WithUrl("http://127.0.0.1:53290/hub").Build();
                connection.Closed += async (error) =>
                {
                    await Task.Delay(new Random().Next(0, 5) * 1000);
                    await connection.StartAsync();
                };
                await connection.StartAsync();
            }

            var key = HttpContext.Connection.RemoteIpAddress.ToString();
            if (!WeChatCache.TryGetValue(key, out var x) || x.wx.LoginUser == null)
            {
                var wx = x.wx ?? new WXLogic();
                var bytes = wx.GetQRCode();
                WeChatCache[key] = (wx, wx.MessageStream.Subscribe(async data =>
                {
                    var username = wx.GetUsers(false).FirstOrDefault(f => f.UserName == data.obj?.ToUserName)?.NickName ?? string.Empty;
                    var message = data.obj?.Content ?? data.msg ?? string.Empty;
                    await connection.InvokeAsync(nameof(Hubs.ChatHub.NewMessage), username, message);
                }));
                return new JsonResult(new { token = key, qrcode = $"data:image/jpeg;base64,{Convert.ToBase64String(bytes)}" });
            }
            return new JsonResult(new { token = key, qrcode = (string)null });
        }

        [HttpGet]
        public IActionResult Login(string token)
        {
            if (WeChatCache.TryGetValue(token, out var x))
            {
                var b = x.wx.DoLogin();
                return new JsonResult(new { login = b, users = UserInfo(x.wx.GetUsers(false)), });
            }
            return new JsonResult(new { login = false });
        }

        private dynamic UserInfo(MemberItem[] users)
        {
            return (users ?? new MemberItem[0]).OrderBy(u => u.VerifyFlag).Select(u => new
            {
                u.UserName,
                u.NickName,
                u.PYInitial,
                u.PYQuanPin,
                u.HeadImgUrl,
                u.Sex,
                u.Signature,
                u.RemarkName,
                u.RemarkPYInitial,
                u.RemarkPYQuanPin,
                u.MemberCount,
                u.MemberList,
                u.Province,
                u.City,
                Group = u.VerifyFlag > 0 || u.VerifyFlag == 0 && u.AttrStatus == 0,
            });
        }

        [HttpGet]
        public IActionResult Users(string token)
        {
            if (WeChatCache.TryGetValue(token, out var x))
            {
                return new JsonResult(new { users = UserInfo(x.wx.GetUsers(true)), });
            }
            return null;
        }

        [HttpPost]
        public IActionResult SendText(string token, string user, string text)
        {
            if (WeChatCache.TryGetValue(token, out var x))
            {
                x.wx.SendText(new MemberItem { UserName = user, }, text);
            }
            return null;
        }
    }
}