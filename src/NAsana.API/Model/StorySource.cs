namespace NAsana.API.v1.Model
{
    using Utils;

    public class StorySource : EnumBase
    {
        public static StorySource Web = new StorySource("web");
        public static StorySource Email = new StorySource("email");
        public static StorySource Mobile = new StorySource("mobile");
        public static StorySource Api = new StorySource("api");
        public static StorySource Unknown = new StorySource("unknown");

        private StorySource(string source) : base(source)
        {
        }

        public string Source
        {
            get { return Name; }
        }
    }
}