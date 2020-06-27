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
        public string LoggerPath { get; set; }
        public bool IsLoggingActive { get; set; }
        public string AppName { get; set; }
        public string ExecutePath { get; set; }
        public bool IsSelected { get; set; }
        public AppEntityJson() { }
        public AppEntityJson(AppEntity app)
        {
            AppName = app.AppName;
            ExecutePath = app.ExecutePath;
            IsSelected = app.IsSelected;
            IsLoggingActive = app.IsLoggingActive;
            if (IsLoggingActive)
            {
                LoggerPath = app.Logger.Path;
            }
        }
        public static List<AppEntity> Convert(List<AppEntityJson> jsonApps)
        {
            var apps = new List<AppEntity>();
            jsonApps.ForEach(a => apps.Add(new AppEntity(a)));
            return apps;
        }
        public static List<AppEntityJson> Convert(List<AppEntity> apps)
        {
            var jsonApps = new List<AppEntityJson>();
            apps.ForEach(a => jsonApps.Add(new AppEntityJson(a)));
            return jsonApps;
        }
    }
}
