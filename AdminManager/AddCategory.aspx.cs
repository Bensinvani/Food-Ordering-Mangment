using BLL;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Food_Ordering_Mangment.AdminManager
{
    public partial class AddCategory : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the user is an admin
            if (Session["Login"] == null || !((User)Session["Login"]).IsAdmin)
            {
                Response.Redirect("/Home.aspx");
                return;
            }

            if (!IsPostBack)
            {
                string catid = Request["CategoryId"] + "";
                string action = Request["action"] + "";

                if (catid == "")
                {
                    catid = "-1";
                }

                HidCatId.Value = catid;

                if (action == "delete" && catid != "-1")
                {
                    Category.Delete(int.Parse(catid));
                    HidCatId.Value = "-1";
                    BindCategoriesGrid();
                    return;
                }

                if (catid != "-1")
                {
                    Category Tmp = Category.GetById(int.Parse(catid));
                    if (Tmp != null)
                    {
                        txtCategoryName.Text = Tmp.CategoryName;
                        txtDescription.Text = Tmp.Description;
                        TxtImage.Text = Tmp.CategoryImage;
                    }
                    else
                    {
                        HidCatId.Value = "-1";
                    }
                }

                BindCategoriesGrid();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string categoryName = txtCategoryName.Text.Trim();
            string description = txtDescription.Text.Trim();
            string categoryImage = TxtImage.Text.Trim();

            if (string.IsNullOrEmpty(categoryName))
            {
                lblMessage.Text = "Category Name is required.";
                return;
            }

            Category category = new Category
            {
                CategoryId = int.Parse(HidCatId.Value),
                CategoryName = categoryName,
                Description = description,
                CategoryImage = categoryImage,
            };

            category.Save();

            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = "Category added successfully!";
            ClearFields();
            BindCategoriesGrid();
        }

        private void ClearFields()
        {
            txtCategoryName.Text = string.Empty;
            txtDescription.Text = string.Empty;
            TxtImage.Text = string.Empty;
        }

        private void BindCategoriesGrid()
        {
            gvCategories.DataSource = Category.GetAll();
            gvCategories.DataBind();
        }
    }
}
