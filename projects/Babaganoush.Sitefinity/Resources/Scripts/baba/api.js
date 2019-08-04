/**
 * This is an API wrapper for Sitefinity
 * Use with NuGet package: Falafel.Sitefinity.WebApi
 */
define([
	'jquery',
	'baba/helpers'
], function ($, BabaHelpers) {
	
    return {
	    //START PAGES API
        getPages: function () {
            return $.getJSON(BabaHelpers.toServicesUrl('/pages'));
        },
	
        getPagesById: function (id) {
            return $.getJSON(BabaHelpers.toServicesUrl('/pages/' + id));
        },
	
        getPagesByUrl: function (value) {
            return $.getJSON(BabaHelpers.toServicesUrl('/pages/getbyurl?value=' + value));
        },

	    //START CONTENT API
        getContentsById: function (id) {
            return $.getJSON(BabaHelpers.toServicesUrl('/contents/' + id));
        },
	
        getContentsByName: function (value) {
            return $.getJSON(BabaHelpers.toServicesUrl('/contents/getbyname?value=' + value));
        },
	
        getContentsByTitle: function (value) {
            return $.getJSON(BabaHelpers.toServicesUrl('/contents/getbytitle?value=' + value));
        },
	
	    //START NEWS API
        getNews: function () {
            return $.getJSON(BabaHelpers.toServicesUrl('/news'));
        },
	
        getNewsById: function (id) {
            return $.getJSON(BabaHelpers.toServicesUrl('/news/' + id));
        },
	
        getNewsByName: function (value) {
            return $.getJSON(BabaHelpers.toServicesUrl('/news/getbyname?value=' + value));
        },
	
        getNewsByTitle: function (value) {
            return $.getJSON(BabaHelpers.toServicesUrl('/news/getbytitle?value=' + value));
        },
	
        getNewsByCategory: function (value) {
            return $.getJSON(BabaHelpers.toServicesUrl('/news/getbycategory?value=' + value));
        },
	
        getNewsByTag: function (value) {
            return $.getJSON(BabaHelpers.toServicesUrl('/news/getbytag?value=' + value));
        },
	
        getNewsByCategoryId: function (id) {
            return $.getJSON(BabaHelpers.toServicesUrl('/news/getbycategoryid/' + id));
        },
	
        getNewsByTagId: function (id) {
            return $.getJSON(BabaHelpers.toServicesUrl('/news/getbytagid/' + id));
        },
	
        getNewsBySearch: function (value) {
            return $.getJSON(BabaHelpers.toServicesUrl('/news/search?value=' + value));
        },
	
        getNewsByRecent: function () {
            return $.getJSON(BabaHelpers.toServicesUrl('/news/getrecent'));
        },
	
	    //START EVENTS API
        getEvents: function () {
            return $.getJSON(BabaHelpers.toServicesUrl('/events'));
        },
	
        getEventsById: function (id) {
            return $.getJSON(BabaHelpers.toServicesUrl('/events/' + id));
        },
	
        getEventsByName: function (value) {
            return $.getJSON(BabaHelpers.toServicesUrl('/events/getbyname?value=' + value));
        },
	
        getEventsByTitle: function (value) {
            return $.getJSON(BabaHelpers.toServicesUrl('/events/getbytitle?value=' + value));
        },
	
        getEventsByPast: function () {
            return $.getJSON(BabaHelpers.toServicesUrl('/events/getpast'));
        },
	
        getEventsByUpcoming: function () {
            return $.getJSON(BabaHelpers.toServicesUrl('/events/getupcoming'));
        },
	
        getEventsByRange: function (start, end) {
            return $.getJSON(BabaHelpers.toServicesUrl(
			    '/events/getbyrange?start=' + start + '&end=' + end));
        },
	
        getEventsByCategory: function (value) {
            return $.getJSON(BabaHelpers.toServicesUrl('/events/getbycategory?value=' + value));
        },
	
        getEventsByTag: function (value) {
            return $.getJSON(BabaHelpers.toServicesUrl('/events/getbytag?value=' + value));
        },
	
        getEventsByCategoryId: function (id) {
            return $.getJSON(BabaHelpers.toServicesUrl('/events/getbycategoryid/' + id));
        },
	
        getEventsByTagId: function (id) {
            return $.getJSON(BabaHelpers.toServicesUrl('/events/getbytagid/' + id));
        },
	
        getEventsBySearch: function (value) {
            return $.getJSON(BabaHelpers.toServicesUrl('/events/search?value=' + value));
        },
	
        getEventsByRecent: function () {
            return $.getJSON(BabaHelpers.toServicesUrl('/events/getrecent'));
        },
	
	    //START PRODUCTS API
        getProducts: function () {
            return $.getJSON(BabaHelpers.toServicesUrl('/products'));
        },
	
        getProductsById: function (id) {
            return $.getJSON(BabaHelpers.toServicesUrl('/products/' + id));
        },
	
        getProductsBySku: function (value) {
            return $.getJSON(BabaHelpers.toServicesUrl('/products/getbysku?value=' + value));
        },
	
        getProductsByName: function (value) {
            return $.getJSON(BabaHelpers.toServicesUrl('/products/getbyname?value=' + value));
        },
	
        getProductsByTitle: function (value) {
            return $.getJSON(BabaHelpers.toServicesUrl('/products/getbytitle?value=' + value));
        },
	
        getProductsByCategory: function (value) {
            return $.getJSON(BabaHelpers.toServicesUrl('/products/getbycategory?value=' + value));
        },
	
        getProductsByTag: function (value) {
            return $.getJSON(BabaHelpers.toServicesUrl('/products/getbytag?value=' + value));
        },
	
        getProductsByCategoryId: function (id) {
            return $.getJSON(BabaHelpers.toServicesUrl('/products/getbycategoryid/' + id));
        },
	
        getProductsByTagId: function (id) {
            return $.getJSON(BabaHelpers.toServicesUrl('/products/getbytagid/' + id));
        },
	
        getProductsBySearch: function (value) {
            return $.getJSON(BabaHelpers.toServicesUrl('/products/search?value=' + value));
        },
	
        getProductsByRecent: function (value) {
            return $.getJSON(BabaHelpers.toServicesUrl('/products/getrecent'));
        },
	
	    //START BLOGS API
        getBlogs: function () {
            return $.getJSON(BabaHelpers.toServicesUrl('/blogs'));
        },
	
        getBlogsById: function (id) {
            return $.getJSON(BabaHelpers.toServicesUrl('/blogs/' + id));
        },
	
        getBlogsByName: function (value) {
            return $.getJSON(BabaHelpers.toServicesUrl('/blogs/getbyname?value=' + value));
        },
	
        getBlogsByTitle: function (value) {
            return $.getJSON(BabaHelpers.toServicesUrl('/blogs/getbytitle?value=' + value));
        },
	
        getBlogsBySearch: function (value) {
            return $.getJSON(BabaHelpers.toServicesUrl('/blogs/search?value=' + value));
        },
	
	    //START BLOG POSTS API
        getBlogPosts: function () {
            return $.getJSON(BabaHelpers.toServicesUrl('/blogposts'));
        },
	
        getBlogPostsById: function (id) {
            return $.getJSON(BabaHelpers.toServicesUrl('/blogposts/' + id));
        },
	
        getBlogPostsByName: function (value) {
            return $.getJSON(BabaHelpers.toServicesUrl('/blogposts/getbyname?value=' + value));
        },
	
        getBlogPostsByTitle: function (value) {
            return $.getJSON(BabaHelpers.toServicesUrl('/blogposts/getbytitle?value=' + value));
        },
	
        getBlogPostsByCategory: function (value) {
            return $.getJSON(BabaHelpers.toServicesUrl('/blogposts/getbycategory?value=' + value));
        },
	
        getBlogPostsByTag: function (value) {
            return $.getJSON(BabaHelpers.toServicesUrl('/blogposts/getbytag?value=' + value));
        },
	
        getBlogPostsByCategoryId: function (id) {
            return $.getJSON(BabaHelpers.toServicesUrl('/blogposts/getbycategoryid/' + id));
        },
	
        getBlogPostsByTagId: function (id) {
            return $.getJSON(BabaHelpers.toServicesUrl('/blogposts/getbytagid/' + id));
        },
	
        getBlogPostsBySearch: function (value) {
            return $.getJSON(BabaHelpers.toServicesUrl('/blogposts/search?value=' + value));
        },
	
        getBlogPostsByParent: function (value) {
            return $.getJSON(BabaHelpers.toServicesUrl('/blogposts/getbyparent?value=' + value));
	    },

	    getBlogPostsByParentTitle: function (value) {
		    return $.getJSON(BabaHelpers.toServicesUrl('/blogposts/getbyparenttitle?value=' + value));
        },

	    getBlogPostsByParentId: function (id) {
		    return $.getJSON(BabaHelpers.toServicesUrl('/blogposts/getbyparentid/' + id));
	    },
	
        getBlogPostsByRecent: function () {
            return $.getJSON(BabaHelpers.toServicesUrl('/blogposts/getrecent'));
        },
	
	    //START IMAGES API
        getImagesById: function (id) {
            return $.getJSON(BabaHelpers.toServicesUrl('/images/' + id));
        },
	
        getImagesByName: function (value) {
            return $.getJSON(BabaHelpers.toServicesUrl('/images/getbyname?value=' + value));
        },
	
        getImagesByTitle: function (value) {
            return $.getJSON(BabaHelpers.toServicesUrl('/images/getbytitle?value=' + value));
        },
	
        getImagesByParent: function (value) {
            return $.getJSON(BabaHelpers.toServicesUrl('/images/getbyparent?value=' + value));
        },
	
        getImagesByParentId: function (id) {
            return $.getJSON(BabaHelpers.toServicesUrl('/images/getbyparentid/' + id));
        },
	
        getImagesByRecent: function () {
            return $.getJSON(BabaHelpers.toServicesUrl('/images/getrecent'));
        },
	
	    //START VIDEOS API
        getVideosById: function (id) {
            return $.getJSON(BabaHelpers.toServicesUrl('/videos/' + id));
        },
	
        getVideosByName: function (value) {
            return $.getJSON(BabaHelpers.toServicesUrl('/videos/getbyname?value=' + value));
        },
	
        getVideosByTitle: function (value) {
            return $.getJSON(BabaHelpers.toServicesUrl('/videos/getbytitle?value=' + value));
        },
	
        getVideosByParent: function (value) {
            return $.getJSON(BabaHelpers.toServicesUrl('/videos/getbyparent?value=' + value));
        },
	
        getVideosByParentId: function (id) {
            return $.getJSON(BabaHelpers.toServicesUrl('/videos/getbyparentid/' + id));
        },
	
        getVideosByRecent: function () {
            return $.getJSON(BabaHelpers.toServicesUrl('/videos/getrecent'));
        },
	
	    //START DOCS API
        getDocumentsById: function (id) {
            return $.getJSON(BabaHelpers.toServicesUrl('/documents/' + id));
        },
	
        getDocumentsByName: function (value) {
            return $.getJSON(BabaHelpers.toServicesUrl('/documents/getbyname?value=' + value));
        },
	
        getDocumentsByTitle: function (value) {
            return $.getJSON(BabaHelpers.toServicesUrl('/documents/getbytitle?value=' + value));
        },
	
        getDocumentsByParent: function (value) {
            return $.getJSON(BabaHelpers.toServicesUrl('/documents/getbyparent?value=' + value));
        },
	
        getDocumentsByParentId: function (id) {
            return $.getJSON(BabaHelpers.toServicesUrl('/documents/getbyparentid/' + id));
        },
	
        getDocumentsByRecent: function () {
            return $.getJSON(BabaHelpers.toServicesUrl('/documents/getrecent'));
        },
	
	    //START LISTS API
        getListsById: function (id) {
            return $.getJSON(BabaHelpers.toServicesUrl('/lists/' + id));
        },
	
        getListsByName: function (value) {
            return $.getJSON(BabaHelpers.toServicesUrl('/lists/getbyname?value=' + value));
        },
	
        getListsByTitle: function (value) {
            return $.getJSON(BabaHelpers.toServicesUrl('/lists/getbytitle?value=' + value));
        },
	
	    //START DEPARTMENTS API
        getDepartments: function () {
            return $.getJSON(BabaHelpers.toServicesUrl('/departments'));
        },
	
        getDepartmentsById: function (id) {
            return $.getJSON(BabaHelpers.toServicesUrl('/departments/' + id));
        },
	
        getDepartmentsByName: function (value) {
            return $.getJSON(BabaHelpers.toServicesUrl('/departments/getbyname?value=' + value));
        },
	
        getDepartmentsByTitle: function (value) {
            return $.getJSON(BabaHelpers.toServicesUrl('/departments/getbytitle?value=' + value));
        },
	
        getDepartmentsByParent: function (value) {
            return $.getJSON(BabaHelpers.toServicesUrl('/departments/getbyparent?value=' + value));
	    },

	    getDepartmentsByParentTitle: function (value) {
		    return $.getJSON(BabaHelpers.toServicesUrl('/departments/getbyparenttitle?value=' + value));
        },

	    getDepartmentsByParentId: function (id) {
		    return $.getJSON(BabaHelpers.toServicesUrl('/departments/getbyparentid/' + id));
	    }
    };
});
