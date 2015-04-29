using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLostPawn
{
    [Serializable]
    class MainChar
    {
        public int posx { get; set; }
        public int posy { get; set; }
        public bool set_already{ get; set; }
        public string countCabang(TileBox[,] tb)
        {
            int sum = 0;
            string s = "";
            //cek atas...
            if (tb[posx - 1, posy].sign == '0') 
            {
                sum = sum + 1;
                s = s + "atas kosong \n";
            }
            //cek bawah...
            if (tb[posx + 1, posy].sign == '0')
            {
                sum = sum + 1;
                s = s + "bawah kosong \n";
            }
            //cek kiri...
            if (tb[posx, posy - 1].sign == '0')
            {
                sum = sum + 1;
                s = s + "kiri kosong \n";
            }
            //cek kanan...
            if (tb[posx, posy + 1].sign == '0')
            {
                sum = sum + 1;
                s = s + "kanan kosong \n";
            }
            //cek kanan atas...
            if (tb[posx + 1, posy - 1].sign == '0')
            {
                sum = sum + 1;
                s = s + "kanan atas kosong \n";
            }
            //cek kanan bawah...
            if (tb[posx + 1, posy + 1].sign == '0')
            {
                sum = sum + 1;
                s = s + "kanan bawah kosong \n";
            }
            //cek kiri bawah...
            if (tb[posx - 1, posy + 1].sign == '0')
            {
                sum = sum + 1;
                s = s + "kiri bawah kosong \n";
            }
            //cek kiri atas...
            if (tb[posx - 1, posy - 1].sign == '0')
            {
                sum = sum + 1;
                s = s + "kiri atas kosong \n";
            }
            return s;
        }

        public bool solveMaze(string arah, int cabang)
        {
            return true;
        }
    }
}
