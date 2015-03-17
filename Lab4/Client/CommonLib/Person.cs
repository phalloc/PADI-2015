using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    public class Person
    {
        public string _name;
        public int _age;
        public int _agentID;

        public Person() { }

        public Person(string name, int age, int agentID)
        {
            _name = name;
            _age = age;
            _agentID = agentID;
        }

        public string getName()
        {
            return _name;
        }

        public int getAge()
        {
            return _age;
        }

        public int getAgentID()
        {
            return _agentID;
        }

        public string toString()
        {
            return "The agent in the file is called " + getName() + " is " +
                getAge() + " years old and his codename is Agent " + getAgentID() + ".";
        }


    }
}
