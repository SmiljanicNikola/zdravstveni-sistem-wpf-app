using SF11_2019_POP2020.Models;
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
    /// Interaction logic for SveAdrese.xaml
    /// </summary>
    public partial class SveAdrese : Window
    {
        ICollectionView view;
        public SveAdrese()
        {
            InitializeComponent();

            UpdateView();
        }


        private void UpdateView()
        {
            view = CollectionViewSource.GetDefaultView(Util.Instance.Adrese);
            DataGridAdrese.ItemsSource = view;
            DataGridAdrese.IsSynchronizedWithCurrentItem = true;
            DataGridAdrese.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void MenuItemDodajAdresu_Click(object sender, RoutedEventArgs e)
        {
            Adresa novaAdresa = new Adresa();
            DodavanjeIzmenaAdrese addAdresa = new DodavanjeIzmenaAdrese(novaAdresa);
            addAdresa.Show();

            this.Hide();
            //if ((bool)addAdmin.ShowDialog())
            //{

            //}
            this.Show();
            view.Refresh();
        }

        private void MenuItemIzmeniAdresu_Click(object sender, RoutedEventArgs e)
        {
            /* Adresa izabranaAdresa = view.CurrentItem as Adresa;
             Adresa staraAdresa = izabranaAdresa.Clone();
             DodavanjeIzmenaAdrese addadrese = new DodavanjeIzmenaAdrese(izabranaAdresa, EStatus.Izmeni);

             this.Hide();
             if ((bool)addadrese.ShowDialog())
             {
                 int index = Util.Instance.Adrese.ToList().FindIndex(a => a.Id.Equals(izabranaAdresa.Id));

                 //Util.Instance.Korisnici[index] = stariLekar;
                 Util.Instance.Adrese[index].Ulica = izabranaAdresa.Ulica;
                 Util.Instance.Adrese[index].Broj = izabranaAdresa.Broj;
                 Util.Instance.Adrese[index].Drzava = izabranaAdresa.Drzava;
                 Util.Instance.Adrese[index].Grad = izabranaAdresa.Grad;
                 Util.Instance.Adrese[index].Aktivan = izabranaAdresa.Aktivan;
             }
             Util.Instance.SacuvajEntitet(izabranaAdresa);

             Util.Instance.DeleteAdresa(izabranaAdresa.Id);


             this.Show();
             view.Refresh();*/

            Adresa izabranaAdresa = view.CurrentItem as Adresa;

            if(izabranaAdresa != null)
            {
                Adresa staraAdresa = izabranaAdresa.Clone();
                DodavanjeIzmenaAdrese addadrese = new DodavanjeIzmenaAdrese(izabranaAdresa, EStatus.Izmeni);
                //this.Hide();
                if ((bool)addadrese.ShowDialog())
                {
                    /*int index = Util.Instance.Adrese.ToList().FindIndex(a => a.Id.Equals(izabranaAdresa.Id));

                    //Util.Instance.Korisnici[index] = stariLekar;
                    Util.Instance.Adrese[index].Ulica = izabranaAdresa.Ulica;
                    Util.Instance.Adrese[index].Broj = izabranaAdresa.Broj;
                    Util.Instance.Adrese[index].Drzava = izabranaAdresa.Drzava;
                    Util.Instance.Adrese[index].Grad = izabranaAdresa.Grad;
                    Util.Instance.Adrese[index].Aktivan = izabranaAdresa.Aktivan;*/
                }

                UpdateView();

            }




        }

        private void MenuItemObrisiAdresu_Click(object sender, RoutedEventArgs e)
        {
            Adresa izabranaAdresa = view.CurrentItem as Adresa;
            Util.Instance.DeleteAdresa(izabranaAdresa.Id);

            Util.Instance.Adrese.Remove(izabranaAdresa);
            UpdateView();

            view.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GlavnaStranicaAdministrator gsa = new GlavnaStranicaAdministrator();
            this.Hide();
            gsa.Show();
        }

        private void DataGridAdrese_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan") || e.PropertyName.Equals("Error"))
                e.Column.Visibility = Visibility.Collapsed;
        }

        private void DataGridAdrese_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
