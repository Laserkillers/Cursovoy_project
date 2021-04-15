using System;
using System.Collections.Generic;
using System.Text;

namespace Cursovoy_project.ViewModel
{
    class MenuViewModel
    {
        public MenuViewModel() { }

        public IMainWindowsCodeBehind CodeBehind { get; set; }

        /// <summary>
        /// Переход к начальной странице
        /// </summary>
        private RelayCommand _LoadStartPageCommand;
        public RelayCommand LoadStartPageCommand
        {
            get
            {
                return _LoadStartPageCommand = _LoadStartPageCommand ??
                    new RelayCommand(OnLoadFirstPage, CanLoadFirstPage);
            }
        }
        private bool CanLoadFirstPage()
        {
            return true;
        }
        private void OnLoadFirstPage()
        {
            CodeBehind.LoadWiew(View_number.Main);
        }

        /// <summary>
        /// Переход к Следующим страницам
        /// </summary>

        private RelayCommand _LoadLoginPageCommand;
        public RelayCommand LoadLoginPageCommand
        {
            get
            {
                return _LoadLoginPageCommand = _LoadLoginPageCommand ?? new RelayCommand(OnLoadLoginPage, CanLoadLoginPage);
            }
        }

        private bool CanLoadLoginPage()
        {
            return true;
        }
        private void OnLoadLoginPage()
        {
            CodeBehind.LoadWiew(View_number.Login);
        }
    }
}
