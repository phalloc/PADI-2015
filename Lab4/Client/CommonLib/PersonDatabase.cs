using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    public interface PersonDatabase
    {

        bool registerPerson(Person person);
        Person getPerson(int agentID);

    }
}
