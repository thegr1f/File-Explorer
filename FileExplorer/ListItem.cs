using MaterialDesignThemes.Wpf;
using static FileExplorer.MainWindow;

namespace FileExplorer{
    public class ListItem{

        public string name { get; set; }
        public string path { get; set; }
        public string modifiedTime { get; set; }
        public ListItemType type { get; set; }
        public PackIconKind icon { get; set; }

    }
}
