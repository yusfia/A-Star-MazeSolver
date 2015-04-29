using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheLostPawn
{
    [Serializable]
    class Map
    {
        public int mapheight;
        public int mapwidth;
        public MainChar finish { get; set; }
        public MainChar pawn { get; set; }
        public int tilesize;
        public TileBox[,] mazeDesign;
        
        public Map(int x, int y, int tilesz)
        {
            this.mapheight = x;
            this.mapwidth = y;
            this.tilesize = tilesz;
            CreateLogicBlankMaze();
        }

        public void setMazeDesign(PictureBox[,] pic){
            //mazeDesign = new TileBox[mapheight, mapwidth];
            TileBox[,] tile = new TileBox[mapheight, mapwidth];
            for (int i = 0; i < mapheight; i++)
            {
                for (int j = 0; j < mapwidth; j++)
                {
                    TileBox tl = new TileBox();
                    if (pic[i, j].BackColor == Color.SteelBlue)
                    {
                        tl.sign = 'x';
                        tile[i, j] = tl;
                        /*
                        mazeDesign[i, j].sign = 'x';
                        mazeDesign[i, j].tampilan = pic[i, j];
                        */
                    }
                    if (pic[i, j].BackColor == Color.Transparent)
                    {
                        tl.sign = 's';
                        tile[i, j] = tl;
                        /*
                        mazeDesign[i, j].sign = 's';
                        mazeDesign[i, j].tampilan = pic[i, j];
                        */
                    }
                    if (pic[i, j].BackColor == Color.Yellow)
                    {
                        tl.sign = 'f';
                        tile[i, j] = tl;
                        /*
                        mazeDesign[i, j].sign = 'f';
                        mazeDesign[i, j].tampilan = pic[i, j];
                         */
                    }
                    if (pic[i, j].BackColor == Color.White)
                    {
                        tl.sign = '0';
                        //tl.tampilan = pic[i, j];
                        tile[i, j] = tl;
                        /*
                        mazeDesign[i, j].sign = 'x';
                        mazeDesign[i, j].tampilan = pic[i, j];
                        */
                    }
                    mazeDesign = tile;
                }
            }
        }
        
        public void lookforchar()
        {
            string s = "";
            for (int i = 0; i < mapheight; i++)
            {
                for (int j = 0; j < mapwidth; j++)
                {
                    Console.Out.Write(mazeDesign[i, j].sign);
                    s = s + (mazeDesign[i,j].sign);
                }
                s = s+"\n";
                Console.Out.WriteLine();
            }
        }
        
        public void CreateLogicBlankMaze()
        {
            mazeDesign = new TileBox[mapheight, mapwidth];
            for (int i = 0; i < mapheight; i++)
            {
                for (int j = 0; j < mapwidth; j++)
                {
                    TileBox tl = new TileBox();
                    if ((i == 0) || j==0 || (i == mapheight-1) || (j == mapwidth-1))
                    {
                        tl.sign = 'x';
                        mazeDesign[i, j]= tl;
                    }
                    else
                    {
                        tl.sign = '0';
                        mazeDesign[i, j] = tl;
                    }
                }
            }
        }
  
        public PictureBox convertSign(char input)
        {
            PictureBox temp = new PictureBox();
            if (input == 'x')
            {
                temp.BackColor = Color.SteelBlue;
                temp.BackgroundImage = Properties.Resources.Image2;
            }
            else if(input == 's')
            {
                temp.BackColor = Color.Transparent;
                temp.BackgroundImage = Properties.Resources.Image1;
            }
            else if (input == 'f')
            {
                temp.BackColor = Color.Yellow;
                temp.BackgroundImage = Properties.Resources.Image31;
            }
            else if (input == '0')
            {
                temp.BackColor = Color.White;
                temp.BackgroundImage = null;
            }
            else if (input == '1')
            {
                temp.BackColor = Color.Silver;
                temp.BackgroundImage = null;
            }
            return temp;
        }
    }
}
