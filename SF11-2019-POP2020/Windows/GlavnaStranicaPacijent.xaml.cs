using SF11_2019_POP2020.Models;
using System;
using System.Collections;
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
    /// Interaction logic for GlavnaStranicaPacijent.xaml
    /// </summary>
    public partial class GlavnaStranicaPacijent : Window
    {
        ICollection view;
        public GlavnaStranicaPacijent()
        {
            InitializeComponent();
        }

        private void MenuItemLekari_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemPacijenti_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemAdministratori_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemLogOut_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow window = new HomeWindow();

            this.Hide();
            window.Show();
        }

        private void MenuItemLekari_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void PregledTermina_Click(object sender, RoutedEventArgs e)
        {
            PrikazTerminaBezCRUD addPrikaz = new PrikazTerminaBezCRUD();
            addPrikaz.Show();
            this.Hide();
            this.Show();
        }

        private void PregledDomova_Click(object sender, RoutedEventArgs e)
        {
            PrikazDomovaBezCRUD addPrikaz = new PrikazDomovaBezCRUD();
            addPrikaz.Show();
            this.Hide();
            this.Show();
        }

        private void ZakazivanjeTermina_Click(object sender, RoutedEventArgs e)
        {
            Termin noviTermin = new Termin();
            DodavanjeIzmenaTermina addTermin = new DodavanjeIzmenaTermina(noviTermin, EStatus.Dodaj);
            addTermin.Show();

            this.Hide();
            //if ((bool)addAdmin.ShowDialog())
            //{

            //}
            this.Show();
            
        }
    }
}
