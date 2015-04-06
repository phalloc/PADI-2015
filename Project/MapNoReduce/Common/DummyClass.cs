using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PADIMapNoReduce
{
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
