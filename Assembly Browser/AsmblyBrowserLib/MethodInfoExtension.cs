using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AsmblyBrowserLib
{
    public static class MethodInfoExtension
    {
        public static TreeNode GetTreeNode(this MethodInfo methodInfo)
        {
            if (methodInfo.IsConstructor)
            {
                return new TreeNode("[constructor]", accessModifier: methodInfo.GetAccessModifier(), name: methodInfo.Name, treeNodes: methodInfo.GetParameterNodes());
            }
            if (!methodInfo.IsSpecialName)
            {
                return null;
            }
            if (methodInfo.IsFinal && (methodInfo.Name == "Finalize"))
            {
                return new TreeNode("[finalizer]", accessModifier: methodInfo.GetAccessModifier(), name: "Finalize");
            }
            return null;
          
        }
        public static IEnumerable<TreeNode> GetParameterNodes(this MethodInfo methodInfo)
        {
            return (from parameter in methodInfo.GetParameters()
                    select parameter.GetTreeNode()).ToList();
        }

        public static string GetAccessModifier(this MethodInfo methodInfo)
        {
            if (methodInfo.IsPublic)
                return "public";
            if (methodInfo.IsPrivate)
                return "private";
            if (methodInfo.IsFamily)
                return "protected";
            if (methodInfo.IsAssembly)
                return "internal";
            if (methodInfo.IsFamilyOrAssembly)
                return "protected internal";

            return "";
        }

    }
}
