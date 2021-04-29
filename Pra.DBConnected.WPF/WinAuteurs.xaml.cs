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
    /// Interaction logic for WinAuteurs.xaml
    /// </summary>
    public partial class WinAuteurs : Window
    {
        bool nieuweAuteur;

        public WinAuteurs()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            VulDeAuteurs();
            ViewStandaard();
        }
        private void VulDeAuteurs()
        {
            VulDeAuteurs(Enumeraties.SortOrder.ASC);
        }
        private void VulDeAuteurs(Enumeraties.SortOrder volgorde)
        {
            lstAuteurs.Items.Clear();
            DataTable dtAuteurs = Auteur.GeefAlleAuteurs("naam", volgorde);
            ListBoxItem itm;
            for (int r = 0; r < dtAuteurs.Rows.Count; r++)
            {
                itm = new ListBoxItem();
                itm.Content = dtAuteurs.Rows[r]["naam"].ToString();
                itm.Tag = dtAuteurs.Rows[r]["auteur_id"].ToString();
                lstAuteurs.Items.Add(itm);
            }
        }

        private void ViewStandaard()
        {
            grpKnoppen.IsEnabled = true;
            grpBewerken.IsEnabled = false;
            lstAuteurs.IsEnabled = true;
            grpBewerken.Header = "";
        }
        private void ViewBewerking()
        {
            grpKnoppen.IsEnabled = false;
            grpBewerken.IsEnabled = true;
            lstAuteurs.IsEnabled = false;
        }

        private void LstAuteurs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstAuteurs.SelectedIndex >= 0)
            {
                ListBoxItem itm = (ListBoxItem)lstAuteurs.SelectedItem;
                int auteur_id = int.Parse(itm.Tag.ToString());
                txtNaam.Text = Auteur.ZoekNaam(auteur_id);
            }
        }



        private void BtnNieuw_Click(object sender, RoutedEventArgs e)
        {
            grpBewerken.Header = "Een nieuwe auteur";
            nieuweAuteur = true;
            ViewBewerking();
            txtNaam.Text = "";
            txtNaam.Focus();
        }

        private void BtnWijzig_Click(object sender, RoutedEventArgs e)
        {
            if (lstAuteurs.SelectedIndex >= 0)
            {
                grpBewerken.Header = "Een auteur wijzigen";
                nieuweAuteur = false;
                ViewBewerking();
                txtNaam.Focus();
            }
        }
        private void BtnAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            LstAuteurs_SelectionChanged(lstAuteurs, null);
            ViewStandaard();
        }
        private void BtnBewaren_Click(object sender, RoutedEventArgs e)
        {
            if (nieuweAuteur)
            {
                if (Auteur.VoegAuteurToe(txtNaam.Text))
                {
                    VulDeAuteurs();
                }
                else
                {
                    MessageBox.Show("Er heeft zich een fout voorgedaan", "Auteur niet toegevoegd");
                }
            }
            else
            {
                ListBoxItem itm = (ListBoxItem)lstAuteurs.SelectedItem;
                int auteur_id = int.Parse(itm.Tag.ToString());
                if (Auteur.WijzigAuteur(auteur_id, txtNaam.Text))
                {
                    VulDeAuteurs();
                }
                else
                {
                    MessageBox.Show("Er heeft zich een fout voorgedaan", "Auteur niet gewijzigd");
                }
            }
            ViewStandaard();
        }
        private void BtnVerwijder_Click(object sender, RoutedEventArgs e)
        {
            if (lstAuteurs.SelectedIndex >= 0)
            {
                if (MessageBox.Show("Zeker?", "Auteur wissen", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    ListBoxItem itm = (ListBoxItem)lstAuteurs.SelectedItem;
                    int auteur_id = int.Parse(itm.Tag.ToString());
                    if (Auteur.VerwijderAuteur(auteur_id))
                    {
                        VulDeAuteurs();
                    }
                    else
                    {
                        MessageBox.Show("Er heeft zich een fout voorgedaan", "Auteur werd niet verwijderd");
                    }
                }
            }
        }


    }
}
