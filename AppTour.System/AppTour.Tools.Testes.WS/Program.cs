using System;
using AppTour.Tools.Testes.WS.WS;
namespace AppTour.Tools.Testes.WS
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WS.AppTourWSClient ws = new WS.AppTourWSClient())
            {
                ws.Open();
                //UserModel user = ws.Authentication("rjcarneiro", "efg");
                UserModel user = ws.Authentication("pedroluisf", "abc");
                Console.WriteLine("Output => {0}", user.RealName);
                Console.ReadKey();
                ws.Close();
            }
        }
    }
}
