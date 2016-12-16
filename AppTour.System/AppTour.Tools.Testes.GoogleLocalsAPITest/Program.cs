using System;

namespace AppTour.Tools.Testes.GoogleLocalsAPITest
{
    class Program
    {

        static void Main(string[] args)
        {

            GooglePlacesAPI api = new GooglePlacesAPI();
            api.Start();

            Console.ReadKey();
        }
        public static void WriteInConsole(string value)
        {
            Console.WriteLine(value);
        }

    }
}
