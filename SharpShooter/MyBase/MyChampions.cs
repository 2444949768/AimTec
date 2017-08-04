using SharpShooter.MyCommon;

namespace SharpShooter.MyBase
{
    #region

    using Aimtec;
    using Aimtec.SDK.Menu;
    using Aimtec.SDK.Menu.Components;

    #endregion

    internal class MyChampions
    {
        private static readonly string[] all = {"Draven", "Kalista"};

        public MyChampions()
        {
            Initializer();
        }

        private static void Initializer()
        {
            MyMenuExtensions.myMenu = new Menu("SharpShooter: " + ObjectManager.GetLocalPlayer().ChampionName,
                "SharpShooter: " + ObjectManager.GetLocalPlayer().ChampionName, true);
            MyMenuExtensions.myMenu.Add(new MenuSeperator("MadebyNightMoon", "Made by NightMoon"));

            var supportMenu = new Menu("SupportChampion", "Support Champion");
            {
                foreach (var name in all)
                {
                    supportMenu.Add(new MenuSeperator("SC_" + name, name));
                }
            }
            MyMenuExtensions.myMenu.Add(supportMenu);

            MyLogic.Orbwalker = new Aimtec.SDK.Orbwalking.Orbwalker();
            MyLogic.Orbwalker.Attach(MyMenuExtensions.myMenu);

            MyMenuExtensions.DrawOption.SetDefalut();

            MyMenuExtensions.myMenu.Add(new MenuSeperator("ASDASDG"));

            switch (ObjectManager.GetLocalPlayer().ChampionName)
            {
                case "Draven":
                    var dravenPlugin = new MyPlugin.Draven();
                    break;
                case "Kalista":

                    break;
            }


            MyMenuExtensions.myMenu.Attach();
        }
    }
}