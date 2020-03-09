using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.DataAccess
{
    public class DBContext<T> : DynamoDBContext, IDBContext<T>
        where T : class
    {
        public DBContext(IAmazonDynamoDB client) : base(client)
        {
        }

        public DBContext(IAmazonDynamoDB client, DynamoDBContextConfig config) : base(client, config)
        {
        }

        public async Task DeleteByIdAsync(T item)
        {
            await base.DeleteAsync(item);
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await base.LoadAsync<T>(id);
        }

        public async Task<IEnumerable<T>> GetItems(IEnumerable<ScanCondition> conditions)
        {
            return await base.ScanAsync<T>(conditions).GetRemainingAsync();
        }

        public async Task<IEnumerable<T>> GetItems(QueryFilter queryFilter)
        {
            return await base.QueryAsync<T>(queryFilter).GetRemainingAsync();
        }

        public async Task SaveAsync(T item)
        {
            await base.SaveAsync(item);
        }

    }
}
