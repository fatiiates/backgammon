using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backgammonGame
{
    public class Player
    {
        private string name;
        private string team;
        private bool order = false;

        public bool Order
        {
            get { return order; }
            set { order = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Team
        {
            get { return team; }
            set { team = value; }
        }
    }
}
