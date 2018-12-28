#if WeChat
namespace WeChat.Lib
#else
namespace HttpSocket
#endif
{
    /// <summary>
    /// 存储Cookies
    /// </summary>
    public class LxwCookie
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Domain { get; set; }
        public string Path { get; set; }
        public string Expires { get; internal set; }
        public override string ToString() => $"{Key}={Value}";
    }

}
