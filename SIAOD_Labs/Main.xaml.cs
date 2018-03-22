using System;
using System.Windows;
using System.Windows.Controls;

namespace SIAOD_Labs
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(comboBox.SelectedIndex!=-1)
                frame.NavigationService.Navigate(new Uri("Lab" + (comboBox.SelectedIndex + 1) + ".xaml", UriKind.Relative));
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(null);
            frame.Content = "Нет открытых работ";
            comboBox.SelectedIndex = -1;
        }
    }
}
