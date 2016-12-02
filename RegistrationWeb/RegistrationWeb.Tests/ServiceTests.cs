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
        public void Test_GetPeople()
        {
            var service = new DataService();
            var actual = service.GetPeople();
            Assert.NotNull(actual);
        }

        [Fact]
        public void Test_GetPersonTypes()
        {
            var service = new DataService();
            var actual = service.GetPersonTypes();
            Assert.NotNull(actual);
        }

        [Fact]
        public void Test_GetSchedules()
        {
            var service = new DataService();
            var actual = service.GetSchedules();

            Assert.NotNull(actual);
        }

        [Fact]
        public void Test_GetCourse()
        {
            var service = new DataService();
            var actual = service.GetCourse(1);

            Assert.NotNull(actual);
        }

        [Fact]
        public void Test_GetPerson()
        {
            var service = new DataService();
            var actual = service.GetPerson(1);

            Assert.NotNull(actual);
        }

        [Fact]
        public void Test_GetPersonType()
        {
            var service = new DataService();
            var actual = service.GetPersonType(1);

            Assert.NotNull(actual);
        }

        [Fact]
        public void Test_GetSchedule()
        {
            var service = new DataService();
            var actual = service.GetSchedule(1, 1);

            Assert.NotNull(actual);
        }

        [Fact]
        public void Test_GetAllFullCourses()
        {
            var service = new DataService();
            var actual = service.GetAllFullCourses();

            Assert.NotNull(actual);
        }

        [Fact]
        public void Test_GetAllOpenCourses()
        {
            var service = new DataService();
            var actual = service.GetAllOpenCourses();

            Assert.NotNull(actual);
        }

        [Fact]
        public void Test_GetEnrolledStudentsByCourse()
        {
            var service = new DataService();
            var course = service.GetCourse(1);
            var actual = service.GetEnrolledStudentsByCourse(course);

            Assert.NotNull(actual);
        }

        [Fact]
        public void Test_GetAllActiveStudents()
        {
            var service = new DataService();
            var actual = service.GetAllActiveStudents();

            Assert.NotNull(actual);
        }

        [Fact]
        public void Test_GetStudentSchedule()
        {
            var service = new DataService();
            var person = service.GetPerson(1);
            var actual = service.GetStudentSchedule(person);

            Assert.NotNull(actual);
        }

        [Fact]
        public void Test_GetCartedCourses()
        {
            var service = new DataService();
            var actual = service.GetCartedCourses(7);

            Assert.NotNull(actual);
        }

        [Fact]
        public void Test_GetAllCoursesByStudent()
        {
            var service = new DataService();
            var person = service.GetPerson(2);
            var actual = service.GetAllCoursesByStudent(person);

            Assert.NotNull(actual);
        }

        [Fact]
        public void Test_AddCourse()
        {
            var service = new DataService();
            var person = service.GetPerson(5);
            var course = new CourseDAO() { Title = "TS-001", Department = "Testing", Professor = person, StartTime = 20, EndTime = 21, Capacity = 1, Credit = 1, Active = true };

            var actual = service.AddCourse(course);

            foreach(var item in service.GetCourses())
            {
                if (item.Title == course.Title && item.Department == course.Department && item.Professor.Id == course.Professor.Id && item.StartTime == course.StartTime && item.EndTime == course.EndTime)
                {
                    if (!service.CancelCourse(item))
                    {
                        actual = false;
                    }
                }
            }

            Assert.True(actual);
        }

        [Fact]
        public void Test_AddPerson()
        {
            var service = new DataService();
            var person = new PersonDAO() { FirstName = "Test", LastName = "Man", PersonType = service.GetPersonType(1), Active = true };

            var actual = service.AddPerson(person);

            foreach (var item in service.GetPeople())
            {
                if (item.FirstName == person.FirstName && item.LastName == person.LastName && item.PersonType.Id == person.PersonType.Id)
                {
                    if (!service.RemoveStudent(item))
                    {
                        actual = false;
                    }
                }
            }

            Assert.True(actual);
        }

        [Fact]
        public void Test_AddPersonType()
        {
            var service = new DataService();
            var personType = new PersonTypeDAO() { Type = "Test", Active = true };

            var actual = service.AddPersonType(personType);

            Assert.True(actual);
        }

        [Fact]
        public void Test_RegisterCourse()
        {
            var service = new DataService();
            var person = service.GetPerson(2);
            var professor = service.GetPerson(5);
            var course = new CourseDAO() { Title = "TS-001", Department = "Testing", Professor = professor, StartTime = 20, EndTime = 21, Capacity = 1, Credit = 1, Active = true };
            var actual = false;

            if (service.AddCourse(course))
            {
                foreach (var item in service.GetCourses())
                {
                    if (item.Title == course.Title && item.Department == course.Department && item.Professor.Id == course.Professor.Id && item.StartTime == course.StartTime && item.EndTime == course.EndTime)
                    {
                        if (service.CartCourse(item, person))
                        {
                            actual = service.RegisterCourse(item, person);

                            if (service.CancelCourse(item))
                            {
                                Assert.True(actual);
                            }
                            else
                            {
                                Assert.True(false);
                            }
                        }
                    }
                }
            }

            Assert.True(actual);
        }

        [Fact]
        public void Test_CartCourse()
        {
            var service = new DataService();
            var person = service.GetPerson(2);
            var professor = service.GetPerson(5);
            var course = new CourseDAO() { Title = "TS-001", Department = "Testing", Professor = professor, StartTime = 20, EndTime = 21, Capacity = 1, Credit = 1, Active = true };
            var actual = false;

            if (service.AddCourse(course))
            {
                foreach (var item in service.GetCourses())
                {
                    if (item.Title == course.Title && item.Department == course.Department && item.Professor.Id == course.Professor.Id && item.StartTime == course.StartTime && item.EndTime == course.EndTime)
                    {
                        actual = service.CartCourse(item, person);

                        if (service.CancelCourse(item))
                        {
                            Assert.True(actual);
                        }
                        else
                        {
                            Assert.True(false);
                        }
                    }
                }
            }

            Assert.True(actual);
        }

        [Fact]
        public void Test_DropCourse()
        {
            var service = new DataService();
            var person = service.GetPerson(2);
            var professor = service.GetPerson(5);
            var course = new CourseDAO() { Title = "TS-001", Department = "Testing", Professor = professor, StartTime = 20, EndTime = 21, Capacity = 1, Credit = 1, Active = true };
            var actual = false;

            if (service.AddCourse(course))
            {
                foreach (var item in service.GetCourses())
                {
                    if (item.Title == course.Title && item.Department == course.Department && item.Professor.Id == course.Professor.Id && item.StartTime == course.StartTime && item.EndTime == course.EndTime)
                    {
                        if (service.CartCourse(item, person))
                        {
                            actual = service.DropCourse(item, person);

                            if (service.CancelCourse(item))
                            {
                                Assert.True(actual);
                            }
                            else
                            {
                                Assert.True(false);
                            }
                        }
                    }
                }
            }

            Assert.True(actual);
        }

        [Fact]
        public void Test_CancelCourse()
        {
            var service = new DataService();
            var person = service.GetPerson(5);
            var course = new CourseDAO() { Title = "TS-001", Department = "Testing", Professor = person, StartTime = 20, EndTime = 21, Capacity = 1, Credit = 1, Active = true };
            var actual = false;

            if (service.AddCourse(course))
            {
                foreach (var item in service.GetCourses())
                {
                    if(item.Title == course.Title && item.Department == course.Department && item.Professor.Id == course.Professor.Id && item.StartTime == course.StartTime && item.EndTime == course.EndTime)
                    {
                        actual = service.CancelCourse(item);
                    }
                }
            }

            Assert.True(actual);
        }

        [Fact]
        public void Test_ModifyCourse()
        {
            var service = new DataService();
            var person = service.GetPerson(2);
            var professor = service.GetPerson(5);
            var course = new CourseDAO() { Title = "TS-001", Department = "Testing", Professor = professor, StartTime = 20, EndTime = 21, Capacity = 1, Credit = 1, Active = true };
            var actual = false;

            if (service.AddCourse(course))
            {
                foreach (var item in service.GetCourses())
                {
                    if (item.Title == course.Title && item.Department == course.Department && item.Professor.Id == course.Professor.Id && item.StartTime == course.StartTime && item.EndTime == course.EndTime)
                    {
                        actual = service.ModifyCourse(item, 3, 21, 22);

                        if (service.CancelCourse(item))
                        {
                            Assert.True(actual);
                        }
                        else
                        {
                            Assert.True(false);
                        }
                    }
                }
            }

            Assert.True(actual);
        }

        [Fact]
        public void Test_RemoveStudent()
        {
            var service = new DataService();
            var person = new PersonDAO() { FirstName = "Test", LastName = "Man", PersonType = service.GetPersonType(1), Active = true };
            var actual = false;

            if (service.AddPerson(person))
            {

                foreach (var item in service.GetPeople())
                {
                    if (item.FirstName == person.FirstName && item.LastName == person.LastName && item.PersonType.Id == person.PersonType.Id)
                    {
                        actual = service.RemoveStudent(item);
                    }
                }
            }

            Assert.True(actual);
        }
    }
}
