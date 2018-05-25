using FluorineFx.Json;
using HttpSocket;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace demo_win_httpsocket
{
    partial class MainForm
    {
        void _ShowMessage(string msg, JavaScriptObject obj = null)
        {
            DelegateFun.ExeControlFun(lstMessage, new DelegateFun.delegateExeControlFun(delegate
            {
                // 全部清除
                if (lstMessage.Items.Count > 3000)
                {
                    lstMessage.Items.Clear();
                }

                string row = null;
                if (obj == null)
                {
                    row = $"{DateTime.Now:MM-dd HH:mm:ss}\tnoJson\t{msg}";
                }
                else
                {
                    var jo = new JObject();
                    foreach (var item in obj)
                    {
                        jo[item.Key] = item.Value.ToString();
                    }
                    var json = JsonConvert.SerializeObject(jo);
                    var data = JsonConvert.DeserializeObject<FunRootObject>(json);
                    row = $"{DateTime.Now:MM-dd HH:mm:ss}\t{GetDIName(data.FromUserName)}>{GetDIName(data.ToUserName)}\t{msg}";
                }
                lstMessage.Items.Insert(0, row);
                lstMessage.SelectedIndex = 0;
            }));
        }

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

    public class FunRootObject
    {
        public string MsgId { get; set; }
        public string FromUserName { get; set; }
        public string ToUserName { get; set; }
        public string MsgType { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }
        public string ImgStatus { get; set; }
        public string CreateTime { get; set; }
        public string VoiceLength { get; set; }
        public string PlayLength { get; set; }
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public string MediaId { get; set; }
        public string Url { get; set; }
        public string AppMsgType { get; set; }
        public string StatusNotifyCode { get; set; }
        public string StatusNotifyUserName { get; set; }
        public string RecommendInfo { get; set; }
        public string ForwardFlag { get; set; }
        public string AppInfo { get; set; }
        public string HasProductId { get; set; }
        public string Ticket { get; set; }
        public string ImgHeight { get; set; }
        public string ImgWidth { get; set; }
        public string SubMsgType { get; set; }
        public string NewMsgId { get; set; }
        public string OriContent { get; set; }
        public string EncryFileName { get; set; }
    }

}