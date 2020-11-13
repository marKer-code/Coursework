namespace DAL
{
    using DAL.Entities;

    public class DAL_Class
    {
        public void AddUser()
        {
            ProgramDatabaseModel pdm = new ProgramDatabaseModel();
            pdm.Users.Add(new User()
            {
                Login = "MarKer",
                HashPassword = "qqq"
            });
            pdm.SaveChanges();
        }
    }
}
