namespace _3DxConfigurationEditor.Objects
{
    public class MacroEntry
    {

        /// <summary>
        /// default ctor
        /// </summary>
        /// <param name="iD"></param>
        public MacroEntry(string iD)
        {
            ID = iD;
        }

        /// <summary>
        /// The macro Id
        /// </summary>
        public string ID { get; set; }

        public override string ToString()
        {
            return this.ID;
        }
    }
}