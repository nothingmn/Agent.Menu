using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation.Media;

namespace Agent.Menu.Menu
{
    public class MenuItem
    {

        public string Title { get; set; }
        public string CommandName { get; set; }
        public string CommandArg { get; set; }
        public bool Selected { get; set; }

    }
}
