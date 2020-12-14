using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using WMPLib;
using AxWMPLib;
using System.IO;


namespace UserInterface
{
    public partial class musicBoxForm : Form
    {
        OpenFileDialog uploadFileSteam = new OpenFileDialog();
        private List<string> musicfile = new List<string>();
        private List<string> musicname = new List<string>();
        string[] fileNames, filePaths;      
        IWMPPlaylist playlist;

        public musicBoxForm()
        {
            InitializeComponent();

            playlist = axWindowsMediaPlayer1.playlistCollection.newPlaylist("music");
        }

        string getFileName(string path)
        {
            int length = path.Length, index = 0;
            string name = "";

            for (int i = length - 1; i >= 0; i--)
            {
                if (path[i] == 92)
                {
                    index = i + 1;
                    break;
                }
            }

            for (; index < length; index++)
            {
                name += path[index];
            }

            return name;
        }

        private void mucsicBoxForm_Load(object sender, EventArgs e)
        {
            uploadFileSteam.InitialDirectory = "Music";
            uploadFileSteam.Filter = "mp3|*.mp3";
            uploadFileSteam.FilterIndex = 1;

            string[] temp = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\music", "*.mp3", SearchOption.AllDirectories);

            for (int i = 0; i < temp.Length; i++)
            {
                IWMPMedia media = axWindowsMediaPlayer1.newMedia(temp[i]);
                playlist.appendItem(media);
                musicfile.Add(temp[i]);
                string n = getFileName(temp[i]);
                musicname.Add(n);
                playlistListbox.Items.Add(n);
            }
        }

        

        private bool checkExisted(string path, List<string> File)
        {
            foreach (string file in File)
            {
                if (string.Compare(path, file, false) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            uploadFileSteam.Multiselect = true;

            if (uploadFileSteam.ShowDialog() == DialogResult.OK)
            {
                fileNames = uploadFileSteam.SafeFileNames;
                filePaths = uploadFileSteam.FileNames;

                string temp = "";

                if (playlist == null) 
                {
                    for (int i = 0; i < filePaths.Length; i++)
                    {
                        IWMPMedia media = axWindowsMediaPlayer1.newMedia(filePaths[i]);
                        playlist.appendItem(media);
                        musicfile.Add(filePaths[i]);
                        temp = filePaths[i];
                        File.Copy(temp, Directory.GetCurrentDirectory() + "\\music\\" + fileNames[i]);
                        musicname.Add(fileNames[i]);
                        playlistListbox.Items.Add(fileNames[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < filePaths.Length; i++)
                    {
                        if (checkExisted(filePaths[i], musicfile) == false && checkExisted(fileNames[i], musicname) == false)
                        {
                            IWMPMedia media = axWindowsMediaPlayer1.newMedia(filePaths[i]);
                            playlist.appendItem(media);
                            musicfile.Add(filePaths[i]);
                            temp = filePaths[i];
                            File.Copy(temp, Directory.GetCurrentDirectory() + "\\music\\" + fileNames[i]);
                            musicname.Add(fileNames[i]);
                            playlistListbox.Items.Add(fileNames[i]);
                        }
                    }

                }
            }
        }

        bool checkStatusPlayMusic = false;
        bool checkPlaylistPlaying = false;
        private void playButton_Click(object sender, EventArgs e)
        {
            if (musicfile.Count == 0)
            {
                return;
            }

            if (checkStatusPlayMusic == true)
            {
                playAndpauseButton.BackgroundImage = Image.FromFile(Application.StartupPath + "\\images\\play.png");
            }
            else
            {
                playAndpauseButton.BackgroundImage = Image.FromFile(Application.StartupPath + "\\images\\pause.png");
            }

            if (checkPlaylistPlaying == false)
            {
                axWindowsMediaPlayer1.currentPlaylist = playlist;
                checkPlaylistPlaying = true;
                checkStatusPlayMusic = true;
                musicNameLabel.Text = "Music: " + axWindowsMediaPlayer1.currentMedia.name;
                return;
            }

            if (axWindowsMediaPlayer1 != null)
            {
                if (checkStatusPlayMusic == true)
                {
                    axWindowsMediaPlayer1.Ctlcontrols.pause();
                    checkStatusPlayMusic = false;
                }
                else
                {
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                    checkStatusPlayMusic = true;
                }
            }
            
        }


        private void nextButton_Click(object sender, EventArgs e)
        {
            if (musicfile.Count == 0)
            {
                return;
            }

            if (axWindowsMediaPlayer1.currentMedia == null)
            {
                return;
            }

            if (playlistListbox.SelectedIndex == playlistListbox.Items.Count - 1) 
            {
                IWMPMedia media = axWindowsMediaPlayer1.currentPlaylist.Item[0];
                axWindowsMediaPlayer1.Ctlcontrols.playItem(media);
                return;
            }

            axWindowsMediaPlayer1.Ctlcontrols.next();

            if (checkStatusPlayMusic == false)
            {
                checkMusicPaused = true;
            }

            musicNameLabel.Text = "Music: " + axWindowsMediaPlayer1.currentMedia.name;
        }

        private void prevButton_Click(object sender, EventArgs e)
        {
            if (musicfile.Count == 0)
            {
                return;
            }

            if (axWindowsMediaPlayer1.currentMedia == null)
            {
                return;
            }

            if (playlistListbox.SelectedIndex == 0)
            {
                IWMPMedia media = axWindowsMediaPlayer1.currentPlaylist.Item[playlistListbox.Items.Count - 1];
                axWindowsMediaPlayer1.Ctlcontrols.playItem(media);
                return;
            }

            axWindowsMediaPlayer1.Ctlcontrols.previous();

            if (checkStatusPlayMusic == false)
            {
                checkMusicPaused = true;
            }

            musicNameLabel.Text = "Music: " + axWindowsMediaPlayer1.currentMedia.name;
        }

        bool checkMusicPaused = false;
        private void axWindowsMediaPlayer1_StatusChange(object sender, EventArgs e)
        {
            if (axWindowsMediaPlayer1.status == "Paused" && checkMusicPaused == true)
            {
                axWindowsMediaPlayer1.Ctlcontrols.play();
                playAndpauseButton.BackgroundImage = Image.FromFile(Application.StartupPath + "\\images\\pause.png");
                checkMusicPaused = false;
                checkStatusPlayMusic = true;
            }

            if (axWindowsMediaPlayer1.status == "Ready")
            {
                IWMPMedia media = axWindowsMediaPlayer1.currentPlaylist.Item[0];
                axWindowsMediaPlayer1.Ctlcontrols.playItem(media);
            }

            if (axWindowsMediaPlayer1.status == "Stopped")
            {
                IWMPMedia media = axWindowsMediaPlayer1.currentPlaylist.Item[0];
                axWindowsMediaPlayer1.Ctlcontrols.playItem(media);
            }
        }

        private void axWindowsMediaPlayer1_CurrentItemChange(object sender, _WMPOCXEvents_CurrentItemChangeEvent e)
        {
            musicNameLabel.Text = "Music: " + axWindowsMediaPlayer1.currentMedia.name;
        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (axWindowsMediaPlayer1 == null)
            {
                Close();
            }
            else
            {
                axWindowsMediaPlayer1.Ctlcontrols.stop();
                Close();
            }
        }

    }
}