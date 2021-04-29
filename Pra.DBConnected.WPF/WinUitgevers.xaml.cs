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

namespace Pra.DBConnected.WPF
{
    /// <summary>
    /// Interaction logic for WinUitgevers.xaml
    /// </summary>
    public partial class WinUitgevers : Window
    {
        bool nieuweUitgever;

        public WinUitgevers()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            VulDeUitgevers();
            ViewStandaard();
        }
        private void VulDeUitgevers()
        {
            lstUitgevers.Items.Clear();
            DataTable dtUitgevers = Uitgever.GeefAlleUitgevers();
            ListBoxItem itm;
            for (int r = 0; r < dtUitgevers.Rows.Count; r++)
            {
                itm = new ListBoxItem();
                itm.Content = dtUitgevers.Rows[r]["uitgever"].ToString();
                itm.Tag = dtUitgevers.Rows[r]["uitg_id"].ToString();
                lstUitgevers.Items.Add(itm);
            }
        }
        private void ViewStandaard()
        {
            grpKnoppen.IsEnabled = true;
            grpBewerken.IsEnabled = false;
            lstUitgevers.IsEnabled = true;
            grpBewerken.Header = "";
        }
        private void ViewBewerking()
        {
            grpKnoppen.IsEnabled = false;
            grpBewerken.IsEnabled = true;
            lstUitgevers.IsEnabled = false;
        }
        private void LstUitgevers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstUitgevers.SelectedIndex >= 0)
            {
                ListBoxItem itm = (ListBoxItem)lstUitgevers.SelectedItem;
                int uitg_id = int.Parse(itm.Tag.ToString());
                txtUitgever.Text = Uitgever.ZoekUitgever(uitg_id);
            }
        }

        private void BtnNieuw_Click(object sender, RoutedEventArgs e)
        {
            grpBewerken.Header = "Een nieuwe uitgever";
            nieuweUitgever = true;
            ViewBewerking();
            txtUitgever.Text = "";
            txtUitgever.Focus();
        }

        private void BtnWijzig_Click(object sender, RoutedEventArgs e)
        {
            if (lstUitgevers.SelectedIndex >= 0)
            {
                grpBewerken.Header = "Een uitgever wijzigen";
                nieuweUitgever = false;
                ViewBewerking();
                txtUitgever.Focus();
            }
        }
        private void BtnAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            LstUitgevers_SelectionChanged(lstUitgevers, null);
            ViewStandaard();
        }
        private void BtnBewaren_Click(object sender, RoutedEventArgs e)
        {
            if (nieuweUitgever)
            {
                if (Uitgever.VoegUitgeverToe(txtUitgever.Text))
                {
                    VulDeUitgevers();
                }
                else
                {
                    MessageBox.Show("Er heeft zich een fout voorgedaan", "Uitgever niet toegevoegd");
                }
            }
            else
            {
                ListBoxItem itm = (ListBoxItem)lstUitgevers.SelectedItem;
                int uitg_id = int.Parse(itm.Tag.ToString());
                if (Uitgever.WijzigUitgever(uitg_id, txtUitgever.Text))
                {
                    VulDeUitgevers();
                }
                else
                {
                    MessageBox.Show("Er heeft zich een fout voorgedaan", "Uitgever niet gewijzigd");
                }
            }
            ViewStandaard();
        }
        private void BtnVerwijder_Click(object sender, RoutedEventArgs e)
        {
            if (lstUitgevers.SelectedIndex >= 0)
            {
                if (MessageBox.Show("Zeker?", "Uitgever wissen", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    ListBoxItem itm = (ListBoxItem)lstUitgevers.SelectedItem;
                    int uitg_id = int.Parse(itm.Tag.ToString());
                    if (Uitgever.VerwijderUitgever(uitg_id))
                    {
                        VulDeUitgevers();
                    }
                    else
                    {
                        MessageBox.Show("Er heeft zich een fout voorgedaan", "Uitgever werd niet verwijderd");
                    }
                }
            }
        }
    }
}
