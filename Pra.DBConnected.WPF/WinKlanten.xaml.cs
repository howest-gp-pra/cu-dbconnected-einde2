using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using Pra.DBConnected.CORE;
using Pra.DBConnected.CORE.Entities;
using Pra.DBConnected.CORE.Services;

namespace Pra.DBConnected.WPF
{
    /// <summary>
    /// Interaction logic for WinKlanten.xaml
    /// </summary>
    public partial class WinKlanten : Window
    {
        public WinKlanten()
        {
            InitializeComponent();
        }
        bool nieuweKlant;
        KlantService klantService;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            klantService = new KlantService();
            lstKlanten.ItemsSource = klantService.Klanten;
            ViewStandaard();
        }
        private void ViewStandaard()
        {
            grpKnoppen.IsEnabled = true;
            grpBewerken.IsEnabled = false;
            lstKlanten.IsEnabled = true;
            grpBewerken.Header = "";
        }
        private void ViewBewerking()
        {
            grpKnoppen.IsEnabled = false;
            grpBewerken.IsEnabled = true;
            lstKlanten.IsEnabled = false;
        }

        private void lstKlanten_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtKlantnaam.Text = "";
            txtPlaats.Text = "";
            if(lstKlanten.SelectedItem != null)
            {
                Klant klant = (Klant)lstKlanten.SelectedItem;
                txtKlantnaam.Text = klant.KlantNaam;
                txtPlaats.Text = klant.Plaats;
            }
        }

        private void btnNieuw_Click(object sender, RoutedEventArgs e)
        {
            grpBewerken.Header = "Een nieuwe klant";
            nieuweKlant = true;
            ViewBewerking();
            txtKlantnaam.Text = "";
            txtPlaats.Text = "";
            txtKlantnaam.Focus();
        }

        private void btnWijzig_Click(object sender, RoutedEventArgs e)
        {
            if (lstKlanten.SelectedIndex >= 0)
            {
                grpBewerken.Header = "Een klant wijzigen";
                nieuweKlant = false;
                ViewBewerking();
                txtKlantnaam.Focus();
            }
        }

        private void btnAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            lstKlanten_SelectionChanged(lstKlanten, null);
            ViewStandaard();
        }

        private void btnBewaren_Click(object sender, RoutedEventArgs e)
        {
            if(nieuweKlant)
            {
                Klant klant;
                try
                {
                    klant = new Klant(-1, txtKlantnaam.Text, txtPlaats.Text);
                }
                catch(Exception fout)
                {
                    MessageBox.Show(fout.Message);
                    return;
                }
                if (klantService.AddKlant(klant))
                {
                    lstKlanten.ItemsSource = null;
                    lstKlanten.ItemsSource = klantService.Klanten;
                    lstKlanten.SelectedItem = klant;
                }
                else
                {
                    MessageBox.Show("Klant werd niet toegevoegd", "Error");
                    return;
                }
            }
            else
            {
                Klant klant = (Klant)lstKlanten.SelectedItem;
                try
                {
                    klant.KlantNaam = txtKlantnaam.Text;
                    klant.Plaats = txtPlaats.Text;
                }
                catch (Exception fout)
                {
                    MessageBox.Show(fout.Message);
                    return;
                }
                if (klantService.EditKlant(klant))
                {
                    lstKlanten.ItemsSource = null;
                    lstKlanten.ItemsSource = klantService.Klanten;
                    lstKlanten.SelectedItem = klant;
                }
                else
                {
                    MessageBox.Show("Klant werd niet gewijzigd", "Error");
                    return;
                }
            }
            ViewStandaard();

        }

        private void btnVerwijder_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Zeker?","Klant wissen", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                Klant klant = (Klant)lstKlanten.SelectedItem;
                if (klantService.DeleteKlant(klant))
                {
                    lstKlanten.ItemsSource = null;
                    lstKlanten.ItemsSource = klantService.Klanten;
                    txtKlantnaam.Text = "";
                    txtPlaats.Text = "";
                }
                else
                {
                    MessageBox.Show("Klant werd niet verwijderd", "Error");
                    return;
                }
            }
        }
    }
}
