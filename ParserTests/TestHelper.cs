using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Contrib.MapperProvider;
using Contrib.XmlSerializer;
using NUnit.Framework;

namespace ParserTests
{
    public abstract class TestHelper
    {
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
        }

        protected IXmlMapperProvider MapperProvider { get; private set; }

        [SetUp]
        public void SetUp()
        {
            MapperProvider = new XmlMapperProvider();
        }

        protected static void AssertEqual(XElement original, XElement generated)
        {
            Assert.IsTrue(XNode.DeepEquals(original, generated),
                          string.Format("Should be:{0}{1}{0}{0}Reality:{0}{2}", Environment.NewLine, original, generated));
        }

        protected static void AssertNotEqual(XElement original, XElement generated)
        {
            Assert.IsFalse(XNode.DeepEquals(original, generated),
                          string.Format("Shouldn't be equal with:{0}{1}{0}{0}", Environment.NewLine, original));
        }



        protected void AssertCollection<T>(IEnumerable<T> original, IEnumerable<T> generated, Action<T, T> action)
        {
            Assert.IsNotNull(generated);
            Assert.AreEqual(generated.Count(), original.Count());
            for (int i = 0; i < generated.Count(); i++)
            {
                action(generated.ElementAt(i), original.ElementAt(i));
            }

        }

        protected static XElement ReadXElement(string xml)
        {
            using (var textReader = new StringReader(xml))
            {
                return XElement.Load(textReader);
            }
        }
    }
}