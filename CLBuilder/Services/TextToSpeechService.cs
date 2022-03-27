using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;

namespace CLBuilder.Services
{
    public class TextToSpeechService
    {
        private static List<string> installedVoices;
        public static List<string> InstalledVoices
        {
            get
            {
                if(installedVoices == null)
                {
                    installedVoices = new List<string>();
                    var synth = new SpeechSynthesizer();
                    foreach (var item in synth.GetInstalledVoices())
                    {
                        if(item.Enabled)
                            installedVoices.Add(item.VoiceInfo.Name);
                    }

                    installedVoices.Sort();
                }

                return installedVoices;
            }
        }

        public static void Speak(string speaker, string prompt, int rate)
        {
            var synth = new SpeechSynthesizer();
            synth.SetOutputToDefaultAudioDevice();
            synth.SelectVoice(speaker);
            synth.Rate = rate;
            _ = synth.SpeakAsync(prompt);
        }

        public static void Speak(string prompt)
        {
            var synth = new SpeechSynthesizer();
            synth.SetOutputToDefaultAudioDevice();
            _ = synth.SpeakAsync(prompt);
        }
    }
}
