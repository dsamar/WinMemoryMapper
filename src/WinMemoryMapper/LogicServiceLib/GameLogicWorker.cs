using D3MemDataLayer;
using SendInputLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace LogicServiceLib
{
    public class GameLogicWorker
    {
        /// <summary>
        /// The _should stop
        /// </summary>
        private volatile bool _shouldStop;

        public GameControlService Service { get; set; }

        public GameLogicWorker(GameControlService service)
        {
            this.Service = service;
        }

        /// <summary>
        /// What do I do?
        /// </summary>
        public void WhatDo()
        {
            // Select Nearest Enemy
            var enemy = this.Service.SelectNearestEnemy();
            var otherPlayer = this.Service.SelectNearestPC();

            if (otherPlayer != null && otherPlayer.DistanceFromPlayer > 40)
            {
                this.Service.MoveTowards(otherPlayer);
            }
            else if (enemy != null && enemy.DistanceFromPlayer < 15)
            {
                // Attack Enemy
                this.Service.AttackSecondary(enemy);
            }
            else if (enemy != null)
            {
                // Move Towards Enemy
                this.Service.MoveTowards(enemy);
            }
            else if (otherPlayer != null)
            {
                this.Service.MoveTowards(otherPlayer);
            }
            
            //var playerScreen_x = (this.InputService.WinRectangle.Width - this.InputService.WinRectangle.Left) / 2;
            //var playerScreen_y = (this.InputService.WinRectangle.Height - this.InputService.WinRectangle.Top) / 2;

            //var waypoint = this.Data.RActors.List.Where(m => m.CharName.Contains("Waypoint_Town")).SingleOrDefault();
            //if (null != waypoint)
            //{
            //}
        }
        
        /// <summary>
        /// Starts the loop.
        /// </summary>
        public void StartLoop()
        {
            while (!_shouldStop)
            {
                this.WhatDo();
                Thread.Sleep(200);
            }
        }

        /// <summary>
        /// Requests the stop.
        /// </summary>
        public void RequestStop()
        {
            _shouldStop = true;
        }
    }
}
