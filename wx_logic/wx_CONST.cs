using System.Collections.Generic;

namespace wx_logic
{
    public partial class WXLogic
    {
        /// <summary>
        /// 全局的一些变量存储
        /// </summary>
        public const string UUID = "UUID";
        public const string NUMBER = "NUMBER";
        public const string REDIRECT_URL = "REDIRECT_URL";

        public const string SKEY = "SKEY";
        public const string WXSID = "WXSID";
        public const string WXUIN = "WXUIN";
        public const string PASS_TICKET = "PASS_TICKET";

        public const string DEVICEID = "DEVICEID";
        public const string SYNCKEY = "SYNCKEY";
        public const string SYNCKEY_LONG = "SYNCKEY_LONG";

        public string UserName = "";
        public string NickName = "";

        List<(string UserName, MemberItem User)> USER_DI = new List<(string, MemberItem)>();


        //NUMBER
    }
}
