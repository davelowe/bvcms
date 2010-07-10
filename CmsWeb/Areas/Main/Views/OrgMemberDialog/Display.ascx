﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CmsData.OrganizationMember>" %>
<% string comboid = "-" + Model.OrganizationId + "-" + Model.PeopleId; %>
<table class="Design2">
    <tr>
        <td><strong><%=Model.Person.Name %></strong></td>
        <td align="right"><a class="display" href="/OrgMemberDialog/Edit/<%=Model.OrganizationId %>?pid=<%=Model.PeopleId %>">Edit</a></td>
    </tr>
    <tr>
        <th>Member Type:</th>
        <td><%=Model.MemberType.Description %></td>
    </tr>
    <tr>
        <th>Inactive Date:</th>
        <td><%=Model.InactiveDate.FormatDate() %></td>
    </tr>
    <tr>
        <th>Enrollment Date:</th>
        <td><%=Model.EnrollmentDate.FormatDate() %></td>
    </tr>
    <tr>
        <th>Pending:</th>
        <td><input type="checkbox" disabled="disabled" <%=Model.Pending == true ? "checked='checked'" : "" %> /></td>
    </tr>
<% if (Model.RegisterEmail.HasValue())
   { %>    
    <tr>
        <th>RegisterEmail:</th>
        <td><%=Model.RegisterEmail %></td>
    </tr>
<% }
   if (Model.Organization.AskRequest == true)
   { %>    
    <tr>
        <th>Request:</th>
        <td><%=Model.Request%></td>
    </tr>
<% }
   if(Model.Grade.HasValue)
   { %>    
    <tr>
        <th>Grade:</th>
        <td><%=Model.Grade %></td>
    </tr>
<% }    
   if(Model.Organization.AskTickets == true) 
   { %>    
    <tr>
        <th>No. Items:</th>
        <td><%=Model.Tickets %></td>
    </tr>
<% }    
   if (Model.Organization.Fee > 0 || Model.Organization.ShirtFee > 0)
   { %>    
    <tr>
        <th>Amount:</th>
        <td><%=Model.Amount.HasValue ? Model.Amount.Value.ToString("C") : ""%></td>
    </tr>
    <tr>
        <th>AmountPaid:</th>
        <td><%=Model.AmountPaid.HasValue ? Model.AmountPaid.Value.ToString("C") : "" %>
        <% if (Model.PayLink.HasValue())
           { %>
        <a href="<%=Model.PayLink %>" target="_blank">paylink</a>
        <% } %>
        </td>
    </tr>
<% }
   if(Model.Organization.AskShirtSize == true) 
   { %>    
    <tr>
        <th>ShirtSize:</th>
        <td><%=Model.ShirtSize %></td>
    </tr>
<% } %>    
    <tr>
        <th valign="top">Extra Member Info:</th>
        <td><%=Util.SafeFormat(Model.UserData) %></td>
    </tr>
    <tr>
        <th>Drop:</th>
        <td><a class="delete" href="/OrgMemberDialog/Drop/d<%=comboid %>"><img src="/images/delete.gif" border="0" /></a></td>
    </tr>
    <tr>
        <td></td>
        <td align="right"><a class="display" href="/OrgMemberDialog/Move/<%=Model.OrganizationId %>?pid=<%=Model.PeopleId %>">move</a></td>
    </tr>
</table>
