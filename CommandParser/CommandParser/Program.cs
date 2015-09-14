using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace CommandParser
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Functional command = new Functional(Console.Out);

            command.Commands.Add('/', new string[] { "help", "?" });
            command.Commands.Add('-', new string[] { "print [value]", "k [key value]", "ping", "music [-s(space) -g(good)] ", "setuser","getuser","exit" });

            string exit = "";
            if (args.Length == 0)
                args = new string[] { "/?" };
            do
            {
                command.Process_the_command(args);
                
                Console.ForegroundColor = ConsoleColor.Yellow;             

                do
                {
                    Console.Write("{0}>", command.User);
                    exit = Console.ReadLine();

                } while (exit.Length== 0);

                Console.ForegroundColor = ConsoleColor.Green;

                args = exit.Split(new char[] { ' ' });


            } while (!exit.Equals("-exit"));

        }

         
        
        
    }
}