using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    [Serializable]
    public class MyFormatException : ApplicationException
    {
        private string problem = "Isto rebentou porque: ";

        public MyFormatException(string problem)
        {
            this.problem += problem;
        }

        public string getProblem()
        {
            return problem;
        }

         public MyFormatException(System.Runtime.Serialization.SerializationInfo info,
		    System.Runtime.Serialization.StreamingContext context)
		    : base(info, context) 
        {
		    problem = info.GetString("problem");
	    }
        
	    public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) 
        {
		    base.GetObjectData(info, context);
		    info.AddValue("problem", problem);
	    }

    }
}
