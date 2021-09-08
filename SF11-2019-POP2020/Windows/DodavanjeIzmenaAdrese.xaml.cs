using SF11_2019_POP2020.Models;
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
    /// Interaction logic for DodavanjeIzmenaAdrese.xaml
    /// </summary>
    public partial class DodavanjeIzmenaAdrese : Window
    {
        private EStatus odabranStatus;
        private Adresa odabranaAdresa;

        public DodavanjeIzmenaAdrese(Adresa adresa, EStatus status = EStatus.Dodaj)
        {
            InitializeComponent();

            this.DataContext = adresa;

            odabranaAdresa = adresa;
            odabranStatus = status;


            if (status.Equals(EStatus.Izmeni) && adresa != null)
            {
                this.Title = "Izmeni adresu";
                txtUlica.Text = odabranaAdresa.Ulica;
                txtBroj.Text = odabranaAdresa.Broj.ToString();
                txtGrad.Text = odabranaAdresa.Grad;
                txtDrzava.Text = odabranaAdresa.Drzava;
            }

        }



        /*
        public void Napuni()
        {


        }*/


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (odabranStatus.Equals(EStatus.Dodaj))
            {
                Adresa adresa = new Adresa()
                {
                    Ulica = txtUlica.Text,
                    Broj = txtBroj.Text,
                    Grad = txtGrad.Text,
                    Drzava = txtDrzava.Text,
                    Aktivan = true

                };
                Util.Instance.Adrese.Add(adresa);          
                Util.Instance.SacuvajEntitet(adresa);

            }
            else if(odabranStatus.Equals(EStatus.Izmeni))
             {
                /*Adresa adresa = new Adresa()
                {
                    Ulica = txtUlica.Text,
                    Broj = txtBroj.Text,
                    Grad = txtGrad.Text,
                    Drzava = txtDrzava.Text,
                    Aktivan = true

                };*/
                Util.Instance.UpdateEntiteta(odabranaAdresa);
             }

            this.Close();
            //this.DialogResult = true;

        }
    }
}
