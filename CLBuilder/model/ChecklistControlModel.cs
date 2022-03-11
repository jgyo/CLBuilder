using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace CLBuilder.model
{
    [DataContract]
    public class ChecklistControlModel
    {
        private string aircraftShortName;
        private string voice;

        [DataMember]
        public string AircraftShortName
        {
            get
            {
                if (string.IsNullOrEmpty(aircraftShortName))
                {
                    return "<Aircraft>";
                }

                return aircraftShortName;
            }

            set => aircraftShortName = value;
        }

        [DataMember]
        public string Voice
        {
            get
            {
                if(string.IsNullOrEmpty(voice))
                {
                    return "<Voice>";
                }

                return voice;
            }

            set => voice = value;
        }

        [DataMember]
        public int VoiceRate { get; set; }

        [DataMember]
        public int VoiceVolume { get; set; }

        [DataMember]
        public List<ChecklistModel> Checklists { get; private set; }

        public ChecklistControlModel()
        {
            VoiceRate = 0;
            VoiceVolume = 75;
            Voice = "Microsoft Zira Desktop";
            Checklists = new List<ChecklistModel>();
            AircraftShortName = "C172";
            ScriptsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"LorbyAxisAndOhs Files\Scripts");
        }

        [DataMember]
        public string ScriptsFolder { get; set; }

        public string ControlText
        {
            get
            {
                StringBuilder text = new StringBuilder();
                // First line
                text.AppendLine($"<Macro Name \"{AircraftShortName}CLFOSPEAK\">VOICE:{Voice}) (VOICERATE:{VoiceRate}) (VOICEVOLUME:{VoiceVolume})");

                // Second line
                text.AppendLine("SPEAK</Macro>");

                // third line
                text.AppendLine($"(L:{AircraftShortName}CheckList) ++ (>L:{AircraftShortName}Checklist)");

                // Checklist lines
                int index = 1;
                foreach (var item in Checklists)
                {
                    text.AppendLine($"(L:{AircraftShortName}CheckList) {index} == if" + "{" + $" (CHECKLIST:{AircraftShortName}\\{index}_{item.Name}_cl.txt) " + "}");
                    index++;
                }

                // last line
                text.AppendLine($"(L:{AircraftShortName}CheckList) {index} == if" + "{" + $" 0 (>L:<{AircraftShortName}Checklist) 1 (>K:AAO_CL_STOP) 0 (>L:clphase) " + ")");

                return text.ToString();

            }
        }

        public string JsonSerializer()
        {
            var js = new DataContractJsonSerializer(this.GetType());
            var ms = new MemoryStream();
            js.WriteObject(ms, this);
            ms.Position = 0;
            var sr = new StreamReader(ms);
            var jsonText = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
            ms.Close();
            ms.Dispose();
            return jsonText;
        }

        public static ChecklistControlModel JsonDeseralizer(string json)
        {
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                var deserializer = new DataContractJsonSerializer(typeof(ChecklistControlModel));
                var model = (ChecklistControlModel) deserializer.ReadObject(ms);
                return model;
            }
        }

    }
}
