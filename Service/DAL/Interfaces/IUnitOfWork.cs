namespace DAL.Interfaces
{
    using DAL.Entities;
    using System;

    public interface IUnitOfWork : IDisposable
    {
        IRepository<Couple> CoupleRepository { get; }
        IRepository<Group> GroupRepository { get; }
        IRepository<Message> MessageRepository { get; }
        IRepository<Request> RequestRepository { get; }
        IRepository<User> UserRepository { get; }
        IRepository<UserInfo> UserInfoRepository { get; }
        IRepository<UserIP> UserIPRepository { get; }

        void Save();
    }
}
