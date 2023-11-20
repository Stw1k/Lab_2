using Lab2;
using Microsoft.Maui;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Xsl;
using static System.Net.Mime.MediaTypeNames;

namespace Lab_2
{
    public partial class MainPage : ContentPage
    {
        private string _filePath = "";
        public string _outPath = "D:\\Проекти ООП\\Lab_2\\Lab_2\\SelectedStudents.xml";
        private string _error = "";
        private Dictionary<string, List<string>> _data = new Dictionary<string, List<string>>
        {
            { "FullName", new List<string>() },
            { "Faculty", new List<string>() },
            { "Group", new List<string>() },
            { "Speciality", new List<string>() },
            { "Marks", new List<string>() },
        };
        private ObservableCollection<Student> _results = new ObservableCollection<Student>();

        public MainPage()
        {
            InitializeComponent();
        }

        private void AddElemToPicker(XmlNode n)
        {
            foreach (var item in _data)
            {
                Pickers.AddPickerValue(n, item.Key, item.Value);
            }

        }

        private void ClearCriterias()
        {
            foreach (var list in _data.Values)
            {
                list.Clear();
            }
        }

        private void ClearPickersValues()
        {
            fullnamePicker.ItemsSource = null;
            facultyPicker.ItemsSource = null;
            groupPicker.ItemsSource = null;
            specialityPicker.ItemsSource = null;
        }

        private void SortPickersValues()
        {
            foreach (var list in _data.Values)
            {
                list.Sort();
            }
        }

        private void AddItemSourses()
        {
            SortPickersValues();
            fullnamePicker.ItemsSource = _data["FullName"];
            facultyPicker.ItemsSource = _data["Faculty"];
            groupPicker.ItemsSource = _data["Group"];
            specialityPicker.ItemsSource = _data["Speciality"];
        }

        private void FillPickers(string xmlPath)
        {
            ClearCriterias();
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlPath);


            XmlElement xRoot = doc.DocumentElement;
            XmlNodeList childNodes = xRoot.SelectNodes("Student");

            for (int i = 0; i < childNodes.Count; i++)
            {
                XmlNode childNode = childNodes.Item(i);
                AddElemToPicker(childNode);
            }
            
            ClearPickersValues();
            AddItemSourses();

        }

        private void UpdateFilters()
        {
            
            fullnamePicker.SelectedItem = null;
            facultyPicker.SelectedItem = null;
            groupPicker.SelectedItem = null;
            specialityPicker.SelectedItem = null;
            DOMOPt.IsChecked = true;
            SAXOpt.IsChecked = false;
            LINQOpt.IsChecked = false;
            _results.Clear();
            notFoundLabel.IsVisible = false;
        }

        async private void OnPickFileClicked(object sender, EventArgs e)
        {
            _error = "";
            try
            {
                var result = await FilePicker.PickAsync();
                if (result != null)
                {
                    string resultPath = result.FullPath;

                    if (File.Exists(resultPath))
                    {
                        string extension = Path.GetExtension(resultPath);
                        if (extension.Equals(".xml", StringComparison.OrdinalIgnoreCase))
                        {
                            _filePath = resultPath;
                            UpdateFilters();
                            FillPickers(_filePath);
                            if (!string.IsNullOrEmpty(_filePath) && string.IsNullOrEmpty(_error))
                            {
                                filters.IsVisible = true;
                            }
                            else
                            {
                                filters.IsVisible = false;
                            }
                        }
                        else
                        {
                            _error = "error";
                            await DisplayAlert("Помилка", "Обраний файл не є XML-файлом.", "ОК");
                        }
                    }
                    else
                    {
                        _error = "error";
                        await DisplayAlert("Помилка", "Обраного файлу не існує.", "ОК");
                    }
                }
            }
            catch (Exception ex)
            {
                _error = "error";
                await DisplayAlert("Помилка", $"{ex.Message}", "ОК");
            }
        }

        async private void OnHelpBtnClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Інформація",
                "Оберіть XML-файл на вашому комп'ютері. " +
                "Виберіть критерії пошуку та спосіб обробки документа та натисність кнопку 'Пошук' (за замовченням, якщо критерій пошуку не обрані, відображається весь вмість документа, а спосіб обробки - DOM). " +
                "Також Ви можете трансформувати обраний документ в HTML-файл або ж очистити критерії пошуку.",
                "ОК");
        }



        private async void OnTransformBtnClicked(object sender, EventArgs e)
        {

            
                XslCompiledTransform xsl = new XslCompiledTransform();
            
                try
                {
            xsl.Load(@"D:\Проекти ООП\Lab_2\Lab_2\XSLTFile1.xslt");
            string input = @"D:\Проекти ООП\Lab_2\Lab_2\SelectedStudents.xml";
            string result = @"D:\Проекти ООП\Lab_2\Lab_2\HTML.html";
            xsl.Transform(input, result);
            await DisplayAlert("Інформація", "Трансформація файлу успішна!", "ОК");
            
                }
            catch (Exception ex)
                {
                await DisplayAlert("Помилка", $"{ex.Message}", "ОК");
            }

            
        }

        private SearchArgument FormCriteria()
        {
            SearchArgument criteria = new SearchArgument();

          
            criteria.FullName = fullnamePicker.SelectedItem != null ? fullnamePicker.SelectedItem as string : string.Empty;
            criteria.Faculty = facultyPicker.SelectedItem != null ? facultyPicker.SelectedItem as string : string.Empty;
            criteria.Group = groupPicker.SelectedItem != null ? groupPicker.SelectedItem as string : string.Empty;
            criteria.Speciality = specialityPicker.SelectedItem != null ? specialityPicker.SelectedItem as string : string.Empty;

            return criteria;
        }

        private void OnCleanBtnClicked(object sender, EventArgs e)
        {
            UpdateFilters();
        }

        private async void OnSearchBtnClicked(object sender, EventArgs e)
        {
            ResultsListView.ItemsSource = null;
            SearchArgument argument = FormCriteria();

            ISearch analizator = new DomXmlSearch();

            if (DOMOPt.IsChecked)
            {
                analizator = new DomXmlSearch();
            }

            if (SAXOpt.IsChecked)
            {
                analizator = new SaxXmlSearch();
            }

            if (LINQOpt.IsChecked)
            {
                analizator = new LinqXmlSearch();
            }

            try
            {
                _results = analizator.Search(argument, _filePath, _outPath);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Помилка", $"{ex.Message}", "ОК");
            }

            ResultsListView.ItemsSource = _results;

            if (_results.Count > 0 && !string.IsNullOrEmpty(_filePath))
            {
                ResultsContainer.IsVisible = true;
                notFoundLabel.IsVisible = false;
            }
            else
            {
                ResultsContainer.IsVisible = false;
                if (!string.IsNullOrEmpty(_filePath))
                {
                    notFoundLabel.IsVisible = true;
                }
            }

        }
    }
}