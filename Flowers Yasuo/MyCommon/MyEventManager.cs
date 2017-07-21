using Aimtec;
using Aimtec.SDK;

namespace Flowers_Yasuo.MyCommon
{
    #region

    using Flowers_Yasuo.MyBase;

    using System;
    using Aimtec.SDK.Orbwalking;

    #endregion

    internal class MyEventManager : MyLogic
    {
        internal static void Initializer()
        {
            try
            {
                Game.OnUpdate += OnUpdate;
                BuffManager.OnAddBuff += OnAddBuff;
                SpellBook.OnStopCast += OnStopCast;
                Obj_AI_Base.OnProcessSpellCast += OnProcessSpellCast;
                Obj_AI_Base.OnPlayAnimation += OnPlayAnimation;
                Orbwalker.PostAttack += OnPostAttack;
                //Interrupt
                //Gapcloser
                Render.OnRender += OnRender;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MyEventManager.Initializer." + ex);
            }
        }

        private static void OnStopCast(Obj_AI_Base sender, SpellBookStopCastEventArgs Args)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MyEventManager.OnAddBuff." + ex);
            }
        }

        private static void OnAddBuff(Obj_AI_Base sender, Buff buff)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MyEventManager.OnAddBuff." + ex);
            }
        }

        private static void OnUpdate()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MyEventManager.OnUpdate." + ex);
            }
        }

        private static void OnProcessSpellCast(Obj_AI_Base sender, Obj_AI_BaseMissileClientDataEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MyEventManager.OnProcessSpellCast." + ex);
            }
        }

        private static void OnPlayAnimation(Obj_AI_Base sender, Obj_AI_BasePlayAnimationEventArgs Args)
        {
            try
            {

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

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MyEventManager.OnRender." + ex);
            }
        }
    }
}