using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftLauncher
{
    [Serializable]
    public class AppEntityJson
    {
        public string AppName { get; set; }
        public string ExecutePath { get; set; }
        public bool IsSelected { get; set; }
        public AppEntityJson() { }
        public AppEntityJson(AppEntity app)
        {
            AppName = app.AppName;
            ExecutePath = app.ExecutePath;
            IsSelected = app.IsSelected;
        }
    }
}
