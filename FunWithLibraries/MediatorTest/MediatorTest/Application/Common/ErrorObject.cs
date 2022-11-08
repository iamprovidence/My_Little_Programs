namespace MediatorTest.Application.Common
{
    public enum ApplicationErrorCode
    {
        Unknown = 0,
        Validation = 1,
    }

    public class ErrorObject
    {
        public ApplicationErrorCode Code { get; set; }

        public ICollection<string> Details { get; set; }

        private ErrorObject()
        {
            Details = new List<string>();
        }

        public static ErrorObject Create(ApplicationErrorCode code, string message)
        {
            var result = new ErrorObject { Code = code };
            result.Details.Add(message);
            return result;
        }
    }
}
