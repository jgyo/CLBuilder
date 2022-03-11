using CLBuilder.viewModel;

namespace CLBuilder.Commands
{
    public class AddInstructionCommand : BaseCommand
    {
        private readonly ChecklistEditorViewModel viewModel;

        public AddInstructionCommand(ChecklistEditorViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            viewModel.Instructions.Add(new ChecklistInstructionViewModel());
        }
    }
}
