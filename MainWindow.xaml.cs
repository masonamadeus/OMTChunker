using Microsoft.Win32;
using System;
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
using System.IO;

namespace OMTChunker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ChunkerFile chunker = new ChunkerFile();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_Browse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog opn = new OpenFileDialog();
            if ((bool)opn.ShowDialog())
            {
                // need to add checks later
                if (File.Exists(opn.FileName))
                {
                    chunker.GetMetadata(opn.FileName);
                }
            }
            
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
