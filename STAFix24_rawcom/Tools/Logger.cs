using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Administration;

namespace Tools
{
    public class Logger
    {
        public static void LogEvent(string subject, string body)
        {
            SPDiagnosticsService diagSvc = SPDiagnosticsService.Local;

            diagSvc.WriteTrace(0,
                new SPDiagnosticsCategory("STAFix category", TraceSeverity.Monitorable, EventSeverity.Error),
                TraceSeverity.Monitorable,
                subject.ToString() + ":  {0}",
                new object[] { body.ToString() });
        }
    }
}
