// file:	Api\EventsController.cs
//
// summary:	Implements the events controller class
using Babaganoush.Sitefinity.Data;
using Babaganoush.Sitefinity.Models;
using Babaganoush.Sitefinity.WebApi.Api.Abstracts;
using Babaganoush.Sitefinity.WebApi.Models;
using System;
using System.Net.Http;
using Telerik.Sitefinity.Events.Model;

namespace Babaganoush.Sitefinity.WebApi.Api
{
    /// <summary>
    /// REST service for events.
    /// </summary>
    public class EventsController : BaseChildController<EventModel, Event>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventsController" /> class.
        /// </summary>
        public EventsController()
            : base(BabaManagers.Events)
        {

        }

        /// <summary>
        /// Gets all past events.
        /// </summary>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <returns>
        /// The past.
        /// </returns>
        public virtual HttpResponseMessage GetPast(int take = 0, int skip = 0)
        {
            return new DataResponse(BabaManagers.Events.GetPast(take: take, skip: skip));
        }

        /// <summary>
        /// Gets all upcoming events.
        /// </summary>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <returns>
        /// The upcoming.
        /// </returns>
        public virtual HttpResponseMessage GetUpcoming(int take = 0, int skip = 0)
        {
            return new DataResponse(BabaManagers.Events.GetUpcoming(take: take, skip: skip));
        }

        /// <summary>
        /// Gets the events within the date range.
        /// </summary>
        /// <param name="start">The start date.</param>
        /// <param name="end">(Optional) The end date.</param>
        /// <param name="take">(Optional) the take.</param>
        /// <param name="skip">(Optional) the skip.</param>
        /// <returns>
        /// The calculated range.
        /// </returns>
        public virtual HttpResponseMessage GetRange(DateTime start, DateTime? end = null, int take = 0, int skip = 0)
        {
            return new DataResponse(BabaManagers.Events.GetRange(start, end, take: take, skip: skip));
        }
    }
}