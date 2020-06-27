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
        public Player() { }
        public static void PlaySound(PlayerSound playerSound)
        {
            try
            {
                var player = new SoundPlayer(playerSound.Value);
                player.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Playing sound error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }

    public class PlayerSound
    {
        private PlayerSound(string value) { Value = value; }
        public string Value { get; set; }
        public static PlayerSound Start { get { return new PlayerSound(@"sounds\Start.wav"); } }
        public static PlayerSound Exit { get { return new PlayerSound(@"sounds\Exit.wav"); } }
        public static PlayerSound Click { get { return new PlayerSound(@"sounds\Switch App.wav"); } }
        public static PlayerSound PositiveAction { get { return new PlayerSound(@"sounds\Speech On.wav"); } }
        public static PlayerSound NegativeAction { get { return new PlayerSound(@"sounds\Speech Off.wav"); } }
    }
}
