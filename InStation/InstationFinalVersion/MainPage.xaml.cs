using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace InstationFinalVersion
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        //Yağmur Trial Area

        //Original selected_song
        public static string selected_song;
        List<string> songs = new List<string>();
        List<string> songs_link = new List<string>();

        public MainPage()
        {

            InitializeComponent();
            //var youtube = new YoutubeSearch();
            //youtube.Run_cmd();

            BindingContext = new AudioPlayerViewModel(DependencyService.Get<IAudioPlayerService>());

        }

        void Handle_SearchButtonPressed(System.Object sender, System.EventArgs e)
        {

            var keyword = MusicSearchBar.Text;


            if (keyword.Length >= 1)
            {
                string jsonResult = Youtube_Search();
                //jsonResult = jsonResult.TrimStart(new char[] { '[' }).TrimEnd(new char[] { ']' });
                var songs_list = JsonConvert.DeserializeObject<List<Song>>(jsonResult);


                for (int i = 0; i < songs_list.Count; i++)
                {
                    songs.Add(songs_list[i].title);
                    songs_link.Add(songs_list[i].link);
                }



                var suggestions = songs.Where(s => s.ToLower().Contains(keyword));
                //var song = from s in songs where s.Contains(keyword) select s;
                SuggestionsListView.ItemsSource = suggestions;
                SuggestionsListView.IsVisible = true;
            }
            else
            {
                SuggestionsListView.IsVisible = false;
            }
        }

        void Handle_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
        }

        void Handle_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {

            string song_name = e.Item as string;
            var replaced = song_name.Replace("(", "");
            replaced = replaced.Replace(")", "");
            replaced = replaced.Replace(' ', '_');
            replaced = replaced.Replace(",", "");

            if (!CheckSongExist(replaced))
            {
                for (int i = 0; i < songs.Count; i++)
                {
                    if (songs[i] == e.Item as string)
                    {
                        string a = Convert_mp3(songs_link[i]);

                        JObject jObj = JObject.Parse(a);                 // Parse the object graph
                        selected_song = jObj["url"].ToString();       // Retrive value by key

                    }
                }

                
            }
            else
            {
                selected_song = "http://instation.codes//youtube-mp3//" + replaced + ".mp3";

            }
            BindingContext = new AudioPlayerViewModel(DependencyService.Get<IAudioPlayerService>());
        }
        string Youtube_Search()
        {
            var input = MusicSearchBar.Text;

            string html = string.Empty;
            string url = @"http://instation.codes/youtube-search/?s=" + input;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }
            return html;

        }
        string Convert_mp3(string selected_song_link)
        {
            string html = string.Empty;
            string url = @"http://instation.codes/youtube-mp3/?url=https://www.youtube.com" + selected_song_link;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }
            return html;
        }
        bool CheckSongExist(string song)
        {
            bool found = false;
            string html = string.Empty;
            string url = @"http://instation.codes/youtube-mp3/exist/";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }
            if (html.Contains(song))
            {

                found = true;
            }
            return found;
        }
        public static string get_selected_song()
        {
            return selected_song;
        }
    }
}
