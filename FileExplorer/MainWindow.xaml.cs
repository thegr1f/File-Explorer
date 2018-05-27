using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static FileExplorer.Properties.Resources;
using static System.Environment;

namespace FileExplorer{

    public partial class MainWindow : Window{

        public enum ListItemType { Drive, Folder, File }

        private string currentPath;
        private ListItem itemToCopy;

        public ObservableCollection<NavItem> navItemsList { get; set; }
        public ObservableCollection<ListItem> itemList { get; set; }

        private DialogHost dialogEdit;
        private DialogHost dialogDelete;
        private DialogHost dialogProperties;

        public MainWindow(){

            InitializeComponent();

            setupNavigation();
            setupList();

            getByPath(null);

            dialogEdit = dialog_edit as DialogHost;
            dialogDelete = dialog_delete as DialogHost;
            dialogProperties = dialog_properties as DialogHost;
        }

        private void dragWindow(object sender, MouseButtonEventArgs e){
            this.DragMove();
        }

        private void hideWindow(object sender, RoutedEventArgs e){
            this.WindowState = WindowState.Minimized;
        }

        private void closeWindow(object sender, RoutedEventArgs e){
            Application.Current.Shutdown();
        }

        private void setupNavigation(){
            string userRoot = GetEnvironmentVariable("USERPROFILE");

            navItemsList = new ObservableCollection<NavItem> {
                new NavItem {name=nav_computer, icon=PackIconKind.Monitor, path=null},
                new NavItem {name=nav_desktop, icon=PackIconKind.Application, path=GetFolderPath(SpecialFolder.Desktop)},
                new NavItem {name=nav_docs, icon=PackIconKind.FileDocument, path=GetFolderPath(SpecialFolder.MyDocuments)},
                new NavItem {name=nav_downloads, icon=PackIconKind.Download, path=Path.Combine(userRoot, "Downloads")},
                new NavItem {name=nav_music, icon=PackIconKind.MusicNote, path=GetFolderPath(SpecialFolder.MyMusic)},
                new NavItem {name=nav_pictures, icon=PackIconKind.Image, path=GetFolderPath(SpecialFolder.MyPictures)},
                new NavItem {name=nav_videos, icon=PackIconKind.Movie, path=GetFolderPath(SpecialFolder.MyVideos)}
            };

            list_navigation.ItemsSource = navItemsList;
        }

        private void setupList(){
            itemList = new ObservableCollection<ListItem> {};
            list_items.ItemsSource = itemList;
        }

        private void getByPath(string path){
            itemList.Clear();

            currentPath = path;

            if(path == null){
                button_back.Visibility = Visibility.Collapsed;
                button_new_folder.Visibility = Visibility.Collapsed;
                getDrives();
                return;
            }

            button_back.Visibility = Visibility.Visible;
            button_new_folder.Visibility = Visibility.Visible;

            string dirName = new FileInfo(path).Name;

            if (dirName.Length != 0)
                text_current_dir.Text = dirName;
            else {
                DriveInfo drive = new DriveInfo(path);
                text_current_dir.Text = drive.VolumeLabel.Length == 0 ? "Local Disk" : drive.VolumeLabel;
                text_current_dir.Text += " (" + drive.Name.Substring(0, drive.Name.Length - 1) + ")";
            }

            FileInfo fileInfo;
            ListItem item;

            foreach (string dir in Directory.GetFileSystemEntries(path)){
                fileInfo = new FileInfo(dir);

                if (!fileInfo.Attributes.HasFlag(FileAttributes.Hidden)){
                    item = new ListItem();

                    item.name = fileInfo.Name;
                    item.path = dir;
                    item.modifiedTime = String.Format("{0:dd.MM.yyyy}", fileInfo.LastWriteTime);

                    if (fileInfo.Attributes.HasFlag(FileAttributes.Directory)){
                        item.icon = PackIconKind.Folder;
                        item.type = ListItemType.Folder;
                    } else {
                        item.icon = PackIconKind.File;
                        item.type = ListItemType.File;
                    }

                    itemList.Add(item);
                }
            }
        }

