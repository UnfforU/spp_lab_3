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
        public static List<TreeNode> GetData(string filePath)
        {
            var assemblyInfo = new Dictionary<string, TreeNode>();
            var assemblyTypes = Assembly.LoadFrom(filePath).GetTypes();
            foreach (var type in assemblyTypes)
            {
                if(type.Namespace != null)
                {
                    if (!assemblyInfo.ContainsKey(type.Namespace)) { assemblyInfo.Add(type.Namespace, new TreeNode(nodeType: "[namespace]", name: type.Namespace)); }

                    var namespaceNode = assemblyInfo[type.Namespace];
                    var nodeType = type.GetTreeNode();
                    namespaceNode.AddTreeNode(nodeType);
                }
            }

            var result = assemblyInfo.Values.ToList();
            return result;
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
