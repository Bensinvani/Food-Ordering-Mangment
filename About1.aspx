<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About1.aspx.cs" Inherits="Food_Ordering_Mangment.About1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/aos/2.3.4/aos.css" rel="stylesheet">
    <style>
        .hero-section {
            background: url('/Images/About/restaurant.jpg') center center no-repeat;
            background-size: cover;
            height: 80vh;
            display: flex;
            align-items: center;
            justify-content: center;
            color: white;
            text-align: center;
        }

        .section {
            padding: 60px 0;
        }

        .team-member img {
            border-radius: 50%;
        }

        .review {
            background: #f8f9fa;
            border-radius: 10px;
            padding: 20px;
            margin-bottom: 20px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Hero Section -->
    <div class="hero-section">
        <div>
            <h1 data-aos="fade-up">Welcome to Our Restaurant</h1>
            <p data-aos="fade-up" data-aos-delay="100">Experience the best dining in town</p>
        </div>
    </div>

    <!-- Introduction Section -->
    <div class="container section">
        <div class="row">
            <div class="col-md-6" data-aos="fade-right">
                <img src="/Images/About/chef.jpg" class="img-fluid" alt="Chef">
            </div>
            <div class="col-md-6" data-aos="fade-left">
                <h2>About Us</h2>
                <p>Our restaurant offers a unique dining experience, combining delicious cuisine with a warm and welcoming atmosphere. We are dedicated to providing our guests with exceptional service and unforgettable meals.</p>
            </div>
        </div>
    </div>

    <!-- History Section -->
    <div class="container section bg-light">
        <div class="row">
            <div class="col-md-6 order-md-2" data-aos="fade-left">
                <img src="/Images/About/restaurant_interior.jpg" class="img-fluid" alt="Restaurant Interior">
            </div>
            <div class="col-md-6 order-md-1" data-aos="fade-right">
                <h2>Our History</h2>
                <p>Established in 1990, our restaurant has a rich history of serving delicious meals to our community. Over the years, we have grown and evolved, but our commitment to quality and service has remained the same.</p>
            </div>
        </div>
    </div>

    <!-- Team Section -->
    <div class="container section">
        <h2 class="text-center" data-aos="fade-up">Meet Our Founders</h2>
        <div class="row text-center">
            <div class="col-md-4" data-aos="flip-left">
                <div class="team-member">
                    <img src="/Images/About/chef.jpg" class="img-fluid" alt="Team Member 1">
                    <h5>John Doe</h5>
                    <p>Head Chef</p>
                </div>
            </div>
            <div class="col-md-4" data-aos="flip-up">
                <div class="team-member">
                    <img src="/Images/About/chef1.jpg" class="img-fluid" alt="Team Member 2">
                    <h5>Jane Smith</h5>
                    <p>Manager</p>
                </div>
            </div>
            <div class="col-md-4" data-aos="flip-right">
                <div class="team-member">
                    <img src="/Images/About/chef2.jpg" class="img-fluid" alt="Team Member 3">
                    <h5>Mike Johnson</h5>
                    <p>Sous Chef</p>
                </div>
            </div>
        </div>
    </div>

    <!-- Specialties Section -->
    <div class="container section bg-light">
        <h2 class="text-center" data-aos="fade-up">Our Specialties</h2>
        <div class="row">
            <div class="col-md-4" data-aos="zoom-in">
                <img src="/Images/HomeOffers/chicken.jpg" class="img-fluid" alt="Specialty 1">
                <h5>Specialty Dish 1</h5>
                <p>A delightful blend of flavors and textures that will leave you craving for more.</p>
            </div>
            <div class="col-md-4" data-aos="zoom-in" data-aos-delay="100">
                <img src="/Images/HomeOffers/burgers1.jpg" class="img-fluid" alt="Specialty 2">
                <h5>Specialty Dish 2</h5>
                <p>A delightful blend of flavors and textures that will leave you craving for more.</p>
            </div>
            <div class="col-md-4" data-aos="zoom-in" data-aos-delay="200">
                <img src="/Images/HomeOffers/dessert.jpg" class="img-fluid" alt="Specialty 3">
                <h5>Specialty Dish 3</h5>
                <p>A delightful blend of flavors and textures that will leave you craving for more.</p>
            </div>
        </div>
    </div>

    <!-- Customer Reviews Section -->
    <div class="container section">
        <h2 class="text-center" data-aos="fade-up">What Our Customers Say</h2>
        <div class="row">
            <div class="col-md-6" data-aos="fade-right">
                <div class="review">
                    <h5>Mary Williams</h5>
                    <p>"The food was absolutely wonderful, from preparation to presentation, very pleasing. We especially enjoyed the special bar drinks, the cucumber/cilantro infused vodka martini was a delightful treat."</p>
                </div>
            </div>
            <div class="col-md-6" data-aos="fade-left">
                <div class="review">
                    <h5>James Brown</h5>
                    <p>"The ambiance is cozy and the service is outstanding. Our meals were absolutely delicious. We will definitely come back!"</p>
                </div>
            </div>
        </div>
    </div>

    <!-- Include AOS Script -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/aos/2.3.4/aos.js"></script>
    <script>
        AOS.init({
            duration: 1200,
        });
    </script>
</asp:Content>
