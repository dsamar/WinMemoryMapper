using Syringe;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SendInputLib
{
    /// <summary>
    /// Service used to send input to another process
    /// </summary>
    public interface ISendMessageService
    {
        /// <summary>
        /// Gets the win rectangle.
        /// </summary>
        /// <value>
        /// The win rectangle.
        /// </value>
        Rectangle WinRectangle { get; }

        /// <summary>
        /// Gets or sets the injector.
        /// </summary>
        /// <value>
        /// The injector.
        /// </value>
        Injector PInjector { get; set; }

        /// <summary>
        /// Sends the key stroke.
        /// </summary>
        /// <param name="k">The k.</param>
        void SendKeyStroke(uint k);

        /// <summary>
        /// Sends the key stroke.
        /// </summary>
        /// <param name="k">The k.</param>
        /// <param name="delay">The delay. Note: the delay will always be randomly shifted +/- 5 MS</param>
        void SendKeyStroke(uint k, int delay);

        /// <summary>
        /// Sends the left click.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        void SendLeftClick(int x, int y);

        /// <summary>
        /// Sends the right click.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        void SendRightClick(int x, int y);

        /// <summary>
        /// Sends the mouse move.
        /// </summary>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        void SendMouseMove(int p1, int p2);

        /// <summary>
        /// Clears the cursor.
        /// </summary>
        void ClearCursor();

        /// <summary>
        /// Sets the keyboard modifier.
        /// </summary>
        /// <param name="keys">The keys.</param>
        void SetKeyboardModifier(uint keys);

        /// <summary>
        /// Unsets the keyboard modifier.
        /// </summary>
        /// <param name="keys">The keys.</param>
        void UnsetKeyboardModifier(uint keys); 
    }
}
