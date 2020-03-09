using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataAccess
{
    public interface IDBContext<T>: IDisposable
        where T : class
    {
        Task<T> GetByIdAsync(string id);
        Task SaveAsync(T item);
        Task DeleteByIdAsync(T item);
        Task<IEnumerable<T>> GetItems(IEnumerable<ScanCondition> conditions);
        Task<IEnumerable<T>> GetItems(QueryFilter queryFilter);


    }
}
