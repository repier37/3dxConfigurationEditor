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
    /// Not used anymore, this window should be able to create a macro entirely, but can not get the correct HID value for Qwerty AND Azerty KB layout
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
            this.Sequence = new List<KeyWithAction>();
        }


        private List<KeyWithAction> Sequence;


        
        private bool CanAccept()
        {
            if (!this.IsLoaded) return false;
            if (string.IsNullOrWhiteSpace(this.FilePathTextBox.Text))
                return false;
            if (string.IsNullOrWhiteSpace(this.TextBloxMacroName.Text))
                return false;

            if (string.IsNullOrWhiteSpace(this.TextBoxMacroKey.Text))
                return false;

            //picture is an option

            return true;
        }

        private void updateValidation()
        {
            if (!this.IsLoaded) return;
            this.AddMacroButton.IsEnabled = this.CanAccept();
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
            }
        }

        private void AddMacroButton_Click(object sender, RoutedEventArgs e)
        {
            //open the file if possible
            string path = this.FilePathTextBox.Text;
            string imagePath = this.TextBoxImageFilePath.Text;
            if (!System.IO.File.Exists(path))
                return;

            string macroId = this.TextBloxMacroName.Text;

            XMLEditor editor = new XMLEditor(path);
            editor.AddMacroEntry(macroId, this.Sequence);//add the macro xml block
            //Optionnaly add image to the macro
            if (!string.IsNullOrEmpty(imagePath))
                editor.AddButtonAction(macroId, imagePath);//ad the image button action

            this.Sequence.Clear();
            this.TextBoxMacroKey.Clear();
        }

        private void TextBloxMacroName_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.updateValidation();
        }

        private void TextBoxMacroKey_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.updateValidation();
        }

        private void TextBoxImageFilePath_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.updateValidation();
        }

        private void FilePathTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.updateValidation();
        }

        private void TextBoxMacroKey_KeyDown(object sender, KeyEventArgs e)
        {
            this.Sequence.Add(new KeyWithAction(e.Key, KeyAction.Pressed));
            this.TextBoxMacroKey.Text += "[Key " + e.Key.ToString() + " pressed]";
        }

        private void TextBoxMacroKey_KeyUp(object sender, KeyEventArgs e)
        {
            this.Sequence.Add(new KeyWithAction(e.Key, KeyAction.Released));
            this.TextBoxMacroKey.Text += "[Key " + e.Key.ToString() + " released]";
        }

        private void ClearMacro_Click(object sender, RoutedEventArgs e)
        {
            this.Sequence.Clear();
            this.TextBoxMacroKey.Clear();
        } 
        #endregion
    }
}
