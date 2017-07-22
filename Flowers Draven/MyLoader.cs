namespace Flowers_Draven
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
                if (ObjectManager.GetLocalPlayer().ChampionName != "Draven")
                {
                    return;
                }

                var DravenLoader = new MyBase.MyChampions();
            };
        }
    }
}
