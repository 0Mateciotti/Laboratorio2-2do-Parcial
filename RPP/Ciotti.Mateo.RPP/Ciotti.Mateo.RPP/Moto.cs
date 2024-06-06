using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ciotti.Mateo.RPP
{
    public class Moto : Vehiculo
    {
        //ATRIBUTOS
        private ECilindrada _cilindrada;

        //PROPIEDADES
        public ECilindrada Cilindrada { get { return _cilindrada; } set { _cilindrada = value; } }

        //CONSTRUCTORES
        public Moto() { }
        public Moto(string marca,EPais pais,string modelo,float precio,ECilindrada cilindrada) : base(marca,pais,modelo,precio)
        { 
            _cilindrada = cilindrada;
        }

        //METODOS Y OPERADORES
        public static bool operator ==(Moto moto1, Moto moto2)
        {
            return ((Vehiculo)moto1 == (Vehiculo)moto2 && moto1.Cilindrada == moto2.Cilindrada) ;
        }
        public static bool operator !=(Moto moto1, Moto moto2)
        {
            return !(moto1 == moto2);        
        }

        public static explicit operator float(Moto moto)
        {
            return moto.Precio;
        }

        public override bool Equals(object obj)
        {
            return obj == this;
        }

        public override string ToString()
        {
            return $"{(string)this}CILINDRADA: {_cilindrada}\n";
        }

    }
}
