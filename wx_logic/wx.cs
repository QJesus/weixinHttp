#if WeChat
using WeChat.Lib;
#else
using HttpSocket;
#endif
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Subjects;
using System.Text.RegularExpressions;

#if WeChat
namespace WeChat
#else
namespace wx_logic
#endif
{
    public partial class WXLogic
    {
        /// <summary>
        /// 全局的一些变量存储
        /// </summary>
        private const string UUID = "UUID";
        private const string NUMBER = "NUMBER";
        private const string REDIRECT_URL = "REDIRECT_URL";
        private const string SKEY = "SKEY";
        private const string WXSID = "WXSID";
        private const string WXUIN = "WXUIN";
        private const string PASS_TICKET = "PASS_TICKET";
        private const string DEVICEID = "DEVICEID";
        private const string SYNCKEY = "SYNCKEY";
        private const string SYNCKEY_LONG = "SYNCKEY_LONG";

        private readonly LxwHttpSocket WEB = new LxwHttpSocket();
        private readonly List<(string UserName, MemberItem User)> USER_DI = new List<(string, MemberItem)>();

        /// <summary>
        ///     当前登录人信息
        /// </summary>
        public User LoginUser { get; set; }

        /// <summary>
        ///     多媒体文件的存储地址（如语音，图片等信息）
        /// </summary>
        public string MediaFileDirecotory { get; set; } = AppDomain.CurrentDomain.BaseDirectory;

        public Subject<(string msg, MessageObject obj)> MessageStream = new Subject<(string, MessageObject)>();

        public byte[] GetQRCode()
        {
            WEB.Add("DEVICEID", GenerateDeviceId());
            WEB.Add("APPID", "wx782c26e4c19acffb");
            //第一步
            _1_JSLOGIN();
            //第二个 获取二维码
            return _2_QRCODE();
        }

        private string GenerateDeviceId()
        {
            // e + 15 个随机数字
            var ran = new Random();
            return new string(Enumerable.Range(0, 16).Select(idx => idx == 0 ? 'e' : (char)(ran.Next(10) + '0')).ToArray());
        }

        public bool DoLogin()
        {
            if (_3_LOGIN())
            {
                _4_REDIRECT_URL();
                _5_WEBWXINIT();
                var members = _6_WEBWXGETCONTACT();
                foreach (var member in members)
                {
                    var find = USER_DI.FirstOrDefault(f => f.UserName == member.UserName);
                    if (find.User == null)
                    {
                        USER_DI.Add((member.UserName, member));
                    }
                    else
                    {
                        find.User = member;
                    }
                }
                //第七步心跳检测
                _7_SYNCCHECK();
                return true;
            }
            return false;
        }

        public MemberItem[] GetUsers(bool fresh = false)
        {
            return (fresh ? _6_WEBWXGETCONTACT() : USER_DI.Select(u => u.User).ToArray()).OrderByDescending(a => a.ContactFlag & 2048).ToArray();
        }

        public void SendText(MemberItem user, string text) => _10_WEBWXSENDMSG(user.UserName, LoginUser.UserName, text);

        private void RecordMessage(string msg, MessageObject obj) => MessageStream.OnNext((msg, obj));

        public void SendFile(MemberItem user, string file) => _11_SENDFILE(user.ToString().Substring(user.ToString().LastIndexOf('>') + 1), file);

        ///// <summary>
        ///// 得到文件类型
        ///// </summary>
        ///// <param name="fileName"></param>
        ///// <returns></returns>
        //string getFileType(string fileName)
        //{
        //    string mimeType = "application/unknown";
        //    string ext = Path.GetExtension(fileName).ToLower();
        //    RegistryKey regKey = Registry.ClassesRoot.OpenSubKey(ext);
        //    if (regKey != null && regKey.GetValue("Content Type") != null)
        //    {
        //        mimeType = regKey.GetValue("Content Type").ToString();
        //    }
        //    return mimeType;
        //}
    }
}