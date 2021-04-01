using _3DxConfigurationEditor.Objects;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace _3DxConfigurationEditor
{
    /// <summary>
    /// Window used to add an icon to an existing macro
    /// </summary>
    public partial class MainWindowEdit : Window
    {

        /// <summary>
        /// Ctor
        /// </summary>
        public MainWindowEdit()
        {
            InitializeComponent();
            this.Editor = new XMLEditor(string.Empty);
        }

        /// <summary>
        /// The XML Editor
        /// </summary>
        public XMLEditor Editor { get; set; }

        /// <summary>
        /// True if all required field to edit macro are filled
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Update hte AddIcon status
        /// </summary>
        private void updateValidation()
        {
            if (!this.IsLoaded) return;
            this.AddIconButton.IsEnabled = this.CanAccept();
        }

        /// <summary>
        /// Fille the macro combobox
        /// </summary>
        private void LoadMacro()
        {
            this.ComboMacro.Items.Clear();

            if (!Editor.HasLoadedFile)
                return;

            foreach (MacroEntry macro in Editor.Macros)
            {
                this.ComboMacro.Items.Add(macro);
            }
            if (this.ComboMacro.Items.Count>0)
                this.ComboMacro.SelectedIndex = 0;
        }

        #region Events
        /// <summary>
        /// Open a diloag to browse to the configuration file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = "%AppData%\\3Dconnexion\\3DxWare\\Cfg";

            dialog.RestoreDirectory = true;
            dialog.Filter = "xml files (*.xml)|*.xml";
            bool? dialogResult = dialog.ShowDialog();
            if (dialogResult == true)
            {
                this.FilePathTextBox.Text = dialog.FileName;
            }
        }

        /// <summary>
        /// Open a dialog to browse to an the image file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Update the editor upon file path change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilePathTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!this.IsLoaded)
                return;
            this.Editor.FilePath = this.FilePathTextBox.Text;
            if (!this.Editor.Load(out string error))
            {
                MessageBox.Show("Could not open the specified file: " + error, "File error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            this.updateValidation();
            this.LoadMacro();
        }

        /// <summary>
        /// Update the configuration file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddIconButton_Click(object sender, RoutedEventArgs e)
        {
            MacroEntry macroID = this.ComboMacro.SelectedItem as MacroEntry;
            string imagePath = this.TextBoxImageFilePath.Text;

            if (!this.Editor.AddButtonAction(macroID, imagePath))
            {
                MessageBox.Show("Something went wrong. The icon has not been added", "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Icon added succesfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Editor.Load(out _);
            }
                
        }

        private void ComboMacro_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.updateValidation();
            ButtonAction buttonFromMacro = Editor.GetButtonActionFromMacro(this.ComboMacro.SelectedItem as MacroEntry);
            if (buttonFromMacro is null)
                return;

            if (buttonFromMacro.HasImage)
            {
                Uri fileUri = new Uri(buttonFromMacro.ImageSource);
                this.CurrentImage.Source = new BitmapImage(fileUri);
            }


        }
        #endregion


    }
}
