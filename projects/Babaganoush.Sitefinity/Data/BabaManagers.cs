// file:	Data\BabaManagers.cs
//
// summary:	Implements the baba managers class
using Babaganoush.Sitefinity.Content.Managers;

namespace Babaganoush.Sitefinity.Data
{
    /// <summary>
    /// A baba managers.
    /// </summary>
    public static class BabaManagers
    {
        /// <summary>
        /// Gets the accounts.
        /// </summary>
        /// <value>
        /// The accounts.
        /// </value>
        public static AccountsManager Accounts { get { return AccountsManager.Instance; } }

        /// <summary>
        /// Gets the blogs.
        /// </summary>
        /// <value>
        /// The blogs.
        /// </value>
        public static BlogsManager Blogs { get { return BlogsManager.Instance; } }

        /// <summary>
        /// Gets the contents.
        /// </summary>
        /// <value>
        /// The contents.
        /// </value>
        public static ContentsManager Contents { get { return ContentsManager.Instance; } }

        /// <summary>
        /// Gets the departments.
        /// </summary>
        /// <value>
        /// The departments.
        /// </value>
        public static DepartmentsManager Departments { get { return DepartmentsManager.Instance; } }

        /// <summary>
        /// Gets the documents.
        /// </summary>
        /// <value>
        /// The documents.
        /// </value>
        public static DocumentsManager Documents { get { return DocumentsManager.Instance; } }

        /// <summary>
        /// Gets the events.
        /// </summary>
        /// <value>
        /// The events.
        /// </value>
        public static EventsManager Events { get { return EventsManager.Instance; } }

        /// <summary>
        /// Gets the forms.
        /// </summary>
        /// <value>
        /// The forms.
        /// </value>
        public static FormsManager Forms { get { return FormsManager.Instance; } }

        /// <summary>
        /// Gets the images.
        /// </summary>
        /// <value>
        /// The images.
        /// </value>
        public static ImagesManager Images { get { return ImagesManager.Instance; } }

        /// <summary>
        /// Gets the lists.
        /// </summary>
        /// <value>
        /// The lists.
        /// </value>
        public static ListsManager Lists { get { return ListsManager.Instance; } }

        /// <summary>
        /// Gets the news.
        /// </summary>
        /// <value>
        /// The news.
        /// </value>
        public static NewsManager News { get { return NewsManager.Instance; } }

        /// <summary>
        /// Gets the pages.
        /// </summary>
        /// <value>
        /// The pages.
        /// </value>
        public static PagesManager Pages { get { return PagesManager.Instance; } }

        /// <summary>
        /// Gets the blog posts.
        /// </summary>
        /// <value>
        /// The blog posts.
        /// </value>
        public static BlogPostsManager BlogPosts { get { return BlogPostsManager.Instance; } }

        /// <summary>
        /// Gets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        public static ProductsManager Products { get { return ProductsManager.Instance; } }

        /// <summary>
        /// Gets the taxonomies.
        /// </summary>
        /// <value>
        /// The taxonomies.
        /// </value>
        public static TaxonomiesManager Taxonomies { get { return TaxonomiesManager.Instance; } }

        /// <summary>
        /// Gets the videos.
        /// </summary>
        /// <value>
        /// The videos.
        /// </value>
        public static VideosManager Videos { get { return VideosManager.Instance; } }
    }
}
