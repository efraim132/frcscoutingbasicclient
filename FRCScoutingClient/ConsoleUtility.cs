using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRCScoutingClient {
    class ConsoleUtility {


        //I like colors that's why this is a thing
        public static void writeColor(object data, ConsoleColor dataColor) {
            Console.ForegroundColor = dataColor;
            Console.Write(data);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        
        public static void writeStatus(string Message, object data, ConsoleColor messageColor, ConsoleColor dataColor) {
            Console.ForegroundColor = messageColor;
            Console.Write(Message + ": ");
            Console.ForegroundColor = dataColor;
            Console.Write(data);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        public static void ConsoleSpin(int Seconds, ConsoleColor Color) {
            ConsoleSpiner spin = new ConsoleSpiner();
            //times 5 because 200 milliseconds is 1/5 of a second
            Console.ForegroundColor = Color;
            int Cycles = Seconds * 5;
            for( int i = Cycles; i >= 0; i-- ) {
                spin.Turn();
                System.Threading.Thread.Sleep(200);
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            Console.WriteLine(" Done!");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        
        }
        public class ConsoleSpiner {
            int counter;
            public ConsoleSpiner() {
                counter = 0;
            }
            public void Turn() {

                counter++;
                switch( counter % 4 ) {
                    case 0: Console.Write("/"); break;
                    case 1: Console.Write("-"); break;
                    case 2: Console.Write("\\"); break;
                    case 3: Console.Write("|"); break;
                }
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            }
        
        }
    }

