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

        public void bpprefferedfiat(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //*fiat
            string fiat = ((string)var_get(objects[0])).ToUpper();
            if (bp.check_FIAT(ref fiat))
            {
                bp.PREFFERED_FIAT = fiat;
                write_to_cui("Prefferred currency set to: " + fiat);
            }
            else
            {
                write_to_cui("Unable to set prefferred currencies. Available prefferred currencies are:\n");
                bp.show_FIAT();
            }

            var_dispose_internal(ref fiat);
            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }

        public void bpshowfiat(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            bp.show_FIAT();

            var_dispose_internal(ref Scorp_Line_Exec);
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

        public void bpcandles(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*return, *currency, *unit, *period, *from_date, *from_time, *to_date, *to_time
            objects.Insert(0, bp.public_URL + bp.candles + "/" + var_get(objects[1]) + "_" + bp.PREFFERED_FIAT + "?unit=" + var_get(objects[2]) + "&period=" + var_get(objects[3]) + "&from=" + var_get(objects[4]) + "T" + var_get(objects[5]) + "Z" + "&to=" + var_get(objects[6]) + "T" + var_get(objects[7]) + "Z");
            jsonget(Scorp_Line_Exec, objects);

            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);
            return;
        }

        public void bpcalculate(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Takes in a candles variable and calculates support and resistance levels
            //*return<<::*candles
            jsontovar(ref Scorp_Line_Exec, ref objects);

            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
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
        public string candles = null;
        public string PREFFERED_FIAT = null;
        private string[] FIAT = { "EUR", "USD" };

        public ScorpionTrading Scorpion_Trading_ = new ScorpionTrading();
        public ScorpionBP()
        {
            public_URL = "https://api.exchange.bitpanda.com/public/v1/";
            base_URL = "https://api.exchange.bitpanda.com/public/v1/account/";
            trades = "trades";
            currencies = "currencies";
            candles = "candlesticks";
            balances = "balances";
            deposit = "deposit";
            depositcrypto = "deposit/crypto";
        }

        public bool check_FIAT(ref string fiat)
        {
            foreach(string currency in FIAT)
            {
                if (fiat == currency)
                    return true;
            }
            return false;
        }

        public void show_FIAT()
        {
            foreach (string currency in FIAT)
                System.Console.WriteLine(currency);
            return;
        }
    }
}