using System;
using System.Windows.Forms;
using AppTour.Agents.Service.Controller;
using AppTour.Agents.Service.Core;

namespace AppTour.UI.Agents.AppTourAgents
{
    public partial class Agents : Form
    {
        #region - Atributos
        AgentsController agent = new AgentsController();
        NoticeService notify = NoticeService.Instance;
        private object obj = new object();
        #endregion

        #region - Construtor
        private Agents()
        {
            InitializeComponent();

            Tracker tracker = new Tracker();
            notify.Subscribe(tracker);


        }
        #endregion

        #region + Instance
        private static readonly Agents _instance = new Agents();
        public static Agents Instance
        {
            get { return _instance; }
        }
        #endregion

        #region + WriteInConsole(string value)
        public void WriteInConsole(string value)
        {
            if (area.InvokeRequired)
            {
                area.Invoke(new MethodInvoker(delegate { area.AppendText(value); }));
            }
            else
            {
                area.AppendText(value);
            }
        }
        #endregion

        #region - void StartAgentProcess(object sender, EventArgs e)
        private void StartAgentProcess(object sender, EventArgs e)
        {
            agent.StartProcess();
        }
        #endregion
    }
    #region + Class Tracker : IObserver<NoticeService>
    public class Tracker : IObserver<NoticeService>
    {
        #region + void OnCompleted()
        public void OnCompleted()
        {
            Agents.Instance.WriteInConsole(DateTime.Now.Date + " - untracked");
        }
        #endregion

        #region + OnError(Exception error)
        public void OnError(Exception error)
        {
            //some error handling
        }
        #endregion

        #region + OnNext(NoticeService value)
        public void OnNext(NoticeService value)
        {
            Agents.Instance.WriteInConsole(Environment.NewLine + DateTime.Now + " - " + value.Message);
        }
        #endregion
    }
    #endregion
}
