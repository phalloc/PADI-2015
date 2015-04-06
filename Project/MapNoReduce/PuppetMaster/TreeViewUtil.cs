using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;


namespace PADIMapNoReduce
{

    //from http://www.codeproject.com/Articles/12606/Loading-and-Saving-XML-to-and-from-a-TreeView-Cont
    class TreeViewUtil
    {
        private XmlTextWriter xr;

        public void exportToXml(TreeView tv, string filename)
        {
            xr = new XmlTextWriter(filename, System.Text.Encoding.UTF8);
            xr.WriteStartDocument();
            //Write our root node
            xr.WriteStartElement(tv.Nodes[0].Text);
            foreach (TreeNode node in tv.Nodes)
            {
                saveNode2(node.Nodes);
            }
            //Close the root node
            xr.WriteEndElement();
            xr.Close();
        }

        private void saveNode2(TreeNodeCollection tnc)
        {
            foreach (TreeNode node in tnc)
            {
                //If we have child nodes, we'll write 
                //a parent node, then iterrate through
                //the children
                if (node.Nodes.Count > 0)
                {
                    xr.WriteStartElement(node.Text);
                    saveNode2(node.Nodes);
                    xr.WriteEndElement();
                }
                else //No child nodes, so we just write the text
                {
                    xr.WriteString(node.Text);
                }
            }
        }
    }
}
