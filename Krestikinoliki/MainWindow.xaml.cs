using System.Collections.Generic;
using System;
using System.Windows;
using System.Windows.Controls;

namespace TicTacToe
{
    public partial class MainWindow
    {
        int turn;
        public List<Button> buttons;
        Random rand = new Random();
        int index;
        string userSign;
        string computerSign;

        public MainWindow() : base()
        {
            this.InitializeComponent();
            DisableButtons();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            turn = 0;
            buttons = new List<Button> { Button1, Button2, Button3, Button4, Button5, Button6, Button7, Button8, Button9 };
            index = 0;
        }

        private void Win(string btnContent)
        {
            if ((Button1.Content == btnContent && Button2.Content == btnContent && Button3.Content == btnContent) ||
                (Button1.Content == btnContent && Button4.Content == btnContent && Button7.Content == btnContent) ||
                (Button1.Content == btnContent && Button5.Content == btnContent && Button9.Content == btnContent) ||
                (Button2.Content == btnContent && Button5.Content == btnContent && Button8.Content == btnContent) ||
                (Button3.Content == btnContent && Button6.Content == btnContent && Button9.Content == btnContent) ||
                (Button4.Content == btnContent && Button5.Content == btnContent && Button6.Content == btnContent) ||
                (Button7.Content == btnContent && Button8.Content == btnContent && Button9.Content == btnContent) ||
                (Button3.Content == btnContent && Button5.Content == btnContent && Button7.Content == btnContent))
            {
                string winner = btnContent == userSign ? "игрок " + userSign : "робот " + computerSign;
                MessageBox.Show("Выиграл " + winner + "!");
                DisableButtons();
            }
            else
            {
                foreach (Button btn in wrapPanel1.Children)
                {
                    if (btn.IsEnabled)
                        return;
                }
                MessageBox.Show("Ничья!");
            }
        }

        private void DisableButtons()
        {
            foreach (Button btn in wrapPanel1.Children)
            {
                btn.IsEnabled = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (turn == 1)
            {
                ComputerMove();
            }
            btn.Content = userSign;
            btn.IsEnabled = false;
            Win(btn.Content.ToString());
            if (turn == 0)
            {
                ComputerMove();
            }
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            if (index > 1)
            {
                index = 0;
            }
            if (index == 0)
            {
                userSign = "X";
                computerSign = "O";
                turn = 1;
            }
            else if (index == 1)
            {
                userSign = "O";
                computerSign = "X";
                turn = 0;
            }
            index += 1;
            foreach (Button btn in wrapPanel1.Children)
            {
                btn.Content = "";
                btn.IsEnabled = true;
            }
        }

        private void ComputerMove()
        {
            List<Button> availableButtons = new List<Button>();
            foreach (Button btn in wrapPanel1.Children)
            {
                if (btn.IsEnabled)
                {
                    availableButtons.Add(btn);
                }
            }

            if (availableButtons.Count > 0)
            {
                int randomIndex = rand.Next(0, availableButtons.Count);
                Button computerButton = availableButtons[randomIndex];
                computerButton.Content = computerSign;
                computerButton.IsEnabled = false;
                Win(computerSign);
            }
        }
    }
}
