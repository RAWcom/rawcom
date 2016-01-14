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
using Microsoft.SharePoint.Utilities;
using System.Net.Mail;

namespace Workflows.swfWyslijZestawienieGodzin
{
    public sealed partial class swfWyslijZestawienieGodzin : SequentialWorkflowActivity
    {
        public swfWyslijZestawienieGodzin()
        {
            InitializeComponent();
        }

        // *** try produkcyjny ***
        private bool isProductionMode = true;

        public Guid workflowId = default(System.Guid);
        public SPWorkflowActivationProperties workflowProperties = new SPWorkflowActivationProperties();
        public String msgSubject = default(System.String);
        public String msgFrom = "STAFix24 Robot<noreply@stafix24.pl>";
        public System.Collections.Specialized.StringDictionary msgHeaders = new System.Collections.Specialized.StringDictionary();
        public String msgTo = default(System.String);
        public String msgBody = default(System.String);

        public String logWiadomoscWyslana_HistoryOutcome = default(System.String);
        public String msgCC = default(System.String);
        public String msgBCC = default(System.String);
        public String msgReplyTo = @"biuro@rawcom24.pl";
        private string _BCC_EMAIL_ = "biuro@rawcom24.pl";
        public String logTargetDate_HistoryOutcome = default(System.String);
        private string _DEFAULT_SENDER = @"noreply@stafix24.pl";
        private string _DEFAULT_SENDER_NAME = @"STAFix24 Robot";

        public String logErrorMessage_HistoryDescription = default(System.String);
        private string _RETURN_PATH_ = @"Biuro RAWcom <biuro@rawcom24.pl";

        private string messageInfo;


        private string _DEFAULT_RECEIPIENT_EMAIL_ = "biuro@rawcom24.pl";
        private string _DEFAULT_SERVICE_EMAIL_ = "jacek.rawiak@hotmail.com";

        SPListItem kItem;
        private string report_S_Type;
        private DateTime targetDate;
        private SerwisReportType sReportType;

        private StringBuilder sbSRT = null;
        private SPWeb targetWeb;

        public StringBuilder sbAdamin = new StringBuilder();
        private string _TS_WEB_ = "TS";

        public String logKlient_HistoryDescription = default(System.String);
        public String logRodzajRaportuS_HistoryDescription = default(System.String);
        public String logRodzajRaportuS_HistoryOutcome = default(System.String);
        public String logKlient_HistoryOutcome = default(System.String);
        private StringBuilder sbPRT;

        private Array klienci;
        private IEnumerator kEnum;
        private string _TR_TEMPLATE_ = @"<tr valign=""top""><td style=""border: 1px solid #808080; white-space: nowrap"">[[colData]]</td> <td align=""left"" style=""border: 1px solid #808080"">[[Title]]</td> <td style=""border: 1px solid #808080; text-align: right; white-space: nowrap"">[[colCzasMin]]</td> </tr>";
        private string _TABLE_TEMPLATE_ = @"<table cellpadding=""5"" cellspacing=""0"" style=""border: 2px solid #808080; width: 100%""><tr><th style=""border-left: 1px solid #808080; border-top: 1px solid #808080; border-bottom: 1px solid #808080; text-align: center; width: 5%; background-color: #CCCCCC; border-right-color: #808080;""><strong>Data rejestracji</strong></th><th align=""left"" style=""border-top: 1px solid #808080; border-bottom: 1px solid #808080; background-color: #CCCCCC; border-left-color: #808080; border-right-color: #808080;""><strong>Tytuł</strong></th><th style=""border-right: 1px solid #808080; border-top: 1px solid #808080; border-bottom: 1px solid #808080; text-align: center; width: 5%; background-color: #CCCCCC; border-left-color: #808080;""><strong>Czas obsługi</strong></th></tr>___TR___ <tr><td colspan=""2"" style=""border-left: 1px solid #808080; border-top: 1px solid #808080; border-bottom: 1px solid #808080; text-align: right; border-right-color: #808080; background-color: #CCCCCC;"">razem:</td><td style=""border-right: 1px solid #808080; border-top: 1px solid #808080; border-bottom: 1px solid #808080; text-align: right; border-left-color: #808080; background-color: #CCCCCC;"">[[Total_CzasMin]]</td></tr><tr><td colspan=""2"" style=""border-left: 1px solid #808080; border-top: 1px solid #808080; border-bottom: 1px solid #808080; text-align: right; border-right-color: #808080; background-color: #E5E5E5;"">razem narastająco 
	od początku miesiąca:</td><td style=""border-right: 1px solid #808080; border-top: 1px solid #808080; border-bottom: 1px solid #808080; text-align: right; border-left-color: #808080; background-color: #E5E5E5;"">[[Total_Narastajaco]]</td></tr><tr><td colspan=""2"" style=""border-left: 1px solid #808080; border-top: 1px solid #808080; border-bottom: 1px solid #808080; text-align: right; border-right-color: #808080; background-color: #E5E5E5;"">godziny zryczałtowane 
		w danym miesiącu:</td><td style=""border-right: 1px solid #808080; border-top: 1px solid #808080; border-bottom: 1px solid #808080; text-align: right; border-left-color: #808080; background-color: #E5E5E5;"">[[Total_Ryczalt]]</td></tr><tr><td colspan=""2"" style=""border-left: 1px solid #808080; border-top: 1px solid #808080; border-bottom: 1px solid #808080; text-align: right; border-right-color: #808080; background-color: #E5E5E5;"">dodatkowe pakiety godzin:</td><td style=""border-right: 1px solid #808080; border-top: 1px solid #808080; border-bottom: 1px solid #808080; text-align: right; border-left-color: #808080; background-color: #E5E5E5;"">[[Total_Pakiety]]</td></tr><tr><td colspan=""2"" style=""border-left: 1px solid #808080; border-top: 1px solid #808080; border-bottom: 1px solid #808080; text-align: right; border-right-color: #808080; background-color: #CCCCCC"">do rozliczenia ponad 
	dostępny limit godzin:</td><td style=""border-right: 1px solid #808080; border-top: 1px solid #808080; border-bottom: 1px solid #808080; text-align: right; border-left-color: #808080; background-color: #CCCCCC"">[[Total_DoRozliczenia]]</td></tr></table>";

