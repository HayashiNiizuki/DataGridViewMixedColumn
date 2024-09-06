using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MixedColumn.Property {
    class PropertyComboBox : PropertyBasic {
        public PropertyComboBox(PropertyBasic parent) {
            this.parent = parent;
        }
        
        public List<string> Items {
            get {
                return _value as List<string>;
            }
            set {
                _value = value;
            }
        }

        public override void Load(XmlNode node) {
            base.Load(node);
            this.Items = node.Attributes["value"].Value?.Split(';').ToList();
            this.selected = int.Parse(node.Attributes["selected"]?.Value);
        }

        public override string ToString() {
            return this.Items[this.selected];
        }

        public int selected = 0;

        public override string Value {
            get {
                return this.selected.ToString();
            }
            set {
                if (value != null)
                    this.selected = int.Parse(value);
            }
        }

        public override byte[] Bytes(string name, string type, string is_ptr, string size) {
            return BitConverter.GetBytes(this.selected);
        }
    }
}
