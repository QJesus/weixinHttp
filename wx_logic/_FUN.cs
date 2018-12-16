using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace wx_logic
{
    public partial class WXLogic
    {
        private void _ShowMessage(string msg, MessageObject obj) => MessageStream.OnNext((msg, obj));

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