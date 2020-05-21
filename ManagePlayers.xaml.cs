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
using System.Windows.Shapes;
using System.Data;

namespace NevermanDarts
{
    /// <summary>
    /// Interaktionslogik für ManagePlayers.xaml
    /// </summary>
    public partial class ManagePlayers : Window
    {
        private SQL sql;
        private int selectedPlayerID = 1;
        private int playerCount;

        public ManagePlayers(SQL Sql)
        {
            InitializeComponent();

            sql = Sql;



            string SQL_Text;
            SQL_Text = string.Format("SELECT ID FROM Players");
            playerCount = sql.Read_SQL(SQL_Text).Rows.Count;



            FetchPlayerBaseInfo();
        }

        private void FetchPlayerBaseInfo()
        {
            string SQL_Text;

            int playerID = selectedPlayerID;

            SQL_Text = string.Format("SELECT * FROM Players WHERE ID IN ({0})", playerID);
            string firstName = Convert.ToString(sql.Read_SQL(SQL_Text).Rows[0]["FirstName"]);
            string lastName = Convert.ToString(sql.Read_SQL(SQL_Text).Rows[0]["LastName"]);
            string alias = Convert.ToString(sql.Read_SQL(SQL_Text).Rows[0]["Alias"]);
            int visible = Convert.ToInt16(sql.Read_SQL(SQL_Text).Rows[0]["visible"]);

            textBox_playerID.Text = playerID.ToString();
            textBox_firstName.Text = firstName;
            textBox_lastName.Text = lastName;
            textBox_alias.Text = alias;
            if (visible == 0)
            {
                checkBox_deleted.IsChecked = true;
            }
            else
            {
                checkBox_deleted.IsChecked = false;
            }

            FetchPlayerAveragesInfo();
        }

        private void FetchPlayerAveragesInfo()
        {
            int playerID = selectedPlayerID;

            DataTable allGames = GetAllGamesOfPlayer(playerID);

            List<double> AVG = new List<double>();

            foreach (DataRow row in allGames.Rows)
            {
                AVG.Add(CalculateAvg_Game(playerID, Convert.ToInt16(row["ID"])));
            }

            double allAVG = 0;
            foreach (double avg in AVG)
            {
                allAVG += avg;
            }
            allAVG /= AVG.Count;

            label_avgAllGames_result.Content = Math.Round(allAVG, 2).ToString();
            label_highestAVG_result.Content = Math.Round(AVG.Max(), 2).ToString();
            label_lowestAVG_result.Content = Math.Round(AVG.Min(), 2).ToString();
        }

        private void button_nextPlayer_Click(object sender, RoutedEventArgs e)
        {
            if (selectedPlayerID + 1 <= playerCount)
            {
                selectedPlayerID++;
            }
            else
            {
                selectedPlayerID = 1;
            }

            FetchPlayerBaseInfo();
        }

        private void button_previousPlayer_Click(object sender, RoutedEventArgs e)
        {
            if (selectedPlayerID - 1 >= 1)
            {
                selectedPlayerID--;
            }
            else
            {
                selectedPlayerID = playerCount;
            }

            FetchPlayerBaseInfo();
        }








        private double CalculateAvg_Game(int PlayerID, int GameID)
        {
            string SQL_Text;

            DataTable allDarts = GetAllDartsOfGame_Player(GameID, PlayerID);

            string allDarts_str = "";
            foreach (DataRow row in allDarts.Rows)
            {
                allDarts_str += row["ID"] + ",";
            }
            allDarts_str = allDarts_str.TrimEnd(',');

            SQL_Text = string.Format("SELECT Value,Bust FROM Darts WHERE ID IN ({0})", allDarts_str);
            DataTable Table = sql.Read_SQL(SQL_Text);

            double AVG = 0.0;
            foreach (DataRow row in Table.Rows)
            {
                if (Convert.ToInt16(row["Bust"]) == 0)
                {
                    AVG += Convert.ToDouble(row["Value"]);
                }
            }
            AVG = AVG / (Table.Rows.Count / 3);

            return AVG;
        }



        private DataTable GetAllGamesOfPlayer(int PlayerID)
        {
            string SQL_Text;

            SQL_Text = string.Format("SELECT Game_ID AS ID FROM Game_Players WHERE Players_ID IN ({0})", PlayerID);
            DataTable Table = sql.Read_SQL(SQL_Text);

            return Table;
        }

        private DataTable GetAllSetsOfGame(int GameID)
        {
            string SQL_Text;

            SQL_Text = string.Format("SELECT Sets_ID AS ID FROM Game_Sets WHERE Game_ID IN ({0})", GameID);
            DataTable Table = sql.Read_SQL(SQL_Text);

            return Table;
        }

        private DataTable GetAllLegsOfGame(int GameID)
        {
            DataTable allSets = GetAllSetsOfGame(GameID);

            string allSets_str = "";
            foreach (DataRow row in allSets.Rows)
            {
                allSets_str += row["ID"] + ",";
            }
            allSets_str = allSets_str.TrimEnd(',');

            string SQL_Text;

            SQL_Text = string.Format("SELECT Legs_ID AS ID FROM Sets_Legs WHERE Sets_ID IN ({0})", allSets_str);
            DataTable Table = sql.Read_SQL(SQL_Text);

            return Table;
        }

        private DataTable GetAllShotsOfGame(int GameID)
        {
            DataTable allLegs = GetAllLegsOfGame(GameID);

            string allLegs_str = "";
            foreach (DataRow row in allLegs.Rows)
            {
                allLegs_str += row["ID"] + ",";
            }
            allLegs_str = allLegs_str.TrimEnd(',');

            string SQL_Text;

            SQL_Text = string.Format("SELECT Shot_ID AS ID FROM Legs_Shot WHERE Legs_ID IN ({0})", allLegs_str);
            DataTable Table = sql.Read_SQL(SQL_Text);

            return Table;
        }

        private DataTable GetAllDartsOfGame_Player(int GameID, int PlayerID)
        {
            DataTable allShots = GetAllShotsOfGame(GameID);

            string allShots_str = "";
            foreach (DataRow row in allShots.Rows)
            {
                allShots_str += row["ID"] + ",";
            }
            allShots_str = allShots_str.TrimEnd(',');

            string SQL_Text;

            SQL_Text = string.Format("SELECT Shot_Darts.Darts_ID AS ID FROM Shot_Darts INNER JOIN Shot ON Shot_Darts.Shot_ID=Shot.ID WHERE Shot_ID IN ({0}) AND Shot.Player_ID IN ({1})", allShots_str, PlayerID);
            DataTable Table = sql.Read_SQL(SQL_Text);

            return Table;
        }
    }
}
