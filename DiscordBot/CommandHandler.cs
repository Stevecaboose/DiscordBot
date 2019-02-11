using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using DiscordBot.Core.LevelingSystem;
using DiscordBot.Core.UserAccounts;

namespace DiscordBot
{
    class CommandHandler
    {
        DiscordSocketClient _client;
        CommandService _service;
        readonly IServiceProvider services;


        public async Task InitalizeAsync(DiscordSocketClient client)
        {
            _client = client;
            _service = new CommandService();
            await _service.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: null);
            _client.MessageReceived += HandleCommandAsync;
        }

        //calls this function when the bot recieves a message
        private async Task HandleCommandAsync(SocketMessage s)
        {
            var msg = s as SocketUserMessage;
            if (msg == null) return;
            var context = new SocketCommandContext(_client, msg);
            if (context.User.IsBot) return;

            // mute check
            var userAccount = UserAccountList.GetAccount(context.User);
            if (userAccount.IsMuted)
            {
                await context.Message.DeleteAsync();
                return;
            }

            //leveling up logic


            Leveling.UserSentMessage((SocketGuildUser)context.User, (SocketTextChannel)context.Channel);

            int argPos = 0;
            if(msg.HasStringPrefix(Config.bot.cmdPrefix, ref argPos) 
                || msg.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                var result = await _service.ExecuteAsync(context, argPos, services);

                //if the results is not successful and is not an unknown command, print the reason why we got the error
                if(!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                {
                    Console.WriteLine(result.ErrorReason);
                }
            }
        }
    }
}
