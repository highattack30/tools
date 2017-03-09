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

namespace passivities_tool_0
{
    public partial class Form1 : Form
    {
        int max_arguments = 0;
        int mob_size_counter = 0;
        uint max_id = 0;
        TreeNode unk_atr = null;
        byte opcode = 0;
        List<string> unknown_atr = new List<string>();
        Dictionary<int, string> read_opcodes = new Dictionary<int, string>();
        Dictionary<string, int> mobSize = new Dictionary<string, int>();
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
            if (e.Name != "Passive")
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
                    if (at.Name.LocalName == "id")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 0;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)0);
                        wr.Write(uint.Parse(at.Value));

                        if (uint.Parse(at.Value) > max_id)
                            max_id = uint.Parse(at.Value);
                    }
                    else if (at.Name.LocalName == "prob")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 1;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)1);
                        wr.Write(float.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "type")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 2;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)2);
                        wr.Write(uint.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "value")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 3;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)3);
                        wr.Write(float.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "balancedByTargetCount")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 4;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)4);
                        wr.Write(at.Value == "True" ? true : false);
                    }
                    else if (at.Name.LocalName == "judgmentOnce")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 5;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)5);
                        wr.Write(at.Value == "True" ? true : false);
                    }
                    else if (at.Name.LocalName == "kind")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 6;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)6);
                        wr.Write(uint.Parse(at.Value));
                    }

                    else if (at.Name.LocalName == "mobSize")
                    {
                        if (!mobSize.ContainsKey(at.Value))
                        {
                            mobSize.Add(at.Value, ++mob_size_counter);
                        }

                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 7;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)7);
                        wr.Write((uint)mobSize[at.Value]);
                    }

                    else if (at.Name.LocalName == "method")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 8;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)8);
                        wr.Write(uint.Parse(at.Value));
                    }

                    else if (at.Name.LocalName == "tickInterval")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 9;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)9);
                        wr.Write(uint.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "condition")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 10;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)10);
                        wr.Write(uint.Parse(at.Value));
                    }

                    else if (at.Name.LocalName == "abnormalityCategory")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 11;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)11);
                        wr.Write(uint.Parse(at.Value));
                    }

                    else if (at.Name.LocalName == "abnormalityKind")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 12;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)12);
                        wr.Write(uint.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "conditionCategory")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 13;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)13);

                        if(at.Value.Contains(';'))
                        {
                            MessageBox.Show("conditionCategory Contains ';'");
                        }
                        else if (at.Value.Contains(','))
                        {
                            string[] values = at.Value.Split(',');
                            byte count = 0;
                            foreach (string i in values)
                                if (i != "" && i != ",")
                                    count++;
                            wr.Write(count);
                            for (uint i = 0; i < values.Length; i++)
                                wr.Write(uint.Parse(values[i]));
                        }
                        else{ wr.Write((byte)1);
                            wr.Write(uint.Parse(at.Value));
                        }
                    }
                    else if (at.Name.LocalName == "conditionValue")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 14;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)14);
                        wr.Write(float.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "mySkillCategory")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 15;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)15);
                        wr.Write(uint.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "passivityChangeId")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 16;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)16);
                        wr.Write(uint.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "prevPassivityId")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 17;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)17);
                        wr.Write(uint.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "passivityChangeTime")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 18;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)18);
                        wr.Write(uint.Parse(at.Value));
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
            textBox1.Enabled = button1.Enabled = false;
            treeView1.Nodes.Clear();
            mob_size_counter = 0;
            max_arguments = 0;
            max_id = 0;
            mobSize.Clear();
            empty_atr = unk_atr = null;

            int max = 0;
            try
            {
                max = int.Parse(textBox1.Text);
            }
            catch (Exception ee)
            {
                MessageBox.Show(this, "Only numbers!");
                textBox1.Clear();
                textBox1.Select();
                textBox1.Enabled = button1.Enabled = true;
                return;
            }

            if (max > 512 || max == 0)
            {
                MessageBox.Show(this, "MAX value ->[1,512]!");
                textBox1.Clear();
                textBox1.Select();
                textBox1.Enabled = button1.Enabled = true;
                return;
            }
            string name = "passivity_data_[" + DateTime.Now.TimeOfDay.TotalSeconds + "].bin";

            FileStream file;
            try
            {
                file = new FileStream(name, FileMode.Create);
            }
            catch (Exception ee)
            {
                MessageBox.Show(this, "ERROR: " + ee.Message);
                textBox1.Clear();
                textBox1.Select();
                textBox1.Enabled = button1.Enabled = true;
                return;
            }

            BinaryWriter wr = new BinaryWriter(file);
            int fail_count = 0, item_count = 0, total_items = 0;
            XDocument document = null;

            for (int i = 0; i < max; i++)
            {
                try
                {
                    document = XDocument.Load("Passivity-" + i + ".xml");
                    foreach (XElement el in document.Root.Elements())
                    {
                        if (!write_to_file(ref wr, el))
                            fail_count++;
                        else item_count++;
                    }


                    TreeNode n = new TreeNode("Read [" + "Passivity-" + i + ".xml]");
                    n.Nodes.Add("Passivity elemets[" + item_count + "]");
                    n.Nodes.Add("Non Passivity elemets[" + fail_count + "]");

                    if (unk_atr != null)
                    {
                        unk_atr.Text += (" [" + unk_atr.Nodes.Count + "]");
                        n.Nodes.Add(unk_atr);
                        unk_atr = null;
                    }

                    treeView1.Nodes.Add(n);
                    total_items += item_count;
                    fail_count = 0; item_count = 0;
                }
                catch (Exception ee)
                {
                    TreeNode n = new TreeNode("Failed to open [" + "Passivity-" + i + ".xml]");
                    n.Nodes.Add("ERROR: " + ee.Message);
                    treeView1.Nodes.Add(n);
                }
            }
            treeView1.Nodes.Add("Max Passivity Id Value[" + max_id + "]");


            wr.Write((byte)255);
            wr.Close();
            file.Close();


            name += ".txt";
            string line;
            var list_ = read_opcodes.Keys.ToList();
            list_.Sort();

            using (StreamWriter wrr = new StreamWriter(name))
            {
                foreach (int key in list_)
                {
                    line = read_opcodes[key] + "=" + key + ",";
                    wrr.WriteLine(line);
                }
            }

            treeView1.Nodes.Add("Unknown Passivity Node Attributes [" + unknown_atr.Count + "]");
            foreach (string s in unknown_atr)
                treeView1.Nodes[treeView1.Nodes.Count - 1].Nodes.Add(s);

            treeView1.Nodes.Add("Total Passivities read[" + total_items + "]");
            treeView1.Nodes.Add("By Narcis96, Enjoy!");
            textBox1.Enabled = button1.Enabled = true;
        }
    }
}
