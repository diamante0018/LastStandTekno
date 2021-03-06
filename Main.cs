﻿using InfinityScript;
using System;

namespace LastStandTekno
{
    public class Main : BaseScript
    {
        private int lastPlayer;
        public Main()
        {
            Notified += ISTest_Notified;
            PlayerConnected += OnPlayerConnected;
            Log.Write(LogLevel.Info, "Loaded Last Stand DLL made by Diavolo#6969 1.1");
        }

        public void OnPlayerConnected(Entity player)
        {
            player.OnNotify("weapon_change", (ent, weapon) => OnWeaponChange(ent, weapon.As<string>()));
        }

        public void OnWeaponChange(Entity player, string weapon)
        {
            if (weapon.Equals("c4death_mp", StringComparison.InvariantCultureIgnoreCase))
            {
                AfterDelay(500, () =>
                {
                    player.TakeAllWeapons();
                    player.GiveWeapon("iw5_usp45_mp");
                    player.Call("SetWeaponAmmoClip", "iw5_usp45_mp", 0);
                    player.Call("SetWeaponAmmoStock", "iw5_usp45_mp", 0);
                });
            }
        }

        private void ISTest_Notified(string arg1, Parameter[] arg2)
        {
            if (arg1.Equals("player_last_stand", StringComparison.InvariantCultureIgnoreCase))
            {
                Entity player = Entity.GetEntity(lastPlayer);

                player.TakeAllWeapons();
                player.GiveWeapon("iw5_barrett_mp");
                player.SwitchToWeaponImmediate("iw5_barrett_mp");
                player.Call("SetWeaponAmmoClip", "iw5_barrett_mp", 0);
                player.Call("SetWeaponAmmoStock", "iw5_barrett_mp", 0);
            }
        }

        public override void OnPlayerDamage(Entity player, Entity inflictor, Entity attacker, int damage, int dFlags, string mod, string weapon, Vector3 point, Vector3 dir, string hitLoc) => lastPlayer = player.EntRef;
    }
}