namespace SharpShooter.MyCommon
{
    #region

    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.TargetSelector;
    using Aimtec.SDK.Util;
    using Aimtec.SDK.Util.Cache;

    using System.Linq;
    using System.Drawing;

    #endregion

    internal static class MyMenuExtensions
    {
        internal static Menu myMenu { get; set; }
        internal static Menu AxeMenu { get; set; }
        internal static Menu ComboMenu { get; set; }
        internal static Menu HarassMenu { get; set; }
        internal static Menu LaneClearMenu { get; set; }
        internal static Menu JungleClearMenu { get; set; }
        internal static Menu LastHitMenu { get; set; }
        internal static Menu FleeMenu { get; set; }
        internal static Menu KillStealMenu { get; set; }
        internal static Menu MiscMenu { get; set; }
        internal static Menu DrawMenu { get; set; }

        private static string heroName => ObjectManager.GetLocalPlayer().ChampionName;

        internal class AxeOption
        {
            private static Menu axeMenu => AxeMenu;

            internal static void AddMenu()
            {
                AxeMenu = new Menu(heroName + "_AxeSettings", "Axe Settings");
                myMenu.Add(AxeMenu);
            }

            internal static void AddSeperator(string name)
            {
                axeMenu.Add(new MenuSeperator("Axe" + name + heroName, name));
            }

            internal static void AddBool(string name, string defaultName, bool enabled = true)
            {
                axeMenu.Add(new MenuBool(name + heroName, defaultName, enabled));
            }

            internal static void AddSlider(string name, string defaultName, int defaultValue, int minValue, int maxValue)
            {
                axeMenu.Add(new MenuSlider(name + heroName, defaultName, defaultValue, minValue, maxValue));
            }

            internal static void AddKey(string name, string defaultName, KeyCode Keys, KeybindType type, bool enabled = false)
            {
                axeMenu.Add(new MenuKeyBind(name + heroName, defaultName, Keys, type, enabled));
            }

            internal static void AddList(string name, string defaultName, string[] values, int defaultValue = 0)
            {
                axeMenu.Add(new MenuList(name + heroName, defaultName, values, defaultValue));
            }

            internal static void AddSliderBool(string name, string defaultName, int defaultValue, int minValue,
                int maxValue, bool enabled = false)
            {
                axeMenu.Add(new MenuSliderBool(name + heroName, defaultName, enabled, defaultValue, minValue, maxValue));
            }

            internal static MenuBool GetBool(string name)
            {
                return axeMenu[name + heroName].As<MenuBool>();
            }

            internal static MenuSlider GetSlider(string name)
            {
                return axeMenu[name + heroName].As<MenuSlider>();
            }

            internal static MenuKeyBind GetKey(string name)
            {
                return axeMenu[name + heroName].As<MenuKeyBind>();
            }

            internal static MenuList GetList(string name)
            {
                return axeMenu[name + heroName].As<MenuList>();
            }

            internal static MenuSliderBool GetSliderBool(string name)
            {
                return axeMenu[name + heroName].As<MenuSliderBool>();
            }
        }

        internal class ComboOption
        {
            private static Menu comboMenu => ComboMenu;

            internal static void AddMenu()
            {
                ComboMenu = new Menu(heroName + "_ComboSettings", "Combo Settings");
                myMenu.Add(ComboMenu);
            }

            internal static bool UseQ
                =>
                    comboMenu["ComboQ" + heroName] != null &&
                    comboMenu["ComboQ" + heroName].As<MenuBool>().Enabled;

            internal static bool UseW
                =>
                    comboMenu["ComboW" + heroName] != null &&
                    comboMenu["ComboW" + heroName].As<MenuBool>().Enabled;

            internal static bool UseE
                =>
                    comboMenu["ComboE" + heroName] != null &&
                    comboMenu["ComboE" + heroName].As<MenuBool>().Enabled;

            internal static bool UseR
                =>
                    comboMenu["ComboR" + heroName] != null &&
                    comboMenu["ComboR" + heroName].As<MenuBool>().Enabled;

            internal static void AddSeperator(string name)
            {
                comboMenu.Add(new MenuSeperator("Combo" + name + heroName, name));
            }

            internal static void AddQ(bool enabled = true)
            {
                comboMenu.Add(new MenuBool("ComboQ" + heroName, "Use Q", enabled));
            }

            internal static void AddW(bool enabled = true)
            {
                comboMenu.Add(new MenuBool("ComboW" + heroName, "Use W", enabled));
            }

