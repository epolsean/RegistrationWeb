using RegistrationWeb.Logic;
using RegistrationWeb.Logic.RegistrationServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RegistrationWeb.Tests
{
    public class ServiceTests
    {
        [Fact]
        public void Test_GetCourses()
        {
            var service = new DataService();
            var actual = service.GetCourses();
            Assert.NotNull(actual);
        }

        [Fact]
        public void Test_CancelCourse()
        {
            var service = new DataService();
            var course = new CourseDAO() { Title = "SR-909", Department = "Shooting Range", Professor = service.GetPerson(5), StartTime = 20, EndTime = 21, Capacity = 1, Credit = 1, Active = true };
            service.AddCourse(course);
            var c = service.GetCourse(22);
            //service.CartCourse(c, new PersonDAO() { Id = 7 });
            var actual = service.CancelCourse(c);
            Assert.True(actual);
        }
    }
}
