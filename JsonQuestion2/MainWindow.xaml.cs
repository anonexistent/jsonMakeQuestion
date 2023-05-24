using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace JsonQuestion2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        object itemPrefub;

        public MainWindow()
        {
            InitializeComponent();
            //string prefXaml = XamlWriter.Save(pref);
            //StringReader sr = new(prefXaml);
            //XmlReader r = XmlReader.Create(sr);
            //itemPrefub = XamlReader.Load(r);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            lbAns.Items.Add(new TextBox() { Margin = new Thickness(5,15,5,5), MinHeight = 35, Background = Brushes.MistyRose, TextWrapping  = TextWrapping.Wrap });
            lbAns.Items.Add(new CheckBox() { Margin = new Thickness(5,5,5,15), Content = "correct", Background = Brushes.Coral });
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            lbAns.Items.Remove(lbAns.Items[^1]); lbAns.Items.Remove(lbAns.Items[^1]);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string tempQuestion = Regex.Replace(tbQs.Text, @"\t|\n|\r", "");
            tbQs.Text = tempQuestion;

            List<Answer> ans = new();

            for (int i = 0; i < lbAns.Items.Count; i+=2)
            {
                Answer temp = new();

                temp.Text = ((TextBox)lbAns.Items[i]).Text;
                temp.IsCorrect = ((CheckBox)lbAns.Items[i + 1]).IsChecked;

                ans.Add(temp);
            }

            Question q = new() { Text = tbQs.Text, Answers = ans };

            SendNewQuestion(q);
        }

        //      json array with only 1 element
        //          [{"Id":-1,"Text":"—","Answers":[{"Text":"—","IsCorrect":true},{"Text":"—","IsCorrect":true}]}]
        void SendNewQuestion(Question quest)
        {
            //string json = JsonConvert.SerializeObject(quest);
            //////MessageBox.Show(json);
            //JArray a = new JArray();
            //a.Add(json);
            //a.Add(json);

            //File.AppendAllText(Assembly.GetExecutingAssembly().Location.Substring(0, Assembly.GetExecutingAssembly().Location.Length-18), JsonConvert.SerializeObject(a));

            //JArray b = (JArray)JsonConvert.DeserializeObject(File.ReadAllText(@"C:\Users\SU\Desktop\JsonQuestion\JsonQuestion\JsonQuestion\bin\Debug\net6.0\test1.json"));

            string path = @"C:\Users\SU\Desktop\JsonQuestion\JsonQuestion2\JsonQuestion2\bin\Debug\net6.0-windows\test1.json";
            
            //  Assembly.GetExecutingAssembly() - dll, .procPath - exe
            int lastSlash = Environment.ProcessPath.LastIndexOf(@"\") + 1;
            string path2 = Environment.ProcessPath.Substring(0, lastSlash) + "test1.json";
            var list = JsonConvert.DeserializeObject<List<Question>>(File.ReadAllText(path2)) ?? new List<Question>();
            //MessageBox.Show(string.Join('—', list.Select(x=>x.Answers.Select(x=>x.Text))));
            list.Add(quest);
            var convertedJson = JsonConvert.SerializeObject(list);
            File.WriteAllText(path2, convertedJson);

            ClearWindow();
        }

        void ClearWindow()
        {
            tbQs.Text = "";

            while (lbAns.Items.Count > 0)
            {
                lbAns.Items.RemoveAt(lbAns.Items.Count-1);
            }
        }

        private void lbAns_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            
        }
    }
}
