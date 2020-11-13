namespace DAL.Repositories
{
    using DAL.Entities;
    using DAL.Interfaces;
    using System;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProgramDatabaseModel context;
        private GenericRepository<Couple> coupleRepository;
        private GenericRepository<Group> groupRepository;
        private GenericRepository<Message> messageRepository;
        private GenericRepository<Request> requestRepository;
        private GenericRepository<User> userRepository;
        private GenericRepository<UserInfo> userInfoRepository;

        public UnitOfWork(ProgramDatabaseModel context)
            => this.context = context;

        public IRepository<Couple> CoupleRepository
        {
            get
            {
                if (coupleRepository == null)
                    coupleRepository =
                        new GenericRepository<Couple>(context);
                return coupleRepository;
            }
        }
        public IRepository<Group> GroupRepository
        {
            get
            {
                if (groupRepository == null)
                    groupRepository =
                        new GenericRepository<Group>(context);
                return groupRepository;
            }
        }
        public IRepository<Message> MessageRepository
        {
            get
            {
                if (messageRepository == null)
                    messageRepository =
                        new GenericRepository<Message>(context);
                return messageRepository;
            }
        }
        public IRepository<Request> RequestRepository
        {
            get
            {
                if (requestRepository == null)
                    requestRepository =
                        new GenericRepository<Request>(context);
                return requestRepository;
            }
        }
        public IRepository<User> UserRepository
        {
            get
            {
                if (userRepository == null)
                    userRepository =
                        new GenericRepository<User>(context);
                return userRepository;
            }
        }
        public IRepository<UserInfo> UserInfoRepository
        {
            get
            {
                if (userInfoRepository == null)
                    userInfoRepository =
                        new GenericRepository<UserInfo>(context);
                return userInfoRepository;
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
                if (disposing)
                    context.Dispose();
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
            => context.SaveChanges();
    }
}
