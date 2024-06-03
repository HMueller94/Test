using LexHvAdminTool.Dialogs;
using LexHvAdminTool.Models;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LexHvAdminTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FaqData _faqData;
        private ObservableCollection<FaqEntry> _faqEntries;
        private Point _dragStartPoint;
        private string _currentFilePath;

        public MainWindow()
        {
            InitializeComponent();
            _faqEntries = new ObservableCollection<FaqEntry>();
            FaqListBox.ItemsSource = _faqEntries;

            FaqListBox.MouseMove += FaqListBox_MouseMove;

            _faqData = new FaqData
            {
                Entries = new List<FaqEntry>()
            };

            UpdateMetadata();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var entryDialog = new EntryDialog();
            if (entryDialog.ShowDialog() == true)
            {
                _faqEntries.Add(entryDialog.FaqEntry);
                UpdateOrder();
                UpdateMetadata();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (FaqListBox.SelectedItem is FaqEntry selectedEntry)
            {
                var entryDialog = new EntryDialog(selectedEntry);
                if (entryDialog.ShowDialog() == true)
                {
                    var index = _faqEntries.IndexOf(selectedEntry);
                    _faqEntries[index] = entryDialog.FaqEntry;
                    UpdateMetadata();
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (FaqListBox.SelectedItem is FaqEntry selectedEntry)
            {
                _faqEntries.Remove(selectedEntry);
                UpdateOrder();
                UpdateMetadata();
            }
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog { Filter = "JSON Files|*.json" };
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    var json = File.ReadAllText(openFileDialog.FileName);
                    _faqData = JsonConvert.DeserializeObject<FaqData>(json);

                    _faqEntries.Clear();
                    foreach (var entry in _faqData.Entries)
                    {
                        _faqEntries.Add(entry);
                    }
                    _currentFilePath = openFileDialog.FileName;  // Store the file path
                    UpdateOrder();
                    UpdateMetadata();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error importing file: " + ex.Message);
                }
            }
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog { Filter = "JSON Files|*.json" };
            if (saveFileDialog.ShowDialog() == true)
            {
                SaveToFile(saveFileDialog.FileName);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_currentFilePath))
            {
                SaveToFile(_currentFilePath);
            }
            else
            {
                MessageBox.Show("No file currently loaded. Please use the Export button to save to a new file.");
            }
        }

        private void SaveToFile(string filePath)
        {
            _faqData.Entries = new List<FaqEntry>(_faqEntries);
            _faqData.LastModified = DateTime.Now;
            _faqData.ModifiedBy = Environment.UserName;  // Automatically set to current user's username

            var json = JsonConvert.SerializeObject(_faqData, Formatting.Indented);
            File.WriteAllText(filePath, json);

            // Update metadata immediately after saving
            UpdateMetadata();
        }

        private void NewFileButton_Click(object sender, RoutedEventArgs e)
        {
            _faqEntries.Clear();
            _faqData = new FaqData
            {
                Entries = new List<FaqEntry>(),
                LastModified = DateTime.Now,
                ModifiedBy = Environment.UserName
            };
            _currentFilePath = null;
            UpdateMetadata();
        }

        private void FaqListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _dragStartPoint = e.GetPosition(null);
        }

        private void FaqListBox_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = _dragStartPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                ListView listBox = sender as ListView;
                ListViewItem listBoxItem = FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);

                if (listBoxItem != null)
                {
                    FaqEntry faqEntry = (FaqEntry)listBox.ItemContainerGenerator.ItemFromContainer(listBoxItem);
                    DataObject dragData = new DataObject("myFormat", faqEntry);
                    DragDrop.DoDragDrop(listBoxItem, dragData, DragDropEffects.Move);
                }
            }
        }

        private static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            while (current != null && !(current is T))
            {
                current = VisualTreeHelper.GetParent(current);
            }
            return current as T;
        }

        private void FaqListBox_DragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Move;
        }

        private void FaqListBox_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                FaqEntry faqEntry = e.Data.GetData("myFormat") as FaqEntry;
                ListView listBox = sender as ListView;
                Point position = e.GetPosition(listBox);
                int index = -1;

                for (int i = 0; i < listBox.Items.Count; i++)
                {
                    ListViewItem listBoxItem = (ListViewItem)listBox.ItemContainerGenerator.ContainerFromIndex(i);
                    if (listBoxItem != null)
                    {
                        Rect bounds = VisualTreeHelper.GetDescendantBounds(listBoxItem);
                        Point topLeft = listBoxItem.TranslatePoint(new Point(bounds.X, bounds.Y), listBox);

                        if (position.Y < topLeft.Y + listBoxItem.ActualHeight)
                        {
                            index = i;
                            break;
                        }
                    }
                }

                if (index == -1)
                {
                    index = listBox.Items.Count;
                }

                _faqEntries.Remove(faqEntry);
                _faqEntries.Insert(index, faqEntry);
                UpdateOrder();
                UpdateMetadata();
            }
        }

        private void UpdateOrder()
        {
            for (int i = 0; i < _faqEntries.Count; i++)
            {
                _faqEntries[i].OrderBy = i + 1;
            }
        }

        private void UpdateMetadata()
        {
            LastModifiedTextBlock.Text = _faqData.LastModified?.ToString() ?? "N/A";
            ModifiedByTextBlock.Text = _faqData.ModifiedBy ?? string.Empty;
        }

        private void FaqListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // This can be used if you need to handle selection change logic
        }
    }
}