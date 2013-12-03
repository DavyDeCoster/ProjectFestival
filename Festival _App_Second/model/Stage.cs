using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festival.Model
{
    public class Stage
    {
        private String _id;

        public String ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private String _name;

        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _colour;

        public string Colour
        {
            get { return _colour; }
            set { _colour = value; }
        }
        
        
    }
}
