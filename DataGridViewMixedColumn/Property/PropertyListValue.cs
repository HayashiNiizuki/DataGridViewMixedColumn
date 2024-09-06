using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MixedColumn.Property {
    class PropertyListValue : PropertyNode {
        public PropertyListValue(PropertyBasic parent) : base(parent) { }

        public override void Load(XmlNode node) {
            base.Load(node);
            int length = int.Parse(node.Attributes["length"].Value);

            string[] headers;
            if (node.Attributes["headers"] != null && node.Attributes["headers"].Value != "") {
                headers = node.Attributes["headers"].Value.Split(',');
            } else {
                headers = new string[length];
                for (int i = 0; i < length; i++) {
                    headers[i] = (i + 1).ToString();
                }
            }

            string[] values;
            if (node.Attributes["values"] != null && node.Attributes["values"].Value != "") {
                values = node.Attributes["values"].Value.Split(',');
            } else {
                values = new string[length];
                for (int i = 0; i < length; i++) {
                    values[i] = "0";
                }
            }

            for (int i = 0; i < length; i++) {
                PropertyText txt = new PropertyText(this);
                txt.name = headers[i];
                txt.Text = values[i];
                this.subProperties.Add(txt);
            }
        }

        public override string Value {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public override string ToString() {
            return string.Join(",", this.subProperties.Select(e => e.ToString()));
        }

        public override byte[] Bytes(string name, string type, string is_ptr, string size) {
            int length = int.Parse(size);
            int bL = type == "int" ? 4 : 8;
            Byte[] bytes = new byte[length * bL];
            for (int i = 0; i < length; i++) {
                Array.Copy(this.subProperties[i].Bytes(name, type, is_ptr, size), 0, bytes, i * bL, bL);
            }
            return bytes;
        }
    }
}
