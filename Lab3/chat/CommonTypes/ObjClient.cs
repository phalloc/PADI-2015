using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemotingSample
{

    public class ObjClient : MarshalByRefObject, IChatClient
    {
        FormRemoteGUI form = null;

        public ObjClient(FormRemoteGUI form)
        {
            this.form = form;
        }

        public bool RecvMsg(string msg)
        {
            form.Invoke(new AddMessageDel(form.UpdateForm), new Object[] { msg });

            return true;
        }
    }
}
