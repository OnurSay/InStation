using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace InstationFinalVersion
{
	public class AudioPlayerViewModel : INotifyPropertyChanged
	{
		private IAudioPlayerService _audioPlayer;
		private bool _isStopped;
		public event PropertyChangedEventHandler PropertyChanged;
		public string selected_song = MainPage.get_selected_song();
		public AudioPlayerViewModel(IAudioPlayerService audioPlayer)
		{

			_audioPlayer = audioPlayer;
			_audioPlayer.OnFinishedPlaying = () =>
			{
				_isStopped = true;
				CommandText = "Play";
			};
			CommandText = "Play";
			_isStopped = true;
		}

		private string _commandText;
		public string CommandText
		{
			get { return _commandText; }
			set
			{
				_commandText = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CommandText"));
			}
		}

		private ICommand _playPauseCommand;
		public ICommand PlayPauseCommand
		{

			get
			{
				return _playPauseCommand ?? (_playPauseCommand = new Command(
					(obj) =>
					{
						if (CommandText == "Play")
						{
							if (_isStopped)
							{
								_isStopped = false;
								_audioPlayer.Play(MainPage.get_selected_song());
							}
							else
							{
								//_audioPlayer.Play();
							}
							CommandText = "Pause";
						}
						else
						{
							//_audioPlayer.Pause();
							CommandText = "Play";
						}
					}));
			}
		
		}
		public ICommand StartPlay
		{

			get
			{
				return _playPauseCommand ?? (_playPauseCommand = new Command(
					(obj) =>
					{
						_audioPlayer.Play(MainPage.get_selected_song());
					}));
			}
		}
	}
}
