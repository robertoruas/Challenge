using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
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

        public async Task SaveAsync(T item)
        {
            await base.SaveAsync(item);
        }
    }
}
