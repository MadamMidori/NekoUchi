using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace NekoUchi.DAL
{
    public interface IDataProvider
    {
        T Get<T>(string field, string value);

        IEnumerable<T> GetMultiple<T>(string field, string value);

        IEnumerable<T> GetMultipleUsingAny<T>(string field, string value);

        T Create<T>(T item);

        bool Update<T>(Dictionary<string, string> changes, string field, string value);

        bool Delete<T>(string field, string value);
    }
}
