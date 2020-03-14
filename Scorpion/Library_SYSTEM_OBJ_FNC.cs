using System.Collections;
using System.Windows.Forms;

//Static Library
namespace Scorpion
{
    public partial class Librarian
    {
        public int get_limit()
        {
            return limit;
        }

        public void set_limit(int limit_)
        {
            if (limit_ < 256)
                limit = limit_;
        }
    }
}