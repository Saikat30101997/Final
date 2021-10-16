using AbstractFactory.Fighters;
using AbstractFactory.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    public interface IWeaponFactory 
    {
        IMissile GetMissile();
        IMachineGun GetMachineGun();
        IFighter GetFighter();
    }
}
