using AbstractFactory.Fighters;
using AbstractFactory.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    public class F16WeaponFactory : IWeaponFactory
    {
        public IFighter GetFighter()
        {
            return new F16Fighter();
        }

        public IMachineGun GetMachineGun()
        {
            return new F16MachineGun();
        }

        public IMissile GetMissile()
        {
            return new F16Missile();
        }
    }
}
