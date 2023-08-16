namespace ApplicationInsightsDemo.Common.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(Type type, int id) : base(CreateExceptionMessage(type, id))
        { }

        public EntityNotFoundException(Type type, string message) : base(CreateExceptionMessage(type, message))
        { }

        private static string CreateExceptionMessage(Type type, int id)
        {
            return $"{type.Name}. Id: {id}.";
        }

        private static string CreateExceptionMessage(Type type, string message)
        {
            return $"{type.Name}. {message}.";
        }
    }
}