        private string _MESSAGE_BODY_TEMPLATE_ = @"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.01 Transitional//EN"" 
""http://www.w3.org/TR/html4/loose.dtd""><html><head><meta content=""text/html; charset=utf-8"" http-equiv=""Content-Type"" /><title>Informacja wysłana automatycznie</title><style type=""text/css"">
body, .mainTable {
	height: 100% !important;
	width: 100% !important;
	margin: 0;
	padding: 0;
}
img, a img {
	border: 0;
	outline: none;
	text-decoration: none;
}
table, td {
	border-collapse: collapse;
}
p {
	margin: 0;
	padding: 0;
	margin-bottom: 0;
}
img {
	-ms-interpolation-mode: bicubic;
}
body, table, td, p, a {
	-ms-text-size-adjust: 100%;
	-webkit-text-size-adjust: 100%;
}
</style>
</head><body scroll=""auto"" style=""padding: 0; margin: 0; FONT-SIZE: 12px; FONT-FAMILY: Arial, Helvetica, sans-serif; cursor: auto; background: #F3F3F3""><table bgcolor=""#f3f3f3"" cellpadding=""0"" cellspacing=""0"" class=""mainTable"" width=""100%""><tr><td style=""FONT-SIZE: 0px; HEIGHT: 20px; LINE-HEIGHT: 0"">
	&nbsp;</td></tr><tr><td valign=""top""><table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" style=""WIDTH: 600px; MARGIN: 0px auto"" width=""600""><tr><td style=""BORDER-TOP: #dbdbdb 1px solid; BORDER-RIGHT: #dbdbdb 1px solid; BORDER-BOTTOM: medium none; PADDING-BOTTOM: 0px; PADDING-TOP: 0px; PADDING-LEFT: 0px; BORDER-LEFT: #dbdbdb 1px solid; PADDING-RIGHT: 0px; BACKGROUND-COLOR: #feffff""><table align=""left"" cellpadding=""0"" cellspacing=""0"" style=""WIDTH: 100%""><tr style=""HEIGHT: 10px""><td style=""BORDER-TOP: medium none; BORDER-RIGHT: medium none; WIDTH: 1%; VERTICAL-ALIGN: top; BORDER-BOTTOM: medium none; PADDING-BOTTOM: 5px; TEXT-ALIGN: center; PADDING-TOP: 5px; PADDING-LEFT: 15px; BORDER-LEFT: medium none; PADDING-RIGHT: 15px; BACKGROUND-COLOR: #feffff""><table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0""><tr><td align=""center"" style=""PADDING-BOTTOM: 2px; PADDING-TOP: 2px; PADDING-LEFT: 2px; PADDING-RIGHT: 2px""><table border=""0"" cellpadding=""0"" cellspacing=""0""><tr><td style=""BORDER-TOP: medium none; BORDER-RIGHT: medium none; BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; BACKGROUND-COLOR: transparent""><a href=""http://www.stafix24.pl/home/contact""><img alt=""RAWcom"" border=""0"" height=""66"" hspace=""0"" src=""https://mailchef.s3.amazonaws.com/uploads/785322/image/6461CB42-89B1-DDBA-6928-52502B66A62D_Image_1.png"" style=""BORDER-TOP: medium none; BORDER-RIGHT: medium none; BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; DISPLAY: block; BACKGROUND-COLOR: transparent"" vspace=""0"" width=""110""></a></td></tr></table></td></tr></table></td><td style=""BORDER-TOP: medium none; BORDER-RIGHT: medium none; WIDTH: 99%; VERTICAL-ALIGN: middle; BORDER-BOTTOM: medium none; PADDING-BOTTOM: 5px; TEXT-ALIGN: center; PADDING-TOP: 5px; PADDING-LEFT: 15px; BORDER-LEFT: medium none; PADDING-RIGHT: 15px; BACKGROUND-COLOR: #feffff"" valign=""middle""><p align=""left"" style=""MARGIN-BOTTOM: 1em; FONT-SIZE: 18px; FONT-FAMILY: Arial, Helvetica, sans-serif; COLOR: #7c7c7c; MARGIN-TOP: 0px; MARGIN-BOTTOM: 0px; LINE-HEIGHT: 1.3; BACKGROUND-COLOR: transparent""><strong>[[MessageHeader]]<br></strong><span style=""font-size: small"">[[MessageSubHeader]]</span></p></td></tr></table></td></tr><tr><td style=""BORDER-TOP: medium none; BORDER-RIGHT: #dbdbdb 1px solid; BORDER-BOTTOM: #dbdbdb 1px solid; PADDING-BOTTOM: 0px; PADDING-TOP: 0px; PADDING-LEFT: 0px; BORDER-LEFT: #dbdbdb 1px solid; PADDING-RIGHT: 0px; BACKGROUND-COLOR: #feffff""><table align=""left"" cellpadding=""0"" cellspacing=""0"" style=""WIDTH: 100%""><tr style=""HEIGHT: 20px""><td style=""BORDER-TOP: medium none; BORDER-RIGHT: medium none; WIDTH: 100%; VERTICAL-ALIGN: top; BORDER-BOTTOM: medium none; PADDING-BOTTOM: 35px; TEXT-ALIGN: center; PADDING-TOP: 15px; PADDING-LEFT: 15px; BORDER-LEFT: medium none; PADDING-RIGHT: 15px; BACKGROUND-COLOR: #feffff""><p align=""left"" style=""MARGIN-BOTTOM: 1em; FONT-SIZE: 12px; FONT-FAMILY: Arial, Helvetica, sans-serif; COLOR: #a7a7a7; MARGIN-TOP: 0px; LINE-HEIGHT: 1.3; BACKGROUND-COLOR: transparent"">[[MessageBody]]</p></td></tr></table></td></tr><tr><td style=""BORDER-TOP: medium none; BORDER-RIGHT: medium none; BORDER-BOTTOM: medium none; PADDING-BOTTOM: 1px; PADDING-TOP: 1px; PADDING-LEFT: 0px; BORDER-LEFT: medium none; PADDING-RIGHT: 0px; BACKGROUND-COLOR: transparent""><table align=""left"" cellpadding=""0"" cellspacing=""0"" style=""WIDTH: 100%""><tr style=""HEIGHT: 10px""><td style=""BORDER-TOP: medium none; BORDER-RIGHT: medium none; WIDTH: 100%; VERTICAL-ALIGN: top; BORDER-BOTTOM: medium none; PADDING-BOTTOM: 1px; TEXT-ALIGN: center; PADDING-TOP: 1px; PADDING-LEFT: 15px; BORDER-LEFT: medium none; PADDING-RIGHT: 15px; BACKGROUND-COLOR: transparent""><p align=""left"" style=""MARGIN-BOTTOM: 1em; FONT-SIZE: 10px; FONT-FAMILY: Arial, Helvetica, sans-serif; COLOR: #7c7c7c; MARGIN-TOP: 0px; LINE-HEIGHT: normal; BACKGROUND-COLOR: transparent"">RAWcom Sp. z o.o., Żelazna 67 lok.13, 00-871 Warszawa, NIP 5272714236, KRS 0000509990</p><p align=""left"" style=""MARGIN-BOTTOM: 1em; FONT-SIZE: 10px; FONT-FAMILY: Arial, Helvetica, sans-serif; COLOR: #7c7c7c; MARGIN-TOP: 0px; LINE-HEIGHT: normal; BACKGROUND-COLOR: transparent""><br><font style=""COLOR: #a7a7a7"">[[MessageID]]</font><br></p></td></tr></table></td></tr></table></td></tr><tr><td style=""FONT-SIZE: 0px; HEIGHT: 8px; LINE-HEIGHT: 0"">&nbsp;</td></tr></table></body></html>";


