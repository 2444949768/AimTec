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
        internal static bool IsFacing(this Obj_AI_Base unit, Vector3 position)
        {
            return unit != null && unit.IsValid &&
                   unit.Orientation.To2D().AngleBetween((position - unit.Position).To2D()) < 90;
        }

        internal static bool IsFacing(this Obj_AI_Base unit, Obj_AI_Base target)
        {
            return unit != null && target != null && unit.IsValid && target.IsValid &&
                   unit.Orientation.To2D().Perpendicular().AngleBetween((target.Position - unit.Position).To2D())
                   < 90;
        }

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

        internal static bool HaveQ3 => ObjectManager.GetLocalPlayer().HasBuff("YasuoQ3W");

        internal static bool IsMyDashing { get; set; }

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

        internal static Vector2 GetDashPos(Obj_AI_Base @base)
        {
            var predictedposition = ObjectManager.GetLocalPlayer().ServerPosition.Extend(@base.Position,
                ObjectManager.GetLocalPlayer().Distance(@base) + 475 - ObjectManager.GetLocalPlayer().Distance(@base));

            MyLogic.YasuolastEPos = predictedposition;

            return predictedposition.To2D();
        }

        internal static bool SafetyCheck(Obj_AI_Base unit)
        {
            //var pos = GetDashPos(unit);

            return true; //TODO//YasuoMenuInit.ComboETurret && MyEvade.Program.IsSafe(pos).IsSafe || !pos.PointUnderEnemyTurret();
        }

        internal static Obj_AI_Base GetNearObj(Obj_AI_Base target = null, bool inQCir = false)
        {
            var pos = target != null
                ? Prediction.GetPrediction(target, 0.75f, 0, 1025f).UnitPosition
                : Game.CursorPos;
            var obj = new List<Obj_AI_Base>();
            obj.AddRange(GameObjects.Minions.Where(x => x.IsValidTarget(475) && !x.IsAlly && x.IsMinion));
            obj.AddRange(GameObjects.EnemyHeroes.Where(i => i.IsValidTarget(475)));
            return
                obj.Where(
                        i =>
                            CanCastE(i) && pos.Distance(PosAfterE(i)) < (inQCir ? 250 : ObjectManager.GetLocalPlayer().Distance(pos))
                            /*&& MyEvade.Program.IsSafe(PosAfterE(i).ToMy2D()).IsSafe*/)
                    .MinOrDefault(i => pos.Distance(PosAfterE(i)));
        }

        internal static bool PointUnderEnemyTurret(this Vector2 Point)
        {
            var EnemyTurrets =
                ObjectManager.Get<Obj_AI_Turret>().ToList()
                    .Find(
                        t =>
                            t.IsEnemy &&
                            Vector2.Distance(Point, t.Position.To2D()) < 910f + ObjectManager.GetLocalPlayer().BoundingRadius);

            return EnemyTurrets != null;
        }

        internal static bool PointUnderEnemyTurret(this Vector3 Point)
        {
            var EnemyTurrets =
                ObjectManager.Get<Obj_AI_Turret>()
                    .Where(t => t.IsEnemy && Vector3.Distance(t.Position, Point) < 910f + ObjectManager.GetLocalPlayer().BoundingRadius);

            return EnemyTurrets.Any();
        }

        internal static void CastQ3()
        {
            var targets = GameObjects.EnemyHeroes.Where(x => x.IsValidTarget(1200)).ToArray();
            var castPos = Vector3.Zero;

            if (!targets.Any())
                return;

            foreach (var pred in
                targets.Select(i => MyLogic.Q3.GetPrediction(i))
                    .Where(
                        i => (int)i.HitChance >= 6 ||
                             (int)i.HitChance >= 5 && i.AoeTargetsHitCount > 1)
                    .OrderByDescending(i => i.AoeTargetsHitCount))
            {
                castPos = pred.CastPosition;
                break;
            }

            if (castPos != Vector3.Zero)
            {
                MyLogic.Q3.Cast(castPos);
            }
        }

        internal static void EGapTarget(Obj_AI_Hero target, bool UnderTurret, int GapcloserDis,
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

        internal static void EGapMouse(Obj_AI_Hero target, bool UnderTurret, int GapcloserDis,
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

        internal static void UseItems(Obj_AI_Base target, bool IsCombo = false)
        {
            //TODO
            //if (IsCombo)
            //{
            //    if (ObjectManager.GetLocalPlayer().HasItem(3153) && ObjectManager.GetLocalPlayer().Item(3153) &&
            //        target.DistanceToPlayer() >= ObjectManager.GetLocalPlayer().GetAutoAttackRange(target))
            //        Item.UseItem(3153, target);

            //    if (Item.HasItem(3143, ObjectManager.GetLocalPlayer()) && Item.CanUseItem(3143) &&
            //        ObjectManager.GetLocalPlayer().Distance(target.Position) <= 400)
            //        Item.UseItem(3143);

            //    if (Item.HasItem(3144, ObjectManager.GetLocalPlayer()) && Item.CanUseItem(3144) &&
            //        target.IsValidTarget(MyLogic.QSkillshot.Range))
            //        Item.UseItem(3144, target);

            //    if (Item.HasItem(3142, ObjectManager.GetLocalPlayer()) && Item.CanUseItem(3142) &&
            //        ObjectManager.GetLocalPlayer().Distance(target.Position) <= MyLogic.QSkillshot.Range)
            //        Item.UseItem(3142);
            //}

            //if (Item.HasItem(3074, ObjectManager.GetLocalPlayer()) && Item.CanUseItem(3074) &&
            //    ObjectManager.GetLocalPlayer().Distance(target.Position) <= 400)
            //    Item.UseItem(3074);

            //if (Item.HasItem(3077, ObjectManager.GetLocalPlayer()) && Item.CanUseItem(3077) &&
            //    ObjectManager.GetLocalPlayer().Distance(target.Position) <= 400)
            //    Item.UseItem(3077);
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
            return
                ObjectManager.Get<Obj_AI_Turret>()
                    .Any(turret => turret.IsEnemy && turret.Health > 1 && turret.IsValidTarget(950, true, true, pos));
        }

        internal static Vector3 PosAfterE(Obj_AI_Base target)
        {
            if (target.IsValidTarget())
                return ObjectManager.GetLocalPlayer().IsFacing(target)
                    ? ObjectManager.GetLocalPlayer().ServerPosition.Extend(target.ServerPosition, 475f)
                    : ObjectManager.GetLocalPlayer().ServerPosition.Extend(Prediction.GetPrediction(target, 0.35f).CastPosition, 475f);

            return Vector3.Zero;
        }
    }

}