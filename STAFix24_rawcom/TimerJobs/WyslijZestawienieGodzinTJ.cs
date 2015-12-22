using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.Workflow;
using System.Diagnostics;

namespace STAFix24_rawcom.TimerJobs
{
    class WyslijZestawienieGodzinTJ : Microsoft.SharePoint.Administration.SPJobDefinition
    {
        public static void CreateTimerJob(SPSite site)
        {
            var timerJob = new WyslijZestawienieGodzinTJ(site);
            timerJob.Schedule = new SPDailySchedule
            {
                BeginHour = 8,
                BeginMinute = 0,
                BeginSecond = 0,
                EndSecond = 15,
                EndMinute = 0,
                EndHour = 8
            };

            timerJob.Update();
        }

        public static void DelteTimerJob(SPSite site)
        {
            site.WebApplication.JobDefinitions
                .OfType<WyslijZestawienieGodzinTJ>()
                .Where(i => string.Equals(i.SiteUrl, site.Url, StringComparison.InvariantCultureIgnoreCase))
                .ToList()
                .ForEach(i => i.Delete());
        }

        public WyslijZestawienieGodzinTJ()
            : base()
        {

        }

        public WyslijZestawienieGodzinTJ(SPSite site)
            : base(string.Format("Rawcom_Zestawienie godzin ({0})", site.Url), site.WebApplication, null, SPJobLockType.Job)
        {
            Title = Name;
            SiteUrl = site.Url;
        }

        public string SiteUrl
        {
            get { return (string)this.Properties["SiteUrl"]; }
            set { this.Properties["SiteUrl"] = value; }
        }

        public override void Execute(Guid targetInstanceId)
        {
            try
            {
                using (var site = new SPSite(SiteUrl))
                {
                    Tools.Workflows.StartSiteWorkflow(site, "Wyślij zestawienie godzin");
                    Debug.WriteLine("Workflow initiated: " + Title);
                }
            }
            catch (Exception ex)
            {
                var result = Tools.ElasticEmail.ReportError(ex, this.SiteUrl);
            }
        }

    }
}
