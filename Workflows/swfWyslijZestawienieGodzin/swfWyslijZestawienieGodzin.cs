using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;
using Microsoft.SharePoint.WorkflowActions;
using System.Diagnostics;
using System.Text;

namespace Workflows.swfWyslijZestawienieGodzin
{
    public sealed partial class swfWyslijZestawienieGodzin : SequentialWorkflowActivity
    {
        public swfWyslijZestawienieGodzin()
        {
            InitializeComponent();
        }

        public Guid workflowId = default(System.Guid);
        public SPWorkflowActivationProperties workflowProperties = new SPWorkflowActivationProperties();
        public String msgTo = default(System.String);
        public String msgBody = default(System.String);

        SPListItem kItem;
        private string report_S_Type;
        private DateTime targetDate;
        private SerwisReportType sReportType;

        private StringBuilder sbSRT = null;
        private SPWeb web;

        public StringBuilder sbAdamin = new StringBuilder();
        private string _TS_WEB_ = "TS";

        private void sendReport_MethodInvoking(object sender, EventArgs e)
        {
            msgBody = string.Format("<ol>{0}</ol>", sbAdamin.ToString());
            msgSubject = string.Format("Zestawienie Godzin - podsumowanie ({0})", workflowProperties.Web.Url);
            msgTo = workflowProperties.Workflow.AuthorUser.Email;
        }

        public String msgSubject = default(System.String);
        public String msgFrom = "STAFix24 Robot<biuro@rawcom24.pl>";

        private void onWorkflowActivated1_Invoked(object sender, ExternalDataEventArgs e)
        {
            msgFrom = workflowProperties.Site.WebApplication.OutboundMailSenderAddress;
            //targetDate = DateTime.Today.AddDays(-1);
            //targetDate = new DateTime(2015, 11, 20); //miesięczny
            //targetDate = new DateTime(2015, 11, 18);
            targetDate = new DateTime(2015, 11, 30);


            web = workflowProperties.Site.OpenWeb(_TS_WEB_);
        }

        private void cmdErrorHandler_ExecuteCode(object sender, EventArgs e)
        {
            FaultHandlerActivity fa = ((Activity)sender).Parent as FaultHandlerActivity;
            if (fa != null)
            {
                Debug.WriteLine(fa.Fault.Source);
                Debug.WriteLine(fa.Fault.Message);
                Debug.WriteLine(fa.Fault.StackTrace);

                logErrorMessage_HistoryDescription = string.Format("{0}::{1}",
                    fa.Fault.Message,
                    fa.Fault.StackTrace);


                Tools.ElasticEmail.ReportErrorFromWorkflow(workflowProperties, fa.Fault.Message, fa.Fault.StackTrace);

            }
        }

        public String logErrorMessage_HistoryDescription = default(System.String);

        private void cmdZainicjujRaportDlaAdmina_ExecuteCode(object sender, EventArgs e)
        {
            sbAdamin.AppendFormat("Zestawienie wykonanych operacji");
        }

        public String logKlient_HistoryDescription = default(System.String);
        public String logRodzajRaportuS_HistoryDescription = default(System.String);
        public String logRodzajRaportuS_HistoryOutcome = default(System.String);
        public String logKlient_HistoryOutcome = default(System.String);
        private StringBuilder sbPRT;

