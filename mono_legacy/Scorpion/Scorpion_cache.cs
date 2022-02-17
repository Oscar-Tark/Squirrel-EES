using System.Threading;
using System.Collections;
using LazyCache;

namespace Scorpion
{
    class Cache
    {
        //Memory structures containing the cached elements
        private ArrayList AL_CACHE_REF;
        private ArrayList AL_CACHE;

        //Default to 20s
        private int interval = 20000;
        private Scorp HANDLE;
        Timer cache_timer;

        public Cache(int interval_, Scorp HANDLE_)
        {
            HANDLE = HANDLE_;
            interval = interval_;
            cache_timer = new Timer(run_cache_worker);
            cache_timer.Change(0, interval_);
            return;
        }

        void run_cache_worker(object state)
        {
            //If our main memory structure is larger than max_value_type
            if(HANDLE.mem.AL_CURR_VAR.Count > HANDLE.mem.max_value_type)
            {

            }
            return;
        }
    }
}