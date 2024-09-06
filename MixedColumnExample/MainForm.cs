using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MixedColumn.Controls;
using MixedColumn.Property;

namespace MixedColumnExample {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
            this.tierDataGridView = new TierDataGridView() { Dock = DockStyle.Fill };
            this.Controls.Add(this.tierDataGridView);
            this.LoadExample();
        }

        protected void LoadExample() {
            if (File.Exists("./example.xml")){
                PropertyNode root = PropertyFactory.readPropertyFile("./example.xml");
                this.tierDataGridView.LoadFromNode(root);
            } else {
                MessageBox.Show("Config file example.xml not exist!");
            }
        }

        protected TierDataGridView tierDataGridView;
    }
}
