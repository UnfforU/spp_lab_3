using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AsmblyBrowserLib
{
    public static class PropertyInfoExtension
    {
        public static TreeNode GetTreeNode(this PropertyInfo propertyInfo)
        {
            return new TreeNode(nodeType: "[property]",
                                accessModifier: propertyInfo.GetGetMethod(true).GetAccessModifier(),
                                type: propertyInfo.PropertyType.ToGenericTypeString(),
                                fullType: propertyInfo.PropertyType.FullName,
                                name: propertyInfo.Name,
                                treeNodes: propertyInfo.GetAccessorsTreeNodes());
        }

        private static IEnumerable<TreeNode> GetAccessorsTreeNodes(this PropertyInfo propertyInfo)
        {
            return (from accessor in propertyInfo.GetAccessors(true)
                    select new TreeNode(nodeType: "[accessor]",
                                        accessModifier: accessor.GetAccessModifier(),
                                        name: accessor.Name)).ToList();
        }
    }
}
