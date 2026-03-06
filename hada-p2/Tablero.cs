using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada_p2
{
    public class Tablero
    {
        public int TamTablero { get; private set; }

        private List<Coordenada> coordenadasDisparadas;
        private List<Coordenada> coordenadasTocadas;
        private List<Barco> barcos;
        private List<Barco> barcosEliminados;
        private Dictionary<Coordenada, string> casillasTablero;

        public event EventHandler<EventArgs> eventoFinPartida;
        public Tablero(int tamTablero, List<Barco> barcos)
        {
            if (tamTablero < 4 || tamTablero > 9)
                throw new ArgumentException("Tamaño de tablero erroneo, debe estar entre 4 y 9");

            this.TamTablero = tamTablero;
            this.barcos = barcos;
            this.barcosEliminados = new List<Barco>();
            this.coordenadasDisparadas = new List<Coordenada>();
            this.coordenadasTocadas = new List<Coordenada>();
            this.casillasTablero = new Dictionary<Coordenada, string>();

            foreach (var barco in barcos)
            {
                barco.eventoTocado += cuandoEventoTocado;
                barco.eventoHundido += cuandoEventoHundido;
            }

            inicializaCasillasTablero();
        }

        private void inicializaCasillasTablero()
        {
            for (int fila = 0; fila < TamTablero; fila++)
            {
                for (int columna = 0; columna < TamTablero; columna++)
                {
                    Coordenada coord = new Coordenada(fila, columna);
                    casillasTablero[coord] = "[AGUA]";
                }
            }

            foreach (var barco in barcos)
            {
                foreach (var coord in barco.CoordenadasBarco.Keys)
                {
                    casillasTablero[coord] = $"[{barco.Nombre}]";
                }
            }

        }

        public void Disparar(Coordenada c)
        {
            if (coordenadasDisparadas.Contains(c))
            {
                Console.WriteLine("Coordenadas ya disparadas anteriormente");
                return;
            }

            if (c.Fila < 0 || c.Fila >= TamTablero || c.Columna < 0 || c.Columna >= TamTablero)
            {
                Console.WriteLine($"La coordnada ({c.Fila},{c.Columna}) esta fuera de el tamaño del tablero");
                return;
            }

            coordenadasDisparadas.Add(c);

            foreach (var barco in barcos)
            {
                barco.Disparo(c);
            }

        }

        public string DibujarTablero()
        {
            string tablero = "";
            for (int fila = 0; fila < TamTablero; fila++)
            {
                for (int columna = 0; columna < TamTablero; columna++)
                {
                    Coordenada coord = new Coordenada(fila, columna);
                    tablero += casillasTablero.ContainsKey(coord) ? casillasTablero[coord] + "" : "AGUA";
                }
                tablero += "\n";
            }
            return tablero;
        }

        public override string ToString()
        {
            string info = "\n";

            foreach (var barco in barcos)
            {
                info += barco.ToString() + "\n";
            }
            info += "\nCoordenadas Disparadas: " + string.Join(", ", coordenadasDisparadas) + "\n";
            info += "Coordenadas Tocadas: " + string.Join(", ", coordenadasTocadas) + "\n";
            info += "\n\nCASILLAS TABLERO\n";
            info += "---------\n";
            info += DibujarTablero();
            return info;
        }

        private void cuandoEventoTocado(object sender, TocadoArgs e)
        {
            Console.WriteLine($"TABLERO: Barco [{e.Nombre}] tocado en Coordenada: [{e.CoordenadaImpacto}]");
            if (!coordenadasTocadas.Contains(e.CoordenadaImpacto))
            {
                coordenadasTocadas.Add(e.CoordenadaImpacto);
            }
            casillasTablero[e.CoordenadaImpacto] = $"[{e.Nombre}_T]";
        }

        private void cuandoEventoHundido(object sender, HundidoArgs e)
        {
            Console.WriteLine($"TABLERO: Barco [{e.Nombre}] hundido!!");
            barcosEliminados.Add(barcos.Find(b => b.Nombre == e.Nombre));

            if (barcosEliminados.Count == barcos.Count)
            {
                eventoFinPartida?.Invoke(this, EventArgs.Empty);
            }

        }

    }
}
