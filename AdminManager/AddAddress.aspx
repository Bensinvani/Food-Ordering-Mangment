<%@ Page Title="Add Address" Language="C#" MasterPageFile="~/AdminManager/BackAdmin.Master" AutoEventWireup="true" CodeBehind="AddAddress.aspx.cs" Inherits="Food_Ordering_Mangment.AdminManager.AddAddress" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.2/css/all.min.css" integrity="sha512-SnH5WK+bZxgPHs44uWIX+LLJAJ9/2PkPKZ5QiAj6Ta86w+fsb2TkcmfRyVX3pBnMFcV7oQPJkl9QevSCWr3W6A==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <style>
        .form-container {
            max-width: 400px;
            margin: 20px;
            padding: 20px;
            border: 1px solid #ddd;
            border-radius: 10px;
            background-color: #f9f9f9;
        }
        .form-container h2 {
            text-align: center;
            margin-bottom: 20px;
        }
        .table-container {
            margin: 20px;
        }
        .table-container table {
            width: 100%;
            border-collapse: collapse;
        }
        .table-container th, .table-container td {
            padding: 10px;
            border: 1px solid #ddd;
            text-align: left;
        }
        .table-container th {
            background-color: #f1f1f1;
        }
        .table-container .action-icons {
            text-align: center;
        }
        .table-container .action-icons a {
            margin: 0 5px;
            color: #007bff;
            text-decoration: none;
        }
        .table-container .action-icons a:hover {
            color: #0056b3;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <div class="container-fluid px-4">
        <div class="row">
            <div class="col-lg-4">
                <div class="form-container">
                    <h2>Add New Address</h2>
                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />
                    <asp:DropDownList ID="ddlUsers" runat="server" CssClass="form-control" />
                    <br />
                    <asp:TextBox ID="txtStreet" runat="server" CssClass="form-control" Placeholder="Street" />
                    <br />
                    <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" Placeholder="City" />
                    <br />
                    <asp:TextBox ID="txtState" runat="server" CssClass="form-control" Placeholder="State" />
                    <br />
                    <asp:TextBox ID="txtZipCode" runat="server" CssClass="form-control" Placeholder="Zip Code" />
                    <br />
                    <asp:TextBox ID="txtCountry" runat="server" CssClass="form-control" Placeholder="Country" />
                    <br />
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary btn-block" OnClick="btnSave_Click" />
                </div>
            </div>
            <div class="col-lg-8">
                <div class="table-container">
                    <asp:HiddenField ID="HidAddressId" Value="-1" runat="server" />
                    <h2>Manage Addresses</h2>
                    <asp:GridView ID="gvAddresses" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" GridLines="None" ShowHeaderWhenEmpty="True" EmptyDataText="No addresses found.">
                        <Columns>
                            <asp:BoundField DataField="AddressId" HeaderText="ID" />
                            <asp:BoundField DataField="UserId" HeaderText="User ID" />
                            <asp:BoundField DataField="Street" HeaderText="Street" />
                            <asp:BoundField DataField="City" HeaderText="City" />
                            <asp:BoundField DataField="State" HeaderText="State" />
                            <asp:BoundField DataField="ZipCode" HeaderText="Zip Code" />
                            <asp:BoundField DataField="Country" HeaderText="Country" />
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <div class="action-icons">
                                        <a href="AddAddress.aspx?AddressId=<%# Eval("AddressId") %>" title="Edit">
                                            <i class="fas fa-edit"></i> Edit
                                        </a>
                                        <a href="AddAddress.aspx?AddressId=<%# Eval("AddressId") %>&action=delete" title="Delete" onclick="return confirm('Are you sure you want to delete this address?');">
                                            <i class="fas fa-trash-alt"></i> Delete
                                        </a>
                                    </div>
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
