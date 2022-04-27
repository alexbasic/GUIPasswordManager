using Microsoft.Data.Sqlite;
using Ru.Mail.AlexBasic.GUIPasswordManager.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ru.Mail.AlexBasic.GUIPasswordManager.Infrastructure.Db
{
    public class SqliteContext : ISqliteContext
    {
        public SqliteConnection SqliteConnection { get; private set; }

        public SqliteContext(string dataSource = null, string password = null)
        {
            var connectionString = new SqliteConnectionStringBuilder()
            {
                DataSource = dataSource ?? "data.db",
                Mode = SqliteOpenMode.ReadWriteCreate,
                Password = password
            }.ToString();
            Open(connectionString);
        }

        public SqliteContext(string connectionString)
        {
            Open(connectionString);
        }

        public void Dispose()
        {
            SqliteConnection.Dispose();
        }

        public int ExecuteNonQuery(string sqlExpression, object parameters = null)
        {
            var command = SqliteConnection.CreateCommand();
            command.CommandText = sqlExpression;
            AddParameters(command.Parameters, parameters);
            return command.ExecuteNonQuery();
        }

        public IEnumerable<T> ExecuteQuery<T>(string sqlExpression, object parameters = null) where T : class, new()
        {
            using (var command = SqliteConnection.CreateCommand())
            {
                command.CommandText = sqlExpression;
                AddParameters(command.Parameters, parameters);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                        return ReturnResult<T>(reader).ToList();
                    else
                        return ReturnEmptyResult<T>();
                }
            }
        }

        public T ExecuteScalar<T>(string sqlExpression, object parameters = null)
        {
            var result = ExecuteScalar(sqlExpression, parameters);
            try
            {
                return result.SqliteAsType<T>();
            }
            catch (InvalidCastException ex)
            {
                throw new InvalidCastException($"Не возможно привести тип результата ({result?.GetType()?.Name ?? "null"}) к {typeof(T).Name}");
            }
        }

        public ISqliteContext WithTransaction(Action<ISqliteContext> action)
        {
            using (var transaction = SqliteConnection.BeginTransaction())
            {
                try
                {
                    action(this);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return this;
        }

        private void Open(string connectionString)
        {
            SqliteConnection = new SqliteConnection(connectionString);
            SqliteConnection.Open();
        }

        private object? ExecuteScalar(string sqlExpression, object parameters)
        {
            using (var command = SqliteConnection.CreateCommand())
            {
                command.CommandText = sqlExpression;
                AddParameters(command.Parameters, parameters);

                return command.ExecuteScalar();
            }
        }

        private void AddParameters(SqliteParameterCollection parameterCollection, object parameters)
        {
            if (parameters == null) return;
            foreach (var item in parameters.GetPropertiesValues())
                parameterCollection.Add(
                    new SqliteParameter("@" + item.Key, item.Value));
        }

        private IEnumerable<T> ReturnEmptyResult<T>() => Enumerable.Empty<T>();

        private IEnumerable<T> ReturnResult<T>(SqliteDataReader dataReader) where T : class, new()
        {
            while (dataReader.Read())
            {
                var result = new T();
                for (var index = 0; index < dataReader.FieldCount; index++)
                {
                    var fieldName = dataReader.GetName(index);
                    var fieldValue = dataReader.GetValue(index).SqliteAsType(result?.GetType()?.GetProperty(fieldName)?.PropertyType ?? typeof(object));
                    result.SetValue(fieldName, fieldValue);
                }
                yield return result;
            }
        }
    }
}
