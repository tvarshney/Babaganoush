using System.Linq;
using Babaganoush.Core.Wrappers;
using Babaganoush.Sitefinity.Utilities;
using System;
using System.Web;

namespace Babaganoush.Sitefinity.Application
{
    /// <summary>
    /// Abstract base class for startup classes.
    /// </summary>
    /// <typeparam name="T">Startup type</typeparam>
    public abstract class BaseStartup<T> : Core.Application.BaseStartup<T> 
        where T : BaseStartup<T>
    {
        /// <summary>
        /// Inits the specified application. Triggered due to DynamicModuleUtility in PreInit.
        /// </summary>
        /// <param name="context">The context.</param>
        public override void OnInit(HttpApplication context)
        {
            base.OnInit(context);
            
            //FIND PAGE INSTANCE
            context.PreRequestHandlerExecute += (sender, e) =>
            {
				var rawUrl = HttpContext.Current.Request.RawUrl.ToUpper();

				// skip sitefinity admin pages
                if (rawUrl.Trim('~', '/').StartsWith("SITEFINITY/")) return;

				// skip web resources
				if (rawUrl.Contains("RESOURCE.AXD?")) return;

                //SUBSCRIBE TO PAGE EVENTS (ONLY IF WEB FORM)
                if (!PageHelper.IsMvcPage())
                {
                    var page = new SystemHttpContextWrapper().GetCurrentHandler();
                    if (page != null)
                    {
                        page.PreInit += OnPagePreInit;
                        page.Init += OnPageInit;
                        page.PreLoad += OnPagePreLoad;
                        page.Load += OnPageLoad;
                        page.PreRender += OnPagePreRender;
                        page.Error += OnPageError;
                    }
                }
                else
                {
                    //TODO: REGISTER SCRIPTS AND STYLES ON MVC PAGE
                    //AUTOMATICALLY ADD GLOBAL JAVASCRIPT
                    /*MvcHelper.RegisterScripts(
                        Config.Get<BabaganoushConfig>().IncludeJquery,
                        Config.Get<BabaganoushConfig>().IncludeRequireJS);*/
                }
            };
        }

        /// <summary>
        /// Called when [page pre initialize].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected virtual void OnPagePreInit(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Called when [page initialize].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected virtual void OnPageInit(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Called when [page pre load].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected virtual void OnPagePreLoad(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Called when [page load].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected virtual void OnPageLoad(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the PreRender event of the Page.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected virtual void OnPagePreRender(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Called when [page error].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected virtual void OnPageError(object sender, EventArgs e)
        {

        }
    }
}
