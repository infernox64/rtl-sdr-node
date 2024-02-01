using RtlSdrManager.Types;
using RtlSdrManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SQLite.Core;
using Newtonsoft.Json.Linq;


namespace ChannelLib
{
    public class Channel
    {
        private Frequency _freq;
        private Frequency _bandwidth;
        public Frequency Freq { get => _freq; set => _freq = value; }
        public Frequency Bandwidth { get => _bandwidth; set => _bandwidth = value; }
        private readonly string _name;
        public string Name { get => _name; }
        public Channel(string name, float freq, int bandwidth_KHz)
        {
            _name = name;
            _freq = new Frequency { MHz = freq };

            _bandwidth = new Frequency { KHz = bandwidth_KHz };


        }
        public void Tune(RtlSdrDeviceManager manager, string name)
        {
            manager[name].CenterFrequency = Freq;
            manager[name].TunerBandwidth = Bandwidth;

        }
    }
    public class CHANNELS
    {
        public static List<ChanStruct> chlist;

        
         
    }

 public public struct ChanStruct
 {
    public string name;
    public int bandwidth;
    public double frequency;
    
 }

    public class Chan_class
    {
        public int bandwidth { get; set; }
        public float frequency { get; set; }
    }



}
