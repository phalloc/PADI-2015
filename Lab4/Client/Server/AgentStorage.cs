using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CommonLib;

namespace Server
{
    public class AgentStorage : MarshalByRefObject, PersonDatabase
    {

        public bool registerPerson(Person person)
        {
            int agentID = person.getAgentID();

            TextWriter tw = new StreamWriter(@"Z:\Downloads\PADI-2015\Lab4\PersonDB\" + agentID + ".txt");
            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(person.GetType());
            x.Serialize(tw, person);
            Console.WriteLine("object written to file");
            tw.Close();

            return true;
        }

        public Person getPerson(int agentID)
        {
            Person fileP = new Person("John", 0, 0);
            TextReader tr = new StreamReader(@"Z:\Downloads\PADI-2015\Lab4\PersonDB\" + agentID + ".txt");
            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(fileP.GetType());
            fileP = (Person)x.Deserialize(tr);
            Console.WriteLine(fileP.toString());
            tr.Close();
            Console.ReadLine();

            return fileP;
        }

    }
}
