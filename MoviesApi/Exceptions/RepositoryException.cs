using System;

namespace MoviesApi.Exceptions
{
    public class RepositoryException : Exception
    {
        public enum ExeptionType
        {
            NotFound,
            ConnectionError,
            FailedToSave
        };

        public ExeptionType Type { get; set; }

        public RepositoryException(ExeptionType type, string message) : base(message)
        {
            this.Type = type;
        }
    }
}