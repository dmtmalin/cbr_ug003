using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace ug003
{
    public partial class Service1 : ServiceBase
    {
        cApi device;
        cAudio audio;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            device = new cApi(cSettings.Usb);           
         
            audio = new cAudio(cSettings.Audio,
                cSettings.Rate,
                cSettings.Chanel,
                cSettings.Trigger);
            audio.ChangeLevelVolumeMax += audio_ChangeLevelVolumeMax;
           
            base.OnStart(args);          
        }

        void audio_ChangeLevelVolumeMax(object sender, NAudio.Wave.WaveInEventArgs e)
        {
            device.setNextColor();            
        }

       

        protected override void OnStop()
        {
            device.Off();
            device.Dispose();
            audio.Dispose();

            base.OnStop();
        }
    }
}
