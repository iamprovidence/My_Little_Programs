using System.Drawing;

namespace SnippetCreation
{
    class Literal
    {
        // FIELDS
        string id;
        string toolTip;
        string defaultValue;
        Color color;

        // PROPERTIES
        public string ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public string ToolTip
        {
            get
            {
                return toolTip;
            }
            set
            {
                toolTip = value;
            }
        }
        public string DefaultValue
        {
            get
            {
                return defaultValue;
            }
            set
            {
                defaultValue = value;
            }
        }
        public Color Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }

        // CONSTRUCTORS
        public Literal(string defaultValueForEach):
            this(defaultValueForEach, defaultValueForEach, defaultValueForEach, Color.Red)
        { }
        public Literal(string defaultValueForEach, Color color) :
            this(defaultValueForEach, defaultValueForEach, defaultValueForEach, color)
        { }
        public Literal(string id, string toolTip, string defaultValue) :
            this(id, toolTip, defaultValue, Color.Red)
        { }

        public Literal(string id, string toolTip, string defaultValue, Color color)
        {
            this.id = id;
            this.toolTip = toolTip;
            this.defaultValue = defaultValue;
            this.color = color;
        }
    }
}
