using CLBuilder.model;
using CLBuilder.viewModel;
using Ookii.Dialogs.Wpf;
using System;
using System.IO;

namespace CLBuilder.Commands
{
    public class OpenChecklistControlCommand : BaseCommand
    {
        private readonly MainViewModel viewModel;

        public OpenChecklistControlCommand(MainViewModel viewModel)
        {
            this.viewModel = viewModel;
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
                Filter = "All files|*.*|CLDB Files|*.cldb",
                FilterIndex = 1,
                Title = "Open Checklist Data File",
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

            var json = File.ReadAllText(viewModel.FullFilename);

            var model = ChecklistControlModel.JsonDeseralizer(json);
            viewModel.ChecklistControlViewModel = ChecklistControlViewModel.Load(model);

        }
    }
}
