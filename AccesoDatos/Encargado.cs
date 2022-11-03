using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AccesoDatos
{
    public class Encargado
    {
        public long id;
        public string nombre;
        public string nombre_usuario;
        public string contraseña;
        public Boolean is_admin;
        public static SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        public Encargado()
        {
            
            
        }
        public static List<Encargado> GetAll()
        {
            builder.DataSource = "C02PC15\\SQLEXPRESS";
            builder.UserID = "Fran";
            builder.Password = "";
            builder.InitialCatalog = "biblioteca_comercio";
            List<Encargado> lista = new List<Encargado>();

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = "SELECT * FROM encargado";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Encargado e = new Encargado
                            {
                                id = reader.GetInt32(0),
                                nombre = reader.GetString(1),
                                nombre_usuario = reader.GetString(2),
                                contraseña = reader.GetString(3),
                                is_admin = reader.GetBoolean(4)
                            };
                            lista.Add(e);
                        }
                    }
                }
            }
            return lista;
        }

        public static Encargado GetById(long id)
        {
            builder.DataSource = "C02PC15\\SQLEXPRESS";
            builder.UserID = "Fran";
            builder.Password = "";
            builder.InitialCatalog = "biblioteca_comercio";

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = "SELECT * FROM encargado WHERE id=" + id;

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Encargado e = new Encargado
                            {
                                id = reader.GetInt32(0),
                                nombre = reader.GetString(1),
                                nombre_usuario = reader.GetString(2),
                                contraseña = reader.GetString(3),
                                is_admin = reader.GetBoolean(4)
                            };
                            return e;
                        }

                    }
                }
            }
            return null;
        }

        public static List<Encargado> GetByName(string name)
        {
            builder.DataSource = "C02PC15\\SQLEXPRESS";
            builder.UserID = "Fran";
            builder.Password = "";
            builder.InitialCatalog = "biblioteca_comercio";
            List<Encargado> lista = new List<Encargado>();

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = "SELECT * FROM encargado WHERE nombre=" + name;

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Encargado e = new Encargado
                            {
                                id = reader.GetInt32(0),
                                nombre = reader.GetString(1),
                                nombre_usuario = reader.GetString(2),
                                contraseña = reader.GetString(3),
                                is_admin = reader.GetBoolean(4)
                            };
                            lista.Add(e);
                        }
                    }
                }
            }
            return lista;
        }

    }
}
