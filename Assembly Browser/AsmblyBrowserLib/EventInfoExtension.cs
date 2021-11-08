using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AsmblyBrowserLib
{
    public static class EventInfoExtension
    {
        public static TreeNode GetTreeNode(this EventInfo eventInfo)
        {
            return new TreeNode(nodeType: "[event]",
                                type: eventInfo.EventHandlerType.ToGenericTypeString(),
                                fullType: eventInfo.EventHandlerType.FullName,
                                name: eventInfo.Name);
        }
    }
}
