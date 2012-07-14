namespace NAsana.API.v1.Model.Utils
{
    public class EnumBase
    {
        public EnumBase(string name)
        {
            Name = name;
        }

        protected string Name { get; private set; }

        public override string ToString()
        {
            return Name;
        }
    }
}