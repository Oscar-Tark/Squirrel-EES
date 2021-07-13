using System.Collections;

namespace Scorpion
{
    class Cache
    {
        ArrayList AL_CACHE; // {*var, DateTime}
        ArrayList AL_CACHE_REF;

        int maxsize = 0;
        //Denote if cache should be used or not //true=on, false=off
        bool onoff = false;
        bool paging = false;

        public Cache(int max_size, bool on_off, bool paging_)
        {
            maxsize = max_size;
            onoff = on_off;
            paging = paging_;

            if(onoff)
            {
                AL_CACHE = new ArrayList(maxsize);
                AL_CACHE_REF = new ArrayList(maxsize);
            }
        }

        public void setCache(object ref_, object var)
        { 
            if(!AL_CACHE_REF.Contains(ref_))
            { 
                lock(AL_CACHE) lock(AL_CACHE_REF)
                    {
                        
                    }
            }
        }

        private void purgeCache()
        { 
        
        }

        private void setPageCache()
        { 
        
        }

        private void getPageCache()
        { 
        
        }
    }
}