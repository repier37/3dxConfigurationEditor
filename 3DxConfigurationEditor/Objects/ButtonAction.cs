namespace _3DxConfigurationEditor.Objects
{
    public class ButtonAction
    {

        public string ID { get; set; }
        public string Name { get; set; }
        public string ImageSource { get; set; }
        public ButtonType ButtonType { get; set; }

        public bool HasImage => (!string.IsNullOrEmpty(this.ImageSource));
    }

    public enum ButtonType
    {
        App=0
    }
}