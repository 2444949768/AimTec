﻿namespace SharpShooter.MyPlugin
{
    #region

    using Aimtec;
    using Aimtec.SDK.Damage;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Orbwalking;
    using Aimtec.SDK.Prediction.Skillshots;
    using Aimtec.SDK.TargetSelector;
    using Aimtec.SDK.Util;
    using Aimtec.SDK.Util.Cache;

    using Flowers_Library;

    using SharpShooter.MyBase;
    using SharpShooter.MyCommon;

    using System.Linq;

    using static SharpShooter.MyCommon.MyMenuExtensions;

    #endregion

    internal class KogMaw : MyLogic
    {
        private static int GetRCount => Me.HasBuff("kogmawlivingartillerycost") ? Me.GetBuffCount("kogmawlivingartillerycost") : 0;

        public KogMaw()
        {
            Initializer();
        }

        private static void Initializer()
        {
            Q = new Aimtec.SDK.Spell(SpellSlot.Q, 950f);
            Q.SetSkillshot(0.25f, 70f, 1650f, true, SkillshotType.Line);

            W = new Aimtec.SDK.Spell(SpellSlot.W, 500f);

            E = new Aimtec.SDK.Spell(SpellSlot.E, 1200f);
            E.SetSkillshot(0.25f, 120f, 1400f, false, SkillshotType.Line);

            R = new Aimtec.SDK.Spell(SpellSlot.R, 1800f);
            R.SetSkillshot(1.20f, 120f, float.MaxValue, false, SkillshotType.Circle);

            ComboOption.AddMenu();
            ComboOption.AddQ();
            ComboOption.AddW();
            ComboOption.AddE();
            ComboOption.AddR();
            ComboOption.AddSlider("ComboRLimit", "Use R|Max Buff Count <= x", 3, 0, 10);
            ComboOption.AddSlider("ComboRHP", "Use R|target HealthPercent <= x%", 70, 1, 101);

            HarassOption.AddMenu();
            HarassOption.AddQ();
            HarassOption.AddE();
            HarassOption.AddR();
            HarassOption.AddSlider("HarassRLimit", "Use R|Max Buff Count <= x", 5, 0, 10);
            HarassOption.AddMana();
            HarassOption.AddTargetList();

            LaneClearOption.AddMenu();
            LaneClearOption.AddQ();
            LaneClearOption.AddE();
            LaneClearOption.AddSlider("LaneClearECount", "Use E|Min Hit Count >= x", 3, 1, 5);
            LaneClearOption.AddR();
            LaneClearOption.AddSlider("LaneClearRLimit", "Use R|Max Buff Count <= x", 4, 0, 10);
            LaneClearOption.AddMana();

            JungleClearOption.AddMenu();
            JungleClearOption.AddQ();
            JungleClearOption.AddW();
            JungleClearOption.AddE();
            JungleClearOption.AddR();
            JungleClearOption.AddSlider("JungleClearRLimit", "Use R|Max Buff Count <= x", 5, 0, 10);
            JungleClearOption.AddMana();

            KillStealOption.AddMenu();
            KillStealOption.AddQ();
            KillStealOption.AddE();
            KillStealOption.AddR();

            GapcloserOption.AddMenu();

            MiscOption.AddMenu();
            MiscOption.AddBasic();
            MiscOption.AddE();
            MiscOption.AddBool("AutoE", "Auto E| Anti Gapcloser");
            MiscOption.AddR();
            MiscOption.AddKey("SemiR", "Semi R Key", KeyCode.T, KeybindType.Press);

            DrawOption.AddMenu();
            DrawOption.AddQ(Q.Range);
            DrawOption.AddE(E.Range);
            DrawOption.AddR(R.Range);
            DrawOption.AddFarm();
            DrawOption.AddEvent();

            Game.OnUpdate += OnUpdate;
            Orbwalker.PostAttack += PostAttack;
            Gapcloser.OnGapcloser += OnGapcloser;
        }

        private static void OnUpdate()
        {
            if (Me.IsDead || Me.IsRecalling())
            {
                return;
            }

            if (W.GetBasicSpell().Level > 0)
            {
                W.Range = 500f + new[] { 130, 150, 170, 190, 210 }[W.GetBasicSpell().Level - 1];
            }

            if (R.GetBasicSpell().Level > 0)
            {
                R.Range = 1200f + 300f * R.GetBasicSpell().Level - 1;
            }

            if (MiscOption.GetKey("SemiR").Enabled)
            {
                SemiRLogic();
            }

            KillSteal();

            if (Orbwalker.Mode == OrbwalkingMode.Combo)
            {
                Combo();
            }

            if (Orbwalker.Mode == OrbwalkingMode.Mixed)
            {
                Harass();
            }

            if (Orbwalker.Mode == OrbwalkingMode.Laneclear)
            {
                FarmHarass();
            }
        }

        private static void SemiRLogic()
        {
            if (R.Ready)
            {
                var target = TargetSelector.GetTarget(R.Range);

                if (target.IsValidTarget(R.Range))
                {
                    var rPred = R.GetPrediction(target);

                    if (rPred.HitChance >= HitChance.High)
                    {
                        R.Cast(rPred.CastPosition);
                    }
                }
            }
        }

        private static void KillSteal()
        {
            if (KillStealOption.UseQ && Q.Ready)
            {
                foreach (
                    var target in
                    GameObjects.EnemyHeroes.Where(
                        x => x.IsValidTarget(Q.Range) && x.Health < Me.GetSpellDamage(x, SpellSlot.Q)))
                {
                    if (target.IsValidTarget(Q.Range) && !target.IsUnKillable())
                    {
                        var qPred = Q.GetPrediction(target);

                        if (qPred.HitChance >= HitChance.Medium)
                        {
                            Q.Cast(qPred.CastPosition);
                        }
                    }
                }
            }

            if (KillStealOption.UseE && E.Ready)
            {
                foreach (
                    var target in
                    GameObjects.EnemyHeroes.Where(
                        x => x.IsValidTarget(E.Range) && x.Health < Me.GetSpellDamage(x, SpellSlot.E)))
                {
                    if (target.IsValidTarget(E.Range) && !target.IsUnKillable())
                    {
                        var ePred = E.GetPrediction(target);

                        if (ePred.HitChance >= HitChance.High)
                        {
                            E.Cast(ePred.UnitPosition);
                        }
                    }
                }
            }

            if (KillStealOption.UseR && R.Ready)
            {
                foreach (
                    var target in
                    GameObjects.EnemyHeroes.Where(
                        x => x.IsValidTarget(R.Range) && x.Health < Me.GetSpellDamage(x, SpellSlot.R)))
                {
                    if (target.IsValidTarget(R.Range) && !target.IsUnKillable())
                    {
                        var rPred = R.GetPrediction(target);

                        if (rPred.HitChance >= HitChance.High)
                        {
                            R.Cast(rPred.CastPosition);
                        }
                    }
                }
            }
        }

        private static void Combo()
        {
            var target = TargetSelector.GetTarget(R.Range);

            if (target.IsValidTarget(R.Range) && !target.IsUnKillable())
            {
                if (ComboOption.UseR && R.Ready && ComboOption.GetSlider("ComboRLimit").Value >= GetRCount &&
                    target.IsValidTarget(R.Range) && target.HealthPercent() <= ComboOption.GetSlider("ComboRHP").Value)
                {
                    var rPred = R.GetPrediction(target);

                    if (rPred.HitChance >= HitChance.High)
                    {
                        R.Cast(rPred.CastPosition);
                    }
                }

                if (ComboOption.UseQ && Q.Ready && target.IsValidTarget(Q.Range))
                {
                    var qPred = Q.GetPrediction(target);

                    if (qPred.HitChance >= HitChance.Medium)
                    {
                        Q.Cast(qPred.CastPosition);
                    }
                }

                if (ComboOption.UseE && E.Ready && target.IsValidTarget(E.Range))
                {
                    var ePred = E.GetPrediction(target);

                    if (ePred.HitChance >= HitChance.High)
                    {
                        E.Cast(ePred.UnitPosition);
                    }
                }

                if (ComboOption.UseW && W.Ready && target.IsValidTarget(W.Range) &&
                    target.DistanceToPlayer() > 550 && Orbwalker.CanAttack())
                {
                    W.Cast();
                }
            }
        }

        private static void Harass()
        {
            if (HarassOption.HasEnouguMana())
            {
                var target = HarassOption.GetTarget(R.Range);

                if (target.IsValidTarget(R.Range))
                {
                    if (HarassOption.UseR && R.Ready && HarassOption.GetSlider("HarassRLimit").Value >= GetRCount &&
                        target.IsValidTarget(R.Range))
                    {
                        var rPred = R.GetPrediction(target);

                        if (rPred.HitChance >= HitChance.High)
                        {
                            R.Cast(rPred.CastPosition);
                        }
                    }

                    if (HarassOption.UseQ && Q.Ready && target.IsValidTarget(Q.Range))
                    {
                        var qPred = Q.GetPrediction(target);

                        if (qPred.HitChance >= HitChance.Medium)
                        {
                            Q.Cast(qPred.CastPosition);
                        }
                    }

                    if (HarassOption.UseE && E.Ready && target.IsValidTarget(E.Range))
                    {
                        var ePred = E.GetPrediction(target);

                        if (ePred.HitChance >= HitChance.High)
                        {
                            E.Cast(ePred.UnitPosition);
                        }
                    }
                }
            }
        }

        private static void FarmHarass()
        {
            if (MyManaManager.SpellHarass)
            {
                Harass();
            }

            if (MyManaManager.SpellFarm)
            {
                LaneClear();
                JungleClear();
            }
        }

        private static void LaneClear()
        {
            if (LaneClearOption.HasEnouguMana())
            {
                var minions = GameObjects.EnemyMinions.Where(x => x.IsValidTarget(R.Range) && x.IsMinion()).ToArray();

                if (minions.Any())
                {
                    if (LaneClearOption.UseR && R.Ready && LaneClearOption.GetSlider("LaneClearRLimit").Value >= GetRCount)
                    {
                        var rMinion =
                            minions.FirstOrDefault(x => x.DistanceToPlayer() > Me.AttackRange + Me.BoundingRadius);

                        if (rMinion != null && rMinion.IsValidTarget(R.Range))
                        {
                            R.Cast(rMinion);
                        }
                    }

                    if (LaneClearOption.UseE && E.Ready)
                    {
                        var eMinions = minions.Where(x => x.IsValidTarget(E.Range)).ToArray();
                        var eFarm = E.GetSpellFarmPosition(eMinions);

                        if (eFarm.HitCount >= LaneClearOption.GetSlider("LaneClearECount").Value)
                        {
                            E.Cast(eFarm.CastPosition);
                        }
                    }

                    if (LaneClearOption.UseQ && Q.Ready)
                    {
                        var qMinion =
                            minions.Where(x => x.IsValidTarget(Q.Range))
                                .FirstOrDefault(
                                    x =>
                                        x.Health < Me.GetSpellDamage(x, SpellSlot.Q) &&
                                        x.Health > Me.GetAutoAttackDamage(x));

                        if (qMinion != null && qMinion.IsValidTarget(Q.Range))
                        {
                            Q.Cast(qMinion);
                        }
                    }
                }
            }
        }

        private static void JungleClear()
        {
            if (JungleClearOption.HasEnouguMana())
            {
                var mobs = GameObjects.EnemyMinions.Where(x => x.IsValidTarget(R.Range) && x.IsMob()).ToArray();

                if (mobs.Any())
                {
                    var bigmob = mobs.FirstOrDefault(x => !x.Name.ToLower().Contains("mini"));

                    if (JungleClearOption.UseR && R.Ready &&
                        JungleClearOption.GetSlider("JungleClearRLimit").Value >= GetRCount &&
                        bigmob != null && (!bigmob.IsValidAutoRange() || !Orbwalker.CanAttack()))
                    {
                        R.Cast(bigmob);
                    }

                    if (JungleClearOption.UseE && E.Ready)
                    {
                        if (bigmob != null && bigmob.IsValidTarget(E.Range) && (!bigmob.IsValidAutoRange() || !Orbwalker.CanAttack()))
                        {
                            E.Cast(bigmob);
                        }
                        else
                        {
                            var eMobs = mobs.Where(x => x.IsValidTarget(E.Range)).ToArray();
                            var eFarm = E.GetSpellFarmPosition(eMobs);

                            if (eFarm.HitCount >= 2)
                            {
                                E.Cast(eFarm.CastPosition);
                            }
                        }
                    }
                }
            }
        }

        private static void PostAttack(object sender, PostAttackEventArgs Args)
        {
            if (Args.Target == null || Args.Target.IsDead || Args.Target.Health < 1)
            {
                return;
            }

            if (Orbwalker.Mode == OrbwalkingMode.Combo)
            {
                var target = Args.Target as Obj_AI_Hero;

                if (target != null && !target.IsDead)
                {
                    if (ComboOption.UseW && W.Ready && target.IsValidTarget(W.Range))
                    {
                        W.Cast();
                    }
                    else if (ComboOption.UseQ && Q.Ready && target.IsValidTarget(Q.Range))
                    {
                        var qPred = Q.GetPrediction(target);

                        if (qPred.HitChance >= HitChance.Medium)
                        {
                            Q.Cast(qPred.CastPosition);
                        }
                    }
                    else if (ComboOption.UseE && E.Ready && target.IsValidTarget(E.Range))
                    {
                        var ePred = E.GetPrediction(target);

                        if (ePred.HitChance >= HitChance.High)
                        {
                            E.Cast(ePred.UnitPosition);
                        }
                    }
                }
            }
            else if (Orbwalker.Mode == OrbwalkingMode.Laneclear && JungleClearOption.HasEnouguMana() && Args.Target.IsMob())
            {
                var mobs = GameObjects.EnemyMinions.Where(x => x.IsValidTarget(R.Range) && x.IsMob()).ToArray();

                if (mobs.Any())
                {
                    var mob = mobs.FirstOrDefault();
                    var bigmob = mobs.FirstOrDefault(x => !x.Name.ToLower().Contains("mini"));

                    if (JungleClearOption.UseW && W.Ready && bigmob != null && bigmob.IsValidTarget(W.Range))
                    {
                        W.Cast();
                    }
                    else if (JungleClearOption.UseE && E.Ready)
                    {
                        if (bigmob != null && bigmob.IsValidTarget(E.Range))
                        {
                            E.Cast(bigmob);
                        }
                        else
                        {
                            var eMobs = mobs.Where(x => x.IsValidTarget(E.Range)).ToArray();
                            var eFarm = E.GetSpellFarmPosition(eMobs);

                            if (eFarm.HitCount >= 2)
                            {
                                E.Cast(eFarm.CastPosition);
                            }
                        }
                    }
                    else if (JungleClearOption.UseQ && Q.Ready && mob != null && mob.IsValidTarget(Q.Range))
                    {
                        Q.Cast(mob);
                    }
                }
            }
        }

        private static void OnGapcloser(Obj_AI_Hero target, GapcloserArgs Args)
        {
            if (MiscOption.GetBool("AutoE").Enabled && E.Ready && target.IsValidTarget(E.Range))
            {
                if (E.Ready && target != null && target.IsValidTarget(E.Range))
                {
                    switch (Args.Type)
                    {
                        case SpellType.Melee:
                            if (target.IsValidTarget(target.AttackRange + target.BoundingRadius + 100))
                            {
                                var ePred = E.GetPrediction(target);
                                E.Cast(ePred.UnitPosition);
                            }
                            break;
                        case SpellType.Dash:
                        case SpellType.SkillShot:
                        case SpellType.Targeted:
                            {
                                var ePred = E.GetPrediction(target);
                                E.Cast(ePred.UnitPosition);
                            }
                            break;
                    }
                }
            }
        }
    }
}