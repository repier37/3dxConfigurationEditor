using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace _3DxConfigurationEditor
{
    /// <summary>
    /// Window used to add an icon to an existing macro
    /// </summary>
    public partial class MainWindowEdit : Window
    {


        public MainWindowEdit()
        {
            InitializeComponent();
            this.Editor = new XMLEditor(string.Empty);
        }

        public XMLEditor Editor { get; set; }


        private bool CanAccept()
        {
            if (!this.IsLoaded) return false;
            if (string.IsNullOrWhiteSpace(this.FilePathTextBox.Text))
                return false;
            if (string.IsNullOrWhiteSpace(this.ComboMacro.Text))
                return false;

            if (string.IsNullOrWhiteSpace(this.TextBoxImageFilePath.Text))
                return false;

            //picture is an option

            return true;
        }

        private void updateValidation()
        {
            if (!this.IsLoaded) return;
            this.AddIconButton.IsEnabled = this.CanAccept();
        }

        private void LoadMacro()
        {
            this.ComboMacro.Items.Clear();
            string path = this.FilePathTextBox.Text;
            if (!System.IO.File.Exists(path))
                return;

            XMLEditor editor = new XMLEditor(path);
            List<String> existingMacros = editor.GetExistingMacros();
            foreach (string macroName in existingMacros)
            {
                this.ComboMacro.Items.Add(macroName);
            }
            if (this.ComboMacro.Items.Count>0)
                this.ComboMacro.SelectedIndex = 0;
        }

        #region Events
        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.RestoreDirectory = true;
            dialog.Filter = "xml files (*.xml)|*.xml";
            bool? dialogResult = dialog.ShowDialog();
            if (dialogResult == true)
            {
                this.FilePathTextBox.Text = dialog.FileName;
            }
        }

        private void BrowseImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.RestoreDirectory = true;
            dialog.Filter = "Image files (*.png;*.jpeg;*.ico)|*.png;*.jpeg;*.ico";
            bool? dialogResult = dialog.ShowDialog();
            if (dialogResult == true)
            {
                this.TextBoxImageFilePath.Text = dialog.FileName;
                Uri fileUri = new Uri(dialog.FileName);
                this.ImagePreview.Source = new BitmapImage(fileUri);

            }
        }

        private void TextBoxImageFilePath_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!this.IsLoaded) return;
            this.updateValidation();
        }

        private void FilePathTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!this.IsLoaded)
                return;
            this.Editor.FilePath = this.FilePathTextBox.Text;
            this.updateValidation();
            this.LoadMacro();
        }

        private void AddIconButton_Click(object sender, RoutedEventArgs e)
        {
            string macroID = this.ComboMacro.Text;
            string imagePath = this.TextBoxImageFilePath.Text;

            if (!this.Editor.AddButtonAction(macroID, imagePath))
            {
                MessageBox.Show("Something went wrong. The icon has not been added", "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
                MessageBox.Show("Icon added succesfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        #endregion

        private void ComboMacro_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.updateValidation();
        }
    }
}
