using System.Threading;
using System.Threading.Tasks;
using MeetupMembersApi.Models;
using MongoDB.Driver;

namespace MeetupMembersApi.Tests
{
    public static class MongoCollectionExtensions
    {
        public static async Task Insert(this IMongoCollection<Member> collection, Member member)
        {
            await collection.InsertOneAsync(member, new InsertOneOptions(), CancellationToken.None);
        }
        
        public static async Task<Member> GetMemberById(this IMongoCollection<Member> collection, string id)
        {
            using var response = await collection.FindAsync(x => x.Id == id, new FindOptions<Member>(), CancellationToken.None);

            return await response.FirstOrDefaultAsync(CancellationToken.None);
        }
    }
}