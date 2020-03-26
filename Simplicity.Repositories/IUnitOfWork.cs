namespace Simplicity.Repositories
{
    public interface IUnitOfWork
    {
        void RollBack();

        void Dispose();

        void Commit();
    }
}
