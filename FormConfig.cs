
namespace SoftLauncher
{
    public class FormConfig
    {
        public int Margin { get; }
        public int IconSize { get; }
        public int ControlButtonSize { get; }
        public int ControlFontSize { get; }
        public int RowCapacity { get; }
        public FormConfig(int margin = 50, int appIconSize = 75, int controlButtonSize = 30, int rowCapacity = 4)
        {
            Margin = margin;
            IconSize = appIconSize;
            ControlButtonSize = controlButtonSize;
            ControlFontSize = ControlButtonSize / 2 - 2;
            RowCapacity = rowCapacity;
        }
    }
}
