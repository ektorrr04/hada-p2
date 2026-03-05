using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class Coordenada
    {
        private int fila;
        private int columna;

        public int Fila
        {
            get { return fila; }
            set
            {
                if (value < 0 || value > 9)
                    throw new ArgumentOutOfRangeException(nameof(Fila), "La fila debe estar entre 0 y 9");
                fila = value;
            }
        }

        public int Columna
        {
            get { return columna; }
            set
            {
                if (value < 0 || value > 9)
                    throw new ArgumentOutOfRangeException(nameof(Columna), "La columna debe estar entre 0 y 9");
                columna = value;

            }
        }

        public Coordenada()
        {
            Fila = 0;
            Columna = 0;
        }
        public Coordenada(int fila, int columna)
        {
            Fila = fila;
            Columna = columna;
        }
        public Coordenada(string fila, string columna)
        {
            if (!int.TryParse(fila, out int filaInt) || !int.TryParse(columna, out int columnaInt))
                throw new ArgumentException("Los valores tienen que ser numeros enteros");

            Fila = filaInt;
            Columna = columnaInt;
        }
        public Coordenada(Coordenada coordenada)
        {
            if (coordenada == null)
                throw new ArgumentNullException(nameof(coordenada), "La coordenada no puede ser nula");

            Fila = coordenada.Fila;
            Columna = coordenada.Columna;
        }

        public override string ToString()
        {
            return $"({Fila},{Columna})";
        }

        public override int GetHashCode()
        {
            return Fila.GetHashCode() ^ Columna.GetHashCode();
        }
        public override bool Equals(object obj)
        {

            if (obj is Coordenada distintaCoordenada)
                return Equals(distintaCoordenada);
            return false;
        }

        public bool Equals(Coordenada coordenada)
        {
            return coordenada != null && this.Fila == coordenada.Fila && this.Columna == coordenada.Columna;
        }
    }
}
