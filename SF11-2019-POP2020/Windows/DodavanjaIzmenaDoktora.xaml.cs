﻿using SF11_2019_POP2020.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for DodavanjaIzmenaDoktora.xaml
    /// </summary>
    public partial class DodavanjaIzmenaDoktora : Window
    {
        private EStatus odabranStatus;
        private Lekar odabranDoktor;
        private DomZdravlja selektovanDomZdravljaComboBox;
        ObservableCollection<DomZdravlja> domoviZdravlja;



        public DodavanjaIzmenaDoktora(Lekar lekar, EStatus status = EStatus.Dodaj)
        {
            InitializeComponent();

            this.DataContext = lekar;

            odabranDoktor = lekar;
            odabranStatus = status;

            this.domoviZdravlja = Util.Instance.DomoviZdravlja;
            cmbDomoviZdravlja.ItemsSource = this.domoviZdravlja;

            //ComboBoxTipKorisnika.ItemsSource = Enum.GetValues(typeof(ETipKorisnika)).Cast<ETipKorisnika>();


            if (status.Equals(EStatus.Izmeni) && lekar != null)
            {
                this.Title = "Izmeni doktora";
                /*lekar.Ime = txtIme.Text;
                lekar.Prezime = txtPrezime.Text;
                txtJmbg.IsEnabled = false;
                lekar.Email = txtEmail.Text;
                txtAdresaId.IsEnabled = false;               
                lekar.Lozinka = txtLozinka.Text;
                Util.Instance.SacuvajEntitet(lekar);*/                //Util.Instance.SacuvajEntitet(lekar);
                //Util.Instance.UpdateEntiteta(lekar);
                //Util.Instance.SacuvajEntitet(lekar);
                lekar.DomZdravlja = cmbDomoviZdravlja.SelectedItem as DomZdravlja;


            }
            else
            {
                this.Title = "Dodaj doktora";
            }


        }

        

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            odabranDoktor.DomZdravlja = cmbDomoviZdravlja.SelectedItem as DomZdravlja;
            odabranDoktor.Aktivan = true;

            /*if (odabranStatus.Equals(EStatus.Dodaj))
            {
                Lekar l = new Lekar()
                {
                    Korisnik = Util.Instance.korisnikPoId(int.Parse(txtKorisnikId.Text)),
                    DomZdravlja = Util.Instance.domZdravljaPoId(int.Parse(txtDomZdravljaId.Text)),
                    Termini = "|",
                    Aktivan = true

                };
                Util.Instance.Lekari.Add(l);
                Util.Instance.SacuvajEntitet(l);


            }*/
            if (odabranStatus.Equals(EStatus.Izmeni))
                {
                    /*Adresa adresa = new Adresa()
                    {
                        Ulica = txtUlica.Text,
                        Broj = txtBroj.Text,
                        Grad = txtGrad.Text,
                        Drzava = txtDrzava.Text,
                        Aktivan = true

                    };*/
                    Util.Instance.UpdateEntiteta(odabranDoktor);
                }
                this.Close();
            }
        

        private void cmbDomoviZdravlja_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.selektovanDomZdravljaComboBox = cmbDomoviZdravlja.SelectedItem as DomZdravlja;

        }
    }
    }