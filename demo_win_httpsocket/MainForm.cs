using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
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

            wx.MessageStream.Subscribe(data =>
            {
                var obj = data.obj;
                var msg = data.msg;
                var users = wx.GetUsers();
                var from = obj == null ? null : users.FirstOrDefault(x => x.UserName == obj.FromUserName);
                var to = obj == null ? null : users.FirstOrDefault(x => x.UserName == obj.ToUserName);
                var row = obj == null ? $"noJson\t{msg}" : $"{from}>{to}\t{msg}";
                Invoke((Action)delegate ()
                {
                    lstMessage.Items.Insert(0, row);
                    lstMessage.SelectedIndex = 0;
                });

                if (obj != null && obj.MsgType != 51)  // 51 没有内容，可能是心跳包
                {
                    var log = JsonConvert.DeserializeObject<MessageObject>(JsonConvert.SerializeObject(obj));
                    log.FromUserName = from?.ToString() ?? log.FromUserName;
                    log.ToUserName = to?.ToString() ?? log.ToUserName;
                    File.AppendAllText("message.json", JsonConvert.SerializeObject(log, new JsonSerializerSettings
                    {
                        DefaultValueHandling = DefaultValueHandling.Ignore,
                        Formatting = Formatting.Indented,
                        ContractResolver = new EmptyToNullStringResolver(),
                    }), System.Text.Encoding.UTF8);
                }
            });

            Load += (s, e) =>
            {
                Task.Run(() => Login());
                Task.Run(() =>
                {
                    var random = new Random();
                    string lastMessage = null;
                    DateTime lastNotifyTime = DateTime.MinValue;
                    while (true)
                    {
                        var user = lstBoxUser.Items.Cast<MemberItem>().FirstOrDefault(o => o.NickName == txtNickName.Text);
                        if (user != null)
                        {
                            string train_date = txt_train_date.Text;
                            string from_station = txt_from_station.Text;
                            string to_station = txt_to_station.Text;
                            string purpose_codes = cbStudent.Checked ? "0X00" : "ADULT";
                            var ts = kyfw12306.TrainsDetails(train_date, from_station, to_station, purpose_codes).Where(t =>
                            {
                                var code = t.GetType().GetProperty("车次")?.GetValue(t)?.ToString();
                                return txt_station_train_code.Text.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).Any(o => string.Equals(o, code, StringComparison.OrdinalIgnoreCase));
                            })
                            .Select(t =>
                            {
                                var ps = t.GetType().GetProperties();
                                return string.Join("\r\n", ps.Select(p => $"{p.Name}: {p.GetValue(t)}"));
                            }).ToArray();

                            var msg = string.Join(Environment.NewLine, ts);
                            if ((cbChanged.Checked && msg != lastMessage) || (cbEveryHour.Checked && DateTime.Now - lastNotifyTime >= TimeSpan.FromHours(1)))
                            {
                                foreach (var text in ts)
                                {
                                    wx.SendText(user, text);
                                }
                                lastMessage = msg;
                                lastNotifyTime = DateTime.Now;
                            }
                        }
                        Thread.Sleep(TimeSpan.FromSeconds(3 + random.Next(0, 7)));
                    }
                });
            };
        }

        private void Login()
        {
            var qrcode = wx.GetQRCode();
            Invoke((Action)delegate ()
            {
                using (Stream stream = new MemoryStream(qrcode))
                {
                    picErWeiMa.BackgroundImage = Image.FromStream(stream);
                }
            });
            if (wx.DoLogin())
            {
                Invoke((Action)delegate ()
                {
                    Text = wx.LoginUser.NickName + $">>>微信客户端 V2.0";
                    foreach (Control c in Controls)
                    {
                        c.Visible = c.GetType() != typeof(PictureBox);
                    }
                    foreach (var item in wx.GetUsers())
                    {
                        lstBoxUser.Items.Add(item);
                    }
                });
            }
        }

        private void BtnSendFile_Click(object sender, EventArgs e)
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

        private void BtnSend_Click(object sender, EventArgs e)
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

        private void TxtBoxMessage_KeyUp(object sender, KeyEventArgs e)
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
                    BtnSend_Click(sender, e);
                }
            }
        }
    }

    public class EmptyToNullStringResolver : DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            return type.GetProperties().Select(p =>
            {
                var jp = base.CreateProperty(p, memberSerialization);
                jp.ValueProvider = new EmptyToNullStringValueProvider(p);
                return jp;
            }).ToList();
        }
    }

    public class EmptyToNullStringValueProvider : IValueProvider
    {
        PropertyInfo propertyInfo;
        public EmptyToNullStringValueProvider(PropertyInfo memberInfo)
        {
            propertyInfo = memberInfo;
        }

        public object GetValue(object target)
        {
            object result = propertyInfo.GetValue(target);
            if (propertyInfo.PropertyType == typeof(string) && (string)result == string.Empty)
            {
                result = null;
            }
            return result;
        }

        public void SetValue(object target, object value)
        {
            propertyInfo.SetValue(target, value);
        }
    }
}