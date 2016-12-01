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
    public partial class ProfessorForm : System.Web.UI.Page
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
            CourseTitle2_List.Items.Clear();
            CourseTitle3_List.Items.Clear();
            CourseTitle4_List.Items.Clear();

            if (PersonId_Text.Text != "N/A" && PersonId_Text.Text != "ERROR: Invalid ID" && PersonId_Text.Text != "")
            {
                foreach (var item in data.GetCourses())
                {
                    if (item.Professor.Id == int.Parse(PersonId_Text.Text))
                    {
                        CourseTitle2_List.Items.Add(new ListItem() { Text = item.Title, Value = item.Id.ToString() });
                        CourseTitle3_List.Items.Add(new ListItem() { Text = item.Title, Value = item.Id.ToString() });
                        CourseTitle4_List.Items.Add(new ListItem() { Text = item.Title, Value = item.Id.ToString() });
                    }
                }

                var course = data.GetCourse(int.Parse(CourseTitle2_List.SelectedValue));
                var course2 = data.GetCourse(int.Parse(CourseTitle3_List.SelectedValue));

                CourseDepartment2_Text.Text = course.Department;
                CourseTime2_Text.Text = course.StartTime + ":00 - " + course.EndTime + ":00";
                CourseCapacity2_Text.Text = data.GetEnrolledStudentsByCourse(course).Count + "/" + course.Capacity;
                CourseCredit2_Text.Text = course.Credit.ToString();
                CourseStart3_Text.Text = course2.StartTime.ToString();
                CourseEnd3_Text.Text = course2.EndTime.ToString();
                CourseCapacity3_Text.Text = course2.Capacity.ToString();

                return;
            }

            CourseDepartment2_Text.Text = "";
            CourseTime2_Text.Text = "";
            CourseCapacity2_Text.Text = "";
            CourseCredit2_Text.Text = "";
            CourseStart3_Text.Text = "";
            CourseEnd3_Text.Text = "";
            CourseCapacity3_Text.Text = "";
        }

        protected void CurrentCourse(object sender, EventArgs e)
        {
            var course = data.GetCourse(int.Parse(CourseTitle2_List.SelectedValue));
            CourseDepartment2_Text.Text = course.Department;
            CourseTime2_Text.Text = course.StartTime + ":00 - " + course.EndTime + ":00";
            CourseCapacity2_Text.Text = data.GetEnrolledStudentsByCourse(course).Count + "/" + course.Capacity;
            CourseCredit2_Text.Text = course.Credit.ToString();
        }

        protected void CurrentCourse2(object sender, EventArgs e)
        {
            var course = data.GetCourse(int.Parse(CourseTitle3_List.SelectedValue));
            CourseStart3_Text.Text = course.StartTime.ToString();
            CourseEnd3_Text.Text = course.EndTime.ToString();
            CourseCapacity3_Text.Text = course.Capacity.ToString();
        }

        protected IEnumerable<PersonDAO> GetEnrolled(int cid)
        {
            return data.GetEnrolledStudentsByCourse(data.GetCourse(cid));
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
                        AddStatus.Text = "";
                        CancelStatus.Text = "";
                        ModifyStatus.Text = "";

                        CourseTitle2_List.Items.Clear();
                        CourseTitle3_List.Items.Clear();
                        CourseTitle4_List.Items.Clear();

                        foreach (var item in data.GetCourses())
                        {
                            if (item.Professor.Id == person.Id)
                            {
                                CourseTitle2_List.Items.Add(new ListItem() { Text = item.Title, Value = item.Id.ToString() });
                                CourseTitle3_List.Items.Add(new ListItem() { Text = item.Title, Value = item.Id.ToString() });
                                CourseTitle4_List.Items.Add(new ListItem() { Text = item.Title, Value = item.Id.ToString() });
                            }
                        }

                        var course = data.GetCourse(int.Parse(CourseTitle2_List.SelectedValue));
                        var course2 = data.GetCourse(int.Parse(CourseTitle3_List.SelectedValue));

                        CourseDepartment2_Text.Text = course.Department;
                        CourseTime2_Text.Text = course.StartTime + ":00 - " + course.EndTime + ":00";
                        CourseCapacity2_Text.Text = data.GetEnrolledStudentsByCourse(course).Count + "/" + course.Capacity;
                        CourseCredit2_Text.Text = course.Credit.ToString();
                        CourseStart3_Text.Text = course2.StartTime.ToString();
                        CourseEnd3_Text.Text = course2.EndTime.ToString();
                        CourseCapacity3_Text.Text = course2.Capacity.ToString();

                        return;
                    }
                }
            }

            PersonName_Text.Text = "ERROR: Invalid ID";
            AddStatus.Text = "";
            CancelStatus.Text = "";
            ModifyStatus.Text = "";
        }

        protected void AddCourse_Click(object sender, EventArgs e)
        {
            if (AddCourseValidated())
            {
                AddStatus.Text = "Course successfully added!";
                CancelStatus.Text = "";
                ModifyStatus.Text = "";
            }
            else
            {
                AddStatus.Text = "Adding course failed!";
                CancelStatus.Text = "";
                ModifyStatus.Text = "";
            }
        }

        protected void CancelCourse_Click(object sender, EventArgs e)
        {
            if (CancelCourseValidated())
            {
                CancelStatus.Text = "Course successfully cancelled!";
                AddStatus.Text = "";
                ModifyStatus.Text = "";
            }
            else
            {
                CancelStatus.Text = "Cancelling course failed!";
                AddStatus.Text = "";
                ModifyStatus.Text = "";
            }
        }

        protected void ModifyCourse_Click(object sender, EventArgs e)
        {
            if (ModifyCourseValidated())
            {
                ModifyStatus.Text = "Course successfully modified!";
                AddStatus.Text = "";
                CancelStatus.Text = "";
            }
            else
            {
                ModifyStatus.Text = "Modifying course failed!";
                AddStatus.Text = "";
                CancelStatus.Text = "";
            }
        }

        private bool AddCourseValidated()
        {
            if (string.IsNullOrWhiteSpace(PersonId_Text.Text))
            {
                return false;
            }

            var person = data.GetPerson(int.Parse(PersonId_Text.Text));
            var course = new CourseDAO() { Title = CourseTitle_Text.Text,
                                           Department = CourseDepartment_Text.Text,
                                           Professor = person,
                                           StartTime = int.Parse(CourseStart_Text.Text),
                                           EndTime = int.Parse(CourseEnd_Text.Text),
                                           Capacity = int.Parse(CourseCapacity_Text.Text),
                                           Credit = int.Parse(CourseCredit_Text.Text),
                                           Active = true};

            var result = data.AddCourse(course);

            if (result)
            {
                CourseTitle_Text.Text = "";
                CourseDepartment_Text.Text = "";
                CourseStart_Text.Text = "";
                CourseEnd_Text.Text = "";
                CourseCapacity_Text.Text = "";
                CourseCredit_Text.Text = "";
                CourseTitle2_List.Items.Clear();
                CourseTitle3_List.Items.Clear();
                CourseTitle4_List.Items.Clear();

                foreach (var item in data.GetCourses())
                {
                    if (item.Professor.Id == person.Id)
                    {
                        CourseTitle2_List.Items.Add(new ListItem() { Text = item.Title, Value = item.Id.ToString() });
                        CourseTitle3_List.Items.Add(new ListItem() { Text = item.Title, Value = item.Id.ToString() });
                        CourseTitle4_List.Items.Add(new ListItem() { Text = item.Title, Value = item.Id.ToString() });
                    }
                }
            }

            return result;
        }

        private bool CancelCourseValidated()
        {
            if (string.IsNullOrWhiteSpace(PersonId_Text.Text))
            {
                return false;
            }

            var course = data.GetCourse(int.Parse(CourseTitle2_List.SelectedValue));

            var result = data.CancelCourse(course);

            if (result)
            {
                CourseTitle2_List.Items.Clear();
                CourseTitle3_List.Items.Clear();
                CourseTitle4_List.Items.Clear();

                foreach (var item in data.GetCourses())
                {
                    if (item.Professor.Id == int.Parse(PersonId_Text.Text))
                    {
                        CourseTitle2_List.Items.Add(new ListItem() { Text = item.Title, Value = item.Id.ToString() });
                        CourseTitle3_List.Items.Add(new ListItem() { Text = item.Title, Value = item.Id.ToString() });
                        CourseTitle4_List.Items.Add(new ListItem() { Text = item.Title, Value = item.Id.ToString() });
                    }
                }
            }

            return result;
        }

        private bool ModifyCourseValidated()
        {
            if (string.IsNullOrWhiteSpace(PersonId_Text.Text))
            {
                return false;
            }

            var course = data.GetCourse(int.Parse(CourseTitle3_List.SelectedValue));

            var result = data.ModifyCourse(course, int.Parse(CourseCapacity3_Text.Text), int.Parse(CourseStart3_Text.Text), int.Parse(CourseEnd3_Text.Text));

            if (result)
            {
                CourseTitle2_List.Items.Clear();
                CourseTitle3_List.Items.Clear();
                CourseTitle4_List.Items.Clear();

                foreach (var item in data.GetCourses())
                {
                    if (item.Professor.Id == int.Parse(PersonId_Text.Text))
                    {
                        CourseTitle2_List.Items.Add(new ListItem() { Text = item.Title, Value = item.Id.ToString() });
                        CourseTitle3_List.Items.Add(new ListItem() { Text = item.Title, Value = item.Id.ToString() });
                        CourseTitle4_List.Items.Add(new ListItem() { Text = item.Title, Value = item.Id.ToString() });
                    }
                }
            }

            return result;
        }
    }
}