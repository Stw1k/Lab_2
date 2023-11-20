using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Lab_2
{
    class Pickers
    {
        static public void AddPickerValue(XmlNode n, string nodeName, List<string> pickerValues)
        {
            XmlNode childNode = n.SelectSingleNode(nodeName);
            if (childNode != null)
            {
                string nodeValue = childNode.InnerText;
                if (!string.IsNullOrEmpty(nodeValue) && !pickerValues.Contains(nodeValue))
                {
                    pickerValues.Add(nodeValue);
                }
            }
        }

        
    }
}