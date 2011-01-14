﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CmsWeb.Models.OnlineRegPersonModel>" %>
<% if (Model.AnyOtherInfo())
   {
       if (Model.index > 0)
       { %>
        <tr>
            <th>Other Information</th>
            <td colspan="4" align="right"><a href="#" id="copy">copy from previous</a></td>
        </tr>
    <% }
   }
   if (Model.org.AskShirtSize == true)
   { %>
    <tr>
        <td><label for="shirtsize">ShirtSize</label></td>
        <td><%= Html.DropDownList3("", Model.inputname("shirtsize"), Model.ShirtSizes(), Model.shirtsize)%>
            <div><%= Html.ValidationMessage(Model.inputname("shirtsize"))%></div></td>
    </tr>
<% } 
   if (Model.org.AskRequest == true)
   { %>
    <tr>
        <td><label for="request"><%=Util.PickFirst(Model.org.RequestLabel, "Request") %></label></td>
        <td><%=Html.TextBox(Model.inputname("request"), Model.request, new { maxlength = "100" }) %>
            <div><%= Html.ValidationMessage(Model.inputname("request")) %></div></td>
    </tr>
<% } 
   if (Model.org.AskGrade == true)
   { %>
    <tr>
        <td><label for="grade"><%=Util.PickFirst(Model.org.GradeLabel, "Grade") %></label></td>
        <td><%=Html.TextBox(Model.inputname("grade"), Model.grade) %>
            <div><%= Html.ValidationMessage(Model.inputname("grade")) %></div></td>
    </tr>
<% } 
   if (Model.org.AskEmContact == true)
   { %>
    <tr>
        <td><label for="emcontact">Emergency Friend</label></td>
        <td><%=Html.TextBox(Model.inputname("emcontact"), Model.emcontact, new { maxlength = "100" })%>
        <td colspan="2"><%= Html.ValidationMessage(Model.inputname("emcontact"))%></td>
    </tr>
    <tr>
        <td><label for="emphone">Emergency Phone</label></td>
        <td><%=Html.TextBox(Model.inputname("emphone"), Model.emphone, new { maxlength = "15" }) %>
            <div><%= Html.ValidationMessage(Model.inputname("emphone"))%></div></td>
    </tr>
<% } 
   if (Model.org.AskInsurance == true)
   { %>
    <tr>
        <td><label for="insurance">Health Insurance Carrier</label></td>
        <td><%=Html.TextBox(Model.inputname("insurance"), Model.insurance, new { maxlength = "100" }) %>
            <div><%= Html.ValidationMessage(Model.inputname("insurance"))%></div></td>
    </tr>
    <tr>
        <td><label for="policy">Policy #</label></td>
        <td><%=Html.TextBox(Model.inputname("policy"), Model.policy, new { maxlength = "100" }) %>
            <div><%= Html.ValidationMessage(Model.inputname("policy"))%></div></td>
    </tr>
<% } 
   if (Model.org.AskDoctor == true)
   { %>
    <tr>
        <td><label for="doctor">Family Physician Name</label></td>
        <td><%=Html.TextBox(Model.inputname("doctor"), Model.doctor, new { maxlength = "100" }) %>
            <div><%= Html.ValidationMessage(Model.inputname("doctor"))%></div></td>
    </tr>
    <tr>
        <td><label for="docphone">Family Physician Phone</label></td>
        <td><%=Html.TextBox(Model.inputname("docphone"), Model.docphone, new { maxlength = "15" }) %>
            <div><%= Html.ValidationMessage(Model.inputname("docphone"))%></div></td>
    </tr>
<% } 
   if (Model.org.AskAllergies == true)
   { %>
    <tr>
        <td><label for="medical">Allergies or<br />
               Medical Problems</label></td>
        <td><%=Html.TextArea(Model.inputname("medical"), Model.medical) %>
            <div class="red"> Leave blank if none</div></td>
    </tr>
<% }
   if (Model.org.AskTylenolEtc == true)
   { %>
    <tr>
        <td><label for="medical">May we give your child</label></td>
        <td>
            <table>
            <tr>
                <td>Tylenol?:</td>
                <td>
                    <%=Html.RadioButton(Model.inputname("tylenol"), true, Model.tylenol) %> Yes
                    <%=Html.RadioButton(Model.inputname("tylenol"), false, Model.tylenol) %> No
                    <div><%=Html.ValidationMessage(Model.inputname("tylenol")) %></div>
                </td>
            </tr>
            <tr>
                <td>Advil?:</td>
                <td>
                    <%=Html.RadioButton(Model.inputname("advil"), true, Model.advil) %> Yes
                    <%=Html.RadioButton(Model.inputname("advil"), false, Model.advil) %> No
                    <div><%=Html.ValidationMessage(Model.inputname("advil")) %></div>
                </td>
            </tr>
            <tr>
                <td>Maalox?:</td>
                <td>
                    <%=Html.RadioButton(Model.inputname("maalox"), true, Model.maalox) %> Yes
                    <%=Html.RadioButton(Model.inputname("maalox"), false, Model.maalox) %> No
                    <div><%=Html.ValidationMessage(Model.inputname("maalox")) %></div>
                </td>
            </tr>
            <tr>
                <td>Robitussin?:</td>
                <td>
                    <%=Html.RadioButton(Model.inputname("robitussin"), true, Model.robitussin) %> Yes
                    <%=Html.RadioButton(Model.inputname("robitussin"), false, Model.robitussin) %> No
                    <div><%=Html.ValidationMessage(Model.inputname("robitussin")) %></div>
                </td>
            </tr>
            </table>
        </td>
    </tr>
<% }
   if (Model.org.AskParents == true)
   { %>
    <tr>
        <td><label for="mname">Mother's Name (first last)</label></td>
        <td><%=Html.TextBox(Model.inputname("mname"), Model.mname, new { maxlength = "80" }) %>
            <div><%= Html.ValidationMessage(Model.inputname("mname"))%></div></td>
    </tr>
    <tr>
        <td><label for="fname">Father's Name (first last)</label></td>
        <td><%=Html.TextBox(Model.inputname("fname"), Model.fname, new { maxlength = "80" }) %>
            <div><%= Html.ValidationMessage(Model.inputname("fname"))%></div></td>
    </tr>
<% }
   if (Model.org.AskCoaching == true)
   { %>
     <tr>
        <td><label for="coaching">Interested in Coaching?</label></td>
        <td>
            <%=Html.RadioButton(Model.inputname("coaching"), true, Model.coaching) %> Yes
            <%=Html.RadioButton(Model.inputname("coaching"), false, Model.coaching) %> No
            <div><%=Html.ValidationMessage(Model.inputname("coaching")) %></div>
        </td>
    </tr>
<% }
   if (Model.org.AskChurch == true)
   { %>
    <tr>
        <td><label for="church"><%= Model.org.AskParents == true ? "Parent's Church" : "Church" %></label></td>
        <td><%=Html.CheckBox(Model.inputname("memberus"), Model.memberus) %> Member of this Church<br />
            <%=Html.CheckBox(Model.inputname("otherchurch"), Model.otherchurch) %> Active in another Local Church
            <div><%=Html.ValidationMessage("member")%></div></td>
    </tr>
<% }
   if (Model.org.AskTickets == true)
   { %>
    <tr>
        <td><label for="ntickets"><%=Util.PickFirst(Model.org.NumItemsLabel, "No. of Items") %></label></td>
        <td><%=Html.TextBox(Model.inputname("ntickets"), Model.ntickets) %> />
            <div><%= Html.ValidationMessage(Model.inputname("ntickets")) %></div></td>
    </tr>
<% }
   if(Model.org.AskOptions.HasValue())
   { %>
    <tr>
        <td><div class="wraparound"><%=Util.PickFirst(Model.org.OptionsLabel, "Options")%></div></td>
        <td><%=Html.DropDownList3("", Model.inputname("option"), Model.Options(), "0")%>
            <div><%=Html.ValidationMessage(Model.inputname("option"))%></div></td>
    </tr>
<%  }
    if(Model.org.ExtraOptions.HasValue())
    { %>
    <tr>
        <td><div class="wraparound"><%=Util.PickFirst(Model.org.ExtraOptionsLabel, "Extra Options")%></div></td>
        <td><%=Html.DropDownList3("", Model.inputname("option2"), Model.ExtraOptions(), "0")%>
            <div><%=Html.ValidationMessage(Model.inputname("option2"))%></div></td>
    </tr>
<%  }
    if(Model.org.GradeOptions.HasValue())
    { %>
    <tr>
        <td>Grade</td>
        <td><%=Html.DropDownList3("", Model.inputname("gradeoption"), Model.GradeOptions(), Model.gradeoption)%>
            <div><%=Html.ValidationMessage(Model.inputname("gradeoption"))%></div></td>
    </tr>
<%  }
    foreach (var a in Model.ExtraQuestions())
    { %>
    <tr>
        <td><div class="wraparound"><%=a.question%></div></td>
        <td>
            <%=Html.Hidden(Model.inputname("ExtraQuestion[" + a.n + "].Key"), a.question) %>
            <%=Html.TextBox(Model.inputname("ExtraQuestion[" + a.n + "].Value"), Model.ExtraQuestionValue(a.question)) %>
            <div><%=Html.ValidationMessage(Model.inputname("ExtraQuestion[" + a.n + "].Value"))%></div></td>
    </tr>
<%  }
    foreach (var a in Model.YesNoQuestions())
    { %>
    <tr>
        <td><div class="wraparound"><%=a.desc%></div></td>
        <td>
            <%=Html.Hidden(Model.inputname("YesNoQuestion[" + a.n + "].Key"), a.name) %>
            <%=Html.RadioButton(Model.inputname("YesNoQuestion[" + a.n + "].Value"), true, Model.YesNoChecked(a.name, true)) %> Yes
            <%=Html.RadioButton(Model.inputname("YesNoQuestion[" + a.n + "].Value"), false, Model.YesNoChecked(a.name, false)) %> No
            <div><%=Html.ValidationMessage(Model.inputname("YesNoQuestion[" + a.n + "].Value")) %></div></td>
    </tr>
<% }
   foreach(var i in Model.MenuItems())
   { %>
    <tr>
        <td></td>
        <td>
            <%=Html.Hidden(Model.inputname("MenuItem[" + i.n + "].Key"), i.sg) %>
            <%=Html.TextBox(Model.inputname("MenuItem[" + i.n + "].Value"), Model.MenuItemValue(i.sg), new { @class = "short" }) %>
            <%=i.desc %> ($<%=i.amt.ToString("N2") %>)
            <div><%=Html.ValidationMessage(Model.inputname("MenuItem[" + i.n + "].Value"))%></div></td>
    </tr>
<% }
   if (Model.org.Deposit > 0)
   { %>
    <tr>
        <td><label for="deposit">Payment</label></td>
        <td>
        <%=Html.RadioButton(Model.inputname("paydeposit"), true, Model.paydeposit) %> Pay Deposit Only<br />
            <%=Html.RadioButton(Model.inputname("paydeposit"), false, Model.paydeposit) %> Pay Full Amount
            <div><%=Html.ValidationMessage(Model.inputname("paydeposit"))%></div></td>
    </tr>
<% } %>
    <tr><td></td>
        <td colspan="4"><a id="otheredit" href="/OnlineReg/SubmitOtherInfo/<%=Model.index %>" class="submitbutton">Submit</a></td>
    </tr>