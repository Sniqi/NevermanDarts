using System;
using System.Collections.Generic;
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

namespace NevermanDarts
{
    /// <summary>
    /// Interaktionslogik für PlayerColumn.xaml
    /// </summary>
    public partial class PlayerColumn : UserControl
    {
        private int PlayerID = 0;
        private int game_score = 0;
        private int darts_count = 0;
        private double average = 0.0;

        MainWindow MW;

        public PlayerColumn(MainWindow mw)
        {
            InitializeComponent();

            MW = mw;
        }

        public void SetPlayerID(int PlayerID)
        {
            this.PlayerID = PlayerID;
        }

        public int GetPlayerID()
        {
            return this.PlayerID;
        }

        public void SetPlayerRealName(string playerRealName)
        {
            label_playerRealName.Content = playerRealName;
        }

        public void SetPlayerName(string playerName)
        {
            label_playerName.Content = playerName;
        }

        public string GetPlayerRealName()
        {
            return label_playerRealName.Content.ToString();
        }

        public string GetPlayerName()
        {
            return label_playerName.Content.ToString();
        }

        public int GetPlayerScore()
        {
            return Convert.ToInt32(label_playerScore.Content);
        }

        public void SetPlayerScore(int playerScore)
        {
            label_playerScore.Content = playerScore.ToString();
        }

        public void SetAsActivePlayer()
        {
            label_playerName.Foreground = Brushes.GreenYellow;
            label_playerRealName.Foreground = Brushes.GreenYellow;
        }

        public void SetAsInactivePlayer()
        {
            label_playerName.Foreground = Brushes.White;
            label_playerRealName.Foreground = Brushes.White;
        }

        public void SetAsStartingPlayer()
        {
            label_startingPlayer.Foreground = Brushes.LemonChiffon;
        }

        public void RemoveAsStartingPlayer()
        {
            label_startingPlayer.Foreground = Brushes.Black;
        }

        public void SetLegGoal(int legGoal)
        {
            label_playerLegs_Goal.Content = legGoal.ToString();
        }

        public void SetSetGoal(int setGoal)
        {
            label_playerSets_Goal.Content = setGoal.ToString();
        }

        public void CalculateAvg(int score)
        {
            game_score += score;

            average = (double)game_score / ((double)darts_count / 3);

            label_playerAvg.Content = Math.Round(average, 1).ToString().Replace(",", ".");
        }

        public void CalculateAvgLeg(int score)
        {
            game_score += score;

            average = (double)game_score / ((double)darts_count / 3);

            label_playerAvgLeg.Content = Math.Round(average, 1).ToString().Replace(",", ".");
        }

        public void SetAvg(double avg)
        {
            label_playerAvg.Content = Math.Round(avg, 1).ToString().Replace(",", ".");
        }

        public void SetAvgLeg(double avg)
        {
            label_playerAvgLeg.Content = Math.Round(avg, 1).ToString().Replace(",", ".");
        }

        public void AddHistoryEntry_End(int score)
        {
            string trennung_txt = "--- " + score + " ---";

            listBox_history.Items.Add(trennung_txt);
            MW.AddHistoryEntry(trennung_txt);
        }

