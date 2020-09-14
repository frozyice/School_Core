using School_Core.Contexts;

namespace School_Core.Querys
{
    public interface ITeacherQuery
    {
        
    }
    
    public class TeachersQuery : ITeacherQuery
    {
        private readonly SchoolCoreDbContext _dbContext;

        public TeachersQuery(SchoolCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        
    }
}