            internal static void AddE(bool enabled = true)
            {
                comboMenu.Add(new MenuBool("ComboE" + heroName, "Use E", enabled));
            }

            internal static void AddR(bool enabled = true)
            {
                comboMenu.Add(new MenuBool("ComboR" + heroName, "Use R", enabled));
            }

            internal static void AddBool(string name, string defaultName, bool enabled = true)
            {
                comboMenu.Add(new MenuBool(name + heroName, defaultName, enabled));
            }

            internal static void AddSlider(string name, string defaultName, int defaultValue, int minValue, int maxValue)
            {
                comboMenu.Add(new MenuSlider(name + heroName, defaultName, defaultValue, minValue, maxValue));
            }

            internal static void AddKey(string name, string defaultName, KeyCode Keys, KeybindType type, bool enabled = false)
            {
                comboMenu.Add(new MenuKeyBind(name + heroName, defaultName, Keys, type, enabled));
            }

            internal static void AddList(string name, string defaultName, string[] values, int defaultValue = 0)
            {
                comboMenu.Add(new MenuList(name + heroName, defaultName, values, defaultValue));
            }

            internal static void AddSliderBool(string name, string defaultName, int defaultValue, int minValue,
                int maxValue, bool enabled = false)
            {
                comboMenu.Add(new MenuSliderBool(name + heroName, defaultName, enabled, defaultValue, minValue, maxValue));
            }

            internal static MenuBool GetBool(string name)
            {
                return comboMenu[name + heroName].As<MenuBool>();
            }

            internal static MenuSlider GetSlider(string name)
            {
                return comboMenu[name + heroName].As<MenuSlider>();
            }

            internal static MenuKeyBind GetKey(string name)
            {
                return comboMenu[name + heroName].As<MenuKeyBind>();
            }

            internal static MenuList GetList(string name)
            {
                return comboMenu[name + heroName].As<MenuList>();
            }

            internal static MenuSliderBool GetSliderBool(string name)
            {
                return comboMenu[name + heroName].As<MenuSliderBool>();
            }
        }

        internal class HarassOption
        {
            private static Menu harassMenu => HarassMenu;

            internal static void AddMenu()
            {
                HarassMenu = new Menu(heroName + "_HarassSettings", "Harass Settings");
                myMenu.Add(HarassMenu);
            }

            internal static bool UseQ
                =>
                    harassMenu["HarassQ" + heroName] != null &&
                    harassMenu["HarassQ" + heroName].As<MenuBool>().Enabled;

            internal static bool UseW
                =>
                    harassMenu["HarassW" + heroName] != null &&
                    harassMenu["HarassW" + heroName].As<MenuBool>().Enabled;

            internal static bool UseE
                =>
                    harassMenu["HarassE" + heroName] != null &&
                    harassMenu["HarassE" + heroName].As<MenuBool>().Enabled;

            internal static bool UseR
                =>
                    harassMenu["HarassR" + heroName] != null &&
                    harassMenu["HarassR" + heroName].As<MenuBool>().Enabled;

            internal static void AddQ(bool enabled = true)
            {
                harassMenu.Add(new MenuBool("HarassQ" + heroName, "Use Q", enabled));
            }

            internal static void AddW(bool enabled = true)
            {
                harassMenu.Add(new MenuBool("HarassW" + heroName, "Use W", enabled));
            }

            internal static void AddE(bool enabled = true)
            {
                harassMenu.Add(new MenuBool("HarassE" + heroName, "Use E", enabled));
            }

            internal static void AddR(bool enabled = true)
            {
                harassMenu.Add(new MenuBool("HarassR" + heroName, "Use R", enabled));
            }

            internal static void AddTargetList()
            {
                harassMenu.Add(new MenuSeperator(heroName + "HarassListSettings", "Harass Target List"));

                foreach (var target in GameObjects.EnemyHeroes)
                {
                    if (target != null)
                    {
                        harassMenu.Add(new MenuBool("HarassList" + target.ChampionName.ToLower(), target.ChampionName));
                    }
                }
            }

            internal static bool GetHarassTargetEnabled(string name)
            {
                return harassMenu["HarassList" + name.ToLower()] != null &&
                       harassMenu["HarassList" + name.ToLower()].As<MenuBool>().Enabled;
            }

            internal static Obj_AI_Hero GetTarget(float range)
            {
                return
                    TargetSelector.GetOrderedTargets(range).
                                   FirstOrDefault(x => x.IsValidTarget(range) && GetHarassTargetEnabled(x.ChampionName));
            }

