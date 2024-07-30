<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Food_Ordering_Mangment.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .hero {
            background-image: url('/Images/HomeOffers/burgers.jpg');
            background-size: cover;
            background-position: center;
            height: 80vh;
            color: white;
            display: flex;
            align-items: center;
            justify-content: center;
            text-align: center;
        }

        .promo {
            padding: 2rem;
            background-color: #f8f9fa;
        }

        .promo h2 {
            margin-bottom: 1rem;
        }

        .featured-items .card {
            margin-bottom: 1rem;
            width: 18rem;
        }

        .featured-items .card-img-top {
            height: 180px;
            object-fit: cover;
        }

        .featured-items .card:hover {
            transform: scale(1.05);
            transition: transform 0.2s;
        }

        .carousel-item img {
            height: 1000px;
            width: 800px;
            object-fit: cover;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Hero Section -->
    <div class="hero">
        <div>
            <h1>Welcome to Food Order</h1>
            <p>Delicious food, delivered to you</p>
            <a href="/Menu.aspx" class="btn btn-primary btn-lg">Order Now</a>
        </div>
    </div>

    <!-- Promotional Section -->
    <div class="promo text-center">
        <h2>Special Offers</h2>
        <p>Don't miss our special offers. Grab them before they're gone!</p>
        <a href="/Menu.aspx" class="btn btn-danger">View Offers</a>
    </div>

    <!-- Carousel Section -->
    <div id="productCarousel" class="carousel slide mb-6" data-bs-ride="carousel">
        <div class="carousel-indicators">
            <asp:Repeater ID="CarouselIndicatorsRepeater" runat="server">
                <ItemTemplate>
                    <li data-bs-target="#productCarousel" data-bs-slide-to="<%# Container.ItemIndex %>" class="<%# Container.ItemIndex == 0 ? "active" : "" %>"></li>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="carousel-inner">
            <asp:Repeater ID="ProductCarouselRepeater" runat="server">
                <ItemTemplate>
                    <div class="carousel-item <%# Container.ItemIndex == 0 ? "active" : "" %>">
                        <img src='<%# "/Images/MenuItem/" + Eval("ImageUrl") %>' class="d-block w-100" alt='<%# Eval("Name") %>'>
                        <div class="carousel-caption d-none d-md-block">
                            <h5><%# Eval("Name") %></h5>
                            <p><%# Eval("Description") %></p>
                            <a href='<%# "/Menu.aspx?MenuItemId=" + Eval("MenuItemId") %>' class="btn btn-primary">Order Now</a>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <button class="carousel-control-prev" type="button" data-bs-target="#productCarousel" data-bs-slide="prev">
            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
            <span class="visually-hidden">Previous</span>
        </button>
        <button class="carousel-control-next" type="button" data-bs-target="#productCarousel" data-bs-slide="next">
            <span class="carousel-control-next-icon" aria-hidden="true"></span>
            <span class="visually-hidden">Next</span>
        </button>
    </div>

    <!-- Featured Items Section -->
    <div class="container featured-items">
        <h2 class="text-center my-4">Featured Items</h2>
        <div class="row">
            <asp:Repeater ID="FeaturedItemsRepeater" runat="server">
                <ItemTemplate>
                    <div class="col-md-4">
                        <div class="card">
                            <img src='<%# "/Images/MenuItem/" + Eval("ImageUrl") %>' class="card-img-top" alt='<%# Eval("Name") %>'>
                            <div class="card-body">
                                <h5 class="card-title"><%# Eval("Name") %></h5>
                                <p class="card-text"><%# Eval("Description") %></p>
                                <a href='<%# "/Menu.aspx?MenuItemId=" + Eval("MenuItemId") %>' class="btn btn-primary">Order Now</a>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
