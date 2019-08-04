using Babaganoush.Sitefinity.Content.Managers.Abstracts;
using Babaganoush.Tests.FooFoo.Sitefinity.Models;
using Telerik.Sitefinity.DynamicModules.Model;

namespace Babaganoush.Tests.FooFoo.Sitefinity.Content.Managers
{
    public class SpeakersManager : DynamicModuleManager<SpeakersManager, SpeakerModel>
    {
        /// <summary>
        /// Creates the Baba instance from the Sitefinity object.
        /// </summary>
        /// <param name="sfContent">Content of the sf.</param>
        /// <returns></returns>
        protected override SpeakerModel CreateInstance(DynamicContent sfContent)
        {
            return new SpeakerModel(sfContent);
        }
    }
}
