using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGrid = new DataGridView();
            dataGrid.Columns.Add("Name", "Name");
            dataGrid.Columns.Add("Age", "Age");
            dataGrid.Rows.Add("John Doe", 30);
            dataGrid.Rows.Add("Jane Smith", 25);

            // Create a TableComponent and use it to save the data to binary and text files
            TableComponent tableComponent = new TableComponent(new BinaryFileAdapter());
            tableComponent.Save("data.bin", dataGrid);

            tableComponent = new TableComponent(new TextFileAdapter());
            tableComponent.Save("data.txt", dataGrid);
            tableComponent = new TableComponent(new BinaryFileAdapter());
            tableComponent.Load("data.bin", dataGrid);
            tableComponent = new TableComponent(new TextFileAdapter());
            tableComponent.Load("data.txt", dataGrid);

            // Display the DataGridView to verify that the data was loaded correctly
            Form form = new Form();
            form.Controls.Add(dataGrid);
            Application.Run(form);
        }
    }
}
