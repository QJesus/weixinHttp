using HttpSocket;
using System;
using System.Linq;

namespace wx_logic
{
    public partial class WXLogic
    {
        private readonly LxwHttpSocket WEB = new LxwHttpSocket();

        public bool Run(Action<byte[]> qrcodeAction, Action<MemberItem[]> usersAction)
        {
            WEB.Add("DEVICEID", generateDeviceId());
            WEB.Add("APPID", "wx782c26e4c19acffb");
            //第一步
            _1_JSLOGIN();

            //第二个 获取二维码
            var qrcode = _2_QRCODE();
            qrcodeAction?.Invoke(qrcode);

            if (_3_LOGIN())
            {
                _4_REDIRECT_URL();

                _5_WEBWXINIT();

                _6_WEBWXGETCONTACT();
                usersAction?.Invoke(USER_DI.Select(u => u.User).ToArray());

                //第七步心跳检测
                _7_SYNCCHECK();
                return true;
            }
            return false;
        }

        public void SendFile(MemberItem user, string file)
        {
            string userId = user.ToString().Substring(user.ToString().LastIndexOf('>') + 1);
            _11_SENDFILE(userId, file);
        }

        public void SendText(MemberItem user, string text)
        {
            _10_WEBWXSENDMSG(user.UserName, UserName, text);
        }
    }
}
