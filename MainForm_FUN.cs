using FluorineFx.Json;
using HttpSocket;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace demo_win_httpsocket
{
    partial class MainForm
    {
        void _ShowMessage(string msg, MessageObject obj = null)
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
                    row = $"noJson\t{msg}";
                }
                else
                {
                    row = $"{GetUserFromDI(obj.FromUserName)}>{GetUserFromDI(obj.ToUserName)}\t{msg}";
                    if (obj.MsgType != 51)  // 51 没有内容，可能是心跳包
                    {
                        Task.Factory.StartNew((json) =>
                        {
                            var log = JsonConvert.DeserializeObject<MessageObject>(json as string);
                            log.FromUserName = GetUserFromDI(log.FromUserName)?.ToString() ?? log.FromUserName;
                            log.ToUserName = GetUserFromDI(log.ToUserName)?.ToString() ?? log.ToUserName;
                            System.IO.File.AppendAllText("message.json", JsonConvert.SerializeObject(log, new JsonSerializerSettings
                            {
                                DefaultValueHandling = DefaultValueHandling.Ignore,
                                Formatting = Newtonsoft.Json.Formatting.Indented,
                                ContractResolver = new EmptyToNullStringResolver(),
                            }), System.Text.Encoding.UTF8);
                        }, JsonConvert.SerializeObject(obj));
                    }
                }
                lstMessage.Items.Insert(0, $"{DateTime.Now:MM-dd HH:mm:ss}\t{row}");
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