        private void sendReport_MethodInvoking(object sender, EventArgs e)
        {
            msgBody = string.Format("<h3>Wykonano raport dla:</h3><ul>{0}</ul>", sbAdamin.ToString());
            msgSubject = string.Format("Zestawienie Godzin - podsumowanie ({0})", workflowProperties.Web.Url);
            msgTo = workflowProperties.Workflow.AuthorUser.Email;
            if (string.IsNullOrEmpty(msgTo)) msgTo = _DEFAULT_SERVICE_EMAIL_;
        }

        private void onWorkflowActivated1_Invoked(object sender, ExternalDataEventArgs e)
        {
            Tools.Logger.LogEvent("rawcom.wyślij zestawienie godzin", "workflow started");

            //msgFrom = workflowProperties.Site.WebApplication.OutboundMailSenderAddress;
            targetDate = DateTime.Today.AddDays(-1);
            //targetDate = new DateTime(2016, 1, 8);
            //targetDate = new DateTime(2016, 1, 10);
            //targetDate = new DateTime(2016, 1, 31);

            logTargetDate_HistoryOutcome = targetDate.ToString();

            targetWeb = workflowProperties.Site.OpenWeb(_TS_WEB_);
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







        private void getListaKlientow_ExecuteCode(object sender, EventArgs e)
        {
            klienci = DAL.tabKlienci.Get_AktywniKlienci(targetWeb);

            //określ tryb pracy raportu
            if (klienci.Length > 0) isProductionMode = DAL.admSetup.Get_ValueByKey(workflowProperties.Site.RootWeb, "PRODUCTION_MODE").Equals("Enabled");
       
            kEnum = klienci.GetEnumerator();
        }

        private void cmdZainicjujRozliczenieKlienta_ExecuteCode(object sender, EventArgs e)
        {
            kItem = kEnum.Current as SPListItem;

            string nazwaKlienta = Tools.Lists.Get_Text(kItem, "colNazwaKlienta");
            sbAdamin.AppendFormat("<li>{0}</li>", nazwaKlienta);

            logKlient_HistoryDescription = nazwaKlienta;
            logKlient_HistoryOutcome = string.Format("#{0}", kItem.ID.ToString());
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

            Array results = DAL.tabKartyPracy.Get_ZestawienieMiesieczne_Serwis(targetWeb, kItem.ID, targetDate);
            if (results != null & results.Length > 0)
            {
                sbSRT = Create_ZestawienieAktywnosci(results, false);
                logRodzajRaportuS_HistoryOutcome = string.Format("Liczba rekordów: {0}", results.Length.ToString());
            }
            else
            {
                sbSRT = null;
                logRodzajRaportuS_HistoryOutcome = "brak danych";
            }
        }

        private void cmdPrzygotujTresc_S_T_ExecuteCode(object sender, EventArgs e)
        {
            logRodzajRaportuS_HistoryDescription = "Raport_S tygodniowy";

            Array results = DAL.tabKartyPracy.Get_ZestawienieTygodniowe_Serwis(targetWeb, kItem.ID, targetDate);
            if (results != null & results.Length > 0)
            {
                sbSRT = Create_ZestawienieAktywnosci(results, true);
                logRodzajRaportuS_HistoryOutcome = string.Format("Liczba rekordów: {0}", results.Length.ToString());
            }
            else
            {
                sbSRT = null;
                logRodzajRaportuS_HistoryOutcome = "brak danych";
            }
        }

        private void cmdPrzygotujTresc_S_D_ExecuteCode(object sender, EventArgs e)
        {
            logRodzajRaportuS_HistoryDescription = "Raport_S dzienny";

            Array results = DAL.tabKartyPracy.Get_ZestawienieDzienne_Serwis(targetWeb, kItem.ID, targetDate);
            if (results != null & results.Length > 0)
            {
                sbSRT = Create_ZestawienieAktywnosci(results, true);
                logRodzajRaportuS_HistoryOutcome = string.Format("Liczba rekordów: {0}", results.Length.ToString());
            }
            else
            {
                sbSRT = null;
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

                string opis = Tools.Lists.Format_String(Tools.Lists.Get_Text(item, "colOpis"));
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

            TimeSpan gTotal = DAL.tabKartyPracy.Get_TotalGodzinSerwisowychNarastajaco(targetWeb, kItem.ID, targetDate);
            sb = ReplacePlaceholder(sb, "Total_Narastajaco", Tools.Lists.Format_TimeSpan(gTotal));

            TimeSpan gRyczalt = new TimeSpan(0, (int)(Tools.Lists.Get_Value(kItem, "colLiczbaGodzinWAbonamencie") * 60), 0);
            sb = ReplacePlaceholder(sb, "Total_Ryczalt", Tools.Lists.Format_TimeSpan(gRyczalt));

            TimeSpan gPakiety = DAL.tabPakiety.Get_TotalAktywnychGodzin(targetWeb, kItem.ID, new DateTime(targetDate.Year, targetDate.Month, 1));
            sb = ReplacePlaceholder(sb, "Total_Pakiety", Tools.Lists.Format_TimeSpan(gPakiety));

            TimeSpan g = gTotal.Subtract(gRyczalt).Subtract(gPakiety);
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
                    case SerwisReportType.S_Dzienny:
                        msgSubject = "::Informacja o wykorzystanych godzinach wsparcia technicznego";
                        sb = ReplacePlaceholder(sb, "MessageHeader", Get_MessageHeader());
                        sb = ReplacePlaceholder(sb, "MessageSubHeader", Get_MessageSubHeader());
                        break;
                    case SerwisReportType.S_Tygodniowy:
                        msgSubject = "::Tygodniowe zestawienie wykorzystanych godzin wsparcia technicznego";
                        sb = ReplacePlaceholder(sb, "MessageHeader", Get_MessageHeader());
                        sb = ReplacePlaceholder(sb, "MessageSubHeader", Get_MessageSubHeader());
                        break;
                    case SerwisReportType.S_Miesieczny:
                        msgSubject = "::Miesięczne zestawienie wykorzystanych godzin wsparcia technicznego";
                        sb = ReplacePlaceholder(sb, "MessageHeader", Get_MessageHeader());
                        sb = ReplacePlaceholder(sb, "MessageSubHeader", Get_MessageSubHeader());
                        break;
                }

                msgBCC = _BCC_EMAIL_;
                msgTo = targetEmail;
                msgBody = sb.ToString();
            }
        }

