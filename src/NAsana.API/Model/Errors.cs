namespace NAsana.API.v1.Model
{
    using System.Collections.Generic;

    public class Error
    {
        public string Message { get; set; }
        public string Phrase { get; set; }
    }

    public class ErrorsModel
    {
        public List<Error> Errors { get; set; }
    }
}