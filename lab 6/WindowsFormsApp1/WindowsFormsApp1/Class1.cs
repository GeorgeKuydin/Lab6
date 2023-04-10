using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

// Define the interface for the adapter
interface IDataAdapter
{
    void Save(string fileName, DataGridView dataGrid);
    void Load(string fileName, DataGridView dataGrid);
}

// Define the adapter that saves and loads data to/from binary files
class BinaryFileAdapter : IDataAdapter
{
    public void Save(string fileName, DataGridView dataGrid)
    {
        List<object[]> rows = new List<object[]>();
        foreach (DataGridViewRow row in dataGrid.Rows)
        {
            object[] values = new object[dataGrid.Columns.Count];
            for (int i = 0; i < dataGrid.Columns.Count; i++)
            {
                values[i] = row.Cells[i].Value;
            }
            rows.Add(values);
        }

        using (FileStream stream = new FileStream(fileName, FileMode.Create))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, rows);
        }
    }

    public void Load(string fileName, DataGridView dataGrid)
    {
        using (FileStream stream = new FileStream(fileName, FileMode.Open))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            List<object[]> rows = (List<object[]>)formatter.Deserialize(stream);

            dataGrid.Rows.Clear();
            foreach (object[] values in rows)
            {
                int index = dataGrid.Rows.Add(values);
            }
        }
    }
}

// Define the adapter that saves and loads data to/from text files
class TextFileAdapter : IDataAdapter
{
    public void Save(string fileName, DataGridView dataGrid)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (DataGridViewRow row in dataGrid.Rows)
            {
                List<string> values = new List<string>();
                for (int i = 0; i < dataGrid.Columns.Count; i++)
                {
                    values.Add(row.Cells[i].Value.ToString());
                }
                writer.WriteLine(string.Join(",", values));
            }
        }
    }

    public void Load(string fileName, DataGridView dataGrid)
    {
        using (StreamReader reader = new StreamReader(fileName))
        {
            dataGrid.Rows.Clear();
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] values = line.Split(',');
                int index = dataGrid.Rows.Add(values);
            }
        }
    }
}

// Define the client that uses the adapter
class TableComponent
{
    private IDataAdapter adapter;

    public TableComponent(IDataAdapter adapter)
    {
        this.adapter = adapter;
    }

    public void Save(string fileName, DataGridView dataGrid)
    {
        adapter.Save(fileName, dataGrid);
    }

    public void Load(string fileName, DataGridView dataGrid)
    {
        adapter.Load(fileName, dataGrid);
    }
}