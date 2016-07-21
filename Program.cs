using System;
using System.Net.NetworkInformation;
using System.Threading;

namespace SquirrelyCMD
{
    class Program
    {
        static void Main(string[] args) {

            Console.WriteLine("SquirrelyCMD (C)2016 MrSquirrely.net");
            for (; true;) {
                Console.WriteLine("");
            START:
                Console.Write(">>");
                string enter = Console.ReadLine();
                string[] command = enter.Split(new char[] { ' ' });
                switch (command[0].ToLower()) {
                    case "help":
                    case "?":
                        Console.WriteLine("Possible Commands");
                        Console.WriteLine(" HELP :: Shows all the possible commands"
                                         +"\n PING :: Pings a host to see if it is reachable"
                                         +"\n CLS :: Clears the screen"
                                         +"\n EXIT :: Closes SquirrelyCMD");
                        goto START;
                    case "ping":
                        if (command.Length > 1) {
                            switch (command[1].ToLower()) {
                                case "":
                                    Console.WriteLine("Could not complete command");
                                    break;
                                default:
                                    bool success = true;
                                    Ping pinger = new Ping();
                                    int numberTries = 4;
                                    if (command[2] != null) {
                                        string number = command[2].ToLower();
                                        if (number != "/t") {
                                            try {
                                                numberTries = Int32.Parse(number.Substring(1));
                                            } catch {

                                            }
                                            
                                        }else {
                                            numberTries = int.MaxValue;
                                        }                              
                                    }
                                    try {
                                        PingReply reply = pinger.Send(command[1].ToLower());
                                        Console.WriteLine("Pinging " + command[1].ToLower() + "[" + reply.Address.ToString() + "]");
                                        for (int i = 0; i < numberTries; i++) {
                                            Console.WriteLine(reply.Status.ToString() + " " + reply.RoundtripTime.ToString() + "ms");
                                            Thread.Sleep(1000);
                                        }
                                    } catch (PingException) {
                                        success = false;
                                    }
                                    if (!success) {
                                        Console.WriteLine("Could not complete command");
                                    }
                                    break;
                            }
                        }
                        break;
                    case "cls":
                    case "clear":
                        Console.Clear();
                        goto START;
                    case "exit":
                        Environment.Exit(0);
                        break;
                    case "":
                        goto START;
                    default:
                        Console.WriteLine("Unkown command, type in '?' to see a list of possible commands");
                        goto START;
                }
            }

        }
    }
}