        private string Get_MessageSubHeader()
        {
            return string.Format("w/g stanu na dzień {0}", Tools.Lists.Format_Date(targetDate));
        }

        private string Get_MessageHeader()
        {
            return string.Format("Dotyczy witryny <i>{0}</i>", Tools.Lists.Get_Text(kItem, "colNazwaKlienta"));
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



        private void cmdUpdateAdminReport_ExecuteCode(object sender, EventArgs e)
        {
            string nazwaKlienta = Tools.Lists.Get_Text(kItem, "colNazwaKlienta");
            sbAdamin.AppendFormat(@"<li> >>> wysłany</li>");
        }

        private void sendRaportDoKlienta_MethodInvoking(object sender, EventArgs e)
        {

        }

        private void SendMail_ExecuteCode(object sender, EventArgs e)
        {

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(_DEFAULT_SENDER, _DEFAULT_SENDER_NAME);
            mail.To.Add(new MailAddress(msgTo));
            mail.Subject = msgSubject;
            mail.Body = msgBody;
            mail.IsBodyHtml = true;
            if (!string.IsNullOrEmpty(msgCC)) mail.CC.Add(new MailAddress(msgCC));
            if (!string.IsNullOrEmpty(msgBCC)) mail.Bcc.Add(new MailAddress(msgBCC));
            if (!string.IsNullOrEmpty(msgReplyTo)) mail.ReplyTo = new MailAddress(msgReplyTo);
            //mail.Headers.Add("Importance", "High");

            Tools.SPEmail.SendMailWithAttachment(workflowProperties.Web, mail, null);
        }

        private void logWorkflowCompleted_ExecuteCode(object sender, EventArgs e)
        {
            Tools.Logger.LogEvent("rawcom.wyślij zestawienie godzin", "workflow completed");
        }

    }

    public enum SerwisReportType
    {
        S_Miesieczny,
        S_Tygodniowy,
        S_Dzienny
    }
}
