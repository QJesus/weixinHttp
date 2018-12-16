using HttpSocket;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace demo_win_httpsocket
{
    /// <summary>
    /// 全局信息
    /// </summary>    
    public partial class MainForm
    {
        public MainForm()
        {
            InitializeComponent();

            var tt = MimeMapping.GetMimeMapping(".mp3");
            var tt2 = MimeMapping.GetExtByMime("application/x-cpio");
            //return;

            Load += (s, e) =>
            {
                System.Threading.Tasks.Task.Run(() =>
                {
                    Run();
                });
            };
        }

        private void btnSendFile_Click(object sender, EventArgs e)
        {
            if (lstBoxUser.SelectedItems.Count <= 0)
            {
                MessageBox.Show("请选择用户！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Random r = new Random();
                foreach (var user in lstBoxUser.SelectedItems.Cast<MemberItem>())
                {
                    SendFile(user, openFileDialog.FileName);
                    Thread.Sleep(r.Next(512, 512 * 3));
                }
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

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (lstBoxUser.SelectedItems.Count <= 0)
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

            txtBoxMessage.Clear();
            Random r = new Random();
            foreach (var user in lstBoxUser.SelectedItems.Cast<MemberItem>())
            {
                //发送消息
                SendText(user, text);
                Thread.Sleep(r.Next(512, 512 * 3));
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

    public partial class MainForm : Form
    {
        LxwHttpSocket WEB = new LxwHttpSocket();

        public void Run()
        {
            WEB.Add("DEVICEID", generateDeviceId());
            WEB.Add("APPID", "wx782c26e4c19acffb");
            //第一步
            _1_JSLOGIN();

            //第二个 获取二维码
            var qrcode = _2_QRCODE();
            Invoke((Action)delegate ()
            {
                using (Stream stream = new MemoryStream(qrcode))
                {
                    picErWeiMa.BackgroundImage = Image.FromStream(stream);
                }
            });

            if (_3_LOGIN())
            {
                foreach (Control c in Controls)
                {
                    Invoke((Action)delegate ()
                    {
                        c.Visible = c.GetType() != typeof(PictureBox);
                    });
                }
                //执行第四步
                OpenMain();
            }
        }

        /// <summary>
        /// 打开主界面
        /// </summary>
        private void OpenMain()
        {
            _4_REDIRECT_URL();

            _5_WEBWXINIT();
            Invoke((Action)delegate ()
            {
                Text = NickName + $">>>微信客户端 V2.0";
            });

            _6_WEBWXGETCONTACT();
            Invoke((Action)delegate ()
            {
                foreach (var item in USER_DI.Select(u => u.User))
                {
                    lstBoxUser.Items.Add(item);
                }
            });

            //第七步心跳检测
            _7_SYNCCHECK();
        }

        private void SendFile(MemberItem user, string file)
        {
            string userId = user.ToString().Substring(user.ToString().LastIndexOf('>') + 1);
            _11_SENDFILE(userId, file);
        }

        private void SendText(MemberItem user, string text)
        {
            _10_WEBWXSENDMSG(user.UserName, UserName, text);
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

        //string ReplaceKey(string s)
        //{
        //    s = ReplaceHeaderKey(s);
        //    s = s.Replace("{uuid}", WEB[UUID] + "");

        //    return s;
        //}

        //int DateTimeToStamp(System.DateTime time)
        //{
        //    System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        //    return (int)(time - startTime).TotalSeconds;
        //}

        //string ReplaceHeaderKey(string s)
        //{
        //    s = s.Replace("{webwx_data_ticket}", WEB["webwx_data_ticket"]);
        //    s = s.Replace("{UIN}", WEB[WXUIN] + "");
        //    s = s.Replace("{SID}", WEB[WXSID] + "");
        //    s = s.Replace("{DeviceID}", WEB[DEVICEID] + "");
        //    s = s.Replace("{SKEY}", WEB[SKEY] + "");
        //    s = s.Replace("{time}", DateTimeToStamp(DateTime.Now) + "");
        //    s = s.Replace("{pass_ticket}", WEB[PASS_TICKET]);
        //    s = s.Replace("{SyncKey}", WEB[SYNCKEY] + "");
        //    s = s.Replace("[number]", WEB[NUMBER]);

        //    return s;
        //}
    }
}
