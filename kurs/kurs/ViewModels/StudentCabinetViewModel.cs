using kurs.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace kurs.ViewModels
{
    public class StudentCabinetViewModel : BaseViewModel
    {

        private BaseViewModel _selectedViewModel;

        public BaseViewModel SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                OnProportyChanged(nameof(SelectedViewModel));
            }
        }


        public ICommand UpdateViewCommand { get; set; }

        public StudentCabinetViewModel()
        {
            UpdateViewCommand = new UpdateViewCommand(this);
        }

    }
}
