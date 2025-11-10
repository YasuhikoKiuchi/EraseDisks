namespace EraseDisks
{
    public partial class Form1 : Form
    {
        private static Random random = new Random(); // 乱数生成用

        private List<Disk> list = new List<Disk>(); // 円盤のリスト

        private int r = 32; // 円盤の半径

        private int numberOfDisks = 10; // 円盤の数

        public Form1()
        {
            InitializeComponent();

            // ===
            //FormBorderStyle = FormBorderStyle.None;
            //SetBounds(0, 0, 1200, 600, BoundsSpecified.Size);
            //numberOfDisks = 50;
            //BackColor = Color.DarkBlue;
            // ===

            timTimer.Interval = 1000; // 【後編】追加
            timTimer.Tick += timTime_Tick; // 【後編】追加

            StartNewGame(); // 新規ゲーム開始
        }

        #region アイキャッチ画像用

        public static (int R, int G, int B) GeneratePastelColor()
        {
            int r = random.Next(200, 256);
            int g = random.Next(200, 256);
            int b = random.Next(200, 256);

            return (r, g, b);
        }

        public static string RGBToHex(int r, int g, int b)
        {
            return $"#{r:X2}{g:X2}{b:X2}";
        }

        #endregion

        public void StartNewGame() // 新規ゲーム開始
        {
            list.Clear(); // リストクリア

            for (int i = 0; i < numberOfDisks; i++) // 決められた数だけ円盤を生成してリストに追加する
            {
                int x = random.Next(r, ClientSize.Width - r);
                int y = random.Next(r, ClientSize.Height - r);

                list.Add(new Disk(i + 1, x, y, r));
            }
            Refresh(); // 画面を再描画する

            currentNo = 1; // ★追加

            elapsed = 0; // 【後編】追加
            //timTimer.Start(); // 【後編】追加
        }

        private void Form1_Paint(object sender, PaintEventArgs e) // 描画処理
        {
            foreach (Disk d in list) // 円盤の数だけ繰り返す
            {
                if (!d.Erased) // 未消去のものであれば
                {
                    //// アイキャッチ画像用
                    //int r, g, b;
                    //(r, g, b) = GeneratePastelColor();
                    //Color color = Color.FromArgb(r, g, b);
                    //Brush br = new SolidBrush(color);
                    //string n = d.N.ToString(); // 番号を文字列化する
                    //e.Graphics.FillEllipse(br, d.X - d.R, d.Y - d.R, d.R * 2, d.R * 2); // 円盤を描く
                    //e.Graphics.DrawEllipse(Pens.Black, d.X - d.R, d.Y - d.R, d.R * 2, d.R * 2); // 円盤を描く
                    //e.Graphics.DrawString(n, new Font("MS ゴシック", 12), Brushes.Black, d.X - 16 - ((n.Length - 1) * 4) + 8, d.Y - 12 + 2); // 番号を描く

                    string n = d.N.ToString(); // 番号を文字列化する
                    e.Graphics.FillEllipse(Brushes.Aqua, d.X - d.R, d.Y - d.R, d.R * 2, d.R * 2); // 円盤を描く
                    e.Graphics.DrawString(n, new Font("MS ゴシック", 16), Brushes.Blue, d.X - 10 - ((n.Length - 1) * 8), d.Y - 12); // 番号を描く
                }
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e) // フォームマウスクリック時処理
        {
            int erasedCount = 0; // 消去カウンタ初期化
            int totalErasedCount = 0; // トータル消去カウンタ初期化

            foreach (Disk d in list) // 円盤の数だけ繰り返す
            {
                if (d.N == currentNo) // ★追加
                {
                    int xx = d.X - e.X;
                    int yy = d.Y - e.Y;
                    double r0 = Math.Sqrt((xx * xx) + (yy * yy));
                    if (r0 <= r) // クリック位置が円盤の中であれば
                    {
                        d.Erased = true; // 該当する円盤の消去フラグを立てる
                        erasedCount++; // 消去カウンタを1増やす
                        currentNo++; // 現在番号を1増やす // ★追加
                    }
                } // 閉じカッコ

                if (d.Erased) // 円盤が消去済みであれば
                {
                    totalErasedCount++; // トータル消去カウンタを1増やす
                }
            }

            if (erasedCount >= 1) // 消去カウンタが1以上であれば
            {
                Refresh(); // 画面再描画
            }

            if (totalErasedCount == list.Count) // 円盤が全部消去されたら
            {
                MessageBox.Show(this, "Clear!"); // Clear!とメッセージボックスで表示
                DoLevelUp();
                StartNewGame(); // 新規ゲーム開始
            }
        }

        int currentNo = 0; // 現在番号(=次に消去すべき円盤の番号)

        // ============================================================ 後編

        int level = 1; // 現在レべル

        private void DoLevelUp() // レベルアップする
        {
            r -= 7; // 半径を7減らす
            if (r <= 0) r = 32; // 半径が0またはマイナスになってしまったら、32に戻す
            level++; // レベルの値を1増やす
            label1.Text = "レベル: " + level.ToString(); // レベルの値を表示する
        }

        private System.Windows.Forms.Timer timTimer = new System.Windows.Forms.Timer(); // タイマー

        private int elapsed = 0; // 経過時間

        private void timTime_Tick(object? sender, EventArgs e) // タイマーTICK時処理
        {
            elapsed++; // 経過時間を1増やす
            label2.Text = "時間: " + elapsed.ToString(); // 経過時間を表示する
        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            timTimer.Start();
        }
    }
}
