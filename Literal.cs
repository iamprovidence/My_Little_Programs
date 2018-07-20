using System.Drawing;

namespace SnippetBuilder
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
        public Literal(Literal literal, Color color) :
            this(literal.id, literal.toolTip, literal.defaultValue, color)
        { }

        public Literal(string id, string toolTip, string defaultValue, Color color)
        {
            this.id = id;
            this.toolTip = toolTip;
            this.defaultValue = defaultValue;
            this.color = color;
        }
        public static Literal XmlParse(string xml)
        {
            char[] values = new char[xml.Length];
            bool write = true;
            int writeIndex = 0;
            for(int i = 0; i < xml.Length; ++i)
            {
                if (xml[i] == '<') write = false;
                if (write) values[writeIndex++] = xml[i];
                if (xml[i] == '>')
                {
                    write = true;
                    values[writeIndex++] = ' ';
                }
            }
            string[] result = new string(values).TrimEnd().Split();
            return new Literal(result[1], result[3], result[5]);
        }
    }
}
