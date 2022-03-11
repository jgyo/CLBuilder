using CLBuilder.viewModel;
using Ookii.Dialogs.Wpf;
using System;
using System.IO;

namespace CLBuilder.Commands
{
    public class SaveChecklistControlAsCommand : BaseCommand
    {
        private readonly MainViewModel viewModel;

        public SaveChecklistControlAsCommand(MainViewModel mainViewModel)
        {
            this.viewModel = mainViewModel;
        }

        public override void Execute(object parameter)
        {

            if (!viewModel.CanContinue())
            {
                return;
            }

            var of = new VistaOpenFileDialog
            {
                AddExtension = true,
                CheckFileExists = false,
                CheckPathExists = true,
                DefaultExt = "clbt",
                Multiselect = false,
                Title = "Save Checklist Data File As",
                ValidateNames = true
            };

            if (string.IsNullOrEmpty(viewModel.FullFilename))
            {
                of.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            else
            {
                of.InitialDirectory = Path.GetDirectoryName(viewModel.FullFilename);
                of.FileName = Path.GetFileName(viewModel.FullFilename);
            }

            var result = of.ShowDialog();
            if (result != true)
            {
                return;
            }

            viewModel.FullFilename = of.FileName;


            var model = viewModel.ChecklistControlViewModel.Store();
            var json = model.JsonSerializer();

            File.WriteAllText(viewModel.FullFilename, json);
        }
    }
}
