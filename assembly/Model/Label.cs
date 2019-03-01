using assembly.Enums;

namespace assembly.Model
{
    public class Label
    {
        public int key;
        public CommandType type;
        public string currentRelation;
        public string lastRelation;
        public string endRelation;
        public int countRelations;

        public Label(int key, CommandType type, string endRelation)
        {
            this.key = key;
            this.type = type;
            this.endRelation = endRelation;
            this.lastRelation = endRelation;
            this.currentRelation = endRelation;
            this.countRelations = 0;
        }
    }
}
