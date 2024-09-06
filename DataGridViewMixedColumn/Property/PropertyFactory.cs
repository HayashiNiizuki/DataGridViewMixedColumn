using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MixedColumn.Property {
    public class PropertyFactory {
        public static PropertyBasic createProperty(XmlNode node, PropertyBasic parent = null) {
            PropertyBasic property;
            switch (node.Name) {
                case "PropertyText":
                    property = new PropertyText(parent);
                    break;
                case "PropertyComboBox":
                    property = new PropertyComboBox(parent);
                    break;
                case "PropertyNode":
                    property = new PropertyNode(parent);
                    break;
                case "PropertyListValue":
                    property = new PropertyListValue(parent);
                    break;
                default:
                    property = null;
                    break;
            }
            property?.Load(node);
            return property;
        }

        public static PropertyNode readPropertyFile(string file) {
            XmlDocument doc = new XmlDocument();
            doc.Load(file);

            PropertyNode root = new PropertyNode(null);
            root.Load(doc.DocumentElement);
            return root;
        }
    }
}
