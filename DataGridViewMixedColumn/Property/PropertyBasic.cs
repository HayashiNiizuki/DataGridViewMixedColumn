using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MixedColumn.Property {
    public abstract class PropertyBasic {
        public string name;
        public string fldname;
        protected object _value;
        protected List<String> Filters = new List<string>();
        public abstract string Value { get; set; }
        public abstract byte[] Bytes(string name, string type, string is_ptr, string size);
        public PropertyBasic parent;

        public virtual void Load(XmlNode node) {
            this.name = node.Attributes["name"]?.Value;
            if (node.Attributes["fldname"] != null)
                this.fldname = node.Attributes["fldname"]?.Value;
            if (node.Attributes["Filters"] != null)
                this.Filters = node.Attributes["Filters"]?.Value.Split('|').ToList();
        }

        public bool isRoot {
            get {
                return parent == null;
            }
        }

        public bool Satisfy(string Filter) {
            if (this.parent != null)
                return this.parent.Satisfy(Filter) && this.SelfSatisfy(Filter);
            else return SelfSatisfy(Filter);
        }

        protected bool SelfSatisfy(string Filter) {
            if (Filters.Count > 0) {
                return Filters.Contains(Filter);
            } else return true;
        }
    }
}
