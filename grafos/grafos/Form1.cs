using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace grafos
{
    private List<Point> nodes = new List<Point>();
    private List<Tuple<int, int>> edges = new List<Tuple<int, int>>();
    private bool isAddingNode = false;
    private int selectedNode = -1;
    public partial class Form1 : Form
    {
        private bool isAddingNode;
        private int selectedNode;
        private object nodes;
        private object panel1;

        public Form1()
        {
            InitializeComponent();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            DrawGraph(e.Graphics);
        }
        private void DrawGraph(Graphics g)
        {
            // Dibujar bordes
            foreach (var edge in edges)
            {
                Point node1 = nodes[edge.Item1];
                Point node2 = nodes[edge.Item2];
                g.DrawLine(Pens.Black, node1, node2);
            }

            // Dibujar nodos
            foreach (var node in nodes)
            {
                g.FillEllipse(Brushes.Blue, node.X - 10, node.Y - 10, 20, 20);
            }
        }

        private object GetPanel1()
        {
            return panel1;
        }

        private void btnAddNode_Click(object sender, EventArgs e, object panel1)
        {
            isAddingNode = true;
            selectedNode = -1;
            panel1.Cursor = Cursors.Cross;
        }

        private void btnAddEdge_Click(object sender, EventArgs e)
        {
            isAddingNode = false;
            selectedNode = -1;
            panel1.Cursor = Cursors.Default;
        }
        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (isAddingNode)
            {
                nodes.Add(e.Location);
                panel1.Invalidate();
            }
            else
            {
                // Agregar bordes haciendo clic en dos nodos
                if (selectedNode == -1)
                {
                    foreach (var node in nodes)
                    {
                        if (Distance(e.Location, node) < 10)
                        {
                            selectedNode = nodes.IndexOf(node);
                            break;
                        }
                    }
                }
                else
                {
                    foreach (var node in nodes)
                    {
                        if (Distance(e.Location, node) < 10)
                        {
                            int secondNode = nodes.IndexOf(node);
                            if (selectedNode != secondNode)
                            {
                                edges.Add(new Tuple<int, int>(selectedNode, secondNode));
                                panel1.Invalidate();
                            }
                            break;
                        }
                    }
                    selectedNode = -1;
                }
            }
        }
        private double Distance(Point p1, Point p2)
        {
            int dx = p2.X - p1.X;
            int dy = p2.Y - p1.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
