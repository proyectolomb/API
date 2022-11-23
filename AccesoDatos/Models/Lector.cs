using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace AccesoDatos.Models
{
    public class Lector
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public DateTime fecha_nacimiento { get; set;}
        public DateTime fecha_alta { get; set; }
        public string curso_departamento { get; set; }

        public static SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        public static List<Lector> GetAll()
        {
            builder.DataSource = "C02PC15\\SQLEXPRESS";
            builder.UserID = "Fran";
            builder.Password = "";
            builder.InitialCatalog = "biblioteca_comercio";
            List<Lector> lista = new List<Lector>();
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = "SELECT id, nombre, apellidos, fecha_de_nacimiento, fecha_de_alta FROM Lector";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Lector e = new Lector
                            {
                                id = reader.GetInt32(0),
                                nombre = reader.GetString(1),
                                apellidos = reader.GetString(2),
                                fecha_nacimiento = reader.GetDateTime(3),
                                fecha_alta = reader.GetDateTime(4),
                                curso_departamento = getCursoOrDepartamento()
                            };
                            lista.Add(e);
                        }
                    }
                }
            }
            return lista;


        }

        public static string getCursoOrDepartamento()
        {
            var seed = Environment.TickCount;
            var random = new Random(seed);
            var value = random.Next(0, 5);
            switch (value)
            {
                case 0:
                    return "INFORMATICA";
                case 1:
                    return "2º MULWEB";
                case 2:
                    return "MATEMATICAS";
                case 3:
                    return "1º MULWEB";
                case 4:
                    return "2º DAM";
                case 5:
                    return "LENGUA";
            }
            return "1º SMR";
        }
    }
}
