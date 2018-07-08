using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;

namespace SnippetCreation
{
    // XML 
    public partial class MainForm
    {
        // WRITE
        private void FormSnippet(string xmlFileName)
        {
            xmlWriter = XmlWriter.Create(xmlFileName);

            xmlWriter.WriteStartDocument();

            xmlWriter.WriteStartElement("CodeSnippets");
            xmlWriter.WriteStartElement("CodeSnippet");
            xmlWriter.WriteAttributeString("Format", "1.0.0");

            WriteHeader(xmlWriter);
            xmlWriter.WriteStartElement("Snippet");
            WriteSnippet(xmlWriter);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();
            xmlWriter.Close();

        }
        private void WriteHeader(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Header");

            if (!String.IsNullOrWhiteSpace(TitleTextBox.Text))
            {
                xmlWriter.WriteStartElement("Title");
                xmlWriter.WriteValue(TitleTextBox.Text);
                xmlWriter.WriteEndElement();
            }
            if (!String.IsNullOrWhiteSpace(AuthorTextBox.Text))
            {
                xmlWriter.WriteStartElement("Author");
                xmlWriter.WriteValue(AuthorTextBox.Text);
                xmlWriter.WriteEndElement();
            }
            if (!String.IsNullOrWhiteSpace(DescTextBox.Text))
            {
                xmlWriter.WriteStartElement("Description");
                xmlWriter.WriteValue(DescTextBox.Text);
                xmlWriter.WriteEndElement();
            }
            if (!String.IsNullOrWhiteSpace(ShortcutTextBox.Text))
            {
                xmlWriter.WriteStartElement("Shortcut");
                xmlWriter.WriteValue(ShortcutTextBox.Text);
                xmlWriter.WriteEndElement();
            }

            xmlWriter.WriteEndElement();
        }
        private void WriteSnippet(XmlWriter xmlWriter)
        {
            WriteImport(xmlWriter);
            WriteDeclaration(xmlWriter);
            WriteCode(xmlWriter);
        }
        private void WriteCode(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Code");
            xmlWriter.WriteAttributeString("Language", "CSharp");

            xmlWriter.WriteCData(FormText());

            xmlWriter.WriteEndElement();
        }
        private string FormText()
        {
            string[] IDS = new string[LiteralList.Count];
            for (int i = 0; i < LiteralList.Count; ++i)
            {
                IDS[i] = LiteralList[i].ID;
            }
            string text = CodeRichTextBox.Text;
            for (int i = 0; i < IDS.Length; ++i)
            {
                text = text.Replace(IDS[i], String.Concat('$', IDS[i], "$"));
            }

            return text;
        }
        private MultiMap<string,string> CheckedRootInfo()
        {
            // бібліотека — ключ, простір імен — значення
            MultiMap<string, string> multiMap = new MultiMap<string, string>();

            foreach (TreeNode Dll in DllAndNamespaceTreeView.Nodes)
            {
                foreach (TreeNode name in Dll.Nodes)
                {
                    if (name.Checked && !multiMap.ContainsValue(name.Text))
                    {
                        // різні бібліотеки можуть містити однакові простори імен
                        // у файл записуємо лише одмн раз простір імен
                        multiMap[name.Parent.Text] = name.Text;
                    }
                }
            }
            return multiMap;
        }
        private void WriteImport(XmlWriter xmlWriter)
        {
            MultiMap<string, string> info = CheckedRootInfo();

            if (!info.IsEmpty()) // кількість вибраних елементів 
            {

                List<string> Assembly = info.Keys;
                List<string> NameSpaces = info.Values;

                xmlWriter.WriteStartElement("References");
                for (int i = 0; i < Assembly.Count; ++i)
                {
                    xmlWriter.WriteStartElement("Reference");
                    xmlWriter.WriteStartElement("Assembly");
                    xmlWriter.WriteValue(Assembly[i] + ".dll");
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                }
                xmlWriter.WriteEndElement();


                xmlWriter.WriteStartElement("Imports");
                for (int i = 0; i < NameSpaces.Count; ++i)
                {
                    xmlWriter.WriteStartElement("Import");
                    xmlWriter.WriteStartElement("Namespace");
                    xmlWriter.WriteValue(NameSpaces[i]);
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                }
                xmlWriter.WriteEndElement();
            }
        }
        private void WriteDeclaration(XmlWriter xmlWriter)
        {
            if (LiteralList.Count > 0)
            {
                xmlWriter.WriteStartElement("Declarations");

                for (int i = 0; i < LiteralList.Count; ++i)
                {
                    xmlWriter.WriteStartElement("Literal");

                    xmlWriter.WriteStartElement("ID");
                    xmlWriter.WriteValue(LiteralList[i].ID);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("ToolTip");
                    xmlWriter.WriteValue(LiteralList[i].ToolTip);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("Default");
                    xmlWriter.WriteValue(LiteralList[i].DefaultValue);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteEndElement();
                }

                xmlWriter.WriteEndElement();
            }
        }


        // READ
        private void ReadSnippet(string filePath)
        {
            xmlReader = XmlReader.Create(filePath);

        }
    }
}
