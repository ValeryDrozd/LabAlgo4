using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
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
            panel1.Refresh();
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
            panel1.Refresh();
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            String val = InsertBox.Text;
            int insval = Convert.ToInt32(val);
            bTr.Add(bTr.Root, insval);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = panel1.CreateGraphics();
            List<Node>[] lvl = new List<Node>[bTr.height+2];
            SolidBrush b = new SolidBrush(System.Drawing.Color.Black);
            Font font = new Font("Arial", 8);
            for(int i = 1;i<=bTr.height+1;i++)
            {
                lvl[i] = new List<Node>();
            }
            lvl[1].Add(bTr.Root);
            for(int i = 1; i < bTr.height; i++)
            {
                foreach(Node child in lvl[i])
                {
                    for (int j = 0; j < child.c.Count(); j++)
                        lvl[i + 1].Add(child.c[j]);
                }
            }
            int topHeight = (244 - (22 * bTr.height*2))/2;
            string line = "";
            for (int i = 1; i <= bTr.height; i++)
            {
                line = "|";
                for (int j = 0; j <lvl[i].Count(); j++)
                {
                    int k = 0;
                    for ( k = 0; k < lvl[i][j].keys.Count(); k++)
                        line = line + " " + lvl[i][j].keys[k].ToString() + " |";
                    for (k = k; k < 2 * bTr.t - 1; k++)
                        line += " - |";
                    if(j!=lvl[i].Count() - 1)line += "        |";
                }
                int horCoord = (478 - 5 * (line.Length - 1) / 2) / 2;
                int verCoord = topHeight + (11 * (i + 1));
                g.DrawString(line,font, b, horCoord, verCoord);
            }

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
        public int height;
        public int t = 2;
        public Node Root = new Node();
        public bTree()
        {
            Root = new Node();
            Root.isRoot = true;
            Root.isLeaf = true;
            height = 0;
        }
		int lowerBound(List<int> ls, int val)
		{
			int l = 0, r = ls.Count() - 1;
			if (val > ls[r]) return -1;
			if (val <= ls[0]) return 0;
			while (r - l != 1)
			{
				int mid = (r + l) / 2;
				if (ls[mid] <= val) r = mid;
				else
					l = mid;
			}
			return r;
		}
		public void Add(Node toAdd, int value)
        {
            if (height == 0) height = 1;
            int i = 0;
            bool added = false;
            for ( i = 0; i < toAdd.keys.Count(); i+=1)
            {
                if (toAdd.keys[i] > value)
                {
                    if (toAdd.isLeaf) { toAdd.keys.Insert(i, value); if (toAdd.keys.Count() > 2 * t - 1) SplitNode(toAdd); }
                    else
                        Add(toAdd.c[i], value);
                    return;
                }
            }
            if (!added && (i == 0 || i==toAdd.keys.Count()))
            {
               if(toAdd.isLeaf) toAdd.keys.Insert(i,value);
               else
                   Add(toAdd.c[i], value);
            }
            if (toAdd.keys.Count() > 2 * t - 1) SplitNode(toAdd);

        }

        void SplitNode(Node toSplit)
        {                
            Node Parent = new Node();
            int medianKey = toSplit.keys.Count()/2;
            if (toSplit.Parent == null) {
                this.Root = Parent;
                Parent.isRoot = true;
                toSplit.isRoot = false;
                toSplit.Parent = Parent;
                Parent.isLeaf = false;
                this.height += 1;
            }
            else
                Parent = toSplit.Parent;
            Node copySplit = new Node();
            copySplit.Parent = toSplit.Parent;
            copySplit.isLeaf = toSplit.isLeaf;
            for(int i = toSplit.keys.Count() / 2+1; i < toSplit.keys.Count(); i++)
            {
                copySplit.keys.Add(toSplit.keys[i]);
            }
            toSplit.keys.RemoveRange(toSplit.keys.Count() / 2 + 1, toSplit.keys.Count() - (toSplit.keys.Count() / 2 + 1));
            
            if (toSplit.c.Count() != 0)
            {
                for(int i = toSplit.c.Count() / 2 + 1; i < toSplit.c.Count(); i++){
                    Node currentPush = toSplit.c[i];
                    currentPush.Parent = copySplit;
                    copySplit.c.Add(currentPush);
                }
                toSplit.c.RemoveRange(toSplit.c.Count() / 2 + 1, toSplit.c.Count() - (toSplit.c.Count() / 2 + 1));
            }
            int pushIndex;
            for (pushIndex = 0; pushIndex < Parent.keys.Count(); pushIndex++)
            {
                if (Parent.keys[pushIndex] > toSplit.keys[medianKey]) break;
            }
            Parent.keys.Insert(pushIndex, toSplit.keys[medianKey]);
            toSplit.keys.RemoveAt(medianKey);
            if (Parent.c.Count() == 0)
            {
                    Parent.c.Add(toSplit);
                    Parent.c.Add(copySplit);
            }
            else
            { 
                for(int i = 0; i < Parent.c.Count(); i++)
                {
                        if (Parent.c[i] == toSplit) {Parent.c.Insert(i+1,copySplit); break; }
                }
            }
            if (Parent.keys.Count() == 2 * this.t)SplitNode(Parent);
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

            Tuple<int, int> found = Find(Root, val);
            if (found.Item1 == -1 && found.Item2==-1) return;
            int Index = lowerBound(current.keys, val);
            if (Index == -1) Delete(current.c[current.c.Count() - 1], val);
            else
            if (current.keys[Index] == val)
            {
                if (current.isLeaf) DeleteLeaf(current, val);
                else
                    DeleteNonLeaf(current, val);
            }
            else
                Delete(current.c[Index],val);
            if (current.c.Count < t-1 ) repair(current);
        }
        void repair(Node n)
        {
            Node Parent = n.Parent;
            if (Parent == null) return;
            int ind = Parent.c.IndexOf(n);
            if (ind != Parent.c.Count() - 1 && Parent.c[ind + 1].c.Count()>=t )
            {
                borrowFromNext(Parent,ind);
            }
            else
            if (ind!=0 && Parent.c[ind-1].c.Count()>=t)               
            {
                borrowFromPrev(Parent,ind);
            }
            else
            {
                if (ind!=Parent.c.Count()-1) merge(Parent.c[ind+1],n);
                else
                    merge(Parent.c[ind - 1], n);
            }
            return;
        }
        void borrowFromPrev(Node n,int ind)
        {
            Node child = n.c[ind];
            Node sibl = n.c[ind - 1];
            child.c.Insert(0,sibl.c[sibl.c.Count() - 1]);
            child.keys.Insert(0, sibl.keys[sibl.keys.Count() - 1]);
            sibl.c.RemoveAt(sibl.c.Count() - 1);
            sibl.keys.RemoveAt(sibl.keys.Count() - 1);

        }
        void borrowFromNext(Node n,int ind)
        {
            Node child = n.c[ind];
            Node sibl = n.c[ind + 1];
            child.keys.Add(sibl.keys[0]);
            child.c.Add(sibl.c[0]);
            sibl.keys.RemoveAt(0);
            sibl.c.RemoveAt(0);
        }
        int getPred(Node From)
        {
            while (!From.isLeaf)
                From = From.c[From.c.Count() - 1];
            return From.keys[From.keys.Count() - 1];
        }

        int getSuc(Node From)
        {
            while (!From.isLeaf)
                From = From.c[0];
            return From.keys[0];
        }
        void DeleteNonLeaf(Node from,int val)
        {
            int ind = from.keys.IndexOf(val);

            if (from.c[ind].keys.Count() >= t-1)
            {
                int pred = getPred(from.c[ind]);
                from.keys[ind] = pred;
                Delete(from.c[ind], pred);
            }
            else
            if (from.c[ind + 1].keys.Count() >= t-1)
            {
                int suc = getSuc(from.c[ind + 1]);
                from.keys[ind] = suc;
                Delete(from.c[ind + 1], suc);
            }
            else
            {
                merge(from.c[ind+1],from.c[ind]);
                Delete(from.c[ind], val);
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
            from.Parent.c.Remove(from);
        }

        void DeleteLeaf(Node from,int val)
        {
            from.keys.Remove(val);
        }
        public void WriteToFile()
        {
                
        }
    }
}
