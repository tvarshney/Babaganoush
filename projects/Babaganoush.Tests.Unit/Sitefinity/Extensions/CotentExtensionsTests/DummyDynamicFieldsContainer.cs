using System;
using Telerik.OpenAccess;
using Telerik.Sitefinity.Model;

namespace Babaganoush.Tests.Unit.Sitefinity.Extensions.CotentExtensionsTests
{
    internal class DummyDynamicFieldsContainer : IDynamicFieldsContainer
    {
        public static readonly string NonexistentFieldName = Guid.NewGuid().ToString();
        public static readonly string FieldThatThrowsName = "FieldThatThrows";
        public static readonly string TaxaFieldName = "Tags";
        public static readonly string BooleanFieldName = "Boolean";
        public static readonly string StringFieldName = "String";


        public TrackedList<Guid> Tags { get; set; }
        public bool Boolean { get; set; }
        public string String { get; set; }
        public object FieldThatThrows { get { throw new Exception("Getter Exception"); } set { throw new Exception("Setter Exception"); } }

        public DummyDynamicFieldsContainer(params Guid[] taxaItemIds)
        {
            Tags = new TrackedList<Guid>(taxaItemIds);
        }
    }
}