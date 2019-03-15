using System;
using System.Collections.Generic;
using assembly.Model;
using assembly.Enums;
using assembly.Data;

namespace assembly.Logic
{
    public class PseudoCodeToHarvardCompiler
    {
        int countLabel;
        Stack<string> assemblySequence;
        Dictionary<String, String> encodingFormat;
        Stack<CodeLine> code;
        CodeLine currentLine;
        string[] pseudoCodeLines;

        public delegate void SyntaxErrorEventHandler(int line, int initialCharacter, string errorMessage);
        public event SyntaxErrorEventHandler onSyntaxError;

        public delegate void compileFinallyEventHandler(string harvardCode);
        public event compileFinallyEventHandler onCompileFinally;

        public PseudoCodeToHarvardCompiler()
        {
            encodingFormat = new Dictionary<string, string>();
            foreach (Command command in DataProvider.HARVARD_COMMANDS)
            {
                encodingFormat.Add(command.Name, command.Format);
            }
        }
        public void Compile( char separator, string [] pseudoCodeLines) {
            try
            {
                this.pseudoCodeLines = pseudoCodeLines;
                ArgumentModeler.SEPERATOR = separator;
                ProcessCode();
                GenerateAssembly();
                onCompileFinally(GetAssemblyCode());
            }
            catch(Exception e){
                onSyntaxError(currentLine.LineNumber, currentLine.InitialCharacter, e.Message);
            }

        }
        void ProcessCode()
        {
            Initialize();
            int lineCount = 0;
            int characterCount = 0;
            foreach (string line in pseudoCodeLines)
            {
                string reducedLine = line.ToLower()
                    .Replace(";", "").Replace("(", " ").Replace(")", " ").Replace("else if", "elseif").
                    Replace("end if", "endif").Replace("end while", "endwhile").Replace("end for", "endfor").Trim();
                while (reducedLine.Contains("  "))
                {
                    reducedLine = reducedLine.Replace("  ", " ");
                }
                if (line.Length > 0)
                {
                    if (line.Length >= 2 && line.Substring(0, 2) == "//")
                    {
                        continue;
                    }
                    string[] lineComponents = reducedLine.Split();
                    code.Push(new CodeLine(lineComponents, lineCount, characterCount));
                }
                lineCount++;
                characterCount += line.Length + 2;

            }
        }
        void Initialize()
        {
            countLabel = 0;
            code = new Stack<CodeLine>();
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
            currentLine = code.Pop();
            switch (currentLine.Args[0])
            {
                case "end":
                    lastLabel = "end";
                    AddSequenceElement(currentLine.Args[0], ArgumentModeler.RemoveFirstAndAdd(currentLine.Args, lastLabel));
                    break;
                case "input":
                case "output":
                    lastLabel = GetLastLabel(labels);
                    AddSequenceElement(currentLine.Args[0], ArgumentModeler.RemoveFirstAndAdd(currentLine.Args, lastLabel));
                    break;
                case "if":
                    if (labels.Count > 0 && labels.Peek().type == CommandType.IF)
                    {
                        currentLabel = labels.Pop();
                        lastLabel = GetLastLabel(labels);
                        AddSequenceElement(currentLine.Args[0] + currentLine.Args[2], ArgumentModeler.RemovePairsAndAdd(currentLine.Args, currentLabel.lastRelation, lastLabel));
                    }
                    else
                    {
                        throw new Exception("End if not found");
                    }
                    break;
                case "while":
                    if (labels.Count > 0 && labels.Peek().type == CommandType.WHILE)
                    {
                        currentLabel = labels.Pop();
                        lastLabel = "while" + currentLabel.key;
                        AddSequenceElement(currentLine.Args[0] + currentLine.Args[2], ArgumentModeler.RemovePairsAndAdd(currentLine.Args, currentLabel.endRelation, lastLabel));
                    }
                    else
                    {
                        throw new Exception("End while not found");
                    }
                    break;
                case "for":
                    if (labels.Count > 0 && labels.Peek().type == CommandType.FOR)
                    {
                        labels.Peek().type = CommandType.WHILE;
                        code.Push(currentLine.GenerateReply(ArgumentModeler.cutArguments(currentLine.Args, 1, 3)));
                        code.Push(currentLine.GenerateReply(string.Format(
                            "while {0} {1} {2}",
                            currentLine.Args[4], currentLine.Args[5], currentLine.Args[6]
                            ).Split()));
                        code.Push(currentLine.GenerateReply(ArgumentModeler.cutArguments(currentLine.Args, 7, currentLine.Args.Length - 1)));
                        if (code.Peek().Args.Length == 0)
                        {
                            code.Pop();
                        }
                    }
                    else {
                        throw new Exception("End for not found");
                    }
                    break;
                case "else":
                    if (labels.Count > 0 && labels.Peek().type == CommandType.IF)
                    {
                        currentLabel = labels.Peek();
                        if (currentLabel.countRelations == 0)
                        {
                            AddSequenceElement(currentLine.Args[0], ArgumentModeler.RemoveFirstAndAdd(currentLine.Args, currentLabel.endRelation));
                        }
                        else
                        {
                            throw new Exception("syntax error in else");
                        }
                    }
                    else
                    {
                        throw new Exception("End if not found");
                    }
                    break;
                case "elseif":
                case "elsif":
                    if (labels.Count > 0 && labels.Peek().type == CommandType.IF)
                    {
                        currentLabel = labels.Peek();
                        currentLabel.currentRelation = "elsif" + currentLabel.key + currentLabel.countRelations;
                        AddSequenceElement("while" + currentLine.Args[2], ArgumentModeler.RemovePairsAndAdd(currentLine.Args, currentLabel.lastRelation, currentLabel.currentRelation));
                        currentLabel.lastRelation = currentLabel.currentRelation;
                        currentLabel.countRelations++;
                    }
                    else
                    {
                        throw new Exception("End if not found");
                    }
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
                    labels.Push(new Model.Label(countLabel++, 
                        currentLine.Args[0] == "endfor" ? CommandType.FOR : CommandType.WHILE, 
                        relatedlLabel));
                    lastLabel = GetLastLabel(labels);
                    AddSequenceElement("endwhile", ArgumentModeler.RemoveFirstAndAdd(currentLine.Args, "while" + (countLabel - 1), lastLabel));
                    break;
                default:
                    lastLabel = GetLastLabel(labels);
                    //aritmetical operation
                    if (currentLine.Args.Length == 5)
                    {
                        if (currentLine.Args[3] == "*")
                        {
                            AddMultipicationToCode(currentLine, currentLine.Args[0], currentLine.Args[2], currentLine.Args[4]);
                        }
                        else
                        {
                            AddSequenceElement(currentLine.Args[1] + currentLine.Args[3], ArgumentModeler.RemoveOddsAndAdd(currentLine.Args, lastLabel));
                        }
                    }
                    //Increment and decrement  or
                    //Assignment
                    else if (currentLine.Args.Length == 3)
                    {
                        if (currentLine.Args[1] == "*=")
                        {
                            AddMultipicationToCode(currentLine, currentLine.Args[0], currentLine.Args[0], currentLine.Args[2]);
                        }
                        else
                        {
                            AddSequenceElement(currentLine.Args[1], ArgumentModeler.RemoveOddsAndAdd(currentLine.Args, lastLabel));
                        }
                    }
                    else if (currentLine.Args.Length == 2)
                    {
                        AddSequenceElement(currentLine.Args[0], ArgumentModeler.RemoveFirstAndAdd(currentLine.Args, lastLabel));
                    }
                    else
                    {
                        throw new Exception();
                    }
                    break;
            }
            lastLines.Push(currentLine.Args);
            return lastLabel;
        }

        string GetLastLabel(Stack<Model.Label> labels)
        {
            if (code.Count > 0)
            {
                string[] next = code.Peek().Args;
                if (next[0] == "else")
                {
                    labels.Peek().lastRelation = next[0] + labels.Peek().key;
                    return labels.Peek().lastRelation;
                }
                else if (next[0] == "endif" || next[0] == "endwhile")
                {
                    return next[0] + countLabel;
                }
                else if (next[0] == "endfor")
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

        void AddMultipicationToCode(CodeLine line, string product, string multiplier, string multiplying)
        {
            code.Push(line.GenerateReply(string.Format("auxmul00000 = {0}", multiplier).Split()));
            code.Push(line.GenerateReply("auxmul00001 = 0".Split()));
            code.Push(line.GenerateReply("while auxmul00000 > 0".Split()));
            code.Push(line.GenerateReply(string.Format("auxmul00001 += {0}", multiplying).Split()));
            code.Push(line.GenerateReply("endwhile".Split()));
            code.Push(line.GenerateReply(string.Format("{0} = auxmul00001", product).Split()));
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
