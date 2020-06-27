using SoftLauncher.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoftLauncher
{
    public class Player
    {
        public Logger Logger { get; set; }
        public bool IsLoggingActive { get; private set; } = false;
        public Player() { }
        public void PlaySound(Sound sound)
        {
            try
            {
                var player = new SoundPlayer(sound.Name);
                player.Play();
            }
            catch (PlayerSoundException ex)
            {
                MessageBox.Show(ex.Message, ex.MessageCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (IsLoggingActive)
                {
                    Logger.Log(LogType.Error, ex.Message);
                }
            }
        }
        public void AddLogging(Logger logger)
        {
            Logger = logger;
            IsLoggingActive = true;
        }
    }

    public class Sounds
    {
        public Sounds() { }

        public string Value { get; set; }
        public Sound Start { get { return new Sound(@"sounds\Start.wav"); } }
        public Sound Exit { get { return new Sound(@"sounds\Exit.wav"); } }
        public Sound Click { get { return new Sound(@"sounds\Switch App.wav"); } }
        public Sound PositiveAction { get { return new Sound(@"sounds\Speech On.wav"); } }
        public Sound NegativeAction { get { return new Sound(@"sounds\Speech Off.wav"); } }
    }

    public class Sound
    {
        public string Name { get; private set; }
        public Sound(string name) => Name = name;


    }
}
