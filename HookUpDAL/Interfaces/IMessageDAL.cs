﻿using HookUpDAL.Entities;

namespace HookUpDAL.Interfaces
{
    public interface IMessageDAL
    {
        void AddMessage(Message message);
        void DeleteMessage(Message message);
        Task<Message> GetMessage(int id);
        Task<IQueryable<Message>> GetMessagesForUser();
        Task<IEnumerable<Message>> GetMessageThread(string currentUsername, string recipientUsername);
        Task<bool> SaveAllAsync();
        void AddGroup(Group group);
        void RemoveConnection(Connection connection);
        Task<Connection> GetConnection(string connectionId);
        Task<Group> GetMessageGroup(string groupName);
        Task<Group> GetGroupForConnection(string connectionId);
    }
}
