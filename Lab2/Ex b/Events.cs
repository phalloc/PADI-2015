using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    public delegate void MoveEventHandler(object source, MoveEventArgs e);
       
    public class MoveEventArgs : EventArgs {
        public int value;
        public MoveEventArgs(int v) {
            this.value = v;
        }
    }

    class Slider {
        public event MoveEventHandler E; //list of events

	    private int position;
	    public int Position {
		    get { return position; }
	        set {
                //launch event
                if (E != null) {
                    E(this, new MoveEventArgs(value));
                }
            
            
            }
	    }
    }


    class Form {
	    static void Main( ) {
		    Slider slider = new Slider();

            Form form = new Form();

            slider.E += new MoveEventHandler(form.slider_Move_callback); 

	        slider.Position = 20;
		    slider.Position = 60;

            Console.ReadLine();
	    }
        
	    void slider_Move_callback(object source, MoveEventArgs e) {
            Console.WriteLine("Event!: {0}", e.value);
	    }
    }

}
