using Aimtec;
using Aimtec.SDK.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu;
using Spell = Aimtec.SDK.Spell;
using Flowers_Library;
using Aimtec.SDK.TargetSelector;

namespace SharpShooter
{
    class MyLoader
    {
        static Spell E;
        static Menu Menu;
        static Item cs = new Item(ItemID.BilgewaterCutlass, 500);

        static void Main(string[] args)
        {
            GameEvents.GameStart += () =>
            {
                E = new Spell(SpellSlot.E, 500);

                Menu = new Menu("asdasd", "asdasd", true);

                Gapcloser.Attach(Menu, "AntiGapcloser");

                Menu.Attach();

                Game.OnUpdate += Game_OnUpdate;
                Gapcloser.OnGapcloser += OnGapcloser;
            };
        }

        private static void Game_OnUpdate()
        {
            if (cs.IsMine && cs.Ready)
            {
                cs.CastOnUnit(TargetSelector.GetSelectedTarget());
            }
        }

        private static void OnGapcloser(Obj_AI_Hero target, GapcloserArgs Args)
        {
            if (target != null)
            {
                if (target.IsValidTarget(E.Range))
                {
                    E.CastOnUnit(target);
                }
            }
        }
    }
}
