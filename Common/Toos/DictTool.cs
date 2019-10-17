using System.Collections.Generic;

namespace Common.Toos
{
    public class DictTool
    {
        //定义一个静态函数,传入key,就能取出Dict中相应的值
        public static T2 GetValue<T1, T2>(Dictionary<T1, T2> dict, T1 key)
        {
            T2 value;
            bool ok = dict.TryGetValue(key, out value);
            if (ok)
            {
                return value;
            }
            else
            {
                return default(T2);
            }
        }
    }
}