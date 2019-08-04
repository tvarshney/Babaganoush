// file:	Content\Managers\FormsManager.cs
//
// summary:	Implements the forms manager class
using Babaganoush.Sitefinity.Content.Managers.Abstracts;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Security.Claims;

namespace Babaganoush.Sitefinity.Content.Managers
{
    /// <summary>
    /// Manager for forms.
    /// </summary>
    public class FormsManager : BaseSingletonManager<Telerik.Sitefinity.Modules.Forms.FormsManager, FormsManager>
    {
        /// <summary>
        /// Imports the form entries.
        /// </summary>
        /// <param name="formName">Name of the form.</param>
        /// <param name="docName">Name of the document.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <returns>
        /// true if it succeeds, false if it fails.
        /// </returns>
        public bool ImportFormEntries(string formName, string docName, string providerName = null)
        {
            //GET DOCUMENT FROM LIBRARY
            var manager = LibrariesManager.GetManager(providerName);
            var doc = manager.GetDocuments().FirstOrDefault(d => d.UrlName == docName && d.Status == ContentLifecycleStatus.Live && d.Visible);

            //PROCEED IF APPLICABLE
            if (doc != null)
            {
                //STREAM FOR FAST FORWARD-ONLY READING
                return ImportFormEntries(formName, manager.Download(doc), providerName);
            }

            return false;
        }

        /// <summary>
        /// Imports the form entries.
        /// </summary>
        /// <param name="formName">Name of the form.</param>
        /// <param name="fileStream">The file stream.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        /// <returns>
        /// true if it succeeds, false if it fails.
        /// </returns>
        public bool ImportFormEntries(string formName, Stream fileStream, string providerName = null)
        {
            using (var csv = new CsvReader(new StreamReader(fileStream)))
            {
                //GET KEY/VALUE PAIR OF SPREADSHEET COLUMNS
                while (csv.Read())
                {
                    //PIVOT RECORD IN DICTIONARY FOR LATER USE
                    var inputs = csv.GetRecord<dynamic>() as IDictionary<string, object>;

                    //SAVE DICTIONARY VALUES TO FORM
                    CreateFormEntries(formName, inputs, providerName);
                }
            }

            return true;
        }

        /// <summary>
        /// Creates the form entries.
        /// </summary>
        /// <param name="formName">Name of the form.</param>
        /// <param name="inputs">The inputs.</param>
        /// <param name="providerName">(Optional) name of the provider.</param>
        public void CreateFormEntries(string formName, IDictionary<string, object> inputs, string providerName = null)
        {
            var formsManager = GetManager(providerName);
            using (new ElevatedModeRegion(formsManager))
            {
                //DECLARE VARIABLES
                var form = formsManager.GetFormByName(formName);
                var entry = formsManager.CreateFormEntry(form.EntriesTypeName);

                //ADD ALL INPUT VALUES
                foreach (var item in inputs.Where(item => entry.DoesFieldExist(item.Key)))
                {
                    entry.SetValue(item.Key, item.Value);
                }

                //SAVE USER RELATED INFO
                entry.UserId = ClaimsManager.GetCurrentUserId();
                entry.IpAddress = inputs.Keys.Contains("IpAddress") ? (string) inputs["IpAddress"] : HttpContext.Current.Request.UserHostAddress;

                //SAVE LANGUAGE FOR MULTI-LINGUAL SUPPORT
                if (AppSettings.CurrentSettings.Multilingual)
                {
                    entry.Language = CultureInfo.CurrentUICulture.Name;
                }

                //UPDATE IDENTIFICATION AND TRACKING
                form.FormEntriesSeed = form.FormEntriesSeed + 1L;
                entry.ReferralCode = form.FormEntriesSeed.ToString();

                DateTime date;
                entry.SubmittedOn = inputs.Keys.Contains("SubmittedOn") && DateTime.TryParse((string) inputs["SubmittedOn"], out date) ? date : DateTime.UtcNow;

                //SAVE TO STORAGE
                formsManager.SaveChanges();
            }
        }
    }
}