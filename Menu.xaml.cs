using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
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
using System.Reflection;

namespace NevermanDarts
{
    /// <summary>
    /// Interaktionslogik für Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public SQL sql = new SQL();

        public DataTable activePlayers;
        public int Legs;
        public int Sets;
        public int Pause;
        public int Mode;

        public Menu()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sql.CreateDB();

            sql.ConnectDB();

            sql.CreateTables();

            DB_LoadPlayers();
        }

        private void button_addPlayer_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (textBox_addPlayer_FirstName.Text == "" || textBox_addPlayer_LastName.Text == "" || textBox_addPlayer_Alias.Text == "")
            {
                MessageBox.Show("Alle Felder müssen ausgefüllt sein!");
                return;
            }

            string SQL_Text;

            SQL_Text = string.Format("SELECT * FROM Players WHERE Alias='{0}'", textBox_addPlayer_Alias.Text);
            DataTable duplicatePlayers = sql.Read_SQL(SQL_Text);

            if (duplicatePlayers.Rows.Count == 0)
            {
                SQL_Text = string.Format("INSERT INTO Players (FirstName, LastName, Alias, visible) VALUES('{0}','{1}','{2}',{3})",
                textBox_addPlayer_FirstName.Text, textBox_addPlayer_LastName.Text, textBox_addPlayer_Alias.Text, 1);
                sql.Execute_SQL(SQL_Text);
            }
            else
            {
                if (Convert.ToInt16(duplicatePlayers.Rows[0]["visible"]) == 0)
                {
                    MessageBoxResult result1 = MessageBox.Show("Die Daten eines Spielers mit dem gleichen Namen (" + duplicatePlayers.Rows[0]["Alias"] + ") exisiteren noch. Diese Daten wiederherstellen?", "Alte Daten des Spielers noch vorhanden", MessageBoxButton.YesNoCancel, MessageBoxImage.Information, MessageBoxResult.Cancel, MessageBoxOptions.DefaultDesktopOnly);
                    if (result1 == MessageBoxResult.Yes)
                    {
                        SQL_Text = string.Format("UPDATE Players SET visible=1 WHERE ID={0}", Convert.ToInt16(duplicatePlayers.Rows[0]["ID"]));
                        sql.Execute_SQL(SQL_Text);
                    }
                    else if (result1 == MessageBoxResult.No)
                    {
                        MessageBoxResult result2 = MessageBox.Show("Wollen Sie die Daten dieses Spielers (" + duplicatePlayers.Rows[0]["Alias"] + ") endgültig löschen? (NICHT EMPFOHLEN)", "Spieler endgütlig löschen?", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly);
                        if (result2 == MessageBoxResult.Yes)
                        {
                            SQL_Text = string.Format("DELETE FROM Players WHERE ID={0}", Convert.ToInt16(duplicatePlayers.Rows[0]["ID"]));
                            sql.Execute_SQL(SQL_Text);
                        }
                        else if (result2 == MessageBoxResult.No)
                        {
                            return;
                        }
                    }
                }
                else
                {
                    MessageBoxResult result1 = MessageBox.Show("Ein Spieler mit dem gleichen Alias (" + duplicatePlayers.Rows[0]["Alias"] + ") exisitiert bereits!", "Spieler vereits vorhanden", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                    return;
                }
            }

            textBox_addPlayer_FirstName.Text = "";
            textBox_addPlayer_LastName.Text = "";
            textBox_addPlayer_Alias.Text = "";

            textBox_addPlayer_FirstName.Focus();

            DB_LoadPlayers();
        }

        private void button_deletePlayer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;

                /*sql = "DELETE Players WHERE ID=" + listBox_availablePlayers.SelectedValue.ToString().Split(']')[0].Substring(1);
                */

                string SQL_Text;

                SQL_Text = string.Format("UPDATE Players SET visible=0 WHERE ID={0}", listBox_availablePlayers.SelectedValue.ToString().Split(']')[0].Substring(1));
                sql.Execute_SQL(SQL_Text);

                DB_LoadPlayers();
            }
            catch (Exception)
            {
            }
        }

        private void button_playerAddToActive_Click(object sender, RoutedEventArgs e)
        {
            addPlayerToActiveList();
            listBox_availablePlayers.UnselectAll();
        }

        private void listBox_availablePlayers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            addPlayerToActiveList();
            listBox_availablePlayers.UnselectAll();
        }

        private void addPlayerToActiveList()
        {
            try
            {
                if (listBox_availablePlayers.SelectedItem != null)
                {
                    var player = listBox_availablePlayers.SelectedValue.ToString();
                    if (!listBox_activePlayers.Items.Contains(player))
                    {
                        listBox_activePlayers.Items.Add(player);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_playerRemoveFromActive_Click(object sender, RoutedEventArgs e)
        {
            removePlayerFromActiveList();
            listBox_activePlayers.UnselectAll();
        }

        private void listBox_activePlayers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            removePlayerFromActiveList();
            listBox_activePlayers.UnselectAll();
        }

        private void removePlayerFromActiveList()
        {
            try
            {
                if (listBox_activePlayers.SelectedItem != null)
                {
                    listBox_activePlayers.Items.Remove(listBox_activePlayers.SelectedItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_startGame_Click(object sender, RoutedEventArgs e)
        {
            string Ids = "";
            foreach (String item in listBox_activePlayers.Items)
            {
                Ids += item.Split(']')[0].Substring(1) + ",";
            }
            Ids = Ids.TrimEnd(',');

            string SQL_Text;

            SQL_Text = string.Format("SELECT * FROM Players WHERE ID IN ({0})", Ids);
            activePlayers = sql.Read_SQL(SQL_Text);

            Legs = Convert.ToInt32(textBox_legs.Text);
            Sets = Convert.ToInt32(textBox_sets.Text);
            Pause = Convert.ToInt32(textBox_pause.Text);

            if ( (bool)radioButton_301.IsChecked )
            {
                Mode = 301;
            }
            else if ((bool)radioButton_401.IsChecked)
            {
                Mode = 401;
            }
            else if ((bool)radioButton_501.IsChecked)
            {
                Mode = 501;
            }

            int GameID = DB_CreateGame();

            MainWindow mainWindow = new MainWindow(this, GameID);
            mainWindow.Show();

            this.Visibility = Visibility.Hidden;
        }


        private void DB_LoadPlayers()
        {
            string SQL_Text;

            SQL_Text = "SELECT * FROM Players";
            DataTable Table = sql.Read_SQL(SQL_Text);

            listBox_availablePlayers.Items.Clear();

            foreach (DataRow row in Table.Rows)
            {
                //visible?
                if (Convert.ToInt16(row["visible"]) == 1)
                {
                    listBox_availablePlayers.Items.Add("[" + row["ID"].ToString() + "] " + row["FirstName"].ToString() + " " + row["LastName"].ToString() + " - " + row["Alias"].ToString());
                }
            }
        }

        private int DB_CreateGame()
        {
            string SQL_Text;
            string dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            // Insert new Game
            SQL_Text = string.Format("INSERT INTO Game (DateTime_Start) OUTPUT INSERTED.ID VALUES('{0}')",
                dateTime);
            int Game_ID = sql.Execute_SQL(SQL_Text);

            // Insert Game Settings
            SQL_Text = string.Format("INSERT INTO Settings (Game_ID, Mode, Legs, Sets, Pause) VALUES({0},{1},{2},{3},{4})",
                Game_ID, Mode, Legs, Sets, Pause);
            sql.Execute_SQL(SQL_Text);

            // Connect Players to Game
            foreach (DataRow row in activePlayers.Rows)
            {
                SQL_Text = string.Format("INSERT INTO Game_Players (Game_ID, Players_ID) VALUES({0},{1})",
                    Game_ID, Convert.ToInt16(row["ID"]));
                sql.Execute_SQL(SQL_Text);
            }

            return Game_ID;
        }


        private void button_deleteDatabase_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Möchten Sie die Datenbank wirklich löschen?\nDieser Vorgang kann nicht rückgängig gemacht werden.", "Datenbank löschen?", MessageBoxButton.YesNo, MessageBoxImage.Exclamation, 0, MessageBoxOptions.DefaultDesktopOnly);

            if (result == MessageBoxResult.Yes)
            {
                string SQL_Text;
                SQL_Text = string.Format("USE master;");
                sql.Execute_SQL(SQL_Text);
                SQL_Text = string.Format("ALTER DATABASE NevermanDarts SET SINGLE_USER WITH ROLLBACK IMMEDIATE;");
                sql.Execute_SQL(SQL_Text);
                SQL_Text = string.Format("DROP DATABASE NevermanDarts");
                sql.Execute_SQL(SQL_Text);

                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            }
        }

        private void button_managePlayers_Click(object sender, RoutedEventArgs e)
        {
            ManagePlayers managePlayers = new ManagePlayers(sql);
            managePlayers.Show();
        }
    }
}
