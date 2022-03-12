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
    /// Interaction logic for DodavanjeIzmenaDomovaZdravlja.xaml
    /// </summary>
    public partial class DodavanjeIzmenaDomovaZdravlja : Window
    {
        private EStatus odabranStatus;
        private DomZdravlja odabranDomZdravlja;
        private Adresa novaAdresa;
        public DodavanjeIzmenaDomovaZdravlja(DomZdravlja domZdravlja, EStatus status = EStatus.Dodaj)
        {
            InitializeComponent();

            this.DataContext = domZdravlja;

            odabranDomZdravlja = domZdravlja;
            odabranStatus = status;


            if (status.Equals(EStatus.Izmeni) && domZdravlja != null)
            {
                this.Title = "Izmeni Dom zdravlja";
           
                novaAdresa = domZdravlja.Adresa;
            }
            else
            {
                novaAdresa = new Adresa();

                this.Title = "Dodaj Dom zdravlja";
            }

        }


        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (odabranStatus.Equals(EStatus.Dodaj))
            {
                DomZdravlja dt = new DomZdravlja()
                {
                    NazivInstitucije = txtNazivInstitucije.Text,      
                    Adresa = DodavanjeIzmenaAdrese.adresa,
                    Aktivan = true

                };
                
                int id = Util.Instance.SacuvajEntitet(dt);
                dt.Id = id;
                Util.Instance.DomoviZdravlja.Add(dt);

                this.Close();
            }
            else if(odabranStatus.Equals(EStatus.Izmeni))
            {
                Util.Instance.UpdateEntiteta(odabranDomZdravlja);
            }
            this.Close();
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void btnAdresa_Click(object sender, RoutedEventArgs e)
        {
            DodavanjeIzmenaAdrese addAdresa = new DodavanjeIzmenaAdrese(novaAdresa, odabranStatus);
            //addAdresa.Show();
            if ((bool)addAdresa.ShowDialog())
            {

            }
        }
    }
}
