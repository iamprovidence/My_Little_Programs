using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;

namespace SnippetBuilder
{
    // XML 
    public partial class MainForm
    {
        // WRITE
        private void FormSnippet(string xmlFileName)
        {
            xmlWriter = XmlWriter.Create(xmlFileName);

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteComment(madeWith);

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
            string[] IDS = LiteralList.Select(literal => literal.ID).ToArray();
            string[] text = CodeRichTextBox.Text.Split();

            for (int i = 0; i < IDS.Length; ++i)
            {
                for(int j = 0; j < text.Length; ++j)
                {
                    if(text[j] == IDS[i])
                    {
                        text[j] = String.Concat('$', IDS[i], '$');
                    }
                }
            }
            return String.Join(separator: " ", value: text);
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
                        // у файл записуємо лише один раз простір імен
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
                List<KeyValuePair<string, string>> NameSpaces = info.Reflection;

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
                    xmlWriter.WriteComment(NameSpaces[i].Key);
                    xmlWriter.WriteStartElement("Namespace");
                    xmlWriter.WriteValue(NameSpaces[i].Value);
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

            //xmlReader = XmlReader.Create(filePath);
            Dictionary<string, Control> net = new Dictionary<string, Control>();
            net.Add("Title", TitleTextBox);
            net.Add("Author", AuthorTextBox);
            net.Add("Description", DescTextBox);
            net.Add("Shortcut", ShortcutTextBox);
            net.Add("CODE", CodeRichTextBox);


            XmlTextReader xReader = new XmlTextReader(filePath);

            while (xReader.Read())
            {
                Next:
                switch (xReader.NodeType)
                {
                    #region DllAndNamespace
                    case XmlNodeType.Comment:
                        if (xReader.Value == madeWith) continue;

                        foreach (TreeNode node in DllAndNamespaceTreeView.Nodes)
                        {
                            if (node.Text == xReader.Value)
                            {

                                xReader.Read(); xReader.Read();
                                foreach (TreeNode child in node.Nodes)
                                {
                                    if (child.Text == xReader.Value)
                                    {
                                        child.Checked = true;
                                        goto End;
                                    }
                                }
                            }
                        }

                        End:
                        break; 
                    #endregion
                    case XmlNodeType.Element:
                        string name = xReader.Name;
                        if (name == "Literal")
                        {
                            AddLiteral(Literal.XmlParse(xReader.ReadInnerXml()));
                            goto Next;
                        }
                        if (net.ContainsKey(name))
                        {
                            xReader.Read();
                            net[name].Text = xReader.Value;
                        }
                        break;
                    case XmlNodeType.CDATA:
                        string[] code = xReader.Value.Trim().Split();
                        for (int i = 0; i < code.Length; ++i)
                        {
                            if (code[i] != String.Empty && code[i].First() == '$' && code[i].Last() == '$')
                            {
                                code[i] = code[i].Substring(1, code[i].Length - 2);
                            }
                        }
                        net["CODE"].Text = System.String.Join(" ", code);
                        break;
                }
            }


        }
    }
}