        public void AddHistoryEntry(int score, int newScore, bool bust, bool finish)
        {
            if (!bust && !finish)
            {
                listBox_history.Items.Add(score.ToString() + " = " + newScore.ToString());
                MW.AddHistoryEntry(score.ToString() + " = " + newScore.ToString());
            }
            else if (bust)
            {
                listBox_history.Items.Add(score.ToString() + " = " + newScore.ToString() + " (!)");
                MW.AddHistoryEntry(score.ToString() + " = " + newScore.ToString() + " (!)");
            }
            else if (finish)
            {
                listBox_history.Items.Add(score.ToString() + " = " + newScore.ToString() + " (WIN)");
                MW.AddHistoryEntry(score.ToString() + " = " + newScore.ToString() + " (WIN)");
            }

            listBox_history.ScrollIntoView(listBox_history.Items[listBox_history.Items.Count - 1]);

            /*

            int currentScore = Convert.ToInt32(label_playerScore.Content);
            int newScore = currentScore - score;

            darts_count++;

            string trennung_txt = "--- Würfe: " + darts_count + " ---";

            if (score > 0)
            {
                listBox_history.Items.Add(currentScore.ToString() + " - " + score + " = " + newScore);
                MW.AddHistoryEntry(currentScore.ToString() + " - " + score + " = " + newScore);

                if (trennung == true)
                {
                    listBox_history.Items.Add(trennung_txt);
                    MW.AddHistoryEntry(trennung_txt);
                }

                SetPlayerScore(newScore);

                CalculateAvg(score);

                listBox_history.ScrollIntoView(listBox_history.Items[listBox_history.Items.Count - 1]);
                return "default";
            }
            else if(newScore == 0)
            {
                listBox_history.Items.Add(currentScore.ToString() + " - " + score + " = " + newScore + " (Win)");
                MW.AddHistoryEntry(currentScore.ToString() + " - " + score + " = " + newScore + " (Win)");

                listBox_history.Items.Add(trennung_txt);
                MW.AddHistoryEntry(trennung_txt);

                SetPlayerScore(newScore);

                bool SetWon = IncrementLegs();

                if (SetWon)
                {
                    bool GameWon = IncrementSets();

                    if (GameWon)
                    {
                        listBox_history.ScrollIntoView(listBox_history.Items[listBox_history.Items.Count - 1]);
                        return "game_won";
                    }

                    listBox_history.ScrollIntoView(listBox_history.Items[listBox_history.Items.Count - 1]);
                    return "set_won";
                }
                else
                {
                    listBox_history.ScrollIntoView(listBox_history.Items[listBox_history.Items.Count - 1]);
                    return "leg_won";
                }
            }
            else if (newScore < 0)
            {
                listBox_history.Items.Add(currentScore.ToString() + " - " + score + " = " + newScore + " (!)");
                MW.AddHistoryEntry(currentScore.ToString() + " - " + score + " = " + newScore + " (!)");

                listBox_history.Items.Add(trennung_txt);
                MW.AddHistoryEntry(trennung_txt);

                CalculateAvg(0);

                listBox_history.ScrollIntoView(listBox_history.Items[listBox_history.Items.Count - 1]);
                return "bust";
            }

            listBox_history.ScrollIntoView(listBox_history.Items[listBox_history.Items.Count - 1]);
            
            return "default";

            */
        }

        public void RemoveLastHistoryEntry()
        {
            //int currentScore = Convert.ToInt32(label_playerScore.Content);

            string currentLine = listBox_history.Items.GetItemAt(listBox_history.Items.Count - 1).ToString();

            if (currentLine.Contains("---"))
            {
                //listBox_history.Items.Remove(listBox_history.Items.GetItemAt(listBox_history.Items.Count - 1));
                /*
                for (int n = listBox_history.Items.Count - 1; n >= 0; --n)
                {
                    string removelistitem = currentLine;
                    if (listBox_history.Items[n].ToString().Contains(removelistitem))
                    {
                        listBox_history.Items.RemoveAt(n);
                    }
                }
                */
                listBox_history.Items.RemoveAt(listBox_history.Items.Count - 1);

                currentLine = listBox_history.Items.GetItemAt(listBox_history.Items.Count - 1).ToString();

                MW.RemoveLastHistoryEntry();
            }

            //int score = Convert.ToInt32(currentLine.Split(' ')[2].Trim());

            //listBox_history.Items.Remove(listBox_history.Items.GetItemAt(listBox_history.Items.Count - 1));
            /*
            for (int n = listBox_history.Items.Count - 1; n >= 0; --n)
            {
                string removelistitem = currentLine;
                if (listBox_history.Items[n].ToString().Contains(removelistitem))
                {
                    listBox_history.Items.RemoveAt(n);
                }
            }
            */
            listBox_history.Items.RemoveAt(listBox_history.Items.Count - 1);

            MW.RemoveLastHistoryEntry();

            /*
            if (!currentLine.Contains("(!)"))
            {
                int newScore = currentScore + score;
                SetPlayerScore(newScore);
            }
            */

            if (listBox_history.Items.Count > 0)
                listBox_history.ScrollIntoView(listBox_history.Items[listBox_history.Items.Count - 1]);

        }

        public void ResetLegs()
        {
            int LegsActual = Convert.ToInt32(label_playerLegs_Actual.Content);

            label_playerLegs_Actual.Content = 0;
        }

        public bool IncrementLegs()
        {
            int LegsActual = Convert.ToInt32(label_playerLegs_Actual.Content);
            int LegsGoal = Convert.ToInt32(label_playerLegs_Goal.Content);

            if (LegsActual == LegsGoal - 1)
            {
                label_playerLegs_Actual.Content = 0;

                return true;
            }
            else
            {
                label_playerLegs_Actual.Content = LegsActual + 1;

                return false;
            }

        }

        public bool IncrementSets()
        {
            int SetsActual = Convert.ToInt32(label_playerSets_Actual.Content);
            int SetsGoal = Convert.ToInt32(label_playerSets_Goal.Content);

            label_playerSets_Actual.Content = SetsActual + 1;

            if (SetsActual == SetsGoal - 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
