﻿namespace Service
{
    using DAL;
    using DAL.Entities;
    using DAL.Interfaces;
    using DAL.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.Text;

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ProgramService : IProgramService
    {
        private readonly IUnitOfWork repositories;

        public ProgramService()
            => repositories = new UnitOfWork(new DAL.ProgramDatabaseModel());

        public bool CheckUser(string login, string password)
        {
            string pass = new Utils().ComputeSha256Hash(password);
            try
            {
                User user = repositories.UserRepository
                            .Get(u => u.Login == login &&
                                 u.HashPassword == pass)
                            .First();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CheckLogin(string login)
        {
            try
            {
                User user = repositories.UserRepository
                                .Get(u => u.Login == login)
                                .First();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void AddUser(string login, string nickname,
            string password, byte[] img, bool online, DateTime lastOnline)
        {
            repositories.UserRepository.Insert(new User()
            {
                Login = login,
                HashPassword = new Utils().ComputeSha256Hash(password)
            });
            repositories.Save();

            User user = repositories.UserRepository
                .Get(u => u.Login == login).First();
            int id = user.Id;

            repositories.UserInfoRepository.Insert(new UserInfo()
            {
                Nickname = nickname,
                LastOnline = lastOnline,
                Online = online,
                Photo = img,
                UserId = id
            });
            repositories.Save();
        }

        public List<byte[]> LoadUserInfo(string login)
        {
            User user = repositories.UserRepository
                .Get(u => u.Login == login).First();
            int id = user.Id;

            IEnumerable<UserInfo> userInfo = repositories
                .UserInfoRepository.Get(ui => ui.UserId == id);
            List<byte[]> infoes = new List<byte[]>();
            infoes.Add(Encoding.Default.GetBytes(userInfo.First().Nickname));
            infoes.Add(Encoding.Default.GetBytes(userInfo.First().LastOnline.ToString()));
            infoes.Add(userInfo.First().Photo);
            infoes.Add(Encoding.Default.GetBytes(userInfo.First().Online.ToString()));
            return infoes;
        }

        public void SaveUserInfo(string lastLogin,
            string login, string nickname,
            string password, byte[] img)
        {
            User newUser = repositories.UserRepository
                .Get(u => u.Login == lastLogin)
                .First();

            int id = newUser.Id;
            repositories.UserRepository.Delete(id);
            repositories.UserInfoRepository.Delete(id);
            repositories.Save();

            repositories.UserRepository.Insert(
                new User
                {
                    Login = login,
                    HashPassword = new Utils().ComputeSha256Hash(password)
                });
            repositories.Save();

            id = repositories.UserRepository
                .Get(u => u.Login == login)
                .First()
                .Id;

            repositories.UserInfoRepository.Insert(
                new UserInfo
                {
                    Nickname = nickname,
                    UserId = id,
                    Online = true,
                    LastOnline = DateTime.Now,
                    Photo = img
                });
            repositories.Save();
        }

        public void AddRequest(string sender, string receiver)
        {
            int idSender = repositories.UserRepository
                .Get(u => u.Login == sender)
                .First()
                .Id,

                idReceiver = repositories.UserRepository
                .Get(u => u.Login == receiver)
                .First()
                .Id;

            repositories.RequestRepository.Insert(
                new Request
                {
                    SenderId = idSender,
                    ReceiverId = idReceiver,
                    SendTime = DateTime.Now
                });

            repositories.Save();
        }
    }
}
