using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppTour.Tools.Testes.WSTester.AppTourWebService;

namespace AppTour.Tools.Testes.WSTester
{
    class Program
    {
        static void Main(string[] args)
        {
            using (AppTourWebService.AppTourWSClient ws = new AppTourWebService.AppTourWSClient())
            {
                ws.Open();

                UserModel user = ws.Authentication("rjcarneiro", "carneiro123@");

                ws.Close();
            }
        }
    }
}
