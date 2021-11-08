using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AsmblyBrowserLib
{
    public static class ParameterInfoExtension
    {
        public static TreeNode GetTreeNode(this ParameterInfo parameterInfo)
        {
            return new TreeNode(nodeType: "[param]",
                                typeModifier: parameterInfo.GetTypeModifier(),
                                type: parameterInfo.ParameterType.ToGenericTypeString(),
                                fullType: parameterInfo.ParameterType.FullName,
                                name: parameterInfo.Name);
        }

        public static string GetTypeModifier(this ParameterInfo parameterInfo)
        {
            if (parameterInfo.IsRetval)
                return "ret";
            if (parameterInfo.IsIn)
                return "in";
            if (parameterInfo.IsOut)
                return "virtual";

            return "";
        }
    }
}
