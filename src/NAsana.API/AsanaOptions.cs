namespace NAsana.API.v1
{
    using System.Collections.Generic;

    public class AsanaOptions
    {
        public List<string> Fields { get; set; }

        public List<string> Expand { get; set; }

        public bool Pretty { get; set; }
    }
}