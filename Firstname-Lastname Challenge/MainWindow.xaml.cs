using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Text.RegularExpressions;

namespace Firstname_Lastname_Challenge
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string name;
        string phoneNum;
        string email;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void openButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog infoDialog = new OpenFileDialog();
            infoDialog.Filter = "Text Document|*.txt";
            infoDialog.DefaultExt = ".txt";
            Nullable<bool> dialogOk = infoDialog.ShowDialog();

            fileLocation.Text = infoDialog.FileName;
            rawInfoTextBlock.Text = File.ReadAllText(fileLocation.Text);

            BusinessCardParser(rawInfoTextBlock.Text);
        }

        private void BusinessCardParser(string contact)
        {
            string[] stringSeperators = new string[] { "\n" };
            string[] contactLine = contact.Split(stringSeperators, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < 3; i++)
            {
                if(!contactLine[i].Contains("Technology") && !contactLine[i].Contains("Technologies") && !contactLine[i].Contains("Systems")
                    && !contactLine[i].Contains("Developer") && !contactLine[i].Contains("Engineer"))
                {
                    name = contactLine[i];
                }
            }

            foreach (string line in contactLine)
            {
                if(line.Contains("Phone") || line.Contains("Tel") || line.Contains("(") || Regex.IsMatch(line, @"^-?\d+$"))
                {
                    phoneNum = line;  
                }
                if (line.Contains("Email") || line.Contains("@"))
                {
                    email = line;
                }
            }

            ShowName(name);
            ShowPhoneNumber(phoneNum);
            ShowEmail(email);
        }

        private void ShowName(string name)
        {
            nameTextBlock.Text = name;
        }

        private void ShowPhoneNumber(string num)
        {
            phoneTextBlock.Text = Regex.Replace(num, @"[^\d]", "");
        }
        private void ShowEmail(string email)
        {
            emailTextBlock.Text = email;
        }

    }
}
