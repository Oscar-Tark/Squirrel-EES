using ScorpionConsoleReadWrite;

namespace Scorpion
{
    static class lineFeed
    {
        static bool read_signal_off_already_set;
        static bool message_shown;

        public static void lineReader()
        {
            List<string> history = new List<string>(0x0f); int history_current = 0;

            //Input signal Bools
            message_shown = false;
            read_signal_off_already_set = false;

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
            read_signal_off_already_set = false;
            message_shown = false;
            return;
        }
    }
}