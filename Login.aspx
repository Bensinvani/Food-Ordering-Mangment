<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Food_Ordering_Mangment.Login" %>

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
        .login-form {
            width: 600px;
            padding: 30px;
            background-color: rgba(255, 255, 255, 0.8);
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }
        .login-form h2 {
            text-align: center;
            margin-bottom: 20px;
        }
        .login-form .form-group {
            margin-bottom: 15px;
        }
        .login-form .btn {
            width: 100%;
            background-color: black;
            color: white;
            font-weight: bold;
        }
        .register-link, .social-login {
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
    <div id="LoginContainer" runat="server">
        <div class="container center-container">
            <div class="login-form">
                <h2>Login</h2>
                <asp:Literal ID="LtlMsg" runat="server" />
                <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                <div class="form-group">
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="Enter your Email"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" Placeholder="Enter your password"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary login-btn" OnClick="btnLogin_Click" />
                </div>
                <div class="register-link">
                    <p>Don't have an account? <asp:HyperLink ID="hlRegister" runat="server" NavigateUrl="/Register.aspx" CssClass="text-black">Register here</asp:HyperLink></p>
                </div>
                <div class="social-login">
                    <p>Or login with:</p>
                    <a href="#" title="Login with Facebook"><i class="fab fa-facebook-f"></i></a>
                    <a href="#" title="Login with Twitter"><i class="fab fa-twitter"></i></a>
                    <a href="#" title="Login with LinkedIn"><i class="fab fa-linkedin-in"></i></a>
                </div>
            </div>
        </div>
    </div>
    <asp:Literal ID="LtlLoggedUser" runat="server" />
</asp:Content>
