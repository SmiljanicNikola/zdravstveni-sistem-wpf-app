﻿using SF11_2019_POP2020.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for SviAdministratori.xaml
    /// </summary>
    public partial class SviAdministratori : Window
    {
        ICollectionView view;
        public SviAdministratori()
        {
            InitializeComponent();

            Updateview();

            view.Filter = CustomFilter;
        }

        private bool CustomFilter(object obj)
        {
            Korisnik korisnik = obj as Korisnik;
            if (korisnik.TipKorisnika.Equals(ETipKorisnika.ADMINISTRATOR))
                return true;
           return false;
        }

        private void Updateview()
        {
            view = CollectionViewSource.GetDefaultView(Util.Instance.Korisnici);
            DataGridAdmini.ItemsSource = view;
            DataGridAdmini.IsSynchronizedWithCurrentItem = true;
        }

        private void DataGridAdmini_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Error"))
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }

        private void MenuItemDodajAdmina_Click(object sender, RoutedEventArgs e)
        {
            Korisnik noviKorisnik = new Korisnik();
            DodavanjeIzmenaAdministratora addAdmin = new DodavanjeIzmenaAdministratora(null);
            addAdmin.Show();

            this.Hide();
            //if ((bool)addAdmin.ShowDialog())
            //{

            //}
            this.Show();
            view.Refresh();
        }

        private void MenuItemIzmeniAdmina_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemObrisiAdmina_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}