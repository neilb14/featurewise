using System;
using NUnit.Framework;
using TaktApp;

namespace Tests
{
    [TestFixture]
    public class JsonContentTest
    {
        [Test]
        public void ShouldGenerateJsonForTaktApp()
        {
            var at = new DateTime(2013,9,5,8,0,30);
            Assert.AreEqual("{\"project\":\"moose\",\"name\":\"munch\",\"type\":\"tick\",\"at\":\"20130905080030\"}",JsonContent.Build("moose", "munch", "tick", at));
        }        
    }
}