using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory.Weapons
{
    public class MigMissile : IMissile
    {
        public double DamagePower { get; set; }
        public double Velocity { get; set; }
    }
}
