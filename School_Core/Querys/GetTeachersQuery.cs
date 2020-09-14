using School_Core.Contexts;

namespace School_Core.Querys
{
    public interface IGetTeacherQuery
    {
        
    }
    
    public class GetTeachersQuery : IGetTeacherQuery
    {
        private readonly SchoolCoreDbContext _dbContext;

        public GetTeachersQuery(SchoolCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        
    }
}