        private void getDrives(){
            text_current_dir.Text = nav_computer;
            list_navigation.SelectedIndex = 0;

            ListItem drive;

            foreach (DriveInfo driveInfo in System.IO.DriveInfo.GetDrives()){
                drive = new ListItem { icon = PackIconKind.Server, type = ListItemType.Drive };

                if (driveInfo.IsReady) {
                    if (driveInfo.VolumeLabel.Length == 0)
                        drive.name = "Local Disk";
                    else
                        drive.name = driveInfo.VolumeLabel;

                    drive.name += " (" + driveInfo.Name.Substring(0, driveInfo.Name.Length - 1) + ")";
                    drive.path = driveInfo.Name;

                    itemList.Add(drive);
                }
            }
        }

        private void createFolder(string name){
            string folderPath = Path.Combine(currentPath, name);

            if (Directory.Exists(folderPath))
                return;

            Directory.CreateDirectory(folderPath);

            getByPath(currentPath);
        }

        private void copy(ListItem item){
            bool isFile = item.type == ListItemType.File;

            string sourceName = item.path;
            string destName = Path.Combine(currentPath, item.name);

            if (isFile)
                File.Copy(sourceName, destName, true);
            else
                copyDirectory(sourceName, destName);
            
            button_copy.Visibility = Visibility.Collapsed;

            getByPath(currentPath);
        }

