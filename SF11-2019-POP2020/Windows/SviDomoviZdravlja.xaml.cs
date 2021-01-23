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
    /// Interaction logic for SviDomoviZdravlja.xaml
    /// </summary>
    public partial class SviDomoviZdravlja : Window
    {
        ICollectionView view;
        public SviDomoviZdravlja()
        {
            InitializeComponent();

            UpdateView();

            view.Filter = CustomFilter;
        }

        private bool CustomFilter(object obj)
        {
            DomZdravlja domZdravlja = obj as DomZdravlja;

            if (domZdravlja.Aktivan)
                if (txtPretragaPoNazivuInstitucije.Text != "")
                {
                    return domZdravlja.NazivInstitucije.Contains(txtPretragaPoNazivuInstitucije.Text);
                }
            if(domZdravlja.Aktivan)
                if(txtPretragaPoAdresaId.Text != "")
                {
                    return domZdravlja.adresaId.Equals(txtPretragaPoAdresaId.Text);
                }
                else
                    return true;


            return false;
        }

        private void UpdateView()
        {
            view = CollectionViewSource.GetDefaultView(Util.Instance.DomoviZdravlja);
            DataGridDomoviZdravlja.ItemsSource = view;
            DataGridDomoviZdravlja.IsSynchronizedWithCurrentItem = true;
            DataGridDomoviZdravlja.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }

        private void MenuItemDodajDomZdravlja_Click(object sender, RoutedEventArgs e)
        {
            DomZdravlja noviDomZdravlja = new DomZdravlja();
            DodavanjeIzmenaDomovaZdravlja addDomZdravlja= new DodavanjeIzmenaDomovaZdravlja(noviDomZdravlja, EStatus.Dodaj);
            addDomZdravlja.Show();

            this.Hide();
            //if ((bool)addAdmin.ShowDialog())
            //{

            //}
            this.Show();
            view.Refresh();
        }

        private void MenuItemIzmeniDomZdravlja_Click(object sender, RoutedEventArgs e)
        {
            //Korisnik izabraniLekar = (Korisnik)DataGridLekari.SelectedItem;
            DomZdravlja izabraniDom = view.CurrentItem as DomZdravlja;
            DomZdravlja stariDom = izabraniDom.Clone();
            DodavanjeIzmenaDomovaZdravlja add = new DodavanjeIzmenaDomovaZdravlja(izabraniDom, EStatus.Izmeni);

            this.Hide();
            if ((bool)add.ShowDialog())
            {
                int index = Util.Instance.DomoviZdravlja.ToList().FindIndex(dt => dt.Id.Equals(izabraniDom.Id));

                //Util.Instance.Korisnici[index] = stariLekar;
                Util.Instance.DomoviZdravlja[index].NazivInstitucije = izabraniDom.NazivInstitucije;
                Util.Instance.DomoviZdravlja[index].adresaId = izabraniDom.adresaId;   
                Util.Instance.DomoviZdravlja[index].Aktivan = izabraniDom.Aktivan;

                //Util.Instance.UpdateEntiteta(izabraniLekar);
                Util.Instance.DeleteDomZdravlja(stariDom.Id);
                Util.Instance.SacuvajEntitet(izabraniDom);



            }
            this.Show();
            view.Refresh();
        }

        private void MenuItemObrisiDomZdravlja_Click(object sender, RoutedEventArgs e)
        {

            DomZdravlja izabraniDom = view.CurrentItem as DomZdravlja;
            Util.Instance.DeleteDomZdravlja(izabraniDom.Id);
        }

        private void DataGridDomoviZdravlja_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Aktivan") || e.PropertyName.Equals("Error"))
                e.Column.Visibility = Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow window = new HomeWindow();

            this.Hide();
            window.Show();
        }

        private void txtPretragaPoNazivuInstitucije_KeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();

        }

        private void txtPretragaPoAdresaId_KeyUp(object sender, KeyEventArgs e)
        {
            view.Refresh();
        }
    }
}
