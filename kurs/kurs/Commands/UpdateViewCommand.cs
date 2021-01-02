using kurs.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace kurs.Commands
{
    public class UpdateViewCommand : ICommand
    {
        private cabinetViewModel viewModel;
        private StudentCabinetViewModel studviewModel;

        public UpdateViewCommand(cabinetViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public UpdateViewCommand(StudentCabinetViewModel viewModel)
        {
            this.studviewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter.ToString() == "list")
                viewModel.SelectedViewModel = new listViewModel();
            if(parameter.ToString() == "addInfoSub")
                viewModel.SelectedViewModel = new addInfoSub();
            if (parameter.ToString() == "addOtr")
                viewModel.SelectedViewModel = new addInfoOtr();
            if (parameter.ToString() == "addStud")
                viewModel.SelectedViewModel = new addNewStudents();



            if (parameter.ToString() == "studentSubbot")
                studviewModel.SelectedViewModel = new infoSubbotStudent();
            if (parameter.ToString() == "studentOtr")
                studviewModel.SelectedViewModel = new infoOtrStudent();
            if (parameter.ToString() == "studentInfo")
                studviewModel.SelectedViewModel = new studetInfo();
            if (parameter.ToString() == "info")
                studviewModel.SelectedViewModel = new infoOtrAndSub();

        }
    }
}
