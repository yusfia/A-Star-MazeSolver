using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Runtime.InteropServices;
using System.Threading;

/*
 * Author : Yusfia Hafid Aristyagama STEI ITB
 * TMDG9 - 23214355
 * SISTEM INTELIJEN
 * 2015
 */
namespace TheLostPawn
{
    public partial class Form1 : Form
    {
        Color CurrentColor = Color.White;
        Image currentBackground = null;
        MainChar pawn, finish;
        PictureBox[,] mazebound;
        Map map;
        int width_temp, height_temp;

        public Form1()
        {
            InitializeComponent();
            disablePawn(groupBox2,pawn_x,pawn_y);
            disablePawn(groupBox4, finish_x, finish_y);
        }

        private void CreateBlankMaze()
        {
//            pawn = new MainChar();
            map = new Map((int)width.Value, (int)height.Value, 24);
            width_temp = (int)width.Value;
            height_temp = (int)height.Value;
            mazebound = new PictureBox[map.mapheight,map.mapwidth];
            getBlankMazeInterface();
        }

        public void getBlankMazeInterface()
        {
            for (int i = 0; i < map.mapheight; i++)
            {
                for (int j = 0; j < map.mapwidth; j++)
                {
                    mazebound[i, j] = new PictureBox();
                    int xPosition = (i * map.tilesize) + 10; //20 is padding from left
                    int yPosition = (j * map.tilesize) + 160; //60 is padding from top
                    mazebound[i, j].SetBounds(xPosition, yPosition, map.tilesize, map.tilesize);
                    mazebound[i, j].BackColor = map.convertSign(map.mazeDesign[i, j].sign).BackColor;
                    mazebound[i, j].BackgroundImage = map.convertSign(map.mazeDesign[i, j].sign).BackgroundImage;
                    EventHandler clickEvent = new EventHandler(PictureBox_Click);
                    mazebound[i, j].Click += clickEvent;
                    this.Controls.Add(mazebound[i, j]);
                }
            }

        }

        public void enablePawn(GroupBox grp, NumericUpDown numx, NumericUpDown numy, NumericUpDown w, NumericUpDown h)
        {
            grp.Visible = true;
            grp.Enabled = true;

            numx.Enabled = true;
            numx.Visible = true;
            numx.Maximum = w.Value - 2;
            numx.Minimum = 1;

            numy.Enabled = true;
            numy.Visible = true;
            numy.Maximum = h.Value - 2;
            numy.Minimum = 1;

        }

        public void disablePawn(GroupBox grp, NumericUpDown numx, NumericUpDown numy)
        {
            grp.Visible = false;
            grp.Enabled = false;

            numx.Enabled = false;
            numx.Visible = false;

            numy.Enabled = false;
            numy.Visible = false;
        }
        /*
        public void enablePawn()
        {
            groupBox2.Visible = true;
            groupBox2.Enabled = true;
            
            pawn_x.Enabled = true;
            pawn_x.Visible = true;
            pawn_x.Maximum = width.Value - 2;
            pawn_x.Minimum = 1;

            pawn_y.Enabled = true;
            pawn_y.Visible = true;
            pawn_y.Maximum = height.Value - 2;
            pawn_y.Minimum = 1;

        }

        public void disablePawn()
        {
            groupBox2.Visible = false;
            groupBox2.Enabled = false;

            pawn_x.Enabled = false;
            pawn_x.Visible = false;

            pawn_y.Enabled = false;
            pawn_y.Visible = false;
        }
        */

        //ubah warna background dan gambarnya untuk set picturebox
        private void PictureBox_Click(object sender, EventArgs e)
        {
            ((PictureBox)sender).BackColor = CurrentColor;
            ((PictureBox)sender).BackgroundImage = currentBackground;
            
        }

        //ubah background berdasarkan klik properti
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            currentBackground = pictureBox1.BackgroundImage;
            CurrentColor = pictureBox1.BackColor;
        }

