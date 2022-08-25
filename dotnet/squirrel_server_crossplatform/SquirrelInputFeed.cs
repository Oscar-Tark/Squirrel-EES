/*  <Scorpion Server>
    Copyright (C) <2022+>  <Oscar Arjun Singh Tark>

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License as
    published by the Free Software Foundation, either version 3 of the
    License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Affero General Public License for more details.

    You should have received a copy of the GNU Affero General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Reflection;
using System.Threading;
using System.IO;
using System.Security;
using ScorpionConsoleReadWrite;

namespace Scorpion
{
    static class lineFeed
    {

        public static void lineReader()
        {
            List<string> history = new List<string>(0x0f); int history_current = 0;
            bool message_shown = false;
            string line = default;
            ConsoleKeyInfo cki;

            while(true)
            {
                while(Types.READ_SIGNAL_CURRENT == Types.READ_SIGNAL_ON)
                {
                    cki = Console.ReadKey();
                    
                    //Leave stream
                    if((cki.Modifiers & ConsoleModifiers.Control) != 0 && cki.Key == ConsoleKey.A)
                    {
                        Types.READ_SIGNAL_CURRENT = Types.READ_SIGNAL_OFF;
                        break;
                    }
                    //Backspace
                    else if (cki.Key == ConsoleKey.Backspace && line.Length > 0)
                    {
                        Console.Write("\b \b");
                        line = line.Remove(Console.CursorLeft);
                    }
                    else if(cki.Key == ConsoleKey.LeftArrow && Console.CursorLeft > 0)
                    {
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    }
                    else if(cki.Key == ConsoleKey.RightArrow && Console.CursorLeft < line.Length && line.Length > 0)
                    {
                        Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
                    }
                    else if(cki.Key == ConsoleKey.UpArrow && history_current < history.Count)
                    {
                        if(history_current == -1)
                            history_current = 0;

                        Console.Write("\r{0}", history[history_current]);
                        line = history[history_current];
                        history_current++;
                    }
                    else if(cki.Key == ConsoleKey.DownArrow && history_current > -1)
                    {
                        history_current--;
                        if(history_current == -1)
                        {
                            Console.SetCursorPosition(0, Console.CursorTop);
                            Console.Write(new string(' ', Console.BufferWidth)); 
                            Console.Write("\r{0}", "");
                            line = "";
                        }
                        else
                        {
                            Console.Write("\r{0}", history[history_current]);
                            line = history[history_current];
                        }
                    }
                    else if(cki.Key == ConsoleKey.PageUp)
                    {
                        Console.SetCursorPosition(0, Console.CursorTop);
                    }
                    else if(cki.Key == ConsoleKey.PageDown && line.Length > 0)
                    {
                        Console.SetCursorPosition(line.Length, Console.CursorTop);
                    }
                    //Enter
                    else if(cki.Key == ConsoleKey.Enter)
                    {
                        Types.HANDLE.librarian_instance.librarian.scorpioniee(line);
                        history.Add(line);
                        line = Types.S_NULL;
                    }
                    else if(char.IsLetter(cki.KeyChar) || char.IsDigit(cki.KeyChar) || char.IsSymbol(cki.KeyChar) || char.IsPunctuation(cki.KeyChar))
                        line = line + cki.KeyChar;
                }

                if(Types.READ_SIGNAL_CURRENT == Types.READ_SIGNAL_OFF)
                {
                    if(message_shown == false)
                    {
                        ConsoleWrite.writeSpecial("\n", "Exited input stream, input stream will be reinstated in 10 seconds");
                        Task.Delay(10000).ContinueWith(t=> setReadSignalOn());
                        message_shown = true;
                    }
                }
            }
        }

        internal static void setReadSignalOn()
        {
            ConsoleWrite.writeSpecial("\n", "Entered input stream, press CRTL+A to leave it");
            Types.READ_SIGNAL_CURRENT = Types.READ_SIGNAL_ON;
            return;
        }
    }
}