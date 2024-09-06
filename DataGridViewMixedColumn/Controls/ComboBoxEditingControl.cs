using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MixedColumn.Controls {
    public class ComboBoxEditingControl : ComboBox, IDataGridViewEditingControl {
        DataGridView dataGridView;
        private bool valueChanged = false;
        int rowIndex;
        public bool connected = false;

        public ComboBoxEditingControl() {
            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public object EditingControlFormattedValue {
            get { return this.Text; }
            set {
                if (value is string) {
                    this.Text = (string)value;
                }
            }
        }

        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context) {
            return EditingControlFormattedValue;
        }

        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle) {
            this.Font = dataGridViewCellStyle.Font;
            this.ForeColor = dataGridViewCellStyle.ForeColor;
            this.BackColor = dataGridViewCellStyle.BackColor;
        }

        public int EditingControlRowIndex {
            get { return rowIndex; }
            set { rowIndex = value; }
        }

        public bool EditingControlWantsInputKey(Keys key, bool dataGridViewWantsInputKey) {
            return key == Keys.Left || key == Keys.Right || key == Keys.Up || key == Keys.Down || key == Keys.Enter || key == Keys.Escape || key == Keys.Tab;
        }

        public void PrepareEditingControlForEdit(bool selectAll) {
        }

        public bool RepositionEditingControlOnValueChange {
            get { return false; }
        }

        public DataGridView EditingControlDataGridView {
            get { return dataGridView; }
            set { dataGridView = value; }
        }

        public bool EditingControlValueChanged {
            get { return valueChanged; }
            set { valueChanged = value; }
        }

        public Cursor EditingPanelCursor {
            get { return base.Cursor; }
        }

        protected override void OnTextChanged(EventArgs eventargs) {
            valueChanged = true;
            this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
            base.OnTextChanged(eventargs);
        }
    }

}
