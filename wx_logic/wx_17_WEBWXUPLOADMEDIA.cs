using HttpSocket;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace wx_logic
{
    public partial class WXLogic
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FromUserName"></param>
        /// <param name="ToUserName"></param>
        /// <param name="message"></param>
        /// <param name="type"></param>
        string _17_WEBWXUPLOADMEDIA(string filename)
        {
            var KEYS = new Dictionary<string, string>
            {
                ["id"] = "WU_FILE_0",
                ["name"] = Path.GetFileName(filename),
                ["type"] = MimeMapping.GetMimeMappingByFile(filename),
                ["lastModifiedDate"] = "Thu Jan 07 2016 20:33:28 GMT+0800 (中国标准时间)",
                ["size"] = File.ReadAllBytes(filename).Length + "",
                ["mediatype"] = "doc"
            };
            KEYS["uploadmediarequest"] = WEB.FormatKeys(@"{""BaseRequest"":{""Uin"":{WXUIN},""Sid"":""{WXSID}"",""Skey"":""{SKEY}"",""DeviceID"":""{DEVICEID}""},""ClientMediaId"":{TIME}363,""TotalLen"":" + KEYS["size"] + @",""StartPos"":0,""DataLen"":" + KEYS["size"] + @",""MediaType"":4}");
            KEYS["webwx_data_ticket"] = WEB["webwx_data_ticket"];
            KEYS["pass_ticket"] = WEB["PASS_TICKET"];

            ShowMessage(System.Reflection.MethodBase.GetCurrentMethod().Name, null);

            var response = WEB.SendUpload(@"POST https://file.wx.qq.com/cgi-bin/mmwebwx-bin/webwxuploadmedia?f=json HTTP/1.1
Host: file.wx.qq.com
Connection: keep-alive
Content-Length: 3386
Origin: https://wx.qq.com
User-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.106 Safari/537.36
Content-Type: multipart/form-data; boundary=----WebKitFormBoundaryuM55vqcAjmXSlVHa
Accept: */*
Referer: https://wx.qq.com/?&lang=zh_CN
Accept-Encoding: gzip, deflate
Accept-Language: zh-CN,zh;q=0.8",
filename, KEYS);
            return _17_Response(response);
        }

        string _17_Response(LxwResponse o)
        {
            return JObject.Parse(o.Value)["MediaId"]?.Value<string>();
        }
    }
}