using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Prism.Commands;
using TagLib;

namespace Controls
{
    public class MediaUxService
    {
        public ObservableCollection<MediaFile> MediaPlayListCollection { get; set; } =
            new ObservableCollection<MediaFile>();
        public MediaFile CurrentMedia { get; set; }
        public double PlayerPosition { get; set; }
        public double MediaFileDuration { get; set; }
        public MediaPlayer Media { get; set; } = new MediaPlayer();
        public bool Playing { get; set; }

        public MediaUxService()
        {
            LoadFile = new DelegateCommand<object>(OnLoad);
            LoadCollectionFile = new DelegateCommand<object>(OnLoadCollection);

            PlayPauseCommand = new DelegateCommand<object>(OnPlayPause, OnCanPlay);

            MediaPlayListCollection.CollectionChanged += MediaPlayListCollectionOnCollectionChanged;
            Media.MediaEnded += MediaOnMediaEnded;
        }

        public DelegateCommand<object> LoadFile { get; }
        public DelegateCommand<object> LoadCollectionFile { get; }
        public DelegateCommand<object> PlayPauseCommand { get; }

        #region Commands Methods
        private void OnPlayPause(object obj)
        {
            Media.Pause();
            Playing = !Playing;
        }

        private bool OnCanPlay(object arg) => MediaPlayListCollection.Count != 0;
        private void OnLoad(object obj)
        {
            if (obj is string name)
            {
                MediaPlayListCollection.Clear();
                MediaPlayListCollection.Add(new MediaFile(name));
                Media.Open(new Uri(CurrentMedia.FullFileName));
            }
        }
        private void OnLoadCollection(object obj)
        {
            if (obj is string[] filenames)
            {
                MediaPlayListCollection.Clear();
                foreach (string name in filenames)
                    MediaPlayListCollection.Add(new MediaFile(name));
            }

            CurrentMedia = MediaPlayListCollection.First();
            Media.Open(new Uri(CurrentMedia.FullFileName));
        }
        #endregion

        #region Private Methods
        private void MediaPlayListCollectionOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            
        }
        private void MediaOnMediaEnded(object? sender, EventArgs e)
        {
            if (MediaPlayListCollection.IndexOf(CurrentMedia)
                != MediaPlayListCollection.Count + 1)
            {
                CurrentMedia = MediaPlayListCollection.ElementAt(MediaPlayListCollection.IndexOf(CurrentMedia)+1);
                Media.Open(new Uri(CurrentMedia.FullFileName));
            }
        }

        #endregion
    }
}
