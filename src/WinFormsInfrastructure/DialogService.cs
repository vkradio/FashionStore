#nullable enable

using Ardalis.GuardClauses;
using MvvmInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;

namespace WinFormsInfrastructure
{
    public class DialogService : IDialogService
    {
        public DialogResultEnum PresentDialog(string question, DialogOptionsEnum options)
        {
            Guard.Against.NullOrEmpty(question, nameof(question));

            var button = options.MapTo<DialogOptionsEnum, MessageBoxButtons>();
            var dlgResult = MessageBox.Show(question, question.Length > 30 ? (question.Substring(0, 28) + "...") : question, button);
            return dlgResult.MapTo<DialogResult, DialogResultEnum>();
        }

        public void ShowMessage(string message) => MessageBox.Show(message);

        public string SelectFilePath()
        {
            //var openFileDialog = new OpenFileDialog();
            //return openFileDialog.ShowDialog() == true ? openFileDialog.FileName : null;
            throw new NotImplementedException();
        }

        public string SelectDirectoryPath()
        {
            //var openFolderDialog = new VistaFolderBrowserDialog();
            //return openFolderDialog.ShowDialog() == true ? openFolderDialog.SelectedPath : null;
            throw new NotImplementedException();
        }
    }
}

#nullable restore