        private Array klienci;
        private IEnumerator kEnum;
        private string _TR_TEMPLATE_ = @"<tr valign=""top""><td style=""border: 1px solid #808080; white-space: nowrap"">[[colData]]</td> <td style=""border: 1px solid #808080"">[[Title]]</td> <td style=""border: 1px solid #808080; text-align: right; white-space: nowrap"">[[colCzasMin]]</td> </tr>";
        private string _TABLE_TEMPLATE_ = @"<table cellpadding=""2"" cellspacing=""0"" style=""border: 2px solid #808080; width: 100%""><thead><tr><th style=""border-left: 1px solid #808080; border-top: 1px solid #808080; border-bottom: 1px solid #808080; text-align: center; width: 5%; background-color: #CCCCCC; border-right-color: #808080;""><strong>Data rejestracji</strong></th><th style=""border-top: 1px solid #808080; border-bottom: 1px solid #808080; text-align: center; background-color: #CCCCCC; border-left-color: #808080; border-right-color: #808080;""><strong>Tytuł</strong></th><th style=""border-right: 1px solid #808080; border-top: 1px solid #808080; border-bottom: 1px solid #808080; text-align: center; width: 5%; background-color: #CCCCCC; border-left-color: #808080;""><strong>Czas obsługi</strong></th></tr></thead><tbody>___TR___</tbody><tfoot><tr><td colspan=""2"" style=""border-left: 1px solid #808080; border-top: 1px solid #808080; border-bottom: 1px solid #808080; text-align: right; border-right-color: #808080; background-color: #CCCCCC;"">razem:</td><td style=""border-right: 1px solid #808080; border-top: 1px solid #808080; border-bottom: 1px solid #808080; text-align: right; border-left-color: #808080; background-color: #CCCCCC;"">[[Total_CzasMin]]</td></tr><tr><td colspan=""2"" style=""border-left: 1px solid #808080; border-top: 1px solid #808080; border-bottom: 1px solid #808080; text-align: right; border-right-color: #808080; background-color: #E5E5E5;"">razem narastająco:</td><td style=""border-right: 1px solid #808080; border-top: 1px solid #808080; border-bottom: 1px solid #808080; text-align: right; border-left-color: #808080; background-color: #E5E5E5;"">[[Total_Narastajaco]]</td></tr><tr><td colspan=""2"" style=""border-left: 1px solid #808080; border-top: 1px solid #808080; border-bottom: 1px solid #808080; text-align: right; border-right-color: #808080; background-color: #E5E5E5;"">godziny zryczałtowane:</td><td style=""border-right: 1px solid #808080; border-top: 1px solid #808080; border-bottom: 1px solid #808080; text-align: right; border-left-color: #808080; background-color: #E5E5E5;"">[[Total_Ryczalt]]</td></tr><tr><td colspan=""2"" style=""border-left: 1px solid #808080; border-top: 1px solid #808080; border-bottom: 1px solid #808080; text-align: right; border-right-color: #808080; background-color: #E5E5E5;"">do rozliczenia ponad ryczałt:</td><td style=""border-right: 1px solid #808080; border-top: 1px solid #808080; border-bottom: 1px solid #808080; text-align: right; border-left-color: #808080; background-color: #E5E5E5;"">[[Total_DoRozliczenia]]</td></tr></tfoot></table>";

