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
    public partial class frmTransformPseudoCode : Form
    {
        char separator;
        int countLabel;
        string finalAssembly;
        Stack<string> assemblySequence;
        Dictionary<String, String> encodingFormat;
        Stack<String[]> code;

        public frmTransformPseudoCode()
        {
            InitializeComponent();
            encodingFormat = new Dictionary<string, string>();

            encodingFormat.Add("input",
                "{2}{0}inA{0}\n" +
                "{0}storeA{0}{1}\n");
            encodingFormat.Add("output",
                "{2}{0}loadA{0}{1}\n" +
                "{0}outA{0}\n");
            encodingFormat.Add("end",
                "{1}{0}halt{0}");

            encodingFormat.Add("=-",
                "{4}{0}loadA{0}{2}\n" +
                "{0}subA{0}{3}\n" +
                "{0}storeA{0}{1}\n");
            encodingFormat.Add("=+",
                "{4}{0}loadA{0}{2}\n" +
                "{0}addA{0}{3}\n" +
                "{0}storeA{0}{1}\n");
            encodingFormat.Add("=",
                "{3}{0}loadA{0}{2}\n" +
                "{0}storeA{0}{1}\n");

            encodingFormat.Add("onlyif>=",
                "{4}{0}loadA{0}{1}\n" +
                "{0}subA{0}{2}\n" +
                "{0}jneg{0}{3}\n");
            encodingFormat.Add("onlyif>",
                "{4}{0}loadA{0}{1}\n" +
                "{0}subA{0}{2}\n" +
                "{0}jneg{0}{3}\n" +
                "{0}jz{0}{3}\n");
            encodingFormat.Add("onlyif==",
                "{4}{0}loadA{0}{1}\n" +
                "{0}subA{0}{2}\n" +
                "{0}jnz{0}{3}\n");
            encodingFormat.Add("onlyif!=",
                "{4}{0}loadA{0}{1}\n" +
                "{0}subA{0}{2}\n" +
                "{0}jz{0}{3}\n");
            encodingFormat.Add("onlyif<=",
                "{4}{0}loadA{0}{1}\n" +
                "{0}subA{0}{2}\n" +
                "{0}jpos{0}{3}\n");
            encodingFormat.Add("onlyif<",
                "{4}{0}loadA{0}{1}\n" +
                "{0}subA{0}{2}\n" +
                "{0}jpos{0}{3}\n" +
                "{0}jz{0}{3}\n");

            encodingFormat.Add("if>=",
                "{4}{0}loadA{0}{1}\n" +
                "{0}subA{0}{2}\n" +
                "{0}jneg{0}else{3}\n");
            encodingFormat.Add("if>",
                "{4}{0}loadA{0}{1}\n" +
                "{0}subA{0}{2}\n" +
                "{0}jneg{0}else{3}\n" +
                "{0}jz{0}else{3}\n");
            encodingFormat.Add("if==",
                "{4}{0}loadA{0}{1}\n" +
                "{0}subA{0}{2}\n" +
                "{0}jnz{0}else{3}\n");
            encodingFormat.Add("if!=",
                "{4}{0}loadA{0}{1}\n" +
                "{0}subA{0}{2}\n" +
                "{0}jz{0}else{3}\n");
            encodingFormat.Add("if<=",
                "{4}{0}loadA{0}{1}\n" +
                "{0}subA{0}{2}\n" +
                "{0}jpos{0}else{3}\n");
            encodingFormat.Add("if<",
                "{4}{0}loadA{0}{1}\n" +
                "{0}subA{0}{2}\n" +
                "{0}jpos{0}else{3}\n" +
                "{0}jz{0}else{3}\n");

            encodingFormat.Add("else",
                "{0}jmp{0}{1}\n");


            encodingFormat.Add("while>=",
                "{4}{0}loadA{0}{1}\n" +
                "{0}subA{0}{2}\n" +
                "{0}jneg{0}{3}\n");
            encodingFormat.Add("while>",
                "{4}{0}loadA{0}{1}\n" +
                "{0}subA{0}{2}\n" +
                "{0}jneg{0}{3}\n" +
                "{0}jz{0}{3}\n");
            encodingFormat.Add("while==",
                "{4}{0}loadA{0}{1}\n" +
                "{0}subA{0}{2}\n" +
                "{0}jnz{0}{3}\n");
            encodingFormat.Add("while!=",
                "{4}{0}loadA{0}{1}\n" +
                "{0}subA{0}{2}\n" +
                "{0}jz{0}{3}\n");
            encodingFormat.Add("while<=",
                "{4}{0}loadA{0}{1}\n" +
                "{0}subA{0}{2}\n" +
                "{0}jpos{0}{3}\n");
            encodingFormat.Add("while<",
                "{4}{0}loadA{0}{1}\n" +
                "{0}subA{0}{2}\n" +
                "{0}jpos{0}{3}\n" +
                "{0}jz{0}{3}\n");


            encodingFormat.Add("endwhile",
                "{2}{0}jmp{0}{1}\n");


            cbSeparator.SelectedIndex = 0;
        }
        private void btnGo_Click(object sender, EventArgs e)
        {
            separator = GetSeparator();
            ProcessCode();
            GenerateAssembly();
            Hide();
            new Form1(GetAssemblyCode(), cbSeparator.SelectedIndex).ShowDialog();
            Show();
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
        void ProcessCode()
        {
            Initialize();
            foreach (string line in txInput.Lines)
            {
                string reducedLine = line
                    .Replace(";", "").Replace(" ( ", " ").Replace(" (", " ").Replace("( ", " ").Replace(" ) ", " ")
                    .Replace(" )", " ").Replace(") ", " ").Replace("(", " ").Replace(")", " ").Trim();
                if (line.Length > 0) {
                    string[] lineComponents = reducedLine.Split(' ');
                    code.Push(lineComponents);
                }
            
            }
        }
        void Initialize()
        {
            countLabel = 0;
            finalAssembly = "";
            code = new Stack<string[]>();
            assemblySequence = new Stack<string>();
        }
        void GenerateAssembly() {
            string[] line;
            Stack<Label> labels = new Stack<Label>();
            Stack<string[]> lastLines = new Stack<string[]>();
            string lastLabel = "";
            string relationalLabel;
            Label curren;
            string[] next;
            bool isLast = false;
            while (code.Count > 0) {
                relationalLabel = "";
                line = code.Pop();
                switch (line[0]) {
                    case "input":
                        if (code.Count > 0)
                        {
                            next = code.Peek();
                            if (next[0] == "else" || next[0] == "endif" || next[0] == "endwhile")
                            {
                                lastLabel = next[0] + countLabel;
                            }
                            else {
                                lastLabel = "";
                            }
                        }

                        AddSequenceElement(line[0], RemoveFirstAndAdd(line, lastLabel));
                        break;
                    case "output":
                        if (code.Count > 0) {
                            next = code.Peek();
                            if (next[0] == "else" || next[0] == "endif" || next[0] == "endwhile")
                            {
                                lastLabel = next[0] + countLabel;
                            }
                        }
                        
                        AddSequenceElement(line[0], RemoveFirstAndAdd(line, lastLabel));
                        break;
                    case "end":
                        lastLabel = "end";
                        AddSequenceElement(line[0], RemoveFirstAndAdd(line, lastLabel));
                        break;
                    case "if":
                        curren = labels.Pop();
                        if (code.Count > 0)
                        {
                            next = code.Peek();
                            if (next[0] == "else")
                            {
                                lastLabel = next[0] + labels.Peek().key;
                            }
                            else if (next[0] == "endif" || next[0] == "endwhile")
                            {
                                lastLabel = next[0] + countLabel;
                            }
                            else
                            {
                                lastLabel = "";
                            }
                        }
                        AddSequenceElement(line[0] + line[2], RemovePairsAndAdd(line, curren.key.ToString(), lastLabel, curren.key.ToString()));
                        break;
                    case "onlyif":
                        curren = labels.Pop();
                        if (code.Count > 0)
                        {
                            next = code.Peek();
                            if (next[0] == "else")
                            {
                                lastLabel = next[0] + labels.Peek().key;
                            }
                            else if (next[0] == "endif" || next[0] == "endwhile")
                            {
                                lastLabel = next[0] + countLabel;
                            }
                            else
                            {
                                lastLabel = "";
                            }
                        }
                        AddSequenceElement(line[0] + line[2], RemovePairsAndAdd(line, curren.relationEnd, lastLabel));
                        break;
                    case "while":
                        curren = labels.Pop();
                        lastLabel = "while" + curren.key;
                        AddSequenceElement(line[0]+line[2], RemovePairsAndAdd(line, curren.relationEnd, lastLabel));
                        break;
                    case "else":
                        curren = labels.Peek();

                        AddSequenceElement(line[0], RemoveFirstAndAdd(line, curren.relationEnd));
                        break;
                    case "endif":
                        if (IsALabel(lastLines.Peek()))
                        {
                            relationalLabel = lastLabel;
                        }
                        else {
                            relationalLabel = "endif"+countLabel;
                        }
                        labels.Push(new Label(countLabel++, CommandType.IF, relationalLabel));
                        break;
                    case "endwhile":
                        if (IsALabel(lastLines.Peek()))
                        {
                            relationalLabel = lastLabel;
                        }
                        else {
                            relationalLabel = "endwhile" + countLabel;
                        }
                        labels.Push(new Label(countLabel++, CommandType.WHILE, relationalLabel));

                        if (code.Count > 0)
                        {
                            next = code.Peek();
                            if (next[0] == "else")
                            {
                                lastLabel = next[0] + labels.Peek().key;
                            }
                            else if (next[0] == "endif" || next[0] == "endwhile")
                            {
                                lastLabel = next[0] + countLabel;
                            }
                            else
                            {
                                lastLabel = "";
                            }
                        }
                        AddSequenceElement(line[0], RemoveFirstAndAdd(line, "while" + (countLabel-1), lastLabel));
                        break;
                    default:
                        if (code.Count > 0)
                        {
                            next = code.Peek();
                            if (next[0] == "else")
                            {
                                lastLabel = next[0] + labels.Peek().key;
                            }
                            else if (next[0] == "endif" || next[0] == "endwhile")
                            {
                                lastLabel = next[0] + countLabel;
                            }
                            else {
                                lastLabel = "";
                            }
                        }
                        if (line.Length >= 4)
                            AddSequenceElement(line[1] + line[3], RemoveOddsAndAdd(line, lastLabel));
                        else
                            AddSequenceElement(line[1], RemoveOddsAndAdd(line, lastLabel));

                        break;

                }
                lastLines.Push(line);
            }
        }
        bool IsALabel(string[] lastLine) {
            return lastLine[0] == "endwhile" || lastLine[0] == "endif" || lastLine[0] == "end" || lastLine[0] == "while";
        }
        string[] RemoveFirst(string[] args) {
            string[] newArgs = new string[args.Length];
            newArgs[0] = separator.ToString();
            for (int i = 1; i < args.Length; i++) {
                newArgs[i] = args[i];
            }
            return newArgs;
        }
        string[] RemoveFirstAndAdd(string[] args, string extra)
        {
            string[] newArgs = new string[args.Length + 1];
            newArgs[0] = separator.ToString();
            for (int i = 1; i < args.Length; i++)
            {
                newArgs[i] = args[i];
            }
            newArgs[args.Length] = extra;
            return newArgs;
        }
        string[] RemoveFirstAndAdd(string[] args, string extra1, string extra2)
        {
            string[] newArgs = new string[args.Length + 2];
            newArgs[0] = separator.ToString();
            for (int i = 1; i < args.Length; i++)
            {
                newArgs[i] = args[i];
            }
            newArgs[args.Length] = extra1;
            newArgs[args.Length + 1] = extra2;
            return newArgs;
        }
        string[] RemovePairs(string[] args)
        {
            string[] newArgs = new string[args.Length / 2 + 1];
            newArgs[0] = separator.ToString();
            for (int i = 1, j = 1; i < args.Length; i += 2, j++)
            {
                newArgs[j] = args[i];
            }
            return newArgs;
        }
        string[] RemovePairsAndAdd(string[] args, string extra)
        {
            string[] newArgs = new string[args.Length / 2 + 2];
            newArgs[0] = separator.ToString();
            for (int i = 1, j = 1; i < args.Length; i += 2, j++)
            {
                newArgs[j] = args[i];
            }
            newArgs[args.Length / 2 + 1] = extra;
            return newArgs;
        }
        string[] RemovePairsAndAdd(string[] args, string extra1, string extra2)
        {
            string[] newArgs = new string[args.Length / 2 + 3];
            newArgs[0] = separator.ToString();
            for (int i = 1, j = 1; i < args.Length; i += 2, j++)
            {
                newArgs[j] = args[i];
            }
            newArgs[args.Length / 2 + 1] = extra1;
            newArgs[args.Length / 2 + 2] = extra2;
            return newArgs;
        }
        string[] RemovePairsAndAdd(string[] args, string extra1, string extra2, string extra3)
        {
            string[] newArgs = new string[args.Length / 2 + 4];
            newArgs[0] = separator.ToString();
            for (int i = 1, j = 1; i < args.Length; i += 2, j++)
            {
                newArgs[j] = args[i];
            }
            newArgs[args.Length / 2 + 1] = extra1;
            newArgs[args.Length / 2 + 2] = extra2;
            newArgs[args.Length / 2 + 3] = extra3;
            return newArgs;
        }

        string[] RemoveOddsAndAdd(string[] args, string extra)
        {
            string[] newArgs = new string[args.Length / 2 + 3];
            newArgs[0] = separator.ToString();
            for (int i = 0, j = 1; i < args.Length; i += 2, j++)
            {
                newArgs[j] = args[i];
            }
            newArgs[args.Length / 2 + 2] = extra;
            return newArgs;
        }
        void AddSequenceElement(string key, string[] args)
        {
            assemblySequence.Push(String.Format(encodingFormat[key], args));
        }

        void AddSequenceElement(string key, string arg)
        {
            assemblySequence.Push(String.Format(encodingFormat[key], arg));
        }

        string GetAssemblyCode() {
            String final = "";
            while (assemblySequence.Count > 0) {
                final += assemblySequence.Pop();
            }
            return final;
        }

        private void frmTransformPseudoCode_Load(object sender, EventArgs e)
        {
            txInput.ScrollBars = ScrollBars.Both;
        }
    }
    class Label {
        public int key;
        public CommandType type;
        public string relationEnd;

        public Label(int key, CommandType type, string relationEnd)
        {
            this.key = key;
            this.type = type;
            this.relationEnd = relationEnd;
        }
    }
    enum CommandType {
        INPUT,
        OUTPUT,
        IF,
        WHILE,
        END,
        SET
    }
}
