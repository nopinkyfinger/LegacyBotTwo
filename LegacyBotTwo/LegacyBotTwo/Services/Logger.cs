// ****************************************************************************
// LEGACYBOT2 
// BY NOPINKY
// v0.1
// LOGGER
// This class is responsible for the logging, go figure.
//
// I want there to be seprate outputs for regular plaintext logs, and fancy RP
// logs. For regular log directory, I'm feeling something like
// /Logs/SERVER_NAME/YYYY/MM MONTH_NAME/CHANNEL_NAME/DD.txt eg.
// /Logs/Star Trek Legacy/2018/05 May/main-chat/15.txt
// This seems kind of clunky though, and I may tweak that.
// For RPs, it'll use a similar ID system or something, maybe a date and
// channel, like "05-15-18 rps2.txt" or something. Or maybe just the number
// it has for that day, like "05-15-2018 #1.txt" IDK, something to think about
// It'll upload to pastebin, like last time.
// 
// TODO:
// Make the RP logger
// Implement pastebin support
// Iron out the directory issue
// 
// ****************************************************************************

using Discord;
using Discord.WebSocket;
using System;
using System.Globalization;
using System.IO;

namespace LegacyBotTwo.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class Logger
    {
        private string topLogDirectory;
        private string chatLogDirectory;
        private string rpDirectory;
        private string year;
        private string month;
        private string monthFull;
        private string day;
        private const string RP_CHANNEL_KEYWORD = "rps";

        public Logger ()
        {
            year = DateTime.Now.ToString("yyyy");
            month = DateTime.Now.ToString("MM");
            monthFull = DateTime.Now.ToString("MMMM", CultureInfo.InvariantCulture);
            day = DateTime.Now.ToString("dd");
            topLogDirectory = System.AppDomain.CurrentDomain.BaseDirectory + "/Logs/";
            chatLogDirectory = topLogDirectory + year + '/' + month + ' '
                + monthFull + '/'; //TODO: find a more graceful solution

            if (!Directory.Exists(chatLogDirectory))
                Directory.CreateDirectory(chatLogDirectory);
        }

        public void changeDate()
        {
            year = DateTime.Now.ToString("yyyy");
            month = DateTime.Now.ToString("MM");
            monthFull = DateTime.Now.ToString("MMMM", CultureInfo.InvariantCulture);
            day = DateTime.Now.ToString("dd");
            chatLogDirectory = topLogDirectory + year + '/' + month + ' ' + monthFull + '/';
            if (!Directory.Exists(chatLogDirectory))
                Directory.CreateDirectory(chatLogDirectory);
        }

        public void logMessage(SocketMessage message)
        {
            if (day != DateTime.Now.ToString("dd"))
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
