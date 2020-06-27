using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftLauncher.Exceptions
{
    public class JsonReadException : Exception
    {
        public string MessageCaption => "Json read error";
    }
}
