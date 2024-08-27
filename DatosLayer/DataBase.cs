using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace DatosLayer
{
    public class DataBase
    {
        // Establecemos los elementos necesario para una cadena de conexion segura
        public static string ConnectionString {
            // Establecemos en un "get" el dato que el metodo de la clase retornara al instanciarlo.
            get
            {
                // Accedemos a la cadena de conexion de nuestro archivo App.config
                 string CadenaConexion = ConfigurationManager
                    // Identificamos el connectionString por el nombre establecido en el App.config 
                    .ConnectionStrings["NWConnection"]
                    .ConnectionString;

                // Le decimos a nuestro codigo que la cadena identificada anteriormente es un tipo de dato SqlConnectionStringBuilder. 
                SqlConnectionStringBuilder conexionBuilder = 
                    new SqlConnectionStringBuilder(CadenaConexion);

                // Establecemos la posibilidad de que nuestra caneda de conexion reciba un ApplicationName
                conexionBuilder.ApplicationName =
                    // El operador "??" indica que se usa ApplicationName solo si no es nulo
                    ApplicationName ?? conexionBuilder.ApplicationName;

                // Establecemos la posibilidad de que nuestra caneda de conexion reciba un ConnectionTimeout
                conexionBuilder.ConnectTimeout = ( ConnectionTimeout > 0 )
                    // Condicion en la cual si el ConnectionTimeout es mayor que 0, esta se le asignara a nuestra cadena
                    ? ConnectionTimeout : conexionBuilder.ConnectTimeout;
                // Devolvemos la cadena de conexion con sus elementos completos.
                return conexionBuilder.ToString();
            }


        }
        // Definimos un metodo en el cual podamos aplicar un ConnectionTimeout a nuestra cadena de conexion
        // ya que el get no nos permite darle como parametro directamente un valor.
        public static int ConnectionTimeout { get; set; }
        // Definimos un metodo en el cual podamos aplicar un ApplicationName a nuestra cadena de conexion
        // ya que el get no nos permite darle como parametro directamente un valor.
        public static string ApplicationName { get; set; }

        // Establecemos el metodo que de la cadena de coneccion; al que tendra acceso todo el codigo.
        public static SqlConnection GetSqlConnection()
        {
            // Instanciamos nuestro metodo que devuelve la cadena de coneccion
            SqlConnection conexion = new SqlConnection(ConnectionString);
            // Abrimos la coneccion.
            conexion.Open();
            // Retornamos la conexion abierta
            return conexion;
            
        } 
    }
}
