using task4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace task4.Services
{
    public class SqlServerDb : IStudentsDb
    {
        public IEnumerable<Student> GetStudents()
        {
            throw new NotImplementedException();
        }
    }
}
