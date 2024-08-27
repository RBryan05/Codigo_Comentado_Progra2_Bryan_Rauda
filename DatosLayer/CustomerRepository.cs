using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DatosLayer
{
    // Clase destinada a las Operaciones CRUD del codigo.
    public class CustomerRepository
    {
        // Metodo para obtener todos los registros a traves de un "SELECT".
        public List<Customers> ObtenerTodos() {
            // Llamamos la cadena de conexion en un "using" para que se cierre automaticamente.
            using (var conexion= DataBase.GetSqlConnection()) {
                // String que almacene el Comando
                String selectFrom = "";
                // Establecemos el codigo para seleccionar todos los clientes
                selectFrom = selectFrom + "SELECT [CustomerID] " + "\n";
                selectFrom = selectFrom + "      ,[CompanyName] " + "\n";
                selectFrom = selectFrom + "      ,[ContactName] " + "\n";
                selectFrom = selectFrom + "      ,[ContactTitle] " + "\n";
                selectFrom = selectFrom + "      ,[Address] " + "\n";
                selectFrom = selectFrom + "      ,[City] " + "\n";
                selectFrom = selectFrom + "      ,[Region] " + "\n";
                selectFrom = selectFrom + "      ,[PostalCode] " + "\n";
                selectFrom = selectFrom + "      ,[Country] " + "\n";
                selectFrom = selectFrom + "      ,[Phone] " + "\n";
                selectFrom = selectFrom + "      ,[Fax] " + "\n";
                selectFrom = selectFrom + "  FROM [dbo].[Customers]";

                // Establecemos el comando en un "using"
                using (SqlCommand comando = new SqlCommand(selectFrom, conexion)) {
                    // Ejecutamos el comando.
                    SqlDataReader reader = comando.ExecuteReader();
                    // Lista del tipo Custormers que almacene los registros.
                    List<Customers> Customers = new List<Customers>();

                    while (reader.Read())
                    {
                        // Llamamos el metodo que asigna los valores del reades a cada propiedad del modelo Customers
                        var customers = LeerDelDataReader(reader);
                        // Agregamos los clientes a la lista.
                        Customers.Add(customers);
                    }
                    // Devolvemos la lista de clientes.
                    return Customers;
                }
            }
           
        }
        public Customers ObtenerPorID(string id) {
            // Llamamos la cadena de conexion en un "using" para que se cierre automaticamente.
            using (var conexion = DataBase.GetSqlConnection()) {
                // String que almacene el Comando
                String selectForID = "";
                //Establecemos el codigo para seleccionar al cliente por ID.
                selectForID = selectForID + "SELECT [CustomerID] " + "\n";
                selectForID = selectForID + "      ,[CompanyName] " + "\n";
                selectForID = selectForID + "      ,[ContactName] " + "\n";
                selectForID = selectForID + "      ,[ContactTitle] " + "\n";
                selectForID = selectForID + "      ,[Address] " + "\n";
                selectForID = selectForID + "      ,[City] " + "\n";
                selectForID = selectForID + "      ,[Region] " + "\n";
                selectForID = selectForID + "      ,[PostalCode] " + "\n";
                selectForID = selectForID + "      ,[Country] " + "\n";
                selectForID = selectForID + "      ,[Phone] " + "\n";
                selectForID = selectForID + "      ,[Fax] " + "\n";
                selectForID = selectForID + "  FROM [dbo].[Customers] " + "\n";
                selectForID = selectForID + $"  Where CustomerID = @customerId";

                // Establecemos el comando en un "using"
                using (SqlCommand comando = new SqlCommand(selectForID, conexion))
                {
                    comando.Parameters.AddWithValue("customerId", id);

                    // Ejecutamos el comando.
                    var reader = comando.ExecuteReader();
                    // Lista del tipo Custormers que almacene los registros.
                    Customers customers = null;
                    //validadmos 
                    if (reader.Read()) {
                        // Llamamos el metodo que asigna los valores del reades a cada propiedad del modelo Customers

                        customers = LeerDelDataReader(reader);
                    }
                    // Retornamos la lista con el registro requerido
                    return customers;
                }

            }
        }

        // Metodo para asignar los datos que el reader obtenga a los elementos de nuestro modelo Customers
        public Customers LeerDelDataReader( SqlDataReader reader) {
          
            // Asignamos los valores del reader a el elemento del modelo que pertenezcan
            // Aseguramos que en caso de recibir valores nulos, sustituirlo por un string vacio ("").
            Customers customers = new Customers();
            customers.CustomerID = reader["CustomerID"] == DBNull.Value ? " " : (String)reader["CustomerID"];
            customers.CompanyName = reader["CompanyName"] == DBNull.Value ? "" : (String)reader["CompanyName"];
            customers.ContactName = reader["ContactName"] == DBNull.Value ? "" : (String)reader["ContactName"];
            customers.ContactTitle = reader["ContactTitle"] == DBNull.Value ? "" : (String)reader["ContactTitle"];
            customers.Address = reader["Address"] == DBNull.Value ? "" : (String)reader["Address"];
            customers.City = reader["City"] == DBNull.Value ? "" : (String)reader["City"];
            customers.Region = reader["Region"] == DBNull.Value ? "" : (String)reader["Region"];
            customers.PostalCode = reader["PostalCode"] == DBNull.Value ? "" : (String)reader["PostalCode"];
            customers.Country = reader["Country"] == DBNull.Value ? "" : (String)reader["Country"];
            customers.Phone = reader["Phone"] == DBNull.Value ? "" : (String)reader["Phone"];
            customers.Fax = reader["Fax"] == DBNull.Value ? "" : (String)reader["Fax"];
            // Devolvemos las el tipo de dato Customers con sus respectivos valores a sus elementos.
            return customers;
        }
        //-------------
        public int InsertarCliente(Customers customer) {
            // Llamamos la cadena de conexion en un "using" para que se cierre automaticamente.
            using (var conexion = DataBase.GetSqlConnection()) {
                // String que almacene el Comando
                String insertInto = "";
                // Establecemos un codigo para isertar un cliente.
                insertInto = insertInto + "INSERT INTO [dbo].[Customers] " + "\n";
                insertInto = insertInto + "           ([CustomerID] " + "\n";
                insertInto = insertInto + "           ,[CompanyName] " + "\n";
                insertInto = insertInto + "           ,[ContactName] " + "\n";
                insertInto = insertInto + "           ,[ContactTitle] " + "\n";
                insertInto = insertInto + "           ,[Address] " + "\n";
                insertInto = insertInto + "           ,[City]) " + "\n";
                insertInto = insertInto + "     VALUES " + "\n";
                insertInto = insertInto + "           (@CustomerID " + "\n";
                insertInto = insertInto + "           ,@CompanyName " + "\n";
                insertInto = insertInto + "           ,@ContactName " + "\n";
                insertInto = insertInto + "           ,@ContactTitle " + "\n";
                insertInto = insertInto + "           ,@Address " + "\n";
                insertInto = insertInto + "           ,@City)";

                // Establecemos el comando en un "using"
                using (var comando = new SqlCommand( insertInto,conexion )) {
                  // Llamamos al metodo que asigne los valores necesario y ejecute el comando.
                  int  insertados = parametrosCliente(customer, comando);
                    // Regresamos el resultado
                    return insertados;
                }

            }
        }
        //-------------
        public int ActualizarCliente(Customers customer) {
            // Llamamos la cadena de conexion en un "using" para que se cierre automaticamente.
            using (var conexion = DataBase.GetSqlConnection()) {
                // String que almacene el Comando
                String ActualizarCustomerPorID = "";
                // Establecemos el codigo para actualizar un cliente.
                ActualizarCustomerPorID = ActualizarCustomerPorID + "UPDATE [dbo].[Customers] " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "   SET [CustomerID] = @CustomerID " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "      ,[CompanyName] = @CompanyName " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "      ,[ContactName] = @ContactName " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "      ,[ContactTitle] = @ContactTitle " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "      ,[Address] = @Address " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "      ,[City] = @City " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + " WHERE CustomerID= @CustomerID";

                // Establecemos el comando en un "using"
                using (var comando = new SqlCommand(ActualizarCustomerPorID, conexion)) {
                    // Llamamos al metodo que asigne los valores necesario y ejecute el comando.
                    int actualizados = parametrosCliente(customer, comando);
                    // Regresamos un int que indiqe el numero de elementos actualizados
                    return actualizados;
                }
            } 
        }

        // Metodo que establezca los parametros del cliente.
        public int parametrosCliente(Customers customer, SqlCommand comando) {
            // Asiganmos los valores de los elementos del modelo Customers a las varibales el el codigo sql
            comando.Parameters.AddWithValue("CustomerID", customer.CustomerID);
            comando.Parameters.AddWithValue("CompanyName", customer.CompanyName);
            comando.Parameters.AddWithValue("ContactName", customer.ContactName);
            comando.Parameters.AddWithValue("ContactTitle", customer.ContactName);
            comando.Parameters.AddWithValue("Address", customer.Address);
            comando.Parameters.AddWithValue("City", customer.City);
            // Ejecutamos el comando.
            var insertados = comando.ExecuteNonQuery();
            // Retornamos los valores insertados.
            return insertados;
        }

        public int EliminarCliente(string id) {
            // Llamamos la cadena de conexion en un "using" para que se cierre automaticamente.
            using (var conexion = DataBase.GetSqlConnection() ){
                // String que almacene el Comando
                String EliminarCliente = "";
                // Establecemos el codigo para eliminar un cliente
                EliminarCliente = EliminarCliente + "DELETE FROM [dbo].[Customers] " + "\n";
                EliminarCliente = EliminarCliente + "      WHERE CustomerID = @CustomerID";

                // Establecemos el comando en un "using"
                using (SqlCommand comando = new SqlCommand(EliminarCliente, conexion)) {
                    comando.Parameters.AddWithValue("@CustomerID", id);
                    // Ejecutamos el comando.
                    int elimindos = comando.ExecuteNonQuery();
                    // Retornamos un int con el numero de filas afectadas.
                    return elimindos;
                }
            }
        }
    }
}
