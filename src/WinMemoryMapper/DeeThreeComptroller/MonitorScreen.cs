using D3MemDataLayer;
using D3MemDataLayer.Constants;
using LogicServiceLib;
using SendInputLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public MonitorScreen(ObjectManager data, ISendMessageService inputService, ILogicService logic) 
        {
            InitializeComponent();
            this.DataService = data;
            this.InputService = inputService;
            this.LogicService = logic;
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
        /// Handles the Click event of the AbortLogicButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void AbortLogicButton_Click(object sender, EventArgs e)
        {
            this.LogicService.AbortLogic();
        }

        /// <summary>
        /// Handles the Click event of the StartLogicButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void StartLogicButton_Click(object sender, EventArgs e)
        {
            this.LogicService.RunLogic();
        }

        
    }
}
