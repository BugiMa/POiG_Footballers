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

namespace lab_1C
{
    public partial class MainWindow : Window
    {
        private string ArchivingFile = "archive.txt";

        public MainWindow()
        {
            TextBoxWithErrorProvider.BrushForAll = Brushes.Red;
            InitializeComponent();
            textBoxWEP_Name.SetFocus();
        }

        private bool IsNotEmpty(TextBoxWithErrorProvider tb)
        {
            if (tb.Text.Trim() == "")
            {
                tb.SetError("Pole nie może być puste!");
                return false;
            }
            tb.SetError("");
            return true;
        }

        private void Clear()
        {
            textBoxWEP_Name.Text = "";
            textBoxWEP_Surname.Text = "";
            sliderAge.Value = 35;
            sliderWeight.Value = 80;
            buttonEdit.IsEnabled = false;
            buttonDelete.IsEnabled = false;
            listBoxFootballer.SelectedIndex = -1;
            textBoxWEP_Name.SetFocus();
        }

        private void LoadPlayer(Footballer fballer)
        {
            textBoxWEP_Name.Text = fballer.Name;
            textBoxWEP_Surname.Text = fballer.Surname;
            sliderAge.Value = fballer.Age;
            sliderWeight.Value = fballer.Weight;
            buttonEdit.IsEnabled = true;
            buttonDelete.IsEnabled = true;
            textBoxWEP_Name.SetFocus();
        }

        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (IsNotEmpty(textBoxWEP_Name) & IsNotEmpty(textBoxWEP_Surname))
            {
                var currentFootballer = new Footballer(textBoxWEP_Name.Text.Trim(), textBoxWEP_Surname.Text.Trim(), (uint)sliderAge.Value, (uint)sliderWeight.Value);
                var onList = false;
                foreach (var p in listBoxFootballer.Items)
                {
                    var fballer = p as Footballer;
                    if (fballer.isTheSame(currentFootballer))
                    {
                        onList = true;
                        break;
                    }
                }
                if (!onList)
                {
                    var dialogResult=MessageBox.Show($"Czy na pewno chcesz zmienić dane  {Environment.NewLine} {listBoxFootballer.SelectedItem}?", "Edycja", MessageBoxButton.YesNo);

                    if (dialogResult == MessageBoxResult.Yes)
                    {        
                        var selectedFootballer = (Footballer)listBoxFootballer.SelectedItem;
                        selectedFootballer.Name = currentFootballer.Name;
                        selectedFootballer.Surname = currentFootballer.Surname;
                        selectedFootballer.Age = currentFootballer.Age;
                        selectedFootballer.Weight = currentFootballer.Weight;
                        listBoxFootballer.Items.Refresh();
                    }
                    Clear();
                    listBoxFootballer.SelectedIndex = -1;
                }
                else
                    MessageBox.Show($"{currentFootballer.ToString()} już jest na liście.", "Uwaga");
            }
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (IsNotEmpty(textBoxWEP_Name) & IsNotEmpty(textBoxWEP_Surname))
            {
                var currentFootballer = new Footballer(textBoxWEP_Name.Text.Trim(), textBoxWEP_Surname.Text.Trim(), (uint)sliderAge.Value, (uint)sliderWeight.Value);
                var onList = false;
                foreach (var p in listBoxFootballer.Items)
                {
                    var fballer = p as Footballer;
                    if (fballer.isTheSame(currentFootballer))
                    {
                        onList = true;
                        break;
                    }
                }
                if (!onList)
                {
                    listBoxFootballer.Items.Add(currentFootballer);
                    Clear();
                }
                else
                {
                    var dialog = MessageBox.Show($"{currentFootballer.ToString()} już jest na liście {Environment.NewLine} Czy wyczyścić formularz?", "Uwaga", MessageBoxButton.OKCancel);
                    if (dialog == MessageBoxResult.OK)
                    {
                        Clear();
                    }
                }
            }
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            var currentFootballer = new Footballer(textBoxWEP_Name.Text.Trim(), textBoxWEP_Surname.Text.Trim(), (uint)sliderAge.Value, (uint)sliderWeight.Value);
            var dialog = MessageBox.Show($"Czy na pewno chcesz usunąć {currentFootballer.ToString()} z listy?", "Uwaga", MessageBoxButton.OKCancel);
            if (dialog == MessageBoxResult.OK)
            {
                listBoxFootballer.Items.Remove(listBoxFootballer.SelectedItem);
                Clear();
            } 
        }

        private void listBoxFootballer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxFootballer.SelectedIndex > -1)
            {
                LoadPlayer((Footballer)listBoxFootballer.SelectedItem);
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            int n = listBoxFootballer.Items.Count;
            Footballer[] fballer = null;
            if (n > 0)
            {
                fballer = new Footballer[n];
                int index = 0;
                foreach (var o in listBoxFootballer.Items)
                {
                    fballer[index++] = o as Footballer;
                }
                Archiving.SaveFootballerToFile(ArchivingFile,fballer);
            } 
        } 

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var fballer=Archiving.ReadFootballerFromFile(ArchivingFile);
            if(fballer!=null)
            foreach (var f in fballer)
            {
                listBoxFootballer.Items.Add(f);
            }
        }
    }
}
