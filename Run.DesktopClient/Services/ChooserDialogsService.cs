using Run.Core;
using System.Linq;
using System.Windows.Forms;

namespace Run.DesktopClient
{
    public class ChooserDialogsService : IChooserDialogsService
    {
        public string ChooseDirectory(string description, string selectedDirectory)
        {
            var dialog = new FolderBrowserDialog
            {
                Description = description,
                ShowNewFolderButton = false,
                SelectedPath = selectedDirectory
            };

            return dialog.ShowDialog() == DialogResult.OK
                ? dialog.SelectedPath
                : null;
        }

        public string ChooseFile(string title, string selectedFile, params string[] allowedExtensions)
        {
            var dialog = new OpenFileDialog
            {
                CheckFileExists = true,
                CheckPathExists = true,
                Multiselect = false,
                FileName = selectedFile,
                Title = title
            };

            if (!allowedExtensions.IsNullOrEmpty())
                dialog.Filter = string.Join("|", allowedExtensions.Select(ext => $"{ext} files (*.{ext})|*.{ext}"));

            return dialog.ShowDialog() == DialogResult.OK
                ? dialog.FileName
                : null;
        }
    }
}