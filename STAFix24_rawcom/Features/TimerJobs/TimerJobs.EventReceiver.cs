using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;

namespace STAFix24_rawcom.Features.TimerJobs
{
    [Guid("5a57172c-8d01-4aab-bc87-a1831e740628")]
    public class TimerJobsEventReceiver : SPFeatureReceiver
    {
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPSite site = properties.Feature.Parent as SPSite;

            try
            {
                STAFix24_rawcom.TimerJobs.WyslijZestawienieGodzinTJ.CreateTimerJob(site);
            }
            catch (Exception ex)
            {
                var result = Tools.ElasticEmail.ReportError(ex, site.Url);
            }
        }

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            var site = properties.Feature.Parent as SPSite;
            try
            {
                STAFix24_rawcom.TimerJobs.WyslijZestawienieGodzinTJ.DelteTimerJob(site);
            }
            catch (Exception ex)
            {
                var result = Tools.ElasticEmail.ReportError(ex, site.Url);
            }
        }
    }
}
