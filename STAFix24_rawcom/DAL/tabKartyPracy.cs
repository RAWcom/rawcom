using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;

namespace DAL
{
    public class tabKartyPracy
    {
        const string targetList = "Karty pracy";
        private static string _CT_KARTA_PRACY_ = "Karta pracy";

        /// <summary>
        /// wyszukuje rekordy rozliczeń godzin serwisoch dla zadanego klienta i miesiąca wskazywanego przez target date.
        /// </summary>
        public static Array Get_ZestawienieMiesieczne_Serwis(SPWeb web, int klientId, DateTime targetDate)
        {
            return web.Lists.TryGetList(targetList).Items.Cast<SPListItem>()
                .Where(i => i.ContentType.Name.Equals(_CT_KARTA_PRACY_)
                            && Tools.Lists.Get_LookupId(i, "selKlient").Equals(klientId)
                            && Tools.Lists.Get_Date(i, "colData").Year.Equals(targetDate.Year)
                            && Tools.Lists.Get_Date(i, "colData").Month.Equals(targetDate.Month))
                .ToArray();
        }

        public static Array Get_ZestawienieTygodniowe_Serwis(SPWeb web, int klientId, DateTime targetDate)
        {
            DateTime startDate = targetDate.AddDays(-6);

            return web.Lists.TryGetList(targetList).Items.Cast<SPListItem>()
                .Where(i => i.ContentType.Name.Equals(_CT_KARTA_PRACY_)
                            && Tools.Lists.Get_LookupId(i, "selKlient").Equals(klientId)
                            && Tools.Lists.Get_Date(i, "colData") >= startDate
                            && Tools.Lists.Get_Date(i, "colData") <= targetDate)
                            .ToArray();
        }

        public static Array Get_ZestawienieDzienne_Serwis(SPWeb web, int klientId, DateTime targetDate)
        {
            return web.Lists.TryGetList(targetList).Items.Cast<SPListItem>()
                .Where(i => i.ContentType.Name.Equals(_CT_KARTA_PRACY_)
                            && Tools.Lists.Get_LookupId(i, "selKlient").Equals(klientId)
                            && Tools.Lists.Get_Date(i, "colData").Equals(targetDate))
                            .ToArray();
        }

        /// <summary>
        /// oblicza sumę wykorzystanych godziny za dany miesiąc do targetDate włącznie.
        /// </summary>
        public static TimeSpan Get_TotalGodzinSerwisowychNarastajaco(SPWeb web, int klientId, DateTime targetDate)
        {
            TimeSpan ts = new TimeSpan();
            Array results = Get_ZestawienieMiesieczne_Serwis(web, klientId, targetDate);
            foreach (SPListItem item in results)
            {
                if (Tools.Lists.Get_Date(item, "colData") <= targetDate)
                {
                    int m = (int)Tools.Lists.Get_Value(item, "colCzasMin");
                    if (m > 0) ts = ts.Add(new TimeSpan(0, m, 0));
                }
            }

            return ts;
        }
    }
}
