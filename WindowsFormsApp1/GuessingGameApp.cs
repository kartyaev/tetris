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
                        }
                        else
                        {
                            remainingAttempts--;
                            lblAttempts.Text = $"Осталось попыток: {remainingAttempts}";
                            if (remainingAttempts == 0)
                            {
                                MessageBox.Show($"Попытки закончились. Правильный ответ: {correctAnswer}");
                                gameTimer.Stop();
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
                }
            }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
    }

