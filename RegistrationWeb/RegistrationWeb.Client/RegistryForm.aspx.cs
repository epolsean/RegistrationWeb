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
    public partial class RegistryForm : System.Web.UI.Page
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
            CourseStatus_List.Items.Clear();
            CourseStatus_List.Items.Add(new ListItem() { Text = "Open", Value = 1.ToString() });
            CourseStatus_List.Items.Add(new ListItem() { Text = "Full", Value = 2.ToString() });

            CourseTitle_List.Items.Clear();
            PersonId_List.Items.Clear();
            PersonId2_List.Items.Clear();
            //PersonId3_List.Items.Clear();

            foreach (var item in data.GetCourses())
            {
                CourseTitle_List.Items.Add(new ListItem() { Text = item.Title, Value = item.Id.ToString() });
            }

            foreach (var item in data.GetPeople())
            {
                if(item.PersonType.Id == 1)
                {
                    PersonId_List.Items.Add(new ListItem() { Text = item.Id.ToString(), Value = item.Id.ToString() });
                    PersonId2_List.Items.Add(new ListItem() { Text = item.Id.ToString(), Value = item.Id.ToString() });
                }
                /*else if(item.PersonType.Id == 2)
                {
                    PersonId3_List.Items.Add(new ListItem() { Text = item.Id.ToString(), Value = item.Id.ToString() });
                }*/
            }
        }

        protected IEnumerable<CourseDAO> GetAllOpenCourses()
        {
            return data.GetAllOpenCourses();
        }

        protected IEnumerable<CourseDAO> GetAllFullCourses()
        {
            return data.GetAllFullCourses();
        }

        protected IEnumerable<PersonDAO> GetEnrolled(int cid)
        {
            return data.GetEnrolledStudentsByCourse(data.GetCourse(cid));
        }

        protected IEnumerable<PersonDAO> GetAllActive()
        {
            return data.GetAllActiveStudents();
        }

        protected IEnumerable<CourseDAO> GetStudentSchedule(int pid)
        {
            return data.GetStudentSchedule(data.GetPerson(pid));
        }

        protected void AddStudent_Click(object sender, EventArgs e)
        {
            if (AddStudentValidated())
            {
                AddStudentStatus.Text = "Student successfully added!";
                AddProfessorStatus.Text = "";
                RemoveStudentStatus.Text = "";
                //RemoveProfessorStatus.Text = "";
            }
            else
            {
                AddStudentStatus.Text = "Adding student failed!";
                AddProfessorStatus.Text = "";
                RemoveStudentStatus.Text = "";
                //RemoveProfessorStatus.Text = "";
            }
        }

        protected void AddProfessor_Click(object sender, EventArgs e)
        {
            if (AddProfessorValidated())
            {
                AddStudentStatus.Text = "";
                AddProfessorStatus.Text = "Professor successfully added!";
                RemoveStudentStatus.Text = "";
                //RemoveProfessorStatus.Text = "";
            }
            else
            {
                AddStudentStatus.Text = "";
                AddProfessorStatus.Text = "Adding professor failed!";
                RemoveStudentStatus.Text = "";
                //RemoveProfessorStatus.Text = "";
            }
        }

        protected void RemoveStudent_Click(object sender, EventArgs e)
        {
            if (RemoveStudentValidated())
            {
                AddStudentStatus.Text = "";
                AddProfessorStatus.Text = "";
                RemoveStudentStatus.Text = "Student successfully removed!";
                //RemoveProfessorStatus.Text = "";
            }
            else
            {
                AddStudentStatus.Text = "";
                AddProfessorStatus.Text = "";
                RemoveStudentStatus.Text = "Removing student failed!";
                //RemoveProfessorStatus.Text = "";
            }
        }

        /*protected void RemoveProfessor_Click(object sender, EventArgs e)
        {
            if (RemoveProfessorValidated())
            {
                AddStudentStatus.Text = "";
                AddProfessorStatus.Text = "";
                RemoveStudentStatus.Text = "";
                RemoveProfessorStatus.Text = "Professor successfully removed!";
            }
            else
            {
                AddStudentStatus.Text = "";
                AddProfessorStatus.Text = "";
                RemoveStudentStatus.Text = "";
                RemoveProfessorStatus.Text = "Remove professor failed!";
            }
        }*/

        private bool AddStudentValidated()
        {
            if (string.IsNullOrWhiteSpace(StudentFirstName_Text.Text) || string.IsNullOrWhiteSpace(StudentLastName_Text.Text))
            {
                return false;
            }

            var person = new PersonDAO()
            {
                FirstName = StudentFirstName_Text.Text,
                LastName = StudentLastName_Text.Text,
                PersonType = new PersonTypeDAO() { Id = 1 },
                Active = true
            };

            var result = data.AddPerson(person);

            if (result)
            {
                PersonId_List.Items.Clear();
                PersonId2_List.Items.Clear();

                foreach (var item in data.GetPeople())
                {
                    if (item.PersonType.Id == 1)
                    {
                        PersonId_List.Items.Add(new ListItem() { Text = item.Id.ToString(), Value = item.Id.ToString() });
                        PersonId2_List.Items.Add(new ListItem() { Text = item.Id.ToString(), Value = item.Id.ToString() });
                    }
                }
            }

            return result;
        }

        private bool AddProfessorValidated()
        {
            if (string.IsNullOrWhiteSpace(ProfessorFirstName_Text.Text) || string.IsNullOrWhiteSpace(ProfessorLastName_Text.Text))
            {
                return false;
            }

            var person = new PersonDAO()
            {
                FirstName = ProfessorFirstName_Text.Text,
                LastName = ProfessorLastName_Text.Text,
                PersonType = new PersonTypeDAO() { Id = 2 },
                Active = true
            };

            var result = data.AddPerson(person);

            if (result)
            {
                /*
                PersonId3_List.Items.Clear();

                foreach (var item in data.GetPeople())
                {
                    if (item.PersonType.Id == 2)
                    {
                        PersonId2_List.Items.Add(new ListItem() { Text = item.Id.ToString(), Value = item.Id.ToString() });
                    }
                }*/
            }

            return result;
        }

        private bool RemoveStudentValidated()
        {
            var person = data.GetPerson(int.Parse(PersonId2_List.SelectedValue));

            var result = data.RemoveStudent(person);

            if (result)
            {
                PersonId_List.Items.Clear();
                PersonId2_List.Items.Clear();

                foreach (var item in data.GetPeople())
                {
                    if (item.PersonType.Id == 1)
                    {
                        PersonId_List.Items.Add(new ListItem() { Text = item.Id.ToString(), Value = item.Id.ToString() });
                        PersonId2_List.Items.Add(new ListItem() { Text = item.Id.ToString(), Value = item.Id.ToString() });
                    }
                }
            }

            return result;
        }
        /*
        private bool AddProfessorValidated()
        {
            var person = data.GetPerson(int.Parse(PersonId2_List.SelectedValue));

            var result = data.(person);

            if (result)
            {
                PersonId3_List.Items.Clear();

                foreach (var item in data.GetPeople())
                {
                    if (item.PersonType.Id == 2)
                    {
                        PersonId2_List.Items.Add(new ListItem() { Text = item.Id.ToString(), Value = item.Id.ToString() });
                    }
                }
            }

            return result;
        }*/
    }
}