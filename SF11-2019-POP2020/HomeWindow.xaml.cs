using SF11_2019_POP2020.Models;
using SF11_2019_POP2020.Windows;
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

namespace SF11_2019_POP2020
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
            InitializeComponent();
            Util.Instance.CitanjeEntiteta("korisnici.txt");
            Util.Instance.CitanjeEntiteta("lekari.txt");
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            SviLekari window = new SviLekari();

            this.Hide();
            window.Show();
        }

        private void btnAdministratori_Click(object sender, RoutedEventArgs e)
        {
            SviAdministratori windowAdministratori = new SviAdministratori();

            this.Hide();
            windowAdministratori.Show();
        }
    }
}
