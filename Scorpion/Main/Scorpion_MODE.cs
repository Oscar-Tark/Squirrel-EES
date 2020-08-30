using System;
namespace Scorpion.Main
{
    public class Scorpion_MODE
    {
        //SETS OPERATION MODES FOR SCORPION
        private operation_mode md;
        private struct operation_mode
        {
            public int cryptography_mode;
            bool network_only;
            bool db_only;
            bool process_only;
            bool client_only;
            bool all;
        };

        public Scorpion_MODE(int cryptography_mode)
        {
            md.cryptography_mode = cryptography_mode;
        }
    }
}
