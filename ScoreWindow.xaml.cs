using System;
using System.Data;
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

namespace NevermanDarts
{
    /// <summary>
    /// Interaktionslogik für ScoreWindow.xaml
    /// </summary>
    public partial class ScoreWindow : Window
    {
        private Menu menu;
        private MainWindow MW;

        private SQL sql;

        private MediaPlayer m_mediaPlayer = new MediaPlayer();

        private List<PlayerColumn> Players = new List<PlayerColumn>();
        private PlayerColumn activePlayer;
        private PlayerColumn lastActivePlayer = null;
        private int legStartPlayerID = 0;
        private int activePlayerID = 0;

        private int Game_ID;
        private int Set_ID = -1;
        private int Leg_ID;
        private int Shot_ID;
        private int Darts_ID;

        public ScoreWindow(Menu m, MainWindow mw, int Game_ID)
        {
            InitializeComponent();

            menu = m;
            MW = mw;

            sql = menu.sql;
            this.Game_ID = Game_ID;
        }

        public void Set_soundVolume(int vol)
        {
            m_mediaPlayer.Volume = Convert.ToDouble(vol) / 100.0f;
        }

        public int DB_Create_Set(int SetNo)
        {
            string SQL_Text;
            string dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            SQL_Text = string.Format("INSERT INTO Sets (DateTime_Start, Set_No) OUTPUT INSERTED.ID VALUES('{0}',{1})",
                dateTime, SetNo);
            var SetID = sql.Execute_SQL(SQL_Text);

            SQL_Text = string.Format("INSERT INTO Game_Sets (Game_ID, Sets_ID) VALUES({0},{1})",
                Game_ID, SetID);
            sql.Execute_SQL(SQL_Text);

            return SetID;
        }

        public int DB_Create_Leg(int LetNo, int Starter)
        {
            string SQL_Text;
            string dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            SQL_Text = string.Format("INSERT INTO Legs (DateTime_Start, Leg_No, Starter) OUTPUT INSERTED.ID VALUES('{0}',{1},{2})",
                dateTime, LetNo, Starter);
            var LegID = sql.Execute_SQL(SQL_Text);

            SQL_Text = string.Format("INSERT INTO Sets_Legs (Sets_ID, Legs_ID) VALUES({0},{1})",
                Set_ID, LegID);
            sql.Execute_SQL(SQL_Text);

            return LegID;
        }

        public int DB_Create_Shot(int ShotNo, int PlayerID)
        {
            string SQL_Text;

            SQL_Text = string.Format("INSERT INTO Shot (Shot_No, Player_ID) OUTPUT INSERTED.ID VALUES({0},{1})",
                ShotNo, PlayerID);
            var ShotID = sql.Execute_SQL(SQL_Text);

            SQL_Text = string.Format("INSERT INTO Legs_Shot (Legs_ID, Shot_ID) VALUES({0},{1})",
                Leg_ID, ShotID);
            sql.Execute_SQL(SQL_Text);

            return ShotID;
        }

        public int DB_Create_Dart(int DartNo, int Value, int DoubleField, int TripleField, int Bust, int Finish)
        {
            string SQL_Text;

            SQL_Text = string.Format("INSERT INTO Darts (Dart_No, Value, DoubleField, TripleField, Bust, Finish) OUTPUT INSERTED.ID VALUES({0},{1},{2},{3},{4},{5})",
                DartNo, Value, DoubleField, TripleField, Bust, Finish);
            var DartID = sql.Execute_SQL(SQL_Text);

            SQL_Text = string.Format("INSERT INTO Shot_Darts (Shot_ID, Darts_ID) VALUES({0},{1})",
                Shot_ID, DartID);
            sql.Execute_SQL(SQL_Text);

            if (Bust == 1)
            {
                SQL_Text = string.Format("UPDATE Shot SET Bust=1 WHERE ID={0}", Shot_ID);
                sql.Execute_SQL(SQL_Text);
            }
            if (Finish == 1)
            {
                SQL_Text = string.Format("UPDATE Shot SET Finish=1 WHERE ID={0}", Shot_ID);
                sql.Execute_SQL(SQL_Text);

                string dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                SQL_Text = string.Format("SELECT Player_ID FROM Shot WHERE ID IN ({0})", Shot_ID);
                int PlayerID = Convert.ToInt32(sql.Read_SQL(SQL_Text).Rows[0]["Player_ID"]);

                SQL_Text = string.Format("UPDATE Legs SET Winner={1},DateTime_End='{2}' WHERE ID={0}", Leg_ID, PlayerID, dateTime);
                sql.Execute_SQL(SQL_Text);
            }

            return DartID;
        }