        private string _MESSAGE_BODY_TEMPLATE_ = @"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.01 Transitional//EN"" 
""http://www.w3.org/TR/html4/loose.dtd""><html><head><meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" /><title>Informacja wysłana automatycznie</title><style type=""text/css"">
  body, .mainTable { height:100% !important; width:100% !important; margin:0; padding:0; }
  img, a img { border:0; outline:none; text-decoration:none; }
  table, td { border-collapse:collapse; }
  p {margin:0; padding:0; margin-bottom:0;}
  img{-ms-interpolation-mode: bicubic;}
  body, table, td, p, a, li, blockquote{-ms-text-size-adjust:100%; -webkit-text-size-adjust:100%;}
</style></head><body scroll=""auto"" style=""padding:0; margin:0; FONT-SIZE: 12px; FONT-FAMILY: Arial, Helvetica, sans-serif; cursor:auto; background:#F3F3F3""><TABLE class=mainTable cellSpacing=0 cellPadding=0 width=""100%"" bgColor=#f3f3f3><TR><TD style=""FONT-SIZE: 0px; HEIGHT: 20px; LINE-HEIGHT: 0"">&nbsp;</TD></TR><TR><TD vAlign=top><TABLE style=""WIDTH: 600px; MARGIN: 0px auto"" cellSpacing=0 cellPadding=0 width=600 align=center border=0><TR><TD style=""BORDER-TOP: #dbdbdb 1px solid; BORDER-RIGHT: #dbdbdb 1px solid; BORDER-BOTTOM: medium none; PADDING-BOTTOM: 0px; PADDING-TOP: 0px; PADDING-LEFT: 0px; BORDER-LEFT: #dbdbdb 1px solid; PADDING-RIGHT: 0px; BACKGROUND-COLOR: #feffff""><TABLE style=""WIDTH: 100%"" cellSpacing=0 cellPadding=0 align=left><TR style=""HEIGHT: 10px""><TD style=""BORDER-TOP: medium none; BORDER-RIGHT: medium none; WIDTH: 1%; VERTICAL-ALIGN: top; BORDER-BOTTOM: medium none; PADDING-BOTTOM: 5px; TEXT-ALIGN: center; PADDING-TOP: 5px; PADDING-LEFT: 15px; BORDER-LEFT: medium none; PADDING-RIGHT: 15px; BACKGROUND-COLOR: #feffff""><TABLE cellSpacing=0 cellPadding=0 align=center border=0><TR><TD style=""PADDING-BOTTOM: 2px; PADDING-TOP: 2px; PADDING-LEFT: 2px; PADDING-RIGHT: 2px"" align=center><TABLE cellSpacing=0 cellPadding=0 border=0><TR><TD style=""BORDER-TOP: medium none; BORDER-RIGHT: medium none; BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; BACKGROUND-COLOR: transparent""><A href=""http://www.stafix24.pl/home/contact""><IMG style=""BORDER-TOP: medium none; BORDER-RIGHT: medium none; BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; DISPLAY: block; BACKGROUND-COLOR: transparent"" border=0 alt=RAWcom src=""https://mailchef.s3.amazonaws.com/uploads/785322/image/6461CB42-89B1-DDBA-6928-52502B66A62D_Image_1.png"" width=108 height=66 hspace=""0"" vspace=""0""></A></TD></TR></TABLE></TD></TR></TABLE></TD><TD style=""BORDER-TOP: medium none; BORDER-RIGHT: medium none; WIDTH: 99%; VERTICAL-ALIGN: middle; BORDER-BOTTOM: medium none; PADDING-BOTTOM: 5px; TEXT-ALIGN: center; PADDING-TOP: 5px; PADDING-LEFT: 15px; BORDER-LEFT: medium none; PADDING-RIGHT: 15px; BACKGROUND-COLOR: #feffff""><P style=""MARGIN-BOTTOM: 1em; FONT-SIZE: 18px; FONT-FAMILY: Arial, Helvetica, sans-serif; COLOR: #7c7c7c; MARGIN-TOP: 0px; LINE-HEIGHT: 1.3; BACKGROUND-COLOR: transparent"" align=left><STRONG><BR>[[MessageHeader]]</STRONG></P></TD></TR></TABLE></TD></TR><TR><TD style=""BORDER-TOP: medium none; BORDER-RIGHT: #dbdbdb 1px solid; BORDER-BOTTOM: #dbdbdb 1px solid; PADDING-BOTTOM: 0px; PADDING-TOP: 0px; PADDING-LEFT: 0px; BORDER-LEFT: #dbdbdb 1px solid; PADDING-RIGHT: 0px; BACKGROUND-COLOR: #feffff""><TABLE style=""WIDTH: 100%"" cellSpacing=0 cellPadding=0 align=left><TR style=""HEIGHT: 20px""><TD style=""BORDER-TOP: medium none; BORDER-RIGHT: medium none; WIDTH: 100%; VERTICAL-ALIGN: top; BORDER-BOTTOM: medium none; PADDING-BOTTOM: 35px; TEXT-ALIGN: center; PADDING-TOP: 15px; PADDING-LEFT: 15px; BORDER-LEFT: medium none; PADDING-RIGHT: 15px; BACKGROUND-COLOR: #feffff""><P style=""MARGIN-BOTTOM: 1em; FONT-SIZE: 12px; FONT-FAMILY: Arial, Helvetica, sans-serif; COLOR: #a7a7a7; MARGIN-TOP: 0px; LINE-HEIGHT: 1.3; BACKGROUND-COLOR: transparent"" align=left>[[MessageBody]]</P></TD></TR></TABLE></TD></TR><TR><TD style=""BORDER-TOP: medium none; BORDER-RIGHT: medium none; BORDER-BOTTOM: medium none; PADDING-BOTTOM: 1px; PADDING-TOP: 1px; PADDING-LEFT: 0px; BORDER-LEFT: medium none; PADDING-RIGHT: 0px; BACKGROUND-COLOR: transparent""><TABLE style=""WIDTH: 100%"" cellSpacing=0 cellPadding=0 align=left><TR style=""HEIGHT: 10px""><TD style=""BORDER-TOP: medium none; BORDER-RIGHT: medium none; WIDTH: 100%; VERTICAL-ALIGN: top; BORDER-BOTTOM: medium none; PADDING-BOTTOM: 1px; TEXT-ALIGN: center; PADDING-TOP: 1px; PADDING-LEFT: 15px; BORDER-LEFT: medium none; PADDING-RIGHT: 15px; BACKGROUND-COLOR: transparent""><P style=""MARGIN-BOTTOM: 1em; FONT-SIZE: 10px; FONT-FAMILY: Arial, Helvetica, sans-serif; COLOR: #7c7c7c; MARGIN-TOP: 0px; LINE-HEIGHT: normal; BACKGROUND-COLOR: transparent"" align=left>RAWcom Sp. z o.o., Żelazna 67 lok.13, 00-871 Warszawa, NIP 5272714236, KRS 0000509990</P><P style=""MARGIN-BOTTOM: 1em; FONT-SIZE: 10px; FONT-FAMILY: Arial, Helvetica, sans-serif; COLOR: #7c7c7c; MARGIN-TOP: 0px; LINE-HEIGHT: normal; BACKGROUND-COLOR: transparent"" align=left><BR><FONT style=""COLOR: #a7a7a7"">[[MessageID]]</FONT><BR></P></TD></TR></TABLE></TD></TR></TABLE></TD></TR><TR><TD style=""FONT-SIZE: 0px; HEIGHT: 8px; LINE-HEIGHT: 0"">&nbsp;</TD></TR></TABLE></body></html>";

