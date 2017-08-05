//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Aimtec;
//using Aimtec.SDK.Menu;
//using Aimtec.SDK.Menu.Components;
//using SharpShooter.MyCommon;
//using Aimtec.SDK.Extensions;

//namespace SharpShooter.MyUtility
//{
//    internal class MyAutoLevelManager
//    {
//        private static Menu Menu;
//        private static Menu AutoLevelMenu;
//        private static Random random;
//        private static int playerLevel;
//        private static bool canbeLevel;

//        public MyAutoLevelManager()
//        {
//            Initializer();
//        }

//        private static void Initializer()
//        {
//            playerLevel = ObjectManager.GetLocalPlayer().Level;
//            canbeLevel = false;

//            Menu = MyMenuExtensions.UtilityMenu;

//            AutoLevelMenu = new Menu("SharpShooter.MyUtility.AutoLevelMenu", "Auto Level")
//            {
//                new MenuBool("SharpShooter.MyUtility.AutoLevelMenu.Enabled", "Enabled", false),
//                new MenuBool("SharpShooter.MyUtility.AutoLevelMenu.R", "Auto Level R"),
//                new MenuSlider("SharpShooter.MyUtility.AutoLevelMenu.MinDelay", "Min Level Delay", 500, 0, 2000),
//                new MenuSlider("SharpShooter.MyUtility.AutoLevelMenu.MaxDelay", "Max Level Delay", 2000, 0, 5000),
//                new MenuBool("SharpShooter.MyUtility.AutoLevelMenu.QWE", "Auto Level QWE"),
//                new MenuList("SharpShooter.MyUtility.AutoLevelMenu.Mode", "Auto Level Mode: ", new []{"QWE", "QEW", "WQE", "WEQ", "EQW", "EWQ"}, 0)
//            };
//            Menu.Add(AutoLevelMenu);

//            Obj_AI_Base.OnLevelUp += OnLevelUp;
//            Game.OnUpdate += OnUpdate;
//        }

//        private static void OnLevelUp(Obj_AI_Base sender, Obj_AI_BaseLevelUpEventArgs Args)
//        {
//            if (sender.IsMe)
//            {
//                Console.WriteLine(Args.Source.UnitSkinName);
//                Console.WriteLine(Args.OldLevel);
//                Console.WriteLine(Args.NewLevel);
//            }
//        }

//        private static void OnUpdate()
//        {
//            if (!AutoLevelMenu["SharpShooter.MyUtility.AutoLevelMenu.Enabled"].Enabled)
//            {
//                return;
//            }

//            if (AutoLevelMenu["SharpShooter.MyUtility.AutoLevelMenu.R"].Enabled)
//            {
//                switch (ObjectManager.GetLocalPlayer().Level)
//                {
//                    case 6:
//                        if (ObjectManager.GetLocalPlayer().SpellBook.GetSpell(SpellSlot.R).Level == 0)
//                        {
//                            ObjectManager.GetLocalPlayer().SpellBook.LevelSpell(SpellSlot.R);
//                        }
//                        break;
//                    case 11:
//                        if (ObjectManager.GetLocalPlayer().SpellBook.GetSpell(SpellSlot.R).Level == 1)
//                        {
//                            ObjectManager.GetLocalPlayer().SpellBook.LevelSpell(SpellSlot.R);
//                        }
//                        break;
//                    case 16:
//                        if (ObjectManager.GetLocalPlayer().SpellBook.GetSpell(SpellSlot.R).Level == 2)
//                        {
//                            ObjectManager.GetLocalPlayer().SpellBook.LevelSpell(SpellSlot.R);
//                        }
//                        break;
//                }
//            }

//            if (AutoLevelMenu["SharpShooter.MyUtility.AutoLevelMenu.QWE"].Enabled)
//            {
//                switch (AutoLevelMenu["SharpShooter.MyUtility.AutoLevelMenu.Mode"].As<MenuList>().Value)
//                {
                    
//                }
//            }
//        }
//    }
//}
