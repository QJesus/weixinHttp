using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
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
                System.Threading.Tasks.Task.Run(() =>
                {
                    wx.Run
                    (
                        qrcode =>
                        {
                            Invoke((Action)delegate ()
                            {
                                using (Stream stream = new MemoryStream(qrcode))
                                {
                                    picErWeiMa.BackgroundImage = Image.FromStream(stream);
                                }
                            });
                        },
                        users =>
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
                        }
                    );
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