        private void copyDirectory(string sourceDirName, string destDirName){
            if (!Directory.Exists(destDirName))
                Directory.CreateDirectory(destDirName);

            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            FileInfo[] files = dir.GetFiles();
            DirectoryInfo[] dirs = dir.GetDirectories();

            foreach (FileInfo file in files){
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            foreach (DirectoryInfo subdir in dirs){
                string temppath = Path.Combine(destDirName, subdir.Name);
                copyDirectory(subdir.FullName, temppath);
            }
            
        }

        private void rename(string name){
            ListItem item = (list_items.SelectedItem as ListItem);
            bool isFile = item.type == ListItemType.File;

            string oldPath = item.path;
            string newPath = Path.Combine(currentPath, name);

            if (File.Exists(newPath) || Directory.Exists(newPath))
                return;

            if (isFile)
                File.Move(oldPath, newPath);
            else
                Directory.Move(oldPath, newPath);

            getByPath(currentPath);
        }

        private void delete(ListItem item){
            if (item.type == ListItemType.File)
                File.Delete(item.path);
            else
                Directory.Delete(item.path, true);

            getByPath(currentPath);
        }

        private void showDeleteDialog(ListItem item){

            bool isFile = item.type == ListItemType.File;

            text_delete_file_title.Text = isFile ? title_delete_file : title_delete_folder;
            text_delete_file_descr.Text = isFile ? descr_delete_file : descr_delete_folder;

            string infoForDeleting = item.name + "\n";
            infoForDeleting += String.Format("{0, -17} {1}\n", "Size:", getDisplayingFileSize(item.path, isFile));
            infoForDeleting += String.Format("{0, -13} {1}", "Created:", new FileInfo(item.path).CreationTime);

            text_delete_file_info.Text = infoForDeleting;

            dialogDelete.IsOpen = true;
        }

        private void showNewFolderDialog(ListItem item){
            text_edit_title.Text = title_edit_new_folder;

            icon_edit.Kind = PackIconKind.FolderPlus;

            HintAssist.SetHint(input_name, hint_new_folder);

            button_edit_accept.Content = button_create;

            dialogEdit.IsOpen = true;
        }

        private void showRenameDialog(ListItem item){
            text_edit_title.Text = title_edit_rename;

            input_name.Text = item.name;

            icon_edit.Kind = PackIconKind.Pencil;

            HintAssist.SetHint(input_name, hint_new_name);

            button_edit_accept.Content = button_save;

            dialogEdit.IsOpen = true;
        }

        private void showPropertiesDialog(ListItem item){
            bool isFile = item.type == ListItemType.File;

            //up info

            string info = String.Format("{0,-15} {1}\n\n", "Name:", item.name);

            if (isFile)
                info += String.Format("{0,-17} {1}", "Type:", "File (" + Path.GetExtension(item.path) + ")");
            else
                info += String.Format("{0,-17} {1}", "Type:", "File folder");

            text_prop_top_info.Text = info;

            //mid info

            info = String.Format("{0,-14} {1}\n\n", "Location:", item.path);
            info += String.Format("{0,-18} {1}", "Size:", getDisplayingFileSize(item.path, isFile));

            if (!isFile){

                int dirCount = Directory.GetDirectories(item.path).Length;
                int fileCount = Directory.GetFiles(item.path).Length;

                info += String.Format("\n\n{0, -13} {1} Files, {2} Folders", "Contains:", fileCount, dirCount);
            }

            text_prop_mid_info.Text = info;

            //low info

            FileInfo fileInfo = new FileInfo(item.path);

            info = String.Format("{0,-14} {1}\n\n", "Created:", fileInfo.CreationTime);
            info += String.Format("{0,-14} {1}\n\n", "Modified:", fileInfo.LastWriteTime);
            info += String.Format("{0,-12} {1}", "Accessed:", fileInfo.LastAccessTime);

            text_prop_low_info.Text = info;

            dialogProperties.IsOpen = true;
        }

        private string getDisplayingFileSize(string path, bool isFile){

            float size = 0;
            string sizeName;

            if (isFile)
                size = new FileInfo(path).Length;
            else
                size = getFolderSize(new DirectoryInfo(path));

            if (size < 1024f)
                sizeName = " bytes";
            else if (size < 1024f * 1024f){
                size /= 1024f;
                sizeName = " KB";
            }else{
                size /= 1024f * 1024f;
                sizeName = " MB";
            }

            return String.Format("{0:0.00}", size).Replace(",", ".") + sizeName;
        }

        private long getFolderSize(DirectoryInfo dirInfo){
            long size = 0;

            foreach (FileInfo f in dirInfo.GetFiles())
                size += f.Length;

            foreach (DirectoryInfo d in dirInfo.GetDirectories())
                size += getFolderSize(d);

            return size;
        }

        private void onBackClick(object sender, RoutedEventArgs e){
            DirectoryInfo parentDir = Directory.GetParent(currentPath);

            if (parentDir == null)
                getByPath(null);
            else
                getByPath(parentDir.FullName);
        }

        private void onEditAcceptClick(object sender, RoutedEventArgs e){
            string input = input_name.Text;

            if (input.Length == 0)
                return;

            if (button_edit_accept.Content.Equals(button_save))
                rename(input);
            else
                createFolder(input);

            dialogEdit.IsOpen = false;
            input_name.Clear();
        }

        private void onCopyClick(object sender, RoutedEventArgs e){
            copy(itemToCopy);
        }

        private void onNavigationItemClick(object sender, MouseButtonEventArgs e){
            NavItem item = (sender as ListView).SelectedItem as NavItem;

            if (item == null)
                return;

            getByPath(item.path);
        }

        private void onListItemClick(object sender, MouseButtonEventArgs e){
            ListItem item = (sender as ListView).SelectedItem as ListItem;

            if (item == null)
                return;

            FileInfo info = new FileInfo(item.path);

            if (info.Attributes.HasFlag(FileAttributes.Directory))
                getByPath(item.path);
            else
                System.Diagnostics.Process.Start(info.FullName);
        }

        private void onNewFolderClick(object sender, RoutedEventArgs e){
            showNewFolderDialog(list_items.SelectedItem as ListItem);
        }

        private void onContextCopyClick(object sender, RoutedEventArgs e){
            itemToCopy = list_items.SelectedItem as ListItem;
            button_copy.Visibility = Visibility.Visible;
        }

        private void onContextRenameClick(object sender, RoutedEventArgs e){
            ListItem item = list_items.SelectedItem as ListItem;

            if (item.type == ListItemType.Drive)
                return;

            showRenameDialog(item);
        }

        private void onContextDeleteClick(object sender, RoutedEventArgs e){
            ListItem item = list_items.SelectedItem as ListItem;

            if (item.type == ListItemType.Drive)
                return;

            showDeleteDialog(item);
        }

        private void onContextPropertiesClick(object sender, RoutedEventArgs e){
            ListItem item = list_items.SelectedItem as ListItem;

            if (item.type == ListItemType.Drive)
                return;

            showPropertiesDialog(item);
        }

        private void onDeleteClick(object sender, RoutedEventArgs e){
            delete(list_items.SelectedItem as ListItem);

            dialogDelete.IsOpen = false;
        }
    }
}
