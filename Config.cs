
namespace SoftLauncher
{
    public class Config
    {
        public int Margin { get; }
        public int IconSize { get; }
        public int ControlButtonSize { get; }
        public int ControlFontSize { get; }
        public int RowCapacity { get; }
        public string FilePath { get; }
        public Config(string jsonFilePath, int margin = 50, int iconSize = 75, int controlButtonSize = 30, int rowCapacity = 4)
        {
            Margin = margin;
            IconSize = iconSize;
            ControlButtonSize = controlButtonSize;
            ControlFontSize = ControlButtonSize / 2;
            RowCapacity = rowCapacity;
            FilePath = jsonFilePath;
        }
    }
}