        public void DB_Delete_Set(int SetID)
        {
            string SQL_Text;

            SQL_Text = string.Format("DELETE FROM Game_Sets WHERE Game_ID={0} AND Sets_ID={1}", Game_ID, SetID);
            sql.Execute_SQL(SQL_Text);

            SQL_Text = string.Format("DELETE FROM Sets WHERE ID={0}", SetID);
            sql.Execute_SQL(SQL_Text);

            SQL_Text = string.Format("SELECT TOP (1) ID FROM Sets ORDER BY ID desc");
            int ID = Convert.ToInt32(sql.Read_SQL(SQL_Text).Rows[0]["ID"]);

            SQL_Text = string.Format("SELECT TOP (1) * FROM Game_Sets ORDER BY Game_ID desc");
            int last_Game_ID = Convert.ToInt32(sql.Read_SQL(SQL_Text).Rows[0]["Game_ID"]);
            Set_ID = Convert.ToInt32(sql.Read_SQL(SQL_Text).Rows[0]["Sets_ID"]);

            if (last_Game_ID != Game_ID)
            {
                Set_ID = -1;
            }
        }

        public void DB_Delete_Leg(int LegID)
        {
            string SQL_Text;

            SQL_Text = string.Format("DELETE FROM Sets_Legs WHERE Sets_ID={0} AND Legs_ID={1}", Set_ID, LegID);
            sql.Execute_SQL(SQL_Text);

            SQL_Text = string.Format("DELETE FROM Legs WHERE ID={0}", LegID);
            sql.Execute_SQL(SQL_Text);

            SQL_Text = string.Format("SELECT TOP (1) * FROM Sets_Legs ORDER BY Sets_ID desc");
            int last_Set_ID = Convert.ToInt32(sql.Read_SQL(SQL_Text).Rows[0]["Sets_ID"]);
            Leg_ID = Convert.ToInt32(sql.Read_SQL(SQL_Text).Rows[0]["Legs_ID"]);

            if (last_Set_ID != Set_ID)
            {
                DB_Delete_Set(Set_ID);
            }
        }

        public void DB_Delete_Shot(int ShotID)
        {
            string SQL_Text;

            SQL_Text = string.Format("DELETE FROM Legs_Shot WHERE Legs_ID={0} AND Shot_ID={1}", Leg_ID, ShotID);
            sql.Execute_SQL(SQL_Text);

            SQL_Text = string.Format("DELETE FROM Shot WHERE ID={0}", ShotID);
            sql.Execute_SQL(SQL_Text);

            SQL_Text = string.Format("SELECT TOP (1) * FROM Legs_Shot ORDER BY Legs_ID desc");
            int last_Leg_ID = Convert.ToInt32(sql.Read_SQL(SQL_Text).Rows[0]["Legs_ID"]);
            Shot_ID = Convert.ToInt32(sql.Read_SQL(SQL_Text).Rows[0]["Shot_ID"]);

            if (last_Leg_ID != Leg_ID)
            {
                DB_Delete_Leg(Leg_ID);
            }
        }

