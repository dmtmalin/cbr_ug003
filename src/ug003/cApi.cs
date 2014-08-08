using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HidLibrary;

namespace ug003
{
    class cApi : IDisposable
    {
        /// <summary>
        /// vendor id cbr ug003
        /// </summary>
        private int vid = 0x1294;

        /// <summary>
        /// Usb notifier
        /// </summary>
        private HidDevice dev;

        private Color _current_color;       

        private bool _isConnected;
        public bool isConnected
        {
            get { return _isConnected; }
        }
        public cApi(int device_num = 0)
        {
            var devs = HidDevices.Enumerate(vid).ToList();

            var count = devs.Count;
            if(count > 0)
            {
                if(count - 1 < device_num)
                    device_num = 0;

                dev = devs[device_num];

                _isConnected = true;
            }
        }

        public enum Color
        {            
            red = 0x01,
            green,
            light_green,
            purple,
            lilac,
            blue,
            light_blue
        }

        public void setColor(Color color)
        {            
            send(color);
        }        

        private Color nextColor()
        {
            var next = Enum.GetValues(typeof(Color)).Cast<Color>()
                .Where(i => (int)i > (int)_current_color).OrderBy(i => i).FirstOrDefault();

            if (next == default(Color))            
                next = Color.red;

            _current_color = next;

            return next;
        }

        public void setNextColor()
        {
            var next = this.nextColor();
            this.setColor(next);
        }

        private void send(object obj)
        {
            if(dev != null)
            {
                var data = new byte[5];
                data[0] = (byte)(int)obj;

                var report = dev.CreateReport();
                report.Data = data;

                dev.WriteReport(report);                
            }
        }        

        public void Off()
        {
            send(0x00);
        }

        public void Dispose()
        {
            if(dev != null)
            {
                dev.Dispose();
            }
        }
    }
}
