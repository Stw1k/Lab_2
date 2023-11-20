using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    internal interface ISearch
    {
        ObservableCollection<Student> Search(SearchArgument argument, string xmlPath, string outPath);
    }
}
