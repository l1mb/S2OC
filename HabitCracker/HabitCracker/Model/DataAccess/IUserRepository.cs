namespace HabitCracker.Model.DataAccess
{
    public interface IUserRepository<TUserEntity> : IRepository<TUserEntity> where TUserEntity : class
    {
        TUserEntity GetUserWithUsername(string username);

        bool ContainsUserWithUsername(string username);
    }
}