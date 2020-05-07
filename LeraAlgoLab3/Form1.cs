using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeraAlgoLab3
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }
        bTree bTr = new bTree();

        private void insertBtn_Click(object sender, EventArgs e)
        {
            String val = InsertBox.Text;
            if (val != "")
            {
                int insval = Convert.ToInt32(val);
                InsertBox.Text = "";
                bTr.Add(bTr.Root, insval);
            }
          
        }

        private void findBtn_Click(object sender, EventArgs e)
        {
            String val = FindBox.Text;
            if (val != "")
            {
                 int findVal = Convert.ToInt32(val);
            FindBox.Text = "";
            Tuple<int, int> t = bTr.Find(bTr.Root,findVal);
            StatusBox.Text = (t.Item1 != -1 && t.Item2 != -1) ? "found" : "not found";
            }
        }

        private void delBtn_Click(object sender, EventArgs e)
        {
            String val = DelBox.Text;
            if (val != "")
            {
            int toDel = Convert.ToInt32(val);
            DelBox.Text = "";
            bTr.Delete(bTr.Root, toDel);
            }

        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            String val = InsertBox.Text;
            int insval = Convert.ToInt32(val);
            bTr.Add(bTr.Root, insval);
        }
    }

    class Node
    {
        public int number;
        public int n;//Number of children
        public bool isLeaf;//If top is leaf
        public List<int> keys = new List<int>();//Keys
        public List<Node> c = new List<Node>();//Children
        public bool isRoot;
        public Node Parent;
        public Node()
        {
            keys = new List<int>();
            c = new List<Node>();
        }
    }
    
    class bTree
    {
        int height;
        int t = 2;
         public Node Root = new Node();
        public bTree()
        {
            Root = new Node();
            Root.isRoot = true;
            Root.isLeaf = true;
        }
        public void Add(Node toAdd, int value)
        {
            int i = 0;
            for ( i = 0; i < toAdd.keys.Count(); i+=1)
            {
                if (toAdd.keys[i] > value)
                {
                    if (toAdd.isLeaf) toAdd.keys.Insert(i, value);
                    else
                        Add(toAdd.c[i], value);
                        break;
                }
            }
            if (i == 0 || i==toAdd.keys.Count())
            {
               if(toAdd.isLeaf) toAdd.keys.Insert(i,value);
               else
                   Add(toAdd.c[i], value);
            }
            if (toAdd.keys.Count() > 2 * t - 1) SplitNode(toAdd);

        }
        void SplitNode(Node toSplit)
        {
            int MedianKey = toSplit.keys[toSplit.keys.Count() / 2];
            if (toSplit.Parent == null) toSplit.Parent = new Node();
            toSplit.Parent.keys.Add(MedianKey);
            toSplit.Parent.keys.Sort();
            toSplit.keys.Remove(MedianKey);
            Node Parent = toSplit.Parent;
            Node toSplitDouble = new Node();
            toSplitDouble.Parent = Parent;
            toSplitDouble.isLeaf = true;
            for(int i = toSplit.keys.Count() / 2 + 1; i < toSplit.keys.Count(); i++)
            {
                toSplitDouble.keys.Add(toSplit.keys[i]);
            }
            for(int i = toSplit.c.Count()/2+1; i < toSplit.c.Count(); i++)
            {
                toSplitDouble.c.Add(toSplit.c[i]);
            }
            toSplit.keys.RemoveRange(toSplit.keys.Count() / 2 + 1,toSplit.keys.Count() - (toSplit.keys.Count() / 2 + 1));
            if (toSplit.c.Count()!=0)toSplit.c.RemoveRange(toSplit.c.Count() / 2 + 1, toSplit.c.Count() - (toSplit.c.Count() / 2 + 1));
            if (toSplit.isRoot)
            {
                Parent.isRoot = true;
                this.Root = Parent;
                Parent.c.Add(toSplit);
                height += 1;
            }
            Parent.c.Add(toSplitDouble);
        }

        public Tuple<int,int> Find(Node current, int val)
        {   
            bool found = false;
            Tuple<int, int> toReturn = new Tuple<int, int>(-1, -1);
            int i;
            if(current.keys.Count()==0)return new Tuple<int, int>(-1, -1);
            if (current.keys.Last()<val && !current.isLeaf)
            { 
                Tuple<int, int> rezult = Find(current.c.Last(), val);
                toReturn = new Tuple<int, int>(Math.Max(toReturn.Item1, rezult.Item1), Math.Max(toReturn.Item2, rezult.Item2));
            }
            else
            for (i = 0; i < current.keys.Count(); i += 1)
            {
                if (current.keys[i] == val) { found = true; toReturn = new Tuple<int, int>(1, 1); break; }
                else
                    if (current.keys[i] > val && current.isLeaf==false) {
                    Tuple<int, int> rezult = Find(current.c[i], val);
                    toReturn = new Tuple<int, int>(Math.Max(toReturn.Item1, rezult.Item1), Math.Max(toReturn.Item2, rezult.Item2));
                    }
            }
            return toReturn;
        }

        public void Delete(Node current, int val)
        {
            Tuple<int, int> toCompare = new Tuple<int, int>(1, 1);
            int i = 1;
            if (Find(Root, val) == toCompare) return;
            for (i = 0; i < current.keys.Count(); i += 1)
            {
                if (current.keys[i] == val) { break; }
                else
                    if (current.keys[i] > val && current.isLeaf == false)
                {
                    Delete(current.c[i - 1], val);
                }
            }
            if (current.isLeaf == true) DeleteLeaf(current, val);
            else
                DeleteNonLeaf(current, val);
        }
        void DeleteNonLeaf(Node from,int val)
        {
            int ind = from.keys.IndexOf(val);
            if (ind-1>=0 && from.c[ind - 1].keys.Count() >= t) { from.keys[ind] = from.c[ind - 1].keys.Last(); Delete(from.c[ind-1],from.keys[ind]); }
            else
            if (ind<from.c[ind].keys.Count() && from.c[ind].keys.Count() >= t) { from.keys[ind] = from.c[ind].keys.First();Delete(from.c[ind], from.keys[ind]); }
            else
            {
                from.keys.Remove(val);
                merge(from.c[ind], from.c[ind - 1]);
                from.c.RemoveAt(ind);
                Delete(from.c[ind - 1], val);
            }
        }


        void merge(Node from,Node to)
        {
            for(int i = 0; i < from.keys.Count(); i++)
            {
                to.keys.Add(from.keys[i]);
            }
            from.keys.Clear();
            for (int i = 0; i < from.c.Count(); i++)
            {
                to.c.Add(from.c[i]);
            }
            from.c.Clear();
        }
        void DeleteLeaf(Node from,int val)
        {
            Node parent = from.Parent;
            from.keys.Remove(val);
            if (from.isRoot == true) return;
            int ind = parent.c.IndexOf(from);
            if (ind == 0)
            {
               if(from.keys.Count()==t-1 && parent.c[ind+1].keys.Count()==t-1)
                {
                    merge(from, parent.c[ind + 1]);
                    parent.c.RemoveAt(0);
                    parent.keys.RemoveAt(0);
                }
               
            }
            else
            if(ind == parent.c.Count()-1)
            {
                if (from.keys.Count() == t - 1 && parent.c[ind - 1].keys.Count() == t - 1)
                {
                    merge(from, parent.c[ind - 1]);
                    parent.c.RemoveAt(ind);
                    parent.keys.RemoveAt(ind);
                }
            }
            else
            {
                if (from.keys.Count() == t - 1 && parent.c[ind - 1].keys.Count() == t - 1 && parent.c[ind + 1].keys.Count() == t - 1)
                {
                    merge(from, parent.c[ind - 1]);
                    parent.c.RemoveAt(ind);
                    parent.keys.RemoveAt(ind);
                }
            }
        }
        public void WriteToFile()
        {
                
        }
    }
}
