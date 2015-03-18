using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapNoReduce
{
    public interface IMap
    {
        //FIX ME Set<String key, String Value> Map(String key, String value)
        string Map(string s);
    }

    public class DummyMapper : IMap
    {
        public string Map(string s)
        {
            return s.ToUpper();
        }
    }
}
