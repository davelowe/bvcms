﻿<%@ Page Language="C#" StylesheetTheme="Standard" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="Organization.aspx.cs"
    Inherits="CMSWeb.Organization" Title="Organization" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="CustomControls" Namespace="CustomControls" TagPrefix="cc1" %>
<%@ Register Src="UserControls/MemberGrid.ascx" TagName="MemberGrid" TagPrefix="uc2" %>
<%@ Register Src="UserControls/MeetingGrid.ascx" TagName="MeetingGrid" TagPrefix="uc3" %>
<%@ Register Src="UserControls/ExportToolBar.ascx" TagName="ExportToolBar" TagPrefix="uc1" %>
<%@ Register Src="UserControls/VisitorGrid.ascx" TagName="VisitorGrid" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        $(function() {
            if ('<%=EditUpdateButton1.Editing?"true":"false"%>' == 'true') {
                $('a.thickbox2').unbind("click")
                $('a.thickbox3').unbind("click")
            }
            else {
                tb_init('a.thickbox2');
                tb_init('a.thickbox3');
            }
            imgLoader = new Image();
            imgLoader.src = tb_pathToImage;

            var $maintabs = $("#main-tab > ul").tabs();
            var t = $.cookie('maintab3');
            if (t) {
                $maintabs.tabs('select', parseInt(t));
                if (t == "6")
                    $get('<%=ShowMeetings.ClientID%>').click();
            }
            $("#main-tab > ul > li > a").click(function() {
                var selected = $maintabs.data('selected.tabs');
                $.cookie('maintab3', selected);
            });
            $("#meetings-link").click(function() {
                if (!$get('<%=MeetingGrid1.GridClientID%>'))
                    $get('<%=ShowMeetings.ClientID%>').click();
            });
        });
        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                if ('<%=EditUpdateButton1.Editing?"true":"false"%>' == 'true')
                    $('div.members a.thickbox2').unbind("click")
                else
                    tb_init('a.thickbox2');
            }
        }

        function OpenRollsheet() {
            $get('<%=TriggerRollsheetPopup.ClientID%>').click();
            $get('<%=RollsheetInputPanel.ClientID%>').focus();
        }

        function ViewRollsheet2() {
            Page_ClientValidate();
            if (Page_IsValid) {
                var id = '<%=Request.QueryString["id"].ToString()%>';
                var d = $get('<%=MeetingDate.ClientID %>').value;
                var t = $get('<%=MeetingTime.ClientID %>').value;
                var args = "?org=" + id + "&dt=" + d + " " + t;
                var newWindowUrl = "Reports/Rollsheet.aspx" + args
                window.open(newWindowUrl);
            }
            return Page_IsValid;
        }

        function OpenNewMeeting() {
            $get('<%=TriggerNewMeetingPopup.ClientID%>').click();
            $get('<%=NewMeetingInputPanel.ClientID%>').focus();
        }
    </script>

    <table class="PersonHead" border="0">
        <tr>
            <td>
                <cc1:DisplayLabel ID="OrganizationNameInHeader" runat="server" BindingMember="OrganizationName"
                    BindingSource="organization"></cc1:DisplayLabel>
            </td>
            <td>
                <cc1:DisplayLabel ID="LeaderNameInHeader" runat="server" BindingMember="LeaderName"
                    BindingSource="organization" CssClass="leadername"></cc1:DisplayLabel>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <table class="Design2">
                    <tr>
                        <th>
                            Name:
                        </th>
                        <td>
                            <cc1:DisplayOrEditText ID="OrganizationName" runat="server" BindingSource="organization"
                                Width="250px">
                            &nbsp;
                            </cc1:DisplayOrEditText>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            Division:
                        </th>
                        <td>
                            <cc1:DisplayOrEditDropCheck ID="TagString" runat="server" BindingMode="TwoWay" BindingSource="organization"
                                BindingMember="TagString" Width="250px">
                            </cc1:DisplayOrEditDropCheck>
                        </td>
                    </tr>
                    <tr id="NewTagRow" runat="server">
                        <th>
                            New Division Tag:
                        </th>
                        <td>
                            <asp:TextBox ID="NewTag" runat="server" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            Leader:
                        </th>
                        <td>
                            <cc1:DisplayOrEditText ID="LeaderName" runat="server" BindingSource="organization"
                                BindingMode="OneWay" Width="250px">
                            &nbsp;
                            </cc1:DisplayOrEditText>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            Location:
                        </th>
                        <td>
                            <cc1:DisplayOrEditText ID="Location" runat="server" BindingSource="organization">
                            &nbsp;
                            </cc1:DisplayOrEditText>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table class="Design2">
                    <tr>
                        <th>
                            Status:
                        </th>
                        <td>
                            <cc1:DisplayOrEditDropDown ID="OrganizationStatusId" runat="server" BindingMode="TwoWay"
                                BindingSource="organization" DataTextField="Value" DataValueField="Id" Width="150px"
                                BindingMember="OrganizationStatusId" DataSourceID="ODS_OrganizationStatusId"
                                MakeDefault0="False">
                            </cc1:DisplayOrEditDropDown>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            Org Type:
                        </th>
                        <td>
                            <cc1:DisplayOrEditDropDown ID="OrganizationTypeId" runat="server" BindingMode="TwoWay"
                                BindingSource="organization" DataTextField="Value" DataValueField="Id" Width="150px"
                                BindingMember="OrganizationTypeId" DataSourceID="ODS_OrganizationTypeId" MakeDefault0="False">
                            </cc1:DisplayOrEditDropDown>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            Leader Type:
                        </th>
                        <td>
                            <cc1:DisplayOrEditDropDown ID="LeaderMemberTypeId" runat="server" BindingMode="TwoWay"
                                BindingSource="organization" DataTextField="Value" MakeDefault0="True" DataValueField="Id"
                                Width="150px" BindingMember="LeaderMemberTypeId" AppendDataBoundItems="true"
                                DataSourceID="ODS_LeaderMemberTypeId">
                                <asp:ListItem Value="0">(not specified)</asp:ListItem>
                            </cc1:DisplayOrEditDropDown>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td style="margin-bottom: 20px">
                <asp:HyperLink ID="RecentAttendRpt" runat="server" Target="_blank">Recent Attendance Report</asp:HyperLink>&nbsp
                | &nbsp
                <asp:LinkButton ID="RollsheetRpt" runat="server" OnClientClick="OpenRollsheet();return false;"
                    Text="Create Roll Sheet" />
                &nbsp;|
                <cc1:LinkButtonConfirm ID="CloneOrg1" OnClick="CloneOrg_Click" Confirm="This will make a copy of the org. Are you sure?"
                    runat="server">Copy this Organization</cc1:LinkButtonConfirm>
            </td>
        </tr>
    </table>
    <cc1:EditUpdateButton ID="EditUpdateButton1" runat="server" OnClick="EditUpdateButton1_Click" />
    <asp:ImageButton ID="DeleteOrg" runat="server" ImageUrl="~/images/delete.gif"
        OnClientClick="return confirm('Are you sure you want to delete?')" OnClick="DeleteOrg_Click" />
    <asp:CustomValidator ID="ValidateDelete" runat="server" Display="Dynamic" ErrorMessage="Too many relationships remain"></asp:CustomValidator>
    <div id="main-tab">
        <ul>
            <li><a href="#Members-tab"><span>Members</span></a></li>
            <li><a href="#Inactive-tab"><span>Inactive</span></a></li>
            <li><a href="#Visitors-tab"><span>Visitors</span></a></li>
            <li><a href="#Demographic-tab"><span>Demographic</span></a></li>
            <li><a href="#Schedule-tab"><span>Schedule</span></a></li>
            <li><a href="#Tracking-tab"><span>Tracking</span></a></li>
            <li><a id="meetings-link" href="#Meetings-tab"><span>Meetings</span></a></li>
        </ul>
        <div id="Members-tab" style='<%=displaynone%>'>
            <uc1:ExportToolBar ID="ExportToolBar1" runat="server" />
            <uc2:MemberGrid ID="MemberGrid1" runat="server" />
        </div>
        <div id="Inactive-tab" style='<%=displaynone%>'>
            <uc1:ExportToolBar ID="ExportToolBar3" runat="server" />
            <uc2:MemberGrid ID="MemberGrid2" runat="server" Active="0" />
        </div>
        <div id="Visitors-tab" style='<%=displaynone%>'>
            <uc1:ExportToolBar ID="ExportToolBar2" runat="server" />
            &nbsp;Visitor Lookback Days:
            <asp:TextBox ID="VisitLookbackDays" runat="server" Width="34px"></asp:TextBox>
            <asp:LinkButton ID="SetDays" runat="server" OnClick="SetDays_Click">Set</asp:LinkButton>
            <div style="clear: both">
            </div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <uc4:VisitorGrid ID="VisitorGrid1" runat="server" DataSourceID="VisitorData" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="Demographic-tab" style='<%=displaynone%>'>
            <table class="Design2">
                <tr>
                    <td>
                        &#160;&nbsp;
                    </td>
                </tr>
                <tr>
                    <th>
                        Flags:
                    </th>
                </tr>
                <tr>
                    <td>
                        <cc1:DisplayOrEditCheckbox ID="VipFlag" runat="server" BindingSource="organization"
                            Text="VIP" TextIfChecked="VIP Organization" BindingMember="VipFlag" BindingMode="TwoWay"
                            TextIfNotChecked="" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <cc1:DisplayOrEditCheckbox ID="Confidential" runat="server" BindingSource="organization"
                            Text="Confidential" TextIfChecked="Confidential Organization" BindingMember="Confidential"
                            BindingMode="TwoWay" TextIfNotChecked="" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <cc1:DisplayOrEditCheckbox ID="PromotableFlag" runat="server" BindingSource="organization"
                            Text="Promotable" TextIfChecked="Promotable Organization" BindingMember="PromotableFlag"
                            BindingMode="TwoWay" TextIfNotChecked="" />
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        &#160;&nbsp
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="Design2">
                            <tr>
                                <th>
                                    Age Range Start:
                                </th>
                                <td>
                                    <cc1:DisplayOrEditText ID="AgeRangeStart" runat="server" BindingSource="organization"
                                        BindingMember="AgeRangeStart" BindingMode="TwoWay" ChangedStatus="False" Width="135px"></cc1:DisplayOrEditText>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    Grade Range Start:
                                </th>
                                <td>
                                    <cc1:DisplayOrEditText ID="GradeRangeStart" runat="server" BindingSource="organization"
                                        BindingMember="GradeRangeStart" BindingMode="TwoWay" ChangedStatus="False" Width="135px"></cc1:DisplayOrEditText>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table class="Design2">
                            <tr>
                                <th>
                                    Age Range End:
                                </th>
                                <td>
                                    <cc1:DisplayOrEditText ID="AgeRangeEnd" runat="server" BindingSource="organization"
                                        BindingMember="AgeRangeEnd" BindingMode="TwoWay" ChangedStatus="False" Width="135px"></cc1:DisplayOrEditText>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    Grade Range End:
                                </th>
                                <td>
                                    <cc1:DisplayOrEditText ID="GradeRangeEnd" runat="server" BindingSource="organization"
                                        BindingMember="GradeRangeEnd" BindingMode="TwoWay" ChangedStatus="False" Width="135px"></cc1:DisplayOrEditText>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table class="Design2">
                <tr>
                    <th>
                        Gender:
                    </th>
                    <td>
                        <cc1:DisplayOrEditDropDown ID="GenderTypeId" runat="server" BindingMode="TwoWay"
                            BindingSource="organization" DataTextField="Value" DataValueField="Id" BindingMember="GenderTypeId"
                            DataSourceID="ODS_GenderTypeId" MakeDefault0="False">
                        </cc1:DisplayOrEditDropDown>
                    </td>
                </tr>
                <tr>
                    <th>
                        Marital Status:
                    </th>
                    <td>
                        <cc1:DisplayOrEditDropDown ID="MaritalStatusId" runat="server" BindingMode="TwoWay"
                            BindingSource="organization" DataTextField="Value" DataValueField="Id" BindingMember="MaritalStatusId"
                            DataSourceID="ODS_MaritalStatusId" MakeDefault0="False">
                        </cc1:DisplayOrEditDropDown>
                    </td>
                </tr>
            </table>
        </div>
        <div id="Schedule-tab" style='<%=displaynone%>'>
            <table class="Design2">
                <tr>
                    <th>
                        Schedule:
                    </th>
                    <td>
                        <cc1:DisplayOrEditDropDown ID="ScheduleId" runat="server" BindingMode="TwoWay" BindingSource="organization"
                            DataTextField="Value" DataValueField="Id" MakeDefault0="True" Width="350px" BindingMember="ScheduleId"
                            DataSourceID="ODS_ScheduleId" AppendDataBoundItems="True">
                            <asp:ListItem Value="0">(not specified)</asp:ListItem>
                        </cc1:DisplayOrEditDropDown>
                    </td>
                </tr>
                <tr>
                    <td>
                        <cc1:DisplayOrEditCheckbox ID="AllowAttendOverlap" runat="server" BindingSource="organization"
                            Text="Allow Attendance Overlap" TextIfChecked="Attendance Overlap Allowed" BindingMode="TwoWay"
                            TextIfNotChecked="Attendance Overlap Not Allowed" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &#160;&nbsp
                    </td>
                </tr>
                <tr>
                    <th>
                        First Meeting Date:
                    </th>
                    <td>
                        <cc1:DisplayOrEditDate ID="FirstMeetingDate" runat="server" BindingSource="organization"
                            BindingMode="TwoWay" BindingMember="FirstMeetingDate"></cc1:DisplayOrEditDate>
                        <cc2:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="FirstMeetingDate"
                            Enabled="True">
                        </cc2:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <th>
                        Last Meeting Date:
                    </th>
                    <td>
                        <cc1:DisplayOrEditDate ID="LastMeetingDate" runat="server" BindingSource="organization"
                            BindingMode="TwoWay" BindingMember="LastMeetingDate"></cc1:DisplayOrEditDate>
                        <cc2:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="LastMeetingDate"
                            Enabled="True">
                        </cc2:CalendarExtender>
                    </td>
                </tr>
            </table>
        </div>
        <div id="Tracking-tab" style='<%=displaynone%>'>
            <table class="Design2">
                <tr>
                    <td>
                        &#160;&nbsp;
                    </td>
                </tr>
                <tr>
                    <th>
                        Flags:
                    </th>
                </tr>
                <tr>
                    <td>
                        <cc1:DisplayOrEditCheckbox ID="TrackVisitors" runat="server" BindingSource="organization"
                            Text="Track Visitors" TextIfChecked="Track Visitors" BindingMember="TrackVisitors"
                            BindingMode="TwoWay" TextIfNotChecked="" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <cc1:DisplayOrEditCheckbox ID="AttendanceSummaryFlag" runat="server" BindingSource="organization"
                            Text="Attendance Summary Flag" TextIfChecked="Attendance Summary Flag" BindingMember="AttendanceSummaryFlag"
                            BindingMode="TwoWay" TextIfNotChecked="" />
                    </td>
                </tr>
            </table>
            <table class="Design2">
                <tr>
                    <th>
                        Attendance Tracking Level:
                    </th>
                    <td>
                        <cc1:DisplayOrEditDropDown ID="AttendTrkLevelId" runat="server" BindingMode="TwoWay"
                            BindingSource="organization" DataTextField="Value" DataValueField="Id" Width="200px"
                            BindingMember="AttendTrkLevelId" DataSourceID="ODS_AttendTrkLevelId" MakeDefault0="False">
                        </cc1:DisplayOrEditDropDown>
                    </td>
                </tr>
                <tr>
                    <th>
                        Attendance Classification:
                    </th>
                    <td>
                        <cc1:DisplayOrEditDropDown ID="AttendClassificationId" runat="server" BindingMode="TwoWay"
                            BindingSource="organization" DataTextField="Value" DataValueField="Id" Width="200px"
                            BindingMember="AttendClassificationId" DataSourceID="ODS_AttendClassificationId"
                            MakeDefault0="False">
                        </cc1:DisplayOrEditDropDown>
                    </td>
                </tr>
                <tr>
                    <th>
                        Rollsheet Type:
                    </th>
                    <td>
                        <cc1:DisplayOrEditDropDown ID="RollSheetTypeId" runat="server" BindingMode="TwoWay"
                            BindingSource="organization" DataTextField="Value" DataValueField="Id" Width="200px"
                            BindingMember="RollSheetTypeId" DataSourceID="ODS_RollSheetTypeId" MakeDefault0="False">
                        </cc1:DisplayOrEditDropDown>
                    </td>
                </tr>
                <tr>
                    <th>
                        Security Type:
                    </th>
                    <td>
                        <cc1:DisplayOrEditDropDown ID="SecurityTypeId" runat="server" BindingMode="TwoWay"
                            BindingSource="organization" DataTextField="Value" DataValueField="Id" Width="200px"
                            BindingMember="SecurityTypeId" DataSourceID="ODS_SecurityTypeId" MakeDefault0="False">
                        </cc1:DisplayOrEditDropDown>
                    </td>
                </tr>
                <tr>
                    <td>
                        &#160;&nbsp;
                    </td>
                </tr>
                <tr>
                    <th>
                        Rollsheet Visitor Weeks:
                    </th>
                    <td>
                        <cc1:DisplayOrEditText ID="RollSheetVisitorWks" runat="server" BindingSource="organization"
                            BindingMember="RollSheetVisitorWks" BindingMode="TwoWay" ChangedStatus="False"
                            Width="135px"></cc1:DisplayOrEditText>
                    </td>
                </tr>
                <tr>
                    <th>
                        Quarterly Summary Interval:
                    </th>
                    <td>
                        <cc1:DisplayOrEditText ID="QtrlySummaryInterval" runat="server" BindingSource="organization"
                            BindingMember="QtrlySummaryInterval" BindingMode="TwoWay" ChangedStatus="False"
                            Width="135px"></cc1:DisplayOrEditText>
                    </td>
                </tr>
            </table>
        </div>
        <div id="Meetings-tab" style='<%=displaynone%>'>
            <asp:UpdatePanel ID="MeetingsPanel" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:LinkButton ID="NewMeetingLink" runat="server" OnClientClick="OpenNewMeeting();return false;"
                        Text="Create New Meeting" />
                    <br />
                    <uc3:MeetingGrid ID="MeetingGrid1" runat="server" DataSourceID="MeetingData" Visible="false" />
                    <div style="visibility: hidden">
                        <asp:Button ID="ShowMeetings" runat="server" OnClick="ShowMeetings_Click" Text="Button" /></div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ShowMeetings" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:UpdatePanel ID="RollsheetPanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:LinkButton ID="TriggerRollsheetPopup" Style="display: none" runat="server">LinkButton</asp:LinkButton>
            <asp:Panel ID="RollsheetInputPanel" runat="server" CssClass="modalDiv" Style="display: none">
                <table>
                    <tr>
                        <th colspan="2" style="font-size: larger; font-weight: bold">
                            Please select a meeting date and time:
                        </th>
                    </tr>
                    <tr>
                        <th>
                            Meeting Date:
                        </th>
                        <td>
                            <asp:TextBox ID="MeetingDate" runat="server"></asp:TextBox>
                            <cc2:CalendarExtender ID="MeetingDateExtender" runat="server" TargetControlID="MeetingDate">
                            </cc2:CalendarExtender>
                            <asp:RequiredFieldValidator ID="MeetingDateRequiredFieldValidator" runat="server"
                                ErrorMessage="Please enter a Meeting Date." ControlToValidate="MeetingDate" SetFocusOnError="True"
                                ValidationGroup="RollSheetValidatorGroup"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            Meeting Time:
                        </th>
                        <td>
                            <asp:TextBox ID="MeetingTime" runat="server" ToolTip="Time in Format hh:mm am or pm"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="MeetingTimeValidator" runat="server" ErrorMessage="Invalid time: Use format hh:mm am or pm."
                                ControlToValidate="MeetingTime" ValidationExpression="^ *(1[0-2]|[1-9]):[0-5][0-9] *(a|p|A|P)(m|M) *$"
                                SetFocusOnError="True" ValidationGroup="RollSheetValidatorGroup"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="MeetingTimeRequiredFieldValidator" runat="server"
                                ErrorMessage="Please enter a meeting time." ControlToValidate="MeetingTime" SetFocusOnError="True"
                                ValidationGroup="RollSheetValidatorGroup"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td class="footer">
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" Text="Create"
                                OnClientClick="ViewRollsheet2();" ValidationGroup="NewMeetingValidatorGroup" />
                            <asp:LinkButton ID="RollsheetCancel" runat="server" CausesValidation="false" Text="Cancel" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <cc2:ModalPopupExtender ID="RollsheetPopup" BehaviorID="RollsheetPopupBehavior" runat="server"
                TargetControlID="TriggerRollsheetPopup" PopupControlID="RollsheetInputPanel"
                CancelControlID="RollsheetCancel" DropShadow="true" BackgroundCssClass="modalBackground">
            </cc2:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="NewMeetingPanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:LinkButton ID="TriggerNewMeetingPopup" Style="display: none" runat="server">LinkButton</asp:LinkButton>
            <asp:Panel ID="NewMeetingInputPanel" runat="server" CssClass="modalDiv" Style="display: none">
                <table>
                    <tr>
                        <th colspan="2" style="font-size: larger; font-weight: bold">
                            Please select a meeting date and time:
                        </th>
                    </tr>
                    <tr>
                        <th>
                            Meeting Date:
                        </th>
                        <td>
                            <asp:TextBox ID="NewMeetingDate" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                                ID="NewMeetingDateRequiredFieldValidator" runat="server" ErrorMessage="Please enter a Meeting Date."
                                ControlToValidate="NewMeetingDate" SetFocusOnError="True" ValidationGroup="NewMeetingValidatorGroup"></asp:RequiredFieldValidator><cc2:CalendarExtender
                                    ID="NewMeetingDateExtender" runat="server" TargetControlID="NewMeetingDate">
                                </cc2:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            Meeting Time:
                        </th>
                        <td>
                            <asp:TextBox ID="NewMeetingTime" runat="server" ToolTip="Time in Format hh:mm am or pm"></asp:TextBox><asp:RequiredFieldValidator
                                ID="NewMeetingTimeRequiredFieldValidator" runat="server" ErrorMessage="Please enter a meeting time."
                                ControlToValidate="NewMeetingTime" SetFocusOnError="True" ValidationGroup="NewMeetingValidatorGroup"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                    ID="NewMeetingTimeValidator" runat="server" ErrorMessage="Invalid time: Use format hh:mm am or pm."
                                    ControlToValidate="NewMeetingTime" ValidationExpression="^ *(1[0-2]|[1-9]):[0-5][0-9] *(a|p|A|P)(m|M) *$"
                                    SetFocusOnError="True" ValidationGroup="NewMeetingValidatorGroup"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td class="footer">
                            <asp:LinkButton ID="NewMeeting" runat="server" CausesValidation="false" Text="New Meeting"
                                OnClick="CreateMeeting" ValidationGroup="NewMeetingValidatorGroup" />
                            <asp:LinkButton ID="NewGroupMeeting" runat="server" CausesValidation="false" Text="New Group Meeting"
                                OnClick="CreateGroupMeeting" ValidationGroup="NewMeetingValidatorGroup" />
                            <asp:LinkButton ID="NewMeetingCancel" runat="server" CausesValidation="false" Text="Cancel" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <cc2:ModalPopupExtender ID="NewMeetingPopup" BehaviorID="NewMeetingPopupBehavior"
                runat="server" TargetControlID="TriggerNewMeetingPopup" PopupControlID="NewMeetingInputPanel"
                CancelControlID="NewMeetingCancel" DropShadow="true" BackgroundCssClass="modalBackground">
            </cc2:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="MeetingData" runat="server" EnablePaging="True" SelectCountMethod="MeetingCount"
        DeleteMethod="DeleteMeeting" SortParameterName="sortExpression" SelectMethod="Meetings"
        TypeName="CMSPresenter.MeetingController">
        <DeleteParameters>
            <asp:Parameter Name="MeetingId" Type="Int32" />
        </DeleteParameters>
        <SelectParameters>
            <asp:QueryStringParameter Name="orgid" QueryStringField="id" Type="Int32" />
            <asp:Parameter Name="sortExpression" Type="String" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="VisitorData" runat="server" EnablePaging="True" SelectCountMethod="VisitorCount"
        SortParameterName="sortExpression" SelectMethod="Visitors" TypeName="CMSPresenter.OrganizationController">
        <SelectParameters>
            <asp:QueryStringParameter Name="orgid" QueryStringField="id" Type="Int32" />
            <asp:Parameter Name="sortExpression" Type="String" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_OrganizationStatusId" runat="server" SelectMethod="OrganizationStatusCodes"
        TypeName="CMSPresenter.CodeValueController"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_OrganizationTypeId" runat="server" SelectMethod="OrganizationTypes"
        TypeName="CMSPresenter.CodeValueController"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_ScheduleId" runat="server" SelectMethod="Schedules"
        TypeName="CMSPresenter.CodeValueController"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_LeaderMemberTypeId" runat="server" SelectMethod="MemberTypeCodes2"
        TypeName="CMSPresenter.CodeValueController"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_GenderTypeId" runat="server" SelectMethod="GenderClasses"
        TypeName="CMSPresenter.CodeValueController"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_MaritalStatusId" runat="server" SelectMethod="MaritalStatusCodes"
        TypeName="CMSPresenter.CodeValueController"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_RollSheetTypeId" runat="server" SelectMethod="RollsheetTypes"
        TypeName="CMSPresenter.CodeValueController"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_SecurityTypeId" runat="server" SelectMethod="SecurityTypeCodes"
        TypeName="CMSPresenter.CodeValueController"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_AttendTrkLevelId" runat="server" SelectMethod="AttendanceTrackLevelCodes"
        TypeName="CMSPresenter.CodeValueController"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_AttendClassificationId" runat="server" SelectMethod="AttendanceClassifications"
        TypeName="CMSPresenter.CodeValueController"></asp:ObjectDataSource>
</asp:Content>