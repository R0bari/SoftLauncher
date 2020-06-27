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
        private static readonly Logger logger = new Logger("log.txt");
        public Player() { }
        public static void PlaySound(Sound sound)
        {
            try
            {
                var player = new SoundPlayer(sound.Value);
                player.Play();
            }
            catch (PlayerSoundException ex)
            {
                MessageBox.Show(ex.Message, ex.MessageCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                logger.Log(LogType.Error, ex.Message);
            }
        }
    }

    public class Sound
    {
        private Sound(string value) { Value = value; }
        public string Value { get; set; }
        public static Sound Start { get { return new Sound(@"sounds\Start.wav"); } }
        public static Sound Exit { get { return new Sound(@"sounds\Exit.wav"); } }
        public static Sound Click { get { return new Sound(@"sounds\Switch App.wav"); } }
        public static Sound PositiveAction { get { return new Sound(@"sounds\Speech On.wav"); } }
        public static Sound NegativeAction { get { return new Sound(@"sounds\Speech Off.wav"); } }
    }
}
