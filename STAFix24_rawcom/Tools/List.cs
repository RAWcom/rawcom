using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using System.Globalization;

namespace Tools
{
    public class Lists
    {
        private static string _EMPTY_MARKER_ = "---";

        public static int Get_LookupId(SPListItem item, string col)
        {
            return item[col] != null ? new SPFieldLookupValue(item[col].ToString()).LookupId : 0;
        }

        public static string Format_Currency(SPListItem item, string colName)
        {
            double n = Get_Value(item, colName);

            if (n > 0) return n.ToString("c", new CultureInfo("pl-PL"));
            else return _EMPTY_MARKER_;

        }

        public static double Get_Value(SPListItem item, string colName)
        {
            if (item[colName] != null)
            {
                return double.Parse(item[colName].ToString());
            }
            else
            {
                return 0;
            }
        }

        public static string Format_Currency(double value)
        {
            if (value > 0) return value.ToString("c", new CultureInfo("pl-PL"));
            else return _EMPTY_MARKER_;
        }

        public static DateTime Get_Date(SPListItem item, string col)
        {
            return item[col] != null ? DateTime.Parse(item[col].ToString()) : new DateTime();
        }

        public static string Get_Text(SPListItem item, string col)
        {
            return item[col] != null ? item[col].ToString() : string.Empty;
        }

        public static string Get_LookupValue(SPListItem item, string col)
        {
            return item[col] != null ? new SPFieldLookupValue(item[col].ToString()).LookupValue : string.Empty;
        }

        public static bool Get_Flag(SPListItem item, string col)
        {
            return item[col] != null ? (bool)item[col] : false;
        }

        public static void Clear_Value(SPListItem item, string col)
        {

            if (item[col] != null)
            {
                item[col] = string.Empty;
                item.SystemUpdate();
            }
        }

        public static void Clear_Flag(SPListItem item, string col)
        {
            if (item[col] != null)
            {
                item[col] = false;
                item.SystemUpdate();
            }
        }

        public static bool Is_ValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static void Set_Text(SPListItem item, string col, string val)
        {
            item[col] = val.ToString();
        }

        internal static Array Get_LookupValueCollection(SPListItem item, string col)
        {
            return item[col] != null ? new SPFieldLookupValueCollection(item[col].ToString()).ToArray() : null;
        }

        public static void Set_Flag(SPListItem item, string col, bool v)
        {
            item[col] = v;
        }

        public static SPFieldLookupValueCollection Get_LookupValueColection(SPListItem item, string col)
        {
            return item[col] != null ? new SPFieldLookupValueCollection(item[col].ToString()) : null;
        }


        public static TimeSpan Get_TimeSpan(SPListItem item, int minutes)
        {
            return new TimeSpan(0, minutes, 0);
        }

        public static string Format_TimeSpan(TimeSpan ts)
        {
            if (ts != null) return string.Format("{0:00}:{1:00}",(int)ts.TotalHours, ts.Minutes);
            else return _EMPTY_MARKER_;
        }

        public static string Format_String(String s)
        {
            return Microsoft.SharePoint.Utilities.SPHttpUtility.HtmlEncode(s);
        }

        public static string Format_Date(DateTime d)
        {
            if (d != null) return d.ToShortDateString();
            else return _EMPTY_MARKER_;
        }

        public static string Get_Email(SPListItem item, string col)
        {
            string email = Get_Text(item, col);
            if (Is_ValidEmail(email))
            {
                return email;
            }
            else
            {
                return string.Empty;
            }
        }
    }

}
