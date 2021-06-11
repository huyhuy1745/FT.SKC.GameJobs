using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using FT.NH88.GameJobs.Commons;
using FT.NH88.GameJobs.Models;
using Quartz;

namespace FT.NH88.GameJobs.Jobs.FinishedXoSo
{
    public class Job: IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Run(() =>
            {
                DateTime currDate = DateTime.Now.Date;
                Logger.Debug($"ExJob.GetResult - [{currDate:d}] - Start get result lotery");
                
                var sessionResult = Databases.SQL.Lottery.StoredProcedureActor.Instance.GetSessionResult(currDate);
                if (sessionResult == null)
                {
                    Logger.Debug($"ExJob.GetResult - [{currDate:d}] - Cannot find session with this date");
                    return;
                }

                var webClient = new WebClient();
                string result = webClient.DownloadString("https://xskt.com.vn/rss-feed/mien-bac-xsmb.rss");
                XDocument document = XDocument.Parse(result);

                var results = (from descendant in document.Descendants("item")
                    select new RssNews()
                    {
                        Description = descendant.Element("description")?.Value,
                        Title = descendant.Element("title")?.Value,
                        PublicationDate = descendant.Element("pubDate")?.Value
                    }).ToList();

                var currResult = results.FirstOrDefault();
                if (currResult != null)
                {
                    var rs = this.MapResultXsmb(currResult.Description);
                    //insert data to db
                    int resp = Databases.SQL.Lottery.StoredProcedureActor.Instance.UpdateResult(rs);
                    Logger.Debug($"ExJob.GetResult - [{currDate:d}] - End get result lotery - response: {resp}");
                    if(resp != 0)
                        return;
                    
                    resp = Databases.SQL.Lottery.StoredProcedureActor.Instance.FinishSession();
                    Logger.Debug($"ExJob.GetResult - [{currDate:d}] - Finish session - response: {resp}");
                }
            });
        }
        
        private XoSoResult MapResultXsmb(string data)
        {
            try
            {
                if (string.IsNullOrEmpty(data)) return new XoSoResult();

                data = data.Replace("\nÄ\u0090", "");

                var arrStr = data.Split('\n');
                XoSoResult result = new XoSoResult();
                //giai dac biet
                result.PrizeDB = this.GetPrizeFromResult(arrStr[1]);

                //giai nhat
                result.Prize1 = this.GetPrizeFromResult(arrStr[2]);

                //giai hai
                result.Prize2 = this.GetPrizeFromResult(arrStr[3]);

                //giai ba
                result.Prize3 = this.GetPrizeFromResult(arrStr[4]);

                //giai tu
                result.Prize4 = this.GetPrizeFromResult(arrStr[5]);

                //giai nam
                result.Prize5 = this.GetPrizeFromResult(arrStr[6]);

                //giai sau
                result.Prize6 = this.GetPrizeFromResult(arrStr[7]);

                //giai bay
                result.Prize7 = this.GetPrizeFromResult(arrStr[8]);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("ResultJob.MapResultXsmb",ex);
            }

            return new XoSoResult();
        }

        private string GetPrizeFromResult(string data)
        {
            if (string.IsNullOrEmpty(data)) return string.Empty;

            try
            {
                data = data.Replace(" ", "");

                var arrStr = data.Split(':');
                if (arrStr != null && arrStr.Length >= 2)
                {
                    return arrStr[1].Replace("-", ",");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("ResultJob.GetPrizeFromResult",ex);
            }

            return string.Empty;
        }
    }
}