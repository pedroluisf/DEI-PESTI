using System;
using WebServicesTester.Login;

namespace WebServicesTester
{
    class Program
    {
        static void Main(string[] args)
        {
            HelloWorldService.HelloWorldClient ws = new HelloWorldService.HelloWorldClient();
            Console.WriteLine(ws.DoWork());
            Console.ReadLine();
            ws.Close();

            Login.LoginClient l = new LoginClient();

            try
            {
                UserModel user = l.Authentication("pedroluisf", "aramis");
                if (user == null)
                {
                    Console.WriteLine("UserName / Password Inválidos");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Hello " + user.RealName);
                    Console.ReadLine();
                }
                l.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.Read();
            }
        }

    }
}
