using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AsmblyBrowserLib
{
    public static class MainPage
    {
        private static List<TreeNode> MethodExtensions = new();
        public static List<TreeNode> GetData(string filePath)
        {
            var assemblyInfo = new Dictionary<string, TreeNode>();
            var assemblyTypes = Assembly.LoadFrom(filePath).GetTypes();
            foreach (var type in assemblyTypes)
            {
                if(type.Namespace != null)
                {
                    if (!assemblyInfo.ContainsKey(type.Namespace)) { assemblyInfo.Add(type.Namespace, new TreeNode(nodeType: "[namespace]", name: type.Namespace)); }

                    var namespaceTreeNode = assemblyInfo[type.Namespace];
                    var nodeType = type.GetTreeNode();
                    MethodExtensions.AddRange(type.GetExtensionMethodTreeNodes());
                    namespaceTreeNode.AddTreeNode(nodeType);
                }
            }

            var result = assemblyInfo.Values.ToList();
            InsertExtensionMethods(result);
            MethodExtensions.Clear();
            return result;
        }
        
        private static void InsertExtensionMethods(List<TreeNode> treeNodes)
        {
            foreach(var extensionMethod in MethodExtensions)
            {
                var extendedType = extensionMethod.TreeNodes[0].FullType;
                foreach(var namespaceTreeNode in treeNodes)
                {
                    foreach(var typeTreeNode in namespaceTreeNode.TreeNodes)
                    {
                        if(typeTreeNode.FullType == extendedType) { typeTreeNode.AddTreeNode(extensionMethod); }
                    }
                }
            }
        }

        public static void AddRange(this TreeNode node, IEnumerable<TreeNode> nodes)
        {
            if (nodes != null)
            {
                node.TreeNodes.AddRange(nodes);
            }
        }
    }
}
