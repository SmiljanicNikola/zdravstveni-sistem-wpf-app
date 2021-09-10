﻿using SF11_2019_POP2020.Models;
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

namespace SF11_2019_POP2020.Windows
{
    /// <summary>
    /// Interaction logic for DodavanjeIzmenaAdministratora.xaml
    /// </summary>
    public partial class DodavanjeIzmenaKorisnika : Window
    {
        private EStatus odabranStatus;
        private Korisnik odabranKorisnik;
        public DodavanjeIzmenaKorisnika(Korisnik korisnik, EStatus status = EStatus.Dodaj)
        {
            InitializeComponent();

            this.DataContext = korisnik;

            odabranKorisnik = korisnik;
            odabranStatus = status;
            ComboBoxTipKorisnika.ItemsSource = Enum.GetValues(typeof(ETipKorisnika)).Cast<ETipKorisnika>();

            ComboBoxPol.ItemsSource = Enum.GetValues(typeof(EPol)).Cast<EPol>();

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (odabranStatus.Equals(EStatus.Dodaj))
            {
                //ComboBoxItem item = (ComboBoxItem)ComboBoxPol.SelectedItem;
                //string value = item.Content.ToString();
                //.TryParse(value, out ETipKorisnika tip);
                //ComboBoxTipKorisnika.ItemsSource = Enum.GetValues(typeof(ETipKorisnika)).Cast<ETipKorisnika>();

               // ComboBoxItem item1 = (ComboBoxItem)ComboBoxPol.SelectedItem;
               // string value1 = item1.Content.ToString();
               // Enum.TryParse(value1, out EPol pol);
                //odabranKorisnik.Aktivan = true;
                Korisnik k = new Korisnik
                {
                    Ime = txtIme.Text,
                    Prezime = txtPrezime.Text,
                    Jmbg = txtJMBG.Text,
                    Email = txtEmail.Text,
                    AdresaId = int.Parse(txtAdresaId.Text),
                    Pol = (EPol)Enum.Parse(typeof(EPol), ComboBoxPol.SelectedItem.ToString()),
                    Lozinka = txtLozinka.Text,
                    TipKorisnika = (ETipKorisnika)Enum.Parse(typeof(ETipKorisnika), ComboBoxTipKorisnika.SelectedItem.ToString()),
                    Aktivan = true

                };
                Util.Instance.Korisnici.Add(k);
                //Util.Instance.Lekari.Add(lekar);
                Util.Instance.SacuvajEntitet(k);

            }
                 else if (odabranStatus.Equals(EStatus.Izmeni))
                {
                    /*Adresa adresa = new Adresa()
                    {
                        Ulica = txtUlica.Text,
                        Broj = txtBroj.Text,
                        Grad = txtGrad.Text,
                        Drzava = txtDrzava.Text,
                        Aktivan = true

                    };*/
                    Util.Instance.UpdateEntiteta(odabranKorisnik);
                }
            this.Close();
            }


                    /* Util.Instance.SacuvajEntitet(obj);*/
            //Util.Instance.SacuvajEntite("lekari.txt");


            //this.DialogResult = false;
           
       
    }
}