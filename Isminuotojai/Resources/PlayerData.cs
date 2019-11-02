using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isminuotojai.Resources
{
    public class PlayerData
    {
        [JsonProperty("userName")]
        public string userName;
        [JsonProperty("password")]
        public string password;
    }
}
