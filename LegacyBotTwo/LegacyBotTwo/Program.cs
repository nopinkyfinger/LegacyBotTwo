using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using System.Reflection;

namespace LegacyBotTwo
{
    class Program
    {
        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            var client = new DiscordSocketClient();
            string path = System.AppDomain.CurrentDomain.BaseDirectory;

            client.Log += Log;
            client.MessageReceived += MessageReceived;

            string token = System.IO.File.ReadAllText(path + "/token.txt");
            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private async Task MessageReceived(SocketMessage message)
        {
            if (message.Content == "!ping")
                await message.Channel.SendMessageAsync("Pong!");
        }
    }
}
