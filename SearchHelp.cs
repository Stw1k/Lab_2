using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Lab_2
{
    internal class SearchHelp
    {
        private static bool IsYearInInterval(string interval, string obj)
        {
            string[] parts = interval.Split('-');
            if (parts.Length == 2 && int.TryParse(parts[0], out int startYear) && int.TryParse(parts[1], out int endYear))
            {
                if (int.TryParse(obj, out int objToCheck))
                {
                    return objToCheck >= startYear && objToCheck <= endYear;
                }
            }

            return false;
        }
        public static bool IsValidEntry(string nodeValue, string criteriaValue)
        {
            return string.IsNullOrEmpty(criteriaValue) || nodeValue.Trim().ToLower().Contains(criteriaValue?.Trim().ToLower());
        }

        public static bool IsValidPickerValue(string nodeValue, string criteriaValue)
        {
            return string.IsNullOrEmpty(criteriaValue) || nodeValue.Equals(criteriaValue);
        }

        public static bool IsValidYear(string nodeValue, string criteriaValue)
        {
            return string.IsNullOrEmpty(criteriaValue) || IsYearInInterval(criteriaValue, nodeValue);
        }
    }
}