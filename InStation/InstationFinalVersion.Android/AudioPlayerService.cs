using System;
using Android.Media;
using Xamarin.Forms;
using InstationFinalVersion.Droid;
using Java.Lang;
using static Android.Media.MediaPlayer;
using Android.Net;
using Android.Content;
using Com.Google.Android.Exoplayer2.Util;
using Com.Google.Android.Exoplayer2.Upstream;
using Com.Google.Android.Exoplayer2;
using Com.Google.Android.Exoplayer2.Source;
using Com.Google.Android.Exoplayer2.Extractor;
using Com.Google.Android.Exoplayer2.Trackselection;

[assembly: Dependency(typeof(AudioPlayerService))]
namespace InstationFinalVersion.Droid
{


    public class AudioPlayerService : IAudioPlayerService
    {
        #region Orginal
        int clicks = 0;
        MediaPlayer _mediaPlayer;
        SimpleExoPlayer exoPlayer;

        public Action OnFinishedPlaying { get; set; }

        public AudioPlayerService()
        {
        }

        [Obsolete]
        public void Play(string url)
        {
            if (clicks % 2 == 0)
            {
                #region ExoPlayer
                
                var mediaUrl = url;
                var mediaUri = Android.Net.Uri.Parse(mediaUrl);
                Android.Content.Context context = Android.App.Application.Context;
                var userAgent = Util.GetUserAgent(context, "InstationPlayer");
                var defaultHttpDataSourceFactory = new DefaultHttpDataSourceFactory(userAgent);
                var defaultDataSourceFactory = new DefaultDataSourceFactory(context, null, defaultHttpDataSourceFactory);
                var extractorMediaSource = new ExtractorMediaSource(mediaUri, defaultDataSourceFactory, new DefaultExtractorsFactory(), null, null);
                var defaultBandwidthMeter = new DefaultBandwidthMeter();
                var adaptiveTrackSelectionFactory = new AdaptiveTrackSelection.Factory(defaultBandwidthMeter);
                var defaultTrackSelector = new DefaultTrackSelector(adaptiveTrackSelectionFactory);

                exoPlayer = ExoPlayerFactory.NewSimpleInstance(context, defaultTrackSelector);
                exoPlayer.Prepare(extractorMediaSource);
                Console.WriteLine("Prepared");
                exoPlayer.PlayWhenReady = true;
                Console.WriteLine("Started");
                clicks++; 
                
                #endregion
          
            }
            else if (clicks % 2 != 0)
            {
             
                exoPlayer.Stop();
                Console.WriteLine("Paused");
                clicks++;
            }

        }

        private void _mediaPlayer_Prepared(object sender, EventArgs e)
        {
            _mediaPlayer.Start();
        }

        void MediaPlayer_Completion(object sender, EventArgs e)
        {
            OnFinishedPlaying?.Invoke();
        }

        public void Pause()
        {
            _mediaPlayer?.Pause();
        }

        public void onPrepared(MediaPlayer mp)
        {
            mp.Start();
        }
        public void Play()
        {
            _mediaPlayer?.Start();
        }
        #endregion
    }
}

