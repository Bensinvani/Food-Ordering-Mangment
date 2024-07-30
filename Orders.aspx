<%@ Page Title="Your Cart" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="Food_Ordering_Mangment.Orders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .cart-card {
            border: 1px solid #ddd;
            border-radius: 10px;
            padding: 20px;
            margin-bottom: 20px;
            background-color: #fff;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }

        .cart-item {
            display: flex;
            justify-content: space-between;
            align-items: center;
            padding: 10px 0;
            border-bottom: 1px solid #ddd;
        }

        .cart-item img {
            width: 80px;
            height: 80px;
            object-fit: cover;
            border-radius: 10px;
        }

        .quantity-selector button {
            background-color: #000;
            color: #fff;
            border: none;
            padding: 5px 10px;
            cursor: pointer;
            border-radius: 5px;
            margin: 0 5px;
        }

        .quantity-selector input {
            width: 50px;
            text-align: center;
            border: 1px solid #ddd;
            border-radius: 5px;
        }

        .remove-item-btn {
            background-color: #ff4d4d;
            color: white;
            border: none;
            padding: 5px 10px;
            border-radius: 5px;
            cursor: pointer;
        }

        .checkout-btn {
            background-color: #000;
            color: white;
            border: none;
            padding: 10px 20px;
            border-radius: 5px;
            cursor: pointer;
            margin-top: 20px;
            display: block;
            width: 100%;
            text-align: center;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container cart-summary">
        <h2 class="text-center">Your Cart</h2>
        <asp:Repeater ID="OrdersRepeater" runat="server">
            <ItemTemplate>
                <div class="cart-card">
                    <div class="cart-items">
                        <asp:Repeater ID="OrderItemsRepeater" runat="server" DataSource='<%# Eval("Items") %>'>
                            <ItemTemplate>
                                <div class="cart-item">
                                    <img src='<%# "/Images/MenuItem/" + Eval("ImageUrl") %>' alt='<%# Eval("Name") %>' />
                                    <span><%# Eval("Name") %></span>
                                    <span>Unit Price: NIS <%# Eval("UnitPrice", "{0:F2}") %></span>
                                    <div class="quantity-selector">
                                        <asp:Button ID="Btn_Decrement" runat="server" Text="-" OnClick="Btn_Decrement_Click" CommandArgument='<%# Eval("MenuItemId") %>' />
                                        <asp:TextBox ID="Quantity" runat="server" Text='<%# Eval("Quantity") %>' Width="40px" ReadOnly="true" />
                                        <asp:Button ID="Btn_Increment" runat="server" Text="+" OnClick="Btn_Increment_Click" CommandArgument='<%# Eval("MenuItemId") %>' />
                                    </div>
                                    <span>Total: NIS <%# Eval("TotalPrice", "{0:F2}") %></span>
                                    <asp:Button ID="RemoveItem" runat="server" Text="Remove" OnClick="RemoveItem_Click" CommandArgument='<%# Eval("MenuItemId") %>' CssClass="remove-item-btn" />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <div class="cart-total">
                        <h3>Total: NIS <%# Eval("TotalAmount", "{0:F2}") %></h3>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <asp:Button ID="CheckoutButton" runat="server" Text="Checkout" CssClass="checkout-btn" OnClick="CheckoutButton_Click" />
        <asp:Panel ID="EmptyCartPanel" runat="server" Visible="false">
            <div class="cart-card">
                <h3 class="text-center">Your cart is empty.</h3>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
