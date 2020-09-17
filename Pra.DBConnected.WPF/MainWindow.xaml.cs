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

namespace Pra.DBConnected.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void mnuStop_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void mnuAuteur_Click(object sender, RoutedEventArgs e)
        {
            WinAuteurs kind = new WinAuteurs();
            kind.ShowDialog();
        }

        private void mnuCategorie_Click(object sender, RoutedEventArgs e)
        {
            WinCategorie venster = new WinCategorie();
            venster.ShowDialog();
        }

        private void mnuUitgever_Click(object sender, RoutedEventArgs e)
        {
            WinUitgevers venster = new WinUitgevers();
            venster.ShowDialog();
        }

        private void mnuKlant_Click(object sender, RoutedEventArgs e)
        {
            WinKlanten venster = new WinKlanten();
            venster.ShowDialog();
        }
    }
}
