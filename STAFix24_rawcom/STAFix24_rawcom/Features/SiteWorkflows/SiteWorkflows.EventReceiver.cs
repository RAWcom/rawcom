using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;

namespace Stafix.Features.SiteWorkflows
{


    [Guid("cafa87aa-5643-460a-8b8c-300c2dee77bd")]
    public class WorkflowsEventReceiver : SPFeatureReceiver
    {

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            try
            {
                var site = properties.Feature.Parent as SPSite;

                // swfWyslikZestawienieGodzin
                string workflowTemplateBaseGuid = "b398c228-9469-4f23-986f-3468821729d3";
                string workflowAssociationName = "Wyślij zestawienie godzin";
                Tools.Workflows.AssociateWorflow(site.RootWeb, workflowTemplateBaseGuid, workflowAssociationName);
            }
            catch (Exception ex)
            {
                Tools.ElasticEmail.ReportError(ex, (properties.Feature.Parent as SPSite).Url);
            }
        }

    }
}
