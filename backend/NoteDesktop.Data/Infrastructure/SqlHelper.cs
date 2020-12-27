using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Npgsql;

namespace NoteDesktop.Data.Infrastructure
{
    public class SqlHelper : ISqlHelper
    {
        private readonly string _connectionString;
        private readonly string _readOnlyConnectionString;

        protected SqlHelper(string connectionString, string readOnlyConnectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("DbConnection_string_has_not_found");
            }

            if (string.IsNullOrEmpty(readOnlyConnectionString))
            {
                //throw new ArgumentException("DbReadOnlyConnection_string_has_not_found");
            }

            _connectionString = connectionString;
            _readOnlyConnectionString = readOnlyConnectionString;
        }

        private NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }

        private NpgsqlConnection GetReadOnlyConnection()
        {
            return new NpgsqlConnection(_readOnlyConnectionString);
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default)
        {
            using (var conn = GetConnection())
            {
                var cmd = new CommandDefinition(sql, parameters, transaction, commandTimeout, commandType, flags, cancellationToken);
                return await conn.QueryAsync<T>(cmd);
            };
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default)
        {
            using (var conn = GetConnection())
            {
                var cmd = new CommandDefinition(sql, parameters, transaction, commandTimeout, commandType, flags, cancellationToken);
                return await conn.QueryFirstOrDefaultAsync<T>(cmd);
            }
        }

        public async Task<T> QuerySingleOrDefaultAsync<T>(string sql, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default)
        {
            using (var conn = GetConnection())
            {
                var cmd = new CommandDefinition(sql, parameters, transaction, commandTimeout, commandType, flags, cancellationToken);
                return await conn.QuerySingleOrDefaultAsync<T>(cmd);
            }
        }

        public async Task<int> ExecuteAsync(string sql, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default)
        {
            //Transactional bir işlem yapılıyorsa aynı connectiondan devam etmeli.
            if (transaction != null)
            {
                var conn = transaction.Connection;
                var cmd = new CommandDefinition(sql, parameters, transaction, commandTimeout, commandType, flags, cancellationToken);
                return await conn.ExecuteAsync(cmd);
            }

            using (var conn = GetConnection())
            {
                var cmd = new CommandDefinition(sql, parameters, null, commandTimeout, commandType, flags, cancellationToken);
                return await conn.ExecuteAsync(cmd);
            }
        }


        public async Task<T> ExecuteScalarAsync<T>(string sql, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default)
        {
            //Transactional bir işlem yapılıyorsa aynı connectiondan devam etmeli.
            if (transaction != null)
            {
                var conn = transaction.Connection;
                var cmd = new CommandDefinition(sql, parameters, transaction, commandTimeout, commandType, flags, cancellationToken);
                return await conn.ExecuteScalarAsync<T>(cmd);
            }

            using (var conn = GetConnection())
            {
                var cmd = new CommandDefinition(sql, parameters, null, commandTimeout, commandType, flags, cancellationToken);
                return await conn.ExecuteScalarAsync<T>(cmd);
            }
        }

        public async Task<IDataReader> ExecuteReaderAsync<T>(string sql, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default)
        {
            //Transactional bir işlem yapılıyorsa aynı connectiondan devam etmeli.
            if (transaction != null)
            {
                var conn = transaction.Connection;
                var cmd = new CommandDefinition(sql, parameters, transaction, commandTimeout, commandType, flags, cancellationToken);
                return await conn.ExecuteReaderAsync(cmd);
            }

            using (var conn = GetConnection())
            {
                var cmd = new CommandDefinition(sql, parameters, null, commandTimeout, commandType, flags, cancellationToken);
                return await conn.ExecuteReaderAsync(cmd);
            }
        }

        public async Task QueryMultipleAsync(string sql, Action<SqlMapper.GridReader> map, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered,
            CancellationToken cancellationToken = default)
        {
            using (var conn = GetConnection())
            {
                var cmd = new CommandDefinition(sql, parameters, transaction, commandTimeout, commandType, flags, cancellationToken);
                var result = await conn.QueryMultipleAsync(cmd);
                map(result);
            }
        }

        public async Task<bool> HealthCheckAsync(CancellationToken cancellationToken)
        {
            using (var conn = GetConnection())
            {
                const string query = "SELECT NEWID()";
                var cmd = new CommandDefinition(query, cancellationToken);
                var result = await conn.ExecuteScalarAsync<Guid>(cmd);
                return result != Guid.Empty;
            }
        }

        public async Task<IEnumerable<T>> QueryReadOnlyAsync<T>(string sql, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default)
        {
            using (var conn = GetConnection())
            {
                var cmd = new CommandDefinition(sql, parameters, transaction, commandTimeout, commandType, flags, cancellationToken);
                return await conn.QueryAsync<T>(cmd);
            }
        }

        public async Task<T> QueryFirstOrDefaultReadOnlyAsync<T>(string sql, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default)
        {
            using (var conn = GetConnection())
            {
                var cmd = new CommandDefinition(sql, parameters, transaction, commandTimeout, commandType, flags, cancellationToken);
                return await conn.QueryFirstOrDefaultAsync<T>(cmd);
            }
        }

        public async Task<T> QuerySingleOrDefaultReadOnlyAsync<T>(string sql, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default)
        {
            using (var conn = GetConnection())
            {
                var cmd = new CommandDefinition(sql, parameters, transaction, commandTimeout, commandType, flags, cancellationToken);
                return await conn.QuerySingleOrDefaultAsync<T>(cmd);
            }
        }

        public async Task<int> ExecuteReadOnlyAsync(string sql, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default)
        {
            using (var conn = GetConnection())
            {
                var cmd = new CommandDefinition(sql, parameters, transaction, commandTimeout, commandType, flags, cancellationToken);
                return await conn.ExecuteAsync(cmd);
            }
        }


        public async Task<T> ExecuteScalarReadOnlyAsync<T>(string sql, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default)
        {
            using (var conn = GetConnection())
            {
                var cmd = new CommandDefinition(sql, parameters, transaction, commandTimeout, commandType, flags, cancellationToken);
                return await conn.ExecuteScalarAsync<T>(cmd);
            }
        }

        public async Task<IDataReader> ExecuteReaderReadOnlyAsync<T>(string sql, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered, CancellationToken cancellationToken = default)
        {
            using (var conn = GetConnection())
            {
                var cmd = new CommandDefinition(sql, parameters, transaction, commandTimeout, commandType, flags, cancellationToken);
                return await conn.ExecuteReaderAsync(cmd);
            }
        }

        public async Task QueryMultipleReadOnlyAsync(string sql, Action<SqlMapper.GridReader> map, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null, CommandFlags flags = CommandFlags.Buffered,
            CancellationToken cancellationToken = default)
        {
            using (var conn = GetConnection())
            {
                var cmd = new CommandDefinition(sql, parameters, transaction, commandTimeout, commandType, flags, cancellationToken);
                var result = await conn.QueryMultipleAsync(cmd);
                result.Dispose();
                map(result);
            }
        }

        public async Task<bool> HealthCheckReadOnlyAsync(CancellationToken cancellationToken)
        {
            using (var conn = GetConnection())
            {
                const string query = "SELECT NEWID()";
                var cmd = new CommandDefinition(query, cancellationToken);
                var result = await conn.ExecuteScalarAsync<Guid>(cmd);
                return result != Guid.Empty;
            }
        }

        public async Task<IDbConnection> GetConnection(CancellationToken cancellationToken)
        {
            var conn = GetConnection();
            await conn.OpenAsync(cancellationToken);
            return conn;
        }
    }
}