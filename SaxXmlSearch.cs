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
    internal class SaxXmlSearch : ISearch
    {
        public SaxXmlSearch() { }

        public ObservableCollection<Student> Search(SearchArgument argument, string xmlPath, string outPath)
        {
            ObservableCollection<Student> results = new ObservableCollection<Student>();

            using (var xmlReader = new XmlTextReader(xmlPath))
            {
                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "Student")
                    {
                        string fullName = "";
                        string faculty = "";
                        string group = "";
                        string speciality = "";
                        string marks = "";

                        while (xmlReader.Read())
                        {
                            if (xmlReader.NodeType == XmlNodeType.Element)
                            {
                                switch (xmlReader.Name)
                                {
                                    case "FullName":
                                        {
                                            xmlReader.Read();
                                            if (xmlReader.NodeType == XmlNodeType.Text)
                                            {
                                                if (SearchHelp.IsValidEntry(xmlReader.Value, argument.FullName))
                                                {
                                                    fullName = xmlReader.Value;
                                                }
                                            }

                                            break;
                                        }
                                    case "Faculty":
                                        {
                                            xmlReader.Read();
                                            if (xmlReader.NodeType == XmlNodeType.Text)
                                            {
                                                if (SearchHelp.IsValidEntry(xmlReader.Value, argument.Faculty))
                                                {
                                                    faculty = xmlReader.Value;
                                                }
                                            }
                                            break;
                                        }
                                    case "Group":
                                        {
                                            xmlReader.Read();
                                            if (xmlReader.NodeType == XmlNodeType.Text)
                                            {
                                                if (SearchHelp.IsValidEntry(xmlReader.Value, argument.Group))
                                                {
                                                    group = xmlReader.Value;
                                                }
                                            }
                                            break;
                                        }
                                    case "Speciality":
                                        {
                                            xmlReader.Read();
                                            if (xmlReader.NodeType == XmlNodeType.Text)
                                            {
                                                if (SearchHelp.IsValidEntry(xmlReader.Value, argument.Speciality))
                                                {
                                                    speciality = xmlReader.Value;
                                                }
                                            }
                                            break;
                                        }
                                    case "Marks":
                                        {
                                            xmlReader.Read();
                                            if (xmlReader.NodeType == XmlNodeType.Text)
                                            {
                                                if (SearchHelp.IsValidEntry(xmlReader.Value, argument.Marks))
                                                {
                                                    marks = xmlReader.Value;
                                                }
                                            }
                                            break;
                                        }

                                }
                            }
                            else if (xmlReader.NodeType == XmlNodeType.EndElement && xmlReader.Name == "Student")
                            {
                                break;
                            }
                        }

                        if (fullName != "" && faculty != "" && group != "" && speciality != "" && marks != "")
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
                }
            }
            XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));
            List<Student> students = new List<Student>(results);
            TextWriter writer = new StreamWriter(outPath);
            serializer.Serialize(writer, students);
            return results;
        }
    }
}