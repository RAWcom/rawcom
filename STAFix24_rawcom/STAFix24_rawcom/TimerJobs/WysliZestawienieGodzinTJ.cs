using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.Workflow;

namespace Stafix.TimerJobs
{
    public class WysliZestawienieGodzinTJ : Microsoft.SharePoint.Administration.SPJobDefinition
    {
        public static void CreateTimerJob(SPSite site)
        {
            var timerJob = new WysliZestawienieGodzinTJ(site);
            timerJob.Schedule = new SPDailySchedule
            {
                BeginHour = 8,
                BeginMinute = 0,
                BeginSecond = 0,
                EndSecond = 0,
                EndMinute = 0,
                EndHour = 8,
            };

            timerJob.Update();
        }

        public static void DelteTimerJob(SPSite site)
        {
            site.WebApplication.JobDefinitions
                .OfType<WysliZestawienieGodzinTJ>()
                .Where(i => string.Equals(i.SiteUrl, site.Url, StringComparison.InvariantCultureIgnoreCase))
                .ToList()
                .ForEach(i => i.Delete());
        }

        public WysliZestawienieGodzinTJ()
            : base()
        {

        }

        public WysliZestawienieGodzinTJ(SPSite site)
            : base(string.Format("Rawcom_Wysylka zestawienia godzin ({0})", site.Url), site.WebApplication, null, SPJobLockType.Job)
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
            using (var site = new SPSite(SiteUrl))
            {
                try
                {
                    SPWorkflow wf = Tools.Workflows.StartSiteWorkflow(site, "Wyślij zestawienie godzin");
                    Tools.Logger.LogEvent(site.Url, " workflow.Wyślij zestawienie godzin = " + wf.InternalState.ToString());
                }
                catch (Exception ex)
                {
                    Tools.ElasticEmail.ReportError(ex, site.Url);
                }
            }
        }
    }
}

