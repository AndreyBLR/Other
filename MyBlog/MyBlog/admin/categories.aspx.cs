using System;
using System.Collections.Generic;
using Classes;
using MBPresentation;
using MBPresentation.Interfaces.ViewInterfaces.Page;
using MBPresentation.Presenters.Page;
using MBPresentation.ServiceReference;

namespace MyBlog.admin
{
    public partial class Categories : System.Web.UI.Page, ICategoriesPage
    {
        private CategoriesPagePresenter _presenter;

        public event VoidEventHandler CategoriesReading;
        public event VoidEventHandler CategoryCreating;
        public event VoidEventHandler CategoryDeleting;
        public event VoidEventHandler CategoryReading;
        public event VoidEventHandler CategoryUpdating;

        public Guid CategoryId { get; set; }
        public List<Category> CategoryList { get; set; }
        public Category Category { get; set; }

        public Categories()
        {
            _presenter = new CategoriesPagePresenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void AddBtnClick(object sender, EventArgs e)
        {
            try
            {
                var category = new Category
                {
                    CategoryId = Guid.NewGuid(),
                    CategoryName = txbName.Text,
                    CategoryDesc = txbDescription.Text
                };
                if (rbtn1.Checked)
                {
                    category.ContentType = "post";
                }
                else if (rbtn2.Checked)
                {
                    category.ContentType = "image";
                }
                CreateCategory(category);
                Response.Redirect("~/admin/categories.aspx");
            }
            catch (Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error(User.Identity.Name + "  " + ex.Message);
                Response.Redirect("~/admin/categories.aspx");
            }
            
        }
        protected void SaveBtnClick(object sender, EventArgs e)
        {
            try
            {
                var category = (Category)Cache["Category"];
                category.CategoryName = txbName.Text;
                category.CategoryDesc = txbDescription.Text;
                if (rbtn1.Checked)
                {
                    category.ContentType = "post";
                }
                else if (rbtn2.Checked)
                {
                    category.ContentType = "image";
                }
                UpdateCategory(category);
                Response.Redirect("~/admin/categories.aspx");
            }
            catch(Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error(User.Identity.Name +"  " + ex.Message);
                Response.Redirect("~/admin/categories.aspx");
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            try
            {
                if (this.IsSet("action"))
                {
                    Guid categoryId;
                    switch (Request.Params["action"])
                    {
                        case "delete":
                                if(this.IsSet("categoryId") && Guid.TryParse(Request.Params["categoryId"], out categoryId))
                                {
                                    DeleteCategory(categoryId);
                                }
                                Response.Redirect("~/admin/categories.aspx");
                            break;
                        case "change":
                                if(this.IsSet("categoryId") && Guid.TryParse(Request.Params["categoryId"], out categoryId))
                                {
                                    ReadCategory(categoryId);
                                    Cache.Insert("Category", Category);
                                    txbName.Text = Category.CategoryName;
                                    txbDescription.Text = Category.CategoryDesc;
                                    multiView.SetActiveView(formView);
                                    SaveBtn.Visible = true;
                                    AddBtn.Visible = false;
                                }
                                else
                                {
                                    Response.Redirect("~/admin/categories.aspx");
                                }
                            break;
                        case "add":
                                multiView.SetActiveView(formView);
                                SaveBtn.Visible = false;
                                AddBtn.Visible = true;
                            break;
                        default:
                                Response.Redirect("~/admin/categories.aspx");
                            break;
                    }
                }
                else
                {
                    multiView.SetActiveView(categoriesView);
                    ReadCategories();
                    lstCategories.DataSource = CategoryList;
                    lstCategories.DataBind();
                }
            }
            catch(Exception ex)
            {
                Logger.InitLogger();
                Logger.Log.Error(User.Identity.Name +"  " + ex.Message);
                Response.Redirect("~/admin/categories.aspx");
            }
        }

        private void ReadCategory(Guid categoryId)
        {
            CategoryId = categoryId;
            if (CategoryReading != null)
            {
                CategoryReading();
            }
        }
        private void ReadCategories()
        {
            if (CategoriesReading != null)
            {
                CategoriesReading();
            }
        }
        private void CreateCategory(Category category)
        {
            Category = category;
            if(CategoryCreating != null)
            {
                CategoryCreating();
            }
        }
        private void DeleteCategory(Guid categoryId)
        {
            CategoryId = categoryId;
            if(CategoryDeleting != null)
            {
                CategoryDeleting();
            }
        }
        private void UpdateCategory(Category category)
        {
            Category = category;
            if(CategoryUpdating != null)
            {
                CategoryUpdating();
            }
        }
    }
}