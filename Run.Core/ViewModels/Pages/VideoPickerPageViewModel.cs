using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using App = Run.Core.Resources.Localization.App;
using VideoPicker = Run.Core.Resources.Localization.VideoPickerPage;

namespace Run.Core
{
    public class VideoPickerPageViewModel : BaseViewModel
    {
        private const string VlcConfigKey = "VLC";
        private const string FolderConfigKey = "VideosFolder";
        private const string StartTimeConfigKey = "StartTime";
        private const string FullScreenConfigKey = "FullScreen";
        private const string FiltersConfigKey = "Filters";
        private const string FileConfigKey = "File";

        public string VlcLocation { get; set; }
        public string Folder { get; set; }
        public string Filters { get; set; }
        public uint StartTime { get; set; }
        public bool FullScreen { get; set; }

        public ICommand VlcLocationPickerCommand { get; }
        public ICommand FolderPickerCommand { get; }
        public ICommand RandomVideoCommand { get; }
        public ICommand CustomVideoCommand { get; }

        public VideoPickerPageViewModel()
        {
            VlcLocation = Core.Configuration.Get(VlcConfigKey);
            Folder = Core.Configuration.Get(FolderConfigKey);
            Filters = Core.Configuration.Get(FiltersConfigKey);
            StartTime = Core.Configuration.Get<uint>(StartTimeConfigKey);
            FullScreen = Core.Configuration.Get<bool>(FullScreenConfigKey);

            var chooserDialogs = Core.Get<IChooserDialogsService>();

            VlcLocationPickerCommand = new RelayCommand(() =>
            {
                VlcLocation = chooserDialogs.ChooseFile(
                    title: VideoPicker.TitleVlcLocation,
                    selectedFile: Core.Configuration.Get(FileConfigKey),
                    allowedExtensions: GetAllowedExtensions()
                );

                Core.Configuration.Set(VlcConfigKey, VlcLocation);
            });

            FolderPickerCommand = new RelayCommand(() =>
            {
                Folder = chooserDialogs.ChooseDirectory(
                    description: App.DirectoryHelp,
                    selectedDirectory: Folder
                );

                Core.Configuration.Set(FolderConfigKey, Folder);
            });

            RandomVideoCommand = new RelayCommand(PickRandomVideo);

            CustomVideoCommand = new RelayCommand(() => Run(chooserDialogs.ChooseFile(
                title: VideoPicker.TitleCustomVideo,
                selectedFile: Core.Configuration.Get(FileConfigKey),
                allowedExtensions: GetAllowedExtensions()
            )));
        }

        private void Run(string location)
        {
            Core.Configuration.Set(StartTimeConfigKey, StartTime);
            Core.Configuration.Set(FullScreenConfigKey, FullScreen);
            Core.Configuration.Set(FiltersConfigKey, Filters);
            Core.Configuration.Set(FileConfigKey, location);

            if (location != null)
                Task.Run(() =>
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = VlcLocation,
                        Arguments = $"\"{location}\" --start-time {StartTime} {(FullScreen ? " --fullscreen" : "")}"
                    });
                });
        }

        private void PickRandomVideo()
        {
            var allowedExtensions = GetAllowedExtensions();

            var files = allowedExtensions == null
                        ? Directory.GetFiles(Folder, "*.*", SearchOption.AllDirectories)
                        : allowedExtensions.SelectMany(
                            ext => Directory.GetFiles(Folder, $"*.{ext}", SearchOption.AllDirectories)
                        ).ToArray();

            Run(files.RandomElement());
        }

        private string[] GetAllowedExtensions()
            => Filters.IsNullOrEmpty()
                ? null
                : new string(
                    Filters.Where(@char => !char.IsWhiteSpace(@char)).ToArray()
                ).Split(',');
    }
}
