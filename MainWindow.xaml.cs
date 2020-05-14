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
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Menu menu;
        private int Game_ID;

        private ScoreWindow scoreWindow;

        System.Windows.Media.BrushConverter brushConverter = new System.Windows.Media.BrushConverter();
        Brush oldFill;

        public MainWindow(Menu m, int Game_ID)
        {
            InitializeComponent();

            menu = m;

            this.Game_ID = Game_ID;

            scoreWindow = new ScoreWindow(menu, this, Game_ID);
            scoreWindow.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        public void AddHistoryEntry(string entry)
        {
            listBox_history.Items.Add(entry);

            if (listBox_history.Items.GetItemAt(0).ToString().Contains("---"))
            {
                listBox_history.Items.Remove(listBox_history.Items.GetItemAt(0));
            }

            if (listBox_history.Items.Count > 4)
            {
                listBox_history.Items.Remove(listBox_history.Items.GetItemAt(0));
            }
        }

        public void RemoveLastHistoryEntry()
        {
            //listBox_history.Items.Remove(listBox_history.Items.GetItemAt(listBox_history.Items.Count - 1));
            if (listBox_history.Items.Count > 0)
                listBox_history.Items.RemoveAt(listBox_history.Items.Count - 1);
        }

        private void Score_MouseEnter(Path obj)
        {
            int score = Convert.ToInt32(obj.Name.Split('_')[1]);

            oldFill = obj.Fill;

            if (score > 0)
            {
                obj.Fill = Brushes.Yellow;
            }
        }

        private void Score_MouseLeave(Path obj)
        {
            int score = Convert.ToInt32(obj.Name.Split('_')[1]);

            if (score > 0)
            {
                obj.Fill = oldFill;
            }
        }

        private void Score_MouseLeftButtonDown(Path obj)
        {
            obj.Fill = Brushes.IndianRed;
        }

        private void Score_MouseLeftButtonUp(Path obj)
        {
            obj.Fill = oldFill;

            int score = Convert.ToInt32(obj.Name.Split('_')[1]);
            int multiplier = 1;

            if (obj.Name.Contains("Double"))
            {
                multiplier = 2;
                score *= multiplier;
            }
            if (obj.Name.Contains("Triple"))
            {
                multiplier = 3;
                score *= multiplier;
            }

            scoreWindow.MakeEntry(score, multiplier);
        }
        
        private void button_undo_Click(object sender, RoutedEventArgs e)
        {
            scoreWindow.UndoLastEntry();
        }

        private void button_nextPlayer_Click(object sender, RoutedEventArgs e)
        {
            scoreWindow.NextPlayer();
        }

        public void disableNextPlayerButton()
        {
            button_nextPlayer.IsEnabled = false;
        }

        private void slider_soundVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                scoreWindow.Set_soundVolume(Convert.ToInt16(slider_soundVolume.Value));
                label_soundVolume.Content = Convert.ToInt16(slider_soundVolume.Value) + "%";
            }
            catch (Exception)
            {

            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            scoreWindow.Close();

            menu.Visibility = Visibility.Visible;
        }

        private void slider_pauseLength_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }








        private void Score_0_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_0_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_0_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_0_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }



        private void Score_1_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_1_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_2_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_2_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_3_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_3_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_4_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_4_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_4_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_5_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_5_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_5_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_6_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_6_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_6_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_6_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_7_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_7_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_7_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_7_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_8_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_8_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_8_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_8_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_9_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_9_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_9_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_9_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_10_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_10_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_10_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_10_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_11_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_11_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_11_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_11_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_12_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_12_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_12_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_12_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_13_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_13_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_13_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_13_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_14_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_14_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_14_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_14_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_15_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_15_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_15_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_15_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_16_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_16_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_16_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_16_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_17_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_17_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_17_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_17_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_18_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_18_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_18_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_18_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_19_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_19_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_19_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_19_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_20_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_20_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_20_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_20_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }







        private void Score_25_Bullseye_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_25_Bullseye_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_25_Bullseye_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_25_Bullseye_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_25_Bullseye_Border_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_25_Bullseye_Border_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_25_Bullseye_Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_25_Bullseye_Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_50_Bullseye_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_50_Bullseye_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_50_Bullseye_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_50_Bullseye_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_50_Bullseye_Border_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_50_Bullseye_Border_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_50_Bullseye_Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_50_Bullseye_Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }









        private void Score_1_lower_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_1_lower_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_1_lower_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_1_lower_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_2_lower_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_2_lower_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_2_lower_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_2_lower_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_3_lower_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_3_lower_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_3_lower_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_3_lower_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_4_lower_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_4_lower_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_4_lower_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_4_lower_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_5_lower_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_5_lower_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_5_lower_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_5_lower_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_6_lower_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_6_lower_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_6_lower_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_6_lower_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_7_lower_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_7_lower_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_7_lower_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_7_lower_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_8_lower_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_8_lower_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_8_lower_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_8_lower_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_9_lower_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_9_lower_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_9_lower_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_9_lower_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_10_lower_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_10_lower_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_10_lower_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_10_lower_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_11_lower_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_11_lower_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_11_lower_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_11_lower_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_12_lower_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_12_lower_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_12_lower_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_12_lower_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_13_lower_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_13_lower_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_13_lower_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_13_lower_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_14_lower_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_14_lower_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_14_lower_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_14_lower_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_15_lower_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_15_lower_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_15_lower_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_15_lower_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_16_lower_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_16_lower_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_16_lower_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_16_lower_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_17_lower_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_17_lower_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_17_lower_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_17_lower_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_18_lower_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_18_lower_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_18_lower_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_18_lower_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_19_lower_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_19_lower_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_19_lower_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_19_lower_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_20_lower_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_20_lower_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_20_lower_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_20_lower_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }







        private void Score_1_Double_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_1_Double_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_1_Double_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_1_Double_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_2_Double_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_2_Double_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_2_Double_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_2_Double_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_3_Double_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_3_Double_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_3_Double_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_3_Double_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_4_Double_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_4_Double_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_4_Double_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_4_Double_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_5_Double_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_5_Double_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_5_Double_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_5_Double_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_6_Double_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_6_Double_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_6_Double_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_6_Double_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_7_Double_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_7_Double_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_7_Double_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_7_Double_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_8_Double_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_8_Double_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_8_Double_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_8_Double_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_9_Double_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_9_Double_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_9_Double_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_9_Double_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_10_Double_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_10_Double_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_10_Double_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_10_Double_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_11_Double_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_11_Double_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_11_Double_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_11_Double_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_12_Double_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_12_Double_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_12_Double_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_12_Double_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_13_Double_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_13_Double_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_13_Double_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_13_Double_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_14_Double_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_14_Double_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_14_Double_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_14_Double_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_15_Double_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_15_Double_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_15_Double_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_15_Double_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_16_Double_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_16_Double_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_16_Double_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_16_Double_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_17_Double_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_17_Double_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_17_Double_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_17_Double_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_18_Double_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_18_Double_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_18_Double_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_18_Double_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_19_Double_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_19_Double_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_19_Double_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_19_Double_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_20_Double_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_20_Double_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_20_Double_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_20_Double_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }






        private void Score_1_Triple_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_1_Triple_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_1_Triple_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_1_Triple_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_2_Triple_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_2_Triple_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_2_Triple_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_2_Triple_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_3_Triple_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_3_Triple_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_3_Triple_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_3_Triple_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_4_Triple_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_4_Triple_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_4_Triple_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_4_Triple_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_5_Triple_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_5_Triple_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_5_Triple_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_5_Triple_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_6_Triple_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_6_Triple_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_6_Triple_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_6_Triple_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_7_Triple_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_7_Triple_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_7_Triple_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_7_Triple_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_8_Triple_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_8_Triple_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_8_Triple_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_8_Triple_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_9_Triple_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_9_Triple_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_9_Triple_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_9_Triple_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_10_Triple_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_10_Triple_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_10_Triple_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_10_Triple_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_11_Triple_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_11_Triple_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_11_Triple_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_11_Triple_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_12_Triple_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_12_Triple_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_12_Triple_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_12_Triple_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_13_Triple_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_13_Triple_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_13_Triple_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_13_Triple_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_14_Triple_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_14_Triple_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_14_Triple_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_14_Triple_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_15_Triple_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_15_Triple_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_15_Triple_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_15_Triple_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_16_Triple_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_16_Triple_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_16_Triple_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_16_Triple_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_17_Triple_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_17_Triple_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_17_Triple_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_17_Triple_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_18_Triple_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_18_Triple_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_18_Triple_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_18_Triple_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_19_Triple_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_19_Triple_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_19_Triple_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_19_Triple_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }

        private void Score_20_Triple_MouseEnter(object sender, RoutedEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseEnter(score);
        }

        private void Score_20_Triple_MouseLeave(object sender, MouseEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeave(score);
        }

        private void Score_20_Triple_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonDown(score);
        }

        private void Score_20_Triple_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Path score = sender as Path;
            Score_MouseLeftButtonUp(score);
        }
    }
}
