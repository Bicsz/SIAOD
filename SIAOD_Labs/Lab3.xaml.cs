using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SIAOD_Labs
{
    /// <summary>
    /// Логика взаимодействия для Lab3.xaml
    /// </summary>
    public partial class Lab3 : Page
    {
        Canvas t = new Canvas();
        int countOfProblems = 0;
        public static List<Note> Notes;
        public static List<Particle> Particles;
        List<Note> NotesNext;
        List<List<Particle>> ParticlesConc;
        List<int> notesNextCount;
        
        int countCONST = 0;
        int iteration = 0;
        int GlobOKiterations = 0;
        int znachi=0;
        int znachj = 0;
        string path,path2,path3;
        int WidthHeight = 0;
        public Lab3()
        {
            InitializeComponent();
            t.Style = this.Resources["Convasek"] as Style;
            Content.Children.Add(t);
           
            
            //anim.Duration = TimeSpan.FromSeconds(0.3);

           
            
            path = Directory.GetCurrentDirectory() + @"\test.txt";
            if (!File.Exists(path))
            {  


                var textFile = new StreamWriter(path);
                textFile.WriteLine("i,j,i2,j2,Задержка");
                textFile.Close();

            }
            path2 = Directory.GetCurrentDirectory() + @"\gridRules.txt";
            if (!File.Exists(path))
            {

                var textFile = new StreamWriter(path);
                textFile.WriteLine("Указание, меж какими точками не будет связи");
                textFile.Close();

            }
            path3 = Directory.GetCurrentDirectory() + @"\Log.txt";
            if (!File.Exists(path3))
            {

                var textFile = new StreamWriter(path3);
                
                textFile.Close();

            }
        }

        private async  void button_Click(object sender, RoutedEventArgs e)
        {
            iteration = 0;
            countOfProblems = 0;
            way1.Items.Clear();
            way1.IsEnabled = false;
           
            var HaveNotNear = new List<Note>();
            var line = "";
            var file = new StreamReader(path2);
            var counter = false;
            var i1 = 0; var j1 = 0; var i2 = 0; var j2 = 0;
            while ((line = file.ReadLine()) != null)
            {
                if (counter)
                {
                    i1 = Int32.Parse(line.Substring(0, line.IndexOf(',')));
                    line = line.Remove(0, line.IndexOf(',') + 1);
                    j1 = Int32.Parse(line.Substring(0, line.IndexOf(',')));
                    line = line.Remove(0, line.IndexOf(',') + 1);
                    i2 = Int32.Parse(line.Substring(0, line.IndexOf(',')));
                    line = line.Remove(0, line.IndexOf(',') + 1);
                    j2 = Int32.Parse(line);

                    HaveNotNear.Add(new Note(i1, j1, 0, 0));

                    HaveNotNear.Add(new Note(i2, j2, 0, 0));
                    


                }
                else
                    counter = true;
            }
            file.Close();
            Notes = new List<Note>();
            Particles = new List<Particle>();

            t.Children.Clear();
            if (textBox.Text.Length > 0)
            {
                znachj = Int32.Parse(textBox.Text) - 1;
                znachi = Int32.Parse(textBox_Copy.Text) - 1;
            }
            else
            {
                znachi = 5; textBox.Text = "6";
                 znachj = 5; textBox_Copy.Text = "6";
            }
            WidthHeight = 0;
            if (znachi < znachj)
                WidthHeight = (int)(380 / znachj);
            else
                WidthHeight = (int)(380 / znachi);

            int[] xi = new int[znachi + 1];
            int[] yj = new int[znachj + 1];
            var mast = true;
            for (var i = 0; i <= znachi; i++)
            {
                for (var j = 0; j <= znachj; j++)
                {
                    try
                    {


                        xi[i] = 200 - WidthHeight * znachj / 2 + j * WidthHeight;
                        yj[j] = 200 - WidthHeight * znachi / 2 + i * WidthHeight;
                       

                        Notes.Add(new Note(i, j, xi[i], yj[j]));
                        Lab1.DrawNote(xi[i], yj[j], 2, t,Notes.Count-1);
                        //  MessageBox.Show(Notes[Notes.Count - 1].i+" "+ Notes[Notes.Count - 1].j);
                        mast = true;
                        if (j > 0)
                        {
                            for(var u =0; u<HaveNotNear.Count-1;u+=2)
                            {   if ((Notes[Notes.Count - 1].i == HaveNotNear[u].i && Notes[Notes.Count - 1].j == HaveNotNear[u].j)
                                    || Notes[Notes.Count -1].i == HaveNotNear[u+1].i && Notes[Notes.Count - 1].j == HaveNotNear[u+1].j)
                                {

                                   // MessageBox.Show("1");
                                    if (!((Notes[Notes.Count - 2].i != HaveNotNear[u+1].i || Notes[Notes.Count - 2].j != HaveNotNear[u+1].j)
                                        && (Notes[Notes.Count - 2].i != HaveNotNear[u].i || Notes[Notes.Count - 2].j != HaveNotNear[u].j)))
                                        mast = false;
                                }
                            /*
                                else
                                {
                                    
                                    Notes[Notes.Count - 1].Near.Add(Notes[Notes.Count - 2]);
                                    Notes[Notes.Count - 2].Near.Add(Notes[Notes.Count - 1]);
                                    DrawSetkLine(xi[i], yj[j], xi[i] - WidthHeight, yj[j]);
                                    
                                }
                                */
                            }
                            if(mast)
                            {
                                Notes[Notes.Count - 1].Near.Add(Notes[Notes.Count - 2]);
                                Notes[Notes.Count - 2].Near.Add(Notes[Notes.Count - 1]);
                                DrawSetkLine(xi[i], yj[j], xi[i] - WidthHeight, yj[j]);
                            }
                            
                         //    MessageBox.Show(Notes[Notes.Count - 2].i + " " + Notes[Notes.Count - 2].j);
                        }
                        mast = true;
                        if (i > 0)
                        {
                            for (var u = 0; u < HaveNotNear.Count - 1; u += 2)
                            {
                                if ((Notes[Notes.Count - 1].i == HaveNotNear[u].i && Notes[Notes.Count - 1].j == HaveNotNear[u].j)
                                    || Notes[Notes.Count - 1].i == HaveNotNear[u+1].i && Notes[Notes.Count - 1].j == HaveNotNear[u+1].j)
                                {
                                    if (!((Notes[Notes.Count - 2 - znachj].i != HaveNotNear[u + 1].i || Notes[Notes.Count - 2 - znachj].j != HaveNotNear[u + 1].j)
                                        && (Notes[Notes.Count - 2 - znachj].i != HaveNotNear[u].i || Notes[Notes.Count - 2 - znachj].j != HaveNotNear[u].j)))
                                        mast = false;
                                }
                                
                            }
                            if(mast)
                            {
                                Notes[Notes.Count - 1].Near.Add(Notes[Notes.Count - znachj - 2]);
                                Notes[Notes.Count - 2 - znachj].Near.Add(Notes[Notes.Count - 1]);
                                DrawSetkLine(xi[i], yj[j], xi[i], yj[j] - WidthHeight);

                            }


                            //MessageBox.Show(Notes[Notes.Count - znachj - 2].i + " " + Notes[Notes.Count -  znachj - 2].j);
                        }
                        var MyLabel = new Label();
                        MyLabel.Content = i + "|" + j;
                        MyLabel.FontSize = 8;
                        MyLabel.Margin = new Thickness(xi[i], yj[j], 0, 0);
                        MyLabel.HorizontalAlignment = HorizontalAlignment.Left;
                        MyLabel.VerticalAlignment = VerticalAlignment.Center;
                        t.Children.Add(MyLabel);
                        //MessageBox.Show(i+" "+j);
                        await Task.Delay(10);
                    }
                    catch { }
                }
                
            }

            //MessageBox.Show("");

        }

        void DrawSetkLine(int x1,int y1, int x2, int y2)
        {
            var MyLine = new Line();

            MyLine.X1 = x1;
            MyLine.Y1 = y1;


            MyLine.X2 = x2;
            MyLine.Y2 = y2;

            MyLine.Stroke = Brushes.White;
            MyLine.StrokeThickness = 1;
            MyLine.HorizontalAlignment = HorizontalAlignment.Left;
            MyLine.VerticalAlignment = VerticalAlignment.Center;
            t.Children.Add(MyLine);
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            var textFile = new StreamWriter(path3, true);
            textFile.WriteLine("");
            textFile.WriteLine("");
            textFile.WriteLine("");
            textFile.WriteLine("---NEW RUN------------------------------------------------");
            textFile.Close();
            var line = "";
            var file =new StreamReader(path);
            var counter=false;
            var i1 = 0;
            var i2 = 0;
            var j1 = 0;
            var j2 = 0;
            var DEleay = 0;
            Note StartNote=new Note();
            Note EndNote = new Note();
            while ((line = file.ReadLine()) != null)
            {
                if(counter)
                {
                   
                    i1 = Int32.Parse(line.Substring(0,line.IndexOf(',')));
                    line = line.Remove(0, line.IndexOf(',') + 1);

                 
                    j1 = Int32.Parse(line.Substring(0,line.IndexOf(',')));
                    line = line.Remove(0, line.IndexOf(',') + 1);

                   
                    i2 = Int32.Parse(line.Substring(0,line.IndexOf(',')));
                    line = line.Remove(0, line.IndexOf(',') + 1);

                    
                    j2 = Int32.Parse(line.Substring(0,line.IndexOf(',')));
                    line = line.Remove(0, line.IndexOf(',') + 1);

                   
                    DEleay = Int32.Parse(line);
                    

                    for (var i = 0; i <= Notes.Count - 1; i++)
                    {
                        //MessageBox.Show(Notes[i].i + " " + Notes[i].j + "     " + i1 + " " + j1);
                        if (Notes[i].i == i1 && Notes[i].j == j1)
                        {
                            
                            StartNote = Notes[i];
                            break;
                        }
                        
                    }
                    for (var i = 0; i <= Notes.Count - 1; i++)
                    {
                        
                        if (Notes[i].i == i2 && Notes[i].j == j2)
                        {
                           
                            EndNote = Notes[i];
                            break;
                        }

                    }
                    var color = Color.FromRgb((byte)Lab1.random.Next(byte.MaxValue), (byte)Lab1.random.Next(byte.MaxValue), (byte)Lab1.random.Next(byte.MaxValue));
                    
                    if (DEleay == 0)
                    {

                        var wayITEM = new Label();
                        Particles.Add(new Particle(StartNote, EndNote, DEleay,color, Lab1.DrawNote(StartNote.x, StartNote.y, color, t)));
                        StartNote.Owner = Particles[Particles.Count - 1];
                        wayITEM.Background = new SolidColorBrush(color);
                        way1.Items.Add(wayITEM);
                    }
                    else
                    {
                        var Maxim = new Rectangle();
                        Maxim.Name = "horosh";
                        Particles.Add(new Particle(StartNote, EndNote, DEleay, color, Maxim));
                    }
                    

                }
                else
                    counter = true ;
            }
             makeIterationAsync();
        }

        /* very simple makeNext bat bags was leave
        Note makeNext(Note Curent, Note End, List<Note> Path)
        {

            var min = double.PositiveInfinity;
            var dist = 0.0;
            var Next = new Note();
            for (var i = 0; i < Curent.Near.Count; i++)
            {
                dist = Math.Sqrt(Math.Pow(End.x - Curent.Near[i].x, 2) + Math.Pow(End.y - Curent.Near[i].y, 2));

                if (dist < min)
                {
                    min = dist;
                    Next = Curent.Near[i];
                }
                // MessageBox.Show(Curent.Near[i].i+"-"+ Curent.Near[i].j+"             " + Next.i+" "+ Next.j+" dist = "+min);
            }

            return Next;
        }
        */
        
            
        Note makeNext(Note Curent, Note End,List<Note> Path)
        {

            var min = double.PositiveInfinity;
            var dist = 0.0;
            var Next = Curent;
            var ok = true;
            for (var i=0; i<Curent.Near.Count;i++)
            {
                ok = true;
                dist = Math.Sqrt(Math.Pow(End.x - Curent.Near[i].x, 2) + Math.Pow(End.y - Curent.Near[i].y, 2));
                if(Path.Count>=2)
                    if (Curent.Near[i].i == Path[Path.Count-2].i && Curent.Near[i].j == Path[Path.Count-2].j)
                        ok = false;
                if (Curent.Near[i].Owner != null)
                {
                    if (Curent.Near[i].Owner.Next.i == Curent.i && Curent.Near[i].Owner.Next.j == Curent.j)
                        ok = false;
                    if (Curent.Near[i].Owner.Current.i == Curent.Near[i].Owner.Finish.i && Curent.Near[i].Owner.Current.j == Curent.Near[i].Owner.Finish.j)
                        ok = false;
                    
                }
                for(var o = 0; o<=Particles.Count-1;o++)
                {
                    if(Particles[o].Delay>iteration && Particles[o].Delay-1==iteration)
                        if(Particles[o].Current.i == Curent.Near[i].i && Particles[o].Current.j == Curent.Near[i].j)
                        {
                            ok = false;
                            break;
                        }
                }
                
                if (dist<min && ok)
                {
                    min = dist;
                    Next = Curent.Near[i];
                }
               // MessageBox.Show(Curent.Near[i].i+"-"+ Curent.Near[i].j+"             " + Next.i+" "+ Next.j+" dist = "+min);
            }

            return Next;
        }
        
        void makeNote(int i, ref Note curva)
        {
            
            curva.c++;
            Particles[i].Next = curva;
            
            if (curva.c == 1)
            {
                
                NotesNext.Add(curva);
                ParticlesConc.Add(new List<Particle>());
                ParticlesConc[ParticlesConc.Count - 1].Add(Particles[i]);
                //MessageBox.Show(NotesNext[0].i + " " + NotesNext[0].j);
            }
            if (curva.c > 1)
            {
               
                

                
                for (var it = 0; it < NotesNext.Count; it++)
                {
 
                    if (NotesNext[it].i == curva.i && NotesNext[it].j == curva.j)
                    {

                        ParticlesConc[it].Add(Particles[i]);
                        break;
                    }
                }
                
            }
          
        }

        private void way1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (iteration != 0)
            {
                try
                {
                    if (countCONST != 0)
                    {
                        t.Children.RemoveRange(countCONST, t.Children.Count);
                    }
                    else
                        countCONST = t.Children.Count;
                    way1Color.Fill = ((Label)way1.SelectedItem).Background;
                    var color = ((SolidColorBrush)(way1Color.Fill)).Color;

                    for (var i = 0; i <= Particles.Count - 1; i++)
                    {
                       // MessageBox.Show(color.ToString() + " " + Particles[i].color.ToString());
                        if (Particles[i].color == color)
                            for (var ty = 0; ty < Particles[i].Path.Count - 1; ty++)
                                Lab1.DrawLine(Particles[i].Path[ty].x, Particles[i].Path[ty].y, Particles[i].Path[ty + 1].x, 
                                    Particles[i].Path[ty + 1].y, t);
                    }
                }
                catch { }
            }
        }

        async Task makeIterationAsync()
        {
            GlobOKiterations = 0;
            ParticlesConc = new List<List<Particle>>();
           

            NotesNext = new List<Note>();
            for (var i = 0; i <= Particles.Count - 1; i++)
            {

                if (!Particles[i].loose && (Particles[i].Current.i != Particles[i].Finish.i || Particles[i].Current.j != Particles[i].Finish.j))
                {
                    if (Particles[i].Delay <= iteration)
                    {

                        if (String.Equals(Particles[i].Iam.Name, "horosh"))
                        {
                            //MessageBox.Show("");
                            var wayITEM = new Label();
                            var color = Color.FromRgb((byte)Lab1.random.Next(byte.MaxValue), (byte)Lab1.random.Next(byte.MaxValue),
                                (byte)Lab1.random.Next(byte.MaxValue));
                            Particles[i].color = color;
                            Particles[i].Iam.Name = null;
                            Particles[i].Iam = Lab1.DrawNote(Particles[i].Current.x, Particles[i].Current.y, color, t);
                            wayITEM.Background = new SolidColorBrush(color);
                            way1.Items.Add(wayITEM);

                            for (var p = 0; p <= Notes.Count - 1; p++)
                            {

                                if (Particles[i].Current.i == Notes[p].i && Particles[i].Current.j == Notes[p].j)
                                {

                                    Notes[p].Owner = Particles[i];
                                    break;
                                }

                            }

                            //Particles[i].Current.Owner = Particles[i];
                        }

                        var curva = makeNext(Particles[i].Current, Particles[i].Finish, Particles[i].Path);
                       // MessageBox.Show(Particles[i].Current.i+" "+ Particles[i].Current.j+"     "+ curva.i+" "+ curva.j);
                        if (curva.Owner != null)
                        {
                            /*
                            if (!curva.Owner.loose)
                                makeNote(i, curva);
                            else
                                Particles[i].loose = true;
                                */
                            Particles[i].loose = true;

                        }
                        else
                            makeNote(i, ref curva);

                    }

                }
                else
                {
                    if (Particles[i].loose)
                    {
                        Particles[i].loose = false;
                        //MessageBox.Show("loose" + Particles[i].color.ToString());
                    }
                }
                    
            }
            
           
            for (var i = 0; i <= NotesNext.Count - 1; i++)
            {

              
               
                    if (NotesNext[i].c == 1)
                    {
                        
                        NotesNext[i].Owner = ParticlesConc[i][0];
                        ParticlesConc[i][0].Current.c = 0;
                        ParticlesConc[i][0].Current.Owner = null;
                        ParticlesConc[i][0].Current = NotesNext[i];
                        ParticlesConc[i][0].Path.Add(NotesNext[i]);
                        NotesNext[i].c = 0;
                        ParticlesConc[i][0].OKinerations++;
                        GlobOKiterations++;
                    }
                    else
                    {
                    
                     countOfProblems++;
                     var chois = Lab1.random.Next(NotesNext[i].c);
                    //MessageBox.Show(chois+" chois from "+NotesNext[i].c--);
                    //MessageBox.Show(ParticlesConc[i].Count + "");

                    MessageBox.Show("concyretion "+ ParticlesConc[i][0].Current.i+" "+ParticlesConc[i][0].Current.j);
                    MessageBox.Show("AND " + ParticlesConc[i][1].Current.i + " " + ParticlesConc[i][1].Current.j);
                    MessageBox.Show("c = " + NotesNext[i].c + "   chis = " + chois);

                     
                     ParticlesConc[i][chois].Current.Owner = null;
                     ParticlesConc[i][chois].loose = false;
                     NotesNext[i].Owner = ParticlesConc[i][chois];

                     NotesNext[i].Owner.Current = NotesNext[i];
                     NotesNext[i].Owner.Path.Add(NotesNext[i]);
                     NotesNext[i].Owner.OKinerations++;
                     GlobOKiterations++;



                     /*
                     for(var q = 0; q <= NotesNext[i].Near.Count - 1; q++)
                     {
                         if(NotesNext[i].Near[q].Owner!=null)
                         {
                             NotesNext[i].Near[q].Owner.loose = true;
                         }
                     }
                     */

                    NotesNext[i].c=0;
                    
                    }
                
               
                /*
                else
                {
                    for (var y = 0; y <= NotesNext[i].c - 1; y++)
                        Wait.Add(ParticlesConc[i][y]);
                }
                */
                
            }
            /*
            for(var y=0;y<=Wait.Count-1;y++)
            {

            }
            */
            iteration++;
            var nado = false;
            await Task.Delay(300);
          
            var gogo = new ThicknessAnimation();
            for (var i = 0; i <= Particles.Count - 1; i++)
            {
                
                //MessageBox.Show(Particles[i].Current.i+" "+ Particles[i].Current.j);
                //Lab1.DrawNote(Particles[i].Current.x, Particles[i].Current.y,Particles[i].color, t);
                gogo = new ThicknessAnimation(Particles[i].Iam.Margin, new Thickness(Particles[i].Current.x - 5, Particles[i].Current.y - 5, 0, 0), new Duration(TimeSpan.FromSeconds(0.3)));
                Particles[i].Iam.BeginAnimation(MarginProperty, gogo);
                //Particles[i].Iam.Margin = new Thickness(Particles[i].Current.x - 5, Particles[i].Current.y - 5, 0, 0);
                if ((Particles[i].Finish.i != Particles[i].Current.i || Particles[i].Finish.j != Particles[i].Current.j) && !nado)
                    nado = true;
                


            }
            var srPartSpeed = 0.0;
            var coPart = 0;
            for (var t = 0; t < Particles.Count; t++)
                if ((Particles[t].Delay < iteration) && (Particles[t].Current.i != Particles[t].Finish.i || Particles[t].Current.j != Particles[t].Finish.j))
                {
                    srPartSpeed += Particles[t].OKinerations+ Particles[t].Delay;
                    coPart++;
                }

            try
            {
                srPartSpeed = srPartSpeed / iteration / coPart;
                Lspeed.Content = (GlobOKiterations / coPart) + "";
            }
            catch
            {
                srPartSpeed = 0;
                Lspeed.Content = 0;
            }
            Lproblem.Content = countOfProblems+"";

            
            Lspeed_Copy.Content = iteration + "";
            var textFile = new StreamWriter(path3,true);
            textFile.WriteLine("LOG-----ITERATION " + (iteration - 1) + "------------------------");
            textFile.WriteLine("    Текущая скорость по сетке = " + Lspeed.Content);
            textFile.WriteLine("    Средняя скорость по сетке = " + srPartSpeed);
            textFile.WriteLine("    Колтчество столкновений   = " + countOfProblems);
            textFile.Close();
            
            if (!nado)
            {
                way1.IsEnabled = true;
            }
            else
            {

                await makeIterationAsync();

            }
            
            
            
               
        }
    }
    
    public class Particle
    {
        
        public Rectangle Iam;
        public Color color;
        public Note Current;
        public Note Next;
        public Note Finish;
        public bool loose = false;
        public int Delay;
        public List<Note> Path;

        public int OKinerations = 0;


        public Particle(Note Current1, Note Finish1,int Delay1,Color color1,Rectangle Iam1)
        {
            this.Current = Current1;
            this.Finish = Finish1;
            this.Delay = Delay1;
            this.Path= new List<Note>();
            this.Path.Add(this.Current);
            this.color = color1;
            this.Iam = Iam1;
        }
        public Particle()
        {

        }


        public List<Note> Near = new List<Note>();

    }
}
