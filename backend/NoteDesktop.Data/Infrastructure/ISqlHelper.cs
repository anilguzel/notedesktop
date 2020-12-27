using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NoteDesktop.Data.Infrastructure
{
    public interface ISqlHelper
    {
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered,
            CancellationToken cancellationToken = default);

        Task<T> QueryFirstOrDefaultAsync<T>(string sql, object parameters = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered,
            CancellationToken cancellationToken = default);

        Task<T> QuerySingleOrDefaultAsync<T>(string sql, object parameters = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered,
            CancellationToken cancellationToken = default);

        Task<int> ExecuteAsync(string sql, object parameters = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered,
            CancellationToken cancellationToken = default);

        Task<T> ExecuteScalarAsync<T>(string sql, object parameters = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered,
            CancellationToken cancellationToken = default);

        Task<IDataReader> ExecuteReaderAsync<T>(string sql, object parameters = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered,
            CancellationToken cancellationToken = default);

        Task QueryMultipleAsync(string sql, Action<SqlMapper.GridReader> map, object parameters = null,
            IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null,
            CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);

        Task<bool> HealthCheckAsync(CancellationToken cancellationToken);

        Task<IEnumerable<T>> QueryReadOnlyAsync<T>(string sql, object parameters = null,
            IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null,
            CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);

        Task<T> QueryFirstOrDefaultReadOnlyAsync<T>(string sql, object parameters = null,
            IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null,
            CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);

        Task<T> QuerySingleOrDefaultReadOnlyAsync<T>(string sql, object parameters = null,
            IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null,
            CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);

        Task<int> ExecuteReadOnlyAsync(string sql, object parameters = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered,
            CancellationToken cancellationToken = default);

        Task<T> ExecuteScalarReadOnlyAsync<T>(string sql, object parameters = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered,
            CancellationToken cancellationToken = default);

        Task<IDataReader> ExecuteReaderReadOnlyAsync<T>(string sql, object parameters = null,
            IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null,
            CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default);
    }
}


