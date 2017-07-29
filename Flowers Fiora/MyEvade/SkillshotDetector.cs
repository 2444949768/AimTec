﻿// Copyright 2014 - 2014 Esk0r
// SkillshotDetector.cs is part of Evade.
// 
// Evade is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Evade is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Evade. If not, see <http://www.gnu.org/licenses/>.

// GitHub: https://github.com/Esk0r/LeagueSharp/blob/master/Evade

namespace Flowers_Fiora.MyEvade
{
    #region

    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Util.Cache;

    using Flowers_Fiora.MyCommon;

    using System;
    using System.Linq;
    using System.Text.RegularExpressions;

    #endregion

    internal static class SkillshotDetector
    {
        public delegate void OnDeleteMissileH(Skillshot skillshot, MissileClient missile);
        public delegate void OnDetectSkillshotH(Skillshot skillshot);
        public static event OnDetectSkillshotH OnDetectSkillshot;
        public static event OnDeleteMissileH OnDeleteMissile;

        static SkillshotDetector()
        {
            Obj_AI_Base.OnProcessSpellCast += OnProcessSpellCast;
            GameObject.OnDestroy += MissileOnDelete;
            GameObject.OnCreate += MissileOnCreate;
            GameObject.OnCreate += OnCreate;
            GameObject.OnDestroy += OnDelete;
        }

        private static void OnCreate(GameObject sender)
        {
            var spellData = SpellDatabase.GetBySourceObjectName(sender.Name);

            if (spellData == null)
            {
                return;
            }
            
            if (EvadeManager.SkillShotMenu["Evade" + spellData.ChampionName.ToLower()]["Enabled" + spellData.MenuItemName].As<MenuBool>() == null)
            {
                return;
            }

            TriggerOnDetectSkillshot(DetectionType.ProcessSpell, spellData, Utils.GameTimeTickCount - Game.Ping/2,
                sender.Position.To2D(), sender.Position.To2D(), sender.Position.To2D(),
                GameObjects.Heroes.MinOrDefault(h => h.IsAlly ? 1 : 0));
        }

        private static void OnDelete(GameObject sender)
        {
            if (!sender.IsValid)
            {
                return;
            }

            for (var i = EvadeManager.DetectedSkillshots.Count - 1; i >= 0; i--)
            {
                var skillshot = EvadeManager.DetectedSkillshots[i];

                if (skillshot.SpellData.ToggleParticleName != "" &&
                    new Regex(skillshot.SpellData.ToggleParticleName).IsMatch(sender.Name))
                {
                    EvadeManager.DetectedSkillshots.RemoveAt(i);
                }
            }
        }

        private static void MissileOnCreate(GameObject sender)
        {
            var missile = sender as MissileClient;

            if (missile == null || !missile.IsValid)
            {
                return;
            }

            var unit = missile.SpellCaster as Obj_AI_Hero;

            if (unit == null || !unit.IsValid || unit.Team == ObjectManager.GetLocalPlayer().Team )
            {
                return;
            }

            var spellData = SpellDatabase.GetByMissileName(missile.SpellData.Name);

            if (spellData == null)
            {
                return;
            }

            Aimtec.SDK.Util.DelayAction.Queue(0, delegate
            {
                ObjSpellMissionOnOnCreateDelayed(sender);
            });
        }

        private static void ObjSpellMissionOnOnCreateDelayed(GameObject sender)
        {
            var missile = sender as MissileClient;

            if (missile == null || !missile.IsValid)
            {
                return;
            }

            var unit = missile.SpellCaster as Obj_AI_Hero;

            if (unit == null || !unit.IsValid || unit.Team == ObjectManager.GetLocalPlayer().Team)
            {
                return;
            }

            var spellData = SpellDatabase.GetByMissileName(missile.SpellData.Name);

            if (spellData == null)
            {
                return;
            }

            var missilePosition = missile.Position.To2D();
            var unitPosition = missile.StartPosition.To2D();
            var endPos = missile.EndPosition.To2D();
            var direction = (endPos - unitPosition).Normalized();

            if (unitPosition.Distance(endPos) > spellData.Range || spellData.FixedRange)
            {
                endPos = unitPosition + direction * spellData.Range;
            }

            if (spellData.ExtraRange != -1)
            {
                endPos = endPos +
                         Math.Min(spellData.ExtraRange, spellData.Range - endPos.Distance(unitPosition)) * direction;
            }

            var castTime = Utils.GameTimeTickCount - Game.Ping / 2 - (spellData.MissileDelayed ? 0 : spellData.Delay) -
                           (int)(1000f * missilePosition.Distance(unitPosition) / spellData.MissileSpeed);

            TriggerOnDetectSkillshot(DetectionType.RecvPacket, spellData, castTime, unitPosition, endPos, endPos, unit);
        }

