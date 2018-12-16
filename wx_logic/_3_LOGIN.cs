using HttpSocket;
using System;

namespace wx_logic
{
    public partial class WXLogic
    {
        /// <summary>
        /// 五秒钟轮训
        /// </summary>
        int SAOMIAO_SLEEPTIME = 5000;

        /// <summary>
        /// 最多重试次数
        /// </summary>
        int LOGIN_TRY_TIMES = 50;

        void _3_LOGIN(Action<string> loginAction)
        {
            //第三步，等待扫描
            var tmr = new System.Timers.Timer { Interval = SAOMIAO_SLEEPTIME, };
            int count = 0;
            tmr.Elapsed += (s, e) =>
            {
                if (count++ > LOGIN_TRY_TIMES)
                {
                    tmr.Stop();
                    loginAction?.Invoke($"错误的次数超过了:{LOGIN_TRY_TIMES}次");
                }

                _ShowMessage(System.Reflection.MethodBase.GetCurrentMethod().Name);
                var response = WEB.SendRequest(@"GET https://login.weixin.qq.com/cgi-bin/mmwebwx-bin/login?loginicon=true&uuid={UUID}&tip=0&r=-784071163&_={TIME} HTTP/1.1
Host: login.weixin.qq.com
Connection: keep-alive
Accept: */*
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.106 Safari/537.36
Referer: https://wx.qq.com/?&lang=zh_CN
Accept-Encoding: gzip, deflate
Accept-Language: zh-CN,zh;q=0.8
Cookie: pgv_pvid=5421692288; ptcz=4e0a323b1662b719e627137efa1221bb5c435b44a27cba4b09293c0e2cb57516; pt2gguin=o0007103505; pgv_pvi=1998095360; pgv_si=s9303685120
");

                if (_3_Response(response))
                {
                    tmr.Stop();
                    loginAction?.Invoke(null);
                }
            };
            tmr.Start();
        }

        bool _3_Response(LxwResponse o)
        {
            var result = o.Value;
            if (result.IndexOf("window.redirect_uri=") != -1)
            {
                var redirect_uri = SearchKey(result, "redirect_uri", "\"(.*?)\"");
                WEB.Add(REDIRECT_URL, redirect_uri);

                WEB.Add(NUMBER, "");
                if (redirect_uri.IndexOf("wx2.qq.com") != -1)
                {
                    WEB.Add(NUMBER, "2");
                }
                return true;
            }
            return false;
        }
    }
}