        private string _DEFAULT_RECEIPIENT_EMAIL_ = "jacek.rawiak@hotmail.com";
        private string messageInfo;
        private bool isProductionMode = false;

        private void getListaKlientow_ExecuteCode(object sender, EventArgs e)
        {
            klienci = DAL.tabKlienci.Get_AktywniKlienci(web);

            //określ tryb pracy raportu
            if (klienci.Length > 0) isProductionMode = DAL.admSetup.Get_ValueByKey(workflowProperties.Site.RootWeb, "PRODUCTION_MODE").Equals("Enabled");

            kEnum = klienci.GetEnumerator();
        }

        private void cmdZainicjujRozliczenieKlienta_ExecuteCode(object sender, EventArgs e)
        {
            kItem = kEnum.Current as SPListItem;

            string nazwaKlienta = Tools.Lists.Get_Text(kItem, "colNazwaKlienta");
            sbAdamin.AppendFormat("Rozliczenie godzin dla: {0}", nazwaKlienta);

            logKlient_HistoryDescription = nazwaKlienta;
            logKlient_HistoryOutcome = string.Format("klientId={0}", kItem.ID.ToString());
        }

        private void whileKlientExist(object sender, ConditionalEventArgs e)
        {
            if (kEnum.MoveNext() && kEnum != null) e.Result = true;
            else e.Result = false;
        }

        private void isRaport_S_Miesieczny(object sender, ConditionalEventArgs e)
        {
            if (isMiesieczny(targetDate))
            {
                sReportType = SerwisReportType.S_Miesieczny;
                e.Result = true;
            }
        }

        private void isRaport_S_Tygodniowy(object sender, ConditionalEventArgs e)
        {
            if (isTygodniowy(targetDate))
            {
                sReportType = SerwisReportType.S_Tygodniowy;
                e.Result = true;
            }
        }

        private void isRaport_S_Dzienny(object sender, ConditionalEventArgs e)
        {
            sReportType = SerwisReportType.S_Dzienny;
            e.Result = true;
        }

        private void cmdPrzygotujTresc_S_M_ExecuteCode(object sender, EventArgs e)
        {
            logRodzajRaportuS_HistoryDescription = "Raport_S miesięczny";

            Array results = DAL.tabKartyPracy.Get_ZestawienieMiesieczne_Serwis(web, kItem.ID, targetDate);
            if (results != null & results.Length > 0)
            {
                sbSRT = Create_ZestawienieAktywnosci(results, false);
                logRodzajRaportuS_HistoryOutcome = string.Format("Liczba rekordów: {0}", results.Length.ToString());
            }
            else
            {
                logRodzajRaportuS_HistoryOutcome = "brak danych";
            }
        }

        private void cmdPrzygotujTresc_S_T_ExecuteCode(object sender, EventArgs e)
        {
            logRodzajRaportuS_HistoryDescription = "Raport_S tygodniowy";

            Array results = DAL.tabKartyPracy.Get_ZestawienieTygodniowe_Serwis(web, kItem.ID, targetDate);
            if (results != null & results.Length > 0)
            {
                sbSRT = Create_ZestawienieAktywnosci(results, true);
                logRodzajRaportuS_HistoryOutcome = string.Format("Liczba rekordów: {0}", results.Length.ToString());
            }
            else
            {
                logRodzajRaportuS_HistoryOutcome = "brak danych";
            }
        }

