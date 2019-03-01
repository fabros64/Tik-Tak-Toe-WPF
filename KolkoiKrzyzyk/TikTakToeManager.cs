using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KolkoiKrzyzyk
{
    public class TikTakToeManager
    {          
        public ShapeType[,] Rects { get; set; } = new ShapeType[3,3];
        public ShapeType ActualShapeType { get; set; }
        public List<Rectangle> Rectangles { get; set; } = new List<Rectangle>();

        private RadioButton rbCircle;
        private RadioButton rbCross;
        private Rectangle RecTour;

        public TikTakToeManager()
        {
            ActualShapeType = ShapeType.Cross;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Rects[i, j] = ShapeType.Blank;
                }
            }
        }

        public TikTakToeManager(RadioButton rbCircle, RadioButton rbCross, Rectangle RecTour) : this()
        {            
            this.rbCircle = rbCircle;
            this.rbCross = rbCross;
            this.RecTour = RecTour;
        }
        

        private bool isEndOfGame()
        {
            if (Rectangles.Count < 5)
                return false;

            if ((Rects[0, 0].Equals(Rects[1, 1]) && Rects[1, 1].Equals(Rects[2, 2]) && Rects[1,1] != ShapeType.Blank)
                 || (Rects[2, 0].Equals(Rects[1, 1]) && Rects[1, 1].Equals(Rects[0, 2]) && Rects[1, 1] != ShapeType.Blank))
                return true;

            for (int i = 0; i < 3; i++)
            {
                List<ShapeType> horizontal = new List<ShapeType>();
                List<ShapeType> vertical = new List<ShapeType>();
                for (int j = 0; j < 3; j++)
                {
                    horizontal.Add(Rects[i,j]);
                    vertical.Add(Rects[j,i]);
                }

                if ( ((horizontal[0].Equals(horizontal[1])) && horizontal[1].Equals(horizontal[2]) && horizontal[1].Equals(ActualShapeType))
                  || ( vertical[0].Equals(vertical[1]) && vertical[1].Equals(vertical[2])) && vertical[1].Equals(ActualShapeType) )
                    return true;
            }

            return false;
        }

        public bool CheckWin()
        {
            if (isEndOfGame())
            {
                if (ActualShapeType == ShapeType.Circle)
                    MessageBox.Show("Wygrało kółeczko", "Zwycięzca", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.RightAlign);
                else
                {
                    MessageBox.Show("Wygrał krzyżyk","Zwycięzca", MessageBoxButton.OK, MessageBoxImage.Information,MessageBoxResult.OK,MessageBoxOptions.RightAlign);
                }
                Reset();
                return true;
            }

            if (Rectangles.Count == 9)
            {
                MessageBox.Show("Remis", "Wynik", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.RightAlign);              
                Reset();
                return true;
            }

            return false;
        }

        public void UpdateGraphicOnRect(Rectangle rect, Func<int, int, ShapeType> lambdFunc)
        {
            int[] indexers = new int[2];
            string number = rect.Name.TrimStart(new char[] {'r', 'e', 'c'});
            indexers[0] = int.Parse(number[0].ToString());
            indexers[1] =  int.Parse(number[1].ToString());

            bool isWin = false;

            if (lambdFunc(indexers[0], indexers[1]) == ShapeType.Blank)
            {
                rect.Fill = UriOfShape(ActualShapeType);
                Rects[indexers[0], indexers[1]] = ActualShapeType;
                Rectangles.Add(rect);
                rbCircle.IsEnabled = false;
                rbCross.IsEnabled = false;
                isWin = CheckWin();
                ChangeTypeShape();
                RecTour.Fill = UriOfShape(ActualShapeType);               
            }

            if (isWin)
                CheckRbsAndFillRectTura();
        }

        public ImageBrush UriOfShape(ShapeType st)
        {
            if(st == ShapeType.Circle)
                return new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Graphics/kolo.png")));
            else if(st == ShapeType.Cross)
                return new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Graphics/cross.png")));
            
            return new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Graphics/blank.png")));
        }

        private void ChangeTypeShape()
        {
            if (ActualShapeType == ShapeType.Cross)           
                ActualShapeType = ShapeType.Circle;            
            else
                ActualShapeType = ShapeType.Cross;
        }

        public void Reset()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Rects[i, j] = ShapeType.Blank;
                }
            }

            foreach (var rect in Rectangles)
            {
                rect.Fill = UriOfShape(ShapeType.Blank);
            }

            Rectangles = new List<Rectangle>();
            rbCircle.IsEnabled = true;
            rbCross.IsEnabled = true;   
        }

        public void CheckRbsAndFillRectTura()
        {
            if (rbCircle.IsChecked == true)
            {
                RecTour.Fill = UriOfShape(ShapeType.Circle);
                ActualShapeType = ShapeType.Circle;
            }
            else
            {
                RecTour.Fill = UriOfShape(ShapeType.Cross);
                ActualShapeType = ShapeType.Cross;
            }
        }

    }

    public enum ShapeType
    {
        Cross,
        Circle,
        Blank
    }
}
