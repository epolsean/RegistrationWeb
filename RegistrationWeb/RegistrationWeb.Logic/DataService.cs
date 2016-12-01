using RegistrationWeb.Logic.RegistrationServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationWeb.Logic
{
    public class DataService
    {
        /// <summary>
        /// TODO: Implement Factories and convert DAOs to DTOs
        /// </summary>

        private RegistrationServiceClient rsc = new RegistrationServiceClient();

        public List<CourseDAO> GetCourses()
        {
            var courses = new List<CourseDAO>();

            foreach (var item in rsc.GetCourses())
            {
                courses.Add(item);
            }

            return courses;
        }

        public List<PersonDAO> GetPeople()
        {
            var people = new List<PersonDAO>();

            foreach (var item in rsc.GetPeople())
            {
                people.Add(item);
            }

            return people;
        }

        public List<PersonTypeDAO> GetPersonTypes()
        {
            var personTypes = new List<PersonTypeDAO>();

            foreach (var item in rsc.GetPersonTypes())
            {
                personTypes.Add(item);
            }

            return personTypes;
        }

        public List<ScheduleDAO> GetSchedules()
        {
            var schedules = new List<ScheduleDAO>();

            foreach (var item in rsc.GetSchedules())
            {
                schedules.Add(item);
            }

            return schedules;
        }

        public CourseDAO GetCourse(int cid)
        {
            var course = rsc.GetCourse(cid);

            return course;
        }

        public PersonDAO GetPerson(int pid)
        {
            var person = rsc.GetPerson(pid);

            return person;
        }

        public PersonTypeDAO GetPersonType(int pid)
        {
            var personType = rsc.GetPersonType(pid);

            return personType;
        }

        public ScheduleDAO GetSchedule(int cid, int pid)
        {
            var schedule = rsc.GetSchedule(cid, pid);

            return schedule;
        }

        public List<CourseDAO> GetAllFullCourses()
        {
            var courses = new List<CourseDAO>();

            foreach (var item in rsc.GetAllFullCourses())
            {
                courses.Add(item);
            }

            return courses;
        }

        public List<CourseDAO> GetAllOpenCourses()
        {
            var courses = new List<CourseDAO>();

            foreach (var item in rsc.GetAllOpenCourses())
            {
                courses.Add(item);
            }

            return courses;
        }

        public List<PersonDAO> GetEnrolledStudentsByCourse(CourseDAO course)
        {
            var people = new List<PersonDAO>();

            foreach (var item in rsc.GetEnrolledStudentsByCourse(course))
            {
                people.Add(item);
            }

            return people;
        }

        public List<PersonDAO> GetAllActiveStudents()
        {
            var people = new List<PersonDAO>();

            foreach (var item in rsc.GetAllActiveStudents())
            {
                people.Add(item);
            }

            return people;
        }

        public List<CourseDAO> GetStudentSchedule(PersonDAO person)
        {
            var courses = new List<CourseDAO>();

            foreach (var item in rsc.GetStudentSchedule(person))
            {
                if (rsc.GetSchedule(item.Id,  person.Id).Active)
                {
                    courses.Add(item);
                }
            }

            return courses;
        }

        public List<CourseDAO> GetCartedCourses(int pid)
        {
            var courses = new List<CourseDAO>();

            foreach (var item in rsc.GetStudentSchedule(rsc.GetPerson(pid)))
            {
                if (!rsc.GetSchedule(item.Id, pid).Active)
                {
                    courses.Add(item);
                }
            }

            return courses;
        }

        public List<CourseDAO> GetAllCoursesByStudent(PersonDAO person)
        {
            var courses = new List<CourseDAO>();

            foreach (var item in rsc.GetStudentSchedule(person))
            {
                courses.Add(item);
            }

            return courses;
        }

        public bool AddCourse(CourseDAO course)
        {
            return rsc.AddCourse(course);
        }

        public bool AddPerson(PersonDAO person)
        {
            return rsc.AddPerson(person);
        }

        public bool AddPersonType(PersonTypeDAO personType)
        {
            return rsc.AddPersonType(personType);
        }

        public bool RegisterCourse(CourseDAO course, PersonDAO person)
        {
            return rsc.RegisterCourse(course, person);
        }

        public bool CartCourse(CourseDAO course, PersonDAO person)
        {
            return rsc.CartCourse(course, person);
        }

        public bool DropCourse(CourseDAO course, PersonDAO person)
        {
            return rsc.DropCourse(course, person);
        }

        public bool CancelCourse(CourseDAO course)
        {
            return rsc.CancelCourse(course);
        }

        public bool ModifyCourse(CourseDAO course, int newCapacity, int newStart, int newEnd)
        {
            return rsc.ModifyCourse(course, newCapacity, newStart, newEnd);
        }

        public bool RemoveStudent(PersonDAO student)
        {
            return rsc.RemoveStudent(student);
        }
    }
}
