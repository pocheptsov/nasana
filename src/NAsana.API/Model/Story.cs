namespace NAsana.API.v1.Model
{
    using System;
    using Utils;

    public class Story : Identity
    {
        public Story()
        {
            Source = StorySource.Web;
            Type = StoryType.Comment;
        }

        //created_at
        public DateTime CreatedAt { get; set; }
        //created_by
        public User CreatedBy { get; set; }

        public string Text { get; set; }

        /// <summary>
        /// May be a task or a project.
        /// </summary>
        public IStoryTarget Target { get; set; }

        public StorySource Source { get; set; }
        public StoryType Type { get; set; }
        public Workspace Workspace { get; set; }
    }
}