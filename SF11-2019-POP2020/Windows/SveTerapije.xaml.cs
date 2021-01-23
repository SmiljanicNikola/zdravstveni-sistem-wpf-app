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
    /// Interaction logic for SveTerapije.xaml
    /// </summary>
    public partial class SveTerapije : Window
    {

        ICollectionView view;

        public SveTerapije()
        {
            InitializeComponent();

            UpdateView();
        }


        private void UpdateView()
        {
            view = CollectionViewSource.GetDefaultView(Util.Instance.Terapije);
            DataGridTerapije.ItemsSource = view;
            DataGridTerapije.IsSynchronizedWithCurrentItem = true;
            DataGridTerapije.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void DataGridTerapije_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan") || e.PropertyName.Equals("Error"))
                e.Column.Visibility = Visibility.Collapsed;
        }

        private void MenuItemObrisiTerapiju_Click(object sender, RoutedEventArgs e)
        {
            Terapija izabranaTerapija = view.CurrentItem as Terapija;
            Util.Instance.DeleteTerapija(izabranaTerapija.Id);

            view.Refresh();
        }

        private void MenuItemIzmeniTerapiju_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemDodajTerapiju_Click(object sender, RoutedEventArgs e)
        {
            Terapija novaTerapija = new Terapija();
            DodavanjeIzmenaTerapija addTerapija = new DodavanjeIzmenaTerapija(novaTerapija, EStatus.Dodaj);
            addTerapija.Show();

            this.Hide();
            //if ((bool)addAdmin.ShowDialog())
            //{

            //}
            this.Show();
            view.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow window = new HomeWindow();

            this.Hide();
            window.Show();
        }
    }
}