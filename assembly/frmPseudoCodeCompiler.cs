using System;
using System.Collections.Generic;
using System.Windows.Forms;
using assembly.Model;
using assembly.Enums;
using assembly.Data;
using assembly.Logic;
using System.Diagnostics;

namespace assembly
{
    public partial class frmPseudoCodeCompiler : Form
    {
        int countLabel;
        Stack<string> assemblySequence;
        Dictionary<String, String> encodingFormat;
        Stack<string[]> code;

        public frmPseudoCodeCompiler()
        {
            InitializeComponent();
            encodingFormat = new Dictionary<string, string>();
            foreach (Command command in DataProvider.COMMANDS)
            {
                encodingFormat.Add(command.Name, command.Format);
            }
        }
        private void frmTransformPseudoCode_Load(object sender, EventArgs e)
        {
            txInput.ScrollBars = ScrollBars.Both;
            cbSeparator.SelectedIndex = 0;
        }
        private void btnHelp_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/jhonatan247/pseudocode-to-vhdl-compiler/blob/master/README.md");
        }
        private void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                ArgumentModeler.SEPERATOR = GetSeparator();
                ProcessCode();
                GenerateAssembly();
                Hide();
                new frmAssemblyTraducer(GetAssemblyCode(), cbSeparator.SelectedIndex).ShowDialog();
                Show();
            }
            catch
            {
                MessageBox.Show("There is a syntax error", "Pseudo code compiler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                string reducedLine = line.ToLower()
                    .Replace(";", "").Replace(" ( ", " ").Replace(" (", " ").Replace("( ", " ").Replace(" ) ", " ")
                    .Replace(" )", " ").Replace(") ", " ").Replace("(", " ").Replace(")", " ").Replace("else if", "elseif").Replace("end if", "endif").Replace("end while", "endwhile").Replace("end for", "endfor").Trim();
                if (line.Length > 0)
                {
                    if (line.Length >= 2 && line.Substring(0, 2) == "//")
                    {
                        continue;
                    }
                    string[] lineComponents = reducedLine.Split();
                    code.Push(lineComponents);
                }

            }
        }
        void Initialize()
        {
            countLabel = 0;
            code = new Stack<string[]>();
            assemblySequence = new Stack<string>();
        }
        void GenerateAssembly()
        {
            Stack<Model.Label> labels = new Stack<Model.Label>();
            Stack<string[]> lastLines = new Stack<string[]>();
            string lastLabel = "";

            while (code.Count > 0)
            {
                lastLabel = FormatLineAndGetLabel(labels, lastLines, lastLabel);
            }
        }
        string FormatLineAndGetLabel(Stack<Model.Label> labels, Stack<string[]> lastLines, string lastLabel)
        {
            Model.Label currentLabel;
            string relatedlLabel = "";
            string[] line = code.Pop();
            switch (line[0])
            {
                case "end":
                    lastLabel = "end";
                    AddSequenceElement(line[0], ArgumentModeler.RemoveFirstAndAdd(line, lastLabel));
                    break;
                case "input":
                case "output":
                    lastLabel = GetLastLabel(labels);
                    AddSequenceElement(line[0], ArgumentModeler.RemoveFirstAndAdd(line, lastLabel));
                    break;
                case "if":
                    currentLabel = labels.Pop();
                    lastLabel = GetLastLabel(labels);
                    AddSequenceElement(line[0] + line[2], ArgumentModeler.RemovePairsAndAdd(line, currentLabel.lastRelation, lastLabel));
                    break;
                case "while":
                    currentLabel = labels.Pop();
                    lastLabel = "while" + currentLabel.key;
                    AddSequenceElement(line[0] + line[2], ArgumentModeler.RemovePairsAndAdd(line, currentLabel.endRelation, lastLabel));
                    break;
                case "for":
                    code.Push(ArgumentModeler.cutArguments(line, 1, 3));
                    code.Push(string.Format("while {0} {1} {2}", line[4], line[5], line[6]).Split());
                    code.Push(ArgumentModeler.cutArguments(line, 7, line.Length - 1));
                    if (code.Peek().Length == 0) {
                        code.Pop();
                    }
                    break;
                case "else":
                    currentLabel = labels.Peek();
                    if (currentLabel.countRelations == 0)
                    {
                        AddSequenceElement(line[0], ArgumentModeler.RemoveFirstAndAdd(line, currentLabel.endRelation));
                    }
                    else
                    {
                        throw new Exception("syntax error in else");
                    }
                    break;
                case "elseif":
                case "elsif":
                    currentLabel = labels.Peek();
                    //the format is the same as the while

                    currentLabel.currentRelation = "elsif" + currentLabel.key + currentLabel.countRelations;
                    AddSequenceElement("while" + line[2], ArgumentModeler.RemovePairsAndAdd(line, currentLabel.lastRelation, currentLabel.currentRelation));
                    currentLabel.lastRelation = currentLabel.currentRelation;
                    currentLabel.countRelations++;
                    break;
                case "endif":
                    if (IsALabel(lastLines.Peek()))
                    {
                        relatedlLabel = lastLabel;
                    }
                    else
                    {
                        relatedlLabel = "endif" + countLabel;
                    }
                    labels.Push(new Model.Label(countLabel++, CommandType.IF, relatedlLabel));
                    break;
                case "endwhile":
                case "endfor":
                    if (IsALabel(lastLines.Peek()))
                    {
                        relatedlLabel = lastLabel;
                    }
                    else
                    {
                        relatedlLabel = "endwhile" + countLabel;
                    }
                    labels.Push(new Model.Label(countLabel++, CommandType.WHILE, relatedlLabel));
                    lastLabel = GetLastLabel(labels);
                    AddSequenceElement("endwhile", ArgumentModeler.RemoveFirstAndAdd(line, "while" + (countLabel - 1), lastLabel));
                    break;
                default:
                    lastLabel = GetLastLabel(labels);
                    //aritmetical operation
                    if (line.Length == 5)
                    {
                        if (line[3] == "*")
                        {
                            AddMultipicationToCode(line[0], line[2], line[4]);
                        }
                        else
                        {
                            AddSequenceElement(line[1] + line[3], ArgumentModeler.RemoveOddsAndAdd(line, lastLabel));
                        }
                    }
                    //Increment and decrement  or
                    //Assignment
                    else if (line.Length == 3)
                    {
                        if (line[1] == "*=")
                        {
                            AddMultipicationToCode(line[0], line[0], line[2]);
                        }
                        else
                        {
                            AddSequenceElement(line[1], ArgumentModeler.RemoveOddsAndAdd(line, lastLabel));
                        }
                    }
                    else if (line.Length == 2)
                    {
                        AddSequenceElement(line[0], ArgumentModeler.RemoveFirstAndAdd(line, lastLabel));
                    }
                    else
                    {
                        throw new Exception();
                    }
                    break;
            }
            lastLines.Push(line);
            return lastLabel;
        }

        string GetLastLabel(Stack<Model.Label> labels)
        {
            if (code.Count > 0)
            {
                string[] next = code.Peek();
                if (next[0] == "else")
                {
                    labels.Peek().lastRelation = next[0] + labels.Peek().key;
                    return labels.Peek().lastRelation;
                }
                else if (next[0] == "endif" || next[0] == "endwhile")
                {
                    return next[0] + countLabel;
                }else if (next[0] == "endfor")
                {
                    return "endwhile" + countLabel;
                }
            }
            return "";
        }
        bool IsALabel(string[] lastLine)
        {
            return lastLine[0] == "endwhile" || lastLine[0] == "endif" || lastLine[0] == "end" ||
                lastLine[0] == "while" || lastLine[0] == "else" || lastLine[0] == "elsif" || lastLine[0] == "elseif" || lastLine[0] == "for" || lastLine[0] == "endfor";
        }
        void AddSequenceElement(string key, string[] args)
        {
            assemblySequence.Push(String.Format(encodingFormat[key], args));
        }

        void AddMultipicationToCode(string product, string multiplier, string multiplying)
        {
            code.Push(string.Format("auxmul00000 = {0}", multiplier).Split());
            code.Push("auxmul00001 = 0".Split());
            code.Push("while auxmul00000 > 0".Split());
            code.Push(string.Format("auxmul00001 += {0}", multiplying).Split());
            code.Push("endwhile".Split());
            code.Push(string.Format("{0} = auxmul00001", product).Split());
        }

        string GetAssemblyCode()
        {
            String final = "";
            while (assemblySequence.Count > 0)
            {
                final += assemblySequence.Pop();
            }
            return final;
        }
    }
}