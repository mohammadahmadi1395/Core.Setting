using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gostar.Common
{
    public class GDateTime
    {
        public System.DateTime MinValue { get; set; }
        public System.DateTime MaxValue { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }

        public System.DateTime Now { get; set; }
        public System.DateTime UtcNow { get; set; }


        public GDateTime()
        {
            CultureInfo ci = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
            Now = System.DateTime.Now.Date + System.DateTime.Now.TimeOfDay;
            UtcNow = System.DateTime.UtcNow.Date + System.DateTime.UtcNow.TimeOfDay;
            this.Year = System.DateTime.Now.Year;
            this.Month = System.DateTime.Now.Month;
            this.Day = System.DateTime.Now.Day;
            this.Hour = System.DateTime.Now.Hour;
            this.Minute = System.DateTime.Now.Minute;
            this.Second = System.DateTime.Now.Second;
            this.MaxValue = System.DateTime.Parse("9999-12-31T00:00:00");
            this.MinValue = System.DateTime.Parse("0001-01-01T00:00:00");
        }

        /// <summary>
        /// False To MinValue , True To MaxValue , Nothing To NowDate
        /// </summary>
        /// <param name="MaxValue"></param>
        /// <returns></returns>
        public static System.DateTime Conversation(bool? MaxValue = null)
        {
            CultureInfo ci = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
            GDateTime oDate = new GDateTime();

            if (System.DateTime.MinValue.ToString() == "01/01/0001 12:00:00 ق.ظ" || System.DateTime.MaxValue.ToString() == "13/10/9378 12:00:00 ق.ظ")
            {
                oDate.ExMaker("Invalid Calture Seleted");
            }
            if (MaxValue == null)
            {
                return System.DateTime.Now;
            }
            else if (MaxValue == true)
            {
                oDate.MaxValue = System.DateTime.Parse("9999-12-31T00:00:00");
                return oDate.MaxValue;
            }
            else
            {
                oDate.MinValue = System.DateTime.Parse("0001-01-01T00:00:00");
                return oDate.MinValue;
            }
        }
        private Exception ExMaker(string Message = null, string Parameter = null)
        {
            throw new System.ArgumentException(Message, Parameter);
           
        }
    }
}


