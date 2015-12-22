using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Workflow;
using System.Globalization;

namespace STAFix24_rawcom.Features.Workflows
{
    [Guid("cafc8782-306b-4e44-bdf0-988c2c28ea4b")]
    public class WorkflowsEventReceiver : SPFeatureReceiver
    {
        private string workflowTemplateBaseGuid = "b398c228-9469-4f23-986f-3468821729d3";
        private string workflowAssociationName = "Wyślij zestawienie godzin";
        private string workFlowHistoryListName = "Workflow History";
        private string workFlowTaskListName = "Workflow Tasks";

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {

            SPSite site = properties.Feature.Parent as SPSite;
            SPWeb web = site.RootWeb;

            try
            {
                SPWorkflowTemplateCollection workflowTemplates = web.WorkflowTemplates;
                SPWorkflowTemplate workflowTemplate = workflowTemplates.GetTemplateByBaseID(new Guid(workflowTemplateBaseGuid));

                if (workflowTemplate != null)
                {
                    // Create the workflow association
                    SPList taskList = web.Lists[workFlowTaskListName];
                    SPList historyList = web.Lists[workFlowHistoryListName];
                    SPWorkflowAssociation workflowAssociation = web.WorkflowAssociations.GetAssociationByName(workflowAssociationName, CultureInfo.InvariantCulture);

                    if (workflowAssociation == null)
                    {
                        workflowAssociation = SPWorkflowAssociation.CreateWebAssociation(workflowTemplate, workflowAssociationName, taskList, historyList);
                        workflowAssociation.AllowManual = true;
                        //workflowAssociation.Enabled = true;
                        web.WorkflowAssociations.Add(workflowAssociation);
                    }
                }
            }
            catch (Exception ex)
            {
	            var result = Tools.ElasticEmail.ReportError(ex, site.Url);
            }
        }

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
        }
    }
}
