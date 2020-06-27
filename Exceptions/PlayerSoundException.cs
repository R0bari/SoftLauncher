using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftLauncher.Exceptions
{
    public class PlayerSoundException : Exception
    {
        public string MessageCaption => "Playing sound error";
    }
}
