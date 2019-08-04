// file:	Content\Managers\EventsManager.cs
//
// summary:	Implements the events manager class
using Babaganoush.Sitefinity.Content.Managers.Abstracts;
using Babaganoush.Sitefinity.Content.Managers.Interfaces;
using Babaganoush.Sitefinity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Events.Model;
using Telerik.Sitefinity.GenericContent.Model;

namespace Babaganoush.Sitefinity.Content.Managers
{
    /// <summary>
    /// Manager for events.
    /// </summary>
    public class EventsManager : BaseContentManager<
        Telerik.Sitefinity.Modules.Events.EventsManager,
        Event,
        EventsManager,
        EventModel>, IChildManager<EventModel, Event>
    {
        /// <summary>
        /// Gets the Sitefinity data.
        /// </summary>
        /// <param name="providerName">(Optional) the provider name to get.</param>
        /// <returns>
        /// An IQueryable&lt;Event&gt;
        /// </returns>
        protected override IQueryable<Event> Get(string providerName = null)
        {
            return GetManager(providerName).GetEvents();
        }

        /// <summary>
        /// Gets the Sitefinity data by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <returns>
        /// An Event.
        /// </returns>
        protected override Event Get(Guid id, string providerName = null)
        {
            return GetManager(providerName).GetEvent(id);
        }

        /// <summary>
        /// Creates the Baba instance from the Sitefinity object.
        /// </summary>
        /// <param name="sfContent">Content of the sf.</param>
        /// <returns>
        /// The new instance.
        /// </returns>
        protected override EventModel CreateInstance(Event sfContent)
        {
            return new EventModel(sfContent);
        }

        /// <summary>
        /// Gets all past events.
        /// </summary>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <param name="convert">(Optional) the convert function from Sitefinity to Baba model, usually if
        /// you want to override the default constructor.</param>
        /// <returns>
        /// The past.
        /// </returns>
        public virtual IEnumerable<EventModel> GetPast(
            string providerName = null, 
            Expression<Func<Event, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<Event, EventModel>> convert = null)
        {
            var sfItems = Get(providerName)
                .Where(i => i.EventStart < DateTime.UtcNow
                    && i.Status == ContentLifecycleStatus.Live
                    && i.Visible);

            //ADD OPTIONAL FILTERS IF APPLICABLE
            if (filter != null)
                sfItems = sfItems.Where(filter);

            //HANDLE SORTING
            sfItems = sfItems
                .OrderByDescending(i => i.EventStart);

            //HANDLE PAGING IF APPLICABLE
            if (skip > 0) sfItems = sfItems.Skip(skip);
            if (take > 0) sfItems = sfItems.Take(take);

            return sfItems.Select(convert != null ? convert : i => CreateInstance(i));
        }

        /// <summary>
        /// Gets all upcoming events.
        /// </summary>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <param name="convert">(Optional) the convert function from Sitefinity to Baba model, usually if
        /// you want to override the default constructor.</param>
        /// <returns>
        /// The upcoming.
        /// </returns>
        public virtual IEnumerable<EventModel> GetUpcoming(
            string providerName = null, 
            Expression<Func<Event, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<Event, EventModel>> convert = null)
        {
            var sfItems = Get(providerName)
                .Where(i => i.EventStart >= DateTime.UtcNow
                    && i.Status == ContentLifecycleStatus.Live
                    && i.Visible);

            //ADD OPTIONAL FILTERS IF APPLICABLE
            if (filter != null)
                sfItems = sfItems.Where(filter);

            //HANDLE SORTING
            sfItems = sfItems
                .OrderBy(i => i.EventStart);

            //HANDLE PAGING IF APPLICABLE
            if (skip > 0) sfItems = sfItems.Skip(skip);
            if (take > 0) sfItems = sfItems.Take(take);

            return sfItems.Select(convert != null ? convert : i => CreateInstance(i));
        }

        /// <summary>
        /// Gets the events within the date range.
        /// </summary>
        /// <param name="start">The start date.</param>
        /// <param name="end">(Optional) The end date.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <param name="convert">(Optional) the convert function from Sitefinity to Baba model, usually if
        /// you want to override the default constructor.</param>
        /// <returns>
        /// The calculated range.
        /// </returns>
        public virtual IEnumerable<EventModel> GetRange(DateTime start, DateTime? end = null, 
            string providerName = null, 
            Expression<Func<Event, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<Event, EventModel>> convert = null)
        {
            var sfItems = Get(providerName)
                .Where(i => i.EventStart.Date >= start.Date
                    && i.Status == ContentLifecycleStatus.Live
                    && i.Visible);

            //HANDLE END RANGE IF APPLICABLE
            if (end.HasValue)
                sfItems = sfItems.Where(i => i.EventStart.Date <= end.Value.Date);

            //ADD OPTIONAL FILTERS IF APPLICABLE
            if (filter != null)
                sfItems = sfItems.Where(filter);

            //HANDLE SORTING
            sfItems = sfItems
                .OrderBy(i => i.EventStart);

            //HANDLE PAGING IF APPLICABLE
            if (skip > 0) sfItems = sfItems.Skip(skip);
            if (take > 0) sfItems = sfItems.Take(take);

            return sfItems.Select(convert != null ? convert : i => CreateInstance(i));
        }

        /// <summary>
        /// Searches events by title and content.
        /// </summary>
        /// <param name="value">The search string.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <param name="convert">(Optional) the convert.</param>
        /// <returns>
        /// An IQueryable&lt;EventModel&gt;
        /// </returns>
        public override IEnumerable<EventModel> Search(string value, 
            string providerName = null, 
            Expression<Func<Event, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<Event, EventModel>> convert = null)
        {
            var sfItems = Get(providerName)
                .Where(i => (i.Title.ToString().ToLower().Contains(value.ToLower())
                    || i.Content.ToString().ToLower().Contains(value.ToLower()))
                    && i.Status == ContentLifecycleStatus.Live
                    && i.Visible);

            //ADD OPTIONAL FILTERS IF APPLICABLE
            if (filter != null)
                sfItems = sfItems.Where(filter);

            //HANDLE SORTING
            sfItems = sfItems
                .OrderBy(i => i.EventStart);

            //HANDLE PAGING IF APPLICABLE
            if (skip > 0) sfItems = sfItems.Skip(skip);
            if (take > 0) sfItems = sfItems.Take(take);

            return sfItems.Select(convert != null ? convert : i => CreateInstance(i));
        }

        /// <summary>
        /// Gets items by parent's UrlName.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <param name="convert">(Optional) the convert.</param>
        /// <returns>
        /// The by parent.
        /// </returns>
        public virtual IEnumerable<EventModel> GetByParent(string value, 
            string providerName = null, 
            Expression<Func<Event, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<Event, EventModel>> convert = null)
        {
            var sfItems = Get(providerName)
                .Where(s => s.Parent.UrlName.Equals(value, StringComparison.OrdinalIgnoreCase)
                    && s.Status == ContentLifecycleStatus.Live
                    && s.Visible);

            //ADD OPTIONAL FILTERS IF APPLICABLE
            if (filter != null)
                sfItems = sfItems.Where(filter);

            //HANDLE SORTING
            sfItems = sfItems
                .OrderBy(i => i.EventStart);

            //HANDLE PAGING IF APPLICABLE
            if (skip > 0) sfItems = sfItems.Skip(skip);
            if (take > 0) sfItems = sfItems.Take(take);

            return sfItems.Select(convert != null ? convert : i => CreateInstance(i));
        }

        /// <summary>
        /// Gets items based off parent's ID.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <param name="convert">(Optional) the convert.</param>
        /// <returns>
        /// The by parent identifier.
        /// </returns>
        public virtual IEnumerable<EventModel> GetByParentId(Guid id, 
            string providerName = null, 
            Expression<Func<Event, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<Event, EventModel>> convert = null)
        {
            var sfItems = Get(providerName)
                .Where(s => (s.Parent.Id == id)
                    && s.Status == ContentLifecycleStatus.Live
                    && s.Visible);

            //ADD OPTIONAL FILTERS IF APPLICABLE
            if (filter != null)
                sfItems = sfItems.Where(filter);

            //HANDLE SORTING
            sfItems = sfItems
                .OrderBy(i => i.EventStart);

            //HANDLE PAGING IF APPLICABLE
            if (skip > 0) sfItems = sfItems.Skip(skip);
            if (take > 0) sfItems = sfItems.Take(take);

            return sfItems.Select(convert != null ? convert : i => CreateInstance(i));
        }

        /// <summary>
        /// Gets the items by parent title.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <param name="filter">(Optional) specifies the filter.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <param name="convert">(Optional) the convert.</param>
        /// <returns>
        /// The by parent title.
        /// </returns>
        public virtual IEnumerable<EventModel> GetByParentTitle(string value, 
            string providerName = null, 
            Expression<Func<Event, bool>> filter = null, 
            int take = 0,
            int skip = 0,
            Expression<Func<Event, EventModel>> convert = null)
        {
            var sfItems = Get(providerName)
                .Where(s => s.Parent.Title.Equals(value, StringComparison.OrdinalIgnoreCase)
                    && s.Status == ContentLifecycleStatus.Live
                    && s.Visible);

            //ADD OPTIONAL FILTERS IF APPLICABLE
            if (filter != null)
                sfItems = sfItems.Where(filter);

            //HANDLE SORTING
            sfItems = sfItems
                .OrderBy(i => i.EventStart);

            //HANDLE PAGING IF APPLICABLE
            if (skip > 0) sfItems = sfItems.Skip(skip);
            if (take > 0) sfItems = sfItems.Take(take);

            return sfItems.Select(convert != null ? convert : i => CreateInstance(i));
        }
    }
}