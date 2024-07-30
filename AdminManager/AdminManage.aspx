<%@ Page Title="Admin Management" Language="C#" MasterPageFile="~/AdminManager/BackAdmin.Master" AutoEventWireup="true" CodeBehind="AdminManage.aspx.cs" Inherits="Food_Ordering_Mangment.AdminManager.AdminManage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .stat-card {
            border: 1px solid #ddd;
            border-radius: 0.25rem;
            padding: 1rem;
            margin-bottom: 1rem;
            text-align: center;
            background-color: #f8f9fa;
        }
        .stat-card h5 {
            margin-bottom: 0.5rem;
        }
        .table-responsive {
            margin-top: 1rem;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <div class="container-fluid px-4">
        <h1 class="mt-4">Admin Dashboard</h1>
        <ol class="breadcrumb mb-4">
            <li class="breadcrumb-item active">Dashboard</li>
        </ol>

        <!-- Statistics Cards -->
        <div class="row">
            <div class="col-xl-3 col-md-6">
                <div class="stat-card">
                    <h5>Total Registered Users</h5>
                    <h2><asp:Label ID="lblTotalUsers" runat="server" Text="0"></asp:Label></h2>
                </div>
            </div>
            <div class="col-xl-3 col-md-6">
                <div class="stat-card">
                    <h5>Total Orders</h5>
                    <h2><asp:Label ID="lblTotalOrders" runat="server" Text="0"></asp:Label></h2>
                </div>
            </div>
            <div class="col-xl-3 col-md-6">
                <div class="stat-card">
                    <h5>Total Contact Messages</h5>
                    <h2><asp:Label ID="lblTotalContacts" runat="server" Text="0"></asp:Label></h2>
                </div>
            </div>
        </div>

        <!-- Registered Users Table -->
        <div class="card mb-4">
            <div class="card-header">
                <i class="fas fa-table me-1"></i>
                Registered Users
            </div>
            <div class="card-body">
                <div class="table-responsive">
                    <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
                        <Columns>
                            <asp:BoundField DataField="UserId" HeaderText="User ID" />
                            <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                            <asp:BoundField DataField="LastName" HeaderText="Last Name" />
                            <asp:BoundField DataField="Email" HeaderText="Email" />
                            <asp:BoundField DataField="PhoneNumber" HeaderText="Phone Number" />
                            <asp:BoundField DataField="IsAdmin" HeaderText="Admin" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>

        <!-- Orders Table -->
        <div class="card mb-4">
            <div class="card-header">
                <i class="fas fa-table me-1"></i>
                Orders
            </div>
            <div class="card-body">
                <div class="table-responsive">
                    <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
                        <Columns>
                            <asp:BoundField DataField="OrderId" HeaderText="Order ID" />
                            <asp:BoundField DataField="UserId" HeaderText="User ID" />
                            <asp:BoundField DataField="OrderDate" HeaderText="Order Date" DataFormatString="{0:yyyy-MM-dd}" />
                            <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" DataFormatString="{0:C}" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>

        <!-- Contact Messages Table -->
        <div class="card mb-4">
            <div class="card-header">
                <i class="fas fa-table me-1"></i>
                Contact Messages
            </div>
            <div class="card-body">
                <div class="table-responsive">
                    <asp:GridView ID="gvContacts" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" OnRowCommand="gvContacts_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="ContactId" HeaderText="Contact ID" />
                            <asp:BoundField DataField="Name" HeaderText="Name" />
                            <asp:BoundField DataField="Email" HeaderText="Email" />
                            <asp:BoundField DataField="Message" HeaderText="Message" />
                            <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:yyyy-MM-dd}" />
                            <asp:BoundField DataField="IsReplied" HeaderText="Replied" DataFormatString="{0:Yes;0:No}" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtReply" runat="server" CssClass="form-control" Text='<%# Eval("IsReplied") %>' />
                                    <asp:Button ID="btnReply" runat="server" Text="Reply" CommandName="Reply" CommandArgument='<%# Eval("ContactId") %>' CssClass="btn btn-primary mt-2" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="UnderFooter" runat="server">
</asp:Content>
