using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace SIAOD_Labs
{
    /// <summary>
    /// Логика взаимодействия для Lab2.xaml
    /// </summary>
    public partial class Lab2 : Page
    {
        Canvas t = new Canvas();
        List<Note> Notes;

        int znachy; int znachx;
        int WidthHeight;
        int gridCount = -1;
        public Lab2()
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
            znachx = Int32.Parse(textBox.Text) - 1;
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
            DoItAsync();
        }


        void shag(int wat1IN,int ij1IN,int watDO,int ijDO, out int wat1, out int ij1,short NoteType)
        {
            var drawX1 = Notes[wat1IN].x;
            var drawY1 = Notes[wat1IN].y;
            ij1IN+= ijDO; ij1 = ij1IN;
            wat1IN+= watDO; wat1 = wat1IN;
            if(NoteType==0)
                Lab1.DrawLine(drawX1, drawY1, Notes[wat1].x, Notes[wat1].y, t,false);
            else
                Lab1.DrawLine(drawX1, drawY1, Notes[wat1].x, Notes[wat1].y, t, true);
            Lab1.DrawNote(Notes[wat1].x, Notes[wat1].y, NoteType, t,Notes.Count-1);
        }

        async Task DoItAsync()
        {
            var i1 = Int32.Parse(textBox_Copy1.Text);
            var j1 = 0;
            var i1Master = i1;

            var i2 = 0;
            var j2 = Int32.Parse(textBox_Copy3.Text);
            var j2Master = j2;


            var wat1 = -1;//kakoi
            var wat2 = -1;//kakoi


            for (var i = 0; i < Notes.Count; i++)
                if (Notes[i].i == i1 && Notes[i].j == j1)
                {
                    wat1 = i;
                    Lab1.DrawNote(Notes[wat1].x, Notes[wat1].y, 0, t,Notes.Count-1);
                }
                else
                    if (Notes[i].i == i2 && Notes[i].j == j2)
                {
                    wat2 = i;
                    Lab1.DrawNote(Notes[wat2].x, Notes[wat2].y, 1, t,Notes.Count-1);
                }

            
            Lab1.DrawLine(0, 0, 0, 0, t);
            Lab1.DrawLine(0, 0, 0, 0, t);
            var rnd = new Random();
            var probility = 1000;
            while (j1 < znachy || i2 < znachx)
            {
                

                if (j1 < znachy)
                {
                    for (var i = 1; i <= 5; i++)
                        if (t.Children[t.Children.Count - i].GetType() == typeof(Rectangle))
                            if (((Rectangle)t.Children[t.Children.Count - i]).Fill == System.Windows.Media.Brushes.Red)
                            {
                                t.Children.RemoveAt(t.Children.Count - i);
                                break;
                            }
                    if (i1Master == i1)
                    {
                       
                        if (j1 + 1 != j2 || i1 != i2+1)
                            shag(wat1, j1, 1, 1, out wat1, out j1,0);
                        else
                        {
                            rnd = new Random();
                            probility = rnd.Next(-100, 101);
                            //MessageBox.Show("j1 + 1 != j2 >" + (j1 + 1) + " " + j2);
                            //MessageBox.Show("i1 != i2+1 >" + i1 + " " + (i2 + 1));
                            //MessageBox.Show(probility+ " - probility");
                            if (probility < 0)//меняет свое направление иначе другого
                            {
                                if (i1 != 0)
                                    shag(wat1, i1, -(znachy + 1), -1, out wat1, out i1, 0);
                            }

                        }
                    }
                    else
                    {
                        if (probility != 1000)
                        {
                            probility = 1000;
                            if (j1 + 1 != znachy)
                                shag(wat1, j1, 1, 1, out wat1, out j1, 0);
                            else
                                shag(wat1, i1, znachy + 1, 1, out wat1, out i1, 0);
                        }
                        else
                            shag(wat1, i1, znachy + 1, 1, out wat1, out i1, 0);
                    }
                }
                if (i2 < znachx)
                {
                    for (var i = 1; i <= 5; i++)
                        if (t.Children[t.Children.Count - i].GetType() == typeof(Rectangle))
                            if (((Rectangle)t.Children[t.Children.Count - i]).Fill == System.Windows.Media.Brushes.Blue)
                            {
                                t.Children.RemoveAt(t.Children.Count - i);
                                break;
                            }
                    //if (i2 + 1 != i1 && j2 != j1++)
                    if (j2Master == j2)
                    {
                        if (probility <= 0 || probility == 1000)
                            shag(wat2, i2, znachy + 1, 1, out wat2, out i2, 1);
                        else
                            shag(wat2, j2, -1, -1, out wat2, out j2, 1);
                    }
                    else
                    {
                        if (probility > 0 && probility != 1000)
                        {
                            if(i2 +1 != znachx)
                                shag(wat2, i2, znachy + 1, 1, out wat2, out i2, 1);
                            else
                                shag(wat2, j2, +1, +1, out wat2, out j2, 1);
                            probility = 1000;
                        }
                        else
                            shag(wat2, j2, +1, +1, out wat2, out j2, 1);
                    }
                }
                await Task.Delay(1000- (int)slider.Value);



            }

        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void checkBox_Click(object sender, RoutedEventArgs e)
        {
            if((bool)checkBox.IsChecked)
            {
                slider.Value = 990;
                slider.IsEnabled = false;
            }
            else
            {
                slider.IsEnabled = true;
            }
        }
    }
}
