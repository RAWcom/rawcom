using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Reflection;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;

namespace Workflows.swfWyslijZestawienieGodzin
{
    public sealed partial class swfWyslijZestawienieGodzin
    {
        #region Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCode]
        private void InitializeComponent()
        {
            this.CanModifyActivities = true;
            System.Workflow.ComponentModel.ActivityBind activitybind1 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind2 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind3 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind4 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind5 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind6 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Activities.CodeCondition codecondition1 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition2 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition3 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition4 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.ComponentModel.ActivityBind activitybind7 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind8 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind9 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Runtime.CorrelationToken correlationtoken1 = new System.Workflow.Runtime.CorrelationToken();
            System.Workflow.ComponentModel.ActivityBind activitybind10 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind11 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind12 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Activities.CodeCondition codecondition5 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.ComponentModel.ActivityBind activitybind13 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind14 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind15 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind16 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind17 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind18 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind19 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.Activities.CodeCondition codecondition6 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.ComponentModel.ActivityBind activitybind21 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind20 = new System.Workflow.ComponentModel.ActivityBind();
            this.cmdPrzygotujTresc_P_D = new System.Workflow.Activities.CodeActivity();
            this.cmdUstawTytulSekcji_P_D = new System.Workflow.Activities.CodeActivity();
            this.cmdPrzygotujTresc_P_T = new System.Workflow.Activities.CodeActivity();
            this.cmdUstawTytulSekcji_P_T = new System.Workflow.Activities.CodeActivity();
            this.cmdPrzygotujTresc_P_M = new System.Workflow.Activities.CodeActivity();
            this.cmdUstawTytulSekcji_P_M = new System.Workflow.Activities.CodeActivity();
            this.ifRaport_P_Dzienny = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifRaport_P_Tygodniowy = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifRaport_P_Miesieczny = new System.Workflow.Activities.IfElseBranchActivity();
            this.OkreślRodzajRaportu_P_regular = new System.Workflow.Activities.IfElseActivity();
            this.cmdPrzygotujTresc_P_O = new System.Workflow.Activities.CodeActivity();
            this.cmdUstawTytulSekcji_P_O = new System.Workflow.Activities.CodeActivity();
            this.Else = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifRaport_P_Zamykajacy = new System.Workflow.Activities.IfElseBranchActivity();
            this.OkreślRodzajRaportu_P = new System.Workflow.Activities.IfElseActivity();
            this.cmdZainicjujRozliczenieProjektu = new System.Workflow.Activities.CodeActivity();
            this.cmdAktualizujSygnature_TrybTestowy = new System.Workflow.Activities.CodeActivity();
            this.cmdZmodyfikujOdiorcow = new System.Workflow.Activities.CodeActivity();
            this.cmdAktualizujSygnature = new System.Workflow.Activities.CodeActivity();
            this.logRodzajRaportuS3 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.cmdPrzygotujTresc_S_D = new System.Workflow.Activities.CodeActivity();
            this.logRodzajRaportuS2 = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.cmdPrzygotujTresc_S_T = new System.Workflow.Activities.CodeActivity();
            this.logRodzajRaportuS = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.cmdPrzygotujTresc_S_M = new System.Workflow.Activities.CodeActivity();
            this.RaportowanieWynikuDlaProjektu = new System.Workflow.Activities.SequenceActivity();
            this.Else_TrybTestowy = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifTrybProdukcyjny = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifRaport_S_Dzienny = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifRaport_S_Tygodniowy = new System.Workflow.Activities.IfElseBranchActivity();
            this.ifRaport_S_Miesieczny = new System.Workflow.Activities.IfElseBranchActivity();
            this.whileProjekt = new System.Workflow.Activities.WhileActivity();
            this.logWiadomoscWyslana = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.sendRaportDoKlienta = new Microsoft.SharePoint.WorkflowActions.SendEmail();
            this.OkreśćTrybPracy = new System.Workflow.Activities.IfElseActivity();
            this.cmdPrzygotujWiadomosc = new System.Workflow.Activities.CodeActivity();
            this.OkreślRodzajRaportu_S = new System.Workflow.Activities.IfElseActivity();
            this.sekObsługaProjektu = new System.Workflow.Activities.SequenceActivity();
            this.cmdListaProjektowByKlient = new System.Workflow.Activities.CodeActivity();
            this.ifTrescDoWyslania = new System.Workflow.Activities.IfElseBranchActivity();
            this.sequenceActivity1 = new System.Workflow.Activities.SequenceActivity();
            this.seqRozliczenieGodzinSerwisowych = new System.Workflow.Activities.SequenceActivity();
            this.seqRozliczenieGodzinProjektowych = new System.Workflow.Activities.SequenceActivity();
            this.logErrorMessage = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.cmdErrorHandler = new System.Workflow.Activities.CodeActivity();
            this.JeżeliJestTreśćDoWysłania = new System.Workflow.Activities.IfElseActivity();
            this.PrzygotwanieTreściDoWysyłki = new System.Workflow.Activities.ParallelActivity();
            this.logKlient = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
            this.cmdZainicjujRozliczenieKlienta = new System.Workflow.Activities.CodeActivity();
            this.faultHandlerActivity1 = new System.Workflow.ComponentModel.FaultHandlerActivity();
            this.RaportowanieWynikówDlaKlienta = new System.Workflow.Activities.SequenceActivity();
            this.faultHandlersActivity1 = new System.Workflow.ComponentModel.FaultHandlersActivity();
            this.sendReport = new Microsoft.SharePoint.WorkflowActions.SendEmail();
            this.whileKlient = new System.Workflow.Activities.WhileActivity();
            this.getListaKlientow = new System.Workflow.Activities.CodeActivity();
            this.cmdZainicjujRaportDlaAdmina = new System.Workflow.Activities.CodeActivity();
            this.onWorkflowActivated1 = new Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated();
            // 
            // cmdPrzygotujTresc_P_D
            // 
            this.cmdPrzygotujTresc_P_D.Name = "cmdPrzygotujTresc_P_D";
            // 
            // cmdUstawTytulSekcji_P_D
            // 
            this.cmdUstawTytulSekcji_P_D.Name = "cmdUstawTytulSekcji_P_D";
            // 
            // cmdPrzygotujTresc_P_T
            // 
            this.cmdPrzygotujTresc_P_T.Name = "cmdPrzygotujTresc_P_T";
            // 
            // cmdUstawTytulSekcji_P_T
            // 
            this.cmdUstawTytulSekcji_P_T.Name = "cmdUstawTytulSekcji_P_T";
            // 
            // cmdPrzygotujTresc_P_M
            // 
            this.cmdPrzygotujTresc_P_M.Name = "cmdPrzygotujTresc_P_M";
            // 
            // cmdUstawTytulSekcji_P_M
            // 
            this.cmdUstawTytulSekcji_P_M.Name = "cmdUstawTytulSekcji_P_M";
            // 
            // ifRaport_P_Dzienny
            // 
            this.ifRaport_P_Dzienny.Activities.Add(this.cmdUstawTytulSekcji_P_D);
            this.ifRaport_P_Dzienny.Activities.Add(this.cmdPrzygotujTresc_P_D);
            this.ifRaport_P_Dzienny.Name = "ifRaport_P_Dzienny";
            // 
            // ifRaport_P_Tygodniowy
            // 
            this.ifRaport_P_Tygodniowy.Activities.Add(this.cmdUstawTytulSekcji_P_T);
            this.ifRaport_P_Tygodniowy.Activities.Add(this.cmdPrzygotujTresc_P_T);
            this.ifRaport_P_Tygodniowy.Name = "ifRaport_P_Tygodniowy";
            // 
            // ifRaport_P_Miesieczny
            // 
            this.ifRaport_P_Miesieczny.Activities.Add(this.cmdUstawTytulSekcji_P_M);
            this.ifRaport_P_Miesieczny.Activities.Add(this.cmdPrzygotujTresc_P_M);
            this.ifRaport_P_Miesieczny.Name = "ifRaport_P_Miesieczny";
            // 
            // OkreślRodzajRaportu_P_regular
            // 
            this.OkreślRodzajRaportu_P_regular.Activities.Add(this.ifRaport_P_Miesieczny);
            this.OkreślRodzajRaportu_P_regular.Activities.Add(this.ifRaport_P_Tygodniowy);
            this.OkreślRodzajRaportu_P_regular.Activities.Add(this.ifRaport_P_Dzienny);
            this.OkreślRodzajRaportu_P_regular.Name = "OkreślRodzajRaportu_P_regular";
            // 
            // cmdPrzygotujTresc_P_O
            // 
            this.cmdPrzygotujTresc_P_O.Name = "cmdPrzygotujTresc_P_O";
            // 
            // cmdUstawTytulSekcji_P_O
            // 
            this.cmdUstawTytulSekcji_P_O.Name = "cmdUstawTytulSekcji_P_O";
            // 
            // Else
            // 
            this.Else.Activities.Add(this.OkreślRodzajRaportu_P_regular);
            this.Else.Name = "Else";
            // 
            // ifRaport_P_Zamykajacy
            // 
            this.ifRaport_P_Zamykajacy.Activities.Add(this.cmdUstawTytulSekcji_P_O);
            this.ifRaport_P_Zamykajacy.Activities.Add(this.cmdPrzygotujTresc_P_O);
            this.ifRaport_P_Zamykajacy.Name = "ifRaport_P_Zamykajacy";
            // 
            // OkreślRodzajRaportu_P
            // 
            this.OkreślRodzajRaportu_P.Activities.Add(this.ifRaport_P_Zamykajacy);
            this.OkreślRodzajRaportu_P.Activities.Add(this.Else);
            this.OkreślRodzajRaportu_P.Name = "OkreślRodzajRaportu_P";
            // 
            // cmdZainicjujRozliczenieProjektu
            // 
            this.cmdZainicjujRozliczenieProjektu.Name = "cmdZainicjujRozliczenieProjektu";
            // 
            // cmdAktualizujSygnature_TrybTestowy
            // 
            this.cmdAktualizujSygnature_TrybTestowy.Name = "cmdAktualizujSygnature_TrybTestowy";
            this.cmdAktualizujSygnature_TrybTestowy.ExecuteCode += new System.EventHandler(this.cmdAktualizujSygnature_TrybTestowy_ExecuteCode);
            // 
            // cmdZmodyfikujOdiorcow
            // 
            this.cmdZmodyfikujOdiorcow.Name = "cmdZmodyfikujOdiorcow";
            this.cmdZmodyfikujOdiorcow.ExecuteCode += new System.EventHandler(this.cmdZmodyfikujOdiorcow_ExecuteCode);
            // 
            // cmdAktualizujSygnature
            // 
            this.cmdAktualizujSygnature.Name = "cmdAktualizujSygnature";
            this.cmdAktualizujSygnature.ExecuteCode += new System.EventHandler(this.cmdAktualizujSygnature_ExecuteCode);
            // 
            // logRodzajRaportuS3
            // 
            this.logRodzajRaportuS3.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logRodzajRaportuS3.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind1.Name = "swfWyslijZestawienieGodzin";
            activitybind1.Path = "logRodzajRaportuS_HistoryDescription";
            activitybind2.Name = "swfWyslijZestawienieGodzin";
            activitybind2.Path = "logRodzajRaportuS_HistoryOutcome";
            this.logRodzajRaportuS3.Name = "logRodzajRaportuS3";
            this.logRodzajRaportuS3.OtherData = "";
            this.logRodzajRaportuS3.UserId = -1;
            this.logRodzajRaportuS3.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            this.logRodzajRaportuS3.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            // 
            // cmdPrzygotujTresc_S_D
            // 
            this.cmdPrzygotujTresc_S_D.Name = "cmdPrzygotujTresc_S_D";
            this.cmdPrzygotujTresc_S_D.ExecuteCode += new System.EventHandler(this.cmdPrzygotujTresc_S_D_ExecuteCode);
            // 
            // logRodzajRaportuS2
            // 
            this.logRodzajRaportuS2.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logRodzajRaportuS2.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind3.Name = "swfWyslijZestawienieGodzin";
            activitybind3.Path = "logRodzajRaportuS_HistoryDescription";
            activitybind4.Name = "swfWyslijZestawienieGodzin";
            activitybind4.Path = "logRodzajRaportuS_HistoryOutcome";
            this.logRodzajRaportuS2.Name = "logRodzajRaportuS2";
            this.logRodzajRaportuS2.OtherData = "";
            this.logRodzajRaportuS2.UserId = -1;
            this.logRodzajRaportuS2.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
            this.logRodzajRaportuS2.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind4)));
            // 
            // cmdPrzygotujTresc_S_T
            // 
            this.cmdPrzygotujTresc_S_T.Name = "cmdPrzygotujTresc_S_T";
            this.cmdPrzygotujTresc_S_T.ExecuteCode += new System.EventHandler(this.cmdPrzygotujTresc_S_T_ExecuteCode);
            // 
            // logRodzajRaportuS
            // 
            this.logRodzajRaportuS.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logRodzajRaportuS.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind5.Name = "swfWyslijZestawienieGodzin";
            activitybind5.Path = "logRodzajRaportuS_HistoryDescription";
            activitybind6.Name = "swfWyslijZestawienieGodzin";
            activitybind6.Path = "logRodzajRaportuS_HistoryOutcome";
            this.logRodzajRaportuS.Name = "logRodzajRaportuS";
            this.logRodzajRaportuS.OtherData = "";
            this.logRodzajRaportuS.UserId = -1;
            this.logRodzajRaportuS.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind5)));
            this.logRodzajRaportuS.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind6)));
            // 
            // cmdPrzygotujTresc_S_M
            // 
            this.cmdPrzygotujTresc_S_M.Name = "cmdPrzygotujTresc_S_M";
            this.cmdPrzygotujTresc_S_M.ExecuteCode += new System.EventHandler(this.cmdPrzygotujTresc_S_M_ExecuteCode);
            // 
            // RaportowanieWynikuDlaProjektu
            // 
            this.RaportowanieWynikuDlaProjektu.Activities.Add(this.cmdZainicjujRozliczenieProjektu);
            this.RaportowanieWynikuDlaProjektu.Activities.Add(this.OkreślRodzajRaportu_P);
            this.RaportowanieWynikuDlaProjektu.Name = "RaportowanieWynikuDlaProjektu";
            // 
            // Else_TrybTestowy
            // 
            this.Else_TrybTestowy.Activities.Add(this.cmdZmodyfikujOdiorcow);
            this.Else_TrybTestowy.Activities.Add(this.cmdAktualizujSygnature_TrybTestowy);
            this.Else_TrybTestowy.Name = "Else_TrybTestowy";
            // 
            // ifTrybProdukcyjny
            // 
            this.ifTrybProdukcyjny.Activities.Add(this.cmdAktualizujSygnature);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.isTrybProdukcyjny);
            this.ifTrybProdukcyjny.Condition = codecondition1;
            this.ifTrybProdukcyjny.Name = "ifTrybProdukcyjny";
            // 
            // ifRaport_S_Dzienny
            // 
            this.ifRaport_S_Dzienny.Activities.Add(this.cmdPrzygotujTresc_S_D);
            this.ifRaport_S_Dzienny.Activities.Add(this.logRodzajRaportuS3);
            codecondition2.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.isRaport_S_Dzienny);
            this.ifRaport_S_Dzienny.Condition = codecondition2;
            this.ifRaport_S_Dzienny.Name = "ifRaport_S_Dzienny";
            // 
            // ifRaport_S_Tygodniowy
            // 
            this.ifRaport_S_Tygodniowy.Activities.Add(this.cmdPrzygotujTresc_S_T);
            this.ifRaport_S_Tygodniowy.Activities.Add(this.logRodzajRaportuS2);
            codecondition3.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.isRaport_S_Tygodniowy);
            this.ifRaport_S_Tygodniowy.Condition = codecondition3;
            this.ifRaport_S_Tygodniowy.Name = "ifRaport_S_Tygodniowy";
            // 
            // ifRaport_S_Miesieczny
            // 
            this.ifRaport_S_Miesieczny.Activities.Add(this.cmdPrzygotujTresc_S_M);
            this.ifRaport_S_Miesieczny.Activities.Add(this.logRodzajRaportuS);
            codecondition4.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.isRaport_S_Miesieczny);
            this.ifRaport_S_Miesieczny.Condition = codecondition4;
            this.ifRaport_S_Miesieczny.Name = "ifRaport_S_Miesieczny";
            // 
            // whileProjekt
            // 
            this.whileProjekt.Activities.Add(this.RaportowanieWynikuDlaProjektu);
            this.whileProjekt.Condition = null;
            this.whileProjekt.Name = "whileProjekt";
            // 
            // logWiadomoscWyslana
            // 
            this.logWiadomoscWyslana.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logWiadomoscWyslana.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            this.logWiadomoscWyslana.HistoryDescription = "Wiadomość wysłana";
            activitybind7.Name = "swfWyslijZestawienieGodzin";
            activitybind7.Path = "logWiadomoscWyslana_HistoryOutcome";
            this.logWiadomoscWyslana.Name = "logWiadomoscWyslana";
            this.logWiadomoscWyslana.OtherData = "";
            this.logWiadomoscWyslana.UserId = -1;
            this.logWiadomoscWyslana.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind7)));
            // 
            // sendRaportDoKlienta
            // 
            activitybind8.Name = "swfWyslijZestawienieGodzin";
            activitybind8.Path = "msgBCC";
            activitybind9.Name = "swfWyslijZestawienieGodzin";
            activitybind9.Path = "msgBody";
            this.sendRaportDoKlienta.CC = null;
            correlationtoken1.Name = "workflowToken";
            correlationtoken1.OwnerActivityName = "swfWyslijZestawienieGodzin";
            this.sendRaportDoKlienta.CorrelationToken = correlationtoken1;
            activitybind10.Name = "swfWyslijZestawienieGodzin";
            activitybind10.Path = "msgFrom";
            this.sendRaportDoKlienta.Headers = null;
            this.sendRaportDoKlienta.IncludeStatus = false;
            this.sendRaportDoKlienta.Name = "sendRaportDoKlienta";
            activitybind11.Name = "swfWyslijZestawienieGodzin";
            activitybind11.Path = "msgSubject";
            activitybind12.Name = "swfWyslijZestawienieGodzin";
            activitybind12.Path = "msgTo";
            this.sendRaportDoKlienta.SetBinding(Microsoft.SharePoint.WorkflowActions.SendEmail.BodyProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind9)));
            this.sendRaportDoKlienta.SetBinding(Microsoft.SharePoint.WorkflowActions.SendEmail.ToProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind12)));
            this.sendRaportDoKlienta.SetBinding(Microsoft.SharePoint.WorkflowActions.SendEmail.SubjectProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind11)));
            this.sendRaportDoKlienta.SetBinding(Microsoft.SharePoint.WorkflowActions.SendEmail.FromProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind10)));
            this.sendRaportDoKlienta.SetBinding(Microsoft.SharePoint.WorkflowActions.SendEmail.BCCProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind8)));
            // 
            // OkreśćTrybPracy
            // 
            this.OkreśćTrybPracy.Activities.Add(this.ifTrybProdukcyjny);
            this.OkreśćTrybPracy.Activities.Add(this.Else_TrybTestowy);
            this.OkreśćTrybPracy.Name = "OkreśćTrybPracy";
            // 
            // cmdPrzygotujWiadomosc
            // 
            this.cmdPrzygotujWiadomosc.Name = "cmdPrzygotujWiadomosc";
            this.cmdPrzygotujWiadomosc.ExecuteCode += new System.EventHandler(this.cmdPrzygotujWiadomosc_ExecuteCode);
            // 
            // OkreślRodzajRaportu_S
            // 
            this.OkreślRodzajRaportu_S.Activities.Add(this.ifRaport_S_Miesieczny);
            this.OkreślRodzajRaportu_S.Activities.Add(this.ifRaport_S_Tygodniowy);
            this.OkreślRodzajRaportu_S.Activities.Add(this.ifRaport_S_Dzienny);
            this.OkreślRodzajRaportu_S.Name = "OkreślRodzajRaportu_S";
            // 
            // sekObsługaProjektu
            // 
            this.sekObsługaProjektu.Activities.Add(this.whileProjekt);
            this.sekObsługaProjektu.Name = "sekObsługaProjektu";
            // 
            // cmdListaProjektowByKlient
            // 
            this.cmdListaProjektowByKlient.Name = "cmdListaProjektowByKlient";
            // 
            // ifTrescDoWyslania
            // 
            this.ifTrescDoWyslania.Activities.Add(this.cmdPrzygotujWiadomosc);
            this.ifTrescDoWyslania.Activities.Add(this.OkreśćTrybPracy);
            this.ifTrescDoWyslania.Activities.Add(this.sendRaportDoKlienta);
            this.ifTrescDoWyslania.Activities.Add(this.logWiadomoscWyslana);
            codecondition5.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.isTrescDoWyslania);
            this.ifTrescDoWyslania.Condition = codecondition5;
            this.ifTrescDoWyslania.Description = "isTrescDoWyslania";
            this.ifTrescDoWyslania.Name = "ifTrescDoWyslania";
            // 
            // sequenceActivity1
            // 
            this.sequenceActivity1.Name = "sequenceActivity1";
            // 
            // seqRozliczenieGodzinSerwisowych
            // 
            this.seqRozliczenieGodzinSerwisowych.Activities.Add(this.OkreślRodzajRaportu_S);
            this.seqRozliczenieGodzinSerwisowych.Name = "seqRozliczenieGodzinSerwisowych";
            // 
            // seqRozliczenieGodzinProjektowych
            // 
            this.seqRozliczenieGodzinProjektowych.Activities.Add(this.cmdListaProjektowByKlient);
            this.seqRozliczenieGodzinProjektowych.Activities.Add(this.sekObsługaProjektu);
            this.seqRozliczenieGodzinProjektowych.Enabled = false;
            this.seqRozliczenieGodzinProjektowych.Name = "seqRozliczenieGodzinProjektowych";
            // 
            // logErrorMessage
            // 
            this.logErrorMessage.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logErrorMessage.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind13.Name = "swfWyslijZestawienieGodzin";
            activitybind13.Path = "logErrorMessage_HistoryDescription";
            this.logErrorMessage.HistoryOutcome = "";
            this.logErrorMessage.Name = "logErrorMessage";
            this.logErrorMessage.OtherData = "";
            this.logErrorMessage.UserId = -1;
            this.logErrorMessage.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind13)));
            // 
            // cmdErrorHandler
            // 
            this.cmdErrorHandler.Name = "cmdErrorHandler";
            this.cmdErrorHandler.ExecuteCode += new System.EventHandler(this.cmdErrorHandler_ExecuteCode);
            // 
            // JeżeliJestTreśćDoWysłania
            // 
            this.JeżeliJestTreśćDoWysłania.Activities.Add(this.ifTrescDoWyslania);
            this.JeżeliJestTreśćDoWysłania.Name = "JeżeliJestTreśćDoWysłania";
            // 
            // PrzygotwanieTreściDoWysyłki
            // 
            this.PrzygotwanieTreściDoWysyłki.Activities.Add(this.seqRozliczenieGodzinProjektowych);
            this.PrzygotwanieTreściDoWysyłki.Activities.Add(this.seqRozliczenieGodzinSerwisowych);
            this.PrzygotwanieTreściDoWysyłki.Activities.Add(this.sequenceActivity1);
            this.PrzygotwanieTreściDoWysyłki.Name = "PrzygotwanieTreściDoWysyłki";
            // 
            // logKlient
            // 
            this.logKlient.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
            this.logKlient.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
            activitybind14.Name = "swfWyslijZestawienieGodzin";
            activitybind14.Path = "logKlient_HistoryDescription";
            activitybind15.Name = "swfWyslijZestawienieGodzin";
            activitybind15.Path = "logKlient_HistoryOutcome";
            this.logKlient.Name = "logKlient";
            this.logKlient.OtherData = "";
            this.logKlient.UserId = -1;
            this.logKlient.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind14)));
            this.logKlient.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind15)));
            // 
            // cmdZainicjujRozliczenieKlienta
            // 
            this.cmdZainicjujRozliczenieKlienta.Name = "cmdZainicjujRozliczenieKlienta";
            this.cmdZainicjujRozliczenieKlienta.ExecuteCode += new System.EventHandler(this.cmdZainicjujRozliczenieKlienta_ExecuteCode);
            // 
            // faultHandlerActivity1
            // 
            this.faultHandlerActivity1.Activities.Add(this.cmdErrorHandler);
            this.faultHandlerActivity1.Activities.Add(this.logErrorMessage);
            this.faultHandlerActivity1.FaultType = typeof(System.Exception);
            this.faultHandlerActivity1.Name = "faultHandlerActivity1";
            // 
            // RaportowanieWynikówDlaKlienta
            // 
            this.RaportowanieWynikówDlaKlienta.Activities.Add(this.cmdZainicjujRozliczenieKlienta);
            this.RaportowanieWynikówDlaKlienta.Activities.Add(this.logKlient);
            this.RaportowanieWynikówDlaKlienta.Activities.Add(this.PrzygotwanieTreściDoWysyłki);
            this.RaportowanieWynikówDlaKlienta.Activities.Add(this.JeżeliJestTreśćDoWysłania);
            this.RaportowanieWynikówDlaKlienta.Name = "RaportowanieWynikówDlaKlienta";
            // 
            // faultHandlersActivity1
            // 
            this.faultHandlersActivity1.Activities.Add(this.faultHandlerActivity1);
            this.faultHandlersActivity1.Name = "faultHandlersActivity1";
            // 
            // sendReport
            // 
            this.sendReport.BCC = null;
            activitybind16.Name = "swfWyslijZestawienieGodzin";
            activitybind16.Path = "msgBody";
            this.sendReport.CC = null;
            this.sendReport.CorrelationToken = correlationtoken1;
            activitybind17.Name = "swfWyslijZestawienieGodzin";
            activitybind17.Path = "msgFrom";
            this.sendReport.Headers = null;
            this.sendReport.IncludeStatus = false;
            this.sendReport.Name = "sendReport";
            activitybind18.Name = "swfWyslijZestawienieGodzin";
            activitybind18.Path = "msgSubject";
            activitybind19.Name = "swfWyslijZestawienieGodzin";
            activitybind19.Path = "msgTo";
            this.sendReport.MethodInvoking += new System.EventHandler(this.sendReport_MethodInvoking);
            this.sendReport.SetBinding(Microsoft.SharePoint.WorkflowActions.SendEmail.ToProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind19)));
            this.sendReport.SetBinding(Microsoft.SharePoint.WorkflowActions.SendEmail.SubjectProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind18)));
            this.sendReport.SetBinding(Microsoft.SharePoint.WorkflowActions.SendEmail.FromProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind17)));
            this.sendReport.SetBinding(Microsoft.SharePoint.WorkflowActions.SendEmail.BodyProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind16)));
            // 
            // whileKlient
            // 
            this.whileKlient.Activities.Add(this.RaportowanieWynikówDlaKlienta);
            codecondition6.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.whileKlientExist);
            this.whileKlient.Condition = codecondition6;
            this.whileKlient.Name = "whileKlient";
            // 
            // getListaKlientow
            // 
            this.getListaKlientow.Name = "getListaKlientow";
            this.getListaKlientow.ExecuteCode += new System.EventHandler(this.getListaKlientow_ExecuteCode);
            // 
            // cmdZainicjujRaportDlaAdmina
            // 
            this.cmdZainicjujRaportDlaAdmina.Name = "cmdZainicjujRaportDlaAdmina";
            this.cmdZainicjujRaportDlaAdmina.ExecuteCode += new System.EventHandler(this.cmdZainicjujRaportDlaAdmina_ExecuteCode);
            activitybind21.Name = "swfWyslijZestawienieGodzin";
            activitybind21.Path = "workflowId";
            // 
            // onWorkflowActivated1
            // 
            this.onWorkflowActivated1.CorrelationToken = correlationtoken1;
            this.onWorkflowActivated1.EventName = "OnWorkflowActivated";
            this.onWorkflowActivated1.Name = "onWorkflowActivated1";
            activitybind20.Name = "swfWyslijZestawienieGodzin";
            activitybind20.Path = "workflowProperties";
            this.onWorkflowActivated1.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs>(this.onWorkflowActivated1_Invoked);
            this.onWorkflowActivated1.SetBinding(Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated.WorkflowIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind21)));
            this.onWorkflowActivated1.SetBinding(Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated.WorkflowPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind20)));
            // 
            // swfWyslijZestawienieGodzin
            // 
            this.Activities.Add(this.onWorkflowActivated1);
            this.Activities.Add(this.cmdZainicjujRaportDlaAdmina);
            this.Activities.Add(this.getListaKlientow);
            this.Activities.Add(this.whileKlient);
            this.Activities.Add(this.sendReport);
            this.Activities.Add(this.faultHandlersActivity1);
            this.Name = "swfWyslijZestawienieGodzin";
            this.CanModifyActivities = false;

        }

        #endregion

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logRodzajRaportuS3;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logRodzajRaportuS2;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logRodzajRaportuS;

        private CodeActivity cmdPrzygotujTresc_P_D;

        private CodeActivity cmdUstawTytulSekcji_P_D;

        private CodeActivity cmdPrzygotujTresc_P_T;

        private CodeActivity cmdUstawTytulSekcji_P_T;

        private CodeActivity cmdPrzygotujTresc_P_M;

        private CodeActivity cmdUstawTytulSekcji_P_M;

        private IfElseBranchActivity ifRaport_P_Dzienny;

        private IfElseBranchActivity ifRaport_P_Tygodniowy;

        private IfElseBranchActivity ifRaport_P_Miesieczny;

        private IfElseActivity OkreślRodzajRaportu_P_regular;

        private CodeActivity cmdPrzygotujTresc_P_O;

        private CodeActivity cmdUstawTytulSekcji_P_O;

        private IfElseBranchActivity Else;

        private IfElseBranchActivity ifRaport_P_Zamykajacy;

        private IfElseActivity OkreślRodzajRaportu_P;

        private CodeActivity cmdZainicjujRozliczenieProjektu;

        private CodeActivity cmdAktualizujSygnature_TrybTestowy;

        private CodeActivity cmdZmodyfikujOdiorcow;

        private CodeActivity cmdAktualizujSygnature;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logWiadomoscWyslana;

        private CodeActivity cmdPrzygotujTresc_S_D;

        private CodeActivity cmdPrzygotujTresc_S_T;

        private CodeActivity cmdPrzygotujTresc_S_M;

        private SequenceActivity RaportowanieWynikuDlaProjektu;

        private IfElseBranchActivity Else_TrybTestowy;

        private IfElseBranchActivity ifTrybProdukcyjny;

        private IfElseBranchActivity ifRaport_S_Dzienny;

        private IfElseBranchActivity ifRaport_S_Tygodniowy;

        private IfElseBranchActivity ifRaport_S_Miesieczny;

        private WhileActivity whileProjekt;

        private Microsoft.SharePoint.WorkflowActions.SendEmail sendRaportDoKlienta;

        private IfElseActivity OkreśćTrybPracy;

        private CodeActivity cmdPrzygotujWiadomosc;

        private IfElseActivity OkreślRodzajRaportu_S;

        private SequenceActivity sekObsługaProjektu;

        private CodeActivity cmdListaProjektowByKlient;

        private IfElseBranchActivity ifTrescDoWyslania;

        private SequenceActivity sequenceActivity1;

        private SequenceActivity seqRozliczenieGodzinSerwisowych;

        private SequenceActivity seqRozliczenieGodzinProjektowych;

        private IfElseActivity JeżeliJestTreśćDoWysłania;

        private ParallelActivity PrzygotwanieTreściDoWysyłki;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logKlient;

        private CodeActivity cmdZainicjujRozliczenieKlienta;

        private SequenceActivity RaportowanieWynikówDlaKlienta;

        private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logErrorMessage;

        private CodeActivity cmdErrorHandler;

        private FaultHandlerActivity faultHandlerActivity1;

        private FaultHandlersActivity faultHandlersActivity1;

        private WhileActivity whileKlient;

        private CodeActivity getListaKlientow;

        private CodeActivity cmdZainicjujRaportDlaAdmina;

        private Microsoft.SharePoint.WorkflowActions.SendEmail sendReport;

        private Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated onWorkflowActivated1;





























































    }
}
