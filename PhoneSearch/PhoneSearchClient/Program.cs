using CsharpHttpHelper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhoneSearchClient
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            HttpHelper helper = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                URL = "http://api.m.taobao.com/rest/api3.do?api=mtop.common.getTimestamp"
            };
            var res = helper.GetHtml(item);
            var obj = JObject.Parse(res.Html);
            var t = obj["data"]["t"].ToString();
            var nowTime = ConvertStringToDateTime(t);
            try
            {
                var endTime = Convert.ToDateTime("2019-07-18");
                if (nowTime < endTime)
                {
                    Application.Run(new Form1());
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(JsonConvert.SerializeObject(e));
            }
        }

        /// <summary>    
        /// 时间戳转为C#格式时间    
        /// </summary>    
        /// <param name=”timeStamp”></param>    
        /// <returns></returns>    
        private static DateTime ConvertStringToDateTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

    }
}
