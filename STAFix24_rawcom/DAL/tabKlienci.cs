using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;

namespace DAL
{
    public class tabKlienci
    {
        const string targetList = "Klienci";

        public static Array Get_AktywniKlienci(SPWeb web)
        {
            return web.Lists.TryGetList(targetList).Items.Cast<SPListItem>()
                .Where(i => Tools.Lists.Get_Flag(i, "colAktywnyMailing"))
                .ToArray();
        }
    }
}
