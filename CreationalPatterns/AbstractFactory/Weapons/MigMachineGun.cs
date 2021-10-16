using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory.Weapons
{
    public class MigMachineGun : IMachineGun
    {
        public double DamagePower { get; set; }
        public int ShotPerRound { get; set; }
    }
}
