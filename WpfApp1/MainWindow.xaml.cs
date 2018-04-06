using System;
using System.Threading.Tasks;
using System.Windows;
using Discord;
using Discord.WebSocket;
using System.Diagnostics;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        DiscordSocketClient client;
        CommandHandler Handler;
        delegate void SetTextCallBack(string text);
        public static MainWindow _MainWindow;
        private System.Windows.Forms.NotifyIcon MyNotifyIcon;
        public int bulletsleft = 0;
        public int prevbulletcount;

        public MainWindow()
        {
            InitializeComponent();
            _MainWindow = this;
            MyNotifyIcon = new System.Windows.Forms.NotifyIcon();
            MyNotifyIcon.Icon = new System.Drawing.Icon(@"C:\Server\icon.ico");
            MyNotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(MyNotifyIcon_MouseDoubleClick);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (WpfApp1.Properties.Settings.Default.BotToken == "") MessageBox.Show("Please input your bot token before attempting to launch bot.", "Warning");
        }
        private async Task Client_Log(LogMessage arg)
        {
            Dispatcher.Invoke((Action)delegate
            {
                BotConsole.AppendText(arg + "\n");
                BotConsole.ScrollToEnd();
            });
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private async void SetGame_Click(object sender, RoutedEventArgs e)
        {
            await client.SetGameAsync(GameText.Text);
        }

        private async void Launch_Click(object sender, RoutedEventArgs e)
        {
            string token = Properties.Settings.Default.BotToken;

            Handler = new CommandHandler();
            client = new DiscordSocketClient(new DiscordSocketConfig()
            {
                LogLevel = LogSeverity.Verbose,
            });

            await Handler.install(client);
            client.Log += Client_Log;
            GameText.Text = Properties.Settings.Default.PlayingText;
            await client.SetGameAsync(GameText.Text);

            try
            {
                await client.LoginAsync(TokenType.Bot, token);
                await client.StartAsync();
            }

            catch
            {
                MessageBox.Show("An unknown error occured while logging in the bot!", "Error");
                return;
            }

            await Task.Delay(3000);
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            Settings sf = new Settings();
            sf.Show();
        }

        public void Minecraft_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Dispatcher.Invoke((Action)delegate
            {
                ServerOutput.AppendText(e.Data + "\n");
                ServerOutput.ScrollToEnd();
            });
        }

        private void RunCommand_Click(object sender, RoutedEventArgs e)
        {
            Server.WriteCommands.WriteLineAsync(ServerComand.Text);
        }

        void MyNotifyIcon_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.WindowState = WindowState.Normal;
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                MyNotifyIcon.Visible = true;
                this.ShowInTaskbar = false;
                MyNotifyIcon.BalloonTipTitle = "Minecraft Server Bot";
                MyNotifyIcon.BalloonTipText = "I will continue to run your server.";
                MyNotifyIcon.ShowBalloonTip(400);               
            }
            else if (this.WindowState == WindowState.Normal)
            {
                MyNotifyIcon.Visible = false;
                this.ShowInTaskbar = true;
            }
        }
    }
}
