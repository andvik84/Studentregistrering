using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studentregistrering.DataAccess.Data
{
    public class CourseRepository : GenericRepository<Course>
    {
        public CourseRepository(StudentDbContext ctx) : base(ctx)
        {
        }

        public IEnumerable<Course> GetAllPlusStudents()
        {
            return _ctx.Courses.Include(c => c.Students).ToList();
        }

        public IEnumerable<Student> GetStudentsInCourse(int id)
        {
            return _ctx.Courses
                .Where(c=>c.CourseId==id)
                .Include(c=>c.Students)
                .Select(c=>c.Students) 
                .FirstOrDefault()
                .ToList();
            //courses
            //        .Where(c => c.CourseId == currentCourse.CourseId)
            //        .First()
            //        .Students;
        }
    }
}
