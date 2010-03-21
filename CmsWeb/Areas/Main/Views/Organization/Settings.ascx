﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CMSWeb.Models.OrganizationPage.OrganizationModel>" %>
<% CMSWeb.Models.OrganizationPage.OrganizationModel m = Model;
   if (Page.User.IsInRole("Edit"))
   { %>
<a class="displayedit" href="/Organization/SettingsEdit/<%=Model.OrganizationId %>">Edit</a>
<% } %>
<div style="float: left">
    <table class="Design2">
        <tr>
            <th>Schedule:</th>
            <td><%=Html.CodeDesc("org.SchedDay", Model.DaysOfWeek()) %> 
            <%=Model.org.SchedTime.ToString2("h:mm tt") %></td>
        </tr>
        <tr>
            <th>Allow Attendance Overlap:</th>
            <td><%=Html.CheckBoxReadonly(Model.org.AllowAttendOverlap) %></td>
        </tr>
        <tr>
            <th>Class Filled:</th>
            <td><%=Html.CheckBoxReadonly(Model.org.ClassFilled) %></td>
        </tr>
        <tr>
            <th>Online Catalog Sort:</th>
            <td><%=Model.org.OnLineCatalogSort %></td>
        </tr>
        <tr>
            <th>Max Limit:</th>
            <td><%=Model.org.Limit%></td>
        </tr>
        <tr>
            <th>Online Notify Emails:</th>
            <td><%=Model.org.EmailAddresses%></td>
        </tr>
        <tr>
            <th>Online Reg Type:</th>
            <td><%=Model.org.RegType%></td>
        </tr>
        <tr>
            <th>Edit Online messages</th>
            <td><a id="emailmessagelink" href="/Display/OrgContent/<%=Model.org.OrganizationId %>?what=message">
                   registration notification</a><br />
                <a id="instructionslink" href="/Display/OrgContent/<%=Model.org.OrganizationId %>?what=instructions">
                    registration instructions</a>
            </td>
        </tr>
        <tr>
            <td></td>
        </tr>
        <tr>
            <th>Allow Self Check-In:</th>
            <td><%=Html.CheckBoxReadonly(Model.org.CanSelfCheckin) %></td>
        </tr>
        <tr>
            <th>Allow Non-Campus Check-In:</th>
            <td><%=Html.CheckBoxReadonly(Model.org.AllowNonCampusCheckIn) %></td>
        </tr>
        <tr>
            <th>Number of CheckIn Labels:</th>
            <td><%=Model.org.NumCheckInLabels%></td>
        </tr>
        <tr>
            <th>Number of Worker CheckIn Labels:</th>
            <td><%=Model.org.NumWorkerCheckInLabels%></td>
        </tr>
        <tr>
            <th>First Meeting Date:</th>
            <td><%=Model.org.FirstMeetingDate.FormatDate2()%></td>
        </tr>
        <tr>
            <th>Last Meeting Date:</th>
            <td><%=Model.org.LastMeetingDate.FormatDate2()%></td>
        </tr>
    </table>
</div>
<div style="float: left">
    <table class="Design2">
    <tr>
        <th>Attendance Tracking Level:</th>
        <td><%=Html.CodeDesc("org.AttendTrkLevelId", Model.AttendTrkLevelList()) %></td>
    </tr>
    <tr>
        <th>Attendance Classification:</th>
        <td><%=Html.CodeDesc("org.AttendClassificationId", Model.AttendClassificationList()) %></td>
    </tr>
    <tr>
        <th>Entry Point:</th>
        <td><%=Html.CodeDesc("org.EntryPointId", Model.EntryPointList()) %></td>
    </tr>
    <tr>
        <th>Security Type:</th>
        <td><%=Html.CodeDesc("org.SecurityTypeId", Model.SecurityTypeList()) %></td>
    </tr>
    <tr>
        <th>Pending Location:</th>
        <td><%=Model.org.PendingLoc%></td>
    </tr>
    <tr>
        <td></td>
    </tr>
    <tr>
        <th>Rollsheet Visitor Weeks:</th>
        <td><%=Model.org.RollSheetVisitorWks%></td>
    </tr>
    <tr>
        <th>Start Grade/Age:</th>
        <td><%=Model.org.GradeAgeStart%></td>
    </tr>
    <tr>
        <th>End Grade/Age:</th>
        <td><%=Model.org.GradeAgeEnd%></td>
    </tr>
    <tr>
        <th>Gender:</th>
        <td><%=Html.CodeDesc("org.GenderId", Model.GenderList()) %></td>
    </tr>
    <tr>
        <th>Fee:</th>
        <td><%=Model.org.Fee.ToString2("n2")%></td>
    </tr>
    <tr>
        <th>Deposit:</th>
        <td><%=Model.org.Deposit.ToString2("n2")%></td>
    </tr>
    <tr>
        <th>Shirt Fee:</th>
        <td><%=Model.org.ShirtFee.ToString2("n2")%></td>
    </tr>
    <tr>
        <th>Extra Fee:</th>
        <td><%=Model.org.ExtraFee.ToString2("n2")%></td>
    </tr>
    <tr>
        <th>Last Day Before Extra:</th>
        <td><%=Model.org.LastDayBeforeExtra.FormatDate2()%></td>
    </tr>
    <tr>
        <th>Ask About Allergies:</th>
        <td><%=Html.CheckBoxReadonly(Model.org.AskAllergies) %></td>
    </tr>
    <tr>
        <th>Ask About Tylenol, Etc:</th>
        <td><%=Html.CheckBoxReadonly(Model.org.AskTylenolEtc) %></td>
    </tr>
    <tr>
        <th>Ask About Shirt Size:</th>
        <td><%=Html.CheckBoxReadonly(Model.org.AskShirtSize) %></td>
    </tr>
    <tr>
        <th>Ask About Request:</th>
        <td><%=Html.CheckBoxReadonly(Model.org.AskRequest) %></td>
    </tr>
    <tr>
        <th>Ask For Parents:</th>
        <td><%=Html.CheckBoxReadonly(Model.org.AskParents) %></td>
    </tr>
    <tr>
        <th>Ask For Emergency Contact:</th>
        <td><%=Html.CheckBoxReadonly(Model.org.AskEmContact) %></td>
    </tr>
    <tr>
        <th>Ask About Medical:</th>
        <td><%=Html.CheckBoxReadonly(Model.org.AskMedical) %></td>
    </tr>
    <tr>
        <th>Ask For Insurance:</th>
        <td><%=Html.CheckBoxReadonly(Model.org.AskInsurance) %></td>
    </tr>
    <tr>
        <th>Ask For Doctor:</th>
        <td><%=Html.CheckBoxReadonly(Model.org.AskDoctor)%></td>
    </tr>
    <tr>
        <th>Allow Last Year Shirt:</th>
        <td><%=Html.CheckBoxReadonly(Model.org.AllowLastYearShirt)%></td>
    </tr>
    <tr>
        <th>Ask About Coaching:</th>
        <td><%=Html.CheckBoxReadonly(Model.org.AskCoaching)%></td>
    </tr>
    <tr>
        <th>Ask About Church:</th>
        <td><%=Html.CheckBoxReadonly(Model.org.AskChurch)%></td>
    </tr>
    </table>
</div>
<div style="clear: both">
</div>
