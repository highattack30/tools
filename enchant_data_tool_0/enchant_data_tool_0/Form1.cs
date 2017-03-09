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

namespace enchant_data_tool_0
{
    public partial class Form1 : Form
    {

        Dictionary<string, int> kind = new Dictionary<string, int>();
        int kind_counter = 0;

        Dictionary<int, string> read_opcodes = new Dictionary<int, string>();
        List<string> unknown_atr = new List<string>();
        int max_arguments = 0;
        int max_id = 0;
        byte opcode = 0;
        TreeNode unk_atr = null;
        public Form1()
        {
            InitializeComponent();
            read_opcodes.Add(240, "start of Effect");
            read_opcodes.Add(241, "end of Effect");
            read_opcodes.Add(242, "start of BasicStat");
            read_opcodes.Add(243, "end of BasicStat");

            read_opcodes.Add(250, "start of item");
            read_opcodes.Add(251, "end of item");
            read_opcodes.Add(255, "end of file");
        }
        TreeNode empty_atr;
        bool write_to_file(ref BinaryWriter wr, XElement e)
        {

            if (e.Name.LocalName == "Effect")
            {
                wr.Write((byte)0xF0);
                foreach (XAttribute at in e.Attributes().ToList())
                {
                    try
                    {
                        if (at.Name == "step")
                        {
                            if (!read_opcodes.ContainsValue(at.Name.LocalName))
                            {
                                opcode = 5;
                                read_opcodes.Add(opcode, at.Name.LocalName);
                            }
                            wr.Write((byte)5);
                            wr.Write(uint.Parse(at.Value));
                        }
                        else if (at.Name == "passivityCategoryId")
                        {
                            if (!read_opcodes.ContainsValue(at.Name.LocalName))
                            {
                                opcode = 6;
                                read_opcodes.Add(opcode, at.Name.LocalName);
                            }
                            wr.Write((byte)6);
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
                wr.Write((byte)0xF1);
            }
            else if (e.Name.LocalName == "BasicStat")
            {
                wr.Write((byte)0xF2);
                foreach (XAttribute at in e.Attributes().ToList())
                {
                    try
                    {
                        if (at.Name == "kind")
                        {
                            if (!read_opcodes.ContainsValue(at.Name.LocalName))
                            {
                                opcode = 1;
                                read_opcodes.Add(opcode, at.Name.LocalName);
                            }

                            if (!kind.ContainsKey(at.Value))
                            {
                                kind.Add(at.Value, ++kind_counter);
                            }

                            wr.Write((byte)1);
                            wr.Write((uint)kind[at.Value]);
                        }
                        else if (at.Name == "rate")
                        {
                            if (!read_opcodes.ContainsValue(at.Name.LocalName))
                            {
                                opcode = 2;
                                read_opcodes.Add(opcode, at.Name.LocalName);
                            }
                            wr.Write((byte)2);
                            wr.Write(float.Parse(at.Value));
                        }
                        else if (at.Name == "enchantStep")
                        {
                            if (!read_opcodes.ContainsValue(at.Name.LocalName))
                            {
                                opcode = 3;
                                read_opcodes.Add(opcode, at.Name.LocalName);
                            }
                            wr.Write((byte)3);
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
                wr.Write((byte)0xF3);
            }
            else
                return false;


            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            max_arguments = 0;
            max_id = 0;
            empty_atr = unk_atr = null;
            kind_counter = 0;
            kind.Clear();

            string filename;
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Xml Document(.xml)|*.xml";
            if (op.ShowDialog(this) != DialogResult.OK)
                return;
            filename = op.FileName;

            string name = "equipment_enchant_data_[" + DateTime.Now.TimeOfDay.TotalSeconds + "].bin";

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
                if (el.Name.LocalName == "EnchantData")
                {
                    var e_list = el.Elements().ToList();
                    if (e_list.Count > 0 && e_list[0].Name.LocalName == "Enchant")
                    {
                        wr.Write((byte)250);
                        if (!read_opcodes.ContainsValue("id"))
                        {
                            opcode = 0;
                            read_opcodes.Add(opcode, "id");
                        }
                        wr.Write((byte)0);
                        wr.Write(uint.Parse(e_list[0].FirstAttribute.Value));


                        foreach (XElement ee in e_list[0].Elements())
                        {
                            if (!write_to_file(ref wr, ee))
                                fail_count++;
                            else
                                item_count++;


                            total_items++;
                        }
                        wr.Write((byte)251);
                    }
                }
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
            n.Nodes.Add("Equipment Enchant Data elemets[" + item_count + "]");
            n.Nodes.Add("Non Equipment Enchant Data elemets[" + fail_count + "]");
            treeView1.Nodes.Add("Max Equipment Enchant Data Id Value[" + max_id + "]");
            treeView1.Nodes.Add("Unknown Equipment Enchant Data Node Attributes [" + unknown_atr.Count + "]");
            foreach (string s in unknown_atr)
                treeView1.Nodes[treeView1.Nodes.Count - 1].Nodes.Add(s);

            treeView1.Nodes.Add("Total Equipment Enchant Data read[" + total_items + "]");
            treeView1.Nodes.Add("By Narcis96, Enjoy!");
        }
    }
}
