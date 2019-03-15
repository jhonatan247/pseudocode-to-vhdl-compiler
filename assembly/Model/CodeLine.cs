using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assembly.Model
{
    public class CodeLine
    {
        public string[] Args { get; set; }
        public int LineNumber { get; set; }
        public int InitialCharacter { get; set; }

        public CodeLine(string[] args, int lineNumber, int initialCharacter)
        {
            Args = args;
            LineNumber = lineNumber;
            InitialCharacter = initialCharacter;
        }

        public CodeLine GenerateReply(string[] args) {
            return new CodeLine(args, LineNumber, InitialCharacter);
        }
    }
}
