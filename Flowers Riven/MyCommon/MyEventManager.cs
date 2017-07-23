namespace Flowers_Riven.MyCommon
{
    #region

    using Aimtec;
    using Aimtec.SDK.Damage;
    using Aimtec.SDK.Damage.JSON;
    using Aimtec.SDK.Events;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Orbwalking;
    using Aimtec.SDK.TargetSelector;
    using Aimtec.SDK.Util.Cache;

    using Flowers_Riven.MyBase;

    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    #endregion

    internal class MyEventManager : MyLogic
    {
        private static readonly int _menuX = (int)(Render.Width * 0.91f);
        private static readonly int _menuY = (int)(Render.Height * 0.04f);

        internal static void Initializer()
        {
            try
            {
                Game.OnUpdate += OnUpdate;
                SpellBook.OnCastSpell += OnCastSpell;
                Obj_AI_Base.OnProcessSpellCast += OnProcessSpellCast;
                Obj_AI_Base.OnPerformCast += OnPerformCast;
                Obj_AI_Base.OnPlayAnimation += OnPlayAnimation;
                Orbwalker.PostAttack += OnPostAttack;
                Render.OnRender += OnRender;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MyEventManager.Initializer." + ex);
            }
        }

        private static void OnUpdate()
        {
            try
            {
                ResetToDefalut();

                if (Me.IsDead || Me.IsRecalling())
                {
                    return;
                }

                if (FleeMenu["FlowersRiven.FleeMenu.FleeKey"].As<MenuKeyBind>().Enabled && Me.CanMoveMent())
                {
                    FleeEvent();
                }

                KillStealEvent();
                AutoUseEvent();
                
                switch (Orbwalker.Mode)
                {
                    case OrbwalkingMode.Combo:
                        ComboEvent();
                        break;
                    case OrbwalkingMode.Mixed:
                        HarassEvent();
                        break;
                    case OrbwalkingMode.Laneclear:
                        ClearEvent();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MyEventManager.OnUpdate." + ex);
            }
        }

        private static void ResetToDefalut()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MyEventManager.ResetToDefalut." + ex);
            }
        }

        private static void FleeEvent()
        {
            try
            {
                Me.IssueOrder(OrderType.MoveTo, Game.CursorPos);


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MyEventManager.FleeEvent." + ex);
            }
        }

        private static void KillStealEvent()
        {
            try
            {
                if (KillStealMenu["FlowersRiven.KillStealMenu.R"].Enabled && R.Ready && isRActive)
                {
                    foreach (
                        var target in
                        GameObjects.EnemyHeroes.Where(
                            x =>
                                x.IsValidTarget(R.Range) &&
                                KillStealMenu["FlowersRiven.KillStealMenu.RTargetFor" + x.ChampionName].Enabled &&
                                x.Health < Me.GetSpellDamage(x, SpellSlot.R)))
                    {
                        if (target.IsValidTarget(R.Range) && !target.IsUnKillable())
                        {
                            R.Cast(target);
                            return;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MyEventManager.KillStealEvent." + ex);
            }
        }

        private static void AutoUseEvent()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MyEventManager.AutoUseEvent." + ex);
            }
        }

        private static void ComboEvent()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MyEventManager.ComboEvent." + ex);
            }
        }

        private static void BurstEvent(Obj_AI_Hero target)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MyEventManager.BurstEvent." + ex);
            }
        }

        private static void HarassEvent()
        {
            try
            {
                if (Me.IsUnderEnemyTurret())
                {
                    return;
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MyEventManager.HarassEvent." + ex);
            }
        }

        private static void ClearEvent()
        {
            try
            {
                if (MyManaManager.SpellFarm)
                {
                    LaneClearEvent();
                    JungleClearEvent();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MyEventManager.ClearEvent." + ex);
            }
        }

        private static void LaneClearEvent()
        {
            try
            {
                if (Me.IsUnderEnemyTurret())
                {
                    return;
                }

                var minions = GameObjects.EnemyMinions.Where(x => x.IsValidTarget(Q.Range) && x.Health > 5).ToArray();

                if (minions.Any())
                {

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MyEventManager.LaneClearEvent." + ex);
            }
        }

        private static void JungleClearEvent()
        {
            try
            {
                var mobs =
                    GameObjects.EnemyMinions.Where(
                        x =>
                            x.IsValidTarget(Q.Range + Me.AttackRange + Me.BoundingRadius) && x.Health > 5 &&
                            x.Team == GameObjectTeam.Neutral).ToArray();

                if (mobs.Any())
                {
                    var mob = mobs.OrderBy(x => x.MaxHealth).FirstOrDefault(x => x.IsValidTarget(Q.Range));

                    if (mob != null)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MyEventManager.JungleClearEvent." + ex);
            }
        }

        private static void OnCastSpell(Obj_AI_Base sender, SpellBookCastSpellEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MyEventManager.OnCastSpell." + ex);
            }
        }

        private static void OnProcessSpellCast(Obj_AI_Base sender, Obj_AI_BaseMissileClientDataEventArgs Args)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MyEventManager.OnProcessSpellCast." + ex);
            }
        }

        private static void OnPerformCast(Obj_AI_Base sender, Obj_AI_BaseMissileClientDataEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MyEventManager.OnPerformCast." + ex);
            }
        }

        private static void OnPlayAnimation(Obj_AI_Base sender, Obj_AI_BasePlayAnimationEventArgs Args)
        {
            try
            {
                if (sender.IsMe)
                {

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MyEventManager.OnPlayAnimation." + ex);
            }
        }

        private static void OnPostAttack(object sender, PostAttackEventArgs Args)
        {
            try
            {
                if (Args.Target == null || Args.Target.IsDead || !Args.Target.IsValidTarget())
                {
                    return;
                }

                switch (Orbwalker.Mode)
                {
                    case OrbwalkingMode.Combo:

                        break;
                    case OrbwalkingMode.Mixed:

                        break;
                    case OrbwalkingMode.Laneclear:

                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MyEventManager.OnPostAttack." + ex);
            }
        }

        private static void OnRender()
        {
            try
            {
                if (DrawMenu["FlowersRiven.DrawMenu.E"].Enabled && E.Ready)
                {
                    Render.Circle(Me.Position, E.Range, 23, Color.FromArgb(0, 136, 255));
                }

                if (DrawMenu["FlowersRiven.DrawMenu.R"].Enabled && R.Ready)
                {
                    Render.Circle(Me.Position, R.Range, 23, Color.FromArgb(251, 0, 133));
                }

                if (DrawMenu["FlowersRiven.DrawMenu.ComboR"].Enabled)
                {
                    Render.Text(_menuX + 10, _menuY + 25, Color.Orange,
                        "Combo R(" + ComboMenu["FlowersRiven.ComboMenu.R"].As<MenuKeyBind>().Key + "): " +
                        (ComboMenu["FlowersRiven.ComboMenu.R"].As<MenuKeyBind>().Enabled ? "On" : "Off"));
                }

                if (DrawMenu["FlowersRiven.DrawMenu.Burst"].Enabled)
                {
                    Render.Text(_menuX + 10, _menuY + 45, Color.Orange,
                        "Burst Combo(" + BurstMenu["FlowersRiven.BurstMenu.Key"].As<MenuKeyBind>().Key + "): " +
                        (BurstMenu["FlowersRiven.BurstMenu.Key"].As<MenuKeyBind>().Enabled ? "On" : "Off"));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MyEventManager.OnRender." + ex);
            }
        }
    }
}