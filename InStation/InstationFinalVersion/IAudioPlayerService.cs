
using System;

namespace InstationFinalVersion
{
    public interface IAudioPlayerService
    {
        void Play(string pathToAudioFile);
        void Play();
        void Pause();
       
        Action OnFinishedPlaying { get; set; }
    }
}
