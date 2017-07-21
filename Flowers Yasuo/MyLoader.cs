namespace Flowers_Yasuo
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
                if (ObjectManager.GetLocalPlayer().ChampionName != "Yasuo")
                {
                    return;
                }
            
                var YasuoLoader = new MyBase.MyChampions();
            };
        }
    }
}
