using Babaganoush.Sitefinity.Content.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SitefinityWebApp.Tests.TestPages
{
	public partial class PageManagerGetAll : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			var mgr = PagesManager.Instance;
			var pages = mgr.GetAll();
			foreach (var page in pages.Items)
			{
				Response.Write(page.Title + "<br />");
			}
		}
	}
}