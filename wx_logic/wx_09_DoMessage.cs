using System;

namespace wx_logic
{
    public partial class WXLogic
    {
        /// <summary>
        /// 处理消息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="message"></param>
        /// <param name="MsgId"></param>
        void _9_DoMessage(string msg, MessageObject message, string MsgId)
        {
            var fromUserName = GetUserFromDI(message.FromUserName);
            var toUserName = GetUserFromDI(message.ToUserName);

            ShowMessage($"[{message.MsgType}]{msg}", message);

            switch (message.MsgType)
            {
                // 图片
                case 3: _12_WEBWXGETMSGIMG(message.MsgId); return;
                // 语音
                case 34: _13_WEBWXGETVOICE(message.MsgId); return;
                // 文件
                case 49: _14_WEBWXGETMEDIA(message.MsgId, message); return;
            }


            //处理消息反馈
            if (msg.StartsWith("<msg><op id='1'>"))
            {
                return;
            }

            //webwxvoipnotifymsg
            //处理广告图片
            if (msg.StartsWith("<msg><img length="))
            {
                //if (USER_AD.ContainsKey(json.FromUserName))
                //{
                //    var MsgID = obj["MsgId"] + "";
                //    SendMsg(json.FromUserName, json.ToUserName, "正在处理广告，请稍后...", false);
                //    UploadWxImage(MsgID, json.FromUserName, FormUserName, USER_AD[json.FromUserName]);
                //    Console.WriteLine("3=>" + MsgID);
                //}
                return;
            }

            //处理连接
            if (msg.StartsWith("<msg>") && msg.Contains("<url>"))
            {
                //如何得到原来的标题和描述
                //ShowMsg(msg);
                var url = SubString(msg, "<url>", "</url>");
                //键	值
                //请求	GET /cgi-bin/mmwebwx-bin/webwxgetmsgimg?&MsgID=567846155428648917&skey=%40crypt_688681e4_72dbe72e2947fd5ecbca14ab4637d962&type=slave HTTP/1.1
                //下载图片
                Console.WriteLine("4=>" + url);

                //正在转换文章
                //if (url.StartsWith("<![CDATA", StringComparison.OrdinalIgnoreCase)) return;

                if (!url.StartsWith("<![CDATA", StringComparison.OrdinalIgnoreCase) && !url.StartsWith("http://mp.weixin.qq.com", StringComparison.OrdinalIgnoreCase))
                {
                    var title = "";
                    var des = "";
                    var img = "";

                    //title = SubString(msg, "<title>", "</title>");
                    //des = SubString(msg, "<des>", "</des>");
                    //img = GetBase64FromImage(DownLoadImage(MsgId));
                }


                //SendMsg(json.FromUserName, json.ToUserName, "正在转换文章，请稍后...", false);
                //DownLoadPage(msg, json.FromUserName, FormUserName, title, des, img, json.ToUserName);

                return;
            }

            //未识别消息
            if (msg.StartsWith("<msg>"))
            {
                return;
            }

            if (message.FromUserName == LoginUser.UserName)
            {
                Console.WriteLine("5=>" + message.FromUserName + ">" + message.ToUserName);
                return;
            }

            if (message.FromUserName.StartsWith("@@") || message.ToUserName.StartsWith("@@"))
            {
                Console.WriteLine("6=>" + message.FromUserName + ">" + message.ToUserName);
                return;
            }
        }

        private string SubString(string objValue, string indexStr = "", string lastStr = "", string iDefault = "", bool throwE = false)
        {
            try
            {
                int index = objValue.IndexOf(indexStr);
                if (lastStr != "" && index > -1)
                {
                    objValue = objValue.Remove(0, index);
                    index = objValue.IndexOf(indexStr);
                }
                int last = objValue.IndexOf(lastStr);
                last = last == 0 ? objValue.Length : last;
                return index > -1 && last > -1 ? objValue.Substring(index + indexStr.Length, last - (index + indexStr.Length)) : iDefault;

            }
            catch (Exception error)
            {
                if (throwE)
                {
                    throw error;
                }

                return iDefault;
            }
        }
    }


    public class MessageObject
    {
        public string MsgId { get; set; }
        public string FromUserName { get; set; }
        public string ToUserName { get; set; }
        public int MsgType { get; set; }
        public string Content { get; set; }
        public int Status { get; set; }
        public int ImgStatus { get; set; }
        public int CreateTime { get; set; }
        public int VoiceLength { get; set; }
        public int PlayLength { get; set; }
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public string MediaId { get; set; }
        public string Url { get; set; }
        public int AppMsgType { get; set; }
        public int StatusNotifyCode { get; set; }
        public string StatusNotifyUserName { get; set; }
        public Recommendinfo RecommendInfo { get; set; }
        public int ForwardFlag { get; set; }
        public Appinfo AppInfo { get; set; }
        public int HasProductId { get; set; }
        public string Ticket { get; set; }
        public int ImgHeight { get; set; }
        public int ImgWidth { get; set; }
        public int SubMsgType { get; set; }
        public long NewMsgId { get; set; }
        public string OriContent { get; set; }
        public string EncryFileName { get; set; }
    }

    public class Recommendinfo
    {
        public string UserName { get; set; }
        public string NickName { get; set; }
        public int QQNum { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Content { get; set; }
        public string Signature { get; set; }
        public string Alias { get; set; }
        public int Scene { get; set; }
        public int VerifyFlag { get; set; }
        public int AttrStatus { get; set; }
        public int Sex { get; set; }
        public string Ticket { get; set; }
        public int OpCode { get; set; }
    }

    public class Appinfo
    {
        public string AppID { get; set; }
        public int Type { get; set; }
    }
}