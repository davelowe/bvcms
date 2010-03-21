﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CMSWeb.Models.OnlineRegPersonModel>" %>
<table>
<% if (Model.org.AskShirtSize == true)
   { %>
    <tr>
        <td><label for="shirtsize">ShirtSize</label></td>
        <td><%=Model.shirtsize %>
        <%=Html.Hidden3("m.list[" + Model.index + "].shirtsize", Model.shirtsize)%>
        </td>
    </tr>
<% }
   if (Model.org.AskRequest == true)
   { %>
    <tr>
        <td><label for="request">Request Teammate</label></td>
        <td><%=Model.request %>
        <%=Html.Hidden3("m.list[" + Model.index + "].request", Model.request)%>
        </td>
    </tr>
<% }
   if (Model.org.AskEmContact == true)
   { %>
    <tr>
        <td><label for="emcontact">Emergency Friend</label></td>
        <td><%=Model.emcontact %>
        <%=Html.Hidden3("m.list[" + Model.index + "].emcontact", Model.emcontact)%>
        </td>
    </tr>
    <tr>
        <td><label for="emphone">Emergency Phone</label></td>
        <td><%=Model.emphone %>
        <%=Html.Hidden3("m.list[" + Model.index + "].emphone", Model.emphone)%>
        </td>
    </tr>
<% }
   if (Model.org.AskInsurance == true)
   { %>
    <tr>
        <td><label for="insurance">Health Insurance Carrier</label></td>
        <td><%=Model.insurance %>
        <%=Html.Hidden3("m.list[" + Model.index + "].insurance", Model.insurance)%>
        </td>
    </tr>
    <tr>
        <td><label for="policy">Policy #</label></td>
        <td><%=Model.policy %>
        <%=Html.Hidden3("m.list[" + Model.index + "].policy", Model.policy)%>
        </td>
    </tr>
<% }
   if (Model.org.AskDoctor == true)
   { %>
    <tr>
        <td><label for="doctor">Family Physician Name</label></td>
        <td><%=Model.doctor %>
        <%=Html.Hidden3("m.list[" + Model.index + "].doctor", Model.doctor)%>
        </td>
    </tr>
    <tr>
        <td><label for="docphone">Family Physician Phone</label></td>
        <td><%=Model.docphone %>
        <%=Html.Hidden3("m.list[" + Model.index + "].docphone", Model.docphone)%>
        </td>
    </tr>
<% }
   if (Model.org.AskAllergies == true)
   { %>
    <tr>
        <td><label for="medical">Allergies or<br />
               Medical Problems</label></td>
        <td><%=Model.medical %>
        <%=Html.Hidden3("m.list[" + Model.index + "].medical", Model.medical)%>
        </td>
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
                <td><%=Model.tylenol == true ? "Yes" : "No" %>
                <%=Html.Hidden3("m.list[" + Model.index + "].tylenol", Model.tylenol) %>
                </td>
            </tr>
            <tr>
                <td>Advil?:</td>
                <td><%=Model.advil == true ? "Yes" : "No" %>
                <%=Html.Hidden3("m.list[" + Model.index + "].advil", Model.advil)%>
                </td>
            </tr>
            <tr>
                <td>Maalox?:</td>
                <td><%=Model.maalox == true ? "Yes" : "No" %>
                <%=Html.Hidden3("m.list[" + Model.index + "].maalox", Model.maalox)%>
                </td>
            </tr>
            <tr>
                <td>Robitussin?:</td>
                <td><%=Model.robitussin == true ? "Yes" : "No" %>
                <%=Html.Hidden3("m.list[" + Model.index + "].robitussin", Model.robitussin)%>
                </td>
            </tr>
            </table>
        </td>
        <td></td>
    </tr>
<% }
   if (Model.org.AskParents == true)
   { %>
    <tr>
        <td><label for="mname">Mother's Name (first last)</label></td>
        <td><%=Model.mname %>
        <%=Html.Hidden3("m.list[" + Model.index + "].mname", Model.mname)%>
        </td>
    </tr>
    <tr>
        <td><label for="fname">Father's Name (first last)</label></td>
        <td><%=Model.fname%>
        <%=Html.Hidden3("m.list[" + Model.index + "].fname", Model.fname)%>
        </td>
    </tr>
<% }
   if (Model.org.AskCoaching == true)
   { %>
     <tr>
        <td><label for="coaching">Interested in Coaching?</label></td>
        <td><%=Model.coaching == true ? "Yes" : "No" %>
        <%=Html.Hidden3("m.list[" + Model.index + "].coaching", Model.coaching)%>
        </td>
    </tr>
<% }
   if (Model.org.AskChurch == true)
   { %>
    <tr>
        <td><label for="church"><%= Model.org.AskParents == true ? "Parent's Church" : "Church" %></label></td>
        <td><%=Model.member? "Member of Bellevue" : "not member of bellevue" %> <br />
        <%=Model.otherchurch? "Active in another Local Church" : "not active in another church" %>
        <%=Html.Hidden3("m.list[" + Model.index + "].member", Model.member)%>
        <%=Html.Hidden3("m.list[" + Model.index + "].otherchurch", Model.otherchurch)%>
        </td>
    </tr>
<% }
   if (Model.org.Deposit > 0)
   { %>
    <tr>
        <td><label for="paydeposit">Payment</label></td>
        <td><%=Model.paydeposit == true ? "Pay Deposit Only" : "Pay Full Amount" %>
            <%=Html.Hidden3("m.list[" + Model.index + "].paydeposit", Model.paydeposit)%>
        </td>
    </tr>
<% } %>
</table>
