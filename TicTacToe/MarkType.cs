using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    /// <summary>
    /// 遊戲中格子狀態
    /// </summary>
    public enum MarkType
    {
        /// <summary>
        /// 初始狀態
        /// </summary>
        Free,
        /// <summary>
        /// 狀態為 O
        /// </summary>
        Nought,
        /// <summary>
        /// 狀態為 X
        /// </summary>
        Cross
    }
}
