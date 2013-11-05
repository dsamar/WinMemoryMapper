using D3MemDataLayer;
using D3MemDataLayer.Constants;
using LogicServiceLib;
using SendInputLib;
using Syringe;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace DeeThreeComptroller
{
    public partial class MonitorScreen : Form
    {
        public ISendMessageService InputService { get; set; }

        public ObjectManager DataService { get; set; }

        public ILogicService LogicService { get; set; }

        public MonitorScreen()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitorScreen"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="inputService">The input service.</param>
        /// <param name="logic">The logic.</param>
        public MonitorScreen(ObjectManager data, ISendMessageService inputService, ILogicService logic) 
        {
            InitializeComponent();
            this.DataService = data;
            this.InputService = inputService;
            this.LogicService = logic;

            this.RefreshWorker = new BackgroundWorker();
            this.RefreshWorker.DoWork += Refresh;
            this.RefreshWorker.RunWorkerAsync();
        }

        private void RefreshAllButton_Click(object sender, EventArgs e)
        {
            this.CharacterNameTextBox.Text = this.DataService.Player.CharacterName;
            this.ClassTextBox.Text = this.DataService.Player.CharacterClass.ToString();
            this.LevelAreaTextBox.Text = this.DataService.Player.LevelArea.DisplayName;
            this.ACDCountTextBox.Text = this.DataService.ACDs.Count.ToString();

            this.ACDListBox.Items.Clear();
            foreach (var acd in this.DataService.ACDs.List)
            {
                this.ACDListBox.Items.Add(acd);
            }
        }

        /// <summary>
        /// Refreshes the specified sender.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DoWorkEventArgs"/> instance containing the event data.</param>
        private void Refresh(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                var acd = this.LogicService.GameService.GetTownWaypoint();
                var quad = acd != null ? acd.QuadrantFromPlayer.ToString() : "Not Found";
                var angle = acd != null ? acd.AngleFromPlayer.ToString() : "Not Found";
                this.SetText(this.TargetQuadTextBox, quad);
                this.SetText(this.TargetAngleTextBox, angle);
                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// Delegate Callback
        /// </summary>
        /// <param name="tb">The tb.</param>
        /// <param name="text">The text.</param>
        delegate void SetTextCallback(TextBox tb, string text);

        /// <summary>
        /// Sets the text.
        /// </summary>
        /// <param name="tb">The tb.</param>
        /// <param name="text">The text.</param>
        private void SetText(TextBox tb, string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (tb.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { tb, text });
            }
            else
            {
                tb.Text = text;
            }
        }

        /// <summary>
        /// Handles the Click event of the AbortLogicButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void AbortLogicButton_Click(object sender, EventArgs e)
        {
            this.LogicService.AbortLogic();
            this.InputService.ClearCursor();
        }

        /// <summary>
        /// Handles the Click event of the StartLogicButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void StartLogicButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.InputService.PInjector.InjectLibrary("Stub.dll");
                this.InputService.PInjector.CallExport("Stub.dll", "ApplyHook");
            }
            catch
            {
                Console.WriteLine("Injection Error");
            }            
            this.LogicService.RunLogic();
        }

        public BackgroundWorker RefreshWorker { get; set; }
    }
}
