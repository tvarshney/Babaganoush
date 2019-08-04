using System.Web.UI;
using System;
using Babaganoush.Sitefinity.Data;

namespace SitefinityWebApp.Custom
{
    public partial class Test1 : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BabaManagers.News.GetAll();
        }
    }
}