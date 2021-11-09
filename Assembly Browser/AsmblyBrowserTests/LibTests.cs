using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using AsmblyBrowserLib;


namespace AsmblyBrowserTests
{
    [TestClass]
    public class LibTests
    {
        private static string Path = "..\\..\\..\\..\\AsmblyBrowserLib\\bin\\Debug\\net5.0\\AsmblyBrowserLib.dll";
        private List<TreeNode> targetData = MainPage.GetData(Path);

        [TestMethod]
        public void TestMethod()
        {
            var testMethod = targetData[0].TreeNodes[0].TreeNodes[0];
            Assert.AreEqual("[method]", testMethod.NodeType);
        }

        [TestMethod]
        public void TestClass()
        {
            var testClass = targetData[0].TreeNodes[0];
            Assert.AreEqual("public", testClass.AccessModifier);
            Assert.AreEqual("class", testClass.ClassType);
            Assert.AreEqual("static", testClass.TypeModifier);
        }

        [TestMethod]
        public void TestParam()
        {
            var testParam = targetData[0].TreeNodes[3].TreeNodes[1].TreeNodes[0];
            Assert.AreEqual("[param]", testParam.NodeType);
            Assert.AreEqual("treeNodes", testParam.Name);
        }
        [TestMethod]
        public void TestProperty()
        {
            var testPropety = targetData[0].TreeNodes[8].TreeNodes[5];
            Assert.AreEqual("[property]", testPropety.NodeType);
            Assert.AreEqual("public", testPropety.AccessModifier);
            Assert.AreEqual("TypeModifier", testPropety.Name);
            Assert.AreEqual("String", testPropety.Type);
        }

        [TestMethod]
        public void TestPropertyAccessor()
        {
            var testPropetyAccessorGet = targetData[0].TreeNodes[8].TreeNodes[2].TreeNodes[0];
            var testPropetyAccessorSet = targetData[0].TreeNodes[8].TreeNodes[2].TreeNodes[1];
            Assert.AreEqual("[accessor]", testPropetyAccessorGet.NodeType);
            Assert.AreEqual("get_NodeType",testPropetyAccessorGet.Name);
            Assert.AreEqual("[accessor]", testPropetyAccessorSet.NodeType);
            Assert.AreEqual("set_NodeType", testPropetyAccessorSet.Name);
        }
    }
}
