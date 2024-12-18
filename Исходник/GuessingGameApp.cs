using System;
using System.Windows.Forms;
using Исходник;

namespace Game
{
    public partial class Form1 : Form
    {
        private double correctAnswer;
        private int remainingAttempts;
        private int totalAttempts;
        private int timeLeft;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblFormula.Text = "Формула: (ln(b))² / (cos(a) - 1)";
            lblAttempts.Text = "Осталось попыток: 0";
            lblTimer.Text = "Осталось времени: 0 сек";
            btnCheck.Visible = false; // Скрываем кнопку "Проверить" при загрузке формы
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            double a = InputValidator.GetDoubleFromTextBox(txtA, greaterThanZero: true);
            double b = InputValidator.GetDoubleFromTextBox(txtB, greaterThanZero: true);

            if (a == 0 || b == 0)
            {
                // Ошибка уже отображена в InputValidator
                return;
            }

            correctAnswer = GuessingGame.CalculateFunction(a, b);
            lblCorrectAnswer.Text = $"Правильный ответ (для проверки): {correctAnswer}";

            if (int.TryParse(txtAttempts.Text, out totalAttempts) && totalAttempts > 0)
            {
                remainingAttempts = totalAttempts;
                lblAttempts.Text = $"Осталось попыток: {remainingAttempts}";

                // Настраиваем и запускаем таймер
                timeLeft = 30;
                lblTimer.Text = $"Осталось времени: {timeLeft} сек";
                gameTimer.Interval = 1000; // 1 секунда
                gameTimer.Start();

                btnCheck.Visible = true; // Показываем кнопку "Проверить" после нажатия кнопки "Начать"
            }
            else
            {
                MessageBox.Show("Введите корректное количество попыток.");
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (remainingAttempts > 0)
            {
                double userGuess;
                if (double.TryParse(txtGuess.Text, out userGuess))
                {
                    if (Math.Round(userGuess, 2) == correctAnswer)
                    {
                        MessageBox.Show("Угадали!");
                        gameTimer.Stop();
                        btnCheck.Visible = false; // Скрываем кнопку "Проверить" после окончания игры
                    }
                    else
                    {
                        remainingAttempts--;
                        lblAttempts.Text = $"Осталось попыток: {remainingAttempts}";
                        if (remainingAttempts == 0)
                        {
                            MessageBox.Show($"Попытки закончились. Правильный ответ: {correctAnswer}");
                            gameTimer.Stop();
                            btnCheck.Visible = false; // Скрываем кнопку "Проверить" после окончания игры
                        }
                        else
                        {
                            MessageBox.Show("Неверно, попробуйте еще раз.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Введите корректное число.");
                }
            }
            else
            {
                MessageBox.Show("Попытки закончились.");
                btnCheck.Visible = false; // Скрываем кнопку "Проверить" после окончания игры
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                timeLeft--;
                lblTimer.Text = $"Осталось времени: {timeLeft} сек";
            }
            else
            {
                gameTimer.Stop();
                MessageBox.Show("Время вышло!");
                MessageBox.Show($"Правильный ответ: {correctAnswer}");
                btnCheck.Visible = false; // Скрываем кнопку "Проверить" после окончания игры
            }
        }

        private void InitializeComponent()
        {
            this.lblFormula = new System.Windows.Forms.Label();
            this.txtA = new System.Windows.Forms.TextBox();
            this.txtB = new System.Windows.Forms.TextBox();
            this.txtGuess = new System.Windows.Forms.TextBox();
            this.btnCheck = new System.Windows.Forms.Button();
            this.lblAttempts = new System.Windows.Forms.Label();
            this.txtAttempts = new System.Windows.Forms.TextBox();
            this.lblTimer = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblCorrectAnswer = new System.Windows.Forms.Label();
            this.gameTimer = new System.Windows.Forms.Timer();
            this.lblA = new System.Windows.Forms.Label();
            this.lblB = new System.Windows.Forms.Label();
            this.lblGuess = new System.Windows.Forms.Label();
            this.lblAttemptsInput = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblFormula
            // 
            this.lblFormula.AutoSize = true;
            this.lblFormula.Location = new System.Drawing.Point(12, 9);
            this.lblFormula.Name = "lblFormula";
            this.lblFormula.Size = new System.Drawing.Size(0, 13);
            this.lblFormula.TabIndex = 0;
            // 
            // lblA
            // 
            this.lblA.AutoSize = true;
            this.lblA.Location = new System.Drawing.Point(12, 35);
            this.lblA.Name = "lblA";
            this.lblA.Size = new System.Drawing.Size(68, 13);
            this.lblA.TabIndex = 10;
            this.lblA.Text = "Введите A:";
            // 
            // txtA
            // 
            this.txtA.Location = new System.Drawing.Point(100, 32);
            this.txtA.Name = "txtA";
            this.txtA.Size = new System.Drawing.Size(100, 20);
            this.txtA.TabIndex = 1;
            // 
            // lblB
            // 
            this.lblB.AutoSize = true;
            this.lblB.Location = new System.Drawing.Point(12, 61);
            this.lblB.Name = "lblB";
            this.lblB.Size = new System.Drawing.Size(68, 13);
            this.lblB.TabIndex = 11;
            this.lblB.Text = "Введите B:";
            // 
            // txtB
            // 
            this.txtB.Location = new System.Drawing.Point(100, 58);
            this.txtB.Name = "txtB";
            this.txtB.Size = new System.Drawing.Size(100, 20);
            this.txtB.TabIndex = 2;
            // 
            // lblGuess
            // 
            this.lblGuess.AutoSize = true;
            this.lblGuess.Location = new System.Drawing.Point(12, 87);
            this.lblGuess.Name = "lblGuess";
            this.lblGuess.Size = new System.Drawing.Size(68, 13);
            this.lblGuess.TabIndex = 12;
            this.lblGuess.Text = "Ваш ответ:";
            // 
            // txtGuess
            // 
            this.txtGuess.Location = new System.Drawing.Point(100, 84);
            this.txtGuess.Name = "txtGuess";
            this.txtGuess.Size = new System.Drawing.Size(100, 20);
            this.txtGuess.TabIndex = 3;
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(15, 113);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(100, 23);
            this.btnCheck.TabIndex = 4;
            this.btnCheck.Text = "Проверить";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // lblAttempts
            // 
            this.lblAttempts.AutoSize = true;
            this.lblAttempts.Location = new System.Drawing.Point(12, 139);
            this.lblAttempts.Name = "lblAttempts";
            this.lblAttempts.Size = new System.Drawing.Size(0, 13);
            this.lblAttempts.TabIndex = 5;
            // 
            // lblAttemptsInput
            // 
            this.lblAttemptsInput.AutoSize = true;
            this.lblAttemptsInput.Location = new System.Drawing.Point(12, 161);
            this.lblAttemptsInput.Name = "lblAttemptsInput";
            this.lblAttemptsInput.Size = new System.Drawing.Size(82, 13);
            this.lblAttemptsInput.TabIndex = 13;
            this.lblAttemptsInput.Text = "Кол-во попыток:";
            // 
            // txtAttempts
            // 
            this.txtAttempts.Location = new System.Drawing.Point(100, 158);
            this.txtAttempts.Name = "txtAttempts";
            this.txtAttempts.Size = new System.Drawing.Size(100, 20);
            this.txtAttempts.TabIndex = 6;
            // 
            // lblTimer
            // 
            this.lblTimer.AutoSize = true;
            this.lblTimer.Location = new System.Drawing.Point(12, 184);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(0, 13);
            this.lblTimer.TabIndex = 7;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(15, 210);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(100, 23);
            this.btnStart.TabIndex = 8;
            this.btnStart.Text = "Начать";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblCorrectAnswer
            // 
            this.lblCorrectAnswer.AutoSize = true;
            this.lblCorrectAnswer.Location = new System.Drawing.Point(12, 236);
            this.lblCorrectAnswer.Name = "lblCorrectAnswer";
            this.lblCorrectAnswer.Size = new System.Drawing.Size(0, 13);
            this.lblCorrectAnswer.TabIndex = 9;
            // 
            // gameTimer
            // 
            this.gameTimer.Tick += new System.EventHandler(this.gameTimer_Tick);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.lblCorrectAnswer);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblTimer);
            this.Controls.Add(this.txtAttempts);
            this.Controls.Add(this.lblAttempts);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.txtGuess);
            this.Controls.Add(this.txtB);
            this.Controls.Add(this.txtA);
            this.Controls.Add(this.lblFormula);
            this.Controls.Add(this.lblA);
            this.Controls.Add(this.lblB);
            this.Controls.Add(this.lblGuess);
            this.Controls.Add(this.lblAttemptsInput);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblFormula;
        private System.Windows.Forms.TextBox txtA;
        private System.Windows.Forms.TextBox txtB;
        private System.Windows.Forms.TextBox txtGuess;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Label lblAttempts;
        private System.Windows.Forms.TextBox txtAttempts;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblCorrectAnswer;
        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Label lblA;
        private System.Windows.Forms.Label lblB;
        private System.Windows.Forms.Label lblGuess;
        private System.Windows.Forms.Label lblAttemptsInput;
    }
}