            internal static void AddMana(int defalutValue = 30)
            {
                harassMenu.Add(new MenuSlider("HarassMana" + heroName, "When Player ManaPercent >= x%", defalutValue, 1, 99));
            }

            internal static bool HasEnouguMana(bool underTurret = false)
                =>
                    ObjectManager.GetLocalPlayer().ManaPercent() >= GetSlider("HarassMana").Value &&
                    (underTurret || !ObjectManager.GetLocalPlayer().IsUnderEnemyTurret());

            internal static void AddBool(string name, string defaultName, bool enabled = true)
            {
                harassMenu.Add(new MenuBool(name + heroName, defaultName, enabled));
            }

            internal static void AddSlider(string name, string defaultName, int defaultValue, int minValue, int maxValue)
            {
                harassMenu.Add(new MenuSlider(name + heroName, defaultName, defaultValue, minValue, maxValue));
            }

            internal static void AddSliderBool(string name, string defaultName, int defaultValue, int minValue,
                int maxValue, bool enabled = false)
            {
                harassMenu.Add(new MenuSliderBool(name + heroName, defaultName, enabled, defaultValue, minValue, maxValue));
            }


            internal static void AddKey(string name, string defaultName, KeyCode Keys, KeybindType type, bool enabled = false)
            {
                harassMenu.Add(new MenuKeyBind(name + heroName, defaultName, Keys, type, enabled));
            }

            internal static void AddList(string name, string defaultName, string[] values, int defaultValue = 0)
            {
                harassMenu.Add(new MenuList(name + heroName, defaultName, values, defaultValue));
            }

            internal static MenuBool GetBool(string name)
            {
                return harassMenu[name + heroName].As<MenuBool>();
            }

            internal static MenuSlider GetSlider(string name)
            {
                return harassMenu[name + heroName].As<MenuSlider>();
            }

            internal static MenuKeyBind GetKey(string name)
            {
                return harassMenu[name + heroName].As<MenuKeyBind>();
            }

            internal static MenuList GetList(string name)
            {
                return harassMenu[name + heroName].As<MenuList>();
            }

            internal static MenuSliderBool GetSliderBool(string name)
            {
                return harassMenu[name + heroName].As<MenuSliderBool>();
            }
        }

        internal class LaneClearOption
        {
            private static Menu laneClearMenu => LaneClearMenu;

            internal static void AddMenu()
            {
                LaneClearMenu = new Menu(heroName + "_LaneClearSettings", "LaneClear Settings");
                myMenu.Add(LaneClearMenu);
            }

            internal static bool UseQ
                =>
                    laneClearMenu["LaneClearQ" + heroName] != null &&
                    laneClearMenu["LaneClearQ" + heroName].As<MenuBool>().Enabled;

            internal static bool UseW
                =>
                    laneClearMenu["LaneClearW" + heroName] != null &&
                    laneClearMenu["LaneClearW" + heroName].As<MenuBool>().Enabled;

            internal static bool UseE
                =>
                    laneClearMenu["LaneClearE" + heroName] != null &&
                    laneClearMenu["LaneClearE" + heroName].As<MenuBool>().Enabled;

            internal static bool UseR
                =>
                    laneClearMenu["LaneClearR" + heroName] != null &&
                    laneClearMenu["LaneClearR" + heroName].As<MenuBool>().Enabled;

            internal static void AddQ(bool enabled = true)
            {
                laneClearMenu.Add(new MenuBool("LaneClearQ" + heroName, "Use Q", enabled));
            }

            internal static void AddW(bool enabled = true)
            {
                laneClearMenu.Add(new MenuBool("LaneClearW" + heroName, "Use W", enabled));
            }

            internal static void AddE(bool enabled = true)
            {
                laneClearMenu.Add(new MenuBool("LaneClearE" + heroName, "Use E", enabled));
            }

            internal static void AddR(bool enabled = true)
            {
                laneClearMenu.Add(new MenuBool("LaneClearR" + heroName, "Use R", enabled));
            }

            internal static void AddMana(int defalutValue = 60)
            {
                laneClearMenu.Add(new MenuSlider("LaneClearMana" + heroName, "When Player ManaPercent >= x%", defalutValue, 1, 99));
            }

            internal static bool HasEnouguMana(bool underTurret = false)
                =>
                    ObjectManager.GetLocalPlayer().ManaPercent() >= GetSlider("LaneClearMana").Value && MyManaManager.SpellFarm &&
                    (underTurret || !ObjectManager.GetLocalPlayer().IsUnderEnemyTurret());