        private static void MissileOnDelete(GameObject sender)
        {
            var missile = sender as MissileClient;

            if (missile == null || !missile.IsValid)
            {
                return;
            }

            var caster = missile.SpellCaster as Obj_AI_Hero;

            if (caster == null || !caster.IsValid || caster.Team == ObjectManager.GetLocalPlayer().Team)
            {
                return;
            }

            var spellName = missile.SpellData.Name;

            if (OnDeleteMissile != null)
            {
                foreach (var skillshot in EvadeManager.DetectedSkillshots)
                {
                    if (
                        skillshot.SpellData.MissileSpellName.Equals(spellName,
                            StringComparison.InvariantCultureIgnoreCase) && skillshot.Unit.NetworkId == caster.NetworkId &&
                        (missile.EndPosition.To2D() - missile.StartPosition.To2D()).AngleBetween(skillshot.Direction) <
                        10 && skillshot.SpellData.CanBeRemoved)
                    {
                        OnDeleteMissile(skillshot, missile);
                        break;
                    }
                }
            }

            EvadeManager.DetectedSkillshots.RemoveAll(
                skillshot =>
                    (skillshot.SpellData.MissileSpellName.Equals(spellName, StringComparison.InvariantCultureIgnoreCase) ||
                     skillshot.SpellData.ExtraMissileNames.Contains(spellName, StringComparer.InvariantCultureIgnoreCase)) &&
                    (skillshot.Unit.NetworkId == caster.NetworkId &&
                     (missile.EndPosition.To2D() - missile.StartPosition.To2D()).AngleBetween(skillshot.Direction) < 10 &&
                     skillshot.SpellData.CanBeRemoved || skillshot.SpellData.ForceRemove));
        }

        internal static void TriggerOnDetectSkillshot(DetectionType detectionType, SpellData spellData, int startT, Vector2 start, Vector2 end, Vector2 originalEnd, Obj_AI_Base unit)
        {
            var skillshot = new Skillshot(detectionType, spellData, startT, start, end, unit)
            {
                OriginalEnd = originalEnd
            };

            OnDetectSkillshot?.Invoke(skillshot);
        }

        private static void OnProcessSpellCast(Obj_AI_Base sender, Obj_AI_BaseMissileClientDataEventArgs args)
        {
            if (sender == null || !sender.IsValid)
            {
                return;
            }

            if (args.SpellData.Name == "dravenrdoublecast")
            {
                EvadeManager.DetectedSkillshots.RemoveAll(
                    s => s.Unit.NetworkId == sender.NetworkId && s.SpellData.SpellName == "DravenRCast");
            }

            if (!sender.IsValid || sender.Team == ObjectManager.GetLocalPlayer().Team)
            {
                return;
            }

            var spellData = SpellDatabase.GetByName(args.SpellData.Name);

            if (spellData == null)
            {
                return;
            }

            var startPos = new Vector2();

            if (spellData.FromObject != "")
            {
                foreach (var o in ObjectManager.Get<GameObject>())
                {
                    if (o.Name.Contains(spellData.FromObject))
                    {
                        startPos = o.Position.To2D();
                    }
                }
            }
            else
            {
                startPos = sender.ServerPosition.To2D();
            }

            if (spellData.FromObjects != null && spellData.FromObjects.Length > 0)
            {
                foreach (var obj in ObjectManager.Get<GameObject>())
                {
                    if (obj.IsEnemy && spellData.FromObjects.Contains(obj.Name))
                    {
                        var start = obj.Position.To2D();
                        var end = start + spellData.Range * (args.End.To2D() - obj.Position.To2D()).Normalized();

                        TriggerOnDetectSkillshot(
                            DetectionType.ProcessSpell, spellData, Utils.GameTimeTickCount - Game.Ping / 2, start, end, end,
                            sender);
                    }
                }
            }

            if (!startPos.IsValid())
            {
                return;
            }

            var endPos = args.End.To2D();

            if (spellData.SpellName == "LucianQ" && args.Target != null &&
                args.Target.NetworkId == ObjectManager.GetLocalPlayer().NetworkId)
            {
                return;
            }

            var direction = (endPos - startPos).Normalized();

            if (startPos.Distance(endPos) > spellData.Range || spellData.FixedRange)
            {
                endPos = startPos + direction * spellData.Range;
            }

            if (spellData.ExtraRange != -1)
            {
                endPos = endPos +
                         Math.Min(spellData.ExtraRange, spellData.Range - endPos.Distance(startPos)) * direction;
            }

            TriggerOnDetectSkillshot(
                DetectionType.ProcessSpell, spellData, Utils.GameTimeTickCount - Game.Ping/2, startPos, endPos,
                args.End.To2D(), sender);
        }
    }
}
