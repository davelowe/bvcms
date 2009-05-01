﻿<%@ Page Language="C#" StylesheetTheme="Standard" AutoEventWireup="True" CodeBehind="Users.aspx.cs" Inherits="CMSWeb.Admin.Users" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="CustomControls" Namespace="CustomControls" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Management</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <script type="text/javascript">
        function showDialog(parentButtonId) {
            var modalPopupBehaviorCtrl = $find('<%= searchpopup.ClientID %>');
            modalPopupBehaviorCtrl.show();
            $get('<%= sd.ClientID %>').src = "../SearchDialog.aspx?selectSingle=1&parentButton=" + parentButtonId;
        }
        function AddSelected(parentButtonId) // called by SearchDialog.aspx inside iframe
        {
            $get(parentButtonId).click();
        }
    </script>

    <asp:HyperLink ID="HyperLink1" NavigateUrl="~/" runat="server">HomePage</asp:HyperLink>
            <asp:TextBox runat="server" ID="TextBox1" BorderStyle="Solid" />
            <asp:Button ID="Button1" runat="server" Text="Search for Users" OnClick="SearchForUsers" /><br />
            <asp:DataPager ID="pager1" PagedControlID="ListView1" runat="server">
                <Fields>
                    <cc1:PagerField NextPageImageUrl="~/images/arrow_right2.gif" PreviousPageImageUrl="~/images/arrow_left.gif" />
                </Fields>
            </asp:DataPager>
            <asp:ListView ID="ListView1" runat="server" DataKeyNames="UserId" DataSourceID="ObjectDataSourceMembershipUser"
                InsertItemPosition="LastItem" OnItemCommand="ListView1_ItemCommand" OnItemDeleted="ListView1_ItemDeleted"
                OnPagePropertiesChanging="ListView1_PagePropertiesChanging" OnSorting="ListView1_Sorting"
                OnItemCreated="ListView1_ItemCreated">
                <ItemTemplate>
                    <tr style="background-color: <%# (Container.DataItemIndex % 2 == 0)?"#eee":"#fff" %>">
                        <td>
                            <asp:LinkButton ID="SelectButton" CommandName="Select" runat="server">select</asp:LinkButton>
                        </td>
                        <td>
                            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%#Eval("UserId", "~/Admin/Activity.aspx?uid={0}") %>'
                                Text='<%# Eval("Username") %>'></asp:HyperLink>
                        </td>
                        <td>
                            <asp:Label ID="UserNameLabel" runat="server" Text='<%# Eval("Name2") %>' />
                        </td>
                        <td>
                            <asp:CheckBox ID="ApprovedCheckBox" runat="server" Checked='<%# Eval("IsApproved") %>'
                                Enabled="false" />
                        </td>
                        <td>
                            <asp:CheckBox ID="ChgPwdCheckBox" runat="server" Checked='<%# Eval("MustChangePassword") %>'
                                Enabled="false" />
                        </td>
                        <td>
                            <asp:CheckBox ID="LockedCheckBox" runat="server" Checked='<%# Eval("IsLockedOut") %>'
                                Enabled="false" />
                        </td>
                        <td>
                            <asp:CheckBox ID="OnlineCheckBox" runat="server" Checked='<%# Eval("IsOnLine") %>'
                                Enabled="false" />
                        </td>
                        <td>
                            <asp:HyperLink ID="EmailAddressLink" runat="server" NavigateUrl='<%# "mailto:" + Eval("Name") + "<" + Eval("EmailAddress") + ">" %>'
                                Text='<%# Eval("EmailAddress") %>' />
                        </td>
                        <td>
                            <asp:Label ID="LastActivityLabel" runat="server" Text='<%# Eval("LastActivityDate", "{0:MM/dd/yy h:mm t}") %>' />
                        </td>
                    </tr>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <table runat="server" style="">
                        <tr>
                            <td>
                                No data was returned.
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <InsertItemTemplate>
                    <tr id="Tr2" runat="server" style="">
                        <th>
                        </th>
                        <th>
                            User
                        </th>
                        <th>
                        </th>
                        <th>
                            Appr
                        </th>
                        <th>
                            Pwd
                        </th>
                        <th>
                            Lock
                        </th>
                        <th>
                        </th>
                        <th>
                        </th>
                        <th>
                        </th>
                    </tr>
                    <tr style="">
                        <td>
                            <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" />
                            <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" />
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="UsernameTextBox" runat="server" Text='<%# Bind("Username") %>' />
                        </td>
                        <td>
                            <asp:CheckBox ID="ApprovedCheckBox" runat="server" Checked='<%# Bind("IsApproved") %>' />
                        </td>
                        <td>
                            <asp:CheckBox ID="ChgPwdCheckBox" runat="server" Checked='<%# Bind("MustChangePassword") %>' />
                        </td>
                        <td>
                            <asp:CheckBox ID="LockedCheckBox" runat="server" Checked='<%# Bind("IsLockedOut") %>' />
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="8">
                            Email:<asp:TextBox ID="EmailAddressTextBox" runat="server" Text='<%# Bind("EmailAddress") %>'></asp:TextBox>
                            Password:<asp:TextBox ID="PasswordTextBox" runat="server" Text='<%# Bind("PasswordSetOnly") %>'></asp:TextBox>
                        </td>
                    </tr>
                </InsertItemTemplate>
                <LayoutTemplate>
                    <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                        <tr runat="server" style="">
                            <th runat="server">
                            </th>
                            <th runat="server">
                                <asp:LinkButton ID="LinkButton0" CommandName="Sort" CommandArgument="Username" runat="server">User</asp:LinkButton>
                            </th>
                            <th runat="server">
                                <asp:LinkButton ID="LinkButton1" CommandName="Sort" CommandArgument="Name" runat="server">Name</asp:LinkButton>
                            </th>
                            <th runat="server">
                                <asp:LinkButton ID="LinkButton2" CommandName="Sort" CommandArgument="IsApproved"
                                    runat="server">Appr</asp:LinkButton>
                            </th>
                            <th runat="server">
                                <asp:LinkButton ID="LinkButton3" CommandName="Sort" CommandArgument="MustChangePassword"
                                    runat="server">Pwd</asp:LinkButton>
                            </th>
                            <th runat="server">
                                <asp:LinkButton ID="LinkButton4" CommandName="Sort" CommandArgument="IsLockedOut"
                                    runat="server">Lock</asp:LinkButton>
                            </th>
                            <th runat="server">
                                <asp:LinkButton ID="LinkButton5" CommandName="Sort" CommandArgument="IsOnLine" runat="server">Online</asp:LinkButton>
                            </th>
                            <th runat="server">
                                Email
                            </th>
                            <th runat="server">
                                <asp:LinkButton ID="LinkButton7" CommandName="Sort" CommandArgument="LastActivityDate"
                                    runat="server">Activity</asp:LinkButton>
                            </th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server">
                        </tr>
                    </table>
                </LayoutTemplate>
                <EditItemTemplate>
                    <tr style="">
                        <td>
                            <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />
                            <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
                        </td>
                        <td>
                            <asp:TextBox ID="UsernameTextBox" runat="server" Text='<%# Bind("Username") %>' />
                        </td>
                        <td>
                            <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name2") %>' />
                        </td>
                        <td>
                            <asp:CheckBox ID="ApprovedCheckBox" runat="server" Checked='<%# Bind("IsApproved") %>' />
                        </td>
                        <td>
                            <asp:CheckBox ID="ChgPwdCheckBox" runat="server" Checked='<%# Bind("MustChangePassword") %>' />
                        </td>
                        <td>
                            <asp:CheckBox ID="LockedCheckBox" runat="server" Checked='<%# Bind("IsLockedOut") %>' />
                        </td>
                        <td>
                            <asp:CheckBox ID="OnlineCheckBox" runat="server" Enabled="false" Checked='<%# Eval("IsOnLine") %>' />
                        </td>
                        <td>
                            <asp:HyperLink ID="EmailAddressLink" runat="server" NavigateUrl='<%# "mailto:" + Eval("Name") + "<" + Eval("EmailAddress") + ">" %>'
                                Text='<%# Eval("EmailAddress") %>' />
                        </td>
                        <td>
                            <asp:Label ID="LastActivityLabel" runat="server" Text='<%# Eval("LastActivityDate", "{0:MM/dd/yy h:mm t}") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="8">
                            Email:<asp:TextBox ID="EmailAddressTextBox" runat="server" Text='<%# Bind("EmailAddress") %>'></asp:TextBox>
                            Password:<asp:TextBox ID="PasswordTextBox" runat="server" Text='<%# Bind("PasswordSetOnly") %>'></asp:TextBox>
                            PeopleId:<asp:TextBox ID="PeopleIdTextBox" runat="server" Text='<%# Bind("PeopleId") %>'></asp:TextBox>
                            <a href="javascript:showDialog('<%= AddSelectedPerson.ClientID %>')">search</a>
                        </td>
                    </tr>
                </EditItemTemplate>
                <SelectedItemTemplate>
                    <tr style="">
                        <td>
                            <asp:Button ID="SelectButton" CommandName="Deselect" Text="Deselect" runat="server">
                            </asp:Button>
                            <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Are you sure?')" />
                            <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                        </td>
                        <td>
                            <asp:Label ID="UserLabel" runat="server" Text='<%# Eval("Username") %>' />
                        </td>
                        <td>
                            <asp:Label ID="UserNameLabel" runat="server" Text='<%# Eval("Name2") %>' />
                        </td>
                        <td>
                            <asp:CheckBox ID="ApprovedCheckBox" runat="server" Checked='<%# Eval("IsApproved") %>'
                                Enabled="false" />
                        </td>
                        <td>
                            <asp:CheckBox ID="ChgPwdCheckBox" runat="server" Checked='<%# Eval("MustChangePassword") %>'
                                Enabled="false" />
                        </td>
                        <td>
                            <asp:CheckBox ID="LockedCheckBox" runat="server" Checked='<%# Eval("IsLockedOut") %>'
                                Enabled="false" />
                        </td>
                        <td>
                            <asp:CheckBox ID="OnlineCheckBox" runat="server" Checked='<%# Eval("IsOnLine") %>'
                                Enabled="false" />
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:Label ID="LastActivityLabel" runat="server" Text='<%# Eval("LastActivityDate", "{0:MM/dd/yy h:mm t}") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="8">
                            Email:<asp:HyperLink ID="EmailAddressLink" runat="server" NavigateUrl='<%# "mailto:" + Eval("Name") + "<" + Eval("EmailAddress") + ">" %>'
                                Text='<%# Eval("EmailAddress") %>' /> 
                            <br />
                            <asp:CheckBoxList ID="Roles" runat="server" DataTextField="RoleName" DataValueField="UserName"
                                DataSourceID="RoleData" OnDataBound="RolesCheckBoxList_DataBound" AutoPostBack="true"
                                OnSelectedIndexChanged="RolesCheckBoxList_SelectedIndexChanged">
                            </asp:CheckBoxList>
                            <%--                        Created: 
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("CreationDate", "{0:M/d/yy") %>'></asp:Label>
                        LastPasswordChangedDate: 
                        <asp:Label ID="Label5" runat="server" Text='<%# Eval("LastPasswordChangedDate", "{0:M/d/yy") %>'></asp:Label>
