using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AsmblyBrowserLib
{
    public static class ConstructorInfoExtension
    {
        public static TreeNode GetTreeNode(this ConstructorInfo constructorInfo)
        {
            return new TreeNode(nodeType: "[constructor]",
                                accessModifier: constructorInfo.GetAccessModifier(),
                                name: constructorInfo.Name,
                                treeNodes: constructorInfo.GetParameterTreeNodes());
        }
    }
}
