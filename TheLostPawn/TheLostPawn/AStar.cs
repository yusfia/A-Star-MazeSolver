using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLostPawn
{
    class Node
    {
        public Point parent { get; set; }
        public Point node { get; set; }
        public double gx { get; set; }
        public double hx { get; set; }
        public double fx { get; set; }
        public double sumgx { get; set; }
    }

    class AStar
    {
        List<Node> Open;
        List<Node> Close;
        Node Start, Goal;
        TileBox[,] masalah;
        public AStar(Node start,Node goal)
        {
            Open = new List<Node>();          
            Close = new List<Node>();
            Start = new Node();
            Goal = new Node();
            start.gx = 0;
            start.sumgx = 0;
            start.parent = start.node;
            hitungHeuristic(start);
            start.fx = start.hx + start.sumgx;
            Start = start;
            Goal = goal;
            Open.Add(Start);
            
        }

        public void hitungHeuristic(Node ceckedpos)
        {
            double dx = Math.Abs(ceckedpos.node.X - Goal.node.X);
            double dy = Math.Abs(ceckedpos.node.Y - Goal.node.Y);
            ceckedpos.hx = dx+dy;//Math.Sqrt((dx * dx) + (dy * dy));
        }

        public void hitungGx(Node ceckedpos, Node current)
        {
            ceckedpos.gx = 0;
            if ((ceckedpos.node.X == current.node.X) || (ceckedpos.node.Y == current.node.Y)){
                ceckedpos.gx = 5;
            }
            else
            {
                ceckedpos.gx = Math.Sqrt(50);// Math.Sqrt(200);
            }
            ceckedpos.sumgx = current.sumgx + ceckedpos.gx;
        }

        public void hitungFX(Node ceckedpos, Node current){
            hitungHeuristic(ceckedpos); 
            hitungGx(ceckedpos, current);
            ceckedpos.fx = ceckedpos.sumgx + ceckedpos.hx;
        }

        public Node temukanFXterkecil()
        {
            Node bestNode = new Node();
            bestNode = Open[0];
            if (Open.Count > 1) 
            {
                for (int i = 1; i < Open.Count; i++)
                {
                    if ((bestNode.fx) > (Open[i].fx))
                    {
                        bestNode = Open[i];
                    }
                }
            }
            return bestNode;
        }

        public void insertKetetanggaan(List<Node> tetangga, int posx, int posy, Node currentNode)
        {
            Node a = new Node();
            a.parent = currentNode.node;
            a.node = new Point(posx, posy);
            tetangga.Add(a);
        }

        public bool cekKetetanggaan(TileBox[,] masalah, int koordinatX, int koordinatY, Node currentNode)
        {
            bool diperbolehkan = false;
            diperbolehkan = ((masalah[koordinatX, koordinatY].sign == '0' || masalah[koordinatX,koordinatY].sign == 'f') && !(koordinatX==currentNode.parent.X && koordinatY==currentNode.parent.Y));
            return diperbolehkan;
        }

        public List<Node> cariTetangga(TileBox[,] masalah,Node currentNode, int height, int width)
        {
            List<Node> tetangga = new List<Node>();
            if (cekKetetanggaan(masalah, currentNode.node.X, currentNode.node.Y - 1, currentNode))
            {
                insertKetetanggaan(tetangga, currentNode.node.X, currentNode.node.Y - 1, currentNode);
            }

            //atas kanan
            if (cekKetetanggaan(masalah, currentNode.node.X + 1, currentNode.node.Y - 1, currentNode))
            {
                insertKetetanggaan(tetangga, currentNode.node.X + 1, currentNode.node.Y - 1, currentNode);
            }

            //kanan
            if (cekKetetanggaan(masalah, currentNode.node.X + 1, currentNode.node.Y, currentNode))
            {
                insertKetetanggaan(tetangga, currentNode.node.X + 1, currentNode.node.Y, currentNode);
            }

            //kanan bawah
            if (cekKetetanggaan(masalah, currentNode.node.X + 1, currentNode.node.Y + 1, currentNode))
            {
                insertKetetanggaan(tetangga, currentNode.node.X + 1, currentNode.node.Y + 1, currentNode);
            }

            //bawah
            if (cekKetetanggaan(masalah, currentNode.node.X, currentNode.node.Y + 1, currentNode))
            {
                insertKetetanggaan(tetangga, currentNode.node.X, currentNode.node.Y + 1, currentNode);
            }
            //kiri bawah

            if (cekKetetanggaan(masalah, currentNode.node.X - 1, currentNode.node.Y + 1, currentNode))
            {
                insertKetetanggaan(tetangga, currentNode.node.X - 1, currentNode.node.Y + 1, currentNode);
            }

            //kiri
            if (cekKetetanggaan(masalah, currentNode.node.X - 1, currentNode.node.Y, currentNode))
            {
                insertKetetanggaan(tetangga, currentNode.node.X - 1, currentNode.node.Y, currentNode);
            }

            //kiri atas
            if (cekKetetanggaan(masalah, currentNode.node.X - 1, currentNode.node.Y - 1, currentNode))
            {
                insertKetetanggaan(tetangga, currentNode.node.X - 1, currentNode.node.Y - 1, currentNode);
            }
            /*
            //atas
            if (cekKetetanggaan(masalah,currentNode.node.X, currentNode.node.Y - 1, currentNode))
            {
                insertKetetanggaan(tetangga, currentNode.node.X, currentNode.node.Y - 1,currentNode);
            }
            
            //atas kanan
            if (cekKetetanggaan(masalah,currentNode.node.X + 1, currentNode.node.Y - 1,currentNode))
            {
                insertKetetanggaan(tetangga, currentNode.node.X + 1, currentNode.node.Y - 1, currentNode);
            }
            
            //kanan
            if (cekKetetanggaan(masalah, currentNode.node.X + 1, currentNode.node.Y, currentNode))
            {
                insertKetetanggaan(tetangga, currentNode.node.X + 1, currentNode.node.Y, currentNode);
            }
            
            //kanan bawah
            if (cekKetetanggaan(masalah,currentNode.node.X + 1, currentNode.node.Y + 1,currentNode))
            {
                insertKetetanggaan(tetangga, currentNode.node.X + 1, currentNode.node.Y + 1, currentNode);
            }

            //bawah
            if (cekKetetanggaan(masalah,currentNode.node.X, currentNode.node.Y + 1,currentNode))
            {
                insertKetetanggaan(tetangga, currentNode.node.X, currentNode.node.Y + 1, currentNode);
            }
            //kiri bawah

            if (cekKetetanggaan(masalah, currentNode.node.X - 1, currentNode.node.Y + 1, currentNode))
            {
                insertKetetanggaan(tetangga, currentNode.node.X - 1, currentNode.node.Y + 1, currentNode);
            }

            //kiri
            if (cekKetetanggaan(masalah,currentNode.node.X - 1, currentNode.node.Y ,currentNode))
            {
                insertKetetanggaan(tetangga, currentNode.node.X - 1, currentNode.node.Y, currentNode);
            }

            //kiri atas
            if (cekKetetanggaan(masalah, currentNode.node.X - 1, currentNode.node.Y - 1, currentNode))
            {
                insertKetetanggaan(tetangga, currentNode.node.X - 1, currentNode.node.Y - 1, currentNode); 
            }
             */
            return tetangga;
        }

        public void insertSuksesor(List<Node> tetangga, Node CurrentNode)
        {
            for (int i = 0; i < tetangga.Count; i++)
            {
                hitungFX(tetangga[i], CurrentNode);
                Open.Add(tetangga[i]);
            }
        }

        public void hitungGXtetangga(List<Node> tetangga, Node CurrentNode)
        {
            for (int i = 0; i < tetangga.Count; i++)
            {
                hitungGx(tetangga[i], CurrentNode);
            }
        }

        public bool cekDiOpenatauClose(Node suksesor, List<Node> oc)
        {
            bool nilai = false;
            for (int i = 0; i < oc.Count; i++)
            {
                if (suksesor.node.X == oc[i].node.X && suksesor.node.Y == oc[i].node.Y)
                {
                    nilai = true;
                }
            }
            return nilai;
        }

        public Node ambilDiOpenatauClose(Node suksesor, List<Node> oc)
        {
            Node nilai = null;
            for (int i = 0; i < oc.Count; i++)
            {
                if (suksesor.node.X == oc[i].node.X && suksesor.node.Y == oc[i].node.Y)
                {
                    nilai = oc[i];
                }
            }
            return nilai;
        }

        public int temukanIndeksOpenatauClose(Node suksesor, List<Node> oc)
        {
            int nilai = oc.Count;
            for (int i = 0; i < oc.Count; i++)
            {
                if (suksesor.node.X == oc[i].node.X && suksesor.node.Y == oc[i].node.Y)
                {
                    nilai = i;
                }
            }
            return nilai;
        }

        
        public void periksaSuksesor(List<Node> tetangga, Node CurrentNode)
        {
            for (int i = 0; i < tetangga.Count; i++)
            {
                if (cekDiOpenatauClose(tetangga[i],Open))
                {
                    Node old = ambilDiOpenatauClose(tetangga[i],Open);
                    if (tetangga[i].sumgx < old.sumgx) // jika suksesor baru yang lebih baik
                    {
                        Open[temukanIndeksOpenatauClose(old, Open)] = tetangga[i];
                    }
                }
                    
                else if (cekDiOpenatauClose(tetangga[i], Close))
                {
                    Node old = ambilDiOpenatauClose(tetangga[i], Close);
                    if (tetangga[i].sumgx < old.sumgx)
                    {
                        Close[temukanIndeksOpenatauClose(old, Close)] = tetangga[i];
                    }
                }
                    
                else
                {
                    Open.Add(tetangga[i]);
                    hitungFX(tetangga[i], CurrentNode);
                }
            }
        }

        public bool AlgoritmaAStar(TileBox[,] problem,int height, int width)
        {
            bool solusi = false;

            masalah = problem;
            List<Node> tetangga = new List<Node>();
            Node bestNode = new Node();
            //char sign = masalah[Start.node.X,Start.node.Y].sign;
            while (!((bestNode.node.X==Goal.node.X) && (bestNode.node.Y==Goal.node.Y)))
            {
                if (Open.Count == 0)
                {
                    //Console.Out.WriteLine("Tidak ada Node untuk digenerate...");
                    return false;
                }
                else
                {
                    bestNode = temukanFXterkecil();
                    Open.Remove(bestNode);
                    //masalah[bestNode.node.X, bestNode.node.Y].sign = '1';
                    Close.Add(bestNode);
                    if ((bestNode.node.X == Goal.node.X) && (bestNode.node.Y == Goal.node.Y))
                    {
                        solusi = true;
                        Goal.parent = bestNode.parent;
                        Console.Out.WriteLine("Finish...");
                    }else{
                        tetangga.Clear();
                        tetangga = new List<Node>();
                        tetangga = cariTetangga(masalah, bestNode, height, width);
                        if (tetangga.Count > 0)
                        {
                            hitungGXtetangga(tetangga, bestNode);
                            periksaSuksesor(tetangga, bestNode);
                        }
                        //insertSuksesor(tetangga,bestNode);
                    }
                }
            }
            return solusi;
        }
        public void testOutput()
        {
            List<Node> jalur = jalurAStar();
            for (int i = 0; i < jalur.Count; i++)
            {
                Console.Out.WriteLine(jalur[i].node.X + "," + jalur[i].node.Y + " parent " + jalur[i].parent.X + ","+jalur[i].parent.Y);
            }
        }

        public List<Node> listClose()
        {
            return Close;
        }

        public List<Node> jalurAStar()
        {
            List<Node> jalur = new List<Node>();
            List<Node> jalur1 = new List<Node>();
            Node inisiator;
            inisiator = Goal;
            jalur.Add(Goal);
            while (!(inisiator.node.X==Start.node.X && inisiator.node.Y==Start.node.Y))
            {
                Node temp = new Node();
                temp.node = new Point(inisiator.parent.X, inisiator.parent.Y);
                inisiator = ambilDiOpenatauClose(temp, Close);
                jalur.Add(inisiator);
            }
            if (jalur.Count > 0) {
                for (int i = 0; i < jalur.Count; i++)
                {
                    jalur1.Add(jalur[jalur.Count - i - 1]);
                }
            }
            return jalur1;
        }
    }
}
