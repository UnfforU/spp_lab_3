using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using static System.Reflection.BindingFlags;
using System.Runtime.CompilerServices;

namespace AsmblyBrowserLib
{
    public static class TypeExtension
    {
        public static TreeNode GetTreeNode(this Type type)
        {
            var typeNode = new TreeNode(nodeType: "[type]",
                                        accessModifier: type.GetAccessModifier(),
                                        typeModifier: type.GetTypeModifier(),
                                        classType: type.GetClassType(),
                                        fullType: type.FullName,
                                        name: type.ToGenericTypeString());

            var typeMembers = type.GetMembers(NonPublic | Instance | Public | Static | DeclaredOnly);
            foreach(var member in typeMembers)
            {
                TreeNode treeNode;
                if (member.MemberType == MemberTypes.Method)
                {
                    treeNode = ((MethodInfo)member).GetTreeNode();
                }
                else if(member.MemberType == MemberTypes.Property)
                {
                    treeNode = ((PropertyInfo)member).GetTreeNode();
                }
                else if(member.MemberType == MemberTypes.Event)
                {
                    treeNode = ((EventInfo)member).GetTreeNode();
                }
                else if(member.MemberType == MemberTypes.Field)
                {
                    treeNode = ((FieldInfo)member).GetTreeNode();
                }
                else if(member.MemberType == MemberTypes.Constructor)
                {
                    treeNode = ((ConstructorInfo)member).GetTreeNode();
                }
                else
                {
                    treeNode = ((TypeInfo)member).GetTreeNode();
                }

                if(treeNode != null) { typeNode.AddTreeNode(treeNode); }

            }

            return typeNode;
        }

        public static string GetAccessModifier(this Type type)
        {
            if (type.IsNestedPublic || type.IsPublic)
                return "public";
            if (type.IsNestedPrivate)
                return "private";
            if (type.IsNestedFamily)
                return "protected";
            if (type.IsNestedAssembly)
                return "internal";
            if (type.IsNestedFamORAssem)
                return "protected internal";
            if (type.IsNestedFamANDAssem)
                return "private protected";
            if (type.IsNotPublic)
                return "private";

            return "";
        }

        public static string GetTypeModifier(this Type type)
        {
            if (type.IsAbstract && type.IsSealed)
                return "static";
            if (type.IsAbstract)
                return "abstract";
            if (type.IsSealed)
                return "sealed";

            return "";
        }

        public static string GetClassType(this Type type)
        {
            if (type.GetMethods().Any(m => m.Name == "<Clone>$"))
                return "record";
            if (type.IsClass)
                return "class";
            if (type.IsEnum)
                return "enum";
            if (type.IsInterface)
                return "interface";
            if (type.IsGenericType)
                return "generic";
            if (type.IsValueType && !type.IsPrimitive)
                return "structure";

            return "";
        }

        public static string ToGenericTypeString(this Type type)
        {
            if (!type.IsGenericType)
                return type.Name;

            var genericTypeName = type.GetGenericTypeDefinition().Name;
            genericTypeName = genericTypeName[..genericTypeName.IndexOf('`')];
            var genericArgs = string.Join(", ", type.GetGenericArguments().Select(ToGenericTypeString).ToArray());
            return genericTypeName + "<" + genericArgs + ">";
        }

        public static string ToGenericTypeString(this Type[] types)
        {
            var listTypes = types.Select(type => type.ToGenericTypeString()).ToList();
            return "<" + string.Join(",", listTypes) + ">";
        }

        public static IEnumerable<TreeNode> GetExtensionMethodTreeNodes(this Type type)
        {
            return (from method in type.GetMethods(NonPublic | Instance | Public | Static | DeclaredOnly).Where(m => !m.IsSpecialName)
                                                                                                         .Where(m => m.IsDefined(typeof(ExtensionAttribute), false))
                    select new TreeNode(nodeType: "[method]",
                                        optional: "[extension]",
                                        accessModifier: method.GetAccessModifier(),
                                        typeModifier: method.GetTypeModifier(),
                                        fullType: method.ReturnType.FullName,
                                        returnType: method.ReturnType.ToGenericTypeString(),
                                        name: method.Name,
                                        treeNodes: method.GetParameterTreeNodes()))
                    .ToList();
        }
    }
}
