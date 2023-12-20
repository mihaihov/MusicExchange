using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Media;
using System.IO;
using Android.Graphics;
using Android.Util;

namespace MusicExchance
{
    public class AppManager
    {
        private static AppManager _instance;
        public static AppManager Instance
        {
            get
            {
                if (_instance == null) _instance = new AppManager();
                return _instance;
            }
        }

        private MediaPlayer _mediaPlayer;
		private List<string> _songs;

        public List<Artist> artistList;
        public List<Album> albumList;
        public List<Playlist> playlistList;
		public List<Song> songsList;
        public PlayerState state;
        

        public AppManager()
        {
            artistList = new List<Artist>();
            albumList = new List<Album>();
            playlistList = new List<Playlist>();
            _songs = new List<string>();
        }

        public void GetDeviceSongs(string dir = "/storage/emulated/0/Download")
        {
            state = PlayerState.ReleaseState;
            var directory = new DirectoryInfo(dir);
            foreach (var item in directory.GetFileSystemInfos())
                if (string.Equals(item.Extension, ".mp3"))
                    _songs.Add(item.FullName);

            CreateSongsFromStrings();

        }

        public void CreateSongsFromStrings()
        {
            if (songsList == null)
                songsList = new List<Song>();
            foreach(string s in _songs)
            {
                Song so = new Song(s);
                songsList.Add(so);
            }
        }



        public List<string> GetSongsList()
        {
            if (_songs == null) _songs = new List<string>();
            return _songs;
        }

        
        public void Play(string path)
        {
            if(state == PlayerState.PlayState)
            {
                StopPlayer();
                ReleasePlayer();
            }

            if (_mediaPlayer == null) _mediaPlayer = new MediaPlayer();
            _mediaPlayer.Reset();
            _mediaPlayer.SetDataSource(path);
            _mediaPlayer.Prepare();
            _mediaPlayer.Start();
        }

        public void Pause()
        {
            if (state == PlayerState.PlayState)
            {
                _mediaPlayer.Pause();
                state = PlayerState.PauseState;
            }
        }

        public void StopPlayer()
        {
            if(state == PlayerState.PlayState || state == PlayerState.PauseState)
            {
                _mediaPlayer.Stop();
                state = PlayerState.StopState;
            }

        }

        public void Resume()
        {
            if (state == PlayerState.PauseState)
            {
                _mediaPlayer.Start();
                state = PlayerState.PlayState;
            }
        }

        private void ReleasePlayer()
        {
            _mediaPlayer.Release();
            state = PlayerState.ReleaseState;
        }
    }

    public enum PlayerState
    {
        PlayState = 0,
        PauseState,
        StopState,
        ReleaseState
    }
}