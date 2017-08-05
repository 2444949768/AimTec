namespace SharpShooter.MyCommon
{
    #region

    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Prediction.Skillshots;

    using System.Collections.Generic;
    using System.Linq;

    #endregion

    internal static class MySpellFarmExtensions
    {
        public struct FarmPosition
        {
            public Vector3 CastPosition { get; set; }
            public int HitCount { get; set; }
        }

        public static FarmPosition GetSpellFarmPosition(this Aimtec.SDK.Spell spell, IEnumerable<Obj_AI_Base> minionList,
           bool addBoundRadius = true, float extraRange = 0, Vector3? source = null)
        {
            switch (spell.Type)
            {
                case SkillshotType.Circle:
                case SkillshotType.Cone:
                    return GetCircleFarmPosition(spell, minionList, addBoundRadius, extraRange, source);
                case SkillshotType.Line:
                    return GetLineFarmPosition(spell, minionList, addBoundRadius, extraRange, source);
            }

            return new FarmPosition();
        }

        public static FarmPosition GetCircleFarmPosition(Aimtec.SDK.Spell spell, IEnumerable<Obj_AI_Base> minionList,
            bool addBoundRadius = true, float extraRange = 0, Vector3? source = null)
        {
            if (ObjectManager.GetLocalPlayer().SpellBook.GetSpell(spell.Slot).Level == 0 ||
                !ObjectManager.GetLocalPlayer().SpellBook.CanUseSpell(spell.Slot))
            {
                return new FarmPosition();
            }

            var range = spell.Range + extraRange;
            var sourcePosition = source?.To2D() ?? ObjectManager.GetLocalPlayer().ServerPosition.To2D();
            var minions =
                minionList.Where(
                    x =>
                        x.ServerPosition.DistanceSquared(sourcePosition) <=
                        (range + (addBoundRadius ? x.BoundingRadius : 0)) *
                        (range + (addBoundRadius ? x.BoundingRadius : 0))).ToArray();

            if (minions.Length == 0)
            {
                return new FarmPosition();
            }

            if (minions.Length == 1)
            {
                return new FarmPosition
                {
                    CastPosition = spell.GetPrediction(minions[0]).UnitPosition,
                    HitCount = 1
                };
            }

            var positionList = new List<Vector2>();
            positionList.AddRange(minions.Where(x => !x.IsDead).Select(x => x.ServerPosition.To2D()));

            var resultPos = Vector2.Zero;
            var hitCount = 0;

            foreach (var pos in positionList.Where(p => p.DistanceSquared(sourcePosition) <= range * range))
            {
                var count =
                    minions.Where(
                            x =>
                                x.IsValidTarget() &&
                                x.ServerPosition.To2D().DistanceSquared(sourcePosition) <=
                                (range + (addBoundRadius ? x.BoundingRadius : 0)) *
                                (range + (addBoundRadius ? x.BoundingRadius : 0)))
                        .Count(x => x.ServerPosition.DistanceSquared(pos) <= spell.Width * spell.Width);

                if (count >= hitCount)
                {
                    resultPos = pos;
                    hitCount = count;
                }
            }

            return new FarmPosition
            {
                CastPosition = resultPos.To3D(),
                HitCount = hitCount
            };
        }

        public static FarmPosition GetLineFarmPosition(Aimtec.SDK.Spell spell, IEnumerable<Obj_AI_Base> minionList,
            bool addBoundRadius = false, float extraRange = 0, Vector3? source = null)
        {
            if (ObjectManager.GetLocalPlayer().SpellBook.GetSpell(spell.Slot).Level == 0 ||
                !ObjectManager.GetLocalPlayer().SpellBook.CanUseSpell(spell.Slot))
            {
                return new FarmPosition();
            }

            var range = spell.Range + extraRange;
            var sourcePosition = source?.To2D() ?? ObjectManager.GetLocalPlayer().ServerPosition.To2D();
            var minions =
                minionList.Where(
                    x =>
                        x.ServerPosition.DistanceSquared(sourcePosition) <=
                        (range + (addBoundRadius ? x.BoundingRadius : 0)) *
                        (range + (addBoundRadius ? x.BoundingRadius : 0))).ToArray();

            if (minions.Length == 0)
            {
                return new FarmPosition();
            }

            if (minions.Length == 1)
            {
                return new FarmPosition
                {
                    CastPosition = spell.GetPrediction(minions[0]).UnitPosition,
                    HitCount = 1
                };
            }

            var positionList = new List<Vector2>();
            positionList.AddRange(minions.Where(x => !x.IsDead).Select(x => x.ServerPosition.To2D()));

            var resultPos = Vector2.Zero;
            var hitCount = 0;
            foreach (var pos in positionList.Where(p => p.DistanceSquared(sourcePosition) <= range * range))
            {
                var endPos = sourcePosition + range * (pos - sourcePosition).Normalized();
                var count =
                    minions.Where(
                            x =>
                                x.IsValidTarget() &&
                                x.ServerPosition.To2D().DistanceSquared(sourcePosition) <=
                                (range + (addBoundRadius ? x.BoundingRadius : 0)) *
                                (range + (addBoundRadius ? x.BoundingRadius : 0)))
                        .Count(
                            x =>
                                x.ServerPosition.To2D().DistanceSquared(sourcePosition, endPos, true) <=
                                spell.Width * spell.Width);

                if (count >= hitCount)
                {
                    resultPos = endPos;
                    hitCount = count;
                }
            }

            return new FarmPosition
            {
                CastPosition = resultPos.To3D(),
                HitCount = hitCount
            };
        }
    }
}
