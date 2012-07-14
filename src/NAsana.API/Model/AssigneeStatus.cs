namespace NAsana.API.v1.Model
{
    using Utils;

    public class AssigneeStatus : EnumBase
    {
        public static AssigneeStatus Inbox = new AssigneeStatus("inbox");
        public static AssigneeStatus Later = new AssigneeStatus("later");
        public static AssigneeStatus Today = new AssigneeStatus("today");
        public static AssigneeStatus Upcoming = new AssigneeStatus("upcoming");

        private AssigneeStatus(string status) : base(status)
        {
        }

        public string Status
        {
            get { return Name; }
        }
    }
}