using WeChat.Lib;
using Newtonsoft.Json.Linq;
using System;
using System.Xml;

namespace WeChat
{
    public partial class WXLogic
    {
        void _4_REDIRECT_URL()
        {
            ShowMessage(System.Reflection.MethodBase.GetCurrentMethod().Name, null);

            var response = WEB.SendRequest(@"GET {REDIRECT_URL} HTTP/1.1
Host: wx.qq.com
Connection: keep-alive
Accept: application/json, text/plain, */*
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.106 Safari/537.36
Referer: https://wx.qq.com/?&lang=zh_CN
Accept-Encoding: gzip, deflate, sdch
Accept-Language: zh-CN,zh;q=0.8
Cookie: pgv_pvid=5421692288; ptcz=4e0a323b1662b719e627137efa1221bb5c435b44a27cba4b09293c0e2cb57516; pt2gguin=o0007103505; pgv_pvi=1998095360; webwxuvid=2be019a311a30a82471c657deb21d97e9d1f45d435bfe8cc3c6d44572bc274f1e6b73083e1be6658fafce8b8da910d9f; pgv_si=s9303685120; wxloadtime=1452481173_expired; wxpluginkey=1452474574; wxuin=840648442; mm_lang=zh_CN; MM_WX_NOTIFY_STATE=1; MM_WX_SOUND_STATE=1



");
            _4_Response(response);
        }

        void _4_Response(LxwResponse o)
        {
            var value = o.Value;
            if (value.IndexOf("pass_ticket") == -1)
            {
                throw new Exception("没有得到wxsid信息");
            }

            var weixin = Xml2Json<WeiXinTicket>(value);
            //存储数据
            WEB.Add(SKEY, weixin.skey);
            WEB.Add(WXSID, weixin.wxsid);
            WEB.Add(WXUIN, weixin.wxuin);
            WEB.Add(PASS_TICKET, weixin.pass_ticket);
        }

        T Xml2Json<T>(string xml, string root = "error")
        {
            var doc = new XmlDocument();
            doc.LoadXml(xml);

            var obj = new JObject();
            foreach (XmlNode node in doc.SelectSingleNode(root).ChildNodes)
            {
                //获取内容
                obj[node.Name] = node.InnerText;
            }
            return obj.ToObject<T>();
        }
    }

    /// <summary>
    /// 返回信息
    /// </summary>
    public class WeiXinTicket
    {
        public int ret { get; set; }
        public string message { get; set; }
        public string skey { get; set; }
        public string wxsid { get; set; }
        public string wxuin { get; set; }
        public string pass_ticket { get; set; }
        public string isgrayscale { get; set; }
    }
}
