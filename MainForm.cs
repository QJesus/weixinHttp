using FluorineFx.Json;
using HttpSocket;
using ImageProcessor;
using ImageProcessor.Imaging;
using ImageProcessor.Imaging.Formats;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Windows.Forms;
using System.Xml;

namespace demo_win_httpsocket
{
    /// <summary>
    /// 全局信息
    /// </summary>    
    public partial class MainForm : Form
    {
        LxwHttpSocket WEB = new LxwHttpSocket();

        public MainForm()
        {
            InitializeComponent();

            var tt = MimeMapping.GetMimeMapping(".mp3");
            var tt2 = MimeMapping.GetExtByMime("application/x-cpio");
            //return;

            this.Load += MainForm_Load;
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            WEB.Add("DEVICEID", generateDeviceId());
            WEB.Add("APPID", "wx782c26e4c19acffb");
            //第一步
            _1_JSLOGIN();

            //第二个 获取二维码
            _2_QRCODE();

            _3_LOGIN();
        }
        /// <summary>
        /// 打开主界面
        /// </summary>
        private void OpenMain()
        {
            foreach (Control c in this.Controls)
            {
                if (c.GetType() == typeof(PictureBox))
                    c.Visible = false;
                else
                    c.Visible = true;
            }

            _4_REDIRECT_URL();

            _5_WEBWXINIT();
            _6_WEBWXGETCONTACT();


            //第七步心跳检测
            _7_SYNCCHECK();
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
        private void btnSendFile_Click(object sender, EventArgs e)
        {
            object user = lstBoxUser.SelectedItem;
            if (user == null)
            {
                MessageBox.Show("请选择用户！");
                return;
            }

            string userid = user.ToString().Substring(user.ToString().LastIndexOf('>') + 1);



            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _11_SENDFILE(userid, openFileDialog.FileName);
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

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (!(lstBoxUser.SelectedItem is MemberItem user))
            {
                MessageBox.Show("请选择用户！");
                return;
            }

            if (txtBoxMessage.Text == "")
            {
                MessageBox.Show("请输入信息！");
                return;
            }

            //发送消息
            _10_WEBWXSENDMSG(user.UserName, UserName, txtBoxMessage.Text);

            txtBoxMessage.Text = "";
        }

        private void btnGetUserList_Click(object sender, EventArgs e)
        {
            //重新获取用户
            _6_WEBWXGETCONTACT();
        }

        private void MainForm_Load_1(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate, UseProxy = false, }))
            {
                var url = @"http://trend.caipiao.163.com/cqssc/";
                var response = client.GetAsync(url).Result;
                var html = response.Content.ReadAsStringAsync().Result;
                // Console.WriteLine(html);

                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(html);
                var navNode = doc.GetElementbyId("cpdata");
                // Console.WriteLine(navNode.InnerHtml);

                var trs = navNode?.Elements("tr")?.ToArray() ?? new HtmlAgilityPack.HtmlNode[0];
                if (trs.Length <= 0)
                {
                    return;
                }

                Array.Reverse(trs);
                var array = trs.Select(o =>
                {
                    var period = o.GetAttributeValue("data-period", "");
                    var award = o.GetAttributeValue("data-award", "");
                    return new Data { Period = period, Award = award.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray(), Sended = false, };
                }).ToArray();
                if (System.IO.File.Exists("data.json"))
                {
                    var content = System.IO.File.ReadAllText("data.json", System.Text.Encoding.UTF8);
                    array = array.Join(JsonConvert.DeserializeObject<Data[]>(content), a => a.Period, b => b.Period, (a, b) =>
                    {
                        a.Sended = b.Sended;
                        return a;
                    }).ToArray();
                }
                array = array.Take(5).ToArray();

                if (lstBoxUser.SelectedItem is MemberItem user)
                {
                    if (array.Any(o => !o.Sended))
                    {
                        var file = Image.FromFile(@"asserts\template.jpg");
                        var image = new ImageFactory().Load(file).Resize(new ResizeLayer(new Size { Width = file.Width, Height = file.Height, }, ResizeMode.Pad));
                        for (var i = 0; i < array.Length; i++)
                        {
                            var item = array[i];
                            image = image.Watermark(new TextLayer
                            {
                                Position = new Point { X = 36, Y = 160 * (i + 1) + 58, },
                                FontColor = Color.Black,
                                Text = $"{item.Period} 期",
                                DropShadow = false,
                                FontSize = 50,
                                Vertical = false,
                                Opacity = 70,
                                Style = FontStyle.Bold,
                            }).Watermark(new TextLayer
                            {
                                Position = new Point { X = 480 - 36, Y = 160 * (i + 1) + 58, },
                                FontColor = Color.Black,
                                Text = $"{string.Join("   ", item.Award)}",
                                DropShadow = false,
                                FontSize = 50,
                                Vertical = false,
                                Opacity = 70,
                                Style = FontStyle.Bold,
                            });

                            item.Sended = true;
                        }
                        using (var ms = new MemoryStream())
                        using (image)
                        {
                            image.Format(new PngFormat()).Image.Save("x.png");
                        }

                        _11_SENDFILE(user.UserName, "x.png");
                    }
                }
                System.IO.File.WriteAllText("data.json", JsonConvert.SerializeObject(array), System.Text.Encoding.UTF8);
            }
        }
    }

    public class Data
    {
        public string Period { get; set; }
        public int[] Award { get; set; }
        public bool Sended { get; set; }
    }
}
