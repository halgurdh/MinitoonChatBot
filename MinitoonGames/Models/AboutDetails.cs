using CsvHelper;
using CsvHelper.Configuration.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MinitoonGames.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class AboutDetails
    {
        [Name("aboutInfo")]
        public string aboutInfo { get; set; }

        public AboutDetails()
        {
            aboutInfo = "Minitoon Games was founded in the summer of 2016\n\n by two developers and good friends located in The Netherlands. \n\n Before the duo were into game development\n\n they were already into developing in general,\n\n because of their study.\n\n At one point they were introduced with:\n\n the Multi - platform game engine Unity.\n\n Our strategy is to maximize reusability by developing\n\n in -house tooling on top of the open source game framework Phaser.\n\n Feel free to contact us any time.";
        }
    }
}
