namespace assembly.Model
{
    public class Command
    {
        public string Name { get; set; }
        public string Format { get; set; }

        public Command(string name, string format)
        {
            Name = name;
            Format = format;
        }
    }
}
