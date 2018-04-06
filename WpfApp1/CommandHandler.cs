using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class CommandHandler
    {
        private CommandService commands { get; set; }
        private DiscordSocketClient client { get; set; }

        public async Task install(DiscordSocketClient client)
        {
            this.client = client;
            commands = new CommandService();

            await commands.AddModulesAsync(Assembly.GetEntryAssembly());
            client.MessageReceived += HandleCommand;
        }

        private async Task HandleCommand(SocketMessage Msg)
        {
            var msg = Msg as SocketUserMessage;
            if (msg == null) return;

            int argPos = 0;
            if (!(msg.HasStringPrefix(";;", ref argPos) || msg.HasMentionPrefix(client.CurrentUser, ref argPos))) return;

            var context = new CommandContext(client, msg);
            var result = await commands.ExecuteAsync(context, argPos);

            if (!result.IsSuccess) await context.Channel.SendMessageAsync(result.ErrorReason);
        }
    }
}
