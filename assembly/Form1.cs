using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace assembly
{
    public partial class Form1 : Form
    {
        Dictionary<string, Instruction> encoding;
        List<string[]> code;
        Dictionary<string, int> labels;
        Dictionary<string, int> variables;
        Dictionary<int, int> constants;
        string finalVHDL;
        int sizeBits;

        public Form1(string txt, int separatorIndx) {
            InitializeComponent();
            encoding = new Dictionary<string, Instruction>();

            Instruction defaultInstruction;
            defaultInstruction = new Instruction("loadA", "0", TypeOfInstruction.DATA);
            encoding.Add(defaultInstruction.Name, defaultInstruction);
            defaultInstruction = new Instruction("storeA", "1", TypeOfInstruction.DATA);
            encoding.Add(defaultInstruction.Name, defaultInstruction);
            defaultInstruction = new Instruction("addA", "2", TypeOfInstruction.DATA);
            encoding.Add(defaultInstruction.Name, defaultInstruction);
            defaultInstruction = new Instruction("subA", "3", TypeOfInstruction.DATA);
            encoding.Add(defaultInstruction.Name, defaultInstruction);
            defaultInstruction = new Instruction("inA", "400", TypeOfInstruction.IO);
            encoding.Add(defaultInstruction.Name, defaultInstruction);
            defaultInstruction = new Instruction("outA", "500", TypeOfInstruction.IO);
            encoding.Add(defaultInstruction.Name, defaultInstruction);
            defaultInstruction = new Instruction("jpos", "6", TypeOfInstruction.PROGRAM);
            encoding.Add(defaultInstruction.Name, defaultInstruction);
            defaultInstruction = new Instruction("jneg", "7", TypeOfInstruction.PROGRAM);
            encoding.Add(defaultInstruction.Name, defaultInstruction);
            defaultInstruction = new Instruction("jz", "8", TypeOfInstruction.PROGRAM);
            encoding.Add(defaultInstruction.Name, defaultInstruction);
            defaultInstruction = new Instruction("jnz", "9", TypeOfInstruction.PROGRAM);
            encoding.Add(defaultInstruction.Name, defaultInstruction);
            defaultInstruction = new Instruction("jmp", "A", TypeOfInstruction.PROGRAM);
            encoding.Add(defaultInstruction.Name, defaultInstruction);
            defaultInstruction = new Instruction("halt", "B00", TypeOfInstruction.IO);
            encoding.Add(defaultInstruction.Name, defaultInstruction);

            txInput.Lines = txt.Split('\n');
            cbBits.SelectedIndex = 0;
            cbSeparator.SelectedIndex = separatorIndx;
        }
        public Form1()
        {
            InitializeComponent();
            encoding = new Dictionary<string, Instruction>();

            Instruction defaultInstruction;
            defaultInstruction = new Instruction("loadA", "0", TypeOfInstruction.DATA);
            encoding.Add(defaultInstruction.Name, defaultInstruction);
            defaultInstruction = new Instruction("storeA", "1", TypeOfInstruction.DATA);
            encoding.Add(defaultInstruction.Name, defaultInstruction);
            defaultInstruction = new Instruction("addA", "2", TypeOfInstruction.DATA);
            encoding.Add(defaultInstruction.Name, defaultInstruction);
            defaultInstruction = new Instruction("subA", "3", TypeOfInstruction.DATA);
            encoding.Add(defaultInstruction.Name, defaultInstruction);
            defaultInstruction = new Instruction("inA", "400", TypeOfInstruction.IO);
            encoding.Add(defaultInstruction.Name, defaultInstruction);
            defaultInstruction = new Instruction("outA", "500", TypeOfInstruction.IO);
            encoding.Add(defaultInstruction.Name, defaultInstruction);
            defaultInstruction = new Instruction("jpos", "6", TypeOfInstruction.PROGRAM);
            encoding.Add(defaultInstruction.Name, defaultInstruction);
            defaultInstruction = new Instruction("jneg", "7", TypeOfInstruction.PROGRAM);
            encoding.Add(defaultInstruction.Name, defaultInstruction);
            defaultInstruction = new Instruction("jz", "8", TypeOfInstruction.PROGRAM);
            encoding.Add(defaultInstruction.Name, defaultInstruction);
            defaultInstruction = new Instruction("jnz", "9", TypeOfInstruction.PROGRAM);
            encoding.Add(defaultInstruction.Name, defaultInstruction);
            defaultInstruction = new Instruction("jmp", "A", TypeOfInstruction.PROGRAM);
            encoding.Add(defaultInstruction.Name, defaultInstruction);
            defaultInstruction = new Instruction("halt", "B00", TypeOfInstruction.IO);
            encoding.Add(defaultInstruction.Name, defaultInstruction);

            cbBits.SelectedIndex = 0;
            cbSeparator.SelectedIndex = 0;
        }
        private void btnGo_Click(object sender, EventArgs e)
        {
            ProcessCode();
            GenerateVHDL();
            ShowOutput();
        }
        void ProcessCode() {
            initialize();

            char separator = GetSeparator();
            sizeBits = GetBits();
            int pos_rom = 0;
            foreach (string line in txInput.Lines)
            {
                string[] lineComponents = line.Split(separator);
                AddLabel(lineComponents, pos_rom);
                AddOperand(lineComponents, pos_rom);
                code.Add(lineComponents);
                pos_rom++;
            }
        }
        void initialize()
        {
            code = new List<string[]>();
            labels = new Dictionary<string, int>();
            variables = new Dictionary<string, int>();
            constants = new Dictionary<int, int>();

            finalVHDL = "signal ROM : rom_mem_type:=(\n";
        }
        char GetSeparator()
        {
            if (cbSeparator.SelectedItem == null)
            {
                cbSeparator.SelectedIndex = 0;
            }
            switch (cbSeparator.SelectedIndex)
            {
                case 0: return '\t';
                case 1: return ';';
                case 2: return ',';
            }

            return '\t';
        }
        int GetBits() {
            if (cbBits.SelectedItem == null) {
                cbBits.SelectedIndex = 0;
            }
            return Convert.ToInt32(cbBits.SelectedItem);
        }
        void AddLabel(string[] lineComponents, int pos_rom) {
            if (lineComponents[0].Length != 0)
            {
                try
                {
                    labels.Add(lineComponents[0], pos_rom);
                }
                catch { }
            }
        }
        void AddOperand(string[] lineComponents, int pos_rom)
        {
            if (lineComponents.Length >=3 && lineComponents[2].Length > 0)
            {
                if (Char.IsDigit(lineComponents[2][0]))
                {
                    AddContstantOrVariable(lineComponents[2], pos_rom);
                }
                else
                {
                    try
                    {
                        variables.Add(lineComponents[2], pos_rom);
                    }
                    catch { }
                }
            }
        }
        void AddContstantOrVariable(string value, int pos_rom) {
            try
            {
                int n = Convert.ToInt32(value);
                try
                {
                    constants.Add(n, pos_rom);
                }
                catch { }
            }
            catch
            {
                try
                {
                    variables.Add(value, pos_rom);
                }
                catch { }
            }
        }
        void GenerateVHDL() {
            int pc = 0;
            foreach (string[] line in code) {
                if (line.Length < 3) MessageBox.Show(line.Length.ToString());
                Instruction content = encoding[line[1]];
                string vhdlLine = content.Value;
                if (content.Type == TypeOfInstruction.PROGRAM) {
                    vhdlLine += FromPositionToHex(labels[ line[2]]);
                }
                else if (content.Type == TypeOfInstruction.DATA)
                {
                    int value = getContstantOrVariable(line[2]);
                    vhdlLine += FromPositionToHex(value);
                }
                finalVHDL += String.Format("{0} => X\"{1}\",\n", pc, vhdlLine);
                pc++;
            }
            finalVHDL += "others => x\"000\");\n";

            finalVHDL += "//\n";

            finalVHDL += "signal RAM : ram_mem_type:=(\n";

            foreach (KeyValuePair<int,int> constantData in constants) {
                string hexValue = FromConstantToHex(constantData.Key, sizeBits);
                finalVHDL += String.Format("{0} => X\"{1}\",\n", constantData.Value, hexValue);
            }

            finalVHDL += String.Format("others => X\"{0}\");", FromConstantToHex(0, sizeBits));
        }
        string FromPositionToHex(int value)
        {
            return value.ToString("X").PadLeft(2, '0').ToUpper();
        }
        int getContstantOrVariable(string key)
        {
            try
            {
                return constants[Convert.ToInt32(key)];
            }
            catch
            {
                return variables[key];
            }
        }
        string FromConstantToHex(int value, int sizeBits) {
            int size = sizeBits / 4;
            return value.ToString("X").PadLeft(size, '0').ToUpper();
        }

        void ShowOutput() {
            new frmOutput(finalVHDL).ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txInput.ScrollBars = ScrollBars.Both;
        }
    }
    enum TypeOfInstruction
    {
        PROGRAM,
        DATA,
        IO
    }
    class Instruction
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public TypeOfInstruction Type { get; set; }

        public Instruction(string name, string value, TypeOfInstruction type)
        {
            Name = name;
            Value = value;
            Type = type;
        }
    }
}
