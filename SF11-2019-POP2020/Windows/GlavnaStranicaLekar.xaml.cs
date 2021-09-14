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
    /// Interaction logic for GlavnaStranicaLekar.xaml
    /// </summary>
    public partial class GlavnaStranicaLekar : Window
    {
        
        public GlavnaStranicaLekar()
        {

           

            InitializeComponent();

        }

        private void MenuItemLicniPodaci_Click(object sender, RoutedEventArgs e)
        {
            string jmbg = textBlock1.Text;

            Korisnik ulogovaniKorisnik = Util.Instance.nadjiUlogovanog(jmbg);
            IzmenaLicnihPodataka add = new IzmenaLicnihPodataka(ulogovaniKorisnik);
            add.ShowDialog();
        }

        private void MenuItemPacijenti_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemLekari_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemLogOut_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow window = new HomeWindow();

            this.Hide();
            window.Show();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }


        private void btnTerapije_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDomovi_Click(object sender, RoutedEventArgs e)
        {

        }

     

        private void btnPojedinacniTermini_Click(object sender, RoutedEventArgs e)
        {
            string jmbg = textBlock1.Text.Trim();
            Console.WriteLine(jmbg);
            ObservableCollection<Termin> termini = Util.Instance.terminiByLekarJmbg(jmbg);
            TerminiPojedinacnogLekara tpl = new TerminiPojedinacnogLekara(termini);
            tpl.textBlock1.Text = jmbg;
            tpl.Show();
        }
    }
}
