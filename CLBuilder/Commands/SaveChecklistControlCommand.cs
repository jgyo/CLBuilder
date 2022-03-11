using CLBuilder.viewModel;
using Ookii.Dialogs.Wpf;
using System;
using System.IO;

namespace CLBuilder.Commands
{
    public class SaveChecklistControlCommand : BaseCommand
    {
        private readonly MainViewModel viewModel;

        public SaveChecklistControlCommand(MainViewModel mainViewModel)
        {
            this.viewModel = mainViewModel;
        }

        public override void Execute(object parameter)
        {

            if (!viewModel.CanContinue())
            {
                return;
            }

            if (string.IsNullOrEmpty(viewModel.FullFilename))
            {
                var of = new VistaOpenFileDialog
                {
                    AddExtension = true,
                    CheckFileExists = false,
                    CheckPathExists = true,
                    DefaultExt = "clbt",
                    Multiselect = false,
                    Title = "Save Checklist Data File",
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    ValidateNames = true
                };

                var result = of.ShowDialog();
                if (result != true)
                {
                    return;
                }

                viewModel.FullFilename = of.FileName;
            }

            var model = viewModel.ChecklistControlViewModel.Store();
            var json = model.JsonSerializer();

            File.WriteAllText(viewModel.FullFilename, json);
        }
    }
}
