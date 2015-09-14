using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CommandParser
{
    class Functional
    {
        public Dictionary<char, string[]> Commands;

        private Random rand;

        private TextWriter Output;

        public string User{get; private set;}

        public Functional(TextWriter output)
        {
            User = "unnown";
            Commands = new Dictionary<char, string[]>();
            rand = new Random();
            Output = output;
        }

        private void SetUser(string name)
        {
            User = name;
        }

        private void Get_All_Command()
        {
            foreach (var key in Commands)
                foreach (var item in key.Value)
                    Output.WriteLine("\t{0}{1}",key.Key,item);
        }

        private void Ping(int frequency, int duration)
        {
            Output.Write(">Ping");
            do
            {
                Output.Write('g');
                Console.Beep(frequency, duration);

            } while (!Console.KeyAvailable);
            Output.Write('\n');            
        }

        private void SpaceMusic()
        {
            for (int i = 0; i < 15; i++)
            {

                int Note = rand.Next(140, 500);

                int Pause = rand.Next(150, 300);

                Console.Beep(Note, Pause);
            }
        }

        private void GoodMusic()
        {
            for (int i = 0; i < 2; i++)
            {
                Console.Beep(247, 400);
                Console.Beep(220, 400);
                Console.Beep(196, 400);
                Console.Beep(220, 400);
                Console.Beep(247, 400);
                Console.Beep(247, 400);
                Console.Beep(247, 800);
                Console.Beep(220, 400);
                Console.Beep(220, 400);
                Console.Beep(220, 800);
                Console.Beep(247, 400);
                Console.Beep(294, 400);
                Console.Beep(294, 800);
            }

        }

        private int Print(string[] args, int current)
        {
            if (args.Length > current + 1)
                Output.WriteLine("> {0}> <{1}>", new string('-', 15), args[++current]);
            else
                Output.WriteLine("> {1}> <without message>", new string('-', 15));
           
            return current;
        }

        private int Command_K(string[] args, int current)
        {
            if (args.Length > current + 1)
                while (current < args.Length - 1 && Regex.IsMatch(args[current + 1], @"^[a-z0-9]"))
                {
                    current++;
                    Output.WriteLine("> {0} - {1}", args[current], (current + 1 < args.Length ? (Regex.IsMatch(args[current + 1], @"^[a-z0-9]") ? args[++current] : "null") : "null"));
                }
            else
                Output.WriteLine(">Error param with command <-k> turn <key> and <value>");

            return current;
        }

        public void Process_the_command(string [] args)
        {
            if (args.Length != 1 || !args[0].Equals(""))
            {
                bool once = true;
                for (int i = 0; i < args.Length; i++)
                    if (args[i].Length == 0) continue;
                    else
                        if (args[i].StartsWith(@"/") || args[i].StartsWith(@"-"))
                            switch (Regex.Replace(args[i].ToLower(), @"[^\w\?]", ""))
                            {
                                case "?":
                                case "help":
                                    if (once)
                                        Get_All_Command();
                                    once = false;
                                    break;
                                case "print":
                                    i = Print(args, i);
                                    break;

                                case "ping":
                                    Ping(800, 500);
                                    break;
                                case "music":
                                    if (args.Length > i + 1)
                                    {
                                        if (args[i + 1].StartsWith("-s"))
                                        { SpaceMusic(); i++; }
                                        else if (args[i + 1].StartsWith("-g"))
                                        { GoodMusic(); i++; }
                                    }
                                    else
                                        Output.WriteLine(">Error param with command <-music> turn <-g> or <-s>");

                                    break;
                                case "k":
                                    i = Command_K(args, i);
                                    break;
                                case "setuser":
                                    if (args.Length > i + 1)
                                        User = args[++i];                                    
                                    break;
                                case "getuser":
                                    Output.WriteLine("Current user --- <{0}>", User);
                                    break;

                                default:
                                    Output.WriteLine("> Command <{0}> not supported use </?> to see set of allowed commands", args[i]);
                                    break;
                            }
                        else
                            Output.WriteLine("> Command <{0}> not supported use </?> to see set of allowed commands", args[i]);
            }
            else
                Output.WriteLine("> Command not correct  use </?> to see set of allowed commands");

        }

    }
}
