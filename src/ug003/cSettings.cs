using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ug003
{
    class cSettings
    {
        public static int Usb
        {
            get { return Properties.Settings.Default.usb; }
        }

        public static int Audio
        {
            get { return Properties.Settings.Default.audio; }
        }

        public static int Rate
        {
            get { return Properties.Settings.Default.rate; }
        }

        public static int Chanel
        {
            get { return Properties.Settings.Default.chanel;  }
        }

        public static float Trigger
        {
            get { return Properties.Settings.Default.trigger; }
        }
    }
}
