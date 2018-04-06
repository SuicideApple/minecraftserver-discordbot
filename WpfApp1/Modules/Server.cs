using Discord;
using Discord.Commands;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Server : ModuleBase
    {
        public static StreamWriter WriteCommands;
        BackgroundWorker StartServer = new BackgroundWorker();
        Process Minecraft = new Process();
        public static Stopwatch stopwatch = new Stopwatch();

        string MaxRam = WpfApp1.Properties.Settings.Default.MaxRam;
        string MinRam = WpfApp1.Properties.Settings.Default.MinRam;
        string serverpath = WpfApp1.Properties.Settings.Default.Path;
        ushort serverport = WpfApp1.Properties.Settings.Default.ServerPort;
        

        public Server()
        {
            StartServer.DoWork += StartServer_DoWork;
        }

        public void StartServer_DoWork(object sender, DoWorkEventArgs e)
        {
            Minecraft.StartInfo.FileName = "cmd.exe";
            Minecraft.StartInfo.RedirectStandardInput = true;
            Minecraft.StartInfo.RedirectStandardOutput = true;
            Minecraft.StartInfo.RedirectStandardError = true;
            Minecraft.StartInfo.UseShellExecute = false;
            Minecraft.StartInfo.CreateNoWindow = true;
            Minecraft.OutputDataReceived += MainWindow._MainWindow.Minecraft_OutputDataReceived;
            Minecraft.Start();
            Minecraft.BeginOutputReadLine();
            Minecraft.BeginErrorReadLine();
            WriteCommands = Minecraft.StandardInput;

            WriteCommands.WriteLineAsync("java -Xmx" + MaxRam + "M -Xms" + MinRam + "M -jar " + serverpath + " " + "nogui");
            stopwatch.Reset();
            stopwatch.Start();
        }

        [Command("start")]
        public async Task SendStarted()
        {
            foreach (var process in Process.GetProcessesByName("java"))
            {
                await Context.Channel.SendMessageAsync($"A server was already running! I will kill it and start a new one!");
                await Task.Run(() => process.Kill());
            }
            if (WpfApp1.Properties.Settings.Default.JarFile == "")
            {
                await Context.Channel.SendMessageAsync($"Error, you never inputed a JarFile in the settings page!");
            }
            else
            {
                await Context.Channel.SendMessageAsync($"Starting server now, allow some time for startup...");
                await Task.Run(() => StartServer.RunWorkerAsync());
            }       
        }

        [Command("stop")]
        public async Task SendStoped()
        {
            await WriteCommands.WriteLineAsync("/stop");
            stopwatch.Stop();
            await Context.Channel.SendMessageAsync("Server Stopped!");
        }

        [Command("time set day")]
        public async Task SendTimeDay()
        {
            await WriteCommands.WriteLineAsync("/time set day");
            await Context.Channel.SendMessageAsync("Ive changed the time to day.");
        }

        [Command("time set night")]
        public async Task SendTimeNight()
        {
            await WriteCommands.WriteLineAsync("/time set night");
            await Context.Channel.SendMessageAsync("Ive changed the time to night.");
        }

        [Command("status")]
        public async Task stats()
        {
            MineStat ms = new MineStat("localhost", serverport);
            var builder = new EmbedBuilder();
            TimeSpan ts = stopwatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

            if (ms.IsServerUp() == true)
            {
                builder.WithTitle("Server Status");
                builder.AddField("Version:", ms.GetVersion());
                builder.AddField("Players:", ms.GetCurrentPlayers());
                builder.AddField("Max Players:", ms.GetMaximumPlayers());
                builder.AddField("Up:", ms.IsServerUp());
                builder.AddField("Uptime:", elapsedTime);

                if (ms.IsServerUp() == true) builder.WithColor(Color.Green);
            }
            else
            {
                builder.WithTitle("Server Status");
                builder.AddField("Server State:", "Down");
                builder.WithColor(Color.Red);
            }
            await Context.Channel.SendMessageAsync("", false, builder);
        }
    }
}
