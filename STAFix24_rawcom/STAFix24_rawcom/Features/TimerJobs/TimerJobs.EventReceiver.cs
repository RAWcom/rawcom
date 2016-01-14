using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;

namespace Stafix.Features.TimerJobs
{
    [Guid("82510d7a-5bbc-459f-b5af-043cb7a59461")]
    public class TimerJobsEventReceiver : SPFeatureReceiver
    {
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            try
            {
                var site = properties.Feature.Parent as SPSite;
                Stafix.TimerJobs.WysliZestawienieGodzinTJ.CreateTimerJob(site);
            }
            catch (Exception ex)
            {
                Tools.ElasticEmail.ReportError(ex, (properties.Feature.Parent as SPSite).Url);
            }
        }

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            var site = properties.Feature.Parent as SPSite;
            Stafix.TimerJobs.WysliZestawienieGodzinTJ.DelteTimerJob(site);
        }
    }
}