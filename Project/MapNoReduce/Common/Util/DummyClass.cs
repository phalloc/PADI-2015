using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PADIMapNoReduce
{
    //used because MSVisual studio limits the usage of designer when using abstracts classes
    public class DummyClass : FormRemoteGUI
    {
        override public RichTextBox getConsoleRichTextBox()
        {
            return null;
        }

        public override void RefreshRemote()
        {
            //empty
        }
    }
}
