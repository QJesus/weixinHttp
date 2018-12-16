using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace wx_logic
{
    public partial class WXLogic
    {
        /// <summary>
        /// 查找对应Key的Value
        /// </summary>
        /// <param name="result"></param>
        /// <param name="code"></param>
        /// <param name="regex"></param>
        /// <returns></returns>
        string SearchKey(string result, string code, string regex)
        {
            if (result.IndexOf(code) != -1)
            {
                Regex reg = new Regex(regex);
                Match m = reg.Match(result);
                while (m.Success)
                {
                    return m.Groups[1].Value;
                }
            }

            //没有找到
            throw new Exception("SearchKey not find \"" + code + "\"");
        }

        string SubString(string objValue, string indexStr = "", string lastStr = "", string iDefault = "", bool throwE = false)
        {
            try
            {
                int index = objValue.IndexOf(indexStr);
                if (lastStr != "" && index > -1)
                {
                    objValue = objValue.Remove(0, index);
                    index = objValue.IndexOf(indexStr);
                }
                int last = objValue.IndexOf(lastStr);
                last = last == 0 ? objValue.Length : last;
                if (index > -1 && last > -1)
                {
                    objValue = objValue.Substring(index + indexStr.Length, last - (index + indexStr.Length));
                    return objValue;
                }
                else
                {
                    return iDefault;
                }

            }
            catch (Exception error)
            {
                if (throwE) throw error;
                return iDefault;
            }
        }

        public MemberItem GetUserFromDI(string skey)
        {
            var find = USER_DI.FirstOrDefault(f => f.UserName == skey).User ?? new MemberItem { UserName = skey, };
            return JsonConvert.DeserializeObject<MemberItem>(JsonConvert.SerializeObject(find));
        }

        string generateDeviceId()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("e");
            int count = 15;//15个随机数字
            Random ran = new Random();
            for (int i = 0; i < count; i++)
            {
                int num = (int)(ran.Next(10));
                sb.Append(num);
            }

            return sb.ToString();
        }

        string CreateWeiXinFilesFolder(string folder = "files")
        {
            //存储到本地
            var filesFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folder);
            if (!Directory.Exists(filesFolder))
            {
                Directory.CreateDirectory(filesFolder);
            }
            return filesFolder;
        }
    }
}