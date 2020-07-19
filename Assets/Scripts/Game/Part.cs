using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Game
{
    public class Part
    {      
        private float pitch;

        private float volume;

        private float tempo;

        private string instrument;

        public float Pitch
        {
            get { return pitch; }
            set { pitch = value; }
        }

        public float Volume
        {
            get { return volume; }
            set { volume = value; }
        }

        public float Tempo
        {
            get { return tempo; }
            set { tempo = value; }
        }

        public string Instrument
        {
            get { return instrument; }
            set { instrument = value; }
        }

        public Part(string name, float pitch, float vol, float temp)
        {
            instrument = name;
            this.pitch = pitch;
            volume = vol;
            tempo = temp;
        }
    }
}
