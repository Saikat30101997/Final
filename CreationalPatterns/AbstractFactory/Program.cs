using AbstractFactory.Fighters;
using System;

namespace AbstractFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            var fighterName = Console.ReadLine();

            var weaponFactory = GetWeaponFactory(fighterName);
            
            IFighter fighter = weaponFactory.GetFighter();
            fighter.Missile = weaponFactory.GetMissile();
            fighter.Gun = weaponFactory.GetMachineGun();
        }

        static IWeaponFactory GetWeaponFactory(string fighterName)
        {
            if (fighterName == "Mig")
            {
                return new MigWeaponFactory();
            }
            else
            {
                return new F16WeaponFactory();
            }
        }
    }
}
