using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Towerdefense
{
    class Field
    {
        int x;
        int y;
        string type;
        int rotation;


        public int X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }

        public int Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }

        public string Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        public int Rotation
        {
            get
            {
                return rotation;
            }

            set
            {
                rotation = value;
            }
        }
    }
}
