using System;
using System.Drawing;
using System.Windows.Forms;

namespace GuessTheNumber
{
    public partial class Form1 : Form
    {
        int number = 0, guessedNumber = 0, counter = 0, x = 2, y = 100;
        string levelDiff = "normalBtn"; 

        public Form1()  { InitializeComponent();  }

        private void Form1_Load(object sender, EventArgs e) { ResetGame(); }

        private void submitBtn_Click(object sender, EventArgs e)
        {
            labelFeedBack.Visible = false;
            labelFeedBack.BackColor = Color.Transparent;
            feedbackLabel.Text = "";
            if (int.TryParse(textBox1.Text, out guessedNumber))
            {
                if (guessedNumber >= x && guessedNumber < y) 
                {
                    ++counter;
                    int totalRange = y - x;
                    int veryCloseThreshold = (int)(0.01 * totalRange); 
                    int closeThreshold = (int)(0.05 * totalRange);    

                    if (guessedNumber == number)
                    {
                        feedbackLabel.Text = $"Correct! You've guessed the number in {counter} attempts!";
                        DialogResult dialogResult = MessageBox.Show(
                          "Correct!  Do you want to play again?", $"Correct! You've guessed the number in {counter} attempts!", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                            ResetGame(); 
                        else
                            this.Close(); 
                    }
                    else if (guessedNumber < number) 
                    {
                        if (number - guessedNumber <= veryCloseThreshold)
                        {
                            feedbackLabel.Text = "";
                            labelFeedBack.Visible = true;
                            labelFeedBack.ForeColor = Color.Black;
                            labelFeedBack.BackColor = Color.OrangeRed;
                        }
                        else if (number - guessedNumber <= closeThreshold)
                        {
                            feedbackLabel.Text = "Try climbing the mountain! You’re getting closer!";
                            feedbackLabel.ForeColor = Color.White;
                            feedbackLabel.BackColor = Color.BlueViolet;
                           
                           
                        }
                        else
                        {
                            feedbackLabel.Text = "you're digging too deep! Try a bigger number!";
                            feedbackLabel.ForeColor = Color.Black;
                            feedbackLabel.BackColor = Color.Red;
                        }
                    }
                    else if (guessedNumber > number) 
                    {
                        if (guessedNumber - number <= veryCloseThreshold)
                        {
                            feedbackLabel.Text = "just a tad lower and you'll hit the mark!";
                            feedbackLabel.ForeColor = Color.Blue;
                            feedbackLabel.BackColor = Color.Coral;
                        }
                        else if (guessedNumber - number <= closeThreshold)
                        {
                            feedbackLabel.Text = "Try coming down the hill! You’re almost there!";
                            feedbackLabel.ForeColor = Color.White;
                            feedbackLabel.BackColor = Color.BlueViolet;
                        }
                        else
                        {
                            feedbackLabel.Text = "You're in the clouds! Head back to the ground.";
                            feedbackLabel.ForeColor = Color.Black;
                            feedbackLabel.BackColor = Color.Red;
                        }
                    }
                }
            
                else
                {
                    MessageBox.Show($"Input is out of range! Please enter a number between {x - 1} and {y}.", "Invalid Input");
                }
            }
            else
            {
                MessageBox.Show("Invalid Input! Please enter only a number.", "Error");
            }
        }

        private void Difficulty_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton selectedRadioButton && selectedRadioButton.Checked)
            {
                levelDiff = selectedRadioButton.Name;
                feedbackLabel.Text = selectedRadioButton.Name;
                ResetGame();
            }
        }

        private void resetBtn_Click(object sender, EventArgs e) {  ResetGame(); }

        private void ResetGame()
        {
            feedbackLabel.Text = "";
            Random rand = new Random(); 
            switch (levelDiff)
            {
                case "normalBtn": // Normal: 1-100 between means 1 and 100 are excluded from the range
                    x = 2;  // 2 included 
                    y = 100; // 100 excluded 
                    break;

                case "hardBtn": // Hard: 100-250
                    x = 101;
                    y = 250; 
                    break;

                case "veteranBtn": // Veteran: 251-1000
                    x = 252;
                    y = 1000;
                    break;

                case "groundedBtn": // Grounded: 1-5000
                    x = 2;
                    y = 5000;
                    break;

                default: //Normal
                    x = 2;
                    y = 100;
                    break;
            }

            number = rand.Next(x, y);
            counter = 0;
            guessedNumber = 0;
            textBox1.Text = "";
            feedbackLabel.Text = "";
            changelabel(x - 1, y);
        }

        private void changelabel(int x, int y){ label2.Text = $"Enter a Number between {x} and {y}"; }
    }
}
