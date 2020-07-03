using System.Collections;

namespace Scorpion
{
    class ScorpionTrading
    {
        public ScorpionTradingCurrencies stc = new ScorpionTradingCurrencies();
    }

    class ScorpionTradingCurrencies
    {
        private ArrayList crypto_currencies = new ArrayList();
        private ArrayList crypto_currencies_symb = new ArrayList();
        private ArrayList crypto_currencies_value = new ArrayList();

        private ArrayList fiat_currencies = new ArrayList();

        public void set_currency(string name, string symbol, float value)
        {
            crypto_currencies.Add(name);
            crypto_currencies_symb.Add(symbol);
            crypto_currencies_value.Add(value);
            return;
        }

        public string get_currency_byname(string name)
        {
            return (string)crypto_currencies_symb[crypto_currencies.IndexOf(name)];
        }
    }
}