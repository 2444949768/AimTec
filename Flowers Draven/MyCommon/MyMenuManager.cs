namespace Flowers_Draven.MyCommon
{
    #region 

    using Aimtec.SDK.Menu;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Util.Cache;

    using Flowers_Draven.MyBase;

    using System;

    #endregion

    internal class MyMenuManager
    {
        internal static void Initializer()
        {
            try
            {
                MyLogic.Menu = new Menu("FlowersDraven", "Flowers Draven", true)
                {
                    new MenuSeperator("CreditName", "Credit: NightMoon")
                };
                MyLogic.Menu.Attach();

                MyLogic.Orbwalker = new Aimtec.SDK.Orbwalking.Orbwalker();
                MyLogic.Orbwalker.Attach(MyLogic.Menu);

                MyLogic.AxeMenu = MyLogic.Menu.Add(new Menu("FlowersDraven.AxeMenu", ":: Axe Settings"));
                {
                    MyLogic.AxeMenu.Add(new MenuList("FlowersDraven.AxeMenu.CatchMode", "Catch Axe Mode: ",
                        new[] {"All", "Only Combo", "Off"}, 0));
                    MyLogic.AxeMenu.Add(new MenuSlider("FlowersDraven.AxeMenu.CatchRange",
                        "Catch Axe Range(Cursor center)", 2000, 180, 3000));
                    MyLogic.AxeMenu.Add(new MenuSlider("FlowersDraven.AxeMenu.CatchCount", "Max Axe Count <= x", 2, 1, 3));
                    MyLogic.AxeMenu.Add(new MenuBool("FlowersDraven.AxeMenu.CatchWSpeed", "Use W| When Axe Too Far"));
                    MyLogic.AxeMenu.Add(new MenuBool("FlowersDraven.AxeMenu.NotCatchKS", "Dont Catch| If Target Can KillAble(1-3 AA)"));
                    MyLogic.AxeMenu.Add(new MenuBool("FlowersDraven.AxeMenu.NotCatchTurret",
                        "Dont Catch| If Axe Under Enemy Turret"));
                    MyLogic.AxeMenu.Add(new MenuSliderBool("FlowersDraven.AxeMenu.NotCatchMoreEnemy",
                        "Dont Catch| If Enemy Count >= x", true, 3, 1, 5));
                    MyLogic.AxeMenu.Add(new MenuBool("FlowersDraven.AxeMenu.CancelCatch", "Enabled Cancel Catch Axe Key"));
                    var cancel = MyLogic.AxeMenu.Add(new MenuKeyBind("FlowersDraven.AxeMenu.CancelKey1", "Cancel Catch Key 1",
                        Aimtec.SDK.Util.KeyCode.G, KeybindType.Press));
                    MyLogic.AxeMenu.Add(new MenuBool("FlowersDraven.AxeMenu.CancelKey2",
                        "Cancel Catch Key 2(is right click)"));
                    MyLogic.AxeMenu.Add(new MenuBool("FlowersDraven.AxeMenu.CancelKey3",
                        "Cancel Catch Key 3(is mouse scroll)", false));

                    cancel.OnValueChanged += MyEventManager.OnCancelValueChange;
                }

                MyLogic.ComboMenu = MyLogic.Menu.Add(new Menu("FlowersDraven.ComboMenu", ":: Combo Settings"));
                {
                    MyLogic.ComboMenu.Add(new MenuSeperator("FlowersDraven.ComboMenu.QSettings", "-- Q Settings"));
                    MyLogic.ComboMenu.Add(new MenuBool("FlowersDraven.ComboMenu.Q", "Use Q"));

                    MyLogic.ComboMenu.Add(new MenuSeperator("FlowersDraven.ComboMenu.WSettings", "-- W Settings"));
                    MyLogic.ComboMenu.Add(new MenuBool("FlowersDraven.ComboMenu.W", "Use W"));

                    MyLogic.ComboMenu.Add(new MenuSeperator("FlowersDraven.ComboMenu.ESettings", "-- E Settings"));
                    MyLogic.ComboMenu.Add(new MenuBool("FlowersDraven.ComboMenu.E", "Use E"));

                    MyLogic.ComboMenu.Add(new MenuSeperator("FlowersDraven.ComboMenu.RSettings", "-- R Settings"));
                    MyLogic.ComboMenu.Add(new MenuBool("FlowersDraven.ComboMenu.RSolo", "Use R| Solo Ks Mode"));
                    MyLogic.ComboMenu.Add(new MenuBool("FlowersDraven.ComboMenu.RTeam", "Use R| Team Fight"));
                }

                MyLogic.HarassMenu = MyLogic.Menu.Add(new Menu("FlowersDraven.HarassMenu", ":: Harass Settings"));
                {
                    MyLogic.HarassMenu.Add(new MenuSeperator("FlowersDraven.HarassMenu.QSettings", "-- Q Settings"));
                    MyLogic.HarassMenu.Add(new MenuBool("FlowersDraven.HarassMenu.Q", "Use Q"));

                    MyLogic.HarassMenu.Add(new MenuSeperator("FlowersDraven.HarassMenu.ESettings", "-- E Settings"));
                    MyLogic.HarassMenu.Add(new MenuBool("FlowersDraven.HarassMenu.E", "Use E", false));

                    MyLogic.HarassMenu.Add(new MenuSeperator("FlowersDraven.HarassMenu.ManaSettings", "-- Mana Settings"));
                    MyLogic.HarassMenu.Add(new MenuSlider("FlowersDraven.HarassMenu.Mana",
                        "When Player ManaPercent >= x%", 60, 1, 99));
                }

                MyLogic.ClearMenu = MyLogic.Menu.Add(new Menu("FlowersDraven.ClearMenu", ":: Clear Settings"));
                {
                    MyLogic.ClearMenu.Add(new MenuSeperator("FlowersDraven.ClearMenu.LaneClearSettings", "-- LaneClear Settings"));
                    MyLogic.ClearMenu.Add(new MenuBool("FlowersDraven.ClearMenu.LaneClearQ", "Use Q"));
                    MyLogic.ClearMenu.Add(new MenuSlider("FlowersDraven.ClearMenu.LaneClearMana",
                        "When Player ManaPercent >= x%", 30, 1, 99));

                    MyLogic.ClearMenu.Add(new MenuSeperator("FlowersDraven.ClearMenu.JungleClearSettings", "-- JungleClear Settings"));
                    MyLogic.ClearMenu.Add(new MenuBool("FlowersDraven.ClearMenu.JungleClearQ", "Use Q"));
                    MyLogic.ClearMenu.Add(new MenuBool("FlowersDraven.ClearMenu.JungleClearW", "Use W"));
                    MyLogic.ClearMenu.Add(new MenuBool("FlowersDraven.ClearMenu.JungleClearE", "Use E"));
                    MyLogic.ClearMenu.Add(new MenuSlider("FlowersDraven.ClearMenu.JungleClearMana",
                        "When Player ManaPercent >= x%", 30, 1, 99));
                }

                MyLogic.KillStealMenu = MyLogic.Menu.Add(new Menu("FlowersDraven.KillStealMenu", ":: KillSteal Settings"));
                {
                    MyLogic.KillStealMenu.Add(new MenuSeperator("FlowersDraven.KillStealMenu.ESettings", "-- E Settings"));
                    MyLogic.KillStealMenu.Add(new MenuBool("FlowersDraven.KillStealMenu.E", "Use E"));

                    MyLogic.KillStealMenu.Add(new MenuSeperator("FlowersDraven.KillStealMenu.RSettings", "-- R Settings"));
                    MyLogic.KillStealMenu.Add(new MenuBool("FlowersDraven.KillStealMenu.R", "Use R"));
                    AddTargetList(MyLogic.KillStealMenu, "FlowersDraven.KillStealMenu.UltKS_");
                }

                MyLogic.MiscMenu = MyLogic.Menu.Add(new Menu("FlowersDraven.MiscMenu", ":: Misc Settings"));
                {
                    MyManaManager.AddFarmToMenu(MyLogic.MiscMenu);

                    MyLogic.MiscMenu.Add(new MenuSeperator("FlowersDraven.MiscMenu.WSettings", "-- W Settings"));
                    MyLogic.MiscMenu.Add(new MenuBool("FlowersDraven.MiscMenu.WSlow", "Auto W| When Player Have Debuff(Slow)"));

                    MyLogic.MiscMenu.Add(new MenuSeperator("FlowersDraven.MiscMenu.ESettings", "-- E Settings"));
                    MyLogic.MiscMenu.Add(new MenuBool("FlowersDraven.MiscMenu.EMelee", "Auto E| Anti Melee"));
                    MyLogic.MiscMenu.Add(new MenuBool("FlowersDraven.MiscMenu.ERengar", "Auto E| Anti Rengar"));
                    MyLogic.MiscMenu.Add(new MenuBool("FlowersDraven.MiscMenu.EKhazix", "Auto E| Anti Khazix"));

                    MyLogic.MiscMenu.Add(new MenuSeperator("FlowersDraven.MiscMenu.RSettings", "-- R Settings"));
                    MyLogic.MiscMenu.Add(new MenuSlider("FlowersDraven.MiscMenu.GlobalRMin",
                        "Global -> Cast R Min Range", 1000, 500, 2500));
                    MyLogic.MiscMenu.Add(new MenuSlider("FlowersDraven.MiscMenu.GlobalRMax",
                        "Global -> Cast R Max Range", 3000, 1500, 3500));
                    MyLogic.MiscMenu.Add(new MenuKeyBind("FlowersDraven.MiscMenu.SemiRKey", "Semi R Key",
                        Aimtec.SDK.Util.KeyCode.T, KeybindType.Press));
                }

                MyLogic.DrawMenu = MyLogic.Menu.Add(new Menu("FlowersDraven.DrawMenu", ":: Draw Settings"));
                {
                    MyLogic.DrawMenu.Add(new MenuSeperator("FlowersDraven.DrawMenu.RangeSettings", "-- Spell Range"));
                    MyLogic.DrawMenu.Add(new MenuBool("FlowersDraven.DrawMenu.E", "Draw E Range", false));
                    MyLogic.DrawMenu.Add(new MenuBool("FlowersDraven.DrawMenu.R", "Draw R Range", false));
                    MyLogic.DrawMenu.Add(new MenuSeperator("FlowersDraven.DrawMenu.AxeSettings", "-- Draw Axe"));
                    MyLogic.DrawMenu.Add(new MenuBool("FlowersDraven.DrawMenu.AxeRange", "Draw Catch Axe Range", false));
                    MyLogic.DrawMenu.Add(new MenuBool("FlowersDraven.DrawMenu.AxePosition", "Draw Axe Position", false));
                    MyManaManager.AddDrawToMenu(MyLogic.DrawMenu);
                }

                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MyMenuManager.Initializer." + ex);
            }
        }

        internal static void AddTargetList(Menu mainMenu, string extraName)
        {
            foreach (var target in GameObjects.EnemyHeroes)
            {
                mainMenu.Add(new MenuBool(extraName + target.ChampionName, "Use On: " + target.ChampionName));
            }
        }
    }
}