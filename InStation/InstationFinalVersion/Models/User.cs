using System;
using System.Collections.Generic;
using System.Text;

namespace InstationFinalVersion.Models
{
    public class User
    {
        public int ID { get; set; }

        public string UserName { get; set; }

        public int ChannelID { get; set; }

        public string Password { get; set; }
    }
}
