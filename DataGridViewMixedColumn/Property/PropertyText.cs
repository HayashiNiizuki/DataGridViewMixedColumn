using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MixedColumn.Property {
    public class PropertyText : PropertyBasic {
        public PropertyText(PropertyBasic parent) {
            this.parent = parent;
        }
        public string Text {
            get {
                return _value as string;
            }
            set {
                _value = value;
            }
        }

        public override void Load(XmlNode node) {
            base.Load(node);
            this.Text = node.Attributes["value"]?.Value;
        }

        public override string ToString() {
            return this.Text;
        }

        public override string Value {
            get {
                return this.Text;
            }
            set {
                if (value != null) 
                    this.Text = value;
            }
        }

        public override byte[] Bytes(string name, string type, string is_ptr, string size) {
            switch (type) {
                case "int":
                    return BitConverter.GetBytes(int.Parse(this.Value));
                case "double":
                    return BitConverter.GetBytes(double.Parse(this.Value));
                case "bool":
                    return BitConverter.GetBytes(bool.Parse(this.Value));
                default:
                    throw new Exception("what format do u expect????");
            }
        }
    }
}
