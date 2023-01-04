using ScorpionConsoleReadWrite;

namespace Scorpion
{
    static class lineFeed
    {
        static bool read_signal_off_already_set;
        static bool message_shown;
        static string line = string.Empty;

        public static void lineReader()
        {
            List<string> history = new List<string>(0x0f); int history_current = 0;

            //Input signal Bools
            message_shown = false;
            read_signal_off_already_set = false;

            ConsoleKeyInfo cki;

            while(true)
            {
                while(Types.READ_SIGNAL_CURRENT == Types.READ_SIGNAL_ON)
                {
                    //Read key from user
                    cki = Console.ReadKey();
                    
                    //Leave stream
                    if((cki.Modifiers & ConsoleModifiers.Control) != 0 && cki.Key == ConsoleKey.A)
                    {
                        Types.READ_SIGNAL_CURRENT = Types.READ_SIGNAL_OFF;
                        read_signal_off_already_set = true;
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

                if(Types.READ_SIGNAL_CURRENT == Types.READ_SIGNAL_OFF && read_signal_off_already_set == true)
                {
                    if(message_shown == false)
                    {
                        ConsoleWrite.writeSpecial("\n", "Exited input stream, input stream will be reinstated in 10 seconds");
                        Task.Delay(10000).ContinueWith(t => setReadSignalOn());
                        message_shown = true;
                    }
                }
            }
        }

        internal static void setReadSignalOn()
        {
            ConsoleWrite.writeSpecial("\n", "Entered input stream, press CRTL+A to leave it");
            Types.READ_SIGNAL_CURRENT = Types.READ_SIGNAL_ON;
            resetLineToEmpty();
            read_signal_off_already_set = false;
            message_shown = false;
            return;
        }

        public static string getUnreadChars()
        {
            //When the signal switches to input. Console.ReadKey still runs for one character. This function is used to take the character and return it so we can append it to the beggining of whatever input method is used in another input feed.
            return line;
        }

        public static void resetLineToEmpty()
        {
            //When the signal switches to input. Console.ReadKey still runs for one character. This function clears line so that the next execution will not contain the character.
            line = string.Empty;
            return;
        }
    }
}