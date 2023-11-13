using System;
using System.IO;
using System.Text;
using System.Windows;
using Microsoft.Win32;

namespace WpfApp6
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            StartButton.Click += StartButton_Click;
            FileButton.Click += FileButton_Click;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            string filePath = FilePathTextBox.Text;
            string password = PasswordTextBox.Text;

            if (string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Həm fayli hemde parolu daxil edin");
                return;
            }

            if (!File.Exists(filePath))
            {
                MessageBox.Show("secilmis fayl movcud deyl");
                return;
            }

            if (EncryptRadioButton.IsChecked == true)
            {
                EncryptFile(filePath, password);
            }
            else if (DecryptRadioButton.IsChecked == true)
            {
                DecryptFile(filePath, password);
            }
        }

        private void EncryptFile(string filePath, string password)
        {
            try
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                byte[] fileBytes = File.ReadAllBytes(filePath);
                for (int i = 0; i < fileBytes.Length; i++)
                {
                    fileBytes[i] = (byte)(fileBytes[i] ^ passwordBytes[i % passwordBytes.Length]);
                }

                File.WriteAllBytes(filePath, fileBytes);
                MessageBox.Show("Fayl sifrelendi");
            }
            catch (Exception ex)
            {
                MessageBox.Show("xeta bas verdi: " + ex.Message);
            }
        }

        private void DecryptFile(string filePath, string password)
        {
            EncryptFile(filePath, password);
        }

        private void FileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "butun fayllar|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                FilePathTextBox.Text = openFileDialog.FileName;
            }
        }
    }
}
