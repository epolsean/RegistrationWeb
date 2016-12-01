using RegistrationWeb.Logic;
using RegistrationWeb.Logic.RegistrationServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RegistrationWeb.Client
{
    public partial class StudentForm : System.Web.UI.Page
    {
        private DataService data = new DataService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCourses();
            }
        }

        private void LoadCourses()
        {
            CourseId_List.Items.Clear();

            foreach (var item in data.GetCourses())
            {
                CourseId_List.Items.Add(new ListItem() { Text = item.Title, Value = item.Id.ToString() });
            }

            var course = data.GetCourse(int.Parse(CourseId_List.SelectedValue));
            CourseDepartment_Text.Text = course.Department;
            CourseProfessor_Text.Text = course.Professor.FirstName + " " + course.Professor.LastName;
            CourseTime_Text.Text = course.StartTime + ":00 - " + course.EndTime + ":00";
            CourseCapacity_Text.Text = data.GetEnrolledStudentsByCourse(course).Count + "/" + course.Capacity;
            CourseCredit_Text.Text = course.Credit.ToString();
        }

        protected void CurrentCourse(object sender, EventArgs e)
        {
            var course = data.GetCourse(int.Parse(CourseId_List.SelectedValue));
            CourseDepartment_Text.Text = course.Department;
            CourseProfessor_Text.Text = course.Professor.FirstName + " " + course.Professor.LastName;
            CourseTime_Text.Text = course.StartTime + ":00 - " + course.EndTime + ":00";
            CourseCapacity_Text.Text = data.GetEnrolledStudentsByCourse(course).Count + "/" + course.Capacity;
            CourseCredit_Text.Text = course.Credit.ToString();
        }

        protected IEnumerable<CourseDAO> GetCartedClasses(int pid)
        {
            return data.GetCartedCourses(pid);
        }

        protected IEnumerable<CourseDAO> GetStudentSchedule(int pid)
        {
            return data.GetStudentSchedule(data.GetPerson(pid));
        }

        protected void UpdateId_Click(object sender, EventArgs e)
        {
            if (PersonId_Text.Text != "")
            {
                foreach (var person in data.GetPeople())
                {
                    if (person.Id == int.Parse(PersonId_Text.Text))
                    {
                        PersonName_Text.Text = person.FirstName + " " + person.LastName;
                        CartStatus.Text = "";
                        DropStatus.Text = "";
                        RegisterStatus.Text = "";

                        CourseId2_List.Items.Clear();
                        CourseId3_List.Items.Clear();

                        foreach (var item in data.GetCartedCourses(person.Id))
                        {
                            CourseId2_List.Items.Add(new ListItem() { Text = item.Title, Value = item.Id.ToString() });
                        }

                        foreach (var item in data.GetStudentSchedule(person))
                        {
                            CourseId3_List.Items.Add(new ListItem() { Text = item.Title, Value = item.Id.ToString() });
                        }

                        return;
                    }
                }
            }

            PersonName_Text.Text = "ERROR: Invalid ID";
            CartStatus.Text = "";
            DropStatus.Text = "";
            RegisterStatus.Text = "";
        }

        protected void CartCourse_Click(object sender, EventArgs e)
        {
            if (CartCourseValidated())
            {
                CartStatus.Text = "Class successfully carted!";
                DropStatus.Text = "";
                RegisterStatus.Text = "";
            }
            else
            {
                CartStatus.Text = "Carting course failed!";
                DropStatus.Text = "";
                RegisterStatus.Text = "";
            }
        }

        protected void RegisterCourse_Click(object sender, EventArgs e)
        {
            if (RegisterCourseValidated())
            {
                RegisterStatus.Text = "Class successfully registered!";
                CartStatus.Text = "";
                DropStatus.Text = "";
            }
            else
            {
                RegisterStatus.Text = "Registering course failed!";
                CartStatus.Text = "";
                DropStatus.Text = "";
            }
        }

        protected void DropCourse1_Click(object sender, EventArgs e)
        {
            if (DropCourseValidated1())
            {
                RegisterStatus.Text = "Class successfully removed!";
                CartStatus.Text = "";
                DropStatus.Text = "";
            }
            else
            {
                RegisterStatus.Text = "Removing course failed!";
                CartStatus.Text = "";
                DropStatus.Text = "";
            }
        }

        protected void DropCourse2_Click(object sender, EventArgs e)
        {
            if (DropCourseValidated2())
            {
                DropStatus.Text = "Class successfully dropped!";
                CartStatus.Text = "";
                RegisterStatus.Text = "";
            }
            else
            {
                DropStatus.Text = "Dropping course failed!";
                CartStatus.Text = "";
                RegisterStatus.Text = "";
            }
        }

        private bool CartCourseValidated()
        {
            if (string.IsNullOrWhiteSpace(PersonId_Text.Text))
            {
                return false;
            }

            var course = data.GetCourse(int.Parse(CourseId_List.SelectedValue));
            var person = data.GetPerson(int.Parse(PersonId_Text.Text));

            var result = data.CartCourse(course, person);

            if (result)
            {
                CourseId2_List.Items.Clear();

                foreach (var item in data.GetCartedCourses(int.Parse(PersonId_Text.Text)))
                {
                    CourseId2_List.Items.Add(new ListItem() { Text = item.Title, Value = item.Id.ToString() });
                }
            }

            return result;
        }

        private bool RegisterCourseValidated()
        {
            if (string.IsNullOrWhiteSpace(PersonId_Text.Text))
            {
                return false;
            }

            var course = data.GetCourse(int.Parse(CourseId2_List.SelectedValue));
            var person = data.GetPerson(int.Parse(PersonId_Text.Text));

            var result = data.RegisterCourse(course, person);

            if (result)
            {
                CourseId2_List.Items.Clear();
                CourseId3_List.Items.Clear();

                foreach (var item in data.GetCartedCourses(int.Parse(PersonId_Text.Text)))
                {
                    CourseId2_List.Items.Add(new ListItem() { Text = item.Title, Value = item.Id.ToString() });
                }

                foreach (var item in data.GetStudentSchedule(person))
                {
                    CourseId3_List.Items.Add(new ListItem() { Text = item.Title, Value = item.Id.ToString() });
                }
            }

            return result;
        }

        private bool DropCourseValidated1()
        {
            if (string.IsNullOrWhiteSpace(PersonId_Text.Text))
            {
                return false;
            }

            var course = data.GetCourse(int.Parse(CourseId2_List.SelectedValue));
            var person = data.GetPerson(int.Parse(PersonId_Text.Text));

            var result = data.DropCourse(course, person);

            if (result)
            {
                CourseId2_List.Items.Clear();

                foreach (var item in data.GetCartedCourses(int.Parse(PersonId_Text.Text)))
                {
                    CourseId2_List.Items.Add(new ListItem() { Text = item.Title, Value = item.Id.ToString() });
                }
            }

            return result;
        }

        private bool DropCourseValidated2()
        {
            if (string.IsNullOrWhiteSpace(PersonId_Text.Text))
            {
                return false;
            }

            var course = data.GetCourse(int.Parse(CourseId3_List.SelectedValue));
            var person = data.GetPerson(int.Parse(PersonId_Text.Text));

            var result = data.DropCourse(course, person);

            if (result)
            {
                CourseId3_List.Items.Clear();

                foreach (var item in data.GetStudentSchedule(person))
                {
                    CourseId3_List.Items.Add(new ListItem() { Text = item.Title, Value = item.Id.ToString() });
                }
            }

            return result;
        }
    }
}