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

namespace npcs_tool_0
{
    public partial class Form1 : Form
    {
        int race_counter = 0, size_counter = 0, class_counter = 0, resource_counter = 0, kind_counter = 0, battle_counter = 0, move_counter = 0, max_arguments = 0;

        Dictionary<int, string> read_opcodes = new Dictionary<int, string>();

        Dictionary<string, int> race_dump = new Dictionary<string, int>();
        Dictionary<string, int> size_dump = new Dictionary<string, int>();
        Dictionary<string, int> resource_dump = new Dictionary<string, int>();
        Dictionary<string, int> kind_dump = new Dictionary<string, int>();
        Dictionary<string, int> battle_dump = new Dictionary<string, int>();
        Dictionary<string, int> move_dump = new Dictionary<string, int>();
        Dictionary<string, int> class_dump = new Dictionary<string, int>();
        TreeNode unk_atr = null;
        byte opcpde;
        List<string> unknown_atr = new List<string>();
        List<TreeNode> failedNodes = new List<TreeNode>();
        public Form1()
        {
            InitializeComponent();


        }

        TreeNode empty_atr;
        bool write_template_head(ref BinaryWriter wr, XElement e)
        {
            failedNodes.Clear();
            List<XAttribute> atr = e.Attributes().ToList();
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
                        if (!read_opcodes.ContainsKey(0))
                        {
                            read_opcodes.Add(0, at.Name.LocalName);
                        }

                        wr.Write((byte)0);
                        wr.Write(uint.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "scale")
                    {
                        if (!read_opcodes.ContainsKey(1))
                        {
                            read_opcodes.Add(1, at.Name.LocalName);
                        }

                        wr.Write((byte)1);
                        wr.Write(float.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "size")
                    {
                        if (!read_opcodes.ContainsKey(2))
                        {
                            read_opcodes.Add(2, at.Name.LocalName);
                        }

                        if (!size_dump.ContainsKey(at.Value))
                        {
                            size_dump.Add(at.Value, ++size_counter);
                        }

                        wr.Write((byte)2);
                        wr.Write((uint)size_dump[at.Value]);
                    }
                    else if (at.Name.LocalName == "shapeId")
                    {
                        if (!read_opcodes.ContainsKey(3))
                        {
                            read_opcodes.Add(3, at.Name.LocalName);
                        }

                        wr.Write((byte)3);
                        wr.Write(uint.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "gender")
                    {
                        opcpde = 4;
                        if (!read_opcodes.ContainsKey(opcpde))
                        {
                            read_opcodes.Add(opcpde, at.Name.LocalName);
                        }
                        wr.Write(opcpde);
                        wr.Write(at.Value == "male" ? true : false);
                    }
                    else if (at.Name.LocalName == "race")
                    {
                        opcpde = 5;
                        if (!read_opcodes.ContainsKey(opcpde))
                        {
                            read_opcodes.Add(opcpde, at.Name.LocalName);
                        }

                        if (!race_dump.ContainsKey(at.Value))
                        {
                            race_dump.Add(at.Value, ++race_counter);
                        }

                        wr.Write(opcpde);
                        wr.Write((uint)race_dump[at.Value]);
                    }
                    else if (at.Name.LocalName == "resourceSize")
                    {
                        opcpde = 6;
                        if (!read_opcodes.ContainsKey(opcpde))
                        {
                            read_opcodes.Add(opcpde, at.Name.LocalName);
                        }
                        wr.Write(opcpde);
                        wr.Write(uint.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "resourceType")
                    {
                        opcpde = 7;
                        if (!read_opcodes.ContainsKey(opcpde))
                        {
                            read_opcodes.Add(opcpde, at.Name.LocalName);
                        }

                        if (!resource_dump.ContainsKey(at.Value))
                        {
                            resource_dump.Add(at.Value, ++resource_counter);
                        }

                        wr.Write(opcpde);
                        wr.Write((uint)resource_dump[at.Value]);
                    }
                    else if (at.Name.LocalName == "basicActionId")
                    {
                        opcpde = 8;
                        if (!read_opcodes.ContainsKey(opcpde))
                        {
                            read_opcodes.Add(opcpde, at.Name.LocalName);
                        }
                        wr.Write(opcpde);
                        wr.Write(uint.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "elite")
                    {
                        opcpde = 9;
                        if (!read_opcodes.ContainsKey(opcpde))
                        {
                            read_opcodes.Add(opcpde, at.Name.LocalName);
                        }
                        wr.Write(opcpde);
                        wr.Write(at.Value == "True" ? true : false);
                    }
                    else if (at.Name.LocalName == "invincible")
                    {
                        opcpde = 10;
                        if (!read_opcodes.ContainsKey(opcpde))
                        {
                            read_opcodes.Add(opcpde, at.Name.LocalName);
                        }
                        wr.Write(opcpde);
                        wr.Write(at.Value == "True" ? true : false);
                    }
                    else if (at.Name.LocalName == "isObjectNpc")
                    {
                        opcpde = 11;
                        if (!read_opcodes.ContainsKey(opcpde))
                        {
                            read_opcodes.Add(opcpde, at.Name.LocalName);
                        }
                        wr.Write(opcpde);
                        wr.Write(at.Value == "True" ? true : false);
                    }
                    else if (at.Name.LocalName == "unionElite")
                    {
                        opcpde = 12;
                        if (!read_opcodes.ContainsKey(opcpde))
                        {
                            read_opcodes.Add(opcpde, at.Name.LocalName);
                        }
                        wr.Write(opcpde);
                        wr.Write(at.Value == "True" ? true : false);
                    }
                    else if (at.Name.LocalName == "villager")
                    {
                        opcpde = 13;
                        if (!read_opcodes.ContainsKey(opcpde))
                        {
                            read_opcodes.Add(opcpde, at.Name.LocalName);
                        }
                        wr.Write(opcpde);
                        wr.Write(at.Value == "True" ? true : false);
                    }
                    else if (at.Name.LocalName == "isServant")
                    {
                        opcpde = 14;
                        if (!read_opcodes.ContainsKey(opcpde))
                        {
                            read_opcodes.Add(opcpde, at.Name.LocalName);
                        }
                        wr.Write(opcpde);
                        wr.Write(at.Value == "True" ? true : false);
                    }
                    else if (at.Name.LocalName == "collideOnMove")
                    {
                        opcpde = 15;
                        if (!read_opcodes.ContainsKey(opcpde))
                        {
                            read_opcodes.Add(opcpde, at.Name.LocalName);
                        }
                        wr.Write(opcpde);
                        wr.Write(at.Value == "True" ? true : false);
                    }
                    else if (at.Name.LocalName == "dontTurn")
                    {
                        opcpde = 16;
                        if (!read_opcodes.ContainsKey(opcpde))
                        {
                            read_opcodes.Add(opcpde, at.Name.LocalName);
                        }
                        wr.Write(opcpde);
                        wr.Write(at.Value == "True" ? true : false);
                    }
                    else if (at.Name.LocalName == "isFreeNamed")
                    {
                        opcpde = 17;
                        if (!read_opcodes.ContainsKey(opcpde))
                        {
                            read_opcodes.Add(opcpde, at.Name.LocalName);
                        }
                        wr.Write(opcpde);
                        wr.Write(at.Value == "True" ? true : false);
                    }
                    else if (at.Name.LocalName == "showAggroTarget")
                    {
                        opcpde = 18;
                        if (!read_opcodes.ContainsKey(opcpde))
                        {
                            read_opcodes.Add(opcpde, at.Name.LocalName);
                        }
                        wr.Write(opcpde);
                        wr.Write(at.Value == "True" ? true : false);
                    }
                    else if (at.Name.LocalName == "showShorttermTarget")
                    {
                        opcpde = 19;
                        if (!read_opcodes.ContainsKey(opcpde))
                        {
                            read_opcodes.Add(opcpde, at.Name.LocalName);
                        }
                        wr.Write(opcpde);
                        wr.Write(at.Value == "True" ? true : false);
                    }
                    else if (at.Name.LocalName == "villagerVolumeActiveRange")
                    {
                        opcpde = 20;
                        if (!read_opcodes.ContainsKey(opcpde))
                        {
                            read_opcodes.Add(opcpde, at.Name.LocalName);
                        }
                        wr.Write(opcpde);
                        wr.Write(float.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "villagerVolumeHalfHeight")
                    {
                        opcpde = 21;
                        if (!read_opcodes.ContainsKey(opcpde))
                        {
                            read_opcodes.Add(opcpde, at.Name.LocalName);
                        }
                        wr.Write(opcpde);
                        wr.Write(float.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "villagerVolumeInteractionDist")
                    {
                        opcpde = 22;
                        if (!read_opcodes.ContainsKey(opcpde))
                        {
                            read_opcodes.Add(opcpde, at.Name.LocalName);
                        }
                        wr.Write(opcpde);
                        wr.Write(float.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "villagerVolumeOffset")
                    {
                        opcpde = 23;
                        if (!read_opcodes.ContainsKey(opcpde))
                        {
                            read_opcodes.Add(opcpde, at.Name.LocalName);
                        }
                        wr.Write(opcpde);
                        wr.Write(float.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "spawnScriptId")
                    {
                        opcpde = 24;
                        if (!read_opcodes.ContainsKey(opcpde))
                        {
                            read_opcodes.Add(opcpde, at.Name.LocalName);
                        }
                        wr.Write(opcpde);
                        wr.Write(uint.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "despawnScriptId")
                    {
                        opcpde = 25;
                        if (!read_opcodes.ContainsKey(opcpde))
                        {
                            read_opcodes.Add(opcpde, at.Name.LocalName);
                        }
                        wr.Write(opcpde);
                        wr.Write(uint.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "isVehicleEx")
                    {
                        opcpde = 26;
                        if (!read_opcodes.ContainsKey(opcpde))
                        {
                            read_opcodes.Add(opcpde, at.Name.LocalName);
                        }
                        wr.Write(opcpde);
                        wr.Write(at.Value == "True" ? true : false);
                    }
                    else if (at.Name.LocalName == "parentId")
                    {
                        opcpde = 27;
                        if (!read_opcodes.ContainsKey(opcpde))
                        {
                            read_opcodes.Add(opcpde, at.Name.LocalName);
                        }
                        wr.Write(opcpde);
                        wr.Write(uint.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "isSpirit")
                    {
                        opcpde = 28;
                        if (!read_opcodes.ContainsKey(opcpde))
                        {
                            read_opcodes.Add(opcpde, at.Name.LocalName);
                        }
                        wr.Write(opcpde);
                        wr.Write(at.Value == "True" ? true : false);
                    }
                    else if (at.Name.LocalName == "unionId")
                    {
                        opcpde = 29;
                        if (!read_opcodes.ContainsKey(opcpde))
                        {
                            read_opcodes.Add(opcpde, at.Name.LocalName);
                        }
                        wr.Write(opcpde);
                        wr.Write(uint.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "cannotPassThrough")
                    {
                        opcpde = 30;
                        if (!read_opcodes.ContainsKey(opcpde))
                        {
                            read_opcodes.Add(opcpde, at.Name.LocalName);
                        }
                        wr.Write(opcpde);
                        wr.Write(at.Value == "True" ? true : false);
                    }
                    else if (at.Name.LocalName == "isRepairableSpirit")
                    {
                        opcpde = 31;
                        if (!read_opcodes.ContainsKey(opcpde))
                        {
                            read_opcodes.Add(opcpde, at.Name.LocalName);
                        }
                        wr.Write(opcpde);
                        wr.Write(at.Value == "True" ? true : false);
                    }
                    else if (at.Name.LocalName == "isHomunculus")
                    {
                        opcpde = 32;
                        if (!read_opcodes.ContainsKey(opcpde))
                        {
                            read_opcodes.Add(opcpde, at.Name.LocalName);
                        }
                        wr.Write(opcpde);
                        wr.Write(at.Value == "True" ? true : false);
                    }
                    else if (at.Name.LocalName == "isIgnoreSelect")
                    {
                        opcpde = 33;
                        if (!read_opcodes.ContainsKey(opcpde))
                        {
                            read_opcodes.Add(opcpde, at.Name.LocalName);
                        }
                        wr.Write(opcpde);
                        wr.Write(at.Value == "True" ? true : false);
                    }
                    else if (at.Name.LocalName == "pushedByNpc")
                    {
                        opcpde = 34;
                        if (!read_opcodes.ContainsKey(opcpde))
                        {
                            read_opcodes.Add(opcpde, at.Name.LocalName);
                        }
                        wr.Write(opcpde);
                        wr.Write(at.Value == "True" ? true : false);
                    }
                    else if (at.Name.LocalName == "class")
                    {
                        opcpde = 35;
                        if (!read_opcodes.ContainsKey(opcpde))
                        {
                            read_opcodes.Add(opcpde, at.Name.LocalName);
                        }

                        if (!class_dump.ContainsKey(at.Value))
                        {
                            class_dump.Add(at.Value, ++class_counter);
                        }

                        wr.Write(opcpde);
                        wr.Write((uint)class_dump[at.Value]);
                    }
                    else if (at.Name.LocalName == "isNotLocationOnFloor")
                    {
                        opcpde = 36;
                        if (!read_opcodes.ContainsKey(opcpde))
                        {
                            read_opcodes.Add(opcpde, at.Name.LocalName);
                        }
                        wr.Write(opcpde);
                        wr.Write(at.Value == "True" ? true : false);
                    }
                    else if (at.Name.LocalName == "noNamePlate")
                    {
                        opcpde = 37;
                        if (!read_opcodes.ContainsKey(opcpde))
                        {
                            read_opcodes.Add(opcpde, at.Name.LocalName);
                        }
                        wr.Write(opcpde);
                        wr.Write(at.Value == "True" ? true : false);
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
                    failedNodes.Add(nn);

                    if (!unknown_atr.Contains(at.Name.LocalName))
                    {
                        unknown_atr.Add(at.Name.LocalName);
                    }
                }
            }
            return true;
        }


        void write_name_plate(ref BinaryWriter wr, XElement e)
        {
            List<XAttribute> atr = e.Elements().ToList().Find(ell => ell.Name.LocalName == "NamePlate")?.Attributes().ToList();
            if (atr == null)
            {
                return;
            }
            wr.Write((byte)240);
            foreach (XAttribute at in atr)
            {
                if (at.Value == "")
                {
                    empty_atr.Nodes.Add(at.Name.LocalName);
                    continue;
                }
                try
                {
                    if (at.Name.LocalName == "nameplateHeight")
                    {
                        opcpde = 60;
                        if (!read_opcodes.ContainsKey(opcpde))
                        {
                            read_opcodes.Add(opcpde, at.Name.LocalName);
                        }
                        wr.Write(opcpde);
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
                    failedNodes.Add(nn);

                    if (!unknown_atr.Contains(at.Name.LocalName))
                    {
                        unknown_atr.Add(at.Name.LocalName);
                    }
                }
            }



            wr.Write((byte)241);
        }

        void write_stat(ref BinaryWriter wr, XElement e)
        {
            List<XAttribute> atr = e.Elements().ToList().Find(ell => ell.Name.LocalName == "Stat")?.Attributes().ToList();
            if (atr == null)
            {
                return;
            }


            wr.Write((byte)242);
            foreach (XAttribute at in atr)
            {
                if (at.Value == "")
                {
                    empty_atr.Nodes.Add(at.Name.LocalName);
                    continue;
                }
                try
                {
                    if (at.Name.LocalName == "level")
                    {
                        opcpde = 50;
                        if (!read_opcodes.ContainsKey(opcpde))
                        {
                            read_opcodes.Add(opcpde, at.Name.LocalName);
                        }

                        wr.Write(opcpde);
                        wr.Write(uint.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "maxHp")
                    {
                        opcpde = 51;
                        if (!read_opcodes.ContainsKey(opcpde))
                        {
                            read_opcodes.Add(opcpde, at.Name.LocalName);
                        }

                        wr.Write(opcpde);
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
                    failedNodes.Add(nn);

                    if (!unknown_atr.Contains(at.Name.LocalName))
                    {
                        unknown_atr.Add(at.Name.LocalName);
                    }
                }
            }



            wr.Write((byte)243);
        }

        void write_vehicle(ref BinaryWriter wr, XElement e)
        {
            //todo
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            read_opcodes.Clear();
            read_opcodes.Add(240, "start of nameplate");
            read_opcodes.Add(241, "end of nameplate");

            read_opcodes.Add(242, "start of stats");
            read_opcodes.Add(243, "end of stats");

            read_opcodes.Add(244, "start of vehicle");
            read_opcodes.Add(245, "end of vehicle");

            read_opcodes.Add(250, "start of template");
            read_opcodes.Add(251, "end of template");

            read_opcodes.Add(150, "local index");

            read_opcodes.Add(255, "end of file");


            race_counter = size_counter = class_counter = resource_counter = kind_counter = battle_counter = max_arguments = move_counter = 0;
            race_dump.Clear();
            size_dump.Clear();
            class_dump.Clear();
            resource_dump.Clear();
            kind_dump.Clear();
            battle_dump.Clear();
            move_dump.Clear();
            unknown_atr.Clear();
            unk_atr = new TreeNode();
            empty_atr = new TreeNode();

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
                checkBox1.Enabled = textBox1.Enabled = button1.Enabled = checkBox2.Enabled = checkBox3.Enabled = checkBox4.Enabled = true;
                return;
            }

            if (max > 512 || max == 0)
            {
                MessageBox.Show(this, "MAX value ->[1,512]!");
                textBox1.Clear();
                textBox1.Select();
                checkBox1.Enabled = textBox1.Enabled = button1.Enabled = checkBox2.Enabled = checkBox3.Enabled = checkBox4.Enabled = true;
                return;
            }

            string name = "npc_data_[" + DateTime.Now.TimeOfDay.TotalSeconds + "].bin";
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
                checkBox1.Enabled = textBox1.Enabled = button1.Enabled = checkBox2.Enabled = checkBox3.Enabled = checkBox4.Enabled = true;
                return;
            }


            BinaryWriter wr = new BinaryWriter(file);
            int fail_count = 0, item_count = 0, total_items = 0;
            XDocument document = null;
            for (int i = 0; i < max; i++)
            {
                try
                {
                    document = XDocument.Load("NpcData-" + i + ".xml");
                }
                catch (Exception ee)
                {
                    TreeNode n3 = new TreeNode("Failed to open [" + "NpcData-" + i + ".xml]");
                    n3.Nodes.Add("ERROR: " + ee.Message);
                    treeView1.Nodes.Add(n3);
                    continue;
                }

                int huntingZoneId = int.Parse(document.Root.FirstAttribute.Value);
                foreach (XElement el in document.Root.Elements())
                {
                    if (el.Name.LocalName == "Template")
                    {
                        wr.Write((byte)250);

                        wr.Write((byte)150);
                        wr.Write((uint)total_items++);

                        if (!read_opcodes.ContainsKey(100))
                        {
                            read_opcodes.Add(100, "huntingZoneId");
                        }
                        wr.Write((byte)100);
                        wr.Write((uint)huntingZoneId);

                        write_template_head(ref wr, el);
                        write_name_plate(ref wr, el);
                        write_stat(ref wr, el);
                        write_vehicle(ref wr, el);


                        wr.Write((byte)251);
                    }
                }


                TreeNode n2 = new TreeNode("Read [" + "NpcData-" + i + ".xml]");
                n2.Nodes.Add("Npc elemets[" + item_count + "]");
                n2.Nodes.Add("Non Npc elemets[" + fail_count + "]");
                if (failedNodes.Count > 0)
                {
                    n2.Nodes.Add("Unk atributes");
                    n2.Nodes[n2.Nodes.Count - 1].Nodes.AddRange(failedNodes.ToArray());
                }
                if (unk_atr != null)
                {
                    unk_atr.Text += (" [" + unk_atr.Nodes.Count + "]");
                    n2.Nodes.Add(unk_atr);
                    unk_atr = null;
                }

                treeView1.Nodes.Add(n2);
                total_items += item_count;
                fail_count = 0; item_count = 0;

            }

            wr.Write((byte)255);

            wr.Close();
            file.Close();


            var list_ = read_opcodes.Keys.ToList();
            list_.Sort();

            name += ".txt";

            using (StreamWriter wrr = new StreamWriter(name))
            {
                string line;
                foreach (int key in list_)
                {
                    line = read_opcodes[key] + "=" + key + ",";
                    wrr.WriteLine(line);
                }
            }


            if (checkBox1.Checked)
            {
                name = "npc_data_race_dump[" + DateTime.Now.TimeOfDay.TotalSeconds + "].txt";
                string line;
                using (StreamWriter wrr = new StreamWriter(name))
                {
                    foreach (string key in race_dump.Keys)
                    {
                        line = key + " = " + race_dump[key] + ",";
                        wrr.WriteLine(line);
                    }
                }
            }

            if (checkBox2.Checked)
            {
                name = "npc_data_resoruceType_dump[" + DateTime.Now.TimeOfDay.TotalSeconds + "].txt";
                string line;
                using (StreamWriter wrr = new StreamWriter(name))
                {
                    foreach (string key in resource_dump.Keys)
                    {
                        line = key + " = " + resource_dump[key] + ",";
                        wrr.WriteLine(line);
                    }
                }
            }

            if (checkBox3.Checked)
            {
                name = "npc_data_size_dump[" + DateTime.Now.TimeOfDay.TotalSeconds + "].txt";
                string line;
                using (StreamWriter wrr = new StreamWriter(name))
                {
                    foreach (string key in size_dump.Keys)
                    {
                        line = key + " = " + size_dump[key] + ",";
                        wrr.WriteLine(line);
                    }
                }
            }

            if (checkBox7.Checked)
            {
                name = "npc_data_class_dump[" + DateTime.Now.TimeOfDay.TotalSeconds + "].txt";
                string line;
                using (StreamWriter wrr = new StreamWriter(name))
                {
                    foreach (string key in class_dump.Keys)
                    {
                        line = key + " = " + class_dump[key] + ",";
                        wrr.WriteLine(line);
                    }
                }
            }






            TreeNode n = new TreeNode("Read [" + name + ".xml]");
            n.Nodes.Add("Npc Data elemets[" + item_count + "]");
            n.Nodes.Add("Non Npc Data elemets[" + fail_count + "]");
            treeView1.Nodes.Add("Unknown Npc Data Node Attributes [" + unknown_atr.Count + "]");
            foreach (string s in unknown_atr)
                treeView1.Nodes[treeView1.Nodes.Count - 1].Nodes.Add(s);

            treeView1.Nodes.Add("Total Npc Data read[" + total_items + "]");
            treeView1.Nodes.Add("By Narcis96, Enjoy!");
        }
    }
}
