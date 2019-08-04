using Babaganoush.Tests.FooFoo.Sitefinity.Content.Managers;

namespace Babaganoush.Tests.FooFoo.Sitefinity.Data
{
    public class FooFooManagers
    {
        public static SpeakersManager Speakers { get { return SpeakersManager.Instance; } }
        public static SessionsManager Sessions { get { return SessionsManager.Instance; } }
    }
}
