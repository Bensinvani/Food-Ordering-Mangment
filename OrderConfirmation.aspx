<%@ Page Title="Order Confirmation" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrderConfirmation.aspx.cs" Inherits="Food_Ordering_Mangment.OrderConfirmation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .confirmation-container { margin-top: 20px; text-align: center; }
        .confirmation-container h2 { margin-bottom: 20px; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container confirmation-container">
        <h2>Thank you for your order!</h2>
        <p>Your order has been placed successfully. You will receive a confirmation email shortly.</p>
        <p>Your order number is: <asp:Label ID="lblOrderNumber" runat="server" /></p>
        <asp:HyperLink ID="hlHome" runat="server" NavigateUrl="~/Home.aspx" CssClass="btn btn-primary">Back to Home</asp:HyperLink>
    </div>
</asp:Content>
