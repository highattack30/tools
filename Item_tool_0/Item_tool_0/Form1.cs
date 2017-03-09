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
using System.Xml;
using System.Xml.Linq;

namespace Item_tool_0
{
    public partial class Form1 : Form
    {
        int max_arguments = 0;
        uint max_id = 0;
        int category_count = 0;
        int required_count = 0;
        int combat_count = 0;
        int bound_count = 0;
        byte opcode = 0;
        TreeNode unk_atr = null;
        Dictionary<string, int> categories = new Dictionary<string, int>();
        Dictionary<string, int> required = new Dictionary<string, int>();
        Dictionary<string, int> combat = new Dictionary<string, int>();
        Dictionary<string, int> bound = new Dictionary<string, int>();
        Dictionary<int, string> read_opcodes = new Dictionary<int, string>();

        Dictionary<string, int> race_dictionary = new Dictionary<string, int>();
        Dictionary<string, int> class_disctionary = new Dictionary<string, int>();

        List<string> unknown_atr = new List<string>();
        public Form1()
        {
            InitializeComponent();


            read_opcodes.Add(250, "start of item");
            read_opcodes.Add(251, "end of item");
            read_opcodes.Add(255, "end of file");

            race_dictionary.Add("human", 0);
            race_dictionary.Add("highelf", 0);
            race_dictionary.Add("aman", 0);
            race_dictionary.Add("castanic", 0);
            race_dictionary.Add("elin", 0);
            race_dictionary.Add("popori", 0);
            race_dictionary.Add("baraka", 0);


            class_disctionary.Add("WARRIOR", 0);
            class_disctionary.Add("LANCER", 1);
            class_disctionary.Add("SLAYER", 2);
            class_disctionary.Add("BERSERKER", 3);
            class_disctionary.Add("SORCERER", 4);
            class_disctionary.Add("ARCHER", 5);
            class_disctionary.Add("PRIEST", 6);
            class_disctionary.Add("ELEMENTALIST", 7);
            class_disctionary.Add("MYSTIC", 7);
            class_disctionary.Add("SOULLESS", 8);
            class_disctionary.Add("REAPER", 8);
            class_disctionary.Add("ENGINEER", 9);
            class_disctionary.Add("FIGHTER", 10);
            class_disctionary.Add("ASSASSIN", 11);
        }
        TreeNode empty_atr;
        bool write_to_file(ref BinaryWriter wr, XElement e)
        {

            if (e.Name != "Item")
                return false;

            empty_atr = new TreeNode("Total empty Node Attributes");
            wr.Write((byte)250);
            unk_atr = new TreeNode("Unknow Node Attributes");
            List<XAttribute> atr = e.Attributes().ToList();
            if (atr.Count > max_arguments)
                max_arguments = atr.Count;

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
                    else if (at.Name.LocalName == "coolTime")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 1;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)1);
                        wr.Write(uint.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "category")
                    {
                        if (!categories.ContainsKey(at.Value))
                        {
                            categories.Add(at.Value, ++category_count);
                        }

                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 2;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)2);
                        wr.Write((uint)categories[at.Value]);

                    }
                    else if (at.Name.LocalName == "level")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 3;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)3);
                        wr.Write((uint)uint.Parse(at.Value));
                    }

                    else if (at.Name.LocalName == "rank")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 4;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)4);
                        wr.Write((uint)uint.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "maxStack")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 5;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)5);
                        wr.Write((uint)uint.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "rareGrade")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 6;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)6);
                        wr.Write(uint.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "artisanable")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 7;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)7);
                        wr.Write(at.Value == "True" ? true : false);
                    }
                    else if (at.Name.LocalName == "requiredEquipmentType")
                    {
                        if (!required.ContainsKey(at.Value))
                        {
                            required.Add(at.Value, ++required_count);
                        }

                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 8;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)8);
                        wr.Write((uint)required[at.Value]);

                    }
                    else if (at.Name.LocalName == "combatItemType")
                    {
                        if (!combat.ContainsKey(at.Value))
                            combat.Add(at.Value, ++combat_count);

                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 9;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }

                        wr.Write((byte)9);
                        wr.Write((uint)combat[at.Value]);

                    }
                    else if (at.Name.LocalName == "boundType")
                    {
                        if (!bound.ContainsKey(at.Value))
                            bound.Add(at.Value, ++bound_count);

                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 10;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)10);
                        wr.Write((uint)bound[at.Value]);

                    }
                    else if (at.Name.LocalName == "buyPrice")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 11;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)11);
                        wr.Write(float.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "changeColorEnable")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 12;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)12);
                        wr.Write(at.Value == "True" ? true : false);
                    }
                    else if (at.Name.LocalName == "changeLook")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 13;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)13);
                        wr.Write(at.Value == "True" ? true : false);
                    }
                    else if (at.Name.LocalName == "coolTimeGroup")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 14;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)14);
                        wr.Write((uint)uint.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "destroyable")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 15;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)15);
                        wr.Write(at.Value == "True" ? true : false);
                    }
                    else if (at.Name.LocalName == "dismantlable")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 16;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)16);
                        wr.Write(at.Value == "True" ? true : false);
                    }
                    else if (at.Name.LocalName == "enchantEnable")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 17;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)17);
                        wr.Write(at.Value == "True" ? true : false);
                    }
                    else if (at.Name.LocalName == "extractLook")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 18;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)18);
                        wr.Write(at.Value == "True" ? true : false);
                    }
                    else if (at.Name.LocalName == "guildWarehouseStorable")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 19;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)19);
                        wr.Write(at.Value == "True" ? true : false);
                    }
                    else if (at.Name.LocalName == "tradable")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 20;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)20);
                        wr.Write(at.Value == "True" ? true : false);
                    }
                    else if (at.Name.LocalName == "linkCrestId")
                    {
                        if (at.Value.Contains(';'))
                        {
                            MessageBox.Show("linkCrestId contains ;");
                        }
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 21;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)21);
                        wr.Write((uint)uint.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "linkCustomizingId")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 22;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)22);
                        wr.Write((uint)uint.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "linkEquipmentId")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 23;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)23);
                        wr.Write((uint)uint.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "linkLookInfoId")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 24;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)24);
                        wr.Write(uint.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "linkPetAdultId")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 25;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)25);
                        wr.Write(uint.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "linkPassivityCategoryId")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 26;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)26);
                        if (at.Value.Contains(';'))
                        {

                            var t = at.Value.Split(';');
                            wr.Write((byte)t.Length);
                            foreach (string i in t)
                            {
                                if (i != "" && i != ";")
                                    wr.Write(uint.Parse(i));
                            }

                        }
                        else
                        {
                            wr.Write((byte)0x01);
                            wr.Write(uint.Parse(at.Value));
                        }
                    }
                    else if (at.Name.LocalName == "linkPetOrbId")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 27;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)27);
                        wr.Write((uint)uint.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "linkSkillId")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 28;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        if (at.Value.Contains(';'))
                        {
                            MessageBox.Show("linkSkillId contains ;");
                        }

                        wr.Write((byte)28);
                        wr.Write((uint)uint.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "masterpieceRate")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 29;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)29);
                        wr.Write(float.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "obtainable")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 30;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)30);
                        wr.Write(at.Value == "True" ? true : false);
                    }
                    else if (at.Name.LocalName == "sellPrice")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 31;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)31);
                        wr.Write(float.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "slotLimit")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 32;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)32);
                        wr.Write((uint)uint.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "sortingNumber")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 33;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)33);
                        wr.Write((uint)uint.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "storeSellable")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 34;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)34);
                        wr.Write(at.Value == "True" ? true : false);
                    }
                    else if (at.Name.LocalName == "unidentifiedItemGrade")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 35;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)35);
                        wr.Write(uint.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "warehouseStorable")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 36;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)36);
                        wr.Write(at.Value == "True" ? true : false);

                    }
                    else if (at.Name.LocalName == "requiredLevel")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 37;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)37);
                        wr.Write(uint.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "requiredClass")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 38;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)38);
                        if (at.Value.Contains(';'))
                        {
                            string[] values = at.Value.Split(';');
                            byte count = 0;
                            foreach (string i in values)
                                if (i != "" && i != ";")
                                    count++;
                            wr.Write(count);
                            for (uint i = 0; i < values.Length; i++)
                                wr.Write((uint)class_disctionary[values[i].ToUpper()]);
                        }
                        else
                        {
                            wr.Write((byte)1);
                            wr.Write((uint)class_disctionary[at.Value.ToUpper()]);
                        }
                    }
                    else if (at.Name.LocalName == "requiredGender")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 39;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)39);
                        wr.Write((byte)(at.Value == "male" ? 0x00 : 0x01));
                    }
                    else if (at.Name.LocalName == "requiredRace")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 40;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)40);
                        if (at.Value.Contains(';'))
                        {
                            string[] values = at.Value.Split(';');
                            byte count = 0;
                            foreach (string i in values)
                                if (i != "" && i != ";")
                                    count++;
                            wr.Write(count);
                            for (uint i = 0; i < values.Length; i++)
                                wr.Write((uint)race_dictionary[values[i].ToLower()]);
                        }
                        else
                        {
                            wr.Write((byte)1);
                            wr.Write((uint)race_dictionary[at.Value.ToLower()]);
                        }

                    }
                    else if (at.Name.LocalName == "linkPassivityId")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 41;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)41);
                        if (at.Value.Contains(';'))
                        {
                            var t = at.Value.Split(';');
                            byte count = 0;
                            foreach (string i in t)
                                if (i != "" && i != ";")
                                    count++;
                            wr.Write(count);
                            foreach (string i in t)
                            {
                                if (i != "" && i != ";")
                                    wr.Write(uint.Parse(i));
                            }

                        }
                        else
                        {
                            wr.Write((byte)0x01);
                            wr.Write(uint.Parse(at.Value));
                        }
                    }
                    else if (at.Name.LocalName == "linkMasterpiecePassivityId")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 42;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)42);
                        if (at.Value.Contains(';'))
                        {

                            var t = at.Value.Split(';');
                            byte count = 0;
                            foreach (string i in t)
                                if (i != "" && i != ";")
                                    count++;
                            wr.Write(count);
                            foreach (string i in t)
                            {
                                if (i != "" && i != ";")
                                    wr.Write(uint.Parse(i));
                            }

                        }
                        else
                        {
                            wr.Write((byte)0x01);
                            wr.Write(uint.Parse(at.Value));
                        }
                    }
                    else if (at.Name.LocalName == "linkMasterpiecePassivityCategoryId")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 43;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)43);
                        if (at.Value.Contains(';'))
                        {

                            var t = at.Value.Split(';');
                            byte count = 0;
                            foreach (string i in t)
                                if (i != "" && i != ";")
                                    count++;
                            wr.Write(count);
                            foreach (string i in t)
                            {
                                if (i != "" && i != ";")
                                    wr.Write(uint.Parse(i));
                            }

                        }
                        else
                        {
                            wr.Write((byte)0x01);
                            wr.Write(uint.Parse(at.Value));
                        }
                    }
                    else if (at.Name.LocalName == "linkMasterpieceEnchantId")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 44;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)44);
                        wr.Write(uint.Parse(at.Value));
                    }
                    else if (at.Name.LocalName == "linkEnchantId")
                    {
                        if (!read_opcodes.ContainsValue(at.Name.LocalName))
                        {
                            opcode = 45;
                            read_opcodes.Add(opcode, at.Name.LocalName);
                        }
                        wr.Write((byte)45);
                        wr.Write(uint.Parse(at.Value));
                    }
                    //else if (at.Name.LocalName == "masterpieceBasicStatRevise")
                    //{
                    //    if (!read_opcodes.ContainsValue(at.Name.LocalName))
                    //    {
                    //        opcode = 46;
                    //        read_opcodes.Add(opcode, at.Name.LocalName);
                    //    }
                    //    wr.Write((byte)46);
                    //    if (at.Value.Contains(';'))
                    //    {
                    //        var t = at.Value.Split(';');
                    //        wr.Write((byte)t.Length);
                    //        foreach (string i in t)
                    //        {
                    //            wr.Write(uint.Parse(i));
                    //        }
                    //    }
                    //    else
                    //    {
                    //        wr.Write((byte)0x00);
                    //        wr.Write(uint.Parse(at.Value));
                    //    }
                    //}
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
            max_arguments =  category_count = required_count = combat_count = bound_count = 0;
            max_id = 0;
            categories.Clear();
            required.Clear();
            combat.Clear();
            bound.Clear();
            read_opcodes.Clear();
            unknown_atr.Clear();
            empty_atr= unk_atr= null;


            checkBox1.Enabled = textBox1.Enabled = button1.Enabled = checkBox2.Enabled = checkBox3.Enabled = checkBox4.Enabled = false;
            treeView1.Nodes.Clear();

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
            string name = "item_data_[" + DateTime.Now.TimeOfDay.TotalSeconds + "].bin";

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
                    document = XDocument.Load("ItemData-" + i + ".xml");
                    foreach (XElement el in document.Root.Elements())
                    {
                        if (!write_to_file(ref wr, el))
                            fail_count++;
                        else item_count++;
                    }


                    TreeNode n = new TreeNode("Read [" + "ItemData-" + i + ".xml]");
                    n.Nodes.Add("Item elemets[" + item_count + "]");
                    n.Nodes.Add("Non Item elemets[" + fail_count + "]");

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
                    TreeNode n = new TreeNode("Failed to open [" + "ItemData-" + i + ".xml]");
                    n.Nodes.Add("ERROR: " + ee.Message);
                    treeView1.Nodes.Add(n);
                }
            }
            treeView1.Nodes.Add("Max Item Id Value[" + max_id + "]");

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

            if (checkBox1.Checked)
            {
                name = "categories_dump_[" + DateTime.Now.TimeOfDay.TotalSeconds + "].txt";
                using (StreamWriter wrr = new StreamWriter(name))
                {
                    foreach (string key in categories.Keys)
                    {
                        line = key + " = " + categories[key] + ",";
                        wrr.WriteLine(line);
                    }
                }
            }

            if (checkBox2.Checked)
            {
                name = "combat_item_type_dump_[" + DateTime.Now.TimeOfDay.TotalSeconds + "].txt";
                using (StreamWriter wrr = new StreamWriter(name))
                {
                    foreach (string key in combat.Keys)
                    {
                        line = key + " = " + combat[key] + ",";
                        wrr.WriteLine(line);
                    }
                }
            }

            if (checkBox3.Checked)
            {
                name = "required_type_dump_[" + DateTime.Now.TimeOfDay.TotalSeconds + "].txt";

                using (StreamWriter wrr = new StreamWriter(name))
                {
                    foreach (string key in required.Keys)
                    {
                        line = key + " = " + required[key] + ",";
                        wrr.WriteLine(line);
                    }
                }
            }

            if (checkBox4.Checked)
            {
                name = "bind_type_dump_[" + DateTime.Now.TimeOfDay.TotalSeconds + "].txt";
                using (StreamWriter wrr = new StreamWriter(name))
                {
                    foreach (string key in bound.Keys)
                    {
                        line = key + " = " + bound[key] + ",";
                        wrr.WriteLine(line);
                    }
                }
            }


            treeView1.Nodes.Add("Unknown Item Node Attributes [" + unknown_atr.Count + "]");
            foreach (string s in unknown_atr)
                treeView1.Nodes[treeView1.Nodes.Count - 1].Nodes.Add(s);

            treeView1.Nodes.Add("Total Items read[" + total_items + "]");
            treeView1.Nodes.Add("By Narcis96, Enjoy!");
            checkBox1.Enabled = textBox1.Enabled = button1.Enabled = checkBox2.Enabled = checkBox3.Enabled = checkBox4.Enabled = true;
        }
    }
}
