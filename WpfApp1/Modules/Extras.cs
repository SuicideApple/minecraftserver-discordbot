using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Modules
{
    public class Extras : ModuleBase
    {
        //Info Command//
        [Command("info")]

        public async Task SendInfo()
        {
            await Context.Channel.SendMessageAsync($"Hello, I am a bot called {Context.Client.CurrentUser.Username} written in Discord.Net");
        }

        private readonly CommandService _service;

        public Extras(CommandService service)
        {
            _service = service;
        }
        ////////////////

        //Help Command//
        [Command("help")]

        public async Task HelpAsync()
        {
            string prefix = ";;";
            var builder = new EmbedBuilder()
            {
                Color = new Color(255, 0, 208),
                Description = "You dont know the commands? What are you fuckin gay?"
            };

            foreach (var module in _service.Modules)
            {
                string description = null;
                foreach (var cmd in module.Commands)
                {
                    var result = await cmd.CheckPreconditionsAsync(Context);
                    if (result.IsSuccess)
                        description += $"{prefix}{cmd.Aliases.First()}\n";
                }

                if (!string.IsNullOrWhiteSpace(description))
                {
                    builder.AddField(x =>
                    {
                        x.Name = module.Name;
                        x.Value = description;
                        x.IsInline = false;
                    });
                }
            }
            await ReplyAsync("", false, builder.Build());
        }
        ////////////////
    }
}
