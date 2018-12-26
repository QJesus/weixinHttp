using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Subjects;
using System.Text.RegularExpressions;
using WeChat.Lib;

namespace WeChat
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

        public MemberItem this[string skey]
        {
            get
            {
                var user = USER_DI.FirstOrDefault(f => f.UserName == skey).User;
                return user == null ? null : JsonConvert.DeserializeObject<MemberItem>(JsonConvert.SerializeObject(user));
            }
        }

        // 对外监听
        public Subject<byte[]> QRCodeStream = new Subject<byte[]>();
        public Subject<MemberItem[]> UsersStream = new Subject<MemberItem[]>();
        public Subject<(string msg, MessageObject obj)> MessageStream = new Subject<(string, MessageObject)>();

        public void Run()
        {
            WEB.Add("DEVICEID", GenerateDeviceId());
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

        public void SendFile(MemberItem user, string file) => _11_SENDFILE(user.ToString().Substring(user.ToString().LastIndexOf('>') + 1), file);

        public void SendText(MemberItem user, string text) => _10_WEBWXSENDMSG(user.UserName, LoginUser.UserName, text);

        private void ShowMessage(string msg, MessageObject obj) => MessageStream.OnNext((msg, obj));

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


        /// <summary>
        /// 查找对应Key的Value
        /// </summary>
        /// <param name="result"></param>
        /// <param name="code"></param>
        /// <param name="regex"></param>
        /// <returns></returns>
        private string SearchKey(string result, string code, string regex)
        {
            if (result.IndexOf(code) != -1)
            {
                var m = new Regex(regex).Match(result);
                if (m.Success)
                {
                    return m.Groups[1].Value;
                }
            }

            //没有找到
            throw new Exception("SearchKey not find \"" + code + "\"");
        }

        private MemberItem GetUserFromDI(string userName) => USER_DI.FirstOrDefault(f => f.UserName == userName).User ?? new MemberItem { UserName = userName, };

        private string GenerateDeviceId()
        {
            // e + 15 个随机数字
            var ran = new Random();
            return new string(Enumerable.Range(0, 16).Select(idx => idx == 0 ? 'e' : (char)(ran.Next(10) + '0')).ToArray());
        }

        /// <summary>
        ///     存储到本地
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="folder"></param>
        /// <returns></returns>
        private string CreateWeiXinFilesFolder(string folder = "files")
        {
            var filesFolder = Path.Combine(MediaFileDirecotory, folder);
            if (!Directory.Exists(filesFolder))
            {
                Directory.CreateDirectory(filesFolder);
            }
            return filesFolder;
        }
    }
}
