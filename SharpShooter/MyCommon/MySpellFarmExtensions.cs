using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aimtec;
using Aimtec.SDK.Extensions;

namespace SharpShooter.MyCommon
{
    internal static class MySpellFarmExtensions
    {
        public struct FarmPosition
        {
            public Vector3 CastPosition { get; set; }
            public int HitCount { get; set; }
        }

        public static FarmPosition GetSpellFarmPosition(this Aimtec.SDK.Spell spell, IEnumerable<Obj_AI_Base> minionList,
            float extraRange = 0,
            Vector3? source = null)
        {
            if (spell.Type == Aimtec.SDK.Prediction.Skillshots.SkillshotType.Line)
            {
                return GetLineFarmPosition(spell, minionList, extraRange, source);
            }

            return new FarmPosition();
        }

        public static FarmPosition GetLineFarmPosition(Aimtec.SDK.Spell spell, IEnumerable<Obj_AI_Base> minionList, float extraRange = 0,
            Vector3? source = null)
        {
            if (ObjectManager.GetLocalPlayer().SpellBook.GetSpell(spell.Slot).Level == 0 ||
                !ObjectManager.GetLocalPlayer().SpellBook.CanUseSpell(spell.Slot))
            {
                return new FarmPosition();
            }

            var range = spell.Range + extraRange;
            var sourcePosition = source?.To2D() ?? ObjectManager.GetLocalPlayer().ServerPosition.To2D();
            var minions = minionList.Where(x => x.ServerPosition.DistanceSquared(sourcePosition) <= range*range).ToArray();

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
                                x.ServerPosition.To2D().DistanceSquared(sourcePosition) <= range * range)
                        .Count(
                            x => x.ServerPosition.To2D().DistanceSquared(sourcePosition, endPos, true) <= spell.Width * spell.Width);

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
