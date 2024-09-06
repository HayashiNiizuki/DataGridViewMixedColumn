using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MixedColumn.Property {
    public class PropertyNode : PropertyBasic {
        public List<PropertyBasic> subProperties = new List<PropertyBasic>();
        public PropertyNode(PropertyBasic parent) {
            this.parent = parent;
        }

        public override void Load(XmlNode node) {
            base.Load(node);
            subProperties.Clear();
            foreach (XmlNode subnode in node.ChildNodes) {
                subProperties.Add(PropertyFactory.createProperty(subnode, this));
            }
        }

        public PropertyNode Root {
            get {
                PropertyNode root = this;
                while (root.parent is PropertyNode parent) {
                    root = parent;
                }
                return root;
            }
        }

        public override string ToString() {
            return "Sub Form...";
        }

        public override string Value {
            get {
                return this.ToString();
            }
            set {
                return;
            }
        }

        public PropertyBasic FindMatchedNode(string name, string type, string is_ptr, string size) {
            foreach (PropertyBasic subNode in subProperties) {
                if (subNode.fldname == name) {
                    return subNode;
                } else if (subNode is PropertyNode) { 
                    PropertyBasic res = (subNode as PropertyNode).FindMatchedNode(name, type, is_ptr, size);
                    if (res != null) { 
                        return res;
                    }
                }
            }
            return null;
        }

        public string FindMatchedStringValue(string name, string type, string is_ptr, string size) {
            PropertyBasic matched = this.FindMatchedNode(name, type, is_ptr, size);
            if (matched != null) { 
                return matched.Value;
            } else {
                return null;
            }
        }

        public override byte[] Bytes(string name, string type, string is_ptr, string size) {
            throw new NotImplementedException();
        }
    }
}
