using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mucho_Mungus.Entities.Munguses
{
    public abstract class BaseMungus : Character
    {
        public BaseMungus(string name) : base(name)
        {
        }

        public BaseMungus(string name, Vector2 position) : base(name)
        {
            this.position = position;
        }
    }
}
