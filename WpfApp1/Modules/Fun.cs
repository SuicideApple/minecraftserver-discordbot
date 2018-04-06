using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace WpfApp1.Modules
{
    public class Fun : ModuleBase
    {
        
        Random rand = new Random();
        Random random = new Random();

        [Command("8ball")]
        [Summary("Answer a question")]
        public async Task Eightball([Required, Remainder] string question = null)
        {
            string[] ans = new string[] {
                        "It is certain",
                        "It is decidedly so",
                        "Without a doubt",
                        "Yes, definitely",
                        "You may rely on it",
                        "As I see it, yes",
                        "Most likely",
                        "Outlook good",
                        "Yes",
                        "Signs point to yes",
                        "Reply hazy try again",
                        "Ask again later",
                        "Better not tell you now",
                        "Cannot predict now",
                        "Concentrate and ask again",
                        "Don't count on it",
                        "My reply is no",
                        "My sources say no",
                        "Outlook not so good",
                        "Very doubtful"
                    };
            EmbedBuilder eb = new EmbedBuilder();
            eb.Description = $"**Question: {question}**\n{ans[new Random().Next(ans.Length)]}";
            await ReplyAsync("", false, eb);
        }

        [Command("rr")]
        [Summary("Do it motherfucker! Pull the fucking trigger!")]
        public async Task Russia()
        {
            
            int willKill = rand.Next(7);
            int random1 = random.Next(3);

            var builder = new EmbedBuilder();

            if (MainWindow._MainWindow.bulletsleft < 5)
            {
                switch (willKill)
                {
                    case 0:
                        builder.WithTitle("Russian Roullete:");
                        builder.AddInlineField("You the muzzle to your head... and pull the trigger...", "The chamber was empty, gg fucker.");
                        builder.WithColor(Color.Green);
                        await Context.Channel.SendMessageAsync("", false, builder);

                        MainWindow._MainWindow.bulletsleft++;
                        break;
                    case 1:
                        builder.WithTitle("Russian Roullete:");
                        builder.AddInlineField("You the muzzle to your head... and pull the trigger...", "You hear a click, and your friend starts crying knowing hes probably gonna die.");
                        builder.WithColor(Color.Green);
                        await Context.Channel.SendMessageAsync("", false, builder);

                        MainWindow._MainWindow.bulletsleft++;
                        MainWindow._MainWindow.bulletsleft++;
                        break;
                    case 2:
                        builder.WithTitle("Russian Roullete:");
                        builder.AddInlineField("You the muzzle to your head... and pull the trigger...", "You survive. Why wont you just die?");
                        builder.WithColor(Color.Green);
                        await Context.Channel.SendMessageAsync("", false, builder);

                        MainWindow._MainWindow.bulletsleft++;
                        break;
                    case 3:
                        builder.WithTitle("Russian Roullete:");
                        builder.AddInlineField("You the muzzle to your head... and pull the trigger...", "There was not a bullet in the gun, lucky shit.");
                        builder.WithColor(Color.Green);
                        await Context.Channel.SendMessageAsync("", false, builder);

                        MainWindow._MainWindow.bulletsleft++;
                        MainWindow._MainWindow.bulletsleft++;
                        MainWindow._MainWindow.bulletsleft++;
                        break;
                    case 4:
                        builder.WithTitle("Russian Roullete:");
                        builder.AddInlineField("You the muzzle to your head... and pull the trigger...", "You survived, and then grinned at your nibba.");
                        builder.WithColor(Color.Green);
                        await Context.Channel.SendMessageAsync("", false, builder);

                        MainWindow._MainWindow.bulletsleft++;
                        MainWindow._MainWindow.bulletsleft++;
                        MainWindow._MainWindow.bulletsleft++;
                        MainWindow._MainWindow.bulletsleft++;
                        MainWindow._MainWindow.bulletsleft++;
                        break;
                    case 5:
                        builder.WithTitle("Russian Roullete:");
                        builder.AddInlineField("You the muzzle to your head... and pull the trigger...", "And then you bitch out, fucking faggot.");
                        builder.WithColor(Color.Green);
                        await Context.Channel.SendMessageAsync("", false, builder);
                        break;
                    case 6:
                        builder.WithTitle("Russian Roullete:");
                        builder.AddInlineField("You the muzzle to your head... and pull the trigger...", "And nothing happens, sadly");
                        builder.WithColor(Color.Green);
                        await Context.Channel.SendMessageAsync("", false, builder);
                        break;
                    case 7:
                        builder.WithTitle("Russian Roullete:");
                        builder.AddInlineField("You the muzzle to your head... and pull the trigger...", "You dont get shot, but your depressed and kill yourself anyway.");
                        builder.WithColor(Color.Green);
                        await Context.Channel.SendMessageAsync("", false, builder);
                        break;
                }
            }

            else
            {
                switch (random1)
                {
                    case 1:
                        builder.WithTitle("Russian Roullete:");
                        builder.AddInlineField("You the muzzle to your head... and pull the trigger...", "And the bullet goes through your head, and into your friends.");
                        builder.WithColor(Color.Green);
                        await Context.Channel.SendMessageAsync("", false, builder);
                        MainWindow._MainWindow.bulletsleft = 0;
                        break;
                    case 2:
                        builder.WithTitle("Russian Roullete:");
                        builder.AddInlineField("You the muzzle to your head... and pull the trigger...", "The bullet turns your head into a bloody mess, and your friends laugh at you.");
                        builder.WithColor(Color.Green);
                        await Context.Channel.SendMessageAsync("", false, builder);
                        MainWindow._MainWindow.bulletsleft = 0;
                        break;
                    case 3:
                        builder.WithTitle("Russian Roullete:");
                        builder.AddInlineField("You the muzzle to your head... and pull the trigger...", "The bullet hits your head, and somehow fucking ricochets, leaving you with crippling depression.");
                        builder.WithColor(Color.Green);
                        await Context.Channel.SendMessageAsync("", false, builder);
                        MainWindow._MainWindow.bulletsleft = 0;
                        break;
                }
            }
        }           
    }
} 
            
