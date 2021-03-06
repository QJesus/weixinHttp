#if WeChat
using WeChat.Lib;
#else
using HttpSocket;
#endif

#if WeChat
namespace WeChat
#else
namespace wx_logic
#endif
{
    public partial class WXLogic
    {
        void _1_JSLOGIN()
        {
            RecordMessage(System.Reflection.MethodBase.GetCurrentMethod().Name, null);

            var response = WEB.SendRequest(@"GET https://login.weixin.qq.com/jslogin?appid={APPID}&redirect_uri=https%3A%2F%2Fwx.qq.com%2Fcgi-bin%2Fmmwebwx-bin%2Fwebwxnewloginpage&fun=new&lang=zh_CN&_=1452483004640 HTTP/1.1
Host: login.weixin.qq.com
Connection: keep-alive
Accept: */*
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.106 Safari/537.36
Referer: https://wx.qq.com/?&lang=zh_CN
Accept-Encoding: gzip, deflate, sdch
Accept-Language: zh-CN,zh;q=0.8
Cookie: pgv_pvid=5421692288; ptcz=4e0a323b1662b719e627137efa1221bb5c435b44a27cba4b09293c0e2cb57516; pt2gguin=o0007103505; pgv_pvi=1998095360; pgv_si=s9303685120

");
            _1_Response(response);
        }

        void _1_Response(LxwResponse o)
        {
            var value = SearchKey(o.Value, "200", "\"(.*?)\"");
            WEB.Add(UUID, value);
        }
    }
}
