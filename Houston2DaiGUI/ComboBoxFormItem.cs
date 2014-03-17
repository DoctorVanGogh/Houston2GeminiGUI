using System.Xml.Linq;

namespace Houston2DaiGUI
{
    public class ComboBoxFormItem
    {
        public string Name { get; set; }
        public XElement XElement { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}