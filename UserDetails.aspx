<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserDetails.aspx.cs" Inherits="Food_Ordering_Mangment.UserDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .user-details-form {
            width: 100%;
            max-width: 800px;
            margin: 0 auto;
            padding: 30px;
            background-color: rgba(255, 255, 255, 0.8);
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }
        .user-details-form h2 {
            text-align: center;
            margin-bottom: 20px;
        }
        .user-details-form .form-group {
            display: flex;
            align-items: center;
            margin-bottom: 15px;
        }
        .user-details-form .form-group label {
            flex: 1;
        }
        .user-details-form .form-group input {
            flex: 2;
            max-width: 100%;
        }
        .user-details-form .edit-button {
            cursor: pointer;
            margin-left: 10px;
        }
        .user-details-form .edit-button i {
            color: #007bff;
        }
        .user-details-form .btn {
            width: 100%;
            background-color: black;
            color: white;
            font-weight: bold;
        }
        .orders, .contacts {
            margin-top: 30px;
        }
        .editable {
            border: 1px solid #007bff;
        }
        .save-button {
            display: none;
            margin-top: 20px;
            background-color: black;
            color: white;
        }
    </style>
    <script>
        function toggleEdit(button) {
            var textBoxes = document.querySelectorAll('.user-details-form input');
            var isEditMode = button.textContent.trim() === 'Edit';

            textBoxes.forEach(function (textBox) {
                if (isEditMode) {
                    textBox.removeAttribute('readonly');
                    textBox.classList.add('editable');
                } else {
                    textBox.setAttribute('readonly', 'readonly');
                    textBox.classList.remove('editable');
                }
            });

            button.textContent = isEditMode ? 'Save' : 'Edit';
            document.getElementById('<%= btnSave.ClientID %>').style.display = isEditMode ? 'block' : 'none';
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="user-details-form">
            <h2>User Details</h2>
            <asp:Literal ID="LtlMsg" runat="server" />
            <asp:LinkButton ID="lnkToggleEdit" runat="server" OnClick="lnkToggleEdit_Click" CssClass="edit-button"><i class="fas fa-edit"></i> Edit</asp:LinkButton>
            <div class="form-group">
                <label for="txtFirstName">First Name</label>
                <asp:TextBox ID="txtFirstName" ClientIDMode="Static" runat="server" CssClass="form-control" ReadOnly="true" />
            </div>
            <div class="form-group">
                <label for="txtLastName">Last Name</label>
                <asp:TextBox ID="txtLastName" ClientIDMode="Static" runat="server" CssClass="form-control" ReadOnly="true" />
            </div>
            <div class="form-group">
                <label for="txtEmail">Email</label>
                <asp:TextBox ID="txtEmail" ClientIDMode="Static" runat="server" CssClass="form-control" ReadOnly="true" />
            </div>
            <div class="form-group">
                <label for="txtPhoneNumber">Phone Number</label>
                <asp:TextBox ID="txtPhoneNumber" ClientIDMode="Static" runat="server" CssClass="form-control" ReadOnly="true" />
            </div>
            <div class="form-group">
                <label for="txtStreet">Street</label>
                <asp:TextBox ID="txtStreet" ClientIDMode="Static" runat="server" CssClass="form-control" ReadOnly="true" />
            </div>
            <div class="form-group">
                <label for="txtCity">City</label>
                <asp:TextBox ID="txtCity" ClientIDMode="Static" runat="server" CssClass="form-control" ReadOnly="true" />
            </div>
            <div class="form-group">
                <label for="txtState">State</label>
                <asp:TextBox ID="txtState" ClientIDMode="Static" runat="server" CssClass="form-control" ReadOnly="true" />
            </div>
            <div class="form-group">
                <label for="txtZipCode">Zip Code</label>
                <asp:TextBox ID="txtZipCode" ClientIDMode="Static" runat="server" CssClass="form-control" ReadOnly="true" />
            </div>
            <div class="form-group">
                <label for="txtCountry">Country</label>
                <asp:TextBox ID="txtCountry" ClientIDMode="Static" runat="server" CssClass="form-control" ReadOnly="true" />
            </div>
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn save-button" OnClick="btnSave_Click" />
        </div>

        <div class="orders">
            <h3>Orders</h3>
            <asp:Repeater ID="rptOrders" runat="server">
                <HeaderTemplate>
                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <th>Order ID</th>
                                <th>Order Date</th>
                                <th>Total Amount</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("OrderId") %></td>
                        <td><%# Eval("OrderDate", "{0:yyyy-MM-dd}") %></td>
                        <td><%# Eval("TotalAmount", "{0:C}") %></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                        </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>

        <div class="contacts">
            <h3>Contact Messages</h3>
            <asp:Repeater ID="rptContacts" runat="server">
                <HeaderTemplate>
                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <th>Contact ID</th>
                                <th>Name</th>
                                <th>Email</th>
                                <th>Message</th>
                                <th>Date</th>
                                <th>Replied</th> <!-- New Column -->
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("ContactId") %></td>
                        <td><%# Eval("Name") %></td>
                        <td><%# Eval("Email") %></td>
                        <td><%# Eval("Message") %></td>
                        <td><%# Eval("Date", "{0:yyyy-MM-dd}") %></td>
                        <td><%# Convert.ToBoolean(Eval("IsReplied")) ? "Yes" : "No" %></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                        </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
