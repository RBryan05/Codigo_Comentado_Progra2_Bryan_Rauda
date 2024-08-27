using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using DatosLayer;
using System.Net;
using System.Reflection;


namespace ConexionEjemplo
{
    public partial class Form1 : Form
    {
        // Inicializamos la clase CustomerRepository para poder acceder de manera correcta
        // a los metodos de acceso a laos registros de la base de datos que esta clase posee.
        CustomerRepository customerRepository = new CustomerRepository();
       
        public Form1()
        {
            InitializeComponent();
        }

        // Evento "Click" que ayude a cargar los datos al dataGridView.
        private void btnCargar_Click(object sender, EventArgs e)
        {
            // Creamos una variable que almacenara los datos que el metodo ObtenerTodos() de la clase
            // CustomerRepository brinde
            var Customers = customerRepository.ObtenerTodos();
            // Asignamos los datos de la variables como fuente de datos del dataGridView.
            dataGrid.DataSource = Customers;
        }

        // Evento "Click que envie los datos requeridos al buscar un registro de la tabla Customers
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Iniciamos una variable que obtiene clientes por ID
            // Luego asignamos los atributos del registro obtenido a cada textBox.
            var cliente = customerRepository.ObtenerPorID(txtBuscar.Text);
            tboxCustomerID.Text = cliente.CustomerID;
            tboxCompanyName.Text = cliente.CompanyName;
            tboxContacName.Text = cliente.ContactName;
            tboxContactTitle.Text= cliente.ContactTitle;
            tboxAddress.Text = cliente.Address;
            tboxCity.Text = cliente.City;

        }

        // Evento "Click" que nos sirva para insertar la informacion requerida para un nuevo registro.
        private void btnInsertar_Click(object sender, EventArgs e)
        {
            // Inicializamos un entero para saber el numero de filas insertadas.
            int resultado;

            // Llamamos el metodo que posee la informacion del nuevo cliente a insertar.
            var nuevoCliente = ObtenerNuevoCliente();

            // Creamos un if para verificar que no haya un campo con valores nulos
            if (validarCampoNull(nuevoCliente) == false)
            {
                // Guardamos en el int resultado el metodo InsertarCliente que devuelve un int de numero
                // de filas afectadas
                resultado = customerRepository.InsertarCliente(nuevoCliente);
                // Mensaje del numero de filas afectadas.
                MessageBox.Show("Guardado" + "Filas modificadas = " + resultado);
            }
            else {
                // Mensaje en caso de que hayan campos null
                MessageBox.Show("Debe completar los campos por favor");
            }
        }
        // Si encuentra un null enviara true de lo contrario false
        public Boolean validarCampoNull(Object objeto) {
            // Bucle que recorra los valores de los textBox para comprobar que sean validos
            foreach (PropertyInfo property in objeto.GetType().GetProperties()) {
                object value = property.GetValue(objeto, null);
                if ((string)value == "") {
                    return true;
                }
            }
            return false;
        }

        // Evento en caso de modificar un cliente
        private void btModificar_Click(object sender, EventArgs e)
        {
            // Variable que obtenga la informacion a actualizar del cliente
            var actualizarCliente = ObtenerNuevoCliente();
            // Llamamos al metodo que actualice al cliente con sus respectvios cambios como parametro.
            int actualizadas = customerRepository.ActualizarCliente(actualizarCliente);
            MessageBox.Show($"Filas actualizadas = {actualizadas}");
        }

        // Metodo que obtenga la informacion de los textBox.
        private Customers ObtenerNuevoCliente() {

            // Variable que contenga cada dato del cliente.
            var nuevoCliente = new Customers
            {
                // Asignamos los atributos a los textBox.
                CustomerID = tboxCustomerID.Text,
                CompanyName = tboxCompanyName.Text,
                ContactName = tboxContacName.Text,
                ContactTitle = tboxContactTitle.Text,
                Address = tboxAddress.Text,
                City = tboxCity.Text
            };

            // Devuelve un elemento de tipo Customers que contiene la informacion del cliente
            return nuevoCliente;
        }

        // Eevento para eliminar un cliente.
        private void btnEliminar_Click(object sender, EventArgs e)
        {
           // Llamamos al metodo para eliminar clientes en un int ya que este metodo devuelve un int.
           int elimindas = customerRepository.EliminarCliente(tboxCustomerID.Text);
            // Mostramos el numero de filas eliminadas.
            MessageBox.Show("Filas eliminadas = " + elimindas);
        }
    }
}