        private void cmdPrzygotujTresc_S_D_ExecuteCode(object sender, EventArgs e)
        {
            logRodzajRaportuS_HistoryDescription = "Raport_S dzienny";

            Array results = DAL.tabKartyPracy.Get_ZestawienieDzienne_Serwis(web, kItem.ID, targetDate);
            if (results != null & results.Length > 0)
            {
                sbSRT = Create_ZestawienieAktywnosci(results, true);
                logRodzajRaportuS_HistoryOutcome = string.Format("Liczba rekordów: {0}", results.Length.ToString());
            }
            else
            {
                logRodzajRaportuS_HistoryOutcome = "brak danych";
            }
        }


        #region Helpers

        private bool isMiesieczny(DateTime targetDate)
        {
            if (targetDate.AddDays(1).Day.Equals(1)) return true;
            else return false;
        }

        private bool isTygodniowy(DateTime targetDate)
        {
            if (targetDate.DayOfWeek.Equals(DayOfWeek.Sunday)) return true;
            else return false;
        }

        private StringBuilder Create_ZestawienieAktywnosci(Array results, bool showDetails)
        {
            StringBuilder sbTableRows = new StringBuilder();
            TimeSpan tm = new TimeSpan();
            StringBuilder sb;

            foreach (SPListItem item in results) //Karty pracy
            {
                sb = new StringBuilder(_TR_TEMPLATE_);
                sb = ReplacePlaceholder(sb, item, "colData", Tools.Lists.Format_Date(Tools.Lists.Get_Date(item, "colData")));

                string opis = Tools.Lists.Get_Text(item, "colOpis");
                if (showDetails && !string.IsNullOrEmpty(opis)) sb = ReplacePlaceholder(sb, item, "Title", item.Title + "<hr>" + opis);
                else sb = ReplacePlaceholder(sb, item, "Title", item.Title);

                int m = (int)Tools.Lists.Get_Value(item, "colCzasMin");
                tm = tm.Add(new TimeSpan(0, m, 0));
                sb = ReplacePlaceholder(sb, item, "colCzasMin", Tools.Lists.Format_TimeSpan(new TimeSpan(0, m, 0)));

                sb = ReplacePlaceholder(sb, item, "Status", Tools.Lists.Get_Text(item, "Status"));

                sbTableRows.Append(sb.ToString());
            }

            sb = new StringBuilder(_TABLE_TEMPLATE_);
            sb = ReplacePlaceholder(sb, "Total_CzasMin", Tools.Lists.Format_TimeSpan(tm));
            sb.Replace("___TR___", sbTableRows.ToString());

            // uzupełnij informacje o wykorzystaniu godzin

            TimeSpan gTotal = DAL.tabKartyPracy.Get_TotalGodzinSerwisowychNarastajaco(web, kItem.ID, targetDate);
            sb = ReplacePlaceholder(sb, "Total_Narastajaco", Tools.Lists.Format_TimeSpan(gTotal));

            TimeSpan gRyczalt = new TimeSpan(0, (int)(Tools.Lists.Get_Value(kItem, "colLiczbaGodzinWAbonamencie") * 60), 0);
            sb = ReplacePlaceholder(sb, "Total_Ryczalt", Tools.Lists.Format_TimeSpan(gRyczalt));

            TimeSpan g = gTotal.Subtract(gRyczalt);
            if (g.TotalMinutes < 0) g = new TimeSpan();
            sb = ReplacePlaceholder(sb, "Total_DoRozliczenia", Tools.Lists.Format_TimeSpan(g));

            return sb;
        }

        private StringBuilder ReplacePlaceholder(StringBuilder sb, string col, string txt)
        {
            return sb.Replace(string.Format("[[{0}]]", col), txt);
        }

