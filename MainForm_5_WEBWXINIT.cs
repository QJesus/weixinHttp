using FluorineFx.Json;
using HttpSocket;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace demo_win_httpsocket
{
    partial class MainForm
    {
        void _5_WEBWXINIT()
        {
            _ShowMessage(System.Reflection.MethodInfo.GetCurrentMethod().Name);

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
            var intObj = JsonConvert.DeserializeObject<WEBWXINITRootObject>(o.Value);
            if (intObj.BaseResponse.Ret != 0)
            {
                throw new Exception("没有返回正确的数据，webwxinit错误!");
            }

            // USER_INFO
            UserName = intObj.User.UserName;
            NickName = intObj.User.NickName;
            Text = $"{NickName} >>> 重庆时时彩微信客户端 V1.0";

            var tmp = string.Join("|", intObj.SyncKey.List.Select(kv => $"{kv.Key}_{kv.Val}"));
            WEB.Add(SYNCKEY, tmp);
            var json = JavaScriptConvert.SerializeObject(intObj.SyncKey);
            WEB.Add(SYNCKEY_LONG, json);
        }
    }


    public class WEBWXINITRootObject
    {
        public Baseresponse BaseResponse { get; set; }
        public int Count { get; set; }
        public Contactlist[] ContactList { get; set; }
        public Synckey SyncKey { get; set; }
        public User User { get; set; }
        public string ChatSet { get; set; }
        public string SKey { get; set; }
        public int ClientVersion { get; set; }
        public int SystemTime { get; set; }
        public int GrayScale { get; set; }
        public int InviteStartCount { get; set; }
        public int MPSubscribeMsgCount { get; set; }
        public Mpsubscribemsglist[] MPSubscribeMsgList { get; set; }
        public int ClickReportInterval { get; set; }
    }

    public class Synckey
    {
        public int Count { get; set; }
        public List[] List { get; set; }
    }

    public class List
    {
        public int Key { get; set; }
        public int Val { get; set; }
    }

    public class User
    {
        public int Uin { get; set; }
        public string UserName { get; set; }
        public string NickName { get; set; }
        public string HeadImgUrl { get; set; }
        public string RemarkName { get; set; }
        public string PYInitial { get; set; }
        public string PYQuanPin { get; set; }
        public string RemarkPYInitial { get; set; }
        public string RemarkPYQuanPin { get; set; }
        public int HideInputBarFlag { get; set; }
        public int StarFriend { get; set; }
        public int Sex { get; set; }
        public string Signature { get; set; }
        public int AppAccountFlag { get; set; }
        public int VerifyFlag { get; set; }
        public int ContactFlag { get; set; }
        public int WebWxPluginSwitch { get; set; }
        public int HeadImgFlag { get; set; }
        public int SnsFlag { get; set; }
    }

    public class Contactlist
    {
        public int Uin { get; set; }
        public string UserName { get; set; }
        public string NickName { get; set; }
        public string HeadImgUrl { get; set; }
        public int ContactFlag { get; set; }
        public int MemberCount { get; set; }
        public object[] MemberList { get; set; }
        public string RemarkName { get; set; }
        public int HideInputBarFlag { get; set; }
        public int Sex { get; set; }
        public string Signature { get; set; }
        public int VerifyFlag { get; set; }
        public int OwnerUin { get; set; }
        public string PYInitial { get; set; }
        public string PYQuanPin { get; set; }
        public string RemarkPYInitial { get; set; }
        public string RemarkPYQuanPin { get; set; }
        public int StarFriend { get; set; }
        public int AppAccountFlag { get; set; }
        public int Statues { get; set; }
        public int AttrStatus { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Alias { get; set; }
        public int SnsFlag { get; set; }
        public int UniFriend { get; set; }
        public string DisplayName { get; set; }
        public int ChatRoomId { get; set; }
        public string KeyWord { get; set; }
        public string EncryChatRoomId { get; set; }
        public int IsOwner { get; set; }
    }

    public class Mpsubscribemsglist
    {
        public string UserName { get; set; }
        public int MPArticleCount { get; set; }
        public Mparticlelist[] MPArticleList { get; set; }
        public int Time { get; set; }
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