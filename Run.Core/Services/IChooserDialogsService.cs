namespace Run.Core
{
    /// <summary>
    /// Service used for working with dialogs that allow
    /// the user to choose a file or folder location.
    /// </summary>
    public interface IChooserDialogsService
    {
        string ChooseFile(string title, string selectedFile = null, params string[] allowedExtensions);

        string ChooseDirectory(string description, string selectedDirectory = null);
    }
}