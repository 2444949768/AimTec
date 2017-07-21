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
                var GetUserMessage = new Aimtec.AppDomain.Remoting.Messages.AccountDetails();
                if (GetUserMessage.Username.Contains("cjshu") || GetUserMessage.Username.Contains("xiaojun"))
                {
                    System.Console.WriteLine("脚本拒绝狗使用");
                    return;
                }

                if (ObjectManager.GetLocalPlayer().ChampionName != "Yasuo")
                {
                    return;
                }
            
                var YasuoLoader = new MyBase.MyChampions();
            };
        }
    }
}
