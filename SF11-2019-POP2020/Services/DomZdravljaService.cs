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
    class DomZdravljaService : IDomZdravljaService
    {
        public void deleteDomoveZdravlja(int id)
        {
            throw new NotImplementedException();
        }

        public void readDomoveZdravlja()
        {
            Util.Instance.DomoviZdravlja = new ObservableCollection<DomZdravlja>();

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"Select * from DomoviZdravlja where Aktivan=1";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Util.Instance.DomoviZdravlja.Add(new DomZdravlja
                    {
                        Id = reader.GetInt32(0),
                        NazivInstitucije = reader.GetString(1),
                        adresaId = reader.GetInt32(2),
                        Aktivan = reader.GetBoolean(3)
                    });
                }
                reader.Close();
            }
            }

            public int saveDomoveZdravlja(object obj)
        {
            throw new NotImplementedException();
        }

        public void updateDomoveZdravlja(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
