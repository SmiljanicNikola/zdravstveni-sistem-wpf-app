﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SF11_2019_POP2020.Models;
using SF11_2019_POP2020.MyExceptions;

namespace SF11_2019_POP2020.Services
{
    public class UserService : IUserService
    {
        public void deleteUser()
        {
            
        }

        public void readUsers()
        {

                
            
        }

      
        public int saveUser(Object obj)
        {
            Korisnik korisnik = obj as Korisnik;

            using (SqlConnection conn = new SqlConnection(Util.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"insert into dbo.Korisnici(ime, prezime, jmbg, email, adresaId, pol, lozinka, TipKorisnika, aktivan)
                       output inserted.id VALUES(@Ime, @Prezime, @Jmbg, @Email, @AdresaId, @Pol, @Lozinka, @TipKorisnika, @Aktivan)";

                command.Parameters.Add(new SqlParameter("Ime", korisnik.Ime));
                command.Parameters.Add(new SqlParameter("Prezime", korisnik.Prezime));
                command.Parameters.Add(new SqlParameter("Jmbg", korisnik.Jmbg));
                command.Parameters.Add(new SqlParameter("Email", korisnik.Email));
                command.Parameters.Add(new SqlParameter("AdresaId", korisnik.AdresaId));
                command.Parameters.Add(new SqlParameter("Pol", korisnik.Pol.ToString()));
                command.Parameters.Add(new SqlParameter("Lozinka", korisnik.Lozinka));
                command.Parameters.Add(new SqlParameter("TipKorisnika", korisnik.TipKorisnika));
                command.Parameters.Add(new SqlParameter("Aktivan", korisnik.Aktivan));

               return (int)command.ExecuteScalar();


            }

               // return -1;
        }

    }
}