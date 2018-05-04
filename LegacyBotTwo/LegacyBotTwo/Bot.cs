using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Discord;
using Discord.WebSocket;
using LegacyBotTwo.Services;

namespace LegacyBotTwo
{
    public class Bot
    {
        DiscordSocketClient client;
        private Logger logger;

        public Bot()
        {
            MainAsync().GetAwaiter().GetResult();
        }

        public async Task MainAsync()
        {
            var config = new DiscordSocketConfig();
            logger = new Logger();
            string token = getToken();

            config.MessageCacheSize = 100;
            config.LogLevel = LogSeverity.Info;
            client = new DiscordSocketClient(config);

            // Logs in
            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            client.Log += Log;
            client.MessageReceived += MessageReceived;

            await Task.Delay(-1);
        }

        private Task Log(LogMessage message)
        {
            Console.WriteLine(message.ToString());
            return Task.CompletedTask;
        }

        private async Task MessageReceived(SocketMessage message)
        {
            logger.logMessage(message);
        }

        private string getToken()
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory;
            string token = System.IO.File.ReadAllText(path + "/token.txt");
            return token;
        }
    }
}

