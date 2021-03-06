#if WeChat
using WeChat.Lib;
#else
using HttpSocket;
#endif
using System.Collections.Generic;

#if WeChat
namespace WeChat
#else
namespace wx_logic
#endif
{
    public partial class WXLogic
    {
        void _10_WEBWXSENDMSG(string fromUserName, string toUserName, string message, int type = 1)
        {
            var KEYS = new Dictionary<string, string>
            {
                ["FROMUSERNAME"] = toUserName,
                ["TOUSERNAME"] = fromUserName,
                ["MSG"] = message,
                ["TYPE"] = type + ""
            };

            RecordMessage(message, new MessageObject { FromUserName = fromUserName, ToUserName = toUserName, Content = message, MsgType = 1, });

            var response = WEB.SendRequest(@"POST https://wx{NUMBER}.qq.com/cgi-bin/mmwebwx-bin/webwxsendmsg?lang=zh_CN&pass_ticket={PASS_TICKET} HTTP/1.1
Host: wx.qq.com
Connection: keep-alive
Content-Length: 411
Accept: application/json, text/plain, */*
Origin: https://wx.qq.com
User-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.106 Safari/537.36
Content-Type: application/json;charset=UTF-8
Referer: https://wx.qq.com/?&lang=zh_CN
Accept-Encoding: gzip, deflate
Accept-Language: zh-CN,zh;q=0.8
Cookie: webwxuvid=bebe27f97a88e8ce573a38b4c48984e8f578650b8ba48b2bacce4ec572e41a3f2b1d021c6656cfeef62091ed7ac2c000; o_cookie=7103505; pgv_pvid=4255253068; pt_clientip=4c950af78b5036b4; pt_serverip=feef0abf0662c02f; ptui_loginuin=7103505; ptcz=6a354e61d971ccc416a5d9fa461f67c599f02d19c0bbb5cc8dad6d82c7850721; pt2gguin=o0007103505; uin=o0007103505; skey=@SVm04rrY6; qm_username=7103505; qm_sid=65f2ed6111bef5d92e7dbf710aec3449,qb3BUenJOMmtmUndFenl3MVFmaExsQkJOYmpVLWRqeC1qNG0xeGxGdDZVUV8.; pgv_pvi=9815860224; pgv_si=s1555394560; MM_WX_NOTIFY_STATE=1; MM_WX_SOUND_STATE=1; mm_lang=zh_CN; wxloadtime=1452518428_expired; wxpluginkey=1452511573; wxuin=840648442; wxsid=BJONNeerx+IW+mmH; webwx_data_ticket=AQfiPZdvOnP2C2jMbzfG7DN/


",
                @"{""BaseRequest"":{""Uin"":{WXUIN},""Sid"":""{WXSID}"",""Skey"":""{SKEY}"",""DeviceID"":""{DEVICEID}""},""Msg"":{""Type"":{TYPE},""Content"":""{MSG}"",""FromUserName"":""{FROMUSERNAME}"",""ToUserName"":""{TOUSERNAME}"",""LocalID"":""{TIME}"",""ClientMsgId"":""{TIME}""}}"

                , KEYS);
            _10_Response(response);
        }

        void _10_Response(LxwResponse o)
        {
            //不做任何处理
        }
    }
}