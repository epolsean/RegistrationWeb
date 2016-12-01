<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistryForm.aspx.cs" Inherits="RegistrationWeb.Client.RegistryForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="Course_Status">
            <h3>Course Status</h3>
            <asp:Label runat="server" ID="CourseStatus">Status: </asp:Label>
            <asp:DropDownList runat="server" ID="CourseStatus_List" AutoPostBack="true"></asp:DropDownList>
            </br>
            <%if (CourseStatus_List.SelectedItem.Text.ToLower() == "open")
                {
                    foreach (var course in GetAllOpenCourses())
                    {
                        Response.Write(string.Format("Title: {0}</br>", course.Title));
                        Response.Write(string.Format("Dept: {0}</br>", course.Department));
                        Response.Write(string.Format("Prof: {0} {1}</br>", course.Professor.FirstName, course.Professor.LastName));
                        Response.Write(string.Format("Period: {0}:00-{1}:00</br>", course.StartTime, course.EndTime));
                        Response.Write(string.Format("Capacity: {0}</br>", course.Capacity));
                        Response.Write(string.Format("Credit: {0}</br></br>", course.Credit));
                    }
                }
                else if(CourseStatus_List.SelectedItem.Text.ToLower() == "full")
                {
                    foreach (var course in GetAllFullCourses())
                    {
                        Response.Write(string.Format("Title: {0}</br>", course.Title));
                        Response.Write(string.Format("Dept: {0}</br>", course.Department));
                        Response.Write(string.Format("Prof: {0} {1}</br>", course.Professor.FirstName, course.Professor.LastName));
                        Response.Write(string.Format("Period: {0}:00-{1}:00</br>", course.StartTime, course.EndTime));
                        Response.Write(string.Format("Capacity: {0}</br>", course.Capacity));
                        Response.Write(string.Format("Credit: {0}</br></br>", course.Credit));
                    }
                }
               %>
            </br>
        </div>
        <div class="Enrolled_By_Course">
            <h3>Enrolled Students By Course</h3>
            <asp:Label runat="server" ID="CourseTitle">Title: </asp:Label>
            <asp:DropDownList runat="server" ID="CourseTitle_List" AutoPostBack="true"></asp:DropDownList>
            </br>
            <%foreach (var person in GetEnrolled(int.Parse(CourseTitle_List.SelectedValue)))
                {
                    Response.Write(string.Format("{0}, {1}</br>", person.LastName, person.FirstName));
                } %>
            </br>
        </div>
        <div class="Active_Students">
            <h3>All Active Students</h3>
            <%foreach (var person in GetAllActive())
                {
                    Response.Write(string.Format("{0}, {1}</br>", person.LastName, person.FirstName));
                } %>
            </br>
        </div>
        <div class="Student_Schedule">
            <h3>Student's Schedule</h3>
            <asp:Label runat="server" ID="PersonId">Student ID: </asp:Label>
            <asp:DropDownList runat="server" ID="PersonId_List" AutoPostBack="true"></asp:DropDownList>
            </br>
            <%foreach (var course in GetStudentSchedule(int.Parse(PersonId_List.SelectedValue)))
                {
                    Response.Write(string.Format("Title: {0}</br>", course.Title));
                    Response.Write(string.Format("Dept: {0}</br>", course.Department));
                    Response.Write(string.Format("Prof: {0} {1}</br>", course.Professor.FirstName, course.Professor.LastName));
                    Response.Write(string.Format("Period: {0}:00-{1}:00</br>", course.StartTime, course.EndTime));
                    Response.Write(string.Format("Capacity: {0}</br>", course.Capacity));
                    Response.Write(string.Format("Credit: {0}</br></br>", course.Credit));
                } %>
            </br>
        </div>
        <div class="Add_Student">
            <h3>Add Student</h3>
            <asp:Label runat="server" ID="StudentFirstName">First Name: </asp:Label>
            <asp:TextBox runat="server" ID="StudentFirstName_Text"></asp:TextBox>
            <asp:Label runat="server" ID="StudentLastName">Last Name: </asp:Label>
            <asp:TextBox runat="server" ID="StudentLastName_Text"></asp:TextBox>
            <asp:Label runat="server" ID="AddStudentStatus"></asp:Label>
            </br>
            <asp:Button runat="server" ID="AddStudentButton" OnClick="AddStudent_Click" Text="Add Student" />
            </br>
        </div>
        <div class="Add_Professor">
            <h3>Add Professor</h3>
            <asp:Label runat="server" ID="ProfessorFirstName">First Name: </asp:Label>
            <asp:TextBox runat="server" ID="ProfessorFirstName_Text"></asp:TextBox>
            <asp:Label runat="server" ID="ProfessorLastName">Last Name: </asp:Label>
            <asp:TextBox runat="server" ID="ProfessorLastName_Text"></asp:TextBox>
            <asp:Label runat="server" ID="AddProfessorStatus"></asp:Label>
            </br>
            <asp:Button runat="server" ID="AddProfessorButton" OnClick="AddProfessor_Click" Text="Add Professor" />
            </br>
        </div>
        <div class="Remove_Student">
            <h3>Remove Student</h3>
            <asp:Label runat="server" ID="PersonId2">Student ID: </asp:Label>
            <asp:DropDownList runat="server" ID="PersonId2_List"></asp:DropDownList>
            <asp:Label runat="server" ID="RemoveStudentStatus"></asp:Label>
            </br>
            <asp:Button runat="server" ID="RemoveStudentButton" OnClick="RemoveStudent_Click" Text="Remove Student" />
            </br>
        </div>
        </br>
        <a href="StudentForm.aspx">Go To Student Form</a>
        <a href="ProfessorForm.aspx">Go To Professor Form</a>
    </form>
</body>
</html>