            internal static void AddBool(string name, string defaultName, bool enabled = true)
            {
                laneClearMenu.Add(new MenuBool(name + heroName, defaultName, enabled));
            }

            internal static void AddSlider(string name, string defaultName, int defaultValue, int minValue, int maxValue)
            {
                laneClearMenu.Add(new MenuSlider(name + heroName, defaultName, defaultValue, minValue, maxValue));
            }

            internal static void AddSliderBool(string name, string defaultName, int defaultValue, int minValue,
                int maxValue, bool enabled = false)
            {
                laneClearMenu.Add(new MenuSliderBool(name + heroName, defaultName, enabled, defaultValue, minValue, maxValue));
            }


            internal static void AddList(string name, string defaultName, string[] values, int defaultValue = 0)
            {
                laneClearMenu.Add(new MenuList(name + heroName, defaultName, values, defaultValue));
            }

            internal static MenuBool GetBool(string name)
            {
                return laneClearMenu[name + heroName].As<MenuBool>();
            }

            internal static MenuSlider GetSlider(string name)
            {
                return laneClearMenu[name + heroName].As<MenuSlider>();
            }

            internal static MenuList GetList(string name)
            {
                return laneClearMenu[name + heroName].As<MenuList>();
            }

            internal static MenuSliderBool GetSliderBool(string name)
            {
                return laneClearMenu[name + heroName].As<MenuSliderBool>();
            }
        }

        internal class JungleClearOption
        {
            private static Menu jungleClearMenu => JungleClearMenu;

            internal static void AddMenu()
            {
                JungleClearMenu = new Menu(heroName + "_JungleClearSettings", "JungleClear Settings");
                myMenu.Add(JungleClearMenu);
            }

            internal static bool UseQ
                =>
                    jungleClearMenu["JungleClearQ" + heroName] != null &&
                    jungleClearMenu["JungleClearQ" + heroName].As<MenuBool>().Enabled;

            internal static bool UseW
                =>
                    jungleClearMenu["JungleClearW" + heroName] != null &&
                    jungleClearMenu["JungleClearW" + heroName].As<MenuBool>().Enabled;

            internal static bool UseE
                =>
                    jungleClearMenu["JungleClearE" + heroName] != null &&
                    jungleClearMenu["JungleClearE" + heroName].As<MenuBool>().Enabled;

            internal static bool UseR
                =>
                    jungleClearMenu["JungleClearR" + heroName] != null &&
                    jungleClearMenu["JungleClearR" + heroName].As<MenuBool>().Enabled;

            internal static void AddQ(bool enabled = true)
            {
                jungleClearMenu.Add(new MenuBool("JungleClearQ" + heroName, "Use Q", enabled));
            }

            internal static void AddW(bool enabled = true)
            {
                jungleClearMenu.Add(new MenuBool("JungleClearW" + heroName, "Use W", enabled));
            }

            internal static void AddE(bool enabled = true)
            {
                jungleClearMenu.Add(new MenuBool("JungleClearE" + heroName, "Use E", enabled));
            }

            internal static void AddR(bool enabled = true)
            {
                jungleClearMenu.Add(new MenuBool("JungleClearR" + heroName, "Use R", enabled));
            }

            internal static void AddBool(string name, string defaultName, bool enabled = true)
            {
                jungleClearMenu.Add(new MenuBool(name + heroName, defaultName, enabled));
            }

            internal static void AddList(string name, string defaultName, string[] values, int defaultValue = 0)
            {
                jungleClearMenu.Add(new MenuList(name + heroName, defaultName, values, defaultValue));
            }

            internal static void AddMana(int defalutValue = 30)
            {
                jungleClearMenu.Add(new MenuSlider("JungleClearMana" + heroName, "When Player ManaPercent >= x%",
                    defalutValue, 1, 99));
            }

            internal static void AddSlider(string name, string defaultName, int defaultValue, int minValue, int maxValue)
            {
                jungleClearMenu.Add(new MenuSlider(name + heroName, defaultName, defaultValue, minValue, maxValue));
            }

            internal static void AddSliderBool(string name, string defaultName, int defaultValue, int minValue,
                int maxValue, bool enabled = false)
            {
                jungleClearMenu.Add(new MenuSliderBool(name + heroName, defaultName, enabled, defaultValue, minValue, maxValue));
            }

            internal static bool HasEnouguMana(bool underTurret = false)
                =>
                    ObjectManager.GetLocalPlayer().ManaPercent() >= GetSlider("JungleClearMana").Value && MyManaManager.SpellFarm &&
                    (underTurret || !ObjectManager.GetLocalPlayer().IsUnderEnemyTurret());

