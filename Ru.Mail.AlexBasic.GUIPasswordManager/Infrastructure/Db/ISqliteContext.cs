using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;

namespace Ru.Mail.AlexBasic.GUIPasswordManager
{
    public interface ISqliteContext : IDisposable
    {
        SqliteConnection SqliteConnection { get; }
        int ExecuteNonQuery(string sqlExpression, object parameters = null);
        IEnumerable<T> ExecuteQuery<T>(string sqlExpression, object parameters = null) where T : class, new();
        T ExecuteScalar<T>(string sqlExpression, object parameters = null);
        ISqliteContext WithTransaction(Action<ISqliteContext> action);
    }
}
