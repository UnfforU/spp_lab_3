using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AsmblyBrowserLib;
using Microsoft.Win32;
using static System.String;

namespace WpfAsmblyBrowser
{
    public class MainPageVM : INotifyPropertyChanged
    {
        public string OpenedFileName { get; set; } = "";
        public IEnumerable<TreeNode> Data { get; set; }
        public ICommand OpenFileCommand => new OpenFileCommand(StartAssembly);

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void StartAssembly()
        {
            var openFileDialog = new OpenFileDialog()
            {
                Filter = "Assemblies|*.dll;*.exe",
                Title = "Select assembly",
                Multiselect = false
            };

            if (openFileDialog.ShowDialog().Value)
            {
                OpenedFileName = openFileDialog.FileName;
                OnPropertyChanged(nameof(OpenedFileName));

                Data = MainPage.GetData(OpenedFileName);
                OnPropertyChanged(nameof(Data));
            }
        }
    }
}
