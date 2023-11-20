using Lab2;
using System.Xml.Serialization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab_2
{
    internal class LinqXmlSearch : ISearch
    {
        public LinqXmlSearch() { }

        public ObservableCollection<Student> Search(SearchArgument argument, string xmlPath, string outPath)
        {
            ObservableCollection<Student> results = new ObservableCollection<Student>();
            var doc = XDocument.Load(xmlPath);
            
            var result = from stud in doc.Descendants("Student")
                         where (
                         SearchHelp.IsValidPickerValue(stud.Element("FullName").Value, argument.FullName) &&
                         SearchHelp.IsValidPickerValue(stud.Element("Faculty").Value, argument.Faculty) &&
                         SearchHelp.IsValidPickerValue(stud.Element("Group").Value, argument.Group) &&
                         SearchHelp.IsValidPickerValue(stud.Element("Speciality").Value, argument.Speciality) 
                         )

                         select new Student
                         {
                             FullName = stud.Element("FullName").Value,
                             Faculty = stud.Element("Faculty").Value,
                             Group = stud.Element("Group").Value,
                             Speciality = stud.Element("Speciality").Value,
                             Marks = stud.Element("Marks").Value,
                         };
            foreach (var stud in result)
            {

                results.Add(stud);
                
            }
            XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));
            List<Student> students = new List<Student>(result);
            TextWriter writer = new StreamWriter(outPath);
            serializer.Serialize(writer, students);

            return results;
        }
    }
}