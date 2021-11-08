using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AsmblyBrowserLib
{
    public static class MethodBaseExtension
    {
        public static string GetAccessModifier(this MethodBase methodBase)
        {
            if (methodBase.IsPublic)
                return "public";
            if (methodBase.IsPrivate)
                return "private";
            if (methodBase.IsFamily)
                return "protected";
            if (methodBase.IsAssembly)
                return "internal";
            if (methodBase.IsFamilyOrAssembly)
                return "protected internal";

            return "";
        }
        public static IEnumerable<TreeNode> GetParameterTreeNodes(this MethodBase methodBase)
        {
            return (from parameter in methodBase.GetParameters()
                    select parameter.GetTreeNode()).ToList();
        }
    }
}
