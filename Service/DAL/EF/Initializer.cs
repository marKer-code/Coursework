namespace DAL.EF
{
    using System.Data.Entity;

    class Initializer : DropCreateDatabaseIfModelChanges<ProgramDatabaseModel>
    {
        protected override void Seed(ProgramDatabaseModel ctx)
        {
            base.Seed(ctx);
        }
    }
}
