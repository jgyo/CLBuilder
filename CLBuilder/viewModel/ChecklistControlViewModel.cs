using CLBuilder.Commands;
using CLBuilder.model;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace CLBuilder.viewModel
{
    public class ChecklistControlViewModel : ViewModelBase
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

        public string ScriptsFolder
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
                ScriptsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "LorbyAxisAndOhs Files", "Scripts");
            }
            catch
            {
                ScriptsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            }

            this.Voice = "Microsoft Zira Desktop";
            this.VoiceRate = 1;
            this.VoiceVolume = 75;

            AddChecklistCommand = new AddChecklistCommand(this);
            DeleteChecklistCommand = new DeleteChecklistCommand(this);
            MoveChecklistDownCommand = new MoveChecklistDownCommand(this);
            MoveChecklistUpCommand = new MoveChecklistUpCommand(this);
            InsertChecklistCommand = new InsertChecklistCommand(this);
            ViewChecklistTextCommand = new ViewChecklistTextCommand(this);
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
                ScriptsFolder = model.ScriptsFolder
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
            model.ScriptsFolder = ScriptsFolder;

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
    }
}