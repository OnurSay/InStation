using System;
using System.Collections.Generic;
using System.Text;

namespace InstationFinalVersion
{
    public class Channel
    {
        public int ID { get; set; }

        public string ChannelName { get; set; }

        public string ChannelDescription { get; set; }

        public int CurrentCategoryID { get; set; }

        public int FollowerCount { get; set; }

        public int ViewerCount { get; set; }

        public string ChannelPicture { get; set; }
    }
}
