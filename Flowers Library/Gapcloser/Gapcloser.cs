namespace Flowers_Library
{
    //TODO
    //1.Rengar, Khazix Jump
    //2.Dash Check
    //3.Anti Melee?

    #region

    using Aimtec;
    using Aimtec.SDK.Menu;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Extensions;

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    public delegate void OnGapcloserEvent(Obj_AI_Hero target, GapcloserArgs Args);

    public enum SpellType
    {
        Attack,
        SkillShot,
        Targeted
    }

    public struct SpellData
    {
        public string ChampionName { get; set; }

        public string SpellName { get; set; }

        public SpellSlot Slot { get; set; }

        public SpellType SpellType { get; set; }
    }

    public struct GapcloserArgs
    {
        public Obj_AI_Hero Unit { get; set; }

        public SpellSlot Slot { get; set; }

        public string SpellName { get; set; }

        public SpellType Type { get; set; }

        public Vector3 StartPosition { get; set; }

        public Vector3 EndPosition { get; set; }

        public int StartTick { get; set; }

        //public int EndTick { get; set; }

        //public int DurationTick { get; set; }

        public GapcloserArgs(Obj_AI_Hero unit, SpellSlot slot, SpellType type, string spellName, Vector3 startPos,
            Vector3 endPos, int startTick)//, int endTick, int durationTick)
        {
            this.Unit = unit;
            this.Slot = slot;
            this.Type = type;
            this.SpellName = spellName;
            this.StartPosition = startPos;
            this.EndPosition = endPos;
            this.StartTick = startTick;
            //this.EndTick = endTick;
            //this.DurationTick = durationTick;
        }
    }

    public static class Gapcloser
    {
        public static event OnGapcloserEvent OnGapcloser;

        public static List<GapcloserArgs> Gapclosers = new List<GapcloserArgs>();
        public static List<SpellData> Spells = new List<SpellData>();

        public static Menu Menu;

        static Gapcloser()
        {
            Initialize();
        }

        private static void Initialize()
        {
            #region Aatrox

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Aatrox",
                    Slot = SpellSlot.Q,
                    SpellName = "aatroxq",
                    SpellType = SpellType.SkillShot
                });

            #endregion

            #region Akali

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Akali",
                    Slot = SpellSlot.R,
                    SpellName = "akalishadowdance",
                    SpellType = SpellType.Targeted
                });

            #endregion

            #region Alistar

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Alistar",
                    Slot = SpellSlot.W,
                    SpellName = "headbutt",
                    SpellType = SpellType.Targeted
                });

            #endregion

            #region Azir

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Azir",
                    Slot = SpellSlot.E,
                    SpellName = "azire",
                    SpellType = SpellType.SkillShot
                });

            #endregion

            #region Camille

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Camille",
                    Slot = SpellSlot.E,
                    SpellName = "camillee",
                    SpellType = SpellType.SkillShot
                });

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Camille",
                    Slot = SpellSlot.E,
                    SpellName = "camilleedash2",
                    SpellType = SpellType.SkillShot
                });

            #endregion

            #region Corki

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Corki",
                    Slot = SpellSlot.W,
                    SpellName = "carpetbomb",
                    SpellType = SpellType.SkillShot
                });

            #endregion

            #region Diana

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Diana",
                    Slot = SpellSlot.R,
                    SpellName = "dianateleport",
                    SpellType = SpellType.Targeted
                });

            #endregion

            #region Ekko

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Ekko",
                    Slot = SpellSlot.E,
                    SpellName = "ekkoeattack",
                    SpellType = SpellType.Targeted
                });

            #endregion

            #region Elise

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Elise",
                    Slot = SpellSlot.Q,
                    SpellName = "elisespiderqcast",
                    SpellType = SpellType.SkillShot
                });

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Elise",
                    Slot = SpellSlot.E,
                    SpellName = "elisespideredescent",
                    SpellType = SpellType.Targeted
                });

            #endregion

            #region Fiora

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Fiora",
                    Slot = SpellSlot.Q,
                    SpellName = "fioraq",
                    SpellType = SpellType.SkillShot
                });

            #endregion

            #region Fizz

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Fizz",
                    Slot = SpellSlot.Q,
                    SpellName = "fizzpiercingstrike",
                    SpellType = SpellType.Targeted
                });

            #endregion

            #region Galio

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Galio",
                    Slot = SpellSlot.E,
                    SpellName = "galioe",
                    SpellType = SpellType.SkillShot
                });

            #endregion

            #region Gnar

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Gnar",
                    Slot = SpellSlot.E,
                    SpellName = "gnarbige",
                    SpellType = SpellType.SkillShot
                });

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Gnar",
                    Slot = SpellSlot.E,
                    SpellName = "gnare",
                    SpellType = SpellType.SkillShot
                });

            #endregion

            #region Gragas

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Gragas",
                    Slot = SpellSlot.E,
                    SpellName = "gragase",
                    SpellType = SpellType.SkillShot
                });

            #endregion

            #region Graves

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Graves",
                    Slot = SpellSlot.E,
                    SpellName = "gravesmove",
                    SpellType = SpellType.SkillShot
                });

            #endregion

            #region Hecarim

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Hecarim",
                    Slot = SpellSlot.R,
                    SpellName = "hecarimult",
                    SpellType = SpellType.SkillShot
                });

            #endregion

            #region Illaoi

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Illaoi",
                    Slot = SpellSlot.W,
                    SpellName = "illaoiwattack",
                    SpellType = SpellType.Targeted
                });

            #endregion

            #region Irelia

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Irelia",
                    Slot = SpellSlot.Q,
                    SpellName = "ireliagatotsu",
                    SpellType = SpellType.Targeted
                });

            #endregion

            #region JarvanIV

            Spells.Add(
                new SpellData
                {
                    ChampionName = "JarvanIV",
                    Slot = SpellSlot.Q,
                    SpellName = "jarvanivdragonstrike",
                    SpellType = SpellType.SkillShot
                });

            #endregion

            #region Jax

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Jax",
                    Slot = SpellSlot.Q,
                    SpellName = "jaxleapstrike",
                    SpellType = SpellType.Targeted
                });

            #endregion

            #region Jayce

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Jayce",
                    Slot = SpellSlot.Q,
                    SpellName = "jaycetotheskies",
                    SpellType = SpellType.Targeted
                });

            #endregion

            #region Kassadin

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Kassadin",
                    Slot = SpellSlot.R,
                    SpellName = "riftwalk",
                    SpellType = SpellType.SkillShot
                });

            #endregion

            #region Katarina

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Katarina",
                    Slot = SpellSlot.E,
                    SpellName = "katarinae",
                    SpellType = SpellType.Targeted
                });

            #endregion

            #region Kayn

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Kayn",
                    Slot = SpellSlot.Q,
                    SpellName = "kaynq",
                    SpellType = SpellType.SkillShot
                });

            #endregion

            #region Khazix

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Khazix",
                    Slot = SpellSlot.E,
                    SpellName = "khazixe",
                    SpellType = SpellType.SkillShot
                });

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Khazix",
                    Slot = SpellSlot.E,
                    SpellName = "khazixelong",
                    SpellType = SpellType.SkillShot
                });

            #endregion

            #region Kindred

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Kindred",
                    Slot = SpellSlot.Q,
                    SpellName = "kindredq",
                    SpellType = SpellType.SkillShot
                });

            #endregion

            #region Leblanc

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Leblanc",
                    Slot = SpellSlot.W,
                    SpellName = "leblancslide",
                    SpellType = SpellType.SkillShot
                });

            #endregion

            #region LeeSin

            Spells.Add(
                new SpellData
                {
                    ChampionName = "LeeSin",
                    Slot = SpellSlot.Q,
                    SpellName = "blindmonkqtwo",
                    SpellType = SpellType.Targeted
                });

            #endregion

            #region Leona

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Leona",
                    Slot = SpellSlot.E,
                    SpellName = "leonazenithblade",
                    SpellType = SpellType.SkillShot
                });

            #endregion

            #region Lucian

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Lucian",
                    Slot = SpellSlot.E,
                    SpellName = "luciane",
                    SpellType = SpellType.SkillShot
                });

            #endregion

            #region Malphite

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Malphite",
                    Slot = SpellSlot.R,
                    SpellName = "ufslash",
                    SpellType = SpellType.SkillShot
                });

            #endregion

            #region MasterYi

            Spells.Add(
                new SpellData
                {
                    ChampionName = "MasterYi",
                    Slot = SpellSlot.Q,
                    SpellName = "alphastrike",
                    SpellType = SpellType.Targeted
                });

            #endregion

            #region MonkeyKing

            Spells.Add(
                new SpellData
                {
                    ChampionName = "MonkeyKing",
                    Slot = SpellSlot.E,
                    SpellName = "monkeykingnimbus",
                    SpellType = SpellType.Targeted
                });

            #endregion

            #region Nautilus

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Nautilus",
                    Slot = SpellSlot.Q,
                    SpellName = "nautilusq",
                    SpellType = SpellType.SkillShot
                });

            #endregion

            #region Pantheon

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Pantheon",
                    Slot = SpellSlot.W,
                    SpellName = "pantheon_leapbash",
                    SpellType = SpellType.Targeted
                });

            #endregion

            #region Poppy

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Poppy",
                    Slot = SpellSlot.E,
                    SpellName = "poppyheroiccharge",
                    SpellType = SpellType.Targeted
                });

            #endregion

            #region Quinn

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Quinn",
                    Slot = SpellSlot.E,
                    SpellName = "quinne",
                    SpellType = SpellType.Targeted
                });

            #endregion

            #region Rakan

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Rakan",
                    Slot = SpellSlot.W,
                    SpellName = "rakanw",
                    SpellType = SpellType.SkillShot
                });

            #endregion

            #region Renekton

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Renekton",
                    Slot = SpellSlot.E,
                    SpellName = "renektonsliceanddice",
                    SpellType = SpellType.SkillShot
                });

            #endregion

            #region Riven

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Riven",
                    Slot = SpellSlot.Q,
                    SpellName = "riventricleave",
                    SpellType = SpellType.SkillShot
                });

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Riven",
                    Slot = SpellSlot.E,
                    SpellName = "rivenfeint",
                    SpellType = SpellType.SkillShot
                });

            #endregion

            #region Sejuani

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Sejuani",
                    Slot = SpellSlot.Q,
                    SpellName = "sejuaniarcticassault",
                    SpellType = SpellType.SkillShot
                });

            #endregion

            #region Shen

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Shen",
                    Slot = SpellSlot.E,
                    SpellName = "shene",
                    SpellType = SpellType.SkillShot
                });

            #endregion

            #region Shyvana

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Shyvana",
                    Slot = SpellSlot.R,
                    SpellName = "shyvanatransformcast",
                    SpellType = SpellType.SkillShot
                });

            #endregion

            #region Talon

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Talon",
                    Slot = SpellSlot.Q,
                    SpellName = "talonq",
                    SpellType = SpellType.Targeted
                });

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Talon",
                    Slot = SpellSlot.E,
                    SpellName = "taloncutthroat",
                    SpellType = SpellType.SkillShot
                });

            #endregion

            #region Tristana

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Tristana",
                    Slot = SpellSlot.W,
                    SpellName = "rocketjump",
                    SpellType = SpellType.SkillShot
                });

            #endregion

            #region Tryndamere

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Tryndamere",
                    Slot = SpellSlot.E,
                    SpellName = "slashcast",
                    SpellType = SpellType.SkillShot
                });

            #endregion

            #region Vi

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Vi",
                    Slot = SpellSlot.Q,
                    SpellName = "viq",
                    SpellType = SpellType.SkillShot
                });

            #endregion

            #region Vayne

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Vayne",
                    Slot = SpellSlot.Q,
                    SpellName = "vayneq",
                    SpellType = SpellType.SkillShot
                });

            #endregion

            #region Warwick

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Warwick",
                    Slot = SpellSlot.R,
                    SpellName = "warwickr",
                    SpellType = SpellType.SkillShot
                });

            #endregion

            #region XinZhao

            Spells.Add(
                new SpellData
                {
                    ChampionName = "XinZhao",
                    Slot = SpellSlot.E,
                    SpellName = "xenzhaosweep",
                    SpellType = SpellType.Targeted
                });

            #endregion

            #region Yasuo

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Yasuo",
                    Slot = SpellSlot.E,
                    SpellName = "yasuodashwrapper",
                    SpellType = SpellType.Targeted
                });

            #endregion

            #region Zac

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Zac",
                    Slot = SpellSlot.E,
                    SpellName = "zace",
                    SpellType = SpellType.SkillShot
                });

            #endregion

            #region Zed

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Zed",
                    Slot = SpellSlot.R,
                    SpellName = "zedr",
                    SpellType = SpellType.Targeted
                });

            #endregion

            #region Ziggs

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Ziggs",
                    Slot = SpellSlot.W,
                    SpellName = "ziggswtoggle",
                    SpellType = SpellType.SkillShot
                });

            #endregion
        }

        public static void Attach(Menu mainMenu, string MenuName)
        {
            if (ObjectManager.Get<Obj_AI_Hero>().All(x => !x.IsEnemy))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("WARN: No Enemy in this Game, Gapcloser Event Dont work!!!");
                Console.ResetColor();
                return;
            }

            Menu = new Menu("Gapcloser.", MenuName)
                {
                    new MenuBool("Gapcloser.Enabled", "Enabled"),
                    new MenuSeperator("Gapcloser.Seperator1")
                };
            mainMenu.Add(Menu);

            foreach (var enemy in ObjectManager.Get<Obj_AI_Hero>().Where(x => x.IsEnemy))
            {
                if (Menu["Gapcloser.HeroMenu_" + enemy.ChampionName] != null)
                {
                    continue;
                }

                var heroMenu = new Menu("Gapcloser.HeroMenu_" + enemy.ChampionName, enemy.ChampionName)
                    {
                        new MenuBool("Gapcloser.Menu_" + enemy.ChampionName + ".Enabled", "Enabled"),
                        new MenuSlider("Gapcloser.Menu_" + enemy.ChampionName + ".Distance",
                                "If Target Distance To Player <= x", 450, 50, 600)
                    };

                foreach (var spell in Spells.Where(x => x.ChampionName == enemy.ChampionName))
                {
                    if (heroMenu["Gapcloser.Menu_" + enemy.ChampionName + "." + spell.SpellName] != null)
                    {
                        continue;
                    }

                    heroMenu.Add(new MenuBool("Gapcloser.Menu_" + enemy.ChampionName + "." + spell.SpellName,
                        "Anti " + spell.Slot + "(" + spell.SpellName +")"));
                }
            }

            Game.OnUpdate += OnUpdate;
            Obj_AI_Base.OnProcessSpellCast += OnProcessSpellCast;
            //Obj_AI_Base.OnNewPath += OnNewPath;
        }

        private static void OnNewPath(Obj_AI_Base sender, Obj_AI_BaseNewPathEventArgs Args)
        {
            //EndTick = (int) (EndPosition.DistanceSqr(StartPosition) / Args.Speed * Args.Speed * 1000) + StartTick;

            //DurationTick = EndTick - StartTick;
        }

        private static void OnUpdate()
        {
            Gapclosers.RemoveAll(x => Game.TickCount - x.StartTick > 900 + Game.Ping);

            if (OnGapcloser == null || Menu["Gapcloser.Enabled"] == null || !Menu["Gapcloser.Enabled"].Enabled)
            {
                return;
            }

            foreach (
                var Args in
                Gapclosers.Where(
                    x =>
                        x.Unit.IsValidTarget() && Menu["Gapcloser.HeroMenu_" + x.Unit.ChampionName] != null &&
                        Menu["Gapcloser.HeroMenu_" + x.Unit.ChampionName][
                            "Gapcloser.Menu_" + x.Unit.ChampionName + ".Enabled"].As<MenuBool>().Enabled &&
                        x.Unit.ServerPosition.DistanceSqr(ObjectManager.GetLocalPlayer().ServerPosition) <=
                        Menu["Gapcloser.HeroMenu_" + x.Unit.ChampionName][
                            "Gapcloser.Menu_" + x.Unit.ChampionName + ".Distance"].As<MenuSlider>().Value *
                        Menu["Gapcloser.HeroMenu_" + x.Unit.ChampionName][
                            "Gapcloser.Menu_" + x.Unit.ChampionName + ".Distance"].As<MenuSlider>().Value))
            {
                OnGapcloser(Args.Unit, Args);
            }
        }

        private static void OnProcessSpellCast(Obj_AI_Base sender, Obj_AI_BaseMissileClientDataEventArgs Args)
        {
            if (sender == null || !sender.IsValid || sender.Type != GameObjectType.obj_AI_Hero || !sender.IsEnemy ||
                string.IsNullOrEmpty(Args.SpellData.Name) ||
                Spells.All(
                    x => !string.Equals(x.SpellName, Args.SpellData.Name, StringComparison.CurrentCultureIgnoreCase)))
            {
                return;
            }

            Gapclosers.Add(new GapcloserArgs(sender as Obj_AI_Hero, Args.SpellSlot,
                Args.Target.IsMe ? SpellType.Targeted : SpellType.SkillShot, Args.SpellData.Name, Args.Start,
                Args.End, Game.TickCount));
        }
    }
}
