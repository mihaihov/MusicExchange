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

namespace MusicExchance
{
    public class SongDetails
    {
        public static string GetSongArtist(string path)
        {
            string artist = string.Empty;
            MediaMetadataRetriever mmr = new MediaMetadataRetriever();
            mmr.SetDataSource(path);
            artist = mmr.ExtractMetadata(MetadataKey.Artist);
            mmr.Release();
            return artist;
        }

        public static string GetSongAlbum(string path)
        {
            string album = string.Empty;
            MediaMetadataRetriever mmr = new MediaMetadataRetriever();
            mmr.SetDataSource(path);
            album = mmr.ExtractMetadata(MetadataKey.Album);
            mmr.Release();
            return album;
        }

        public static string GetSongTitle(string path)
        {
            string title = string.Empty;
            MediaMetadataRetriever mmr = new MediaMetadataRetriever();
            mmr.SetDataSource(path);
            title = mmr.ExtractMetadata(MetadataKey.Title);
            mmr.Release();

            return title;
        }

        public static byte[] GetSongArtwork(string path)
        {
            byte[] artwork;
            MediaMetadataRetriever mmr = new MediaMetadataRetriever();
            mmr.SetDataSource(path);
            artwork = mmr.GetEmbeddedPicture();
            mmr.Release();

            return artwork;
        }

        public static string GetSongDuration(string path)
        {
            string duration;
            MediaMetadataRetriever mmr = new MediaMetadataRetriever();
            mmr.SetDataSource(path);
            duration = mmr.ExtractMetadata(MetadataKey.Duration);
            mmr.Release();

            return duration;
        }
    }
}