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
                return new TreeNode(nodeType: "[constructor]",
                                    accessModifier: methodInfo.GetAccessModifier(),
                                    name: methodInfo.Name,
                                    treeNodes: methodInfo.GetParameterTreeNodes());
            }
            if (!methodInfo.IsSpecialName)
            {
                return new TreeNode(nodeType: "[method]",
                                    accessModifier: methodInfo.GetAccessModifier(),
                                    typeModifier: methodInfo.GetTypeModifier(),
                                    returnType: methodInfo.ReturnType.ToGenericTypeString(),
                                    name: methodInfo.Name + (methodInfo.IsGenericMethod ? methodInfo.GetGenericArguments().ToGenericTypeString() : null),
                                    treeNodes: methodInfo.GetParameterTreeNodes());
            }
            if (methodInfo.IsFinal && (methodInfo.Name == "Finalize"))
            {
                return new TreeNode(nodeType: "[finalizer]",
                                    accessModifier: methodInfo.GetAccessModifier(),
                                    name: methodInfo.Name);
            }
            return null;
          
        }
        public static string GetTypeModifier(this MethodInfo methodInfo)
        {
            if (methodInfo.IsAbstract)
                return "abstract";
            if (methodInfo.IsStatic)
                return "static";
            if (methodInfo.IsVirtual)
                return "virtual";
            if (methodInfo.GetBaseDefinition() != methodInfo)
                return "override";

            return "";
        }

    }
}
