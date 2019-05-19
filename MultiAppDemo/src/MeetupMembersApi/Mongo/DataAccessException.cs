using System;

namespace MeetupMembersApi.Mongo
{
    public class DataAccessException : Exception
    {
        public DataAccessException(string message, Exception ex) : base(message, ex)
        {
        }
    }
}