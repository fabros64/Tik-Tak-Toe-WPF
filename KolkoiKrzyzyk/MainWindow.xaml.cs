﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KolkoiKrzyzyk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {      

        public TikTakToeManager TikTakToe = new TikTakToeManager();

        public MainWindow()
        {
            
            InitializeComponent();
            TikTakToe = new TikTakToeManager(rbKolo, rbCross, RecTura);
        }

        private void RbCross_Checked(object sender, RoutedEventArgs e)
        {
            TikTakToe.ActualShapeType = ShapeType.Cross;
            RecTura.Fill = TikTakToe.UriOfShape(ShapeType.Cross);
        }

        private void RbKolo_Checked(object sender, RoutedEventArgs e)
        {
            TikTakToe.ActualShapeType = ShapeType.Circle;
            RecTura.Fill = TikTakToe.UriOfShape(ShapeType.Circle);
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TikTakToe.UpdateGraphicOnRect(sender as Rectangle, (f, s) => TikTakToe.Rects[f, s]);
        }              

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TikTakToe.Reset();
            rbKolo.IsEnabled = true;
            rbCross.IsEnabled = true;
            TikTakToe.CheckRbsAndFillRectTura();
        }
    }
}
