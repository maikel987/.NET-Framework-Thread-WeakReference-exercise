using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp45
{
    public class CashManager
    {
        private int StrongCount;
        public Dictionary<int,object> Strong { get; set; }

        private int WeakCount;
        //static public Dictionary<int, object> Weak { get; set; }
        WeakReference Weak { get; set; }

        public CashManager(int MaxStrong,int MaxWeak)
        {
            StrongCount = MaxStrong;
            WeakCount = MaxWeak;
            Weak = new WeakReference(new Dictionary<int, object>());
            Strong = new Dictionary<int, object>();
        }

        public bool Add(int id,object obj)
        {
            if (StrongCount > 0)
            {
                Strong.Add(id, obj);
                StrongCount--;
                return true;
            }
            else if (WeakCount > 0)
            {
                Dictionary<int, object> DicTmp = (Dictionary<int, object>)Weak.Target;
                DicTmp.Add(id, obj);
                Weak = new WeakReference(DicTmp);
                WeakCount--;
                return true;
            }
            return false;
        }

        public bool GetAndRemove(int id, out object obj)
        {
            obj = null;
            object tmp = null;
            if (Strong.FirstOrDefault(c=>c.Key == id).Value != null) tmp = Strong[id];
            

            if (tmp == null)
            {
                Dictionary<int, object> DicTmp = (Dictionary<int, object>)Weak.Target;
                if (DicTmp == null) return false;
                object tmp2 = null;
                if (DicTmp.FirstOrDefault(c => c.Key == id).Value != null) tmp2 = DicTmp[id];

                if (tmp2 == null) return false;
                else
                {
                    obj = tmp2;
                    DicTmp.Remove(id);
                    WeakCount++;
                    return true;
                }
            }
            else
            {
                Strong.Remove(id);
                obj = tmp;
                StrongCount++;
                return true;
            }



        }
    }
}
