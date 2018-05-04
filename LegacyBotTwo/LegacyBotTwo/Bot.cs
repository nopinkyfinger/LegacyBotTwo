using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

// Eventually I'll move most of the functions over here, since having it all
// in main just seems silly

namespace LegacyBotTwo
{
    public class Bot
    {
        private DiscordSocketClient client;

        public Bot()
        {
            MainAsync().GetAwaiter().GetResult();
        }

        public async Task MainAsync()
        {
            client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Info
            });
            string path = System.AppDomain.CurrentDomain.BaseDirectory;

            client.Log += Log;
            client.MessageReceived += MessageReceived;

            string token = System.IO.File.ReadAllText(path + "/token.txt");
            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            await Task.Delay(-1);
        }

        private Task Log(LogMessage message)
        {
            Console.WriteLine(message.ToString());
            return Task.CompletedTask;
        }

        private async Task MessageReceived(SocketMessage message)
        {
            if (message.Content == "!ping")
                await message.Channel.SendMessageAsync("Pong!");
        }
    }
}

