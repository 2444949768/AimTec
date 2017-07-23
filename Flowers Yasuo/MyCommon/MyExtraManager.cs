namespace Flowers_Yasuo.MyCommon
{
    #region

    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Prediction.Skillshots;
    using Aimtec.SDK.Util.Cache;

    using Flowers_Yasuo.MyBase;

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    internal static class MyExtraManager
    {
        internal static float DistanceToPlayer(this Obj_AI_Base source)
        {
            return ObjectManager.GetLocalPlayer().Distance(source);
        }

        internal static float DistanceToPlayer(this Vector3 position)
        {
            return position.To2D().DistanceToPlayer();
        }

        internal static float DistanceToPlayer(this Vector2 position)
        {
            return ObjectManager.GetLocalPlayer().Distance(position);
        }

        public static T MinOrDefault<T, TR>(this IEnumerable<T> container, Func<T, TR> valuingFoo)
            where TR : IComparable
        {
            var enumerator = container.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return default(T);
            }

            var minElem = enumerator.Current;
            var minVal = valuingFoo(minElem);

            while (enumerator.MoveNext())
            {
                var currVal = valuingFoo(enumerator.Current);

                if (currVal.CompareTo(minVal) < 0)
                {
                    minVal = currVal;
                    minElem = enumerator.Current;
                }
            }

            return minElem;
        }

        internal static IEnumerable<Vector3> FlashPoints()
        {
            var points = new List<Vector3>();

            for (var i = 1; i <= 360; i++)
            {
                var angle = i * 2 * Math.PI / 360;
                var point = new Vector2(ObjectManager.GetLocalPlayer().Position.X + 425f * (float)Math.Cos(angle),
                    ObjectManager.GetLocalPlayer().Position.Y + 425f * (float)Math.Sin(angle)).To3D();

                points.Add(point);
            }

            return points;
        }

        internal static bool CanCastR(Obj_AI_Hero target)
        {
            return target.HasBuffOfType(BuffType.Knockup) || target.HasBuffOfType(BuffType.Knockback);
        }

        internal static Obj_AI_Base GetNearObj(Obj_AI_Base target = null)
        {
            var pos = target != null
                ? Prediction.GetPrediction(target, 0.75f, 0, 1025f).UnitPosition
                : Game.CursorPos;
            var obj = new List<Obj_AI_Base>();

            obj.AddRange(GameObjects.Minions.Where(x => x.IsValidTarget(475) && !x.IsAlly && x.IsMinion));
            obj.AddRange(GameObjects.EnemyHeroes.Where(i => i.IsValidTarget(475)));

            return obj.Where(i => CanCastE(i) && pos.Distance(PosAfterE(i)) < ObjectManager.GetLocalPlayer().Distance(pos))
                    .MinOrDefault(i => pos.Distance(PosAfterE(i)));
        }

        internal static void EGapTarget(Obj_AI_Hero target, bool UnderTurret, float GapcloserDis,
            bool includeChampion = true)
        {
            var dashtargets = new List<Obj_AI_Base>();
            dashtargets.AddRange(
                GameObjects.EnemyHeroes.Where(
                    x =>
                        !x.IsDead && (includeChampion || x.NetworkId != target.NetworkId) && x.IsValidTarget(475) &&
                        CanCastE(x)));
            dashtargets.AddRange(
                GameObjects.Minions.Where(x => x.IsValidTarget(475) && !x.IsAlly && x.IsMinion)
                    .Where(CanCastE));

            if (dashtargets.Any())
            {
                var dash = dashtargets.Where(x => x.IsValidTarget(475))
                    .OrderBy(x => target.Position.Distance(PosAfterE(x)))
                    .FirstOrDefault();//(x => MyEvade.Program.IsSafe(PosAfterE(x).ToMy2D()).IsSafe);

                if (dash != null && dash.DistanceToPlayer() <= 475 && CanCastE(dash) &&
                    target.DistanceToPlayer() >= GapcloserDis &&
                    target.Position.Distance(PosAfterE(dash)) <= target.DistanceToPlayer() &&
                    ObjectManager.GetLocalPlayer().IsFacing(dash) && (UnderTurret || !UnderTower(PosAfterE(dash))))
                    ObjectManager.GetLocalPlayer().SpellBook.CastSpell(SpellSlot.E, dash);
            }
        }

        internal static void EGapMouse(Obj_AI_Hero target, bool UnderTurret, float GapcloserDis,
            bool includeChampion = true)
        {
            if (target.DistanceToPlayer() > (ObjectManager.GetLocalPlayer().AttackRange + ObjectManager.GetLocalPlayer().BoundingRadius) * 1.2 ||
                target.DistanceToPlayer() >
                (ObjectManager.GetLocalPlayer().AttackRange + ObjectManager.GetLocalPlayer().BoundingRadius + target.BoundingRadius) * 0.8 ||
                Game.CursorPos.DistanceToPlayer() >=
                (ObjectManager.GetLocalPlayer().AttackRange + ObjectManager.GetLocalPlayer().BoundingRadius) * 1.2)
            {
                var dashtargets = new List<Obj_AI_Base>();
                dashtargets.AddRange(
                    GameObjects.EnemyHeroes.Where(
                        x =>
                            !x.IsDead && (includeChampion || x.NetworkId != target.NetworkId) && x.IsValidTarget(475) &&
                            CanCastE(x)));
                dashtargets.AddRange(
                    GameObjects.Minions.Where(x => x.IsValidTarget(475) && !x.IsAlly && x.IsMinion)
                        .Where(CanCastE));

                if (dashtargets.Any())
                {
                    var dash =
                        dashtargets.Where(x => x.IsValidTarget(475) /*&& MyEvade.Program.IsSafe(PosAfterE(x).ToMy2D()).IsSafe*/)
                            .MinOrDefault(x => PosAfterE(x).Distance(Game.CursorPos));

                    if (dash != null && dash.DistanceToPlayer() <= 475 && CanCastE(dash) &&
                        target.DistanceToPlayer() >= GapcloserDis && ObjectManager.GetLocalPlayer().IsFacing(dash) &&
                        (UnderTurret || !UnderTower(PosAfterE(dash))))
                        ObjectManager.GetLocalPlayer().SpellBook.CastSpell(SpellSlot.E, dash);
                }
            }
        }

        internal static bool CanMoveMent(this Obj_AI_Base target)
        {
            return !(target.MoveSpeed < 50) && !target.HasBuffOfType(BuffType.Stun) &&
                   !target.HasBuffOfType(BuffType.Fear) && !target.HasBuffOfType(BuffType.Snare) &&
                   !target.HasBuffOfType(BuffType.Knockup) && !target.HasBuff("recall") &&
                   !target.HasBuffOfType(BuffType.Knockback)
                   && !target.HasBuffOfType(BuffType.Charm) && !target.HasBuffOfType(BuffType.Taunt) &&
                   !target.HasBuffOfType(BuffType.Suppression) &&
                   !target.HasBuff("zhonyasringshield") && !target.HasBuff("bardrstasis");
        }

        internal static bool IsUnKillable(this Obj_AI_Base target)
        {
            if (target == null || target.IsDead || target.Health <= 0)
            {
                return true;
            }

            if (target.HasBuff("KindredRNoDeathBuff"))
            {
                return true;
            }

            if (target.HasBuff("UndyingRage") && target.GetBuff("UndyingRage").EndTime - Game.ClockTime > 0.3 &&
                target.Health <= target.MaxHealth * 0.10f)
            {
                return true;
            }

            if (target.HasBuff("JudicatorIntervention"))
            {
                return true;
            }

            if (target.HasBuff("ChronoShift") && target.GetBuff("ChronoShift").EndTime - Game.ClockTime > 0.3 &&
                target.Health <= target.MaxHealth * 0.10f)
            {
                return true;
            }

            if (target.HasBuff("VladimirSanguinePool"))
            {
                return true;
            }

            if (target.HasBuff("ShroudofDarkness"))
            {
                return true;
            }

            if (target.HasBuff("SivirShield"))
            {
                return true;
            }

            if (target.HasBuff("itemmagekillerveil"))
            {
                return true;
            }

            return target.HasBuff("FioraW");
        }

        internal static bool CanCastDelayR(Obj_AI_Hero target)
        {
            var buff = target.Buffs.FirstOrDefault(i => i.Type == BuffType.Knockback || i.Type == BuffType.Knockup);
            return buff != null && buff.EndTime - Game.ClockTime <= (buff.EndTime - buff.StartTime) / 3;
        }

        internal static bool CanCastE(Obj_AI_Base target)
        {
            return !target.HasBuff("YasuoDashWrapper");
        }

        internal static bool UnderTower(Vector3 pos)
        {
            return pos.PointUnderEnemyTurret();
        }

        internal static Vector3 PosAfterE(Obj_AI_Base target)
        {
            if (target.IsValidTarget())
            {
                return ObjectManager.GetLocalPlayer().IsFacing(target)
                    ? ObjectManager.GetLocalPlayer().ServerPosition.Extend(target.ServerPosition, 475f)
                    : ObjectManager.GetLocalPlayer().ServerPosition.Extend(Prediction.GetPrediction(target, 0.35f).CastPosition, 475f);
            }

            return Vector3.Zero;
        }
    }
}