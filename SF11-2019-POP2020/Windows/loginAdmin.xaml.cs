using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class loginAdmin : Window
    {
        public loginAdmin()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlConn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;
                                                    Integrated Security=True;Connect Timeout=30;Encrypt=False;
                                                    TrustServerCertificate=False;ApplicationIntent=ReadWrite;
                                                    MultiSubnetFailover=False");
            string query = "Select * from korisnici where TipKorisnika like 'ADMINISTRATOR' and jmbg ='" + txtJmbg.Text.Trim() + "' and lozinka ='" + txtLozinka.Password.Trim() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, sqlConn);
            DataTable dtbl = new DataTable();
            sda.Fill(dtbl);
            if(dtbl.Rows.Count==1)
            {
                string jmbg = txtJmbg.Text.Trim();
                GlavnaStranicaAdministrator gsa = new GlavnaStranicaAdministrator();
                gsa.textBlock1.Text = txtJmbg.Text;
                this.Hide();
                gsa.Show();
            }
            else if (txtJmbg.Text.Equals(""))
            {
                MessageBox.Show("Unesite JMBG u polje namenjeno za to!");
            }
            else
            {
                MessageBox.Show("Netacni podaci za prijavu! Proveri ih ponovo!");
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonOdustani_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow window = new HomeWindow();

            this.Hide();
            window.Show();
        }
    }
}
