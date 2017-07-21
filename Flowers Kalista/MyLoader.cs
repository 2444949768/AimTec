namespace Flowers_Kalista
{
    #region 

    using Aimtec;

    #endregion

    internal class MyLoader
    {
        public static void Main()
        {
            Game.OnStart += delegate
            {
                if (ObjectManager.GetLocalPlayer().ChampionName != "Kalista")
                {
                    return;
                }

                var KalistaLoader = new MyBase.MyChampions();
            };
        }
    }
}