            internal static MenuBool GetBool(string name)
            {
                return jungleClearMenu[name + heroName].As<MenuBool>();
            }

            internal static MenuSlider GetSlider(string name)
            {
                return jungleClearMenu[name + heroName].As<MenuSlider>();
            }

            internal static MenuList GetList(string name)
            {
                return jungleClearMenu[name + heroName].As<MenuList>();
            }

            internal static MenuSliderBool GetSliderBool(string name)
            {
                return jungleClearMenu[name + heroName].As<MenuSliderBool>();
            }
        }

        internal class LastHitOption
        {
            private static Menu lastHitMenu => LastHitMenu;

            internal static void AddMenu()
            {
                LastHitMenu = new Menu(heroName + "_LastHitSettings", "LastHit Settings");
                myMenu.Add(LastHitMenu);
            }

            internal static bool HasEnouguMana
                => ObjectManager.GetLocalPlayer().ManaPercent() >= GetSlider("LastHitMana").Value && MyManaManager.SpellFarm;

            internal static bool UseQ
                =>
                    lastHitMenu["LastHitQ" + heroName] != null &&
                    lastHitMenu["LastHitQ" + heroName].As<MenuBool>().Enabled;

            internal static bool UseW
                =>
                    lastHitMenu["LastHitW" + heroName] != null &&
                    lastHitMenu["LastHitW" + heroName].As<MenuBool>().Enabled;

            internal static bool UseE
                =>
                    lastHitMenu["LastHitE" + heroName] != null &&
                    lastHitMenu["LastHitE" + heroName].As<MenuBool>().Enabled;

            internal static bool UseR
                =>
                    lastHitMenu["LastHitR" + heroName] != null &&
                    lastHitMenu["LastHitR" + heroName].As<MenuBool>().Enabled;
            internal static void AddQ(bool enabled = true)
            {
                lastHitMenu.Add(new MenuBool("LastHitQ" + heroName, "Use Q", enabled));
            }

            internal static void AddW(bool enabled = true)
            {
                lastHitMenu.Add(new MenuBool("LastHitW" + heroName, "Use W", enabled));
            }

            internal static void AddE(bool enabled = true)
            {
                lastHitMenu.Add(new MenuBool("LastHitE" + heroName, "Use E", enabled));
            }

            internal static void AddR(bool enabled = true)
            {
                lastHitMenu.Add(new MenuBool("LastHitR" + heroName, "Use R", enabled));
            }

            internal static void AddBool(string name, string defaultName, bool enabled = true)
            {
                lastHitMenu.Add(new MenuBool(name + heroName, defaultName, enabled));
            }

            internal static void AddMana(int defalutValue = 30)
            {
                lastHitMenu.Add(new MenuSlider("LastHitMana" + heroName, "When Player ManaPercent >= x%", defalutValue));
            }

            internal static void AddSlider(string name, string defaultName, int defaultValue, int minValue, int maxValue)
            {
                lastHitMenu.Add(new MenuSlider(name + heroName, defaultName, defaultValue, minValue, maxValue));
            }

            internal static void AddSliderBool(string name, string defaultName, int defaultValue, int minValue,
                int maxValue, bool enabled = false)
            {
                lastHitMenu.Add(new MenuSliderBool(name + heroName, defaultName, enabled, defaultValue, minValue, maxValue));
            }

            internal static MenuBool GetBool(string name)
            {
                return lastHitMenu[name + heroName].As<MenuBool>();
            }

            internal static MenuSlider GetSlider(string name)
            {
                return lastHitMenu[name + heroName].As<MenuSlider>();
            }

            internal static MenuList GetList(string name)
            {
                return lastHitMenu[name + heroName].As<MenuList>();
            }

            internal static MenuSliderBool GetSliderBool(string name)
            {
                return lastHitMenu[name + heroName].As<MenuSliderBool>();
            }
        }

        internal class FleeOption
        {
            private static Menu fleeMenu => FleeMenu;

            internal static void AddMenu()
            {
                FleeMenu = new Menu(heroName + "_FleeSettings", "Flee Settings");
                myMenu.Add(FleeMenu);
            }

            internal static bool DisableMove
                =>
                    fleeMenu["FleeMove" + heroName] != null &&
                    fleeMenu["FleeMove" + heroName].As<MenuBool>().Enabled;

