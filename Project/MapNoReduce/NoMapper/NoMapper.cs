using System.Collections.Generic;

namespace PADIMapNoReduce
{
    public class NoMapper : IMapper
    {
        private static long lineNumber = 0;

        public IList<KeyValuePair<string, string>> Map(string fileLine)
        {
            List<KeyValuePair<string,string>> list = new List<KeyValuePair<string, string>>();

            list.Add(new KeyValuePair<string, string> ((++lineNumber).ToString(), fileLine));

            return list;
        }
    }
}
