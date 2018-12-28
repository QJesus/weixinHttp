using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WeChat.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WeChatController : ControllerBase
    {
        private static readonly Dictionary<string, WXLogic> WeChatCache = new Dictionary<string, WXLogic>();

        [HttpGet]
        public IActionResult AccessToken()
        {
            lock (WeChatCache)
            {
                var key = HttpContext.Connection.RemoteIpAddress.ToString();
                if (!WeChatCache.TryGetValue(key, out var x) || x.LoginUser == null)
                {
                    var wx = x ?? new WXLogic();
                    var bytes = wx.GetQRCode();
                    WeChatCache[key] = wx;
                    return new JsonResult(new { token = key, qrcode = $"data:image/jpeg;base64,{Convert.ToBase64String(bytes)}" });
                }
                return new JsonResult(new { token = key, qrcode = (string)null });
            }
        }

        [HttpGet]
        public IActionResult Login(string token)
        {
            if (WeChatCache.TryGetValue(token, out var wx))
            {
                var b = wx.DoLogin();
                return new JsonResult(new { login = b, users = UserInfo(wx.GetUsers(false)), });
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
            if (WeChatCache.TryGetValue(token, out var wx))
            {
                var users = wx.GetUsers(true);
                return new JsonResult(new { users = UserInfo(wx.GetUsers(false)), });
            }
            return null;
        }
    }
}