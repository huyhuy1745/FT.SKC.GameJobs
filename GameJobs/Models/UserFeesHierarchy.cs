using System;
using System.Globalization;

namespace FT.NH88.GameJobs.Models
{
    public class UserFeesHierarchy
    {
        public int ID { get; set; }
        public int UnixUpdatedAt { get; set; }
        public int Date { get; set; }
        public long UserID { get; set; }
        public long TotalFees { get; set; }
        public double TotalCommissions { get; set; }
        public bool IsTransferToWallet { get; set; }

        public DateTime GetDate()
        {
            string format = "yyyyMMdd";
            CultureInfo provider = CultureInfo.InvariantCulture;
            return DateTime.ParseExact(this.Date.ToString(), format, provider);
        }
    }
}