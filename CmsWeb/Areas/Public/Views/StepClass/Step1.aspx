﻿<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site3.Master" Inherits="System.Web.Mvc.ViewPage<CMSWeb.Models.StepClassModel>" %>

<asp:Content ID="registerHead" ContentPlaceHolderID="TitleContent" runat="server">
    <title>Step 1 Registration</title>
</asp:Content>

<asp:Content ID="registerContent" ContentPlaceHolderID="MainContent" runat="server">

    <%= Html.ValidationSummary() %>
    <% using (Html.BeginForm()) { %>
        <div>
            <fieldset>
                <table style="empty-cells:show">
                <col style="width: 13em; text-align:right" />
                <col />
                <col />
                <tr>
                    <td><label for="first">First Name</label></td>
                    <td><%= Html.TextBox("first") %></td>
                    <td><%= Html.ValidationMessage("first") %></td>
                </tr>
                <tr>
                    <td><label for="last">Last Name</label></td>
                    <td><%= Html.TextBox("last") %></td>
                    <td><%= Html.ValidationMessage("last") %></td>
                </tr>
                <tr>
                    <td><label for="dob">Date of Birth</label></td>
                    <td><%= Html.TextBox("dob") %></td>
                    <td><%= Html.ValidationMessage("dob") %></td>
                </tr>
                <tr>
                    <td><label for="phone">Phone #</label></td>
                    <td><%= Html.TextBox("phone")%></td>
                    <td><%= Html.ValidationMessage("phone")%></td>
                </tr>
                <tr>
                    <td><label for="email">Email</label></td>
                    <td><%= Html.TextBox("email") %></td>
                    <td><%= Html.ValidationMessage("email") %></td>
                </tr>
                <tr>
                    <td><label for="MeetingId">Step 1 Class Dates</label></td>
                    <td><%= Html.DropDownList("MeetingId", Model.AvailableClasses("Step 1"))%></td>
                    <td><%= Html.ValidationMessage("MeetingId")%></td>
                </tr>
                <tr>
                    <td>&nbsp;</td><td><input type="submit" value="Submit" /></td>
                </tr>
                </table>
            </fieldset>
            If you are having difficulty registering online, please call Gail Stewart at 347-5763.
        </div>
    <% } %>
</asp:Content>