        //ubah background berdasarkan klik properti
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            currentBackground = pictureBox2.BackgroundImage;
            CurrentColor = pictureBox2.BackColor;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            reset();
            this.Refresh();
            CreateBlankMaze();
            enablePawn(groupBox2,pawn_x,pawn_y,width,height);
            enablePawn(groupBox4,finish_x,finish_y,width,height);
        }

        private void reset()
        {
            for (int i = 0; i < width_temp; i++)
            {
                for (int j = 0; j < height_temp; j++)
                {
                    this.Controls.Remove(mazebound[i,j]);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AllocConsole();
        }
        
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
        
        private void button2_Click(object sender, EventArgs e)
        {
            pawn = letakkanPawn(pawn,pawn_x,pawn_y,'s');
        }

        private MainChar letakkanPawn(MainChar comp, NumericUpDown numx, NumericUpDown numy, char sign)
        {
            comp = hapusPawn(comp);
            comp = new MainChar();
            comp.posx = (int)numx.Value;
            comp.posy = (int)numy.Value;
            for (int i = 0; i < map.mapheight; i++)
            {
                for (int j = 0; j < map.mapwidth; j++)
                {
                    if (i == comp.posx && j == comp.posy)
                    {
                        mazebound[i, j].BackColor = Color.White;
                        if (sign == 's')
                        {
                            mazebound[i, j].BackgroundImage = Properties.Resources.Image1;
                            mazebound[i, j].BackColor = Color.Transparent;
                        }
                        else if (sign == 'f')
                        {
                            mazebound[i, j].BackgroundImage = Properties.Resources.Image31;
                            mazebound[i, j].BackColor = Color.Yellow;
                        }
                        
                    }
                }
            }
            return comp;
        }

        private MainChar hapusPawn(MainChar comp)
        {
            if (comp != null)
            {
                for (int i = 0; i < map.mapheight; i++)
                {
                    for (int j = 0; j < map.mapwidth; j++)
                    {
                        if (i == comp.posx && j == comp.posy)
                        {
                            mazebound[i, j].BackColor = Color.White;
                            mazebound[i, j].BackgroundImage = null;
                        }
                    }
                }
            }
            return comp;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            finish = letakkanPawn(finish, finish_x, finish_y,'f');
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pawn != null && finish != null)
            {
                map.setMazeDesign(mazebound);
                map.lookforchar();
                Node mulai = new Node();
                mulai.node = new Point(pawn.posx, pawn.posy);
                Node selesai = new Node();
                selesai.node = new Point(finish.posx, finish.posy);
                AStar a = new AStar(mulai, selesai);
                if (a.AlgoritmaAStar(map.mazeDesign, map.mapheight, map.mapwidth))
                {
                    MessageBox.Show("Mencari petunjuk....");
                    //drawNodePencarian(a);
                    a.testOutput();
                    drawNodeJalur(a);
                    MessageBox.Show("Alhamdulillah,... Akhirnya aku menemukan jalan keluar T-T!!!");
                    pawn = letakkanPawn(pawn, pawn_x, pawn_y, 's');
                    finish = letakkanPawn(finish, finish_x, finish_y, 'f');
                }
                else
                {
                    MessageBox.Show("Astagfirullah,... Yang buat maze kejam, aku gak dikasih jalan keluar T-T", "Peringatan!!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                MessageBox.Show("Hayoo, lupa input start sama finish ya?","Peringatan!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            
            //map.lookforchar();
        }

        private void drawNodeJalur(AStar a)
        {
            List<Node> jalurClose = a.jalurAStar();
            Node temp = new Node();
            temp = jalurClose[0];
            for (int i = 0; i < jalurClose.Count; i++)
            {
                mazebound[temp.node.X, temp.node.Y].BackgroundImage = null;
                mazebound[temp.node.X, temp.node.Y].BackColor = Color.Teal;
                Thread.Sleep(300);
                mazebound[jalurClose[i].node.X, jalurClose[i].node.Y].BackColor = Color.Transparent;
                mazebound[jalurClose[i].node.X, jalurClose[i].node.Y].BackgroundImage = Properties.Resources.Image1;
                temp = jalurClose[i];
                Refresh();
            }
            for (int i = 0; i < jalurClose.Count; i++)
            {
                mazebound[jalurClose[i].node.X, jalurClose[i].node.Y].BackColor = Color.White;
                mazebound[jalurClose[i].node.X, jalurClose[i].node.Y].BackgroundImage = null;
                temp = jalurClose[i];
                Refresh();
            }
        }

        private void drawNodePencarian(AStar a)
        {
            List<Node> jalurClose = a.listClose();
            for (int i = 0; i < jalurClose.Count; i++)
            {
                mazebound[jalurClose[i].node.X, jalurClose[i].node.Y].BackColor = Color.Aquamarine;
                Thread.Sleep(30);
                Refresh();
            }
            for (int i = 0; i < jalurClose.Count; i++)
            {
                mazebound[jalurClose[i].node.X, jalurClose[i].node.Y].BackColor = Color.White;
                Refresh();
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Map Files (.map)|*.map";
            saveFileDialog1.Title = "Save your map";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                map.pawn = this.pawn;
                map.finish = this.finish;
                map.setMazeDesign(mazebound);
                map.mapheight = height_temp;
                map.mapwidth = width_temp;
                savename.Text = saveFileDialog1.FileName;
                SaveandLoad sv = new SaveandLoad();
                sv.Serialize(map, saveFileDialog1.FileName);
            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            
            openFileDialog1.Filter = "Map Files (.map)|*.map";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Multiselect = false;

            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                loadname.Text = openFileDialog1.FileName;
                SaveandLoad sv = new SaveandLoad();
                map = sv.Deserialize(openFileDialog1.FileName);
                if (map != null)
                {
                    pawn_x.Value = map.pawn.posx;
                    pawn_y.Value = map.pawn.posy;
                    finish_x.Value = map.finish.posx;
                    finish_y.Value = map.finish.posy;
                    width.Value = map.mapwidth;
                    height.Value = map.mapheight;
                    pawn = map.pawn;
                    finish = map.finish;
                    
                    //mazebound = new PictureBox[map.mapheight, map.mapwidth];
                    this.pawn = map.pawn;
                    this.finish = map.finish;
                    reset();
                    this.Refresh();
                    mazebound = new PictureBox[map.mapheight, map.mapwidth];
                    getBlankMazeInterface();
                    enablePawn(groupBox2, pawn_x, pawn_y, width, height);
                    enablePawn(groupBox4, finish_x, finish_y, width, height);
                }
                else
                {
                    MessageBox.Show("Maaf, terjadi kesalahan!");
                }
            }
            Console.WriteLine(result); // <-- For debugging use.
        }
    }
}
