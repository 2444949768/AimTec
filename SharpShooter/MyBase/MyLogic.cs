﻿namespace SharpShooter.MyBase
{
    #region

    using Aimtec;
    using Aimtec.SDK.Menu;
    using Aimtec.SDK.Orbwalking;

    #endregion

    internal class MyLogic
    {
        internal static Orbwalker Orbwalker { get; set; }

        internal static Aimtec.SDK.Spell Q { get; set; }
        internal static Aimtec.SDK.Spell Q3 { get; set; }
        internal static Aimtec.SDK.Spell W { get; set; }
        internal static Aimtec.SDK.Spell E { get; set; }
        internal static Aimtec.SDK.Spell R { get; set; }
        internal static Aimtec.SDK.Spell Flash { get; set; }
        internal static Aimtec.SDK.Spell Ignite { get; set; }


        internal static SpellSlot IgniteSlot { get; set; } = SpellSlot.Unknown;
        internal static SpellSlot FlashSlot { get; set; } = SpellSlot.Unknown;

        internal static Obj_AI_Hero Me = ObjectManager.GetLocalPlayer();

        internal static readonly string[] MobsName = 
        {
            "sru_baronspawn", "sru_blue", "sru_dragon_water", "sru_dragon_fire", "sru_dragon_earth", "sru_dragon_air",
            "sru_dragon_elder", "sru_red", "sru_riftherald"
        };
    }
}
