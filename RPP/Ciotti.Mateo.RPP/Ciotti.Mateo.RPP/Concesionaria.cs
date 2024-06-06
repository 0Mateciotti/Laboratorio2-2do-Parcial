using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Ciotti.Mateo.RPP
{
    [XmlInclude(typeof(Vehiculo))]
    [XmlInclude(typeof(Auto))]
    [XmlInclude(typeof(Moto))]
    [XmlInclude(typeof(Fabricante))]
    public class Concesionaria
    {
        //ATRIBUTOS
        private int _capacidad;
        private List<Vehiculo> _Vehiculos;

        //PROPIEDADES
        public int Capacidad { get { return _capacidad; } set { _capacidad = value; } }
        public List<Vehiculo> Vehiculos { get { return _Vehiculos; } set { _Vehiculos = value; } }

        //CONSTRUCTORES
        public Concesionaria()
        {
            _Vehiculos = new List<Vehiculo>();
        }

        private Concesionaria(int capacidad) : this()
        {
            _capacidad = capacidad;
        }
        public double PrecioDeAutos
        {
            get { return ObtenerPrecios(EVehiculo.Auto); }
            set { }

        }
        public double PrecioDeMotos
        {
            get { return ObtenerPrecios(EVehiculo.Moto); }
            set { }
        }
        public double PrecioDeTotal
        {
            get { return ObtenerPrecios(EVehiculo.Ambos); }
            set { }
        }
        
        //METODOS Y OPERADORES

        /// <summary>
        /// Valida que tipo de vehiculo recibio y suma todos los precios de ese tipo de vehiculo que tiene la concesionaria.
        /// </summary>
        /// <param name="tipoVehiculo">Un elemento del enumerado EVehiculo</param>
        /// <returns>Devuelve el precio de los autos, motos o ambos</returns>
        private double ObtenerPrecios(EVehiculo tipoVehiculo)
        {

            double precio = 0;

            foreach (Vehiculo vehiculo in _Vehiculos)
            {
                if (tipoVehiculo == EVehiculo.Auto && vehiculo is Auto)
                {
                    precio += vehiculo.Precio;
                }
                else if (tipoVehiculo == EVehiculo.Moto && vehiculo is Moto)
                {
                    precio += vehiculo.Precio;
                }
                else if (tipoVehiculo == EVehiculo.Ambos)
                {
                    precio += vehiculo.Precio;
                }
            }

            return precio;
        }

        /// <summary>
        /// Crea un texto a partir de todos los precios y vehiculos de la concesionaria.
        /// </summary>
        /// <param name="concesionaria"></param>
        /// <returns>Devuelve la informacion completa de la concesionaria.</returns>
        public static string Mostrar(Concesionaria concesionaria)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Capacidad: {concesionaria.Capacidad}\n" +
                $"Total por autos: ${concesionaria.PrecioDeAutos}\n" +
                $"Total por motos: ${concesionaria.PrecioDeMotos}\n" +
                $"Total: ${concesionaria.PrecioDeTotal}\n");
            sb.Append("********************************************\n              Listado de Vehiculos             \n********************************************\n");
            foreach (Vehiculo vehiculo in concesionaria.Vehiculos)
            {
                sb.AppendLine(vehiculo.ToString());
            }

            return sb.ToString();
        }
        /// <summary>
        /// devolvera true en caso de que la concesionaria contenga al vehiculo.
        /// </summary>
        /// <param name="concesionaria"></param>
        /// <param name="vehiculo"></param>
        /// <returns>devolvera true en caso de que la concesionaria contenga al vehiculo.</returns>
        public static bool operator ==(Concesionaria concesionaria, Vehiculo vehiculo)
        {
           
            return concesionaria.Vehiculos.Contains(vehiculo);
        }
        public static bool operator !=(Concesionaria concesionaria, Vehiculo vehiculo)
        {
            return !(concesionaria == vehiculo);
        }
        public static implicit operator Concesionaria(int capacidad)
        {
            return new Concesionaria(capacidad);
        }
        /// <summary>
        /// en caso de pasar las validaciones agrega al vehiculo a la concesionaria de lo contrario mostrara un mensaje.
        /// </summary>
        /// <param name="concesionaria"></param>
        /// <param name="vehiculo"></param>
        /// <returns></returns>
        public static Concesionaria operator +(Concesionaria concesionaria, Vehiculo vehiculo)
        {
            Concesionaria nueva = concesionaria;
            
            if(concesionaria == vehiculo)
            {
                Console.WriteLine("¡El vehículo ya está en la concesionaria!");
            }
            else if(concesionaria.Vehiculos.Count >= concesionaria._capacidad)
            {
                Console.WriteLine("¡No hay más lugar en la concesionaria!");
            }
            else 
            {
                nueva.Vehiculos.Add(vehiculo);
            }


            return nueva;
        }
        /// <summary>
        /// En caso de que el vehiculo se encuentre en la concesionaria lo elimina de la lista, caso contrario mostrara un mensaje
        /// </summary>
        /// <param name="concesionaria"></param>
        /// <param name="vehiculo"></param>
        /// <returns>instancia de concesionaria igual o sin el vehiculo </returns>
        public static Concesionaria operator -(Concesionaria concesionaria, Vehiculo vehiculo)
        {
            Concesionaria nueva = concesionaria;

            if (concesionaria == vehiculo)
            {
                nueva.Vehiculos.Remove(vehiculo);
            }
            else
            {
                Console.WriteLine("¡El vehículo no está en la concesionaria!");
            }
            return nueva;

        }

        /// <summary>
        /// Guarda el texto de la concecionaria, lo escribe y guarda en un archivo de texto
        /// </summary>
        /// <param name="rutaArchivo">Ruta donde escribir el archivo</param>
        public void GuardarConcesionaria(string rutaArchivo)
        {
            string texto = Mostrar(this);

            using (StreamWriter sw = new StreamWriter(rutaArchivo))
            {
                sw.WriteLine(texto);
                sw.Close();
            }

        }

        /// <summary>
        /// Serializa la instancia actual de Concesionaria en formato XML
        /// </summary>
        /// <param name="rutaArchivo">Ruta donde guardar el XML</param>
        public void SerializarConcesionaria(string rutaArchivo)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Concesionaria));
            
            using(StreamWriter sw = new StreamWriter(rutaArchivo))
            {
                try
                {
                    xmlSerializer.Serialize(sw, this);
                }
                catch 
                {

                }
            }
        }

        /// <summary>
        /// Deserializa un archivo XML y lo guarda
        /// </summary>
        /// <param name="rutaArchivo">Ruta donde estaria el archivo XML</param>
        /// <returns>Instancia de la clase Concesionaria resultado de la deserializacion.</returns>
        public static Concesionaria DeserializarConcesionaria(string rutaArchivo)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Concesionaria));
            object objeto = new object();
            //Concesionaria concesionaria;
            Concesionaria concesionaria = new Concesionaria();

            using (StreamReader sr = new StreamReader(rutaArchivo))
            {
                try
                {
                     concesionaria = (Concesionaria)xmlSerializer.Deserialize(sr);
                }
                catch
                {

                }


            }
            return concesionaria;
        }



    }
}
