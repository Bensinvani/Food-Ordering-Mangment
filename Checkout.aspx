<%@ Page Title="Checkout" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="Food_Ordering_Mangment.Checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .checkout-container {
            margin-top: 20px;
        }
        .checkout-form, .order-summary {
            border: 1px solid #ddd;
            padding: 20px;
            border-radius: 5px;
            background-color: #fff;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
        }
        .checkout-form h2, .order-summary h3 {
            margin-bottom: 20px;
            font-size: 24px;
            color: #333;
        }
        .form-group {
            margin-bottom: 15px;
        }
        .form-group label {
            display: block;
            margin-bottom: 5px;
            font-weight: bold;
            color: #555;
        }
        .form-group input, .form-group select {
            width: 100%;
            padding: 10px;
            border: 1px solid #ddd;
            border-radius: 5px;
            transition: border-color 0.2s;
        }
        .form-group input:focus, .form-group select:focus {
            border-color: #007bff;
        }
        .submit-btn {
            width: 100%;
            padding: 15px;
            background-color: #28a745;
            color: white;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 16px;
            font-weight: bold;
            text-transform: uppercase;
            transition: background-color 0.2s;
        }
        .submit-btn:hover {
            background-color: #218838;
        }
        .order-summary h3 {
            font-size: 24px;
            margin-bottom: 20px;
            text-align: center;
        }
        .order-summary .summary-details {
            display: flex;
            justify-content: space-between;
            margin-bottom: 10px;
        }
        .order-summary .summary-details label {
            font-weight: bold;
        }
        .order-summary .order-total {
            font-size: 24px;
            font-weight: bold;
            text-align: right;
            margin-top: 20px;
        }
        .order-item {
            display: flex;
            align-items: center;
            margin-bottom: 20px;
            border-bottom: 1px solid #ddd;
            padding-bottom: 10px;
            justify-content: space-between;
        }
        .order-item img {
            width: 80px;
            height: 80px;
            margin-right: 20px;
            border-radius: 5px;
            object-fit: cover;
        }
        .order-item-details {
            flex-grow: 1;
            display: flex;
            flex-direction: column;
        }
        .order-item-details div {
            margin-bottom: 5px;
        }
        .order-item-details .item-code,
        .order-item-details .price-quantity {
            font-size: 14px;
            color: #666;
        }
        .order-item-details .item-name {
            font-size: 18px;
            font-weight: bold;
            color: #333;
        }
        .order-item-total {
            font-size: 16px;
            font-weight: bold;
            color: #333;
            text-align: right;
        }
        hr {
            border: 0;
            border-top: 1px solid #ddd;
            margin: 20px 0;
        }
        .total-amount {
            font-size: 20px;
            font-weight: bold;
            text-align: right;
            color: #333;
        }
        .quantity-selector {
            display: flex;
            align-items: center;
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
            background-color: black;
            color: white;
        }
        .remove-item-btn {
            background-color: #ff4d4d;
            color: white;
            border: none;
            padding: 5px 10px;
            border-radius: 5px;
            cursor: pointer;
        }
        .back-btn {
            background-color: #6c757d;
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

        /* Responsive Styles */
        @media (max-width: 768px) {
            .checkout-container {
                grid-template-columns: 1fr;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container checkout-container">
        <div class="py-5 text-center">
            <img class="d-block mx-auto mb-4" src="Images/HomeOffers/Logo.png" alt="" width="72" height="57">
            <h2>Checkout</h2>
            <p class="lead">Complete the form below to finish your order.</p>
        </div>
        <div class="row g-5">
            <div class="col-md-5 col-lg-4 order-md-last order-summary">
                <h4 class="d-flex justify-content-between align-items-center mb-3">
                    <span class="text-primary">Your cart</span>
                    <span class="badge bg-primary rounded-pill"><%# rptOrderSummary.Items.Count %></span>
                </h4>
                <ul class="list-group mb-3">
                    <asp:Repeater ID="rptOrderSummary" runat="server">
                        <ItemTemplate>
                            <li class="list-group-item d-flex justify-content-between lh-sm">
                                <div class="order-item">
                                    <img src='<%# "/Images/MenuItem/" + Eval("ImageUrl") %>' alt='<%# Eval("Name") %>' />
                                    <div class="order-item-details">
                                        <div class="item-name"><%# Eval("Name") %></div>
                                        <div class="item-code">Item Code: <%# Eval("MenuItemId") %></div>
                                        <div class="price-quantity">
                                            <div class="quantity-selector">
                                                <asp:Button ID="Btn_Decrement" runat="server" Text="-" OnClick="Btn_Decrement_Click" CommandArgument='<%# Eval("MenuItemId") %>' />
                                                <asp:TextBox ID="Quantity" runat="server" Text='<%# Eval("Quantity") %>' Width="40px" ReadOnly="true" />
                                                <asp:Button ID="Btn_Increment" runat="server" Text="+" OnClick="Btn_Increment_Click" CommandArgument='<%# Eval("MenuItemId") %>' />
                                            </div>
                                            <asp:Button ID="RemoveItem" runat="server" Text="Remove" OnClick="RemoveItem_Click" CommandArgument='<%# Eval("MenuItemId") %>' CssClass="remove-item-btn" />
                                        </div>
                                    </div>
                                    <div class="order-item-total">Total: NIS <%# Eval("TotalPrice", "{0:F2}") %></div>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                    <li class="list-group-item d-flex justify-content-between">
                        <span>Total (NIS)</span>
                        <strong>NIS <asp:Label ID="lblTotalAmount" runat="server" Text=""></asp:Label></strong>
                    </li>
                </ul>

                <form class="card p-2">
                    <div class="input-group">
                        <input type="text" class="form-control" placeholder="Promo code">
                        <button type="submit" class="btn btn-secondary">Redeem</button>
                    </div>
                </form>
                <asp:Button ID="BackButton" runat="server" Text="Back to Orders" CssClass="back-btn" OnClick="BackButton_Click" />
            </div>

            <div class="col-md-7 col-lg-8 checkout-form">
                <h4 class="mb-3">Billing address</h4>
                <asp:Literal ID="LtlMsg" runat="server" />
                <div class="row g-3">
                    <div class="col-sm-6">
                        <label for="txtStreet" class="form-label">Street</label>
                        <asp:TextBox ID="txtStreet" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-sm-6">
                        <label for="txtCity" class="form-label">City</label>
                        <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-sm-6">
                        <label for="txtState" class="form-label">State</label>
                        <asp:TextBox ID="txtState" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-sm-6">
                        <label for="txtZipCode" class="form-label">Zip Code</label>
                        <asp:TextBox ID="txtZipCode" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-12">
                        <label for="txtCountry" class="form-label">Country</label>
                        <asp:TextBox ID="txtCountry" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-12">
                        <label for="ddlPaymentMethod" class="form-label">Payment Method</label>
                        <asp:DropDownList ID="ddlPaymentMethod" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPaymentMethod_SelectedIndexChanged">
                            <asp:ListItem Text="Credit Card" Value="CreditCard" />
                            <asp:ListItem Text="PayPal" Value="PayPal" />
                            <asp:ListItem Text="Cash" Value="Cash" />
                        </asp:DropDownList>
                    </div>
                </div>

                <asp:Panel ID="pnlCreditCard" runat="server" Visible="false">
                    <div class="form-group">
                        <label for="txtCardNumber">Card Number</label>
                        <asp:TextBox ID="txtCardNumber" runat="server" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="txtCardHolderName">Card Holder Name</label>
                        <asp:TextBox ID="txtCardHolderName" runat="server" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="txtCardExpiry">Expiration Date</label>
                        <asp:TextBox ID="txtCardExpiry" runat="server" CssClass="form-control" />
                    </div>
                    <div class="form-group">
                        <label for="txtCardCVV">CVV</label>
                        <asp:TextBox ID="txtCardCVV" runat="server" CssClass="form-control" />
                    </div>
                    <div class="form-group checkbox">
                        <label for="chkSaveCard">Save card for future purchases</label>
                        <asp:CheckBox ID="chkSaveCard" runat="server" />
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlPayPal" runat="server" Visible="false">
                    <div class="form-group">
                        <label for="txtPayPalEmail">PayPal Email</label>
                        <asp:TextBox ID="txtPayPalEmail" runat="server" CssClass="form-control" />
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlCash" runat="server" Visible="false">
                    <div class="form-group">
                        <asp:Label ID="lblCash" runat="server" Text="Please prepare the exact amount in cash." />
                    </div>
                </asp:Panel>
                <hr class="my-4">
                <asp:Button ID="btnSubmit" runat="server" Text="Place Order" CssClass="submit-btn btn btn-primary btn-lg w-100" OnClick="btnSubmit_Click" />
            </div>
        </div>
    </div>
</asp:Content>
