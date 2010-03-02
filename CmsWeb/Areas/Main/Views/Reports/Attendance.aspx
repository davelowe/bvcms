<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<CMSWeb.Areas.Main.Models.Report.AttendanceModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(function() {
            $('#Attendances > thead a.sortable').click(function(ev) {
                var newsort = $(this).text();
                var oldsort = $("#Sort").val();
                $("#Sort").val(newsort);
                var dir = $("#Dir").val();
                if (oldsort == newsort && dir == 'asc')
                    $("#Dir").val('desc');
                else
                    $("#Dir").val('asc');
                RefreshList();
            });
        });
        function RefreshList() {
            var q = $('form').serialize();
            $.navigate("/Reports/Attendance/<%=Model.OrgId%>", q);
        }
    </script>

    <h2>Attendance for <a href="/Organization.aspx?id=<%=Model.OrgId %>"><%=Model.OrgName %></a></h2>
    <% using (Html.BeginForm()) 
       { %>
        <div>
            <fieldset>
                <table style="empty-cells:show">
                <col style="width: 13em; text-align:right" />
                <col />
                <col />
                 <tr>
                    <td><label for="end">Start Date</label></td>
                    <td><%= Html.TextBox("start") %></td>
                    <td><%= Html.ValidationMessage("start") %></td>
                </tr>
                 <tr>
                    <td><label for="end">End Date</label></td>
                    <td><%= Html.TextBox("end") %></td>
                    <td><%= Html.ValidationMessage("end") %></td>
                </tr>
                <tr>
                    <td>&nbsp;</td><td><input type="submit" value="Run" /></td>
                </tr>
                </table>
            </fieldset>
        </div>
    <%=Html.Hidden("Sort", Model.Sort) %>
    <%=Html.Hidden("Dir", Model.Dir) %>
    <% } %>
    <table id="Attendances">
    <thead>
    <th><a href='#' class="sortable">Name</a></th>
    <th><a href='#' class="sortable">Age</a></th>
    <th>Attendance String</th>
    <th><a href='#' class="sortable">Percent</a></th>
    <th><a href='#' class="sortable">Count</a></th>
    </thead>
    <tbody>
    <% foreach (var p in Model.Attendances())
       { %>
        <tr>
        <td><a href="/Person/Index/<%=p.PeopleId %>"><%= p.Name %></a></td>
        <td align="right"><%=p.Age %></td>
        <td><span style="font-family:Courier New"><%=p.AttendStr %></span></td>
        <td align="right"><%=p.AttendPct.ToString("N1") %></td>
        <td align="right"><%=p.AttendCount %></td>
        </tr>
    <% } %>
    </tbody>
    </table>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

</asp:Content>