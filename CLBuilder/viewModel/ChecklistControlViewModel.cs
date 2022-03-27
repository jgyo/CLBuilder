using CLBuilder.Commands;
using CLBuilder.Extentions;
using CLBuilder.model;
using CLBuilder.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace CLBuilder.viewModel
{
    public class ChecklistControlViewModel : ViewModelBase, ITestTextToSpeech
    {
        private string aircraftShortName;
        private string voice;
        private int voiceRate;
        private int voiceVolume;
        private string scriptsFolder;
        private ChecklistEditorViewModel selectedChecklist;
        private int selectedIndex;

        public ICommand AddChecklistCommand { get; }
        public ICommand DeleteChecklistCommand { get; }
        public ICommand MoveChecklistUpCommand { get; }
        public ICommand MoveChecklistDownCommand { get; }
        public ICommand InsertChecklistCommand { get; }
        public ICommand ViewChecklistTextCommand { get; }
        public ICommand TestTextToSpeechCommand { get; }

        public List<String> InstalledVoices { get; }

        public string AircraftShortName
        {
            get => aircraftShortName;
            set { this.SetProperty(ref aircraftShortName, value); }
        }

        public string Voice
        {
            get => voice;
            set { this.SetProperty(ref voice, value); }
        }

        public int VoiceRate
        {
            get => voiceRate;
            set { this.SetProperty(ref voiceRate, value); }
        }

        public int VoiceVolume
        {
            get => voiceVolume;
            set { this.SetProperty(ref voiceVolume, value); }
        }

        public string InstallFolder
        {
            get => scriptsFolder;
            set { this.SetProperty(ref scriptsFolder, value); }
        }

        public ObservableCollection<ChecklistEditorViewModel> Checklists { get; private set; }
        public bool IsItemSelected
        { get { return SelectedIndex >= 0; } }

        public ChecklistEditorViewModel SelectedChecklist
        {
            get => selectedChecklist;
            set => SetProperty(ref selectedChecklist, value);
        }

        public int SelectedIndex
        {
            get => selectedIndex;
            set => SetProperty(ref selectedIndex, value);
        }

        public ChecklistControlViewModel()
        {
            Checklists = new ObservableCollection<ChecklistEditorViewModel>();

            try
            {
                InstallFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "LorbyAxisAndOhs Files");
            }
            catch
            {
                InstallFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            }

            // this.Voice = "Microsoft Zira Desktop";
            this.VoiceRate = 1;
            this.VoiceVolume = 75;

            AddChecklistCommand = new AddChecklistCommand(this);
            DeleteChecklistCommand = new DeleteChecklistCommand(this);
            MoveChecklistDownCommand = new MoveChecklistDownCommand(this);
            MoveChecklistUpCommand = new MoveChecklistUpCommand(this);
            InsertChecklistCommand = new InsertChecklistCommand(this);
            ViewChecklistTextCommand = new ViewChecklistTextCommand(this);
            TestTextToSpeechCommand = new TestTextToSpeechCommand(this);

            InstalledVoices = TextToSpeechService.InstalledVoices;
            if(InstalledVoices.Count > 0)
                this.Voice = InstalledVoices[0];
        }

        public void Clear()
        { }

        public static ChecklistControlViewModel Load(ChecklistControlModel model)
        {
            var vm = new ChecklistControlViewModel
            {
                AircraftShortName = model.AircraftShortName,
                Voice = model.Voice,
                VoiceRate = model.VoiceRate,
                VoiceVolume = model.VoiceVolume,
                InstallFolder = model.InstallFolder
            };

            foreach (var item in model.Checklists)
            {
                var vm2 = new ChecklistEditorViewModel(item);
                vm.Checklists.Add(vm2);
            }

            return vm;
        }

        public ChecklistControlModel Store(ChecklistControlModel model = null)
        {
            if (model == null)
            {
                model = new ChecklistControlModel();
            }

            model.AircraftShortName = AircraftShortName;
            model.Voice = Voice;
            model.VoiceRate = VoiceRate;
            model.VoiceVolume = VoiceVolume;
            model.InstallFolder = InstallFolder;

            model.Checklists.Clear();

            foreach (var item in Checklists)
            {
                var nextCheckListTitle = string.Empty;
                var index = Checklists.IndexOf(item);
                if (index < Checklists.Count() - 1)
                {
                    nextCheckListTitle = Checklists[index + 1].Title;
                }

                var m = new ChecklistModel
                {
                    Name = item.Name,
                    NextChecklistTitle = nextCheckListTitle,
                    Title = item.Title
                };

                m.ChecklistItems.Clear();
                foreach (var subItem in item.Instructions)
                {
                    var checklistItemModel = new ChecklistInstructionModel
                    {
                        CheckedResponse = subItem.CheckedResponse,
                        Instruction = subItem.Instruction,
                        Option = subItem.Option
                    };
                    m.ChecklistItems.Add(checklistItemModel);
                }

                model.Checklists.Add(m);
            }

            return model;
        }

        bool ITestTextToSpeech.CanExecute(object parameter)
        {
            return !Voice.IsNullOrEmpty() && InstalledVoices.Any(m => m == Voice);
        }

        void ITestTextToSpeech.TestTTS(object parameter)
        {
            var prompt = $"You have selected this voice with a rate of {VoiceRate}. {Voice}";
            TextToSpeechService.Speak(Voice, prompt, VoiceRate);
        }
    }
}