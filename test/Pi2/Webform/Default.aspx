<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Webform_HMI_PLCPi_App1.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        &nbsp;&nbsp;&nbsp;
        <br />
        <asp:Label ID="Label1" runat="server" Text="DI/DO"></asp:Label>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Đọc Ngõ vào" style="margin-left: 38px" Width="98px" />
    
        <asp:TextBox ID="TextBox1" runat="server" BackColor="#669999" style="margin-left: 5px" Width="46px"></asp:TextBox>
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" style="margin-left: 88px" Text="Bật Q5" Width="80px" />
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Tắt Q5" Width="83px" />
        <asp:TextBox ID="TextBox3" runat="server" BackColor="#669999" style="margin-left: 11px" Width="56px"></asp:TextBox>
        <br />
        <br />
        <br />
    
    </div>
    </form>
</body>
</html>
