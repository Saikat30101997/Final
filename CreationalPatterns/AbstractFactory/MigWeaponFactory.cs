using AbstractFactory.Fighters;
using AbstractFactory.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    public class MigWeaponFactory : IWeaponFactory
    {
        public IFighter GetFighter()
        {
            return new MigFighter();
        }

        public IMachineGun GetMachineGun()
        {
            return new MigMachineGun();
        }

        public IMissile GetMissile()
        {
            return new MigMissile();
        }
    }
}
