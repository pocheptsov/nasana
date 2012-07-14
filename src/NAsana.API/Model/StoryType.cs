namespace NAsana.API.v1.Model
{
    using Utils;

    public class StoryType : EnumBase
    {
        public static StoryType Comment = new StoryType("Comment");
        public static StoryType System = new StoryType("system");

        private StoryType(string type) : base(type)
        {
        }

        public string Type
        {
            get { return Name; }
        }
    }
}