            internal static bool UseQ
                =>
                    fleeMenu["FleeQ" + heroName] != null &&
                    fleeMenu["FleeQ" + heroName].As<MenuBool>().Enabled;

            internal static bool UseW
                =>
                    fleeMenu["FleeW" + heroName] != null &&
                    fleeMenu["FleeW" + heroName].As<MenuBool>().Enabled;

            internal static bool UseE
                =>
                    fleeMenu["FleeE" + heroName] != null &&
                    fleeMenu["FleeE" + heroName].As<MenuBool>().Enabled;

            internal static bool UseR
                =>
                    fleeMenu["FleeR" + heroName] != null &&
                    fleeMenu["FleeR" + heroName].As<MenuBool>().Enabled;

            internal static void AddQ(bool enabled = true)
            {
                fleeMenu.Add(new MenuBool("FleeQ" + heroName, "Use Q", enabled));
            }

            internal static void AddW(bool enabled = true)
            {
                fleeMenu.Add(new MenuBool("FleeW" + heroName, "Use W", enabled));
            }

            internal static void AddE(bool enabled = true)
            {
                fleeMenu.Add(new MenuBool("FleeE" + heroName, "Use E", enabled));
            }

            internal static void AddR(bool enabled = true)
            {
                fleeMenu.Add(new MenuBool("FleeR" + heroName, "Use R", enabled));
            }

            internal static void AddBool(string name, string defaultName, bool enabled = true)
            {
                fleeMenu.Add(new MenuBool(name + heroName, defaultName, enabled));
            }

            internal static MenuBool GetBool(string name)
            {
                return fleeMenu[name + heroName].As<MenuBool>();
            }
        }

        internal class KillStealOption
        {
            private static Menu killStealMenu => KillStealMenu;

            internal static bool UseQ
                =>
                    killStealMenu["KillStealQ" + heroName] != null &&
                    killStealMenu["KillStealQ" + heroName].As<MenuBool>().Enabled;

            internal static bool UseW
                =>
                    killStealMenu["KillStealW" + heroName] != null &&
                    killStealMenu["KillStealW" + heroName].As<MenuBool>().Enabled;

            internal static bool UseE
                =>
                    killStealMenu["KillStealE" + heroName] != null &&
                    killStealMenu["KillStealE" + heroName].As<MenuBool>().Enabled;

            internal static bool UseR
                =>
                    killStealMenu["KillStealR" + heroName] != null &&
                    killStealMenu["KillStealR" + heroName].As<MenuBool>().Enabled;

            internal static void AddMenu()
            {
                KillStealMenu = new Menu(heroName + "_KillStealSettings", "KillSteal Settings");
                myMenu.Add(KillStealMenu);
            }

            internal static void AddQ(bool enabled = true)
            {
                killStealMenu.Add(new MenuBool("KillStealQ" + heroName, "Use Q", enabled));
            }

            internal static void AddW(bool enabled = true)
            {
                killStealMenu.Add(new MenuBool("KillStealW" + heroName, "Use W", enabled));
            }

            internal static void AddE(bool enabled = true)
            {
                killStealMenu.Add(new MenuBool("KillStealE" + heroName, "Use E", enabled));
            }

            internal static void AddR(bool enabled = true)
            {
                killStealMenu.Add(new MenuBool("KillStealR" + heroName, "Use R", enabled));
            }

            internal static void AddTargetList()
            {
                killStealMenu.Add(new MenuSeperator(heroName + "KillStealListSettings", "KillSteal Target List"));

                foreach (var target in GameObjects.EnemyHeroes)
                {
                    if (target != null)
                    {
                        killStealMenu.Add(new MenuBool("KillStealList" + target.ChampionName.ToLower(), "Use On: " + target.ChampionName));
                    }
                }
            }

            internal static bool GetKillStealTarget(string name)
            {
                return killStealMenu["KillStealList" + name.ToLower()] != null &&
                       killStealMenu["KillStealList" + name.ToLower()].As<MenuBool>().Enabled;
            }

            internal static void AddBool(string name, string defaultName, bool enabled = true)
            {
                killStealMenu.Add(new MenuBool(name + heroName, defaultName, enabled));
            }

            internal static MenuBool GetBool(string name)
            {
                return killStealMenu[name + heroName].As<MenuBool>();
            }
        }

        internal class GapcloserOption
        {
            internal static void AddMenu()
            {
                Flowers_Library.Gapcloser.Attach(myMenu, "Gapcloser Settings");
            }
        }

        internal class MiscOption
        {
            private static Menu miscMenu => MiscMenu;

