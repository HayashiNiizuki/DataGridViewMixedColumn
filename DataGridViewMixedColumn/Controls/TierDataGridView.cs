using MixedColumn.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MixedColumn.Controls {

    class TierDataGridView : DataGridView {
        public PropertyNode currentNode = null;
        private DataGridViewMixedColumn valueColumn;
        protected string Filter = null;
        public bool useFilter = false;
        public TierDataGridView() : base() {
            this.Columns.Add(new DataGridViewTextBoxColumn() {
                HeaderText = "Property",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            this.valueColumn = new DataGridViewMixedColumn() {
                HeaderText = "Value",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };
            this.Columns.Add(this.valueColumn);
            
            this.RowHeadersVisible = false;
            this.EditMode = DataGridViewEditMode.EditOnEnter;
            this.CellMouseEnter += tierCellMouseEnter;
            this.CellMouseLeave += tierCellMouseLeave;

            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
        }

        public void SetFilter(string filter) {
            this.Filter = filter;
            this.useFilter = true;
            this.RefreshDisplay();
        }

        public void ShowAll() {
            this.useFilter = false;
            this.RefreshDisplay();
        }

        public void RefreshDisplay() {
            this.LoadFromNode(this.currentNode);
        }

        public void LoadFromNode(PropertyNode node) {
            if (node == null) { 
                return;
            }
            this.currentNode = node;
            this.Rows.Clear();
            foreach (PropertyBasic property in node.subProperties) {
                if (!useFilter || property.Satisfy(this.Filter))
                    this.Rows.Add(property.name, property);
            }
        }

        public void Clear() {
            this.Rows.Clear();
            this.currentNode = null;
        }

        public void Up() {
            if (currentNode != null && !currentNode.isRoot) { 
                this.LoadFromNode(currentNode.parent as PropertyNode);
            }
        }

        public void RefreshNode() {
            for (int i = 0; i < this.Rows.Count; i++) {
                this.currentNode.subProperties[i].Value = (this.Rows[i].Cells[1].Value as PropertyBasic)?.Value;
            }
        }

        private void tierCellMouseEnter(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex >= 0 && e.ColumnIndex == 1) {
                this.CurrentCell = this.Rows[e.RowIndex].Cells[e.ColumnIndex];
                this.BeginEdit(true);
            }
        }

        private void tierCellMouseLeave(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex >= 0 && e.ColumnIndex == 1) {
                DataGridViewMixedCell cell = this.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewMixedCell;
                if (cell != null) {
                    cell.UpdateValue();
                }
            }
            this.EndEdit();
        }
    }
}
