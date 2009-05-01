<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<CMSWeb.Models.QueryModel>" %>
<h4>Total Count: <%=Model.FetchCount().ToString("N0") %></h4>
<table id="people">
<thead>
    <tr>
        <th>
            <a href="#" class="sortable">Name</a>
        </th>
        <th>
            <a href="#" class="sortable">Status</a>/Age
            -
            <a href="#" class="sortable">DOB</a>
        </th>
        <th>
            <a href="#" class="sortable">Address</a>
        </th>
        <th>
            Communication
        </th>
        <th>
            <a href="#" class="sortable">Teacher</a>
        </th>
        <th>
            Tag
        </th>
    </tr>
</thead>
<tbody>
<% if(Model.Count == 0)
   { %>
<tr><td colspan="5">No matching records.</td></tr>
<% } %>
<% foreach (var p in Model.FetchPeopleList())
   { %>
<tr>
    <td><img src="/images/individual.gif" width="10px" height="12px" />
        <a href='<%="/Person.aspx?id=" + p.PeopleId %>'><%=p.Name %></a>
    </td>
    <td>
        <%=p.MemberStatus %><br />
        <%=p.Age %>
        -
        <%=p.BirthDate %>
    </td>
    <td>
        <a href='<%="http://www.google.com/maps?q=" + p.Address + ",+" + p.CityStateZip %>'><%=p.Address %></a>
        <br />
        <%=p.CityStateZip %>
    </td>
    <td>
    <% foreach (var ph in p.Phones)
       { %>
        <%=ph%><br />
    <% } %>
        <a href='<%="mailto:" + p.Email %>'><%=p.Email %></a>
    </td>
    <td>
        <a href='<%="/Person.aspx?id=" + p.BFTeacherId %>'><%=p.BFTeacher %></a>
    </td>
    <td>
        <a href="#" class="taguntag" title="Add to/Remove from Active Tag" value='<%=p.PeopleId %>'><%=p.HasTag? "Remove" : "Add" %></a>
    </td>
</tr>
<% } %>
</tbody>
</table>
<%=Html.Hidden("Count", Model.Count)%>
<%=Html.Hidden("Sort", Model.Sort)%>
<%=Html.Hidden("Direction", Model.Direction) %>
<% Html.RenderPartial("Pager", Model.pagerModel()); %>
