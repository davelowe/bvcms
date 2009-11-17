<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<CmsData.MemberType>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="/Content/js/jquery.jeditable.js" type="text/javascript"></script>
    <script type="text/javascript">
        //id=elements_id&value=user_edited_content
        $(function() {
        $(".clickEdit").editable("/MemberType/Edit/", {
            indicator: "<img src='/images/loading.gif'>",
            tooltip: "Click to edit...",
            style: 'display: inline',
            width: '200px'
        });
        $(".clickSelect").editable("/MemberType/EditAttendType/", {
                indicator: '<img src="/images/loading.gif">',
                loadurl: "/MemberType/AttendTypeCodes/",
                loadtype: "POST",
                type: "select",
                submit: "OK",
                style: 'display: inline'
            });
            $("a.delete").click(function(ev) {
                if (confirm("are you sure?"))
                    $.post("/MemberType/Delete/" + $(this).attr("id"), null, function(ret) {
                        window.location = "/MemberType/";
                    });
                return false;
            });
        });
    </script>
    <h2>Member Types</h2>

    <table>
        <tr>
            <th>
                Id
            </th>
            <th>
                Code
            </th>
            <th>
                Description
            </th>
            <th>
                AttendType
            </th>
            <th></th>
        </tr>

    <% foreach (var item in Model) 
       { %>
        <tr>
            <td><%= Html.Encode(item.Id)%></td>
            <td>
                <span id='c<%=item.Id %>' 
                    class='clickEdit'><%=item.Code%></span>
            </td>
            <td>
                <span id='v<%=item.Id %>'
                    class='clickEdit'><%=item.Description%></span>
            </td>
            <td>
                <span id='a<%=item.Id %>' 
                    class='clickSelect'><%=item.AttendType.Description%></span>
            </td>
            <td>
                <a id='d<%=item.Id %>' href="#" class="delete"><img border="0" src="/images/delete.gif" /></a>
            </td>
        </tr>
    <% } %>

    </table>

    <% using (Html.BeginForm("Create", "MemberType"))
       { %>
    <p>
        New MemberTypeId: <%= Html.TextBox("id") %>
        <input type="submit" value="Create" />
    </p>
    <% } %>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
