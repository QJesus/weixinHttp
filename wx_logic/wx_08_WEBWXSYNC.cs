#if WeChat
using WeChat.Lib;
#else
using HttpSocket;
#endif
using Newtonsoft.Json;
using System;

#if WeChat
namespace WeChat
#else
namespace wx_logic
#endif
{
    public partial class WXLogic
    {
        void _8_WEBWXSYNC()
        {
            //_ShowMessage(System.Reflection.MethodInfo.GetCurrentMethod().Name);
            var response = WEB.SendRequest(@"POST https://wx{NUMBER}.qq.com/cgi-bin/mmwebwx-bin/webwxsync?sid={WXSID}&skey={SKEY}&lang=zh_CN&pass_ticket={PASS_TICKET} HTTP/1.1
Host: wx.qq.com
Connection: keep-alive
Content-Length: 301
Accept: application/json, text/plain, */*
Origin: https://wx.qq.com
User-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.106 Safari/537.36
Content-Type: application/json;charset=UTF-8
Referer: https://wx.qq.com/?&lang=zh_CN
Accept-Encoding: gzip, deflate
Accept-Language: zh-CN,zh;q=0.8
Cookie: webwxuvid=bebe27f97a88e8ce573a38b4c48984e8f578650b8ba48b2bacce4ec572e41a3f2b1d021c6656cfeef62091ed7ac2c000; o_cookie=7103505; pgv_pvid=4255253068; pt_clientip=4c950af78b5036b4; pt_serverip=feef0abf0662c02f; ptui_loginuin=7103505; ptcz=6a354e61d971ccc416a5d9fa461f67c599f02d19c0bbb5cc8dad6d82c7850721; pt2gguin=o0007103505; uin=o0007103505; skey=@SVm04rrY6; qm_username=7103505; qm_sid=65f2ed6111bef5d92e7dbf710aec3449,qb3BUenJOMmtmUndFenl3MVFmaExsQkJOYmpVLWRqeC1qNG0xeGxGdDZVUV8.; pgv_pvi=9815860224; pgv_si=s1555394560; MM_WX_NOTIFY_STATE=1; MM_WX_SOUND_STATE=1; wxuin=840648442; wxsid=BJONNeerx+IW+mmH; wxloadtime=1452518428; mm_lang=zh_CN; webwx_data_ticket=AQfiPZdvOnP2C2jMbzfG7DN/



",
@"{""BaseRequest"":{""Uin"":{WXUIN},""Sid"":""{WXSID}"",""Skey"":""{SKEY}"",""DeviceID"":""{DEVICEID}""},""SyncKey"":{SYNCKEY_LONG},""rr"":{TIME}}"
);
            _8_Response(response);
        }

        void _8_Response(LxwResponse o)
        {
            var value = o.Value;
            if (value.IndexOf("\"SyncKey\": ") == -1)
            {
                throw new Exception("SyncKey 没有捕获到");
            }

            var root = JsonConvert.DeserializeObject<SyncRoot>(value);
            if (root.SyncKey != null && root.SyncKey.Count != null)
            {
                WEB.Add(SYNCKEY_LONG, JsonConvert.SerializeObject(root.SyncKey));
            }
            foreach (var item in root.AddMsgList)
            {
                var content = item.Content.Replace("&gt;", ">").Replace("&lt;", "<").Replace("<br/>", "");
                //处理消息
                _9_DoMessage(content, item, item.MsgId);
            }
        }
    }


    public class SyncRoot
    {
        public Baseresponse BaseResponse { get; set; }
        public int AddMsgCount { get; set; }
        public MessageObject[] AddMsgList { get; set; }
        public int ModContactCount { get; set; }
        public Modcontactlist[] ModContactList { get; set; }
        public int DelContactCount { get; set; }
        public object[] DelContactList { get; set; }
        public int ModChatRoomMemberCount { get; set; }
        public object[] ModChatRoomMemberList { get; set; }
        public Profile Profile { get; set; }
        public int ContinueFlag { get; set; }
        public Synckey SyncKey { get; set; }
        public string SKey { get; set; }
        public Synccheckkey SyncCheckKey { get; set; }
    }

    public class Profile
    {
        public int BitFlag { get; set; }
        public Username UserName { get; set; }
        public Nickname NickName { get; set; }
        public int BindUin { get; set; }
        public Bindemail BindEmail { get; set; }
        public Bindmobile BindMobile { get; set; }
        public int Status { get; set; }
        public int Sex { get; set; }
        public int PersonalCard { get; set; }
        public string Alias { get; set; }
        public int HeadImgUpdateFlag { get; set; }
        public string HeadImgUrl { get; set; }
        public string Signature { get; set; }
    }

    public class Username
    {
        public string Buff { get; set; }
    }

    public class Nickname
    {
        public string Buff { get; set; }
    }

    public class Bindemail
    {
        public string Buff { get; set; }
    }

    public class Bindmobile
    {
        public string Buff { get; set; }
    }

    public class Synccheckkey
    {
        public int Count { get; set; }
        public List1[] List { get; set; }
    }

    public class List1
    {
        public int Key { get; set; }
        public int Val { get; set; }
    }

    public class Modcontactlist
    {
        public string UserName { get; set; }
        public string NickName { get; set; }
        public int Sex { get; set; }
        public int HeadImgUpdateFlag { get; set; }
        public int ContactType { get; set; }
        public string Alias { get; set; }
        public string ChatRoomOwner { get; set; }
        public string HeadImgUrl { get; set; }
        public int ContactFlag { get; set; }
        public int MemberCount { get; set; }
        public Memberlist[] MemberList { get; set; }
        public int HideInputBarFlag { get; set; }
        public string Signature { get; set; }
        public int VerifyFlag { get; set; }
        public string RemarkName { get; set; }
        public int Statues { get; set; }
        public int AttrStatus { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public int SnsFlag { get; set; }
        public string KeyWord { get; set; }
    }

    public class Memberlist
    {
        public int Uin { get; set; }
        public string UserName { get; set; }
        public string NickName { get; set; }
        public long AttrStatus { get; set; }
        public string PYInitial { get; set; }
        public string PYQuanPin { get; set; }
        public string RemarkPYInitial { get; set; }
        public string RemarkPYQuanPin { get; set; }
        public int MemberStatus { get; set; }
        public string DisplayName { get; set; }
        public string KeyWord { get; set; }
    }
}
