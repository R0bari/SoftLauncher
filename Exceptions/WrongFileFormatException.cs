using System;

namespace SoftLauncher
{
    class WrongFileFormatException : Exception
    {
        public override string Message => "Wrong format. Required format - .exe!";
        public string MessageCaption => "Wrong format";
    }
}
