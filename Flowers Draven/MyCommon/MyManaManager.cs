namespace Flowers_Draven.MyCommon
{
    #region 

    using Aimtec;
    using Aimtec.SDK.Menu;
    using Aimtec.SDK.Menu.Components;

    using System;

    #endregion

    internal class MyManaManager
    {
        internal static bool SpellFarm { get; set; } = true;
        internal static bool SpellHarass { get; set; } = true;

        private static int tick { get; set; } = 0;

        internal static void AddFarmToMenu(Menu mainMenu)
        {
            try
            {
                if (mainMenu != null)
                {
                    mainMenu.Add(new MenuSeperator("MyManaManager.SpellFarmSettings", ":: Spell Farm Logic"));
                    var spellFarm = mainMenu.Add(new MenuBool("MyManaManager.SpellFarm", "Use Spell To Farm(Mouse Scrool)"));
                    var spellHarass = mainMenu.Add(new MenuKeyBind("MyManaManager.SpellHarass", "Use Spell To Harass(In Clear Mode)",
                        Aimtec.SDK.Util.KeyCode.H, KeybindType.Toggle, true));

                    Game.OnWndProc += delegate (WndProcEventArgs Args)
                    {
                        try
                        {
                            if (Args.Message == 0x20a)
                            {
                                spellFarm.As<MenuBool>().Value = !spellFarm.As<MenuBool>().Value;
                                SpellFarm = spellFarm.Enabled;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error in MyManaManager.OnWndProcEvent." + ex);
                        }
                    };

                    Game.OnUpdate += delegate
                    {
                        if (Environment.TickCount - tick > 100 * Game.Ping)
                        {
                            tick = Environment.TickCount;
                            SpellFarm = spellFarm.Enabled;
                            SpellHarass = spellHarass.Enabled;
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MyManaManager.AddFarmToMenu." + ex);
            }
        }

        internal static void AddDrawToMenu(Menu mainMenu)
        {
            try
            {
                if (mainMenu != null)
                {
                    var spellFarm = mainMenu.Add(new MenuBool("MyManaManager.DrawSpelFarm", "Draw Spell Farm Status"));
                    var spellHarass = mainMenu.Add(new MenuBool("MyManaManager.DrawSpellHarass", "Draw Spell Harass Status"));

                    Render.OnRender += delegate
                    {
                        try
                        {
                            if (ObjectManager.GetLocalPlayer().IsDead)
                            {
                                return;
                            }

                            if (spellFarm.Enabled)
                            {
                                Vector2 MePos = Vector2.Zero;
                                Render.WorldToScreen(ObjectManager.GetLocalPlayer().Position, out MePos);

                                Render.Text(MePos.X - 57, MePos.Y + 48, System.Drawing.Color.FromArgb(242, 120, 34),
                                    "Spell Farms:" + (SpellFarm ? "On" : "Off"));
                            }

                            if (spellHarass.Enabled)
                            {
                                Vector2 MePos = Vector2.Zero;
                                Render.WorldToScreen(ObjectManager.GetLocalPlayer().Position, out MePos);

                                Render.Text(MePos.X - 57, MePos.Y + 68, System.Drawing.Color.FromArgb(242, 120, 34),
                                    "Spell Harass:" + (SpellFarm ? "On" : "Off"));
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error in MyManaManager.OnRender." + ex);
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MyManaManager.AddDrawToMenu." + ex);
            }
        }
    }
}