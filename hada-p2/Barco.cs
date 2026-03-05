using hada_p2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hada_p2
{
    class Barco
    {
  
            public Dictionary<Coordenada, string> CoordenadasBarco { get; private set; }
            public string Nombre { get; private set; }
            public int NumDanyos { get; private set; }

            public event EventHandler<TocadoArgs> eventoTocado;
            public event EventHandler<HundidoArgs> eventoHundido;

            public Barco(string nombre, int longitud, char orientacion, Coordenada coordenadaInicio)
            {
                Nombre = nombre;
                NumDanyos = 0;
                CoordenadasBarco = new Dictionary<Coordenada, string>();

                for (int i = 0; i < longitud; i++)
                {
                    Coordenada newCoordenada;
                    if (orientacion == 'h')
                    {
                        newCoordenada = new Coordenada(coordenadaInicio.Fila, coordenadaInicio.Columna + i);
                    }
                    else
                    {
                        newCoordenada = new Coordenada(coordenadaInicio.Fila + i, coordenadaInicio.Columna);
                    }
                    CoordenadasBarco[newCoordenada] = nombre;
                }

            }

            public void Disparo(Coordenada c)
            {
                if (CoordenadasBarco.ContainsKey(c) && !CoordenadasBarco[c].EndsWith("_T"))
                {
                    CoordenadasBarco[c] += "_T";
                    NumDanyos++;
                    eventoTocado?.Invoke(this, new TocadoArgs(Nombre, c));

                    if (hundido())
                    {
                        eventoHundido?.Invoke(this, new HundidoArgs(Nombre));
                    }
                }

            }

            public bool hundido()
            {
                foreach (var etiqueta in CoordenadasBarco.Values)
                {
                    if (!etiqueta.EndsWith("_T"))
                        return false;
                }
                return true;

            }

            public override string ToString()
            {
                string estado = hundido() ? "True" : "False";
                string info = $"[{Nombre}] - DAÑOS: [{NumDanyos}] - HUNDIDO: [{estado}] - COORDENADAS:";
                foreach (var coord in CoordenadasBarco)
                {
                    info += $" [{coord.Key} :{coord.Value}]";
                }
                return info;
            }
        }
    }
<<<<<<< HEAD
}
}
=======

>>>>>>> 23f109c (Barco añadido)
