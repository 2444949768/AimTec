namespace Flowers_Library.Items
{
    #region

    using Aimtec;
    using Aimtec.SDK.Damage;
    using Aimtec.SDK.Extensions;

    using System;
    using System.Linq;

    #endregion

    public static class ItemExtensions
    {
        public static bool HasItem(this Obj_AI_Hero source, uint itemID)
        {
            if (source == null)
            {
                return false;
            }

            var slot = source.Inventory.Slots.FirstOrDefault(x => x.ItemId == itemID);
            if (slot != null && slot.SpellSlot != SpellSlot.Unknown)
            {
                return true;
            }

            return false;
        }

        public static bool HasItem(this Obj_AI_Hero source, string itemName)
        {
            if (source == null || string.IsNullOrEmpty(itemName))
            {
                return false;
            }

            var slot =
                source.Inventory.Slots.FirstOrDefault(
                    x => string.Equals(itemName, x.SpellName, StringComparison.CurrentCultureIgnoreCase));
            if (slot != null && slot.SpellSlot != SpellSlot.Unknown)
            {
                return true;
            }

            return false;
        }

        public static SpellSlot GetItemSlot(this Obj_AI_Hero source, string itemName)
        {
            if (source == null || string.IsNullOrEmpty(itemName))
            {
                return SpellSlot.Unknown;
            }

            var slot =
                source.Inventory.Slots.FirstOrDefault(
                    x => string.Equals(itemName, x.SpellName, StringComparison.CurrentCultureIgnoreCase));
            if (slot != null && slot.SpellSlot != SpellSlot.Unknown)
            {
                return slot.SpellSlot;
            }

            return SpellSlot.Unknown;
        }

        public static bool CanUseItem(this Obj_AI_Hero source, string itemName)
        {
            if (source == null || string.IsNullOrEmpty(itemName))
            {
                return false;
            }

            var slot = source.GetItemSlot(itemName);
            if (slot != SpellSlot.Unknown)
            {
                return source.SpellBook.GetSpellState(slot) == SpellState.Ready;
            }

            return false;
        }

        public static void UseItem(this Obj_AI_Hero source, Obj_AI_Hero target, string itemName)
        {
            if (source == null || target == null || !target.IsValidTarget() || string.IsNullOrEmpty(itemName))
            {
                return;
            }

            var slot = source.GetItemSlot(itemName);
            if (slot != SpellSlot.Unknown && source.CanUseItem(itemName))
            {
                source.SpellBook.CastSpell(slot, target);
            }
        }

        public static void UseItem(this Obj_AI_Hero source, Vector3 position, string itemName)
        {
            if (source == null || position == Vector3.Zero || string.IsNullOrEmpty(itemName))
            {
                return;
            }

            var slot = source.GetItemSlot(itemName);
            if (slot != SpellSlot.Unknown && source.CanUseItem(itemName))
            {
                source.SpellBook.CastSpell(slot, position);
            }
        }

        public static void UseItem(this Obj_AI_Hero source, string itemName)
        {
            if (source == null || string.IsNullOrEmpty(itemName))
            {
                return;
            }

            var slot = source.GetItemSlot(itemName);
            if (slot != SpellSlot.Unknown && source.CanUseItem(itemName))
            {
                source.SpellBook.CastSpell(slot);
            }
        }

        public static SpellSlot GetItemSlot(this Obj_AI_Hero source, uint itemID)
        {
            if (source == null)
            {
                return SpellSlot.Unknown;
            }

            var slot = source.Inventory.Slots.FirstOrDefault(x => x.ItemId == itemID);
            if (slot != null && slot.SpellSlot != SpellSlot.Unknown)
            {
                return slot.SpellSlot;
            }

            return SpellSlot.Unknown;
        }

        public static bool CanUseItem(this Obj_AI_Hero source, uint itemID)
        {
            if (source == null)
            {
                return false;
            }

            var slot = source.GetItemSlot(itemID);
            if (slot != SpellSlot.Unknown)
            {
                return source.SpellBook.GetSpellState(slot) == SpellState.Ready;
            }

            return false;
        }

        public static bool UseItem(this Obj_AI_Hero source, uint itemID, Obj_AI_Base target)
        {
            if (source == null || target == null || !target.IsValidTarget())
            {
                return false;
            }

            var slot = source.GetItemSlot(itemID);
            if (slot != SpellSlot.Unknown && source.CanUseItem(itemID))
            {
                return source.SpellBook.CastSpell(slot, target);
            }

            return false;
        }

        public static bool UseItem(this Obj_AI_Hero source, uint itemID, Vector3 position)
        {
            if (source == null || position == Vector3.Zero)
            {
                return false;
            }

            var slot = source.GetItemSlot(itemID);
            if (slot != SpellSlot.Unknown && source.CanUseItem(itemID))
            {
                return source.SpellBook.CastSpell(slot, position);
            }

            return false;
        }

        public static bool UseItem(this Obj_AI_Hero source, uint itemID)
        {
            if (source == null)
            {
                return false;
            }

            var slot = source.GetItemSlot(itemID);
            if (slot != SpellSlot.Unknown && source.CanUseItem(itemID))
            {
                return source.SpellBook.CastSpell(slot);
            }

            return false;
        }

        public static double GetItemDamage(this Obj_AI_Hero source, uint itemID, Obj_AI_Hero target)
        {
            if (!source.HasItem(itemID) || !source.CanUseItem(itemID) || 
                source.IsDead || target == null || !target.IsValidTarget())
            {
                return 0d;
            }

            switch (itemID)
            {
                case 3153: //Blade of the Ruined King 破败
                    return source.CalculateDamage(target, DamageType.Magical, 100);
                default:
                    return 0d;
            }
        }


        //科技枪 HextechGunblade - 3146
        //冰冻枪 ItemWillBoltSpellBase - 3030
        //沙漏 ZhonyasHourglass - 3157
        //冰霜女王指令 ItemWraithCollar - 3092
        //推推棒 ItemSoFBoltSpellBase - 3152
        //皇冠 shurelyascrest - 3069
        //黄色视频lv11 TrinketTotemLv1 - 3340
        //扫描 TrinketSweeperLv1 - 3341 - TrinketSweeperLvl3 - 3364
        //灯泡 TrinketOrbLvl3 - 3363
        //150水晶瓶 ItemCrystalFlask - 2031
        //猎人药水 ItemCrystalFlaskJungle - 2032
        //腐蚀药水 ItemDarkCrystalFlask - 2033
        //巫术药剂 ElixirOfSorcery - 2139
        //嗜血药剂 ElixirOfWrath - 2140
        //钢铁药剂 ElixirOfIron - 2138
        //水银腰带 QuicksilverSash - 3140
        //比尔沃特吉弯刀 BilgewaterCutlass - 3144
        //大眼石 ItemGhostWard - 2045
        //皇冠升级为眼石 ItemGhostWard - 2302
        //救赎 ItemRedemption - 3107
        //坩埚 ItemMorellosBane - 3222
        //号令之旗 ItemPromote - 3060
        //幽梦 YoumusBlade - 3142
        //破败 ItemSwordOfFeastAndFamine - 3153
        //九头蛇 ItemTiamatCleave - 3074
        //巨人九头蛇 ItemTitanicHydraCleave - 3748
        //水银弯刀 ItemMercurial - 3139
        //提亚马特 ItemTiamatCleave - 3077
        //传送门 ItemVoidGate - 3512
        //蓝盾 RanduinsOmen - 3143
        //护盾(加攻击的) ItemVeilChannel - 3814
        //石像鬼铠甲 Item3193Active - 3193
    }
}
