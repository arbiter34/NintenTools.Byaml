using Syroot.BinaryData;
using Syroot.NintenTools.Byaml.Dynamic;
using Syroot.NintenTools.Yaz0;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ByamlEditor
{
    public partial class BymlEditor : Form
    {
        private const string TEMP_FILE = "temp.byml";
        private const UInt16 YAZ0_MAGIC_BYTES = 0x6159; // "Ya"

        private OpenFileDialog openFileDialog;

        private List<TreeNode> CurrentNodeMatches = new List<TreeNode>();

        private int LastNodeIndex = 0;

        private string LastSearchText;

        private bool compressed = false;

        public BymlEditor()
        {
            InitializeComponent();
            openFileDialog = new OpenFileDialog();
        }

        private void ButtonLoadByml_Click(object sender, EventArgs e)
        {
            string path = textBoxBrowseByml.Text;
            if (path == null || path.Length == 0)
            {
                MessageBox.Show("Please entery a valid byml path.");
                return;
            }
            if (!File.Exists(path))
            {
                return;
            }
            File.Copy(path, path + ".bak", true);

            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (BinaryDataReader reader = new BinaryDataReader(stream, Encoding.UTF8, true))
                {
                    UInt16 magicBytes = reader.ReadUInt16();
                    compressed = magicBytes == YAZ0_MAGIC_BYTES;
                }
            }
            if (compressed)
            {
                Yaz0Compression.Decompress(path, TEMP_FILE);
                if (textBoxBrowseByml.Text.Contains("sbyml"))
                {
                    textBoxBrowseByml.Text = textBoxBrowseByml.Text.Replace("sbyml", "byml");
                }
                path = TEMP_FILE;
            }
            Dictionary<string, dynamic> byamlData = ByamlFile.Load(path);

            treeViewByml.Nodes.Clear();
            treeViewByml.Nodes.Add(AddNode(byamlData, null));
        }

        private void BrowserYml_Click(object sender, EventArgs e)
        {
            // Show the dialog and get result.
            openFileDialog.Reset();
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                textBoxBrowseByml.Text = openFileDialog.FileName;
            }
        }

        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            string searchText = this.textBox1.Text;
            if (String.IsNullOrEmpty(searchText))
            {
                return;
            };


            if (LastSearchText != searchText)
            {
                //It's a new Search
                CurrentNodeMatches.Clear();
                LastSearchText = searchText;
                LastNodeIndex = 0;
                SearchNodes(searchText, treeViewByml.Nodes[0]);
            }

            if (LastNodeIndex >= 0 && CurrentNodeMatches.Count > 0 && LastNodeIndex < CurrentNodeMatches.Count)
            {
                TreeNode selectedNode = CurrentNodeMatches[LastNodeIndex];
                LastNodeIndex++;
                this.treeViewByml.SelectedNode = selectedNode;
                this.treeViewByml.SelectedNode.Expand();
                this.treeViewByml.Select();

            }
        }

        private void TreeViewByml_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (((TypedTreeNode)e.Node).Type.IsGenericType)
            {
                return;
            }
            treeViewByml.LabelEdit = true;
            e.Node.BeginEdit();
        }

        private TreeNode AddNode(object data, TreeNode rootNode)
        {
            TreeNode node = null;
            if (rootNode == null)
            {
                rootNode = new TypedTreeNode("root", null);
                node = rootNode;
            }
            if (IsGenericType(typeof(Dictionary<,>)).Invoke(data) &&
                       HasGenericArgTypes(typeof(string), typeof(object)).Invoke(data)) {
                node = new TypedTreeNode("Dictionary", typeof(Dictionary<,>));
                rootNode.Nodes.Add(node);
                foreach (KeyValuePair<string, object> pair in (Dictionary<string, object>)data) {
                    AddNode(pair, node);
                }
            } else if (IsGenericType(typeof(List<>)).Invoke(data) &&
                       HasGenericArgTypes(typeof(object)).Invoke(data)) {
                node = new TypedTreeNode("List", typeof(List<>));
                rootNode.Nodes.Add(node);
                foreach (object o in (List<object>)data) {
                    AddNode(o, node);
                }
            } else if (IsGenericType(typeof(KeyValuePair<,>)).Invoke(data) &&
                       HasGenericArgTypes(typeof(string), typeof(object)).Invoke(data)) {
                KeyValuePair<string, object> pair = (KeyValuePair<string, object>)data;
                node = new TypedTreeNode(pair.Key, typeof(KeyValuePair<,>));
                rootNode.Nodes.Add(node);
                AddNode(pair.Value, node);
            } else if (!data.GetType().IsGenericType) {
                node = new TypedTreeNode(data.ToString(), data.GetType());
                rootNode.Nodes.Add(node);
            } else {
                throw new Exception("Whoops");
            }

            return node;
        }

        private void SearchNodes(string SearchText, TreeNode StartNode)
        {
            while (StartNode != null)
            {
                if (StartNode.Text.ToLower().Contains(SearchText.ToLower()))
                {
                    CurrentNodeMatches.Add(StartNode);
                };
                if (StartNode.Nodes.Count != 0)
                {
                    SearchNodes(SearchText, StartNode.Nodes[0]);//Recursive Search 
                };
                StartNode = StartNode.NextNode;
            };
        }

        private Predicate<object> HasGenericArgTypes(params Type[] types)
        {
            return maybeHasTypes => maybeHasTypes.GetType().IsGenericType && 
                                    types.ToList<Type>()
                                         .TrueForAll(type => maybeHasTypes.GetType()
                                                                          .GetGenericArguments()
                                                                          .ToList<Type>()
                                                                          .Contains<Type>(type));
        }

        private Predicate<object> IsGenericType(Type type) {
            return maybeGenericType => maybeGenericType.GetType().IsGenericType && maybeGenericType.GetType().GetGenericTypeDefinition() == type;
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            Dictionary<string, dynamic> bymlData = TreeToByml((TypedTreeNode)treeViewByml.Nodes[0]);
            ByamlFile.Save(textBoxBrowseByml.Text, bymlData);
        }

        private dynamic TreeToByml(TypedTreeNode rootNode)
        {
            if (rootNode == null)
            {
                return null;
            }
            if (rootNode.Type == typeof(List<>)) {
                List<TypedTreeNode> nodes = new List<TypedTreeNode>();
                foreach (TypedTreeNode node in rootNode.Nodes) {
                    nodes.Add(node);
                }
                return nodes.ConvertAll<dynamic>(node => TreeToByml(node));
            } else if (rootNode.Type == typeof(Dictionary<,>)) {
                List<TypedTreeNode> nodes = new List<TypedTreeNode>();
                foreach (TypedTreeNode node in rootNode.Nodes)
                {
                    nodes.Add(node);
                }
                return nodes.ConvertAll<KeyValuePair<string, dynamic>>(node => TreeToByml(node))
                            .ToDictionary(pair => pair.Key, pair => pair.Value);
            } else if (rootNode.Type == typeof(KeyValuePair<,>)) {
                return new KeyValuePair<string, dynamic>(rootNode.Text, TreeToByml((TypedTreeNode)rootNode.Nodes[0]));
            } else if (!rootNode.Type.IsGenericType) {
                return Convert.ChangeType(rootNode.Text, rootNode.Type);
            } else {
                throw new Exception("Double Whoops!");
            }
        }
    }
}
