using assembly.Enums;
using assembly.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assembly.Data
{
    public class DataProvider
    {
        public static Command[] COMMANDS = {
            new Command("input",
                "{2}{0}inA{0}\n" +
                "{0}storeA{0}{1}\n"),
            new Command("output",
                "{2}{0}loadA{0}{1}\n" +
                "{0}outA{0}\n"),
            new Command("end",
                "{1}{0}halt{0}"),

            new Command("=-",
                "{4}{0}loadA{0}{2}\n" +
                "{0}subA{0}{3}\n" +
                "{0}storeA{0}{1}\n"),
            new Command("=+",
                "{4}{0}loadA{0}{2}\n" +
                "{0}addA{0}{3}\n" +
                "{0}storeA{0}{1}\n"),
            new Command("=*",
                "{4}{0}loadA{0}{2}\n" +
                "{0}addA{0}{3}\n" +
                "{0}storeA{0}{1}\n"),
            new Command("=",
                "{3}{0}loadA{0}{2}\n" +
                "{0}storeA{0}{1}\n"),
            new Command("+=",
                "{3}{0}loadA{0}{1}\n" +
                "{0}addA{0}{2}\n" +
                "{0}storeA{0}{1}\n"),
            new Command("-=",
                "{3}{0}loadA{0}{1}\n" +
                "{0}subA{0}{2}\n" +
                "{0}storeA{0}{1}\n"),

            new Command("onlyif>=",
                "{4}{0}loadA{0}{1}\n" +
                "{0}subA{0}{2}\n" +
                "{0}jneg{0}{3}\n"),
            new Command("onlyif>",
                "{4}{0}loadA{0}{1}\n" +
                "{0}subA{0}{2}\n" +
                "{0}jneg{0}{3}\n" +
                "{0}jz{0}{3}\n"),
            new Command("onlyif==",
                "{4}{0}loadA{0}{1}\n" +
                "{0}subA{0}{2}\n" +
                "{0}jnz{0}{3}\n"),
            new Command("onlyif=",
                "{4}{0}loadA{0}{1}\n" +
                "{0}subA{0}{2}\n" +
                "{0}jnz{0}{3}\n"),
            new Command("onlyif!=",
                "{4}{0}loadA{0}{1}\n" +
                "{0}subA{0}{2}\n" +
                "{0}jz{0}{3}\n"),
            new Command("onlyif<=",
                "{4}{0}loadA{0}{1}\n" +
                "{0}subA{0}{2}\n" +
                "{0}jpos{0}{3}\n"),
            new Command("onlyif<",
                "{4}{0}loadA{0}{1}\n" +
                "{0}subA{0}{2}\n" +
                "{0}jpos{0}{3}\n" +
                "{0}jz{0}{3}\n"),

            new Command("if>=",
                "{4}{0}loadA{0}{1}\n" +
                "{0}subA{0}{2}\n" +
                "{0}jneg{0}else{3}\n"),
            new Command("if>",
                "{4}{0}loadA{0}{1}\n" +
                "{0}subA{0}{2}\n" +
                "{0}jneg{0}else{3}\n" +
                "{0}jz{0}else{3}\n"),
            new Command("if==",
                "{4}{0}loadA{0}{1}\n" +
                "{0}subA{0}{2}\n" +
                "{0}jnz{0}else{3}\n"),
            new Command("if=",
                "{4}{0}loadA{0}{1}\n" +
                "{0}subA{0}{2}\n" +
                "{0}jnz{0}else{3}\n"),
            new Command("if!=",
                "{4}{0}loadA{0}{1}\n" +
                "{0}subA{0}{2}\n" +
                "{0}jz{0}else{3}\n"),
            new Command("if<=",
                "{4}{0}loadA{0}{1}\n" +
                "{0}subA{0}{2}\n" +
                "{0}jpos{0}else{3}\n"),
            new Command("if<",
                "{4}{0}loadA{0}{1}\n" +
                "{0}subA{0}{2}\n" +
                "{0}jpos{0}else{3}\n" +
                "{0}jz{0}else{3}\n"),

            new Command("else",
                "{0}jmp{0}{1}\n"),


            new Command("while>=",
                "{4}{0}loadA{0}{1}\n" +
                "{0}subA{0}{2}\n" +
                "{0}jneg{0}{3}\n"),
            new Command("while>",
                "{4}{0}loadA{0}{1}\n" +
                "{0}subA{0}{2}\n" +
                "{0}jneg{0}{3}\n" +
                "{0}jz{0}{3}\n"),
            new Command("while==",
                "{4}{0}loadA{0}{1}\n" +
                "{0}subA{0}{2}\n" +
                "{0}jnz{0}{3}\n"),
            new Command("while!=",
                "{4}{0}loadA{0}{1}\n" +
                "{0}subA{0}{2}\n" +
                "{0}jz{0}{3}\n"),
            new Command("while<=",
                "{4}{0}loadA{0}{1}\n" +
                "{0}subA{0}{2}\n" +
                "{0}jpos{0}{3}\n"),
            new Command("while<",
                "{4}{0}loadA{0}{1}\n" +
                "{0}subA{0}{2}\n" +
                "{0}jpos{0}{3}\n" +
                "{0}jz{0}{3}\n"),


            new Command("endwhile",
                "{2}{0}jmp{0}{1}\n")
        };

        public static Instruction[] INSTRUCTIONS = {
            new Instruction("loadA", "0", TypeOfInstruction.DATA),
            new Instruction("storeA", "1", TypeOfInstruction.DATA),
            new Instruction("addA", "2", TypeOfInstruction.DATA),
            new Instruction("subA", "3", TypeOfInstruction.DATA),
            new Instruction("inA", "400", TypeOfInstruction.IO),
            new Instruction("outA", "500", TypeOfInstruction.IO),
            new Instruction("jpos", "6", TypeOfInstruction.PROGRAM),
            new Instruction("jneg", "7", TypeOfInstruction.PROGRAM),
            new Instruction("jz", "8", TypeOfInstruction.PROGRAM),
            new Instruction("jnz", "9", TypeOfInstruction.PROGRAM),
            new Instruction("jmp", "A", TypeOfInstruction.PROGRAM),
            new Instruction("halt", "B00", TypeOfInstruction.IO)
        };
    }
}
