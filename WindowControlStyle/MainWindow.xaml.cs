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

namespace WindowControlStyle
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow 
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Xml_BtnClick(object sender, RoutedEventArgs e)
        {
            var win = new MutiLanguagesWindow();
            win.ShowDialog();
        }

        private void Xaml_BtnClick(object sender, RoutedEventArgs e)
        {
            var win = new MutiLanguageWindow2();
            win.ShowDialog();
        }

        private void Soundcard_BtnClick(object sender, RoutedEventArgs e)
        {
            var win = new SoundcardWindow();
            win.ShowDialog();
        }
    }
}