        private StringBuilder ReplacePlaceholder(StringBuilder sb, SPListItem item, string col, string txt)
        {
            if (item[col] != null)
            {
                return sb.Replace(string.Format("[[{0}]]", col), txt);
            }
            else
            {
                return sb.Replace(string.Format("[[{0}]]", col), string.Empty);
            }
        }
        private void AppendHeader(StringBuilder sb, SerwisReportType rt)
        {
            if (sb != null)
            {
                switch (rt)
                {
                    case SerwisReportType.S_Miesieczny:
                        sb = new StringBuilder(string.Format("<h3>{0}</h3>{1}", "Raport miesięczny", sb.ToString()));
                        break;
                    case SerwisReportType.S_Tygodniowy:
                        sb = new StringBuilder(string.Format("<h3>{0}</h3>{1}", "Raport tygodniowy", sb.ToString()));
                        break;
                    case SerwisReportType.S_Dzienny:
                        sb = new StringBuilder(string.Format("<h3>{0}</h3>{1}", "Raport dzienny", sb.ToString()));
                        break;
                }
            }
        }


        #endregion

        private void isTrescDoWyslania(object sender, ConditionalEventArgs e)
        {
            if (sbSRT != null | sbPRT != null) e.Result = true;
        }


        private void cmdPrzygotujTrescWiadomości_ExecuteCode(object sender, EventArgs e)
        {

        }

        private void cmdPrzygotujWiadomosc_ExecuteCode(object sender, EventArgs e)
        {
            string targetEmail = Tools.Lists.Get_Email(kItem, "colEmail");
            if (!string.IsNullOrEmpty(targetEmail))
            {
                StringBuilder sb = new StringBuilder(_MESSAGE_BODY_TEMPLATE_);

                if (sbSRT != null)
                {
                    sb = ReplacePlaceholder(sb, "MessageBody", sbSRT.ToString());
                }


                switch (sReportType)
                {
                    case SerwisReportType.S_Miesieczny:
                        msgSubject = "::Informacja o wykorzystanych godzinach wsparcia technicznego";
                        sb = ReplacePlaceholder(sb, "MessageHeader", Get_MessageHeader());
                        break;
                    case SerwisReportType.S_Tygodniowy:
                        msgSubject = "::Tygodniowe zestawienie wykorzystanych godzin wsparcia technicznego";
                        sb = ReplacePlaceholder(sb, "MessageHeader", Get_MessageHeader());
                        break;
                    case SerwisReportType.S_Dzienny:
                        msgSubject = "::Miesięczne zestawienie wykorzystanych godzin wsparcia technicznego";
                        sb = ReplacePlaceholder(sb, "MessageHeader", Get_MessageHeader());
                        break;
                }

                msgBCC = _BCC_EMAIL_;
                msgTo = targetEmail;
                msgBody = sb.ToString();
            }
        }

        private string Get_MessageHeader()
        {
            return string.Format("Dotyczy witryny <i>{0}</i><br>w/g stanu na dzień {1}",
                Tools.Lists.Get_Text(kItem, "colNazwaKlienta"),
                Tools.Lists.Format_Date(targetDate));
        }

        private void isTrybProdukcyjny(object sender, ConditionalEventArgs e)
        {
            if (isProductionMode) e.Result = true;

        }

        private void cmdAktualizujSygnature_ExecuteCode(object sender, EventArgs e)
        {
            msgBody = ReplacePlaceholder(new StringBuilder(msgBody), "MessageID", string.Empty).ToString();

            logWiadomoscWyslana_HistoryOutcome = "Tryb produkcyjny";
        }

        private void cmdZmodyfikujOdiorcow_ExecuteCode(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(msgTo))
            {
                sb.AppendFormat("<li>do: {0}</li>", msgTo);
                msgTo = _DEFAULT_RECEIPIENT_EMAIL_;
            }
            if (!string.IsNullOrEmpty(msgBCC))
            {
                sb.AppendFormat("<li>bcc: {0}</li>", msgBCC);
                msgBCC = string.Empty;
            }

            messageInfo = string.Format("<ul>{0}</ul>", sb.ToString());
        }

        private void cmdAktualizujSygnature_TrybTestowy_ExecuteCode(object sender, EventArgs e)
        {
            msgBody = ReplacePlaceholder(new StringBuilder(msgBody), "MessageID", messageInfo).ToString();

            logWiadomoscWyslana_HistoryOutcome = "Tryb testowy";
        }

        public String logWiadomoscWyslana_HistoryOutcome = default(System.String);
        public String msgBCC = default(System.String);
        private string _BCC_EMAIL_ = "biuro@rawcom24.pl";

    }

    public enum SerwisReportType
    {
        S_Miesieczny,
        S_Tygodniowy,
        S_Dzienny
    }
}
