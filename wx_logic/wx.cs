using HttpSocket;
using System.Linq;
using System.Reactive.Subjects;

namespace wx_logic
{
    public partial class WXLogic
    {
        private readonly LxwHttpSocket WEB = new LxwHttpSocket();

        public Subject<byte[]> QRCodeStream = new Subject<byte[]>();
        public Subject<MemberItem[]> UsersStream = new Subject<MemberItem[]>();
        public Subject<(string msg, MessageObject obj)> MessageStream = new Subject<(string, MessageObject)>();

        public void Run()
        {
            WEB.Add("DEVICEID", generateDeviceId());
            WEB.Add("APPID", "wx782c26e4c19acffb");
            //第一步
            _1_JSLOGIN();

            //第二个 获取二维码
            var qrcode = _2_QRCODE();
            QRCodeStream.OnNext(qrcode);

            if (_3_LOGIN())
            {
                _4_REDIRECT_URL();

                _5_WEBWXINIT();

                _6_WEBWXGETCONTACT();
                UsersStream.OnNext(USER_DI.Select(u => u.User).ToArray());

                //第七步心跳检测
                _7_SYNCCHECK();
            }
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
