using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory.Weapons
{
    public interface IMachineGun
    {
        public double DamagePower { get; set; }
        public int ShotPerRound { get; set; }
    }
}
