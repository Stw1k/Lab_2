

using Lab2;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Lab_2
{
    internal class DomXmlSearch : ISearch
    {
        public DomXmlSearch() { }

        private void ProcessNode(XmlNode n, ObservableCollection<Student> results, SearchArgument argument)
        {
            string fullName = "";
            string faculty = "";
            string group = "";
            string speciality = "";
            string marks = "";


            foreach (XmlNode childNode in n.ChildNodes)
            {
                string nodeValue = childNode.InnerText;

                if (childNode.Name.Equals("FullName") && SearchHelp.IsValidEntry(nodeValue, argument.FullName))
                {
                    fullName = nodeValue;
                }
                if (childNode.Name.Equals("Faculty") && SearchHelp.IsValidPickerValue(nodeValue, argument.Faculty))
                {
                    faculty = nodeValue;
                }
                if (childNode.Name.Equals("Group") && SearchHelp.IsValidPickerValue(nodeValue, argument.Group))
                {
                    group = nodeValue;
                }
                if (childNode.Name.Equals("Speciality") && SearchHelp.IsValidPickerValue(nodeValue, argument.Speciality))
                {
                    speciality = nodeValue;
                }
                if (childNode.Name.Equals("Marks") && SearchHelp.IsValidPickerValue(nodeValue, argument.Marks))
                {
                    marks = nodeValue;
                }

            }

            if (fullName != "" && faculty != "" && faculty != "" && group != "" && speciality != "" && marks != "")
            {
                Student stud = new Student();
                stud.FullName = fullName;
                stud.Faculty = faculty;
                stud.Group = group;
                stud.Speciality = speciality;
                stud.Marks = marks;

                results.Add(stud);
            }
        }

        public ObservableCollection<Student> Search(SearchArgument argument, string xmlPath, string outPath)
        {
            ObservableCollection<Student> results = new ObservableCollection<Student>();
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlPath);

            XmlNode node = doc.DocumentElement;
            foreach (XmlNode nod in node.ChildNodes)
            {
                ProcessNode(nod, results, argument);
            }
            XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));
            List<Student> students = new List<Student>(results);
            TextWriter writer = new StreamWriter(outPath);
            serializer.Serialize(writer, students);
            return results;
        }
    }
}