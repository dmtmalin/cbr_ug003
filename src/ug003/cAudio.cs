using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAudio.Wave;

namespace ug003
{
    class cAudio : IDisposable
    {
        private WaveIn dev;

        /// <summary>
        /// trigger on event
        /// </summary>
        private float _trigger;

        public event EventHandler<WaveInEventArgs> ChangeLevelVolumeMax; 
        public cAudio(int device_num = 0, int rate = 8000, int chanel = 1, float trigger = 0.5f)
        {
            _trigger = trigger;

            var count = WaveIn.DeviceCount;
            if(count > 0)
            {
                if (count - 1 < device_num)
                    device_num = 0;

                dev = new WaveIn(WaveCallbackInfo.FunctionCallback());
                dev.DeviceNumber = device_num;
                dev.DataAvailable += dev_DataAvailable;
                dev.WaveFormat = new WaveFormat(rate, chanel);
                dev.StartRecording();
            }

        }

        void dev_DataAvailable(object sender, WaveInEventArgs e)
        {
            var max = getValueMax(e.Buffer, e.BytesRecorded);
            
            if(max > _trigger)
            {
                if(ChangeLevelVolumeMax != null)
                {                   
                    ChangeLevelVolumeMax(this, e);
                }
            }

        }

        private float getValueMax(byte[] buffer, int lenght)
        {
            List<float> l_sample32 = new List<float>();

            for (int index = 0; index < lenght; index += 2)
            {
                float sample32 = (float)(short)((buffer[index + 1] << 8) |
                                        buffer[index + 0]) / 32768f;

                l_sample32.Add(sample32);
            }

            var max = l_sample32.Max();

            return max;
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
