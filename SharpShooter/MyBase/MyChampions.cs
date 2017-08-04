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
            MyLogic.Menu = new Menu("SharpShooter", ":: SharpShooter", true);
            {
                MyLogic.Menu.Add(new MenuSeperator("MadebyNightMoon", "Made by NightMoon"));
                MyLogic.Menu.Add(new MenuSeperator("ASDASDF"));
            }

            var supportMenu = new Menu("SupportChampion", ":: Support Champion");
            {
                foreach (var name in all)
                {
                    supportMenu.Add(new MenuSeperator("SC_" + name, name));
                }
            }

            MyMenuExtensions.DrawOption.SetDefalut();

            switch (ObjectManager.GetLocalPlayer().ChampionName)
            {
                case "Draven":
                    var dravenPlugin = new MyPlugin.Draven();
                    break;
                case "Kalista":

                    break;
            }

            MyLogic.Orbwalker = new Aimtec.SDK.Orbwalking.Orbwalker();
            MyLogic.Orbwalker.Attach(MyLogic.Menu);

            MyLogic.Menu.Attach();
        }
    }
}