using System;
using System.Net.NetworkInformation;
using System.Threading;
using SquirrelyCMD;

namespace SquirrelyCMD
{
    class Program
    {
        static string directory = "";
        static bool hasDIR = false;

        static void Main(string[] args) {
            //So there isn't a safe way to change the font size... so that's not goint to happen.
            Console.WriteLine("SquirrelyCMD (C)2016 MrSquirrely.net");
            for (; true;) {
                Console.WriteLine("");
            START:
                if (hasDIR) {
                    goto CD_START;
                }
                Console.Write(">>");
            CD_START:
                if(hasDIR != false){
                    Console.Write(directory + ">");
                }
                string enter = Console.ReadLine();
                string[] command = enter.Split(new char[] { ' ' });
                switch (command[0].ToLower()) {
                    case "cd":
                        if (command.Length > 1) {
                            if (command[1] == "...") {
                                hasDIR = false;
                                goto START;
                            }
                            directory = command[1];
                            hasDIR = true;
                            goto CD_START;
                        } else {
                            Console.WriteLine("Could not complete command");
                            goto START;
                        }
                    case "dir":
                        SquirrelyUtils.FullDirList(directory);
                        for (int i = 0; i < SquirrelyUtils.folders.Count; i++) {
                            Console.WriteLine(SquirrelyUtils.folders[i].ToString());
                        }
                        for (int i = 0; i < SquirrelyUtils.files.Count; i++) {
                            Console.WriteLine(SquirrelyUtils.files[i].ToString());
                        }
                        goto CD_START;
                    case "color":
                        if (command.Length > 1) {
                            switch (command[1].ToLower()) {
                                case "black":
                                    Console.BackgroundColor = ConsoleColor.Black;
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.Clear();
                                    goto START;
                                case "blue":
                                    Console.BackgroundColor = ConsoleColor.Blue;
                                    Console.ForegroundColor = ConsoleColor.Black;
                                    Console.Clear();
                                    goto START;
                                case "cyan":
                                    Console.BackgroundColor = ConsoleColor.Cyan;
                                    Console.ForegroundColor = ConsoleColor.Black;
                                    Console.Clear();
                                    goto START;
                                default:
                                    Console.BackgroundColor = ConsoleColor.Black;
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.Clear();
                                    goto START;
                            }
                        } else {
                            Console.WriteLine("Could not complete command");
                            goto START;
                        }
                    case "date":
                        DateTime localDate = DateTime.Now;
                        Console.WriteLine(localDate.ToString());
                        goto START;
                    case "echo":
                        if (command.Length > 1) {
                            string str = "";
                            for (int i = 1; i < command.Length; i++) {
                                string str2 = command[i];
                                str = str +" "+ str2;
                            }
                            Console.WriteLine(str);
                            goto START;
                        } else {
                            goto START;
                        }
                    case "help":
                    case "?":
                        if (command.Length > 1) {
                            switch (command[1].ToLower()) {
                                case "help":
                                    Console.WriteLine("Shows a list of all possible commands to choose from");
                                    goto START;
                                case "ping":
                                    Console.WriteLine("Pings a host to see if it is reachable"
                                                     +"\n Possible flags: "
                                                     +"\n    /A NUMBER     - Sets how many times you wish to repet command"
                                                     +"\n    /i            - Sets to repete until closed (Bug:: CTRL + C Closes app, so doesn't work to stop command)");
                                    goto START;
                                case "cls":
                                    Console.WriteLine("Clears the screen of all text. You can type 'CLS' or 'CLEAR'");
                                    goto START;
                                case "exit":
                                    Console.WriteLine("Exits the applications... Did you really need to know that?");
                                    goto START;
                                case "cd":
                                    Console.WriteLine("Changes the current directory, very primitively I will add.");
                                    goto START;
                                case "dir":
                                    Console.WriteLine("List all files in current Directory, again very primitively I add.");
                                    goto START;
                                case "color":
                                    Console.WriteLine("Changes the color of the console"
                                                     +"\n Possible colors:"
                                                     +"\n    BLACK"
                                                     +"\n    BLUE"
                                                     +"\n    CYAN");
                                    goto START;
                                case "date":
                                    Console.WriteLine("Shows the date, primitively. I see a trend here.");
                                    goto START;
                                case "echo":
                                    Console.WriteLine("Echos a string, not much you can do here.");
                                    goto START;
                                default:
                                    goto START;
                            }
                        }
                        Console.WriteLine("Add command to end to find more info on the command (Ex. help ping)");
                        Console.WriteLine("Possible Commands");
                        Console.WriteLine("\n HELP    :: Shows all the possible commands"
                                         +"\n PING    :: Pings a host to see if it is reachable"
                                         +"\n CLS     :: Clears the screen"
                                         +"\n EXIT    :: Closes SquirrelyCMD"
                                         +"\n CD      :: Changes directory"
                                         +"\n DIR     :: List all files and folders in current directory"
                                         +"\n COLOR   :: Changes color of console"
                                         +"\n DATE    :: Shows current date"
                                         +"\n ECHO    :: Echos an inputed string");
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
