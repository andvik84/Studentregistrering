using Microsoft.EntityFrameworkCore;

namespace Studentregistrering.DataAccess.Data
{
    public class StudentRepository : GenericRepository<Student>
    {
        public StudentRepository(StudentDbContext ctx) : base(ctx)
        {
        }

        public override List<Student> GetAll()
        {
            return (_ctx.Students
                            .Include(s => s.Courses))
                            .OrderBy(s => s.LastName)
                            .ThenBy(s => s.FirstName)
                            .ToList();
        }
    }
}