            internal static void AddMenu()
            {
                MiscMenu = new Menu(heroName + "_MiscSettings", "Misc Settings");
                myMenu.Add(MiscMenu);
            }

            internal static void AddBasic()
            {
                MyManaManager.AddFarmToMenu(miscMenu);
            }

            internal static void AddBool(string name, string defaultName, bool enabled = true)
            {
                miscMenu.Add(new MenuBool(name + heroName, defaultName, enabled));
            }

            internal static void AddSlider(string name, string defaultName, int defalueValue, int minValue = 0,
                int maxValue = 100)
            {
                miscMenu.Add(new MenuSlider(name + heroName, defaultName, defalueValue, minValue, maxValue));
            }

            internal static void AddKey(string name, string defaultName, KeyCode Keys, KeybindType type, bool enabled = false)
            {
                miscMenu.Add(new MenuKeyBind(name + heroName, defaultName, Keys, type, enabled));
            }

            internal static void AddList(string name, string defaultName, string[] values, int defaultValue = 0)
            {
                miscMenu.Add(new MenuList(name + heroName, defaultName, values, defaultValue));
            }

            internal static void AddSliderBool(string name, string defaultName, int defaultValue, int minValue,
                int maxValue, bool enabled = false)
            {
                miscMenu.Add(new MenuSliderBool(name + heroName, defaultName, enabled, defaultValue, minValue, maxValue));
            }

            internal static void AddSetting(string name)
            {
                miscMenu.Add(new MenuSeperator(heroName + "_" + name + " Settings", name + " Settings"));
            }

            internal static void AddQ()
            {
                miscMenu.Add(new MenuSeperator(heroName + "_Q Settings", "Q Settings"));
            }

            internal static void AddW()
            {
                miscMenu.Add(new MenuSeperator(heroName + "_W Settings", "W Settings"));
            }

            internal static void AddE()
            {
                miscMenu.Add(new MenuSeperator(heroName + "_E Settings", "E Settings"));
            }

            internal static void AddR()
            {
                miscMenu.Add(new MenuSeperator(heroName + "_R Settings", "R Settings"));
            }

            internal static MenuBool GetBool(string name)
            {
                return miscMenu[name + heroName].As<MenuBool>();
            }

            internal static MenuSlider GetSlider(string name)
            {
                return miscMenu[name + heroName].As<MenuSlider>();
            }

            internal static MenuKeyBind GetKey(string name)
            {
                return miscMenu[name + heroName].As<MenuKeyBind>();
            }

            internal static MenuList GetList(string name)
            {
                return miscMenu[name + heroName].As<MenuList>();
            }

            internal static MenuSliderBool GetSliderBool(string name)
            {
                return miscMenu[name + heroName].As<MenuSliderBool>();
            }
        }

        internal class DrawOption
        {
            private static float QRange;
            private static float WRange;
            private static float ERange;
            private static float RRange;
            private static float QExtendRange;

            private static Menu drawMenu => DrawMenu;

            internal static void AddMenu()
            {
                DrawMenu = new Menu(heroName + "_DrawSettings", "Draw Settings");
                myMenu.Add(DrawMenu);
            }

            internal static void SetDefalut()
            {
                QRange = 0;
                WRange = 0;
                ERange = 0;
                RRange = 0;
                QExtendRange = 0;
            }

            internal static void AddBool(string name, string defaultName, bool enabled = true)
            {
                drawMenu.Add(new MenuBool(name + heroName, defaultName, enabled));
            }

            internal static void AddKey(string name, string defaultName, KeyCode Keys, KeybindType type, bool enabled = false)
            {
                drawMenu.Add(new MenuKeyBind(name + heroName, defaultName, Keys, type, enabled));
            }


            internal static void AddSlider(string name, string defaultName, int defalueValue, int minValue = 0,
                int maxValue = 100)
            {
                drawMenu.Add(new MenuSlider(name + heroName, defaultName, defalueValue, minValue, maxValue));
            }

            internal static MenuBool GetBool(string name)
            {
                return drawMenu[name + heroName].As<MenuBool>();
            }

            internal static MenuSlider GetSlider(string name)
            {
                return drawMenu[name + heroName].As<MenuSlider>();
            }

            internal static MenuKeyBind GetKey(string name)
            {
                return drawMenu[name + heroName].As<MenuKeyBind>();
            }

            internal static MenuList GetList(string name)
            {
                return drawMenu[name + heroName].As<MenuList>();
            }

            internal static void AddList(string name, string defaultName, string[] values, int defaultValue = 0)
            {
                drawMenu.Add(new MenuList(name + heroName, defaultName, values, defaultValue));
            }

