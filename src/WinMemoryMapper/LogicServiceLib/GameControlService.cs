using D3MemDataLayer;
using D3MemDataLayer.Constants;
using SendInputLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LogicServiceLib
{
    public class GameControlService
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GameControlService"/> class.
        /// </summary>
        /// <param name="dataService">The data service.</param>
        /// <param name="inputService">The input service.</param>
        public GameControlService(ObjectManager dataService, ISendMessageService inputService)
        {
            this.Data = dataService;
            this.Input = inputService;
            this.LearningDB = new LearningDB("test");
        }

        /// <summary>
        /// Gets or sets the input.
        /// </summary>
        /// <value>
        /// The input.
        /// </value>
        public ISendMessageService Input { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public ObjectManager Data { get; set; }

        /// <summary>
        /// Gets or sets the learning database.
        /// </summary>
        /// <value>
        /// The learning database.
        /// </value>
        public LearningDB LearningDB { get; set; }

        /// <summary>
        /// Gets the attribute helper.
        /// </summary>
        /// <param name="acd_FormulaMapData">The acd_ formula map data.</param>
        /// <param name="acd_FormulaMapMask">The acd_ formula map mask.</param>
        /// <param name="AttributeIndex">Index of the attribute.</param>
        /// <param name="AttributeMask">The attribute mask.</param>
        /// <returns></returns>
        public T GetAttribHelper<T>(int acd_FormulaMapData, int acd_FormulaMapMask, int AttributeIndex, uint AttributeMask, T defaultValue = default(T))
        {
            uint full_id = (uint)AttributeIndex | (AttributeMask << 12);
            uint idxmask = full_id ^ (full_id >> 16);
            int idx = (int)(acd_FormulaMapMask & idxmask);

            int link_root_address = acd_FormulaMapData + idx * 4;
            int link_address = this.Data.Mapper.Read<int>((uint)link_root_address);
            int n = 0;
            while ((link_address != 0) && (n < 20))
            {
                uint id = this.Data.Mapper.Read<uint>((uint)link_address + Offsets.attrib_link_ofs__id);
                if (id == full_id)
                {
                    var v = this.Data.Mapper.Read<T>((uint)link_address + Offsets.attrib_link_ofs__value);
                    return v;
                }
                link_address = this.Data.Mapper.Read<int>((uint)link_address + Offsets.attrib_link_ofs__next);
                n += 1;
            }
            return defaultValue;
        }

        /// <summary>
        /// Gets the team of the acd.
        /// </summary>
        /// <param name="acd">The acd.</param>
        /// <returns></returns>
        public int GetTeam(ACD acd)
        {
            try
            {
                var enemyAttrGroup = acd.MatchingAttribGroup();
                var attr = AttributeCodes.Instance.GetAttributeByName("TeamID");
                return this.GetAttribHelper<int>((int)enemyAttrGroup.FormulaMapDataPtr, (int)enemyAttrGroup.FormulaMapMask, (int)attr.Index, 0xFFFFFFFF, -1);
            }
            catch
            {
                return -1;
            }            
        }

        /// <summary>
        /// Determines whether the specified acd is targetable.
        /// </summary>
        /// <param name="acd">The acd.</param>
        /// <returns></returns>
        public bool IsTargetable(ACD acd)
        {
            try
            {
                var enemyAttrGroup = acd.MatchingAttribGroup();
                var attr = AttributeCodes.Instance.GetAttributeByName("Untargetable");
                var value = this.GetAttribHelper<int>((int)enemyAttrGroup.FormulaMapDataPtr, (int)enemyAttrGroup.FormulaMapMask, (int)attr.Index, 0xFFFFFFFF, -1);
                return value == 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Selects the nearest enemy.
        /// </summary>
        /// <returns></returns>
        public ACD SelectNearestEnemy()
        {            
            var playerActorId = this.Data.Player.Actor_ID;
            var player = this.Data.RActors.List.SingleOrDefault(m => m.ActorId == playerActorId);

            var enemy = this.Data.ACDs.List
                    .Where(m => 
                        m.ActorId != player.ActorId && 
                        m.GameBalanceType != 2 &&
                        m.DistanceFromPlayer < 70 &&
                        this.GetTeam(m) == 10 &&
                        this.GetInvuln(m) != 1 &&
                        this.GetCurHitpoints(m) > 1)
                    .OrderBy(m => m.DistanceFromPlayer).FirstOrDefault();

            return enemy;
        }

        /// <summary>
        /// Gets the current hitpoints.
        /// </summary>
        /// <param name="acd">The acd.</param>
        /// <returns></returns>
        private float GetCurHitpoints(ACD acd)
        {
            try
            {
                var enemyAttrGroup = acd.MatchingAttribGroup();
                var attr = AttributeCodes.Instance.GetAttributeByName("Hitpoints_Cur");
                return this.GetAttribHelper<float>((int)enemyAttrGroup.FormulaMapDataPtr, (int)enemyAttrGroup.FormulaMapMask, (int)attr.Index, 0xFFFFFFFF, 0);
            }
            catch
            {
                return 0;
            }  
        }

        /// <summary>
        /// Gets the invuln.
        /// </summary>
        /// <param name="acd">The acd.</param>
        /// <returns></returns>
        private int GetInvuln(ACD acd)
        {
            try
            {
                var enemyAttrGroup = acd.MatchingAttribGroup();
                var attr = AttributeCodes.Instance.GetAttributeByName("Invulnerable");
                return this.GetAttribHelper<int>((int)enemyAttrGroup.FormulaMapDataPtr, (int)enemyAttrGroup.FormulaMapMask, (int)attr.Index, 0xFFFFFFFF, -1);
            }
            catch
            {
                return -1;
            }       
        }

        /// <summary>
        /// Moves towards the target.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="distance">The distance. In screen coordinates</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void MoveTowards(ACD target)
        {
            var xyCoords = GetCoordsOf(target);
            if (xyCoords != null)
            {
                this.Input.SendLeftClick(xyCoords.Item1, xyCoords.Item2);
            }
            else
            {
                this.Input.ClearCursor();
            }
        }

        /// <summary>
        /// Gets the coords of.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        private Tuple<int, int> GetCoordsOf(ACD target)
        {
            if (target != null)
            {
                // Get center of screen coordinates (player)
                var playerScreen_x = (this.Input.WinRectangle.Width - this.Input.WinRectangle.Left) / 2;
                var playerScreen_y = (this.Input.WinRectangle.Height - this.Input.WinRectangle.Top) / 2;

                var d = 0;
                if (target.DistanceFromPlayer < 35)
                {
                    d = Convert.ToInt32(target.DistanceFromPlayerScreen);
                    // Save Feedback
                    this.LearningDB.SaveTargetPos(target.Pos);
                    var player = this.Data.RActors.List.Single(m => m.ActorId == this.Data.Player.Actor_ID);
                    if (player != null) this.LearningDB.SavePlayerPos(player.Pos);
                }
                else
                {
                    var maxDistance = (this.Input.WinRectangle.Height - this.Input.WinRectangle.Top) / 3;
                    d = maxDistance;
                }

                var angle = target.AngleFromPlayer;
                var clickX = playerScreen_x + (d * Math.Cos(angle * Math.PI / 180));
                var clickY = playerScreen_y + (d * Math.Sin(angle * Math.PI / 180));

                return new Tuple<int, int>(Convert.ToInt32(clickX), Convert.ToInt32(clickY));
            }

            return null;
        }

        /// <summary>
        /// Gets the tristram waypoint.
        /// </summary>
        /// <returns></returns>
        public ACD GetTownWaypoint()
        {
            var waypoint = this.Data.ACDs.List
                    .Where(m =>
                        m.GameBalanceType != 2 &&
                        m.Name.Contains("Waypoint_Town"));

            return waypoint.FirstOrDefault();
        }

        /// <summary>
        /// Attacks the secondary.
        /// </summary>
        /// <param name="target">The target.</param>
        public void AttackSecondary(ACD target)
        {
            var xyCoords = GetCoordsOf(target);
            if (xyCoords != null)
            {
                if (target.DistanceFromPlayer < 10 ) this.Input.SetKeyboardModifier((uint)Keys.LShiftKey);
                this.Input.SendRightClick(xyCoords.Item1, xyCoords.Item2);
                if (target.DistanceFromPlayer < 10) this.Input.UnsetKeyboardModifier((uint)Keys.LShiftKey);
            }
            else
            {
                this.Input.ClearCursor();
            }
        }

        /// <summary>
        /// Hovers the over.
        /// </summary>
        /// <param name="enemy">The enemy.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void HoverOver(ACD target)
        {
            var xyCoords = GetCoordsOf(target);
            if (xyCoords != null)
            {
                this.Input.SendMouseMove(xyCoords.Item1, xyCoords.Item2);
            }
            else
            {
                this.Input.ClearCursor();
            }
        }

        public ACD SelectNearestPC()
        {
            var playerActorId = this.Data.Player.Actor_ID;
            var player = this.Data.RActors.List.SingleOrDefault(m => m.ActorId == playerActorId);

            var enemy = this.Data.ACDs.List
                    .Where(m =>
                        m.ActorId != player.ActorId &&
                        m.GameBalanceType == 7 &&
                        this.GetTeam(m) == 2)
                    .OrderBy(m => m.DistanceFromPlayer).FirstOrDefault();

            return enemy;
        }
    }
}
