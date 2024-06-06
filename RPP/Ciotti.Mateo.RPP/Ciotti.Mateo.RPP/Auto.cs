using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ciotti.Mateo.RPP
{
    public class Auto : Vehiculo
    {
        //ATRIBUTOS
        private ETipo _tipo;

        //PROPIEDADES
        public ETipo Tipo { get { return _tipo; } set { _tipo = value; } }

        //CONSTRUCTORES
        public Auto() { }
        /// <summary>
        /// Genera una instancia apartir de los datos recibidos y reutiliza el constructor base de Vehiculo
        /// </summary>
        /// <param name="modelo"></param>
        /// <param name="precio"></param>
        /// <param name="fabricante"></param>
        /// <param name="tipo"></param>
        public Auto(string modelo, float precio, Fabricante fabricante, ETipo tipo) : base(modelo, precio, fabricante)
        {
            _tipo = tipo;
        }

        //OPERADORES
        public static bool operator ==(Auto auto1, Auto auto2)
        {
            return (Vehiculo)auto1 == (Vehiculo)auto2;
        }
        public static bool operator !=(Auto auto1, Auto auto2)
        {
            return !(auto1 == auto2);
        }
        public static implicit operator float(Auto auto)
        {
            if(auto is null)
            {
                return -1;
            }
            return auto.Precio;
        }

        public override bool Equals(object obj)
        {
            return obj == this;
        }

        public override string ToString()
        {
            return $"{(string)this}Tipo: {_tipo}\n";
        }


    }
}
