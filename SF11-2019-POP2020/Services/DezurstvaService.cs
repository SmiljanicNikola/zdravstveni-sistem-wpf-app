using SF11_2019_POP2020.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF11_2019_POP2020.Services
{
    class DezurstvaService : IDezurstvaService
    {
        

        public void readDezurstva()
        {
            Util.Instance.Dezurstva = new ObservableCollection<Dezurstvo>();

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"Select * from Dezurstva where Aktivan=1";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Util.Instance.Dezurstva.Add(new Dezurstvo
                    { 
                    Id = reader.GetInt32(0),
                    Lekar = Util.Instance.lekarPoId(reader.GetInt32(1)),
                    Pocetak = reader.GetDateTime(2).Date,
                    Kraj = reader.GetDateTime(3).Date,
                    Aktivan = reader.GetBoolean(4)
                });
                }
                reader.Close();          
            }
        } 
    }
}
