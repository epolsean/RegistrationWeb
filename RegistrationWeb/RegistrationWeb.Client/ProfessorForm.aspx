<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProfessorForm.aspx.cs" Inherits="RegistrationWeb.Client.ProfessorForm" %>

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
        <div class="Add_Course">
            <h3>Add</h3>
            <asp:Label runat="server" ID="CourseTitle">Title: </asp:Label>
            <asp:TextBox runat="server" ID="CourseTitle_Text"></asp:TextBox>
            <asp:Label runat="server" ID="CourseDepartment">Department: </asp:Label>
            <asp:TextBox runat="server" ID="CourseDepartment_Text"></asp:TextBox>
            <asp:Label runat="server" ID="CourseStart">Start Time: </asp:Label>
            <asp:TextBox runat="server" ID="CourseStart_Text"></asp:TextBox>
            <asp:Label runat="server" ID="CourseEnd">End Time: </asp:Label>
            <asp:TextBox runat="server" ID="CourseEnd_Text"></asp:TextBox>
            <asp:Label runat="server" ID="CourseCapacity">Capacity: </asp:Label>
            <asp:TextBox runat="server" ID="CourseCapacity_Text"></asp:TextBox>
            <asp:Label runat="server" ID="CourseCredit">Credit: </asp:Label>
            <asp:TextBox runat="server" ID="CourseCredit_Text"></asp:TextBox>
            <asp:Button runat="server" ID="AddCourseButton" OnClick="AddCourse_Click" Text="Add Course" />
            </br>
            <asp:Label runat="server" ID="AddStatus"></asp:Label>
            </br>
        </div>
        <div class="Cancel_Course">
            <h3>Cancel</h3>
            <asp:Label runat="server" ID="CourseTitle2">Title: </asp:Label>
            <asp:DropDownList runat="server" ID="CourseTitle2_List" AutoPostBack="true" OnSelectedIndexChanged="CurrentCourse"></asp:DropDownList>
            <asp:Label runat="server" ID="CancelStatus"></asp:Label>
            </br>
            <asp:Button runat="server" ID="CancelCourseButton" OnClick="CancelCourse_Click" Text="Cancel Course" />
            </br>
            <asp:Label runat="server" ID="CourseDepartment2">Department: </asp:Label>
            <asp:Label runat="server" ID="CourseDepartment2_Text"></asp:Label>
            </br>
            <asp:Label runat="server" ID="CourseTime2">Period: </asp:Label>
            <asp:Label runat="server" ID="CourseTime2_Text"></asp:Label>
            </br>
            <asp:Label runat="server" ID="CourseCapacity2">Capacity: </asp:Label>
            <asp:Label runat="server" ID="CourseCapacity2_Text"></asp:Label>
            </br>
            <asp:Label runat="server" ID="CourseCredit2">Credit: </asp:Label>
            <asp:Label runat="server" ID="CourseCredit2_Text"></asp:Label>
            </br>
        </div>
        <div class="Modify_Course">
            <h3>Modify</h3>
            <asp:Label runat="server" ID="CourseTitle3">Title: </asp:Label>
            <asp:DropDownList runat="server" ID="CourseTitle3_List" AutoPostBack="true" OnSelectedIndexChanged="CurrentCourse2"></asp:DropDownList>
            <asp:Label runat="server" ID="ModifyStatus"></asp:Label>
            </br>
            <asp:Button runat="server" ID="ModifyCourseButton" OnClick="ModifyCourse_Click" Text="Modify Course" />
            </br>
            <asp:Label runat="server" ID="CourseStart3">Start Time: </asp:Label>
            <asp:TextBox runat="server" ID="CourseStart3_Text"></asp:TextBox>
            </br>
            <asp:Label runat="server" ID="CourseEnd3">End Time: </asp:Label>
            <asp:TextBox runat="server" ID="CourseEnd3_Text"></asp:TextBox>
            </br>
            <asp:Label runat="server" ID="CourseCapacity3">Capacity: </asp:Label>
            <asp:TextBox runat="server" ID="CourseCapacity3_Text"></asp:TextBox>
            </br>
        </div>
        <div class="Enrolled_By_Course">
            <h3>Currently Enrolled</h3>
            <asp:Label runat="server" ID="CourseTitle4">Title: </asp:Label>
            <asp:DropDownList runat="server" ID="CourseTitle4_List" AutoPostBack="true"></asp:DropDownList>
            </br>
            <%if(PersonId_Text.Text != string.Empty && PersonName_Text.Text != "ERROR: Invalid ID")
                {
                    foreach (var person in GetEnrolled(int.Parse(CourseTitle4_List.SelectedValue)))
                    {
                        Response.Write(string.Format("{0}, {1}</br>", person.LastName, person.FirstName));
                    }
                } %>
            </br>
        </div>
        </br>
        <a href="StudentForm.aspx">Go To Student Form</a>
        <a href="RegistryForm.aspx">Go To Registry Form</a>
    </form>
</body>
</html>
