using MeetupMembers.Mongo;

namespace MeetupMembers.Model
{
    public class Member : IHaveAUniqueId
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Bio { get; set;}
    }
}