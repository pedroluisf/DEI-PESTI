using System;
using System.Windows.Forms;

namespace AppTour.UI.Agents.AppTourAgents
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(Agents.Instance);
        }
    }
}
