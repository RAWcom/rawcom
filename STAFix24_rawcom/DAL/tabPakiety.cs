using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;

namespace DAL
{
    public class tabPakiety
    {
        const string targetList = "Pakiety";

        public static TimeSpan Get_TotalAktywnychGodzin(SPWeb web, int klientId, DateTime targetDate)
        {
            Array results = web.Lists.TryGetList(targetList).Items.Cast<SPListItem>()
                .Where(i => Tools.Lists.Get_LookupId(i, "selKlient").Equals(klientId))
                .Where(i => Tools.Lists.Get_Text(i, "colStatusPakietu").Equals("Aktywny"))
                .Where(i => Tools.Lists.Get_Date(i, "colObowiazujeDo").CompareTo(targetDate) >= 0
                            || Tools.Lists.Get_Date(i, "colObowiazujeDo").CompareTo(new DateTime())==0)
                .ToArray();

            double godzinyRazem = 0;

            foreach (SPListItem item in results)
            {
                godzinyRazem = godzinyRazem + Tools.Lists.Get_Value(item, "colLiczbaGodzinWPakiecie");
            }

            return new TimeSpan(0,(int)(godzinyRazem * 60), 0);
        }
    }
}
