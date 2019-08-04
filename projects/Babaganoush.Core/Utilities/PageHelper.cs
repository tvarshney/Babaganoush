using System;
using System.Collections;
using System.Web.UI;

namespace Babaganoush.Core.Utilities
{
    /// <summary>
    /// A page helper.
    /// </summary>
    public static class PageHelper
    {
        /// <summary>
        /// Searches recursively in this control to find a control with the name specified.
        /// </summary>
        ///
        /// <param name="root">The Control in which to begin searching.</param>
        /// <param name="id">The ID of the control to be found.</param>
        /// <param name="recurse">if set to <c>true</c> [recurse].</param>
        ///
        /// <returns>
        /// The control if it is found or null if it is not.
        /// </returns>
        public static Control FindControl(this Control root, string id, bool recurse)
        {
            if (!recurse)
                return root.FindControl(id);

            Control controlFound;

            if (root != null)
            {
                controlFound = root.FindControl(id);

                if (controlFound != null)
                    return controlFound;

                foreach (Control c in root.Controls)
                {
                    controlFound = c.FindControl(id, true);

                    if (controlFound != null)
                        return controlFound;
                }
            }

            return null;
        }

        /// <summary>
        /// Similar to Control.FindControl, but recurses through child controls.
        /// </summary>
        ///
        /// <tparam name="T">
        /// Generic type parameter.
        /// </tparam>
        /// <param name="startingControl">The starting control.</param>
        /// <param name="id">The identifier.</param>
        ///
        /// <returns>
        /// The found control.
        /// </returns>
        ///
        /// ### <typeparam name="T">.</typeparam>
        public static T FindControl<T>(this Control startingControl, string id) where T : Control
        {
            T found = startingControl.FindControl(id) as T;

            if (found == null)
                found = FindChildControl<T>(startingControl, id);

            return found;
        }

        /// <summary>
        /// Similar to Control.FindControl, but recurses through child controls. Assumes that
        /// startingControl is NOT the control you are searching for.
        /// </summary>
        ///
        /// <tparam name="T">
        /// Generic type parameter.
        /// </tparam>
        /// <param name="startingControl">The starting control.</param>
        /// <param name="id">The identifier.</param>
        ///
        /// <returns>
        /// The found child control.
        /// </returns>
        ///
        /// ### <typeparam name="T">.</typeparam>
        private static T FindChildControl<T>(this Control startingControl, string id) where T : Control
        {
            T found = null;

            foreach (Control activeControl in startingControl.Controls)
            {
                found = activeControl as T;

                if (found == null || (string.Compare(id, found.ID, true) != 0))
                    found = FindChildControl<T>(activeControl, id);

                if (found != null)
                    break;
            }

            return found;
        }

        /// <summary>
        /// Returns a list of controls of a certain type, recursively.
        /// </summary>
        ///
        /// <param name="parent">The parent.</param>
        /// <param name="type">The type.</param>
        ///
        /// <returns>
        /// The found controls.
        /// </returns>
        public static ArrayList FindControls(this Control parent, Type type)
        {
            ArrayList list = new ArrayList();

            foreach (Control c in parent.Controls)
            {
                if (c.GetType() == type)
                    list.Add(c);

                if (c.HasControls())
                    list.AddRange(FindControls(c, type));
            }

            return list;
        }
    }
}