        public void DB_Delete_Dart(int DartID)
        {
            string SQL_Text;

            SQL_Text = string.Format("SELECT TOP (1) * FROM Shot_Darts ORDER BY Shot_ID desc");
            int last_Shot_ID = Convert.ToInt32(sql.Read_SQL(SQL_Text).Rows[0]["Shot_ID"]);

            int dartCount = GetDartCount();

            //if (last_Shot_ID != Shot_ID)
            if (dartCount == 1)
            {
                DB_Delete_Shot(Shot_ID);
                Shot_ID = last_Shot_ID;
            }

            SQL_Text = string.Format("DELETE FROM Shot_Darts WHERE Shot_ID={0} AND Darts_ID={1}", Shot_ID, DartID);
            sql.Execute_SQL(SQL_Text);

            SQL_Text = string.Format("DELETE FROM Darts WHERE ID={0}", DartID);
            sql.Execute_SQL(SQL_Text);

            SQL_Text = string.Format("SELECT TOP (1) * FROM Shot_Darts ORDER BY Shot_ID desc");
            Darts_ID = Convert.ToInt32(sql.Read_SQL(SQL_Text).Rows[0]["Darts_ID"]);
        }

        public void DB_SetWon(int Winner)
        {
            string SQL_Text;

            string dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            SQL_Text = string.Format("UPDATE Sets SET Winner={1},DateTime_End='{2}' WHERE ID={0}", Set_ID, Winner, dateTime);
            sql.Execute_SQL(SQL_Text);
        }

        public void DB_GameWon(int Winner)
        {
            string SQL_Text;

            string dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            SQL_Text = string.Format("UPDATE Game SET Winner={1},DateTime_End='{2}' WHERE ID={0}", Game_ID, Winner, dateTime);
            sql.Execute_SQL(SQL_Text);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EventManager.RegisterClassHandler(typeof(Window), Window.PreviewKeyUpEvent, new KeyEventHandler(OnWindowKeyUp));

            var i = 0;
            foreach (DataRow player in menu.activePlayers.Rows)
            {
                PlayerColumn pcol = new PlayerColumn(MW);

                int PlayerID = Convert.ToInt32(player["ID"]);
                string playerRealName = player["FirstName"].ToString();
                string playerName = player["Alias"].ToString();

                pcol.SetPlayerID(PlayerID);
                pcol.SetPlayerRealName(playerRealName);
                pcol.SetPlayerName(playerName);
                
                pcol.SetPlayerScore(menu.Mode);

                pcol.SetLegGoal(menu.Legs);
                pcol.SetSetGoal(menu.Sets);

                //var maxWidth = (1920 / menu.activePlayers.Count);

                //pcol.Width = maxWidth;

                pcol.Margin = new Thickness((i * 320), 0, 25, 0);

                pcol.HorizontalAlignment = HorizontalAlignment.Left;
                pcol.VerticalAlignment = VerticalAlignment.Top;

                LayoutRoot.Children.Add(pcol);

                Players.Add(pcol);

                i++;
            }

            SetActiveStartingPlayer(0);

            labelWinnerText.Visibility = Visibility.Hidden;
        }

