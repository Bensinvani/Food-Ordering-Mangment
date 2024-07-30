<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="Food_Ordering_Mangment.Contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .contact-form {
            margin-top: 20px;
            max-width: 800px;
            margin: auto;
        }
        .contact-form h2 {
            text-align: center;
            margin-bottom: 20px;
        }
        .contact-form .form-group {
            margin-bottom: 15px;
        }
        .contact-form .btn {
            width: 100%;
        }
        .alert {
            padding: 15px;
            background-color: #d4edda;
            color: #155724;
            border: 1px solid transparent;
            border-radius: 4px;
        }
        .alert-success {
            border-color: #c3e6cb;
        }
    </style>
    <script>
        function hideMessage() {
            setTimeout(function() {
                var messageContainer = document.getElementById('<%= lblMessage.ClientID %>');
                if (messageContainer) {
                    messageContainer.style.display = 'none';
                }
            }, 3000);
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container contact-form">
        <h2>Contact Us</h2>
        <div id="messageContainer">
            <asp:Label ID="lblMessage" runat="server" CssClass="alert alert-success" Visible="false" />
        </div>
        <form class="row g-3">
            <div class="col-md-6">
                <label for="txtFirstName" class="form-label">First Name</label>
                <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" />
            </div>
            <div class="col-md-6">
                <label for="txtLastName" class="form-label">Last Name</label>
                <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" />
            </div>
            <div class="col-md-6">
                <label for="txtEmail" class="form-label">Email</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
            </div>
            <div class="col-md-6">
                <label for="txtPhone" class="form-label">Phone</label>
                <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" />
            </div>
            <div class="col-md-6">
                <label for="txtUsername" class="form-label">Username</label>
                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" Enabled="false" />
            </div>
            <div class="col-md-6">
                <label for="ddlIssueType" class="form-label">Issue Type</label>
                <asp:DropDownList ID="ddlIssueType" runat="server" CssClass="form-select">
                    <asp:ListItem Text="Select Issue Type" Value="" />
                    <asp:ListItem Text="General Inquiry" Value="General Inquiry" />
                    <asp:ListItem Text="Order Issue" Value="Order Issue" />
                    <asp:ListItem Text="Product Review" Value="Product Review" />
                    <asp:ListItem Text="Other" Value="Other" />
                </asp:DropDownList>
            </div>
            <div class="col-12">
                <label for="txtMessage" class="form-label">Message</label>
                <asp:TextBox ID="txtMessage" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" />
            </div>
            <div class="col-12">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
            </div>
        </form>
    </div>
</asp:Content>
