using Discord;
using Discord.WebSocket;
using System;
using System.Globalization;
using System.IO;

namespace LegacyBotTwo.Services
{
    public class Logger
    {
        private string topLogDirectory;
        private string chatLogDirectory;
        private string rpDirectory;
        private string year;
        private string month;
        private string day;

        public Logger ()
        {
            year = DateTime.Now.ToString("YYYY");
            month = DateTime.Now.ToString("MM") + DateTime.Now.ToString("MMMM", CultureInfo.InvariantCulture);
            day = DateTime.Now.ToString("DD");
            topLogDirectory = System.AppDomain.CurrentDomain.BaseDirectory + "/Logs/";
            chatLogDirectory = topLogDirectory + year + '/' + month; //TODO: find a more graceful solution

            if (!Directory.Exists(chatLogDirectory))
                Directory.CreateDirectory(chatLogDirectory);
        }

        public void changeDate()
        {
            year = DateTime.Now.ToString("YYYY");
            month = DateTime.Now.ToString("MM") + DateTime.Now.ToString("MMMM", CultureInfo.InvariantCulture);
            day = DateTime.Now.ToString("DD");
            chatLogDirectory = topLogDirectory + year + '/' + month + '/';
            if (!Directory.Exists(chatLogDirectory))
                Directory.CreateDirectory(chatLogDirectory);
        }

        public async void logMessage(SocketMessage message)
        {
            if (day != DateTime.Now.ToString("DD"))
                changeDate();
            string logText = '[' + DateTime.Now.ToString("hh:mm:ss") + "] " + message.Author + ": "+ message.Content;
            string logFile = chatLogDirectory + message.Source + DateTime.Now.ToString("MMM") + ' ' + day + ".txt";

            if (!File.Exists(logFile))
                File.Create(logFile);

            File.AppendAllText(logFile, logText + "\n");
            Console.Out.WriteLineAsync(logText);
        }

        public string formatMessage(SocketMessage msg)
        {
            string formattedMsg = '[' + DateTime.Now.ToString("hh:mm:ss") + "] " + msg.Author.ToString()
                + ": " + msg.Content.ToString();
            return formattedMsg;
        }

        public void logRP(LogMessage message)
        {
            // TODO: This will log RPs
        }
    }
}
