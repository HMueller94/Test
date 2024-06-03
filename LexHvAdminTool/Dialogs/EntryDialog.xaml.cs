using LexHvAdminTool.Models;
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
using System.Windows.Shapes;

namespace LexHvAdminTool.Dialogs
{
    /// <summary>
    /// Interaktionslogik für EntryDialog.xaml
    /// </summary>
    public partial class EntryDialog : Window
    {
        public FaqEntry FaqEntry { get; private set; }

        public EntryDialog(FaqEntry faqEntry = null)
        {
            InitializeComponent();
            FaqEntry = faqEntry ?? new FaqEntry();

            if (faqEntry != null)
            {
                TitleTextBox.Text = faqEntry.Title;
                UrlTextBox.Text = faqEntry.Url;
                InformationTextBox.Text = faqEntry.Information;
                ImageURLTextBox.Text = faqEntry.ImageURL;
                CreateCalenderEventCheckBox.IsChecked = faqEntry.CreateCalenderEvent;
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            FaqEntry.Title = TitleTextBox.Text;
            FaqEntry.Url = UrlTextBox.Text;
            FaqEntry.Information = InformationTextBox.Text;
            FaqEntry.ImageURL = ImageURLTextBox.Text;
            FaqEntry.CreateCalenderEvent = CreateCalenderEventCheckBox.IsChecked ?? false;
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
