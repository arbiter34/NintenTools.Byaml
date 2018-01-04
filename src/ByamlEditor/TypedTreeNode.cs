using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ByamlEditor
{
    class TypedTreeNode : TreeNode
    {
        private Type type;

        public TypedTreeNode(string name, Type type) : base(name)
        {
            this.type = type;
        }

        public Type Type { get => type; set => type = value; }
    }
}
