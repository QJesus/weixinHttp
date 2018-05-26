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

            btnSendFile.Hide();

            // 获取号码
            lblSSCMsg.Text = $"本次获取号码时间为 {DateTime.Now:HH:mm:ss} 下次获取号码的时间为 {DateTime.Now.AddMinutes(5):HH:mm:ss}";
            timer1_Tick(this, null);
            var timer = new Timer { Interval = 5 * 60 * 1000, };
            timer.Tick += (s, e) =>
            {
                lblSSCMsg.Text = $"本次获取号码时间为 {DateTime.Now:HH:mm:ss} 下次获取号码的时间为 {DateTime.Now.AddMinutes(5):HH:mm:ss}";
                timer1_Tick(s, e);
            };
            timer.Start();
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
                MessageBox.Show("请选择用户！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var text = txtBoxMessage.Text?.Trim();
            if (string.IsNullOrEmpty(text))
            {
                MessageBox.Show("请输入信息！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //发送消息
            _10_WEBWXSENDMSG(user.UserName, UserName, text);

            txtBoxMessage.Text = "";
        }

        private void btnGetUserList_Click(object sender, EventArgs e)
        {
            //重新获取用户
            _6_WEBWXGETCONTACT();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate, UseProxy = false, }))
            {
                // var url = @"http://trend.caipiao.163.com/cqssc/";
                // var url = $@"http://caipiao.163.com/award/daily_refresh.html?cache={DateTime.Now.Ticks}&gameEn=ssc&selectDate=2&date={DateTime.Now.AddDays(-1):yyyyMMdd}";  // 昨天
                var url = $@"http://caipiao.163.com/award/daily_refresh.html?cache={DateTime.Now.Ticks}&gameEn=ssc&selectDate=1&date={DateTime.Now:yyyyMMdd}"; // 今天
                var response = client.GetAsync(url).Result;
                var html = response.Content.ReadAsStringAsync().Result;
                // Console.WriteLine(html);

                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(html);
                // var navNode = doc.GetElementbyId("cpdata");
                var navNode = doc.DocumentNode.SelectSingleNode("//table");
                // Console.WriteLine(navNode.InnerHtml);

                var trs = navNode?.Elements("tr")?.ToArray() ?? new HtmlAgilityPack.HtmlNode[0];
                if (trs.Length <= 0)
                {
                    return;
                }

                var array = trs.SelectMany(tr =>
                {
                    return tr.Elements("td").SelectMany(l1 =>
                    {
                        var d1s = l1.Elements("td").Select(l2 =>
                        {
                            var period = l2.GetAttributeValue("data-period", "");
                            var award = l2.GetAttributeValue("data-win-number", "")?.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                            return new Data { Period = period, Award = award, Sended = false };
                        });
                        var d2 = new Data
                        {
                            Period = l1.GetAttributeValue("data-period", ""),
                            Award = l1.GetAttributeValue("data-win-number", "")?.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray(),
                            Sended = false
                        };
                        return d1s.Concat(new[] { d2 });
                    });
                }).Where(o => !string.IsNullOrEmpty(o.Period)).OrderByDescending(o => o.Period).ToArray();
                if (array.Length <= 0)
                {
                    return;
                }

                if (System.IO.File.Exists("data.json"))
                {
                    var content = System.IO.File.ReadAllText("data.json", System.Text.Encoding.UTF8);
                    var cache = JsonConvert.DeserializeObject<Data[]>(content);
                    foreach (var item in array)
                    {
                        item.Sended = cache.FirstOrDefault(o => o.Period == item.Period)?.Sended ?? false;
                    }
                }
                array = array.Where(a => a.Award.Length > 0).Concat(array.Where(a => a.Award.Length <= 0)).Take(5).ToArray();

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

        private void txtBoxMessage_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (e.Alt)
                {
                    // 换行, 光标移动到最后
                    txtBoxMessage.Text = txtBoxMessage.Text + Environment.NewLine;
                    txtBoxMessage.SelectionLength = 0;
                    txtBoxMessage.SelectionStart = int.MaxValue;
                }
                else
                {
                    btnSend_Click(sender, e);
                }
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
