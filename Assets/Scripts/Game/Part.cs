using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Game
{
    public class Part
    {
        public float Pitch { get; set; }

        public float Volume { get; set; }

        public float Tempo { get; set; }

        public string Instrument { get; set; }

        public bool IsVolumeValid = false;
        public bool IsPitchValid = false;
        public bool IsTempoValid = false;

        public Part(string name, float pitch, float vol, float temp)
        {
            Instrument = name;
            Pitch = pitch;
            Volume = vol;
            Tempo = temp;
        }

        public bool IsOnTune(InstrumentSection instrument)
        {
            IsVolumeValid = instrument.Volume == Volume;
            IsPitchValid = instrument.Pitch == Pitch;
            IsTempoValid = instrument.Tempo == Tempo;

            return IsVolumeValid && IsPitchValid && IsTempoValid;
        }
    }
}
