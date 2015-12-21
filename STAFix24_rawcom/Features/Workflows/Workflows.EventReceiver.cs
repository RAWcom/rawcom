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
            var site = properties.Feature.Parent as SPSite;
            var web = site.RootWeb;
            //var web = properties.Feature.Parent as SPWeb;

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

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
        }

            //        SPSite site = properties.Feature.Parent as SPSite;

            //using (SPWeb web = properties.Feature.Parent as SPWeb)
            //{
            //    SPList taskList = web.Lists.TryGetList("Workflow Tasks");
            //    SPList historyList = web.Lists.TryGetList("Workflows History");
            //    SPList registrationList = web.Lists.TryGetList("Zadania");
            //    //SPList historyList = web.Lists[Lists.WorkflowHistory];
            //    //SPList registrationList = web.Lists[Lists.Registrations]; 

            //    var existingAssociation = registrationList.WorkflowAssociations.GetAssociationByName(WorkflowTemplates.RegistrationApprovalName_v1, CultureInfo.CurrentCulture);
            //    if (existingAssociation == null)
            //    {
            //        //Create a worklow manager and associate the Course Registration Approval Workflow template
            //        //to our Registrations list.
            //        SPWorkflowManager workflowManager = web.Site.WorkflowManager;
            //        SPWorkflowTemplateCollection templates = workflowManager.GetWorkflowTemplatesByCategory(web, null);
            //        SPWorkflowTemplate template = templates.GetTemplateByBaseID(new Guid(WorkflowTemplates.RegistrationApprovalBaseId_v1));
            //        SPWorkflowAssociation association = SPWorkflowAssociation.CreateListAssociation(template, template.Name, taskList, historyList);
            //        association.AllowManual = true;
            //        association.AutoStartCreate = true;
            //        registrationList.AddWorkflowAssociation(association);
            //        registrationList.Update();
            //        association.Enabled = true;
            //    }
            //}

        //public void ProvisionSiteWorkflow(SPWeb web)
        //{
        //    using (SPSite site = web.Site)
        //    {
        //        using (web)
        //        {
        //            //Bind to lists.
        //            SPList approvalsList = web.Lists["Your Approval List Name Goes Here"];
        //            SPList workflowTasksList = web.Lists["Task List"];
        //            SPList workflowHistoryList = web.Lists["Workflow History"];

        //            //92EF6E3C-8DC1-41bc-AE1D-C5A04A06E028 is the workflow GUID
        //            //from workflow.xml\<Elements>\<Workflow>\[ID].
        //            SPWorkflowTemplate workflowTemplate =
        //              web.WorkflowTemplates[new Guid("b398c228-9469-4f23-986f-3468821729d3")];

        //            //Create workflow association.
        //            SPWorkflowAssociation workflowAssociation =
        //              SPWorkflowAssociation.CreateListAssociation(workflowTemplate,
        //              workflowTemplate.Description, approvalTasksList,
        //              workflowHistoryList);

        //            //Set workflow options.
        //            workflowAssociation.AllowManual = true;
        //            workflowAssociation.AutoStartChange = true;
        //            workflowAssociation.AutoStartCreate = false;

        //            //Add workflow association.
        //            SPWorkflowAssociation workflowAssociationInList =
        //              approvalsList.AddWorkflowAssociation(workflowAssociation);
        //        }
        //        return;

        //    }
        //}

        //public void ProvisionListWorkflow(SPWeb web)
        //{
        //    using (web)
        //    {
        //        //Bind to lists.
        //        SPList approvalsList = web.Lists["Your Approval List Name Goes Here"];
        //        SPList approvalTasksList = web.Lists["Task List"];
        //        SPList workflowHistoryList = web.Lists["Workflow History"];

        //        //92EF6E3C-8DC1-41bc-AE1D-C5A04A06E028 is the workflow GUID
        //        //from workflow.xml\<Elements>\<Workflow>\[ID].
        //        SPWorkflowTemplate workflowTemplate =
        //          web.WorkflowTemplates[new Guid("b398c228-9469-4f23-986f-3468821729d3")];

        //        //Create workflow association.
        //        SPWorkflowAssociation workflowAssociation =
        //          SPWorkflowAssociation.CreateListAssociation(workflowTemplate,
        //          workflowTemplate.Description, approvalTasksList,
        //          workflowHistoryList);

        //        //Set workflow options.
        //        workflowAssociation.AllowManual = true;
        //        workflowAssociation.AutoStartChange = true;
        //        workflowAssociation.AutoStartCreate = false;

        //        //Add workflow association.
        //        SPWorkflowAssociation workflowAssociationInList =
        //          approvalsList.AddWorkflowAssociation(workflowAssociation);
        //    }
        //    return;
        //}
    }
}
