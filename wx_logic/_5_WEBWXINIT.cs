using FluorineFx.Json;
using HttpSocket;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace wx_logic
{
    public partial class WXLogic
    {
        void _5_WEBWXINIT()
        {
            _ShowMessage(System.Reflection.MethodBase.GetCurrentMethod().Name);

            var response = WEB.SendRequest(@"POST https://wx{NUMBER}.qq.com/cgi-bin/mmwebwx-bin/webwxinit?r=-784058664&lang=zh_CN&pass_ticket={PASS_TICKET} HTTP/1.1
Host: wx.qq.com
Connection: keep-alive
Content-Length: 148
Accept: application/json, text/plain, */*
Origin: https://wx.qq.com
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.106 Safari/537.36
Content-Type: application/json;charset=UTF-8
Referer: https://wx.qq.com/?&lang=zh_CN
Accept-Encoding: gzip, deflate
Accept-Language: zh-CN,zh;q=0.8
Cookie: pgv_pvid=5421692288; ptcz=4e0a323b1662b719e627137efa1221bb5c435b44a27cba4b09293c0e2cb57516; pt2gguin=o0007103505; pgv_pvi=1998095360; webwxuvid=2be019a311a30a82471c657deb21d97e9d1f45d435bfe8cc3c6d44572bc274f1e6b73083e1be6658fafce8b8da910d9f; pgv_si=s9303685120; wxpluginkey=1452474574; MM_WX_NOTIFY_STATE=1; MM_WX_SOUND_STATE=1; wxuin=840648442; wxsid=5D8s8i5gHhv7JbGO; wxloadtime=1452483021; mm_lang=zh_CN; webwx_data_ticket=AQfq+7neXON/cnHqx9WtuaT/
",

@"{""BaseRequest"":{""Uin"":""{WXUIN}"",""Sid"":""{WXSID}"",""Skey"":""{SKEY}"",""DeviceID"":""{DEVICEID}""}}");
            _5_Response(response);
        }

        void _5_Response(LxwResponse o)
        {
            var login = JsonConvert.DeserializeObject<WEBWXINITRootObject>(o.Value);
            if (login.BaseResponse.Ret != 0)
            {
                throw new Exception("没有返回正确的数据，webwxinit错误!");
            }

            // USER_INFO
            UserName = login.User.UserName;
            NickName = login.User.NickName;

            WEB.Add(SYNCKEY, string.Join("|", login.SyncKey.List.Select(kv => $"{kv.Key}_{kv.Val}")));
            WEB.Add(SYNCKEY_LONG, JavaScriptConvert.SerializeObject(login.SyncKey));

            foreach (var item in login.ContactList.Where(c => string.IsNullOrEmpty(c.KeyWord)).OrderByDescending(c => c.ContactFlag))
            {
                USER_DI.Add((item.UserName, item));
            }
        }
    }

    public class WEBWXINITRootObject
    {
        public Baseresponse BaseResponse { get; set; }
        public long? Count { get; set; }
        public MemberItem[] ContactList { get; set; }
        public Synckey SyncKey { get; set; }
        public User User { get; set; }
        public string ChatSet { get; set; }
        public string SKey { get; set; }
        public long? ClientVersion { get; set; }
        public long? SystemTime { get; set; }
        public long? GrayScale { get; set; }
        public long? InviteStartCount { get; set; }
        public long? MPSubscribeMsgCount { get; set; }
        public Mpsubscribemsglist[] MPSubscribeMsgList { get; set; }
        public long? ClickReportInterval { get; set; }
    }

    public class Synckey
    {
        public long? Count { get; set; }
        public List[] List { get; set; }
    }

    public class List
    {
        public long? Key { get; set; }
        public long? Val { get; set; }
    }

    public class User
    {
        public long? Uin { get; set; }
        public string UserName { get; set; }
        public string NickName { get; set; }
        public string HeadImgUrl { get; set; }
        public string RemarkName { get; set; }
        public string PYInitial { get; set; }
        public string PYQuanPin { get; set; }
        public string RemarkPYInitial { get; set; }
        public string RemarkPYQuanPin { get; set; }
        public long? HideInputBarFlag { get; set; }
        public long? StarFriend { get; set; }
        public long? Sex { get; set; }
        public string Signature { get; set; }
        public long? AppAccountFlag { get; set; }
        public long? VerifyFlag { get; set; }
        public long? ContactFlag { get; set; }
        public long? WebWxPluginSwitch { get; set; }
        public long? HeadImgFlag { get; set; }
        public long? SnsFlag { get; set; }
    }

    public class Mpsubscribemsglist
    {
        public string UserName { get; set; }
        public long? MPArticleCount { get; set; }
        public Mparticlelist[] MPArticleList { get; set; }
        public long? Time { get; set; }
        public string NickName { get; set; }
    }

    public class Mparticlelist
    {
        public string Title { get; set; }
        public string Digest { get; set; }
        public string Cover { get; set; }
        public string Url { get; set; }
    }
}