            internal static void AddQ(float Range = 0, bool enabled = false)
            {
                drawMenu.Add(new MenuBool("DrawQ" + heroName, "Draw Q Range", enabled));
                QRange = Range;
            }

            internal static void AddW(float Range = 0, bool enabled = false)
            {
                drawMenu.Add(new MenuBool("DrawW" + heroName, "Draw W Range", enabled));
                WRange = Range;
            }

            internal static void AddE(float Range = 0, bool enabled = false)
            {
                drawMenu.Add(new MenuBool("DrawE" + heroName, "Draw E Range", enabled));
                ERange = Range;
            }

            internal static void AddR(float Range = 0, bool enabled = false)
            {
                drawMenu.Add(new MenuBool("DrawR" + heroName, "Draw R Range", enabled));
                RRange = Range;
            }

            internal static void AddQExtend(float Range = 0, bool enabled = false)
            {
                drawMenu.Add(new MenuBool("DrawQExtend" + heroName, "Draw Q Extend Range", enabled));
                QExtendRange = Range;
            }

            internal static void AddFarm()
            {
                MyManaManager.AddDrawToMenu(drawMenu);
            }

            private static void DrawQ()
            {
                if (drawMenu["DrawQ" + heroName].As<MenuBool>().Enabled &&
                    ObjectManager.GetLocalPlayer().SpellBook.GetSpell(SpellSlot.Q).Level > 0 &&
                    ObjectManager.GetLocalPlayer().SpellBook.CanUseSpell(SpellSlot.Q))
                {
                    Render.Circle(ObjectManager.GetLocalPlayer().ServerPosition, QRange, 30, Color.FromArgb(19, 130, 234));
                }
            }

            private static void DrawW()
            {
                if (drawMenu["DrawW" + heroName].As<MenuBool>().Enabled &&
                    ObjectManager.GetLocalPlayer().SpellBook.GetSpell(SpellSlot.W).Level > 0 &&
                    ObjectManager.GetLocalPlayer().SpellBook.CanUseSpell(SpellSlot.W))
                {
                    Render.Circle(ObjectManager.GetLocalPlayer().ServerPosition, WRange, 30, Color.FromArgb(248, 246, 6));
                }
            }

            private static void DrawE()
            {
                if (drawMenu["DrawE" + heroName].As<MenuBool>().Enabled &&
                    ObjectManager.GetLocalPlayer().SpellBook.GetSpell(SpellSlot.E).Level > 0 &&
                    ObjectManager.GetLocalPlayer().SpellBook.CanUseSpell(SpellSlot.E))
                {
                    Render.Circle(ObjectManager.GetLocalPlayer().ServerPosition, ERange, 30, Color.FromArgb(188, 6, 248));
                }
            }

            private static void DrawR()
            {
                if (drawMenu["DrawR" + heroName].As<MenuBool>().Enabled &&
                    ObjectManager.GetLocalPlayer().SpellBook.GetSpell(SpellSlot.R).Level > 0 &&
                    ObjectManager.GetLocalPlayer().SpellBook.CanUseSpell(SpellSlot.R))
                {
                    Render.Circle(ObjectManager.GetLocalPlayer().ServerPosition, RRange, 30, Color.Red);
                }
            }

            private static void DrawQExtend()
            {
                if (drawMenu["DrawQExtend" + heroName].As<MenuBool>().Enabled &&
                    ObjectManager.GetLocalPlayer().SpellBook.GetSpell(SpellSlot.Q).Level > 0 &&
                    ObjectManager.GetLocalPlayer().SpellBook.CanUseSpell(SpellSlot.Q))
                {
                    Render.Circle(ObjectManager.GetLocalPlayer().ServerPosition, QExtendRange, 30, Color.FromArgb(0, 255, 161));
                }
            }

            internal static void AddEvent()
            {
                Render.OnRender += delegate
                {
                    if (ObjectManager.GetLocalPlayer().IsDead || MenuGUI.IsChatOpen() || MenuGUI.IsShopOpen())
                    {
                        return;
                    }

                    if (QRange > 0)
                    {
                        DrawQ();
                    }

                    if (WRange > 0)
                    {
                        DrawW();
                    }

                    if (ERange > 0)
                    {
                        DrawE();
                    }

                    if (RRange > 0)
                    {
                        DrawR();
                    }

                    if (QExtendRange > 0)
                    {
                        DrawQExtend();
                    }
                };
            }
        }
    }
}