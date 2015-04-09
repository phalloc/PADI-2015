using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PADIMapNoReduce
{
    public interface IMapper
    {
        //FIX ME Remove, used just for testing purposes
        string MapDummy(string s);
        IList<KeyValuePair<string, string>> Map(string fileLine);
    }

    public class DummyMapper : IMapper
    {
        public IList<KeyValuePair<string, string>> Map(string fileLine)
        {
            IList<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();

            KeyValuePair<string, string> pair = new KeyValuePair<string, string>(fileLine.ToUpper(), fileLine.ToLower());

            result.Add(pair);


            return result;
        }

        public string MapDummy(string s)
        {
            return s.ToUpper();
        }
    }
}
