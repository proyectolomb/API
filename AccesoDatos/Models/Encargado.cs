using AccesoDatos.Utils;
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

        // Método que devuelve todos los encargados
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

        // Método que devuelve un encargado por su id
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

        // Método que devuelve una lista de encargados por su nombre
        public static List<Encargado> GetByName(string name)
        {
            builder.DataSource = "C02PC15\\SQLEXPRESS";
            builder.UserID = "Fran";
            builder.Password = "";
            builder.InitialCatalog = "biblioteca_comercio";
            List<Encargado> lista = new List<Encargado>();

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = "SELECT * FROM encargado WHERE nombre='" + name + "'";

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

        // Método que crea un encargado
        public static bool Create(Encargado e)
        {
            builder.DataSource = "C02PC15\\SQLEXPRESS";
            builder.UserID = "Fran";
            builder.Password = "";
            builder.InitialCatalog = "biblioteca_comercio";
            // e.contraseña = Encryptor.MD5Hash(e.contraseña);

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = String.Format("INSERT INTO encargado VALUES ({0}, '{1}', '{2}', '{3}', {4})", e.id, e.nombre, e.nombre_usuario, e.contraseña, e.is_admin ? 1:0);

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();

                    try { 
                        command.ExecuteNonQuery(); 
                        return true; 
                    } 
                    catch
                    {
                        return false;     
                    }
                   
                }
            }
        }


        // Método que borra un encargado por su id
        public static bool Delete(int id)
        {
            builder.DataSource = "C02PC15\\SQLEXPRESS";
            builder.UserID = "Fran";
            builder.Password = "";
            builder.InitialCatalog = "biblioteca_comercio";

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = String.Format("DELETE FROM encargado WHERE id={0}", id);

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();

                    try
                    {
                        command.ExecuteNonQuery();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }

                }
            }
        }

        // Método que actualiza un encargado
        public static bool Update(Encargado e)
        {
            builder.DataSource = "C02PC15\\SQLEXPRESS";
            builder.UserID = "Fran";
            builder.Password = "";
            builder.InitialCatalog = "biblioteca_comercio";

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                String sql = String.Format("UPDATE encargado SET nombre='{0}', Nombre_de_usuario='{1}', contraseña='{2}', es_admin={3} WHERE id={4}", e.nombre, e.nombre_usuario, e.contraseña, e.is_admin ? 1:0, e.id);

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();

                    try
                    {
                        command.ExecuteNonQuery();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }

            }
            }

        }
    }
}

