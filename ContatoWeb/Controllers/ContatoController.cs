using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ContatoWeb.Controllers
{
    public class ContatoController : ApiController
    {
        private SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["azure"].ConnectionString);
        public List<Contato> Get()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"Select * from Contato FOR JSON auto";

            SqlDataReader reader = cmd.ExecuteReader();
            var lista = new List<Contato>();
            while (reader.Read())
            {
                Contato c = new Contato
                {
                    Nome = (string)reader["Nome"],
                    Email = (string)reader["email"]
                };
                lista.Add(c);
            }
            return lista;
            
            
            conn.Close();
            return null;
        }
    }

    public class Contato
    {
        public int ContatoId { get; set;  }
        public string Nome { get; set; }
        public string Email { get; set; }
    }
    
}
