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
            try
            {
                TextWriter tw = new StreamWriter(@"Z:\Downloads\" + agentID + ".txt");
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(person.GetType());
                x.Serialize(tw, person);
                Console.WriteLine("object written to file");
                tw.Close();
                return true;
            }
            catch (Exception e)//perguntar ao prof como encontrar erros de fromatacao de maneira decente
            {
                throw new MyFormatException(e.ToString());//nao sei o que isto escreve lol
            }
            
            
        }

        public Person getPerson(int agentID)
        {
            try{
                Person fileP = new Person("John", 0, 0);//perguntar ao prof se existe alternativa a isto
                TextReader tr = new StreamReader(@"Z:\Downloads\" + agentID + ".txt");
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(fileP.GetType());
                fileP = (Person)x.Deserialize(tr);
                Console.WriteLine(fileP.toString());
                tr.Close();
                return fileP;
     
            }
            catch (Exception e)
            {
                throw new MyFormatException(e.ToString());//nao sei o que isto escreve lol
            }

            
        }

    }
}
