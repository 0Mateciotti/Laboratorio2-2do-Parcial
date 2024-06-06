using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ciotti.Mateo.RPP
{
    public abstract class Vehiculo
    {
        // ATRIBUTOS
        protected Fabricante _fabricante;
        protected static Random _GeneradorDeVelocidad;
        protected string _modelo;
        protected float _precio;
        protected int _velocidadMaxima;

        //CONSTRUCTORES
        static Vehiculo()
        {
            _GeneradorDeVelocidad = new Random();
        }
        public Vehiculo() { }
        /// <summary>
        /// Crea una instancia de una clase heredada de Vehiculo llamando al otro constructor con los datos recibidos 
        /// </summary>
        /// <param name="marca"></param>
        /// <param name="pais"></param>
        /// <param name="modelo"></param>
        /// <param name="precio"></param>
        public Vehiculo(string marca, EPais pais, string modelo, float precio) : this(modelo, precio, new Fabricante(marca, pais))
        {

        }
        public Vehiculo(string modelo, float precio, Fabricante fabricante)
        {
            _fabricante = fabricante;
            _modelo = modelo;
            _precio = precio;

        }

        //PROPIEDADES
        /// <summary>
        /// Si la velocidad maxima es igual a cero le asigna un valor random de 100-280
        /// </summary>
        public int VelocidadMaxima
        {
            get
            {
                if (_velocidadMaxima == 0)
                {
                    _velocidadMaxima = _GeneradorDeVelocidad.Next(100, 280);
                }
                return _velocidadMaxima;
            }
            set
            {
                _velocidadMaxima = value;
            }
        }
        public Fabricante Fabricante { get { return _fabricante; } set { _fabricante = value; } }
        public string Modelo { get { return _modelo; } set { _modelo = value; } }
        public float Precio { get { return _precio; } set { _precio = value; } }
        
        //METODOS Y OPERADORES
        /// <summary>
        /// Genera un texto a partir de los datos del vehiculo
        /// </summary>
        /// <returns>Una cadena de texto especificando los datos del vehiculo.</returns>
        private string Mostrar()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"FABRICANTE: {(string)Fabricante}\n" +
                $"MODELO: {Modelo}\n" +
                $"VELOCIDAD MAXIMA: {VelocidadMaxima}\n" +
                $"PRECIO: ${Precio}");

            return sb.ToString();
        }
        /// <summary>
        /// Devolvera true si el modelo y fabricante de dos autos es el mismo
        /// </summary>
        /// <param name="vehiculo1"></param>
        /// <param name="vehiculo2"></param>
        /// <returns></returns>
        public static bool operator ==(Vehiculo vehiculo1, Vehiculo vehiculo2)
        {
            return (vehiculo1.Modelo == vehiculo2.Modelo && vehiculo1.Fabricante == vehiculo2.Fabricante);
        }
        public static bool operator !=(Vehiculo vehiculo1, Vehiculo vehiculo2)
        {
            return !(vehiculo1 == vehiculo2);
        }
        /// <summary>
        /// convierte una instancia de Vehiculo a string con sus datos.
        /// </summary>
        /// <param name="vehiculo"></param>
        public static explicit operator string(Vehiculo vehiculo) 
        {
            return $"{vehiculo.Mostrar()}";
        }

    }
}
