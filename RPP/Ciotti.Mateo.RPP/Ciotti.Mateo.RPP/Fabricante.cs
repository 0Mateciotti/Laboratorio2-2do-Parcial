using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Ciotti.Mateo.RPP
{
    public class Fabricante
    {
        //ATRIBUTOS
        private string _marca;
        private EPais _pais;

        //PROPIEDADES
        public string Marca {  get { return _marca; } set { _marca = value; } }
        public EPais Pais { get { return _pais; } set { _pais = value; } }

        //CONSTRUCTORES
        public Fabricante() { }
        public Fabricante(string marca, EPais pais)
        {
            _marca = marca;
            _pais = pais;
        }
        
        //METODOS Y OPERADORES

        public static bool operator ==(Fabricante fabricante1, Fabricante fabricante2)
        {
            return (fabricante1.Marca == fabricante2.Marca && fabricante1._pais == fabricante2.Pais);
        }

        public static bool operator !=(Fabricante fabricante1,Fabricante fabricante2)
        {
            return !(fabricante1 == fabricante2);
        }

        /// <summary>
        /// parsea de forma implicita un objeto de clase Fabricante a string
        /// </summary>
        /// <param name="fabricante"></param>
        public static implicit operator string(Fabricante fabricante) 
        {
            return $"{fabricante.Marca.ToUpper()} - {fabricante.Pais}";
        }
    }
}
