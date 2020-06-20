using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftLauncher
{
    class WrongFileFormatException : Exception
    {
        public override string Message => "Wrong format. Required format - .exe!";
        public string MessageCaption => "Wrong format";
    }
}
