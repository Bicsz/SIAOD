using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SIAOD_Labs
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class Lab1 : Page
    {
        Canvas t = new Canvas();
        List<Note> Notes;
        public static Random random = new Random();
        int znachy;
        int WidthHeight;
        int gridCount = -1;
        public Lab1()
        {
            InitializeComponent();




            t.Style = this.Resources["Convasek"] as Style;
            Content.Children.Add(t);
        }
    
        private void button_Click(object sender, RoutedEventArgs e)
        {
            Notes = new List<Note>();
            gridCount = -1;
            t.Children.Clear();
            var znachx = Int32.Parse(textBox.Text) - 1;
            znachy = Int32.Parse(textBox_Copy.Text) - 1;

            WidthHeight = 0;
            if (znachx < znachy)
                WidthHeight = (int)(380 / znachy);
            else
                WidthHeight = (int)(380 / znachx);

            int[] x = new int[znachx + 1];
            int[] y = new int[znachy + 1];

            for (var i = 0; i <= znachx; i++)
            {
                var MyLine = new Line();

                MyLine.X1 = 200 - WidthHeight * znachx / 2 + i * WidthHeight;
                MyLine.Y1 = 200 - WidthHeight * znachy / 2;
                x[i] = (int)MyLine.X1;
                gridCount++;

                MyLine.X2 = 200 - WidthHeight * znachx / 2 + i * WidthHeight;
                MyLine.Y2 = 200 + WidthHeight * znachy / 2;

                MyLine.Stroke = System.Windows.Media.Brushes.White;
                MyLine.StrokeThickness = 1;
                MyLine.HorizontalAlignment = HorizontalAlignment.Left;
                MyLine.VerticalAlignment = VerticalAlignment.Center;
                t.Children.Add(MyLine);

            }
            for (var i = 0; i <= znachy; i++)
            {
                var MyLine = new Line();

                MyLine.X1 = 200 - WidthHeight * znachx / 2;
                MyLine.Y1 = 200 - WidthHeight * znachy / 2 + i * WidthHeight;
                y[i] = (int)MyLine.Y1;

                gridCount++;
                MyLine.X2 = 200 + WidthHeight * znachx / 2;
                MyLine.Y2 = 200 - WidthHeight * znachy / 2 + i * WidthHeight;

                MyLine.Stroke = System.Windows.Media.Brushes.White;
                MyLine.StrokeThickness = 1;
                MyLine.HorizontalAlignment = HorizontalAlignment.Left;
                MyLine.VerticalAlignment = VerticalAlignment.Center;
                t.Children.Add(MyLine);
            }

            // var count = 0;
            for (var i = 0; i <= znachx; i++)
                for (var j = 0; j <= znachy; j++)
                {

                    Notes.Add(new Note(i, j, x[i], y[j]));
                    //if (Notes[i].i == 1 && Notes[i].j == 1)
                    // MessageBox.Show(count+" ----- "+i + "/"+j + "- Notes[i].i = " + Notes[count].i + " Notes[i].j = " + Notes[count].j + " Notes[i].x = " + Notes[count].x + 
                    // " Notes[i].y = " + Notes[count].y);
                    //count++;
                }

            //MessageBox.Show("");
            //count = 0;
            for (var i = 0; i < Notes.Count; i++)
            {
                //MessageBox.Show(Notes[i].x + " xy "+ Notes[i].y+"   "+ Notes[i].i+"-"+Notes[i].j);
                //count = 0;
                //if (Notes[i].i == 1 && Notes[i].j == 1)
                //MessageBox.Show(i + "Notes[i].i = "+ Notes[i].i + " Notes[i].j = " + Notes[i].j + " Notes[i].x = " + Notes[i].x + " Notes[i].y = " + Notes[i].y);

                if (Notes[i].i != 0)
                {
                    Notes[i].Near.Add(Notes[i - znachy - 1]);
                    //count++;
                }
                if (Notes[i].i != znachx)
                {
                    Notes[i].Near.Add(Notes[i + znachy + 1]);
                    //count++;
                }

                if (Notes[i].j != 0)
                {
                    Notes[i].Near.Add(Notes[i - 1]);
                    //count++;
                }
                if (Notes[i].j != znachy)
                {
                    Notes[i].Near.Add(Notes[i + 1]);
                    //count++;
                }

                //for(var t =0;t<count;t++)
                //MessageBox.Show(Notes[i].Near[t].x + " "+Notes[i].Near[t].y);
            }
            //MessageBox.Show(Notes[6].i+"  "+ Notes[6].j);
            //MessageBox.Show(Notes[6].x+" "+ Notes[6].y + "      " + Notes[6].Near[2].x+" " + Notes[6].Near[2].y);
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            var ist = Int32.Parse(textBox_Copy1.Text);
            var jst = Int32.Parse(textBox_Copy2.Text);

            var ien = Int32.Parse(textBox_Copy3.Text);
            var jen = Int32.Parse(textBox_Copy4.Text);

            var wat = -1;//kakoi
            for (var i = 0; i < Notes.Count; i++)
                if (Notes[i].i == ist && Notes[i].j == jst)
                {
                    wat = i;
                    DrawNote(Notes[wat].x, Notes[wat].y, 0,t,Notes.Count-1);
                    break;
                }


            var drawX1 = 0; var drawX2 = 0;
            var drawY1 = 0; var drawY2 = 0;
            while (ist != ien || jst != jen)
            {

                if (ist < ien)
                {
                    drawX1 = Notes[wat].x;
                    drawY1 = Notes[wat].y;
                    wat += znachy + 1;
                    ist++;

                    drawX2 = Notes[wat].x;
                    drawY2 = Notes[wat].y;
                    DrawLine(drawX1, drawY1, drawX2, drawY2,t);
                    //MessageBox.Show(ist + " " + jst + "    " + ien + " " + jen);
                }
                if (jst < jen)
                {

                    drawX1 = Notes[wat].x;
                    drawY1 = Notes[wat].y;
                    wat++;
                    drawX2 = Notes[wat].x;
                    drawY2 = Notes[wat].y;
                    jst++;
                    DrawLine(drawX1, drawY1, drawX2, drawY2,t);
                    //MessageBox.Show(ist + " " + jst + "    " + ien + " " + jen);
                }




                if (jst > jen)
                {

                    drawX1 = Notes[wat].x;
                    drawY1 = Notes[wat].y;
                    wat--;
                    drawX2 = Notes[wat].x;
                    drawY2 = Notes[wat].y;
                    jst--;
                    DrawLine(drawX1, drawY1, drawX2, drawY2,t);
                    //MessageBox.Show(ist + " " + jst + "    " + ien + " " + jen);
                }
                if (ist > ien)
                {
                    drawX1 = Notes[wat].x;
                    drawY1 = Notes[wat].y;
                    wat -= znachy + 1;
                    ist--;

                    drawX2 = Notes[wat].x;
                    drawY2 = Notes[wat].y;
                    DrawLine(drawX1, drawY1, drawX2, drawY2,t);
                    //MessageBox.Show(ist + " " + jst + "    " + ien + " " + jen);
                }

            }
            DrawNote(Notes[wat].x, Notes[wat].y, 1,t,Notes.Count-1);
        }
       
        public static void DrawNote(int x1, int y1, short type, Canvas tt, int NoteId)
        {
            var MyLine = new Rectangle();

            MyLine.Margin = new Thickness(x1 - 5, y1 - 5, 0, 0);

            MyLine.Height = 10;
            MyLine.Width = 10;
            
            switch (type)
            {
                case 0: MyLine.Fill = Brushes.Red;  break;
                case 1: MyLine.Fill = Brushes.Blue; break;
                case 2: MyLine.Fill = Brushes.Gray; break;
          
            }

            MyLine.Opacity = 0.5;
            
            MyLine.Name = "I" + NoteId;
            

            MyLine.MouseLeftButtonDown += MIClick;

            MyLine.HorizontalAlignment = HorizontalAlignment.Left;
            MyLine.VerticalAlignment = VerticalAlignment.Top;

            tt.Children.Add(MyLine);
            Canvas.SetZIndex(MyLine,10);
        }
        public static Rectangle DrawNote(int x1, int y1, Color color, Canvas tt)
        {
          
            var MyLine = new Rectangle();

            MyLine.Margin = new Thickness(x1 - 5, y1 - 5, 0, 0);

            MyLine.Height = 10;
            MyLine.Width = 10;
          
            var fcolor = new SolidColorBrush(color);
           
            MyLine.Fill = fcolor;

            MyLine.HorizontalAlignment = HorizontalAlignment.Left;
            MyLine.VerticalAlignment = VerticalAlignment.Top;

            tt.Children.Add(MyLine);
            return MyLine;
        }
        public static void MIClick(object sender, RoutedEventArgs e)
        {
            var Mi = (Rectangle)sender;
            var MyNote = new Note();
            var id = Int32.Parse(Mi.Name.Remove(0, 1));
            MyNote = Lab3.Notes[id];
            var txt = "Узел " + id + "\n i = " + MyNote.i + "\n j = " + MyNote.j + "\n near = ";
            for (var i = 0; i<=MyNote.Near.Count-1;i++)
                txt += "|" + MyNote.Near[i].i + " " + MyNote.Near[i].j + "|";
            txt += "\nc = " + MyNote.c;
            txt += "\n\n";
            if (MyNote.Owner != null)
            {
                txt += "  owner = " + MyNote.Owner.Iam.Fill.ToString();
                txt += "\n  owner loose = " + MyNote.Owner.loose.ToString();
                txt += "\n  owner Next = " + MyNote.Owner.Next.i + " " + MyNote.Owner.Next.j;
                txt +="\n  owner Cerrent = "+MyNote.Owner.Current.i+" "+MyNote.Owner.Current.j;
            }
            else
                txt += "owner = null";
            MessageBox.Show(txt,"Инфо узла");
            //туть
        }
        public static void DrawLine(int x1, int y1, int x2, int y2,Canvas tt,bool type)
        {
            var MyLine = new Line();

            MyLine.X1 = x1;
            MyLine.Y1 = y1;

            MyLine.X2 = x2;
            MyLine.Y2 = y2;

            if(type)
                MyLine.Stroke = System.Windows.Media.Brushes.Azure;
            else
                MyLine.Stroke = System.Windows.Media.Brushes.Chocolate;
            MyLine.StrokeThickness = 5;
            MyLine.HorizontalAlignment = HorizontalAlignment.Left;
            MyLine.VerticalAlignment = VerticalAlignment.Top;
            tt.Children.Add(MyLine);
        }
        public static void DrawLine(int x1, int y1, int x2, int y2, Canvas tt)
        {
            var MyLine = new Line();

            MyLine.X1 = x1;
            MyLine.Y1 = y1;

            MyLine.X2 = x2;
            MyLine.Y2 = y2;

                MyLine.Stroke = System.Windows.Media.Brushes.Black;
            MyLine.StrokeThickness = 5;
            MyLine.HorizontalAlignment = HorizontalAlignment.Left;
            MyLine.VerticalAlignment = VerticalAlignment.Top;
            tt.Children.Add(MyLine);
        }
        void DrawLine(int x1, int y1, int x2, int y2, int count, bool Hor)
        {
            var MyLine = new Line();

            MyLine.X1 = x1;
            MyLine.Y1 = y1;

            MyLine.X2 = x2;
            MyLine.Y2 = y2;

            MyLine.Stroke = System.Windows.Media.Brushes.Black;
            MyLine.StrokeThickness = 5;
            MyLine.HorizontalAlignment = HorizontalAlignment.Left;
            MyLine.VerticalAlignment = VerticalAlignment.Top;



            //anim(MyLine);

            var animation = new DoubleAnimation();
            animation.From = 0;

            animation.Duration = TimeSpan.FromSeconds(1.5);

            animation.To = 400;
            animation.BeginTime = TimeSpan.FromSeconds(count / 1.9);
            t.Children.Add(MyLine);
            //MessageBox.Show("");
            if (Hor)
            {
                MyLine.Width = 0;
                ((Line)t.Children[gridCount + 2 + count]).BeginAnimation(WidthProperty, animation);
            }
            else
            {
                MyLine.Height = 0;
                ((Line)t.Children[gridCount + 2 + count]).BeginAnimation(HeightProperty, animation);
            }




        }

        private void button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            var count = 0;
            var ist = Int32.Parse(textBox_Copy1.Text);
            var jst = Int32.Parse(textBox_Copy2.Text);

            var ien = Int32.Parse(textBox_Copy3.Text);
            var jen = Int32.Parse(textBox_Copy4.Text);

            var wat = -1;//kakoi
            for (var i = 0; i < Notes.Count; i++)
                if (Notes[i].i == ist && Notes[i].j == jst)
                {
                    wat = i;
                    DrawNote(Notes[wat].x, Notes[wat].y, 0,t, Notes.Count-1);
                    break;
                }


            var drawX1 = 0; var drawX2 = 0;
            var drawY1 = 0; var drawY2 = 0;
            var shag = false;
            while (ist != ien || jst != jen)
            {
                //MessageBox.Show("+");
                shag = false;
                if (ist < ien && !shag && (ien - ist) >= (jen - jst))
                {
                    //MessageBox.Show("1");
                    shag = true;
                    drawX1 = Notes[wat].x;
                    drawY1 = Notes[wat].y;
                    wat += znachy + 1;
                    ist++;

                    drawX2 = Notes[wat].x;
                    drawY2 = Notes[wat].y;
                    DrawLine(drawX1, drawY1, drawX2, drawY2, count, true);
                    count++;
                    // MessageBox.Show(ist + " " + jst + "    " + ien + " " + jen);
                }

                if (jst < jen && !shag && (ien - ist) < (jen - jst))
                {
                    //MessageBox.Show("2");
                    shag = true;
                    drawX1 = Notes[wat].x;
                    drawY1 = Notes[wat].y;
                    wat++;
                    drawX2 = Notes[wat].x;
                    drawY2 = Notes[wat].y;
                    jst++;
                    DrawLine(drawX1, drawY1, drawX2, drawY2, count, false);
                    count++;
                    // MessageBox.Show(ist + " " + jst + "    " + ien + " " + jen);
                }


                if (jst > jen && !shag && (ist - ien) < (jst - jen))
                {
                    //MessageBox.Show("3");
                    shag = true;
                    drawX1 = Notes[wat].x;
                    drawY1 = Notes[wat].y;
                    wat--;
                    drawX2 = Notes[wat].x;
                    drawY2 = Notes[wat].y;
                    jst--;
                    DrawLine(drawX1, drawY1, drawX2, drawY2, count, false);
                    count++;
                    //MessageBox.Show(ist + " " + jst + "    " + ien + " " + jen);
                }

                if (ist > ien && !shag && (ist - ien) > (jst - jen))
                {
                    //MessageBox.Show("4");
                    shag = true;
                    drawX1 = Notes[wat].x;
                    drawY1 = Notes[wat].y;
                    wat -= znachy + 1;
                    ist--;

                    drawX2 = Notes[wat].x;
                    drawY2 = Notes[wat].y;
                    DrawLine(drawX1, drawY1, drawX2, drawY2, count, true);
                    count++;
                    // MessageBox.Show(ist + " " + jst + "    " + ien + " " + jen);
                }

            }
            DrawNote(Notes[wat].x, Notes[wat].y, 1,t,Notes.Count-1);

        }
    }
    public class Note
    {
        public int x, y, i, j, c;
        public Particle Owner = null;
        public Note(int i1, int j1, int x1, int y1)
        {
            this.i = i1;
            this.j = j1;
            this.x = x1;
            this.y = y1;
            this.c = 0;

        }

        public Note()
        {
        }

        public List<Note> Near = new List<Note>();

    }
}
