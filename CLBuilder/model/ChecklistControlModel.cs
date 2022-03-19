// ***********************************************************************
// Assembly         : CLBuilder
// Author           : gilyo
// Created          : 03-10-2022
//
// Last Modified By : gilyo
// Last Modified On : 03-10-2022
// ***********************************************************************
// <copyright file="ChecklistControlModel.cs" company="Gil Yoder">
//     Copyright © by Gil Yoder 2022
// </copyright>
// <summary></summary>
// ***********************************************************************
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
    /// <summary>
    /// Class ChecklistControlModel.
    /// </summary>
    [DataContract]
    public class ChecklistControlModel
    {
        /// <summary>
        /// The aircraft short name
        /// </summary>
        private string aircraftShortName;
        /// <summary>
        /// The voice
        /// </summary>
        private string voice;

        /// <summary>
        /// Gets or sets the short name of the aircraft.
        /// </summary>
        /// <value>The short name of the aircraft.</value>
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

        /// <summary>
        /// Gets or sets the voice.
        /// </summary>
        /// <value>The voice.</value>
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

        /// <summary>
        /// Gets or sets the voice rate.
        /// </summary>
        /// <value>The voice rate.</value>
        [DataMember]
        public int VoiceRate { get; set; }

        /// <summary>
        /// Gets or sets the voice volume.
        /// </summary>
        /// <value>The voice volume.</value>
        [DataMember]
        public int VoiceVolume { get; set; }

        /// <summary>
        /// Gets the checklists.
        /// </summary>
        /// <value>The checklists.</value>
        [DataMember]
        public List<ChecklistModel> Checklists { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChecklistControlModel"/> class.
        /// </summary>
        public ChecklistControlModel()
        {
            VoiceRate = 0;
            VoiceVolume = 75;
            Voice = "Microsoft Zira Desktop";
            Checklists = new List<ChecklistModel>();
            InstallFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"LorbyAxisAndOhs Files\Scripts");
        }

        /// <summary>
        /// Gets or sets the scripts folder.
        /// </summary>
        /// <value>The scripts folder.</value>
        [DataMember]
        public string InstallFolder { get; set; }

        /// <summary>
        /// Gets the control text.
        /// </summary>
        /// <value>The control text.</value>
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

        /// <summary>
        /// Serializes this into json text.
        /// </summary>
        /// <returns>System.String.</returns>
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

        /// <summary>
        /// Deseralizes json text inti a ChecklistControlModel.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <returns>ChecklistControlModel.</returns>
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
