<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Food_Ordering_Mangment.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        body {
            background: url('/Images/HomeOffers/intro.jpg') no-repeat center center fixed;
            background-size: cover;
        }
        .center-container {
            display: flex;
            justify-content: center;
            align-items: center;
            height: calc(100vh - 200px); /* Adjust the height according to your navbar and footer */
        }
        .register-form {
            width: 600px;
            padding: 30px;
            background-color: rgba(255, 255, 255, 0.8);
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }
        .register-form h2 {
            text-align: center;
            margin-bottom: 20px;
        }
        .register-form .form-group {
            margin-bottom: 15px;
        }
        .register-form .btn {
            width: 100%;
            background-color: black;
            color: white;
            font-weight: bold;
        }
        .login-link, .social-login {
            text-align: center;
            margin-top: 20px;
        }
        .social-login a {
            margin: 0 10px;
            font-size: 24px;
            color: #555;
        }
        .social-login a:hover {
            color: #000;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container center-container">
        <div class="register-form">
            <h2>Register</h2>
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />
            <div class="form-group">
                <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" Placeholder="First Name" />
            </div>
            <div class="form-group">
                <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" Placeholder="Last Name" />
            </div>
            <div class="form-group">
                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" Placeholder="Username" />
            </div>
            <div class="form-group">
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="Email" TextMode="Email" />
            </div>
            <div class="form-group">
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" Placeholder="Password" TextMode="Password" />
            </div>
            <div class="form-group">
                <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" Placeholder="Confirm Password" TextMode="Password" />
            </div>
            <div class="form-group">
                <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="form-control" Placeholder="Phone Number" />
            </div>
            <div class="login-link">
                <p>Have an account? <a href="/Login.aspx">Login here</a></p>
            </div>
            <div class="social-login">
                <a href="#" title="Login with Facebook"><i class="fab fa-facebook-f"></i></a>
                <a href="#" title="Login with Twitter"><i class="fab fa-twitter"></i></a>
                <a href="#" title="Login with LinkedIn"><i class="fab fa-linkedin-in"></i></a>
            </div>
            <div class="form-group">
                <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn btn-primary" OnClick="btnRegister_Click" />
            </div>
        </div>
    </div>
</asp:Content>
