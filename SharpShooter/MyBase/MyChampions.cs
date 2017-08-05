namespace SharpShooter.MyBase
{
    #region

    using Aimtec;
    using Aimtec.SDK.Menu;
    using Aimtec.SDK.Menu.Components;

    using SharpShooter.MyCommon;

    using System.Linq;

    #endregion

    internal class MyChampions
    {
        private static readonly string[] all = {"Corki", "Draven", "Graves"};

        public MyChampions()
        {
            Initializer();
        }

        private static void Initializer()
        {
            MyMenuExtensions.myMenu = new Menu("SharpShooter: " + ObjectManager.GetLocalPlayer().ChampionName,
                "SharpShooter: " + ObjectManager.GetLocalPlayer().ChampionName, true)
            {
                new MenuSeperator("MadebyNightMoon", "Made by NightMoon")
            };
            MyMenuExtensions.myMenu.Attach();

            var supportMenu = new Menu("SupportChampion", "Support Champion");
            {
                foreach (var name in all)
                {
                    supportMenu.Add(new MenuSeperator("SC_" + name, name));
                }
            }
            MyMenuExtensions.myMenu.Add(supportMenu);

            if (
                all.All(
                    x =>
                        !string.Equals(x, ObjectManager.GetLocalPlayer().ChampionName,
                            System.StringComparison.CurrentCultureIgnoreCase)))
            {
                MyMenuExtensions.myMenu.Add(
                    new MenuSeperator("NotSupport_" + ObjectManager.GetLocalPlayer().ChampionName,
                        "Not Support: " + ObjectManager.GetLocalPlayer().ChampionName));
                return;
            }

            MyLogic.Orbwalker = new Aimtec.SDK.Orbwalking.Orbwalker();
            MyLogic.Orbwalker.Attach(MyMenuExtensions.myMenu);

            MyMenuExtensions.DrawOption.SetDefalut();

            MyMenuExtensions.myMenu.Add(new MenuSeperator("ASDASDG"));

            switch (ObjectManager.GetLocalPlayer().ChampionName)
            {
                case "Corki":
                    var corkiPlugin = new MyPlugin.Corki();
                    break;
                case "Draven":
                    var dravenPlugin = new MyPlugin.Draven();
                    break;
                case "Graves":
                    var gravesPlugin = new MyPlugin.Graves();
                    break;
            }



        }
    }
}