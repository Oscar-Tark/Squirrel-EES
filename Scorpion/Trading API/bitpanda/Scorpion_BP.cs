using System.Collections;

namespace Scorpion
{ 
    public partial class Librarian
    {
        private ScorpionBP bp = new ScorpionBP();

        public void bpkey(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*key
            bp.API_KY = (string)var_get(objects[0]);
            Scorp_Line_Exec = null;
            var_arraylist_dispose(ref objects);
            return;
        }

        public void bpbalances(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*return_variable
            ArrayList al_trade = new ArrayList { bp.base_URL + bp.balances, objects[0] };
            //for (int i = 1; i < objects.Count; i++)
            //    al_trade.Add(objects[i]);
            al_trade.Add("Authorization");
            al_trade.Add("Bearer " + bp.API_KY);

            //Do_on.write_to_cui((string)al_trade[1]);

            jsongetauth(Scorp_Line_Exec, al_trade);

            Scorp_Line_Exec = null;
            var_arraylist_dispose(ref al_trade);
            var_arraylist_dispose(ref objects);
            return;
        }
    }

    class ScorpionBP
    {
        public string base_URL = "";
        public string trades = null;
        public string balances = null;
        public string API_KY = null;

        public ScorpionBP()
        {
            base_URL = "https://api.exchange.bitpanda.com/public/v1/account/";
            trades = "trades";
            balances = "balances";
        }
    }
}