<%@ Page Title="Menu" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="Food_Ordering_Mangment.Menu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .quantity-selector button {
            background-color: #000;
            color: #fff;
            border: none;
            padding: 5px 10px;
            cursor: pointer;
            border-radius: 5px;
        }

        .quantity-selector input {
            width: 40px;
            text-align: center;
            border: 1px solid #ddd;
            border-radius: 5px;
            margin-bottom: 15px;
            color: white;
            background-color: black;
        }

        .add-to-cart-btn {
            background-color: #000;
            color: white;
            border: none;
            padding: 10px;
            border-radius: 5px;
            cursor: pointer;
            width: 100%;
        }

        .add-to-cart-btn:hover {
            background-color: #333;
        }

        .category-title {
            font-size: 1.5rem;
            margin-top: 1rem;
        }

        .product-card {
            border: 1px solid #ddd;
            border-radius: 10px;
            transition: transform 0.2s;
            margin-bottom: 20px;
            padding: 10px;
            background-color: #fff;
        }

        .product-card:hover {
            transform: scale(1.05);
            box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.1);
        }

        .product-img {
            height: 200px;
            object-fit: cover;
            width: 100%;
            border-radius: 10px;
        }

        .product-price {
            font-size: 1.25rem;
            font-weight: bold;
            color: #333;
        }

        .search-container {
            display: flex;
            flex-direction: column;
            align-items: center;
            margin-top: 20px;
            margin-bottom: 20px;
        }

        .search-bar {
            display: flex;
            align-items: center;
            border: 1px solid #ddd;
            border-radius: 5px;
            padding: 5px;
            width: 300px;
            position: relative;
        }

        .search-bar input {
            border: none;
            outline: none;
            width: 100%;
            padding-left: 5px;
        }

        .search-bar button {
            background: none;
            border: none;
            cursor: pointer;
        }

        .ajax__autocomplete_container {
            background-color: white;
            border: 1px solid #ddd;
            border-radius: 5px;
            width: 100%;
            z-index: 1000;
            position: absolute;
            top: 40px;
        }

        .ajax__autocomplete_item {
            padding: 5px;
            cursor: pointer;
        }

        .ajax__autocomplete_item:hover {
            background-color: #f1f1f1;
        }

        .category-list {
            position: sticky;
            top: 100px;
            left: 20px;
            width: 300px;
            height: 500px;
            overflow-y: auto;
            background-color: #fff;
            border: 1px solid #ddd;
            border-radius: 5px;
            padding: 30px;
            margin-right: 20px;
        }

        .category-list .category-item {
            display: flex;
            align-items: center;
            margin-bottom: 15px;
            transition: background-color 0.3s;
            padding: 10px;
            border-radius: 10px;
        }

        .category-list .category-item img {
            width: 50px;
            height: 50px;
            margin-right: 15px;
            border-radius: 50%;
        }

        .category-list .category-item span {
            font-size: 18px;
            font-weight: 500;
        }

        .category-list .category-item a {
            color: black;
            text-decoration: none;
            display: flex;
            align-items: center;
        }

        .category-list .category-item:hover {
            background-color: #f1f1f1;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container mt-5">
        <h1 class="text-center category-title"><%= CategoryName %></h1>
        <h2 class="text-center">Menu</h2>

        <div class="search-container">
            <div class="search-bar">
                <asp:TextBox ID="searchBox" runat="server" CssClass="form-control" placeholder="Which product are you looking for?" AutoPostBack="true" OnTextChanged="searchBox_TextChanged"></asp:TextBox>
                <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="searchBox" ServiceMethod="GetMenuItems" MinimumPrefixLength="1" CompletionSetCount="10" CompletionInterval="100"></asp:AutoCompleteExtender>
                <button type="button" onclick="searchProduct()"><i class="fas fa-search"></i></button>
            </div>
        </div>

        <div class="row">
            <div class="col-md-3">
                <div class="category-list">
                    <asp:Repeater ID="CategoryRepeater" runat="server">
                        <ItemTemplate>
                            <div class="category-item">
                                <a href='Menu.aspx?CategoryId=<%# Eval("CategoryId") %>'>
                                    <img src='<%# "/Images/Category/" + Eval("CategoryImage") %>' alt='<%# Eval("CategoryName") %>' />
                                    <span><%# Eval("CategoryName") %></span>
                                </a>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div class="col-md-9">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                    <ContentTemplate>
                        <div class="row row-cols-1 row-cols-md-3 g-4">
                            <asp:Repeater ID="MenuItemsRepeater" runat="server">
                                <ItemTemplate>
                                    <div class="col">
                                        <div class="card product-card">
                                            <img src='<%# "/Images/MenuItem/" + Eval("ImageUrl") %>' class="card-img-top product-img" alt='<%# Eval("Name") %>'>
                                            <div class="card-body">
                                                <h5 class="card-title"><%# Eval("Name") %></h5>
                                                <p class="card-text"><%# Eval("Description") %></p>
                                                <p class="product-price">NIS <%# Eval("Price") %></p>
                                                <div class="quantity-selector">
                                                    <asp:Button ID="Btn_Decrement" runat="server" Text="-" OnClick="Btn_Decrement_Click" CommandArgument='<%# Eval("MenuItemId") %>' CssClass="quantity-button" />
                                                    <asp:TextBox ID="Quantity" runat="server" Text="1" Width="40px" />
                                                    <asp:Button ID="Btn_Increment" runat="server" Text="+" OnClick="Btn_Increment_Click" CommandArgument='<%# Eval("MenuItemId") %>' CssClass="quantity-button" />
                                                </div>
                                                <asp:Button ID="AddToCart" runat="server" Text="Add to Cart" OnClick="AddToCart_Click" CommandArgument='<%# Eval("MenuItemId") %>' CssClass="add-to-cart-btn" />
                                                <asp:Label ID="ItemMessage" runat="server" ForeColor="Green" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function searchProduct() {
            var searchTerm = document.getElementById('<%= searchBox.ClientID %>').value;
            if (searchTerm) {
                window.location.href = 'Menu.aspx?Search=' + encodeURIComponent(searchTerm);
            }
        }

        function hideMessage(labelId) {
            setTimeout(function () {
                document.getElementById(labelId).style.display = 'none';
            }, 3000);
        }
    </script>
</asp:Content>
