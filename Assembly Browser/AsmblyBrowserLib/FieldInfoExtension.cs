using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AsmblyBrowserLib
{
    public static class FieldInfoExtension
    {
        public static TreeNode GetTreeNode(this FieldInfo fieldInfo)
        {
            return new TreeNode(nodeType: "[field]",
                                accessModifier: fieldInfo.GetAccessModifier(),
                                typeModifier: fieldInfo.GetTypeModifier(),
                                type: fieldInfo.FieldType.ToGenericTypeString(),
                                fullType: fieldInfo.FieldType.FullName,
                                name: fieldInfo.Name);
        }
        private static string GetAccessModifier(this FieldInfo fieldInfo)
        {
            if (fieldInfo.IsPublic)
                return "public";
            if (fieldInfo.IsPrivate)
                return "private";
            if (fieldInfo.IsFamily)
                return "protected";
            if (fieldInfo.IsAssembly)
                return "internal";
            if (fieldInfo.IsFamilyOrAssembly)
                return "protected internal";

            return "";
        }
        private static string GetTypeModifier(this FieldInfo fieldInfo)
        {
            return fieldInfo.IsStatic ? "static" : "";
        }

    }
}
