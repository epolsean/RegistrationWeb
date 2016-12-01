<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="entryform.aspx.cs" Inherits="RegistrationWeb.Client.entryform" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="Current_User">
            <asp:Label runat="server" ID="PersonId">Person ID: </asp:Label>
            <asp:TextBox runat="server" ID="PersonId_Text"></asp:TextBox>
            <asp:Button runat="server" ID="PersonId_Button" OnClick="UpdateId_Click" Text="Update" />
            </br>
            <asp:Label runat="server" ID="PersonName">User: </asp:Label>
            <asp:Label runat="server" ID="PersonName_Text">N/A</asp:Label>
            </br></br>
        </div>
        <div class="All_Classes">
            <asp:Label runat="server" ID="CourseId">Course Name: </asp:Label>
            <asp:DropDownList runat="server" ID="CourseId_List" AutoPostBack="true" OnSelectedIndexChanged="CurrentCourse"></asp:DropDownList>
            <asp:Label runat="server" ID="CartStatus"></asp:Label>
            </br>
            <asp:Label runat="server" ID="CourseDepartment">Department: </asp:Label>
            <asp:Label runat="server" ID="CourseDepartment_Text"></asp:Label>
            </br>
            <asp:Label runat="server" ID="CourseProfessor">Professor: </asp:Label>
            <asp:Label runat="server" ID="CourseProfessor_Text"></asp:Label>
            </br>
            <asp:Label runat="server" ID="CourseTime">Period: </asp:Label>
            <asp:Label runat="server" ID="CourseTime_Text"></asp:Label>
            </br>
            <asp:Label runat="server" ID="CourseCapacity">Capacity: </asp:Label>
            <asp:Label runat="server" ID="CourseCapacity_Text"></asp:Label>
            </br>
            <asp:Label runat="server" ID="CourseCredit">Credit: </asp:Label>
            <asp:Label runat="server" ID="CourseCredit_Text"></asp:Label>
            </br>
            <asp:Button runat="server" ID="CartCourseButton" OnClick="CartCourse_Click" Text="Cart Course" />
            </br>
        </div>
        <div class="Carted_Classes">
            <h3>Cart</h3>
            <asp:Label runat="server" ID="CourseId2">Course Name: </asp:Label>
            <asp:DropDownList runat="server" ID="CourseId2_List"></asp:DropDownList>
            <asp:Label runat="server" ID="RegisterStatus"></asp:Label>
            </br>
            <asp:Button runat="server" ID="RegisterCourseButton" OnClick="RegisterCourse_Click" Text="Register Course" />
            <asp:Button runat="server" ID="RemoveCourseButton" OnClick="DropCourse1_Click" Text="Remove Course" />
            </br></br>
            <%if(PersonId_Text.Text != string.Empty && PersonName_Text.Text != "ERROR: Invalid ID")
                {
                    foreach (var course in GetCartedClasses(int.Parse(PersonId_Text.Text)))
                    {
                        Response.Write(string.Format("Title: {0}</br>", course.Title));
                        Response.Write(string.Format("Dept: {0}</br>", course.Department));
                        Response.Write(string.Format("Prof: {0} {1}</br>", course.Professor.FirstName, course.Professor.LastName));
                        Response.Write(string.Format("Period: {0}:00-{1}:00</br>", course.StartTime, course.EndTime));
                        Response.Write(string.Format("Capacity: {0}</br>", course.Capacity));
                        Response.Write(string.Format("Credit: {0}</br></br>", course.Credit));
                    }
                } %>
        </div>
        <div class="Registered_Classes">
            <h3>Schedule</h3>
            <asp:Label runat="server" ID="CourseId3">Course Name: </asp:Label>
            <asp:DropDownList runat="server" ID="CourseId3_List"></asp:DropDownList>
            <asp:Label runat="server" ID="DropStatus"></asp:Label>
            </br>
            <asp:Button runat="server" ID="DropCourseButton" OnClick="DropCourse2_Click" Text="Drop Course" />
            </br></br>
            <%if (PersonId_Text.Text != string.Empty && PersonName_Text.Text != "ERROR: Invalid ID")
                {
                    foreach (var course in GetStudentSchedule(int.Parse(PersonId_Text.Text)))
                    {
                        Response.Write(string.Format("Title: {0}</br>", course.Title));
                        Response.Write(string.Format("Dept: {0}</br>", course.Department));
                        Response.Write(string.Format("Prof: {0} {1}</br>", course.Professor.FirstName, course.Professor.LastName));
                        Response.Write(string.Format("Period: {0}:00-{1}:00</br>", course.StartTime, course.EndTime));
                        Response.Write(string.Format("Capacity: {0}</br>", course.Capacity));
                        Response.Write(string.Format("Credit: {0}</br></br>", course.Credit));
                    }
                } %>
        </div>
    </form>
</body>
</html>
