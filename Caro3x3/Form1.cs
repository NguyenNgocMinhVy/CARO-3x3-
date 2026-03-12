using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caro3x3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        bool playerTurn = true;
        Random rand = new Random();
        Button[] board;
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btThoatGame_Click(object sender, EventArgs e)
        {
            Form2 exit = new Form2();
            exit.Show();
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            board = new Button[]
            {
                button1,button2,button3,
                button4,button5,button6,
                button7,button8,button9,
            };
        }
        private void button_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Text == "" && playerTurn) 
            {
                btn.Text = "X"; 
                btn.Enabled = false; 

                // Kiểm tra xem người chơi có thắng không sau nước đi
                if (CheckWin("X"))
                {
                    MessageBox.Show("Bạn đã thắng!");
                    DisableAllButtons(); 
                    return; 
                }

                // Nếu chưa có người thắng, đến lượt máy tính
                playerTurn = false; 
                ComputerMove();
            }
        }

        private void ComputerMove()
        {
         
            List<int> availableCells = GetAvailableCells();

            int bestMove = FindWinningMove("O"); 
            if (bestMove != -1)
            {
                board[bestMove].Text = "O";
                board[bestMove].Enabled = false;
                playerTurn = true; 
                if (CheckWin("O"))
                {
                    MessageBox.Show("Máy tính đã thắng!");
                    DisableAllButtons();
                }
                return;
            }

            bestMove = FindWinningMove("X"); 
            if (bestMove != -1)
            {
                board[bestMove].Text = "O";
                board[bestMove].Enabled = false;
                playerTurn = true;
                if (CheckWin("O")) 
                {
                    MessageBox.Show("Máy tính đã thắng!");
                    DisableAllButtons();
                }
                return;
            }

            int centerIndex = 4; 
            if (availableCells.Contains(centerIndex) && board[centerIndex].Text == "")
            {
                board[centerIndex].Text = "O";
                board[centerIndex].Enabled = false;
                playerTurn = true;
                return;
            }

            if (availableCells.Count > 0)
            {
                int randomIndex = rand.Next(availableCells.Count);
                int moveIndex = availableCells[randomIndex];
                board[moveIndex].Text = "O";
                board[moveIndex].Enabled = false;
                playerTurn = true;
                if (CheckWin("O")) 
                {
                    MessageBox.Show("Máy tính đã thắng!");
                    DisableAllButtons();
                }
                return;
            }
            else
            {
                MessageBox.Show("Hòa cờ!");
                DisableAllButtons();
            }
        }

        private List<int> GetAvailableCells()
        {
            List<int> cells = new List<int>();
            for (int i = 0; i < board.Length; i++)
            {
                if (board[i].Text == "")
                {
                    cells.Add(i);
                }
            }
            return cells;
        }

        private bool CheckWin(string playerSymbol)
        {
            // Kiểm tra hàng ngang
            if (CheckLine(0, 1, 2, playerSymbol)) return true;
            if (CheckLine(3, 4, 5, playerSymbol)) return true;
            if (CheckLine(6, 7, 8, playerSymbol)) return true;

            // Kiểm tra hàng dọc
            if (CheckLine(0, 3, 6, playerSymbol)) return true;
            if (CheckLine(1, 4, 7, playerSymbol)) return true;
            if (CheckLine(2, 5, 8, playerSymbol)) return true;

            // Kiểm tra đường chéo
            if (CheckLine(0, 4, 8, playerSymbol)) return true;
            if (CheckLine(2, 4, 6, playerSymbol)) return true;

            return false;
        }

        private bool CheckLine(int index1, int index2, int index3, string playerSymbol)
        {
            return board[index1].Text == playerSymbol &&
                   board[index2].Text == playerSymbol &&
                   board[index3].Text == playerSymbol;
        }

        private int FindWinningMove(string playerSymbol)
        {
            
            for (int i = 0; i < board.Length; i++)
            {
                if (board[i].Text == "") 
                {
                    
                    board[i].Text = playerSymbol;

                    if (CheckWin(playerSymbol))
                    {
                        board[i].Text = ""; 
                        return i; 
                    }
                    else
                    {
                        board[i].Text = "";
                    }
                }
            }
            return -1; 
        }

        
        private void DisableAllButtons()
        {
            foreach (var btn in board)
            {
                btn.Enabled = false;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {

            foreach (var btn in board)
            {
                btn.Text = "";
                btn.Enabled = true;
            }
            playerTurn = true; 
        }
    }
}
