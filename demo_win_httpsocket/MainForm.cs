using Newtonsoft.Json;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using wx_logic;

namespace demo_win_httpsocket
{
    /// <summary>
    /// 全局信息
    /// </summary>    
    public partial class MainForm : Form
    {
        private readonly WXLogic wx = new WXLogic();

        public MainForm()
        {
            InitializeComponent();

            Load += (s, e) =>
            {
                wx.QRCodeStream.Subscribe(qrcode =>
                {
                    Invoke((Action)delegate ()
                    {
                        using (Stream stream = new MemoryStream(qrcode))
                        {
                            picErWeiMa.BackgroundImage = Image.FromStream(stream);
                        }
                    });
                });

                wx.UsersStream.Subscribe(users =>
                {
                    Invoke((Action)delegate ()
                    {
                        Text = wx.NickName + $">>>微信客户端 V2.0";
                        foreach (Control c in Controls)
                        {
                            c.Visible = c.GetType() != typeof(PictureBox);
                        }
                        foreach (var item in users)
                        {
                            lstBoxUser.Items.Add(item);
                        }
                    });
                });

                wx.MessageStream.Subscribe(((string msg, MessageObject obj) data) =>
                {
                    var from = data.obj == null ? null : wx.GetUserFromDI(data.obj.FromUserName);
                    var to = data.obj == null ? null : wx.GetUserFromDI(data.obj.ToUserName);
                    var row = data.obj == null ? $"noJson\t{data.msg}" : $"{from}>{to}\t{data.msg}";
                    Invoke((Action)delegate ()
                    {
                        lstMessage.Items.Insert(0, row);
                        lstMessage.SelectedIndex = 0;
                    });

                    if (data.obj != null && data.obj.MsgType != 51)  // 51 没有内容，可能是心跳包
                    {
                        Task.Factory.StartNew((json) =>
                        {
                            var log = JsonConvert.DeserializeObject<MessageObject>(json as string);
                            log.FromUserName = from?.ToString() ?? log.FromUserName;
                            log.ToUserName = to?.ToString() ?? log.ToUserName;
                            File.AppendAllText("message.json", JsonConvert.SerializeObject(log, new JsonSerializerSettings
                            {
                                DefaultValueHandling = DefaultValueHandling.Ignore,
                                Formatting = Formatting.Indented,
                                ContractResolver = new EmptyToNullStringResolver(),
                            }), System.Text.Encoding.UTF8);
                        }, JsonConvert.SerializeObject(data.obj));
                    }
                });

                Task.Run(() => wx.Run());
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
                    wx.SendFile(user, openFileDialog.FileName);
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
                wx.SendText(user, text);
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
}