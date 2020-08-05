using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToe
{
    /// <summary>
    /// MainWindow.xaml 的邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members

        /// <summary>
        /// 儲存每個格子的狀態
        /// </summary>
        private MarkType[] mResults;

        /// <summary>
        /// 輪到玩家1 為 true，否則為 false
        /// </summary>
        private bool mPlayer1Turn;

        /// <summary>
        /// 遊戲結束時為 true
        /// </summary>
        private bool mGameEnded;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }

        #endregion

        /// <summary>
        /// 開始新遊戲並且清除格子內的內容
        /// </summary>
        private void NewGame()
        {
            mResults = new MarkType[9];

            for (var i = 0; i < mResults.Length; i++)
                mResults[i] = MarkType.Free;

            // 玩家1 先開始遊戲
            mPlayer1Turn = true;

            // 初始化每個格子的狀態
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            mGameEnded = false;
        }

        /// <summary>
        /// 處理 button 事件
        /// </summary>
        /// <param name="sender">被點擊的按鈕</param>
        /// <param name="e">點擊事件</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // 當遊戲已經結束時
            if (mGameEnded)
            {
                NewGame();
                return;
            }

            var button = (Button)sender;

            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            // 若按鈕已經有內容則不改變
            if (mResults[index] != MarkType.Free)
                return;

            // player1 為 X， player2 為 O
            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;
            button.Content = mPlayer1Turn ? "X" : "O";

            // 將 O 改為綠色
            if (!mPlayer1Turn)
                button.Foreground = Brushes.Red;

            // 切換玩家
            mPlayer1Turn ^= true;
           
            CheckForWinner();
        }

        /// <summary>
        /// 判斷是否有贏家
        /// </summary>
        private void CheckForWinner()
        {
            #region 水平

            // 檢查水平
            //
            //  - 列 0
            //
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                // 遊戲結束
                mGameEnded = true;

                // Highlight 贏的線條
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }
            //
            //  - 列 1
            //
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                mGameEnded = true;
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }
            //
            //  - 列 2
            //
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                mGameEnded = true;
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }

            #endregion

            #region 垂直

            // 檢查垂直
            //
            //  - 行 0
            //
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                mGameEnded = true;
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }
            //
            //  - 行1
            //
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                mGameEnded = true;
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }
            //
            //  - 行 2
            //
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                mGameEnded = true;
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }

            #endregion

            #region 對角

            // 檢查對角
            //
            //  - 左上右下
            //
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                mGameEnded = true;
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }
            //
            //  - 由上左下
            //
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                mGameEnded = true;
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
            }

            #endregion

            #region No Winners

            // 判斷是否有贏家
            if (!mResults.Any(f => f == MarkType.Free))
            {
                mGameEnded = true;
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Orange;
                });
            }

            #endregion
        }
    }
}