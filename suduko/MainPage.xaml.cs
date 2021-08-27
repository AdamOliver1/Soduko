using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Toolkit.Uwp.UI.Controls;
using Windows.UI.Popups;
using Windows.UI;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace suduko
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        UIManager _uiManager;
        LogicManager _logicManager;
        Level level;
        bool toCheck = true;
        DispatcherTimer _timer;
        public MainPage()
        {
            this.InitializeComponent();
            _uiManager = new UIManager(cnvsSuduko);
            SetEvent();
            _logicManager = new LogicManager(_uiManager.SudukoSquare);
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            _timer.Tick += _timer_Tick;

        }

        private void _timer_Tick(object sender, object e)
        {
            _uiManager.SetColors();
        }

        private void SetEvent()
        {
            for (int i = 0; i < _uiManager.SudukoSquare.TexBoxes.GetLength(0); i++)
            {
                for (int j = 0; j < _uiManager.SudukoSquare.TexBoxes.GetLength(1); j++)
                {
                    _uiManager.SudukoSquare.TexBoxes[i, j].TexBox.TextChanging += TexBox_TextChanging;
                }
            }
        }

        private async void TexBox_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            if (!toCheck) return;
            TextBox _selectedTextBox = sender as TextBox;
            TextBoxSudoku t = _uiManager.SudukoSquare.GetTexboxesAsList().Find(tb => tb.TexBox.Equals(_selectedTextBox));
            if (!_logicManager.CheckTextBoxInputValidation(t))
            {
                MessageDialog md = new MessageDialog("Wrong Input");
                await md.ShowAsync();
                _selectedTextBox.Text = string.Empty;
            }
           
        }

        private void radioButtonEasy_Checked(object sender, RoutedEventArgs e)
        {
            level = Level.Easy;        
        }

        private void radioButtonMedium_Checked(object sender, RoutedEventArgs e)
        {
            level = Level.Medium;        
        }

        private void radioButtonHard_Checked(object sender, RoutedEventArgs e)
        {
            level = Level.Hard;       
        }

        private void btnNewGame_Click(object sender, RoutedEventArgs e)
        {
            toCheck = false;
            _logicManager.NewGame(level);        
            toCheck = true;
            _timer.Stop();
            _uiManager.InitColor();
        }

        private void btnSolution_Click(object sender, RoutedEventArgs e)
        {
            toCheck = false;
            _logicManager.ShowSolution();
            toCheck = true;
            _timer.Start();
        }

        private async void btnCheckBoard_Click(object sender, RoutedEventArgs e)
        {
            string message = "";
            if (_logicManager.CheckBoard())
                message = "Good Job! You Did It!";
            else
                message = "The Board is'nt valid, Try again... You can do it!";

            MessageDialog md = new MessageDialog(message);
            await md.ShowAsync();

        }
    }
}
