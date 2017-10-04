using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Profile;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyBlog.Forms
{
    public partial class RegistrationForm : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void RegistrationBtnClick(object sender, EventArgs e)
        {
            Membership.CreateUser(login.Text, password.Text, e_mail.Text);
            login.Visible = false;
            password.Visible = false;
            e_mail.Visible = false;
            registrationBtn.Visible = false;
            regCompleteText.Text = "Регистрация завершена. Теперь вы можете зайти под своим логином и настроить параметры профайла";
            
        }
    }
}