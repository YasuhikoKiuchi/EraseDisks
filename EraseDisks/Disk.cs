namespace EraseDisks
{
    internal class Disk // 円盤の情報
    {
        public int N { get; set; } // 番号

        public int X { get; set; } // X座標

        public int Y { get; set; } // Y座標

        public int R { get; set; } // 半径

        public bool Erased { get; set; } = false; // 消去されたか否か

        internal Disk(int n, int x, int y, int r) // コンストラクタ
        {
            N = n;
            X = x;
            Y = y;
            R = r;
        }
    }
}
