using HttpSocket;
using Newtonsoft.Json;
using System.Configuration;
using System.Linq;

namespace demo_win_httpsocket
{
    partial class MainForm
    {
        void _6_WEBWXGETCONTACT()
        {
            _ShowMessage(System.Reflection.MethodInfo.GetCurrentMethod().Name);
            var response = WEB.SendRequest(@"GET https://wx{NUMBER}.qq.com/cgi-bin/mmwebwx-bin/webwxgetcontact?lang=zh_CN&pass_ticket={PASS_TICKET}&r={TIME}&seq=0&skey={SKEY} HTTP/1.1
Host: wx.qq.com
Connection: keep-alive
Accept: application/json, text/plain, */*
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.106 Safari/537.36
Referer: https://wx.qq.com/?&lang=zh_CN
Accept-Encoding: gzip, deflate, sdch
Accept-Language: zh-CN,zh;q=0.8
Cookie: pgv_pvid=5421692288; ptcz=4e0a323b1662b719e627137efa1221bb5c435b44a27cba4b09293c0e2cb57516; pt2gguin=o0007103505; pgv_pvi=1998095360; webwxuvid=2be019a311a30a82471c657deb21d97e9d1f45d435bfe8cc3c6d44572bc274f1e6b73083e1be6658fafce8b8da910d9f; pgv_si=s1289632768; wxpluginkey=1452474574; MM_WX_NOTIFY_STATE=1; MM_WX_SOUND_STATE=1; wxuin=840648442; wxsid=+tGm0ywQU38jT5PG; wxloadtime=1452502754; mm_lang=zh_CN; webwx_data_ticket=AQd2WpESNN3Z2JoHNxnridSy


");
            _6_Response(response);
        }

        void _6_Response(LxwResponse o)
        {
            var ro = JsonConvert.DeserializeObject<WEBWXGETCONTACTRootObject>(o.Value);
            var members = ro.MemberList.Where(x => !string.IsNullOrEmpty(x.RemarkName)).OrderBy(x => x.RemarkName)
                .Concat(ro.MemberList.Where(x => string.IsNullOrEmpty(x.RemarkName)).OrderBy(x => x.RemarkName)).ToArray();
            lstBoxUser.Items.Clear();
            for (var i = 0; i < members.Length; i++)
            {
                var item = members[i];
                USER_DI[item.UserName] = item;
                lstBoxUser.Items.Add(item);
            }
            var dft = ConfigurationManager.AppSettings["DefaultIndex"] ?? "0";
            lstBoxUser.SelectedIndex = int.Parse(dft);
            lstBoxUser.TopIndex = lstBoxUser.SelectedIndex - 9 <= 0 ? 0 : lstBoxUser.SelectedIndex - 9;
        }
    }

    public class WEBWXGETCONTACTRootObject
    {
        public Baseresponse BaseResponse { get; set; }
        public int MemberCount { get; set; }
        public MemberItem[] MemberList { get; set; }
        public int Seq { get; set; }
    }

    public class Baseresponse
    {
        public int Ret { get; set; }
        public string ErrMsg { get; set; }
    }

    public class MemberItem
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
        public long AttrStatus { get; set; }
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

        public override string ToString() => string.IsNullOrEmpty(RemarkName) ? NickName : $"{RemarkName}({NickName})";
    }

}