--%>
                        </td>
                    </tr>
                </SelectedItemTemplate>
            </asp:ListView>
            <asp:DataPager ID="pager2" PagedControlID="ListView1" runat="server">
                <Fields>
                    <cc1:PagerField NextPageImageUrl="~/images/arrow_right2.gif" PreviousPageImageUrl="~/images/arrow_left.gif" />
                </Fields>
            </asp:DataPager>
            <asp:CustomValidator ID="CheckNewUser" runat="server" ErrorMessage="User already exists"></asp:CustomValidator>
            <asp:Label ID="LabelInsertMessage" runat="server"></asp:Label>
    <asp:Button ID="ButtonCreateNewRole" runat="server" OnClick="ButtonCreateNewRole_Click"
        Text="Create New Role" />
    <asp:TextBox ID="TextBoxCreateNewRole" runat="server"></asp:TextBox>
    <asp:ObjectDataSource ID="ObjectDataSourceMembershipUser" runat="server" DeleteMethod="Delete"
        SelectMethod="GetUsers" TypeName="CMSPresenter.UserController" UpdateMethod="Update"
        SortParameterName="sortExpression" OnInserted="ObjectDataSourceMembershipUser_Inserted"
        EnableViewState="False" EnablePaging="True" SelectCountMethod="Count" StartRowIndexParameterName="startIndex"
        InsertMethod="Insert">
        <DeleteParameters>
            <asp:Parameter Name="UserId" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="IsApproved" Type="Boolean" />
            <asp:Parameter Name="MustChangePassword" Type="Boolean" />
            <asp:Parameter Name="IsLockedOut" Type="Boolean" />
            <asp:Parameter Name="PeopleId" Type="Int32" />
            <asp:Parameter Name="EmailAddress" Type="String" />
            <asp:Parameter Name="Username" Type="String" />
            <asp:Parameter Name="PasswordSetOnly" Type="String" />
            <asp:Parameter Name="UserId" Type="Int32" />
        </UpdateParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="TextBox1" Name="name" Type="String" />
            <asp:Parameter Name="sortExpression" Type="String" />
            <asp:Parameter Name="startIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="IsApproved" Type="Boolean" />
            <asp:Parameter Name="MustChangePassword" Type="Boolean" />
            <asp:Parameter Name="IsLockedOut" Type="Boolean" />
            <asp:Parameter Name="PeopleId" Type="Int32" />
            <asp:Parameter Name="EmailAddress" Type="String" />
            <asp:Parameter Name="Username" Type="String" />
            <asp:Parameter Name="PasswordSetOnly" Type="String" />
        </InsertParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="RoleData" runat="server" SelectMethod="GetRoles" TypeName="CMSPresenter.RoleController">
    </asp:ObjectDataSource>
    <asp:Panel runat="server" ID="Panel1" CssClass="modalPopup SearchDialog" Style="display: none">
        <div>
            <iframe id="sd" runat="server" width="600" height="450" frameborder="0" scrolling="yes"
                marginheight="0" marginwidth="0" />
        </div>
        <asp:Button ID="AddSelectedPerson" runat="server" OnClick="AddSelectedPerson_Click"
            Text="Add Selected" />
        <asp:Button ID="cmdClose" runat="server" Text="Cancel" />
    </asp:Panel>
    <cc2:ModalPopupExtender ID="searchpopup" runat="server" TargetControlID="HiddenDoNothingButton"
        PopupControlID="Panel1" BackgroundCssClass="modalBackground" DropShadow="true"
        CancelControlID="cmdClose" OkControlID="cmdClose" />
    <asp:LinkButton ID="HiddenDoNothingButton" runat="server" Style="display: none"></asp:LinkButton>
    </form>
</body>
</html>