        public void MakeEntry(int score, int multiplier)
        {
            labelWinnerText.Visibility = Visibility.Hidden;

            int DoubleField = 0;
            int TripleField = 0;
            if (multiplier == 2) { DoubleField = 1; }
            else if (multiplier == 3) { TripleField = 1; }

            if (Set_ID == -1)
            {
                Set_ID = DB_Create_Set(1);
                Leg_ID = DB_Create_Leg(1, GetActivePlayer().GetPlayerID());
                Shot_ID = DB_Create_Shot(1, GetActivePlayer().GetPlayerID());

                MW.disableNextPlayerButton();
            }
            
            int currentScore = GetActivePlayer().GetPlayerScore();
            int newScore = currentScore - score;
            int dartCount = GetDartCount();

            // DEFAULT
            if (newScore > 0)
            {
                GetActivePlayer().SetPlayerScore(newScore);

                GetActivePlayer().AddHistoryEntry(score, newScore, false, false);

                Darts_ID = DB_Create_Dart(dartCount, score, DoubleField, TripleField, 0, 0);

                if (dartCount == 3)
                {
                    GetActivePlayer().SetAvgLeg(CalculateAvg_Leg(Leg_ID, GetActivePlayer().GetPlayerID()));
                    GetActivePlayer().SetAvg(CalculateAvg_Game(GetActivePlayer().GetPlayerID()));

                    GetActivePlayer().AddHistoryEntry_End(GetShotScore());

                    SetActivePlayer(GetNextPlayer());

                    Shot_ID = DB_Create_Shot(GetShot(), GetActivePlayer().GetPlayerID());
                }
            }
            // BUST
            else if (newScore < 0)
            {
                GetActivePlayer().SetPlayerScore(currentScore + GetShotScore());

                Darts_ID = DB_Create_Dart(dartCount, score, DoubleField, TripleField, 1, 0);

                GetActivePlayer().SetAvgLeg(CalculateAvg_Leg(Leg_ID, GetActivePlayer().GetPlayerID()));
                GetActivePlayer().SetAvg(CalculateAvg_Game(GetActivePlayer().GetPlayerID()));

                GetActivePlayer().AddHistoryEntry(score, newScore, true, false);
                GetActivePlayer().AddHistoryEntry_End(GetShotScore());

                SetActivePlayer(GetNextPlayer());

                Shot_ID = DB_Create_Shot(GetShot(), GetActivePlayer().GetPlayerID());
            }
            // WIN
            else if (newScore == 0)
            {
                GetActivePlayer().SetPlayerScore(newScore);

                Darts_ID = DB_Create_Dart(dartCount, score, DoubleField, TripleField, 0, 1);

                GetActivePlayer().SetAvgLeg(CalculateAvg_Leg(Leg_ID, GetActivePlayer().GetPlayerID()));
                GetActivePlayer().SetAvg(CalculateAvg_Game(GetActivePlayer().GetPlayerID()));

                GetActivePlayer().AddHistoryEntry(score, newScore, false, true);
                GetActivePlayer().AddHistoryEntry_End(GetShotScore());

                string Keyword;

                bool SetWon = GetActivePlayer().IncrementLegs();

                if (SetWon)
                {
                    bool GameWon = GetActivePlayer().IncrementSets();

                    if (GameWon)
                    {
                        Keyword = "Spiel";

                        DB_SetWon(GetActivePlayer().GetPlayerID());
                        DB_GameWon(GetActivePlayer().GetPlayerID());

                        // Show Banner
                        textBlockWinnerText.Text = Players[activePlayerID].GetPlayerName().Trim() + " hat das " + Keyword + " gewonnen!";
                        labelWinnerText.Visibility = Visibility.Visible;

                        m_mediaPlayer.Open(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\The Darts Anthem - Chase The Sun.wav"));
                        m_mediaPlayer.Play();

                        MW.Hide();

                        return;
                    }

                    Keyword = "Set";

                    DB_SetWon(GetActivePlayer().GetPlayerID());

                    Set_ID = DB_Create_Set(GetCurrentSetNumber() + 1);

                    // Reset Legs
                    foreach (PlayerColumn plyr in Players)
                    {
                        plyr.ResetLegs();
                    }

                    // Set-Pauses
                    int maxSets = (menu.Pause * Players.Count) - (Players.Count - 1);

                    List<int> SetPauses = new List<int>();
                    for (int i = 1; i < maxSets; i++)
                    {
                        SetPauses.Add(menu.Pause * i);
                    }

                    if (SetPauses.Contains(GetCurrentSetNumber()-1))
                    {
                        m_mediaPlayer.Open(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\The Darts Anthem - Chase The Sun.wav"));
                        m_mediaPlayer.Play();
                    }
                }
                else
                {
                    Keyword = "Leg";
                }

                // Find new active player
                if (Players.Count > 1)
                {
                    if (legStartPlayerID < Players.Count - 1)
                    {
                        legStartPlayerID++;
                    }
                    else
                    {
                        legStartPlayerID = 0;
                    }
                }
                else
                {
                    legStartPlayerID = 0;
                }

                // Reset Score
                foreach (PlayerColumn plyr in Players)
                {
                    plyr.SetPlayerScore(menu.Mode);
                }

                // Show Banner
                textBlockWinnerText.Text = Players[activePlayerID].GetPlayerName().Trim() + " hat das " + Keyword + " gewonnen!";
                labelWinnerText.Visibility = Visibility.Visible;

                // Set new active Player
                SetActivePlayer(Players[legStartPlayerID]);
                
                // Adjust starting player visible point
                foreach (PlayerColumn plyr in Players)
                {
                    plyr.RemoveAsStartingPlayer();
                }
                Players[legStartPlayerID].SetAsStartingPlayer();


                Leg_ID = DB_Create_Leg(GetCurrentLegNumber() + 1, GetActivePlayer().GetPlayerID());

                Shot_ID = DB_Create_Shot(1, GetActivePlayer().GetPlayerID());
            }
            
        }


        public void UndoLastEntry()
        {

            if (GetDartCount() == 1)
            {
                SetActivePlayer(GetPreviousPlayer());
                lastActivePlayer = GetPreviousPlayer();

                DB_Delete_Shot(Shot_ID);
            }

            // check for bust
            DataTable darts = GetAllDartsOfShot(Shot_ID);

            string SQL_Text;
            SQL_Text = string.Format("SELECT Bust FROM Darts WHERE ID IN ({0})", darts.Rows[darts.Rows.Count - 1]["ID"]);
            DataTable LastDart = sql.Read_SQL(SQL_Text);

            // adjust Score
            if (Convert.ToBoolean(LastDart.Rows[0]["Bust"]) && darts.Rows.Count > 1)
            {
                string darts_str = "";
                for (int i = 0; i < darts.Rows.Count - 1; i++)
                {
                    darts_str += darts.Rows[i]["ID"] + ",";
                }
                darts_str = darts_str.TrimEnd(',');

                SQL_Text = string.Format("SELECT Value FROM Darts WHERE ID IN ({0}) AND Bust='false'", darts_str);
                DataTable Table = sql.Read_SQL(SQL_Text);

                int bust_score = 0;
                foreach (DataRow row in Table.Rows)
                {
                    bust_score += Convert.ToInt16(row["Value"]);
                }

                int currentScore = GetActivePlayer().GetPlayerScore();
                int newScore = currentScore - bust_score;
                GetActivePlayer().SetPlayerScore(newScore);
            }

            if (!Convert.ToBoolean(LastDart.Rows[0]["Bust"]))
            {
                int currentScore = GetActivePlayer().GetPlayerScore();
                int newScore = currentScore + GetDartScore();
                GetActivePlayer().SetPlayerScore(newScore);
            }

            DB_Delete_Dart(Darts_ID);

            GetActivePlayer().RemoveLastHistoryEntry();
        }






        private double CalculateAvg_Leg(int LegID, int PlayerID)
        {
            string SQL_Text;

            DataTable allDarts = GetAllDartsOfLeg_Player(LegID, PlayerID);

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

        private double CalculateAvg_Game(int PlayerID)
        {
            string SQL_Text;

            DataTable allDarts = GetAllDartsOfGame_Player(Game_ID, PlayerID);

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



        private DataTable GetAllSetsOfGame(int GameID)
        {
            string SQL_Text;

            SQL_Text = string.Format("SELECT Sets_ID AS ID FROM Game_Sets WHERE Game_ID IN ({0})", GameID);
            DataTable Table = sql.Read_SQL(SQL_Text);

            return Table;
        }

        private DataTable GetAllLegsOfSet(int SetID)
        {
            string SQL_Text;

            SQL_Text = string.Format("SELECT Legs_ID AS ID FROM Sets_Legs WHERE Sets_ID IN ({0})", SetID);
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

        private DataTable GetAllShotsOfLeg(int LegID)
        {
            string SQL_Text;

            SQL_Text = string.Format("SELECT Shot_ID AS ID FROM Legs_Shot WHERE Legs_ID IN ({0})", LegID);
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

        private DataTable GetAllShotsOfLeg_Player(int LegID, int PlayerID)
        {
            string SQL_Text;

            SQL_Text = string.Format("SELECT Legs_Shot.Shot_ID AS ID FROM Legs_Shot INNER JOIN Shot ON Legs_Shot.Shot_ID = Shot.ID WHERE Legs_ID IN({0}) AND Shot.Player_ID IN({1})", LegID, PlayerID);
            DataTable Table = sql.Read_SQL(SQL_Text);

            return Table;
        }

        private DataTable GetAllShotsOfGame_Player(int GameID, int PlayerID)
        {
            DataTable allLegs = GetAllLegsOfGame(GameID);

            string allLegs_str = "";
            foreach (DataRow row in allLegs.Rows)
            {
                allLegs_str += row["ID"] + ",";
            }
            allLegs_str = allLegs_str.TrimEnd(',');

            string SQL_Text;

            SQL_Text = string.Format("SELECT Legs_Shot.Shot_ID AS ID FROM Legs_Shot INNER JOIN Shot ON Legs_Shot.Shot_ID = Shot.ID WHERE Legs_ID IN({0}) AND Shot.Player_ID IN({1})", allLegs_str, PlayerID);
            DataTable Table = sql.Read_SQL(SQL_Text);

            return Table;
        }

        private DataTable GetAllDartsOfShot(int ShotID)
        {
            string SQL_Text;

            SQL_Text = string.Format("SELECT Darts_ID AS ID FROM Shot_Darts WHERE Shot_ID IN ({0})", ShotID);
            DataTable Table = sql.Read_SQL(SQL_Text);

            return Table;
        }

        private DataTable GetAllDartsOfGame(int GameID)
        {
            DataTable allShots = GetAllShotsOfGame(GameID);

            string allShots_str = "";
            foreach (DataRow row in allShots.Rows)
            {
                allShots_str += row["ID"] + ",";
            }
            allShots_str = allShots_str.TrimEnd(',');

            string SQL_Text;

            SQL_Text = string.Format("SELECT Darts_ID AS ID FROM Shot_Darts WHERE Shot_ID IN ({0})", allShots_str);
            DataTable Table = sql.Read_SQL(SQL_Text);

            return Table;
        }

        private DataTable GetAllDartsOfLeg_Player(int LegID, int PlayerID)
        {
            DataTable allShots = GetAllShotsOfLeg_Player(LegID, PlayerID);

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





        public void NextPlayer()
        {
            GetActivePlayer().SetAsInactivePlayer();

            SetActiveStartingPlayer(GetNextPlayerID());

            // Find new active player
            if (Players.Count > 1)
            {
                if (legStartPlayerID < Players.Count - 1)
                {
                    legStartPlayerID++;
                }
                else
                {
                    legStartPlayerID = 0;
                }
            }
            else
            {
                legStartPlayerID = 0;
            }
        }

        private void SetActiveStartingPlayer(int id)
        {
            activePlayerID = id;

            activePlayer = Players[activePlayerID];
            lastActivePlayer = Players[activePlayerID];

            SetActivePlayer(Players[activePlayerID]);
            Players[activePlayerID].SetAsStartingPlayer();

            // Adjust starting player visible point
            foreach (PlayerColumn plyr in Players)
            {
                plyr.RemoveAsStartingPlayer();
            }
            Players[id].SetAsStartingPlayer();
        }

        private void SetActivePlayer(PlayerColumn pcol)
        {
            lastActivePlayer = GetActivePlayer();
            lastActivePlayer.SetAsInactivePlayer();

            activePlayer = pcol;
            activePlayer.SetAsActivePlayer();

            int i = 0;
            foreach (PlayerColumn plyr in Players)
            {
                if (plyr == activePlayer)
                {
                    activePlayerID = i;
                    break;
                }
                i++;
            }
        }

        private PlayerColumn GetActivePlayer()
        {
            return activePlayer;
        }

        private PlayerColumn GetNextPlayer()
        {
            int PlayerID = 0;
            if (activePlayerID == Players.Count - 1)
            {
                PlayerID = 0;
            }
            else
            {
                PlayerID = activePlayerID + 1;
            }
            return Players[PlayerID];
        }

        private int GetNextPlayerID()
        {
            int PlayerID = 0;
            if (activePlayerID == Players.Count - 1)
            {
                PlayerID = 0;
            }
            else
            {
                PlayerID = activePlayerID + 1;
            }
            return PlayerID;
        }

        private PlayerColumn GetPreviousPlayer()
        {
            int PlayerID = 0;
            if (activePlayerID == 0)
            {
                PlayerID = Players.Count - 1;
            }
            else
            {
                PlayerID = activePlayerID - 1;
            }
            return Players[PlayerID];
        }

        private int GetDartCount()
        {
            string SQL_Text;

            SQL_Text = string.Format("SELECT * FROM Shot_Darts WHERE Shot_ID IN ({0})", Shot_ID);
            int dartCount = Convert.ToInt16(sql.Read_SQL(SQL_Text).Rows.Count);

            return dartCount + 1;
        }

        private int GetShot()
        {
            string SQL_Text;

            //SQL_Text = string.Format("SELECT Shot_No FROM Shot WHERE Player_ID IN ({0})", GetActivePlayer().GetPlayerID());
            SQL_Text = string.Format("SELECT Shot.Shot_No FROM Shot INNER JOIN Legs_Shot ON Shot.ID=Legs_Shot.Shot_ID WHERE Shot.Player_ID IN ({0}) AND Legs_Shot.Legs_ID IN ({1})", GetActivePlayer().GetPlayerID(), Leg_ID);
            int shot = Convert.ToInt16(sql.Read_SQL(SQL_Text).Rows.Count);

            return shot + 1;
        }

        private int GetCurrentLegNumber()
        {
            string SQL_Text;

            SQL_Text = string.Format("SELECT * FROM Sets_Legs WHERE Sets_ID IN ({0})", Set_ID);
            int leg = Convert.ToInt16(sql.Read_SQL(SQL_Text).Rows.Count);

            return leg;
        }

        private int GetCurrentSetNumber()
        {
            string SQL_Text;

            SQL_Text = string.Format("SELECT * FROM Game_Sets WHERE Game_ID IN ({0})", Game_ID);
            int set = Convert.ToInt16(sql.Read_SQL(SQL_Text).Rows.Count);

            return set;
        }

        private int GetShotScore()
        {
            string SQL_Text;

            SQL_Text = string.Format("SELECT * FROM Shot_Darts WHERE Shot_ID IN ({0})", Shot_ID);
            DataTable Table = sql.Read_SQL(SQL_Text);

            int score = 0;

            foreach (DataRow row in Table.Rows)
            {
                SQL_Text = string.Format("SELECT Value FROM Darts WHERE ID IN ({0})", row["Darts_ID"]);
                score += Convert.ToInt16(sql.Read_SQL(SQL_Text).Rows[0]["Value"]);
            }

            return score;
        }

        private int GetDartScore()
        {
            string SQL_Text;

            int score = 0;

            SQL_Text = string.Format("SELECT Value FROM Darts WHERE ID IN ({0})", Darts_ID);
            score = Convert.ToInt16(sql.Read_SQL(SQL_Text).Rows[0]["Value"]);

            return score;
        }







        private void OnWindowKeyUp(object source, KeyEventArgs e)
        {
            if (e.Key == Key.F11)
            {
                if (this.WindowStyle != WindowStyle.None)
                {
                    this.WindowStyle = WindowStyle.None;
                    //this.ResizeMode = ResizeMode.NoResize;
                    this.WindowState = WindowState.Maximized;
                    //this.Left = 0;
                    //this.Top = 0;
                    //this.Width = SystemParameters.VirtualScreenWidth;
                    //this.Height = SystemParameters.VirtualScreenHeight;
                    //this.Topmost = true;
                }
                else
                {
                    this.WindowStyle = WindowStyle.ThreeDBorderWindow;
                    //this.ResizeMode = ResizeMode.CanResize;
                    //this.Left = SystemParameters.VirtualScreenWidth / 6;
                    //this.Top = SystemParameters.VirtualScreenHeight / 6;
                    //this.Width = SystemParameters.VirtualScreenWidth / 1.5;
                    //this.Height = SystemParameters.VirtualScreenHeight / 1.5;
                    //this.Topmost = false;
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MW.Close();

            menu.Visibility = Visibility.Visible;
        }
    }
}
