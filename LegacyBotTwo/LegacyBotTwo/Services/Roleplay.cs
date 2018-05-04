// ============================================================================
// This class represents a roleplay.
// ============================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace LegacyBotTwo.Services
{
    ///
    public class Roleplay
    {
        private int id;
        private int channel;
        private DateTime startingDate;

        /// <summary>
        /// An instance of a roleplay
        /// </summary>
        /// <param name="channelNum">The number of the channel the RP is taking place in</param>
        public Roleplay(int channelNum)
        {
            channel = channelNum;
            startingDate = DateTime.Now;
        }


    }
}
