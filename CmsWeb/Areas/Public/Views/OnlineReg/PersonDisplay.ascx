﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CMSWeb.Models.OnlineRegPersonModel>" %>
<%=Html.Hidden3("m.List[" + Model.index + "].PeopleId", Model.PeopleId) %>
<%=Html.Hidden3("m.List[" + Model.index + "].Found", Model.Found) %>
<%=Html.Hidden3("m.List[" + Model.index + "].IsNew", Model.IsNew) %>
<%=Html.Hidden3("m.List[" + Model.index + "].OtherOK", Model.OtherOK) %>
<%=Html.Hidden3("m.List[" + Model.index + "].ShowAddress", Model.ShowAddress) %>
<%=Html.Hidden3("m.List[" + Model.index + "].first", Model.first) %>
<%=Html.Hidden3("m.List[" + Model.index + "].last", Model.last) %>
<%=Html.Hidden3("m.List[" + Model.index + "].dob", Model.dob) %>
<%=Html.Hidden3("m.List[" + Model.index + "].phone", Model.phone) %>
<%=Html.Hidden3("m.List[" + Model.index + "].homecell", Model.homecell) %>
<%=Html.Hidden3("m.List[" + Model.index + "].address", Model.address) %>
<%=Html.Hidden3("m.List[" + Model.index + "].email", Model.email) %>
<%=Html.Hidden3("m.List[" + Model.index + "].zip", Model.zip) %>
<%=Html.Hidden3("m.List[" + Model.index + "].city", Model.city) %>
<%=Html.Hidden3("m.List[" + Model.index + "].state", Model.state) %>
<%=Html.Hidden3("m.List[" + Model.index + "].gender", Model.gender) %>
<%=Html.Hidden3("m.List[" + Model.index + "].married", Model.married) %>
<table>
    <tr>
        <td><label for="first">First Name</label></td>
        <td><%=Model.first %>
        </td>
    </tr>
    <tr>
        <td><label for="last">Last Name</label></td>
        <td><%=Model.last %>
        </td>
    </tr>
     <tr>
        <td><label for="dob">Date of Birth</label></td>
        <td><%=Model.birthday.ToShortDateString() %> <span><%=Model.age %></span>
        </td>
    </tr>
    <tr>
        <td><label for="phone">Phone</label></td>
        <td><%=Model.phone.FmtFone(Model.homecell) %>
        </td>
    </tr>
    <% if (Model.email.HasValue())
       { %>
    <tr>
        <td><label for="email">Contact Email</label></td>
        <td><%=Model.email %>
        </td>
    </tr>
    <% }
       if (Model.ShowAddress)
       {
           Html.RenderPartial("AddressDisplay", Model);
       } %>
</table>
