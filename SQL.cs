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
    public class SQL
    {
        System.Data.SqlClient.SqlConnection con;
        
        public void CreateDB()
        {
            try
            {
                string dbName = "NevermanDarts";
                string outputFolder = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"NevermanDarts");
                //string outputFolder = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Data");
                //string outputFolder = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "NevermanDarts");

                System.IO.Directory.CreateDirectory(outputFolder);

                string mdfFilename = dbName + ".mdf";
                string dbFileName = System.IO.Path.Combine(outputFolder, mdfFilename);
                string logFileName = System.IO.Path.Combine(outputFolder, String.Format("{0}_log.ldf", dbName));

                if (!File.Exists(dbFileName))
                {
                    con = new SqlConnection();
                    con.ConnectionString = @"Data Source=(LocalDb)\v11.0;Integrated Security=True";
                    con.Open();

                    string SQL_Text = string.Format(@"
	IF EXISTS(SELECT * FROM sys.databases WHERE name='{0}')
	BEGIN
		ALTER DATABASE [{0}]
		SET SINGLE_USER
		WITH ROLLBACK IMMEDIATE
		DROP DATABASE [{0}]
	END
	DECLARE @FILENAME AS VARCHAR(255)
	SET @FILENAME = '{1}';
	EXEC ('CREATE DATABASE [{0}] ON PRIMARY 
		(NAME = [{0}], 
		FILENAME =''' + @FILENAME + ''', 
		SIZE = 5MB, 
		MAXSIZE = 50MB, 
		FILEGROWTH = 1MB )')",
                    dbName, dbFileName);

                    Execute_SQL(SQL_Text);

                }
                else
                {

                }

            }
            catch (Exception)
            {
                //MessageBox.Show(ex.ToString());
                //throw;
            }
        }

        public void CreateTables()
        {
            string SQL_Text;

            SQL_Text = @"IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = 'Players') CREATE TABLE Players(
    ID int IDENTITY(1, 1),
    FirstName varchar(50) NOT NULL,
    LastName varchar(50) NOT NULL,
    Alias varchar(50) NOT NULL,
    Logo varchar(250),
    Soundtrack varchar(250),
    visible bit NOT NULL,
    PRIMARY KEY (ID)
    )";

            Execute_SQL(SQL_Text);

            SQL_Text = @"IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = 'Game') CREATE TABLE Game(
    ID int IDENTITY(1, 1),
    DateTime_Start datetime2,
    DateTime_End datetime2,
    Winner int,
    PRIMARY KEY (ID)
    )";

            Execute_SQL(SQL_Text);

            SQL_Text = @"IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = 'Game_Players') CREATE TABLE Game_Players(
    Game_ID int,
    Players_ID int,
    PRIMARY KEY (Game_ID, Players_ID),
    FOREIGN KEY (Game_ID) REFERENCES Game(ID),
    FOREIGN KEY (Players_ID) REFERENCES Players(ID)
    )";

            Execute_SQL(SQL_Text);

            SQL_Text = @"IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = 'Settings') CREATE TABLE Settings(
    Game_ID int,
    Mode int,
    Legs int,
    Sets int,
    Pause int,
    PRIMARY KEY (Game_ID),
    FOREIGN KEY (Game_ID) REFERENCES Game(ID)
    )";

            Execute_SQL(SQL_Text);

            SQL_Text = @"IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = 'Sets') CREATE TABLE Sets(
    ID int IDENTITY(1, 1),
    Set_No int,
    DateTime_Start datetime2,
    DateTime_End datetime2,
    Winner int,
    PRIMARY KEY (ID),
    FOREIGN KEY (Winner) REFERENCES Players(ID)
    )";

            Execute_SQL(SQL_Text);

            SQL_Text = @"IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = 'Game_Sets') CREATE TABLE Game_Sets(
    Game_ID int,
    Sets_ID int,
    PRIMARY KEY (Game_ID, Sets_ID),
    FOREIGN KEY (Game_ID) REFERENCES Game(ID),
    FOREIGN KEY (Sets_ID) REFERENCES Sets(ID)
    )";

            Execute_SQL(SQL_Text);

            SQL_Text = @"IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = 'Legs') CREATE TABLE Legs(
    ID int IDENTITY(1, 1),
    Leg_No int,
    DateTime_Start datetime2,
    DateTime_End datetime2,
    Winner int,
    Starter	int,
    PRIMARY KEY (ID),
    FOREIGN KEY (Winner) REFERENCES Players(ID),
    FOREIGN KEY (Starter) REFERENCES Players(ID)
    )";

            Execute_SQL(SQL_Text);

            SQL_Text = @"IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = 'Sets_Legs') CREATE TABLE Sets_Legs(
    Sets_ID int,
    Legs_ID int,
    PRIMARY KEY (Sets_ID, Legs_ID),
    FOREIGN KEY (Sets_ID) REFERENCES Sets(ID),
    FOREIGN KEY (Legs_ID) REFERENCES Legs(ID)
    )";

            Execute_SQL(SQL_Text);

            SQL_Text = @"IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = 'Shot') CREATE TABLE Shot(
    ID bigint IDENTITY(1, 1),
    Shot_No tinyint,
    Player_ID int,
    Bust bit,
    Finish bit,
    PRIMARY KEY (ID),
    FOREIGN KEY (Player_ID) REFERENCES Players(ID),
    )";

            Execute_SQL(SQL_Text);

            SQL_Text = @"IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = 'Legs_Shot') CREATE TABLE Legs_Shot(
    Legs_ID int,
    Shot_ID bigint,
    PRIMARY KEY (Legs_ID, Shot_ID),
    FOREIGN KEY (Legs_ID) REFERENCES Legs(ID),
    FOREIGN KEY (Shot_ID) REFERENCES Shot(ID)
    )";

            Execute_SQL(SQL_Text);

            SQL_Text = @"IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = 'Darts') CREATE TABLE Darts(
    ID bigint IDENTITY(1, 1),
    Dart_No tinyint,
    Value tinyint,
    DoubleField bit,
    TripleField bit,
    Bust bit,
    Finish bit,
    PRIMARY KEY (ID)
    )";

            Execute_SQL(SQL_Text);

            SQL_Text = @"IF NOT EXISTS (SELECT name FROM sysobjects WHERE name = 'Shot_Darts') CREATE TABLE Shot_Darts(
    Shot_ID bigint,
    Darts_ID bigint,
    PRIMARY KEY (Shot_ID, Darts_ID),
    FOREIGN KEY (Shot_ID) REFERENCES Shot(ID),
    FOREIGN KEY (Darts_ID) REFERENCES Darts(ID)
    )";

            Execute_SQL(SQL_Text);
        }

        public SqlConnection ConnectDB()
        {
            con = new SqlConnection();

            //con.ConnectionString = "Data Source=(LocalDB)\\v11.0;AttachDbFilename=|DataDirectory|\\NevermanDarts;Database=NevermanDarts;Integrated Security=True";
            con.ConnectionString = @"Data Source=(LocalDb)\v11.0;Database=NevermanDarts;Integrated Security=True";

            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            return con;
        }

        public void DisconnectDB()
        {
            if (con.State != ConnectionState.Closed)
            {
                con.Close();
            }
        }

        public DataTable Read_SQL(string SQL_Text)
        {
            DataTable table = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter(SQL_Text, con);

            adapter.Fill(table);

            return table;
        }

        public int Execute_SQL(string SQL_Text)
        {
            SqlCommand cmd_Command = new SqlCommand(SQL_Text, con);

            try
            {
                int result = -1;

                if (SQL_Text.Contains("INSERT INTO"))
                {
                    result = Convert.ToInt16(cmd_Command.ExecuteScalar());
                }
                else
                {
                    cmd_Command.ExecuteNonQuery();
                }
                cmd_Command.Dispose();

                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }
    }
}
