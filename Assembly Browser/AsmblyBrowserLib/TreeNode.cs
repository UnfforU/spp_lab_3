using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsmblyBrowserLib
{
    public class TreeNode
    {
        public string NodeType { get; set; }
        public string Optional { get; set; }
        public string AccessModifier { get; set; }
        public string TypeModifier { get; set; }
        public string ClassType { get; set; }
        public string Type { get; set; }
        public string FullType { get; set; }
        public string ReturnType { get; set; }
        public string Name { get; set; }
        public List<TreeNode> TreeNodes { get; } = new();

        public TreeNode(string nodeType, string optional = "", string accessModifier = "", string typeModifier = "", string classType = "",
                        string type = "", string fullType = "", string returnType = "", string name = "", IEnumerable<TreeNode> treeNodes = null)
        {
            NodeType = nodeType;//7
            Optional = optional;
            AccessModifier = accessModifier;//7
            TypeModifier = typeModifier;//3
            ClassType = classType;//1
            Type = type;//1
            FullType = fullType;//2
            ReturnType = returnType;//1
            Name = name;//7
            this.AddRange(treeNodes);
        }
        public void AddTreeNode(TreeNode TreeNode)
        {
            if(TreeNode != null)
            {
                TreeNodes.Add(TreeNode);
            }
        }
    }
}
