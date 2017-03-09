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
using System.Xml.Linq;

namespace equipment_data_tool__0
{
    public partial class Form1 : Form
    {
        Dictionary<string, int> parts = new Dictionary<string, int>();
        int part_counter = 0;

        Dictionary<int, string> read_opcodes = new Dictionary<int, string>();
        List<string> unknown_atr = new List<string>();
        int max_arguments = 0;
        int max_id = 0;
        byte opcode = 0;
        TreeNode unk_atr = null;

        public Form1()
        {
            InitializeComponent();
            read_opcodes.Add(250, "start of item");
            read_opcodes.Add(251, "end of item");
            read_opcodes.Add(255, "end of file");
        }

        TreeNode empty_atr;
        bool write_to_file(ref BinaryWriter wr, XElement e)
        {
            if (e.Name != "Equipment")
                return false;
            empty_atr = new TreeNode("Total empty Node Attributes");
            unk_atr = new TreeNode("Unknow Node Attributes");
            List<XAttribute> atr = e.Attributes().ToList();
            if (atr.Count > max_arguments)
                max_arguments = atr.Count;

            wr.Write((byte)250);
            foreach (XAttribute at in atr)
            {
                if (at.Value == "")
                {
                    empty_atr.Nodes.Add(at.Name.LocalName);
                    continue;
                }
                try
                {
                    if (at.Name.LocalName == "equipmentId")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 0;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)0);
                        wr.Write(uint.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "balance")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 1;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)1);
                        wr.Write(uint.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "countOfSlot")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 2;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)2);
                        wr.Write(uint.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "def")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 3;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)3);
                        wr.Write(uint.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "impact")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 4;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)4);
                        wr.Write(uint.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "maxAtk")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 5;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)5);
                        wr.Write(uint.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "minAtk")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 6;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)6);
                        wr.Write(uint.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "passivityLinkG")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 7;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)7);
                        wr.Write(uint.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "part")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 8;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }

                        if (!parts.ContainsKey(at.Value))
                        {
                            parts.Add(at.Value, ++part_counter);
                        }

                        wr.Write((byte)8);
                        wr.Write((uint)parts[at.Value]);
                    }
                    else
                    {
                        unk_atr.Nodes.Add(at.Name.LocalName);
                    }

                }
                catch (Exception ee)
                {
                    TreeNode nn = new TreeNode("Failed to parse NodeAttribute[" + at.Name.LocalName + "]!");
                    nn.Nodes.Add(e.ToString());
                    nn.Nodes.Add("ERROR " + ee.Message + " VALUE[" + at.Value + "]");
                    treeView1.Nodes.Add(nn);

                    if (!unknown_atr.Contains(at.Name.LocalName))
                    {
                        unknown_atr.Add(at.Name.LocalName);
                    }
                }
            }

            wr.Write((byte)251);
            return true;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            max_arguments = 0;
            max_id = 0;
            empty_atr = unk_atr = null;
            part_counter = 0;
            parts.Clear();

            string filename;
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Xml Document(.xml)|*.xml";
            if (op.ShowDialog(this) != DialogResult.OK)
                return;
            filename = op.FileName;

            string name = "equipment_data_[" + DateTime.Now.TimeOfDay.TotalSeconds + "].bin";

            FileStream file;
            try
            {
                file = new FileStream(name, FileMode.Create);
            }
            catch (Exception ee)
            {
                MessageBox.Show(this, "ERROR: " + ee.Message);

                return;
            }

            BinaryWriter wr = new BinaryWriter(file);
            int fail_count = 0, item_count = 0, total_items = 0;
            XDocument doc = XDocument.Load(filename);
            foreach (XElement el in doc.Root.Elements())
            {
                if (!write_to_file(ref wr, el))
                    fail_count++;
                else
                    item_count++;

                total_items++;
            }

            wr.Write((byte)255);
            wr.Close();
            file.Close();

            var list_ = read_opcodes.Keys.ToList();
            list_.Sort();

            name += ".txt";
            string line;
            using (StreamWriter wrr = new StreamWriter(name))
            {
                foreach (int key in list_)
                {
                    line = read_opcodes[key] + "=" + key + ",";
                    wrr.WriteLine(line);
                }
            }


            

            TreeNode n = new TreeNode("Read [" + filename + ".xml]");
            n.Nodes.Add("Equipment Data elemets[" + item_count + "]");
            n.Nodes.Add("Non Equipment Data elemets[" + fail_count + "]");
            treeView1.Nodes.Add("Max Equipment Data Id Value[" + max_id + "]");

            if (checkBox1.Checked)
            {
                name = "parts_dump[" + DateTime.Now.TimeOfDay.TotalSeconds + "].txt";
                using (StreamWriter wrr = new StreamWriter(name))
                {
                    foreach (string key in parts.Keys)
                    {
                        wrr.WriteLine(key + " = " + parts[key] + ',');
                    }
                }


                treeView1.Nodes.Add("Dumped [" + parts.Count + "] 'part' data");
            }

            treeView1.Nodes.Add("Unknown Equipment Data Node Attributes [" + unknown_atr.Count + "]");
            foreach (string s in unknown_atr)
                treeView1.Nodes[treeView1.Nodes.Count - 1].Nodes.Add(s);

            treeView1.Nodes.Add("Total Equipment Data read[" + total_items + "]");
            treeView1.Nodes.Add("By Narcis96, Enjoy!");
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }
}
