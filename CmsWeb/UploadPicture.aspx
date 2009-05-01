﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadPicture.aspx.cs"
    Inherits="CMSWeb.UploadPicture" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Upload Picture</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <asp:HyperLink ID="HyperLink2" runat="server" ForeColor="Red"></asp:HyperLink>
        </div>
        Select Image File To Upload:
        <asp:FileUpload ID="ImageFile" runat="server" Width="307px" />
        <br />
        <asp:Button ID="Upload" runat="server" Text="Submit" OnClick="Upload_Click"></asp:Button>
        <div>
            <asp:HiddenField ID="HiddenField1" runat="server" />
        </div>
        <div>
            <asp:ImageButton ID="ImageButton1" runat="server" 
                onclick="ImageButton1_Click" />
        </div>
    </div>
    </form>
</body>
</html>