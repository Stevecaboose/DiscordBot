using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.API;

namespace TestDiscordBot
{
    class Program
    {
        


        private DiscordSocketClient _client;
        private CommandHandler _handler;

        // we want to start our program asynchronously so we start with StartAsync() instead of Main()
        static void Main(string[] args) => new Program().StartAsync().GetAwaiter().GetResult();


        public async Task StartAsync()
        {

            string name = Environment.UserName;
            string botName = "TestBot";

            string message = Utilities.GetFormattedAlert("WELCOME_&NAME_&BOTNAME", name, botName);
            
            Console.WriteLine(message);


            //if the token is empty, do nothing
            if (Config.bot.token == "" || Config.bot.token == null) return;
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = Discord.LogSeverity.Verbose
            });

            //subscribe to a logevent
            _client.Log += Log;
            _handler = new CommandHandler();

            //login the client
            await _client.LoginAsync(Discord.TokenType.Bot, Config.bot.token);

            //start the client
            await _client.StartAsync();

            _handler = new CommandHandler();
            await _handler.InitalizeAsync(_client);

            //await till the operation ends
            await Task.Delay(-1);
        }

        private async Task Log(Discord.LogMessage msg)
        {
            Console.WriteLine(msg.Message);
            
        }
    }
}
