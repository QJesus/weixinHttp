using FluorineFx.Json;
using HttpSocket;
using System;
using System.Linq;
using System.Threading;
using System.Xml;

namespace wx_logic
{
    public partial class WXLogic
    {
        LxwHttpSocket WEB = new LxwHttpSocket();

        public void Run(Action<byte[]> qrcodeAction, Action<bool> loginAction, Action<MemberItem[]> usersAction)
        {
            WEB.Add("DEVICEID", generateDeviceId());
            WEB.Add("APPID", "wx782c26e4c19acffb");
            //第一步
            _1_JSLOGIN();

            //第二个 获取二维码
            var qrcode = _2_QRCODE();
            qrcodeAction?.Invoke(qrcode);

            _3_LOGIN(msg =>
            {
                var ok = string.IsNullOrEmpty(msg);
                loginAction?.Invoke(ok);
                if (!ok)
                {
                    return;
                }
                _4_REDIRECT_URL();

                _5_WEBWXINIT();

                var users = _6_WEBWXGETCONTACT();
                usersAction?.Invoke(users);

                //第七步心跳检测
                _7_SYNCCHECK();
            });
        }

        T Xml2Json<T>(string xml, string root = "error")
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            JavaScriptObject obj = new JavaScriptObject();
            foreach (XmlNode node in doc.SelectSingleNode(root).ChildNodes)
            {
                //获取内容
                obj[node.Name] = node.InnerText;
            }

            return JavaScriptConvert.DeserializeObject<T>(JavaScriptConvert.SerializeObject(obj));
        }

        //        string upload_header = @"POST https://file.wx.qq.com/cgi-bin/mmwebwx-bin/webwxuploadmedia?f=json HTTP/1.1
        //Host: file.wx.qq.com
        //Connection: keep-alive
        //Content-Length: 3386
        //Origin: https://wx.qq.com
        //User-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.106 Safari/537.36
        //Content-Type: multipart/form-data; boundary=----WebKitFormBoundaryuM55vqcAjmXSlVHa
        //Accept: */*
        //Referer: https://wx.qq.com/?&lang=zh_CN
        //Accept-Encoding: gzip, deflate
        //Accept-Language: zh-CN,zh;q=0.8";

        //        string upload_body = @"------WebKitFormBoundaryuM55vqcAjmXSlVHa
        //Content-Disposition: form-data; name=""id""
        //
        //WU_FILE_0
        //------WebKitFormBoundaryuM55vqcAjmXSlVHa
        //Content-Disposition: form-data; name=""name""
        //
        //{filename}
        //------WebKitFormBoundaryuM55vqcAjmXSlVHa
        //Content-Disposition: form-data; name=""type""
        //
        //{filetype}
        //------WebKitFormBoundaryuM55vqcAjmXSlVHa
        //Content-Disposition: form-data; name=""lastModifiedDate""
        //
        //Thu Jan 07 2016 20:33:28 GMT+0800 (中国标准时间)
        //------WebKitFormBoundaryuM55vqcAjmXSlVHa
        //Content-Disposition: form-data; name=""size""
        //
        //[CD]
        //------WebKitFormBoundaryuM55vqcAjmXSlVHa
        //Content-Disposition: form-data; name=""mediatype""
        //
        //doc
        //------WebKitFormBoundaryuM55vqcAjmXSlVHa
        //Content-Disposition: form-data; name=""uploadmediarequest""
        //
        //{""BaseRequest"":{""Uin"":{UIN},""Sid"":""{SID}"",""Skey"":""{SKEY}"",""DeviceID"":""{DeviceID}""},""ClientMediaId"":{time}363,""TotalLen"":[CD],""StartPos"":0,""DataLen"":[CD],""MediaType"":4}
        //------WebKitFormBoundaryuM55vqcAjmXSlVHa
        //Content-Disposition: form-data; name=""webwx_data_ticket""
        //
        //{webwx_data_ticket}
        //------WebKitFormBoundaryuM55vqcAjmXSlVHa
        //Content-Disposition: form-data; name=""pass_ticket""
        //
        //{pass_ticket}
        //------WebKitFormBoundaryuM55vqcAjmXSlVHa
        //Content-Disposition: form-data; name=""filename""; filename=""{filename}""
        //Content-Type: {filetype}
        //
        //
        //[FILE]
        //------WebKitFormBoundaryuM55vqcAjmXSlVHa--
        //";
        //private void button1_Click(object sender, EventArgs e)
        //{
        //    object user = lstBoxUser.SelectedItem;
        //    if (user == null)
        //    {
        //        MessageBox.Show("请选择用户！");
        //        return;
        //    }

        //    string userid = user.ToString().Substring(user.ToString().LastIndexOf('>') + 1);



        //    var openFileDialog = new OpenFileDialog();
        //    if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //    {
        //        _11_SENDFILE(userid, openFileDialog.FileName);
        //    }
        //}
        private void SendFile(MemberItem[] users, string file)
        {
            if (users?.Any() != true)
            {
                return;
            }

            Random r = new Random();
            foreach (var user in users)
            {
                string userid = user.ToString().Substring(user.ToString().LastIndexOf('>') + 1);
                _11_SENDFILE(userid, file);
                Thread.Sleep(r.Next(512, 512 * 3));
            }

            //var openFileDialog = new OpenFileDialog();
            //if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    //_11_SENDFILE(userid, openFileDialog.FileName);

            //    lxwHttp lxw = new lxwHttp();
            //    //var response = lxw.SendHeader(ReplaceKey(upload), filePath);
            //    var response = lxw.SendHeader(ReplaceKey(upload_header), ReplaceKey(upload_body), openFileDialog.FileName);

            //    if (response != null)
            //    {
            //        var ret = Encoding.UTF8.GetString(response.Body.ToArray());
            //        MessageBox.Show(ret);
            //    }
            //}

        }

        string ReplaceKey(string s)
        {
            s = ReplaceHeaderKey(s);
            s = s.Replace("{uuid}", WEB[UUID] + "");

            return s;
        }
        int DateTimeToStamp(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
        string ReplaceHeaderKey(string s)
        {
            s = s.Replace("{webwx_data_ticket}", WEB["webwx_data_ticket"]);
            s = s.Replace("{UIN}", WEB[WXUIN] + "");
            s = s.Replace("{SID}", WEB[WXSID] + "");
            s = s.Replace("{DeviceID}", WEB[DEVICEID] + "");
            s = s.Replace("{SKEY}", WEB[SKEY] + "");
            s = s.Replace("{time}", DateTimeToStamp(DateTime.Now) + "");
            s = s.Replace("{pass_ticket}", WEB[PASS_TICKET]);
            s = s.Replace("{SyncKey}", WEB[SYNCKEY] + "");
            s = s.Replace("[number]", WEB[NUMBER]);

            return s;
        }

        private void SendText(MemberItem[] users, string text)
        {
            if (users?.Any() != true || string.IsNullOrEmpty(text))
            {
                return;
            }

            Random r = new Random();
            foreach (var user in users)
            {
                //发送消息
                _10_WEBWXSENDMSG(user.UserName, UserName, text);
                Thread.Sleep(r.Next(512, 512 * 3));
            }
        }

        private void GetUserList()
        {
            //重新获取用户
            _6_WEBWXGETCONTACT();
        }
    }
}
