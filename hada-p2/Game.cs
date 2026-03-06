using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hada_p2
{
    public class Game
    {
        private bool finPartida;
        private Tablero tablero;

        public Game()
        {
            finPartida = false;
            gameLoop();
        }

        private void gameLoop()
        {
            List<Barco> barcos = new List<Barco>
            {
                new Barco("THOR", 1, 'h', new Coordenada(0, 0)),
                new Barco("LOKI", 2, 'v', new Coordenada(1, 2)),
                new Barco("MAYA", 3, 'h', new Coordenada(3, 1))
            };

            tablero = new Tablero(9, barcos);
            tablero.eventoFinPartida += cuandoEventoFinPartida;

            while (!finPartida)
            {

                Console.WriteLine(tablero);

                Console.WriteLine("Introduce la coordenada a la que disparar FILA,COLUMNA  ('S' para salir):");
                string input = Console.ReadLine();

                if (input.ToLower() == "s")
                {
                    finPartida = true;
                    Console.WriteLine("PARTIDA FINALIZADA!!");
                    break;
                }
                string[] valores = input.Split(',');
                if (valores.Length == 2 && int.TryParse(valores[0], out int fila) && int.TryParse(valores[1], out int columna))
                {
                    Coordenada disparo = new Coordenada(fila, columna);
                    tablero.Disparar(disparo);
                }
                else
                {
                    Console.WriteLine("Formato de coordenada invalido");
                }
            }

        }

        private void cuandoEventoFinPartida(object sender, EventArgs e)
        {
            Console.WriteLine("PARTIDA FINALIZADA!!");
            finPartida = true;
        }
    }
}