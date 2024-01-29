using RtlSdrManager;
using RtlSdrManager.Types;
using Newtonsoft.Json;
using System.Linq;
using System.Data.Common;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Dynamic;

namespace SDRConfig
{
    public enum SAMPLE_RATES
    {
        Rate_256K = 0,
        Rate_1024 = 1,
        Rate_2048 = 2,
        Rate_256 = 3,
        Rate_288 = 4,
        Rate_322 = 5
    }
    public class SAMPLERATE
    {
        private static readonly List<double> _rates = new List<double>{
            0.256,
            1.024,
            2.045,
            2.560,
            2.880,
            3.220
        };
        private static readonly List<string> RATES = new List<string>
        {
            "Rate_256K",
            "Rate_1024",
            "Rate_2048",
            "Rate_256",
            "Rate_288",
            "Rate_322"
        };
        public static  Frequency Get_Rate(SAMPLE_RATES r)
        {
            int selectedrate = (int)r;
            return new Frequency { MHz = _rates[selectedrate] };
        }
        public double Get_rate_float(SAMPLE_RATES r)
        {
            int selectedrate = (int)r;
            return _rates[selectedrate];
        }
        public static readonly double Rate_256K = 0.256;
        static readonly double Rate_1024 = 1.024f;
        public static readonly double Rate_2048 = 2.048f;
        public static readonly double Rate_256 = 2.560f;
        public static readonly double Rate_288 = 288.0f;
         public static readonly double Rate_322K = 322.0f;
        private static Frequency _rate;
        private static SAMPLE_RATES selected_rate;

        public static Frequency Rate { get => _rate; set => value; }

        public SAMPLERATE(SAMPLE_RATES rate)
        {
            selected_rate = rate;
            _rate = Get_Rate(rate);
        }
        public static Frequency Get_RateByName(string rname)
        {
            if (!RATES.Contains(rname))
            {
                string errmsg = $"{rname} is not a valid rate name";
                throw new Exception(errmsg);
                
            }
            int ridx = RATES.IndexOf(rname);
            return new Frequency { MHz = _rates[ridx] };
        }
        public Frequency GetFreq()
        {
            return Rate
        }
        public static bool IsValid(double rate)
        {
            if (_rates.Contains(rate))
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
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
            manager[name].TunerBandwidth  = Bandwidth;
            
        }
    }
    public struct rtl_Config
    {
    
        private string name;
        private Frequency _sampleRate;
        private RtlSdrDeviceManager _manager;

        public string Name { get => name; set => name = value; }
        public Frequency SampleRate { get => _sampleRate; set => _sampleRate = value; }
        public void set_sampleRate(double rate)
        {
            if (SAMPLERATE.IsValid(rate)) 
                _sampleRate = new Frequency { KHz = rate };
            _manager[name].SampleRate = _sampleRate;
        }

        public rtl_Config(RtlSdrDeviceManager manager, string devname,SAMPLE_RATES sampRate)
        {
            //Srates = new SAMPLERATE(sampRate);

            name = devname;
            _sampleRate = SAMPLERATE.Get_Rate(sampRate);
            _manager = manager;
        }
    }
    public class SDR_Config
    {
        public List<Channel> channels;
        private static rtl_Config _config;
        public SDR_Config(rtl_Config cfg) 
        {
            _config = cfg;

        }
         static SDR_Config()
        {

        }
        {

        }

    }
}