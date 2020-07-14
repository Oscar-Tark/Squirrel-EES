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

        public void bp_depositcrypto(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*return_variable, *currency
            ArrayList al_trade = new ArrayList { bp.base_URL + bp.depositcrypto, objects[0] };
            //for (int i = 1; i < objects.Count; i++)
            //    al_trade.Add(objects[i]);
            al_trade.Add("Authorization");
            al_trade.Add("Bearer " + bp.API_KY);
            al_trade.Add("currency");
            al_trade.Add(bp.Scorpion_Trading_.stc.get_currency_byname((string)var_get(objects[1])));

            //Do_on.write_to_cui((string)al_trade[1]);

            jsongetauth(Scorp_Line_Exec, al_trade);

            Scorp_Line_Exec = null;
            var_arraylist_dispose(ref al_trade);
            var_arraylist_dispose(ref objects);
            return;
        }

        public void bpcurrencies(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*return
            objects.Insert(0, bp.public_URL + bp.currencies);
            jsonget(Scorp_Line_Exec, objects);

            var_arraylist_dispose(ref objects);
            Scorp_Line_Exec = null;
            return;
        }
    }

    class ScorpionBP
    {
        public string public_URL;
        public string currencies = null;
        public string base_URL = "";
        public string trades = null;
        public string balances = null;
        public string deposit = null;
        public string depositcrypto = null;
        public string API_KY = null;
        public ScorpionTrading Scorpion_Trading_ = new ScorpionTrading();

        public ScorpionBP()
        {
            public_URL = "https://api.exchange.bitpanda.com/public/v1/";
            base_URL = "https://api.exchange.bitpanda.com/public/v1/account/";
            trades = "trades";
            currencies = "currencies";
            balances = "balances";
            deposit = "deposit";
            depositcrypto = "deposit/crypto";
        }
    }
}