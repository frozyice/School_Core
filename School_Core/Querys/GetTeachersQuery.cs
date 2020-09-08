using System.Collections.Generic;
using System.Linq;
using Domain.Specifications;
using School_Core.Contexts;
using School_Core.Domain.Models;

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