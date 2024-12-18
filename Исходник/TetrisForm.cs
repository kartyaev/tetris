using System;
using System.Drawing;
using System.Windows.Forms;

namespace Tetris
{
    public partial class TetrisForm : Form
    {
        private GameBoard gameBoard;
        private Tetrus currentTetromino;
        private Timer gameTimer;
        private int score;
        private int difficulty;
        private int cellSize = 20;

        public TetrisForm(int difficulty)
        {
            InitializeComponent();
            this.difficulty = difficulty;
            this.score = 0;
            this.gameBoard = new GameBoard(20, 10);
            this.currentTetromino = Tetrus.GenerateRandomTetromino();
            this.gameTimer = new Timer();
            this.gameTimer.Interval = 1000 / difficulty;
            this.gameTimer.Tick += GameTimer_Tick;
            this.gameTimer.Start();
            this.KeyDown += TetrisForm_KeyDown;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            MoveTetrominoDown();
        }

        private void MoveTetrominoDown()
        {
            currentTetromino.Move(0, 1);

            if (!gameBoard.IsPositionValid(currentTetromino.Blocks))
            {
                currentTetromino.Move(0, -1);
                gameBoard.PlaceTetromino(currentTetromino.Blocks, 1);
                score += gameBoard.ClearFullRows() * 100;
                lblScore.Text = $"Очки: {score}";
                currentTetromino = Tetrus.GenerateRandomTetromino();
                if (!gameBoard.IsPositionValid(currentTetromino.Blocks))
                {
                    gameTimer.Stop();
                    MessageBox.Show("Игра окончена!");
                }
            }
            gamePanel.Invalidate();
        }

        private void TetrisForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    currentTetromino.Move(-1, 0);
                    if (!gameBoard.IsPositionValid(currentTetromino.Blocks))
                    {
                        currentTetromino.Move(1, 0);
                    }
                    break;
                case Keys.Right:
                    currentTetromino.Move(1, 0);
                    if (!gameBoard.IsPositionValid(currentTetromino.Blocks))
                    {
                        currentTetromino.Move(-1, 0);
                    }
                    break;
                case Keys.Up:
                    currentTetromino.Rotate();
                    if (!gameBoard.IsPositionValid(currentTetromino.Blocks))
                    {
                        currentTetromino.Rotate();
                        currentTetromino.Rotate();
                        currentTetromino.Rotate();
                    }
                    break;
                case Keys.Down:
                    MoveTetrominoDown();
                    break;
            }
            gamePanel.Invalidate();
        }

        private void GamePanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.Black, 0, 0, gamePanel.Width - 1, gamePanel.Height - 1);
            gameBoard.Draw(e.Graphics, cellSize);
            foreach (var block in currentTetromino.Blocks)
            {
                e.Graphics.FillRectangle(new SolidBrush(currentTetromino.Color), block.X * cellSize, block.Y * cellSize, cellSize, cellSize);
                e.Graphics.DrawRectangle(Pens.Black, block.X * cellSize, block.Y * cellSize, cellSize, cellSize);
            }
        }

        private void InitializeComponent()
        {
            this.lblScore = new System.Windows.Forms.Label();
            this.lblControls = new System.Windows.Forms.Label();
            this.gamePanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.Location = new System.Drawing.Point(220, 13);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(38, 13);
            this.lblScore.TabIndex = 0;
            this.lblScore.Text = "Очки: 0";
            // 
            // lblControls
            // 
            this.lblControls.AutoSize = true;
            this.lblControls.Location = new System.Drawing.Point(220, 40);
            this.lblControls.Name = "lblControls";
            this.lblControls.Size = new System.Drawing.Size(200, 65);
            this.lblControls.TabIndex = 1;
            this.lblControls.Text = "Управление:\n" +
                                "Влево: ←\n" +
                                "Вправо: →\n" +
                                "Поворот: ↑\n" +
                                "Ускорение: ↓";
            // 
            // gamePanel
            // 
            this.gamePanel.Location = new System.Drawing.Point(13, 13);
            this.gamePanel.Name = "gamePanel";
            this.gamePanel.Size = new System.Drawing.Size(200, 400);
            this.gamePanel.TabIndex = 2;
            this.gamePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.GamePanel_Paint);
            // 
            // TetrisForm
            // 
            this.ClientSize = new System.Drawing.Size(434, 461);
            this.Controls.Add(this.gamePanel);
            this.Controls.Add(this.lblControls);
            this.Controls.Add(this.lblScore);
            this.Name = "TetrisForm";
            this.Text = "Тетрис";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label lblControls;
        private System.Windows.Forms.Panel gamePanel;
    }
}

