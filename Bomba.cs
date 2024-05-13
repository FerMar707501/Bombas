using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONS
{
    public class Bomba
    {
        public int Numero;
        public bool Disponible;
        public double PrecioPorLitro;

        public Bomba(int numero,bool disponible,double precioporlitro)
        {
            this.Numero = numero;
            this.Disponible = disponible;
            this.PrecioPorLitro = precioporlitro;
        }
    }
    public class Abastecimiento
    {
        public string cliente;
        public int LitrosAbastecidos;
        public double MontoPagado;
        public string TipoAbastecimiento;//prepago o tanque lleno
    }
    public class cliente
    {
        public string Nombre;
        public string tipo;

        public cliente(string nombre, string tipo)
        {
            this.Nombre=nombre;
            this.tipo = tipo;
        }
    }
    public class GestionAbastecimiento
    {
        public List<Abastecimiento> Abastecimientos { get; set; }

        public GestionAbastecimiento()
        {
            Abastecimientos = new List<Abastecimiento>();
        }
    }


}
