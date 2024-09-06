using MixedColumn.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MixedColumn.Controls {
    public class DataGridViewMixedColumn : DataGridViewColumn {
        public DataGridViewMixedColumn() : base(new DataGridViewMixedCell()) {
        }

        public override DataGridViewCell CellTemplate {
            get { return base.CellTemplate; }
            set {
                if (value != null && !value.GetType().IsAssignableFrom(typeof(DataGridViewMixedCell))) {
                    throw new InvalidCastException("Must be a DataGridViewMixedCell");
                }
                base.CellTemplate = value;
            }
        }
    }

    public class DataGridViewMixedCell : DataGridViewTextBoxCell {
        public DataGridViewMixedCell() : base() {

        }

        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle) {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

            var control = DataGridView.EditingControl as IDataGridViewEditingControl;

            if (this.Value is PropertyComboBox) {
                var comboBoxControl = control as ComboBoxEditingControl;
                if (comboBoxControl != null) {
                    comboBoxControl.Items.Clear();
                    comboBoxControl.Items.AddRange((this.Value as PropertyComboBox).Items.ToArray());
                    comboBoxControl.SelectedIndex = (this.Value as PropertyComboBox).selected;
                }
            } else if (this.Value is PropertyText) {
                var textBoxControl = control as TextBoxEditingControl;
                if (textBoxControl != null) {
                    textBoxControl.Text = initialFormattedValue.ToString();
                }
            } else if (this.Value is PropertyNode) {
                var nodeControl = control as NodeEditingControl;
                if (nodeControl != null) {
                    nodeControl.Text = "Sub Form";
                    //nodeControl.Click += this.UpdateValue;
                }
            } else {
                throw new NotImplementedException();
            }
        }

        public override Type EditType {
            get {
                if (this.Value is PropertyComboBox) {
                    return typeof(ComboBoxEditingControl);
                } else if (this.Value is PropertyText) {
                    return typeof(TextBoxEditingControl);
                } else if (this.Value is PropertyNode) {
                    return typeof(NodeEditingControl);
                } else {
                    throw new NotImplementedException();
                }
            }
        }

        public override Type ValueType {
            get { return typeof(string); }
        }

        public override object DefaultNewRowValue {
            get { return string.Empty; }
        }

        protected override bool SetValue(int rowIndex, object value) {
            if (this.Value is PropertyText) {
                (this.Value as PropertyText).Value = value.ToString();
                return true;
            } else {
                return base.SetValue(rowIndex, value);
            }
        }

        public void UpdateValue() {
            var control = DataGridView.EditingControl as IDataGridViewEditingControl;
            if (this.Value is PropertyComboBox) {
                var comboBoxControl = control as ComboBoxEditingControl;
                if (comboBoxControl != null)
                    (this.Value as PropertyComboBox).selected = comboBoxControl.SelectedIndex;
            } else if (this.Value is PropertyNode) {
                // (this.DataGridView as TierDataGridView)?.LoadFromNode(this.Value as PropertyNode);
            } else if (this.Value is PropertyNode) {
                var textBoxControl = control as TextBoxEditingControl;
                if (textBoxControl != null)
                    (this.Value as PropertyText).Text = textBoxControl.Text;
            }
        }

        protected override void OnMouseClick(DataGridViewCellMouseEventArgs e) {
            base.OnMouseClick(e);
            if (this.Value is PropertyNode) {
                (this.DataGridView as TierDataGridView)?.LoadFromNode(this.Value as PropertyNode);
            }
        }
    }
}
