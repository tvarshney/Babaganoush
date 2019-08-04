using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Babaganoush.Sitefinity.Mvc.Constraints;
using Moq;
using NUnit.Framework;

namespace Babaganoush.Tests.Unit.Mvc.Routes.GuidConstraintTests
{
    [TestFixture]
    internal class MatchShould
    {
        [Test]
        public void ReturnTrueWhenRouteParameterIsGuid()
        {
            const string parameterName = "foo";
            object guidValue = Guid.NewGuid();

            bool match = Match(parameterName, parameterName, guidValue);

            Assert.IsTrue(match, "Route constraint should match on a GUID.");
        }

        [Test]
        public void ReturnTrueWhenRouteParameterIsStringGuid()
        {
            const string parameterName = "foo";
            object guidAsString = Guid.NewGuid().ToString();

            bool match = Match(parameterName, parameterName, guidAsString);

            Assert.IsTrue(match, "Route constraint should match on a GUID passed as a string.");
        }

        [Test]
        public void ReturnFalseWhenParameterNotInRouteValueDictionary()
        {
            bool match = Match("foo", "bar", null);

            Assert.IsFalse(match, "Route constraint should not match when given parameter is not found in the given route value collection.");
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        [TestCase(3)]
        [TestCase("Bob")]
        public void ReturnFalseWhenParameterIsNotAGuid(object value)
        {
            const string parameterName = "param";

            bool match = Match(parameterName, parameterName, value);

            Assert.IsFalse(match, "Route constraint should not match when value is '{0}'.", value);
        }

        // TODO: Are the following two tests accurate? Tests were written against the existing code.
        [Test]
        public void ReturnTrueForEmptyGuid()
        {
            const string parameterName = "bar";

            bool match = Match(parameterName, parameterName, Guid.Empty);

            Assert.IsTrue(match, "Route constraint should match when value is empty GUID.");
        }

        [Test]
        public void ReturnFalseForEmptyGuidString()
        {
            const string parameterName = "bar";

            bool match = Match(parameterName, parameterName, Guid.Empty.ToString());

            Assert.IsFalse(match, "Route constraint should not match when value is empty GUID string.");
        }

        private static bool Match(string parameterName, string parameterNameInCollection, object value)
        {
            var mockedContext = new Mock<HttpContextBase>();
            IRouteConstraint guidConstraint = new GuidConstraint();
            var routeValueDictionary = new RouteValueDictionary(new Dictionary<string, object> { { parameterNameInCollection, value } });
            return guidConstraint.Match(mockedContext.Object, new Route(null, null), parameterName, routeValueDictionary, RouteDirection.IncomingRequest);
        }
    }
}