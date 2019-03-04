using System;
using System.Collections.Generic;
using System.Windows.Forms;
using assembly.Model;
using assembly.Enums;
using assembly.Data;

namespace assembly
{
    public partial class frmAssemblyTraducer : Form
    {
        Dictionary<string, Instruction> encoding;
        List<string[]> code;
        Dictionary<string, int> labels;
        SortedDictionary<string, int> variables;
        SortedDictionary<int, int> constants;
        List<string> finalVHDL;
        int sizeBits;

        public frmAssemblyTraducer(string txt, int separatorIndx)
        {
            InitializeComponent();

            encoding = new Dictionary<string, Instruction>();
            foreach (Instruction instruction in DataProvider.INSTRUCTIONS)
            {
                encoding.Add(instruction.Name, instruction);
            }

            txInput.Lines = txt.Split('\n');
            cbSeparator.SelectedIndex = separatorIndx;
        }
        public frmAssemblyTraducer()
        {
            InitializeComponent();

            encoding = new Dictionary<string, Instruction>();
            foreach (Instruction instruction in DataProvider.INSTRUCTIONS)
            {
                encoding.Add(instruction.Name, instruction);
            }

            cbBits.SelectedIndex = 0;
            cbSeparator.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txInput.ScrollBars = ScrollBars.Both;
            cbBits.SelectedIndex = 0;
        }
        private void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessCode();
                GenerateVHDL();
                ShowOutput();
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }
        void ProcessCode()
        {
            initialize();

            char separator = GetSeparator();
            sizeBits = GetBits();
            int pos_rom = 0;
            int lineCount = 1;
            int accumulatedLenght = 0;
            foreach (string line in txInput.Lines)
            {
                if (line.Length > 0)
                {
                    string[] lineComponents = line.Split(separator);
                    if (lineComponents.Length >= 3 && lineComponents[1].Length > 0)
                    {
                        AddLabel(lineComponents, pos_rom);
                        AddOperand(lineComponents, pos_rom);
                        code.Add(lineComponents);
                        pos_rom++;
                    }
                    else
                    {
                        txInput.SelectionStart = accumulatedLenght;
                        txInput.SelectionLength = line.Length;
                        throw new Exception("Inconsistency at the line " + lineCount.ToString());
                    }
                }
                accumulatedLenght += line.Length + 1;
                lineCount++;
            }
        }
        void ShowErrorMessage(string e)
        {
            MessageBox.Show("An error has occurred:\n" + e.ToString(), "Assembly traducer", MessageBoxButtons.OK, MessageBoxIcon.Error);

            txInput.Focus();
        }
        void initialize()
        {
            code = new List<string[]>();
            labels = new Dictionary<string, int>();
            variables = new SortedDictionary<string, int>();
            constants = new SortedDictionary<int, int>();
            finalVHDL = new List<string>();
            finalVHDL.Add("signal ROM : rom_mem_type:=(");
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
        int GetBits()
        {
            if (cbBits.SelectedItem == null)
            {
                cbBits.SelectedIndex = 0;
            }
            return Convert.ToInt32(cbBits.SelectedItem);
        }
        void AddLabel(string[] lineComponents, int pos_rom)
        {
            if (lineComponents[0].Length != 0 && !labels.ContainsKey(lineComponents[0]))
            {
                labels.Add(lineComponents[0], pos_rom);
            }
        }
        void AddOperand(string[] lineComponents, int pos_rom)
        {
            if (lineComponents.Length >= 3 && lineComponents[2].Length > 0)
            {
                AddContstantOrVariable(lineComponents[2], pos_rom);
            }
        }
        void AddContstantOrVariable(string value, int pos_rom)
        {
            try
            {
                int n = Convert.ToInt32(value);
                if(!constants.ContainsKey(n))
                    constants.Add(n, pos_rom);
            }
            catch
            {
                if (!variables.ContainsKey(value))
                    variables.Add(value, pos_rom);
            }
        }
        void GenerateVHDL()
        {
            int pc = 0;
            foreach (string[] line in code)
            {
                Instruction content = encoding[line[1]];
                string vhdlLine = content.Value;
                if (content.Type == TypeOfInstruction.PROGRAM)
                {
                    vhdlLine += FromPositionToHex(labels[line[2]]);
                }
                else if (content.Type == TypeOfInstruction.DATA)
                {
                    int value = getContstantOrVariable(line[2]);
                    vhdlLine += FromPositionToHex(value);
                }
                finalVHDL.Add(String.Format("{0} => X\"{1}\",", pc, vhdlLine));
                pc++;
            }
            finalVHDL.Add(String.Format("others => x\"000\");"));

            finalVHDL.Add(String.Format("//"));

            finalVHDL.Add(String.Format("signal RAM : ram_mem_type:=("));

            foreach (KeyValuePair<int, int> constantData in constants)
            {
                string hexValue = FromConstantToHex(constantData.Key, sizeBits);
                finalVHDL.Add(String.Format(string.Format("{0} => X\"{1}\",", constantData.Value, hexValue)));
            }

            finalVHDL.Add(String.Format(string.Format("others => X\"{0}\");", FromConstantToHex(0, sizeBits))));
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
        string FromConstantToHex(int value, int sizeBits)
        {
            int size = sizeBits / 4;
            return value.ToString("X").PadLeft(size, '0').ToUpper();
        }

        void ShowOutput()
        {
            Hide();
            new frmOutput(finalVHDL.ToArray()).ShowDialog();
            Show();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

}
