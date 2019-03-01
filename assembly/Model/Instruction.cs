using assembly.Enums;

namespace assembly.Model
{
    public class Instruction
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
