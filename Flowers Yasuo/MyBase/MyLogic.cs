namespace Flowers_Yasuo.MyBase
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

        internal static SpellSlot IgniteSlot { get; set; } = SpellSlot.Unknown;
        internal static SpellSlot FlashSlot { get; set; } = SpellSlot.Unknown;

        internal static Obj_AI_Hero Me { get; set; } = ObjectManager.GetLocalPlayer();

        internal static Menu Menu { get; set; }
        internal static Menu ComboMenu { get; set; }
        internal static Menu HarassMenu { get; set; }
        internal static Menu ClearMenu { get; set; }
        internal static Menu LastHitMenu { get; set; }
        internal static Menu FleeMenu { get; set; }
        internal static Menu KillStealMenu { get; set; }
        internal static Menu MiscMenu { get; set; }
        internal static Menu EvadeMenu { get; set; }
        internal static Menu DrawMenu { get; set; }

    }
}
