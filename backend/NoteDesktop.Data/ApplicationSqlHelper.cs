using NoteDesktop.Data.Infrastructure;

namespace NoteDesktop.Data
{
    public interface IApplicationSqlHelper : ISqlHelper
    {
    }

    public class ApplicationSqlHelper : SqlHelper, IApplicationSqlHelper
    {
        public ApplicationSqlHelper(string connectionString, string readOnlyConnectionString) : base(connectionString, readOnlyConnectionString)
        {
        }
    }
}