using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;

namespace DAL
{
    public class admSetup
    {
        const string targetList = "admSetup";


        public static string Get_ValueByKey(Microsoft.SharePoint.SPWeb web, string key)
        {
            SPListItem item = web.Lists.TryGetList(targetList).Items.Cast<SPListItem>()
                .Where(i => Tools.Lists.Get_Text(i, "KEY").Equals(key))
                .FirstOrDefault();

            return Tools.Lists.Get_Text(item, "VALUE");
        }
    }
}
