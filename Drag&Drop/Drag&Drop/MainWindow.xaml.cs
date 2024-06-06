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

namespace Drag_Drop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListBox lbSender = sender as ListBox;
            ListBoxItem przenoszonyElement = lbSender.GetItemAt(e.GetPosition(lbSender));
            if (przenoszonyElement != null )
            {
                DataObject dane = new DataObject();
                dane.SetData("Format_Lista", lbSender);
                dane.SetData("Format_ElementListy", przenoszonyElement);
                DragDrop.DoDragDrop(lbSender, dane, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }

        private void ListBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.KeyStates.HasFlag(DragDropKeyStates.ControlKey))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects |= DragDropEffects.Move;
            }
        }

        private void ListBox_Drop(object sender, DragEventArgs e)
        {
            ListBox lbSender = sender as ListBox;
            ListBox lbSource = e.Data.GetData("Format_Lista") as ListBox;

            ListBoxItem przenoszonyElement = e.Data.GetData("Format_ElementListy") as ListBoxItem;

            if (!e.KeyStates.HasFlag(DragDropKeyStates.ControlKey))
                lbSource.Items.Remove(przenoszonyElement);
            else przenoszonyElement = new ListBoxItem() { Content = przenoszonyElement.Content };

            int indeks = lbSender.IndexFromPoint(e.GetPosition(lbSender));
            if (indeks < 0) lbSender.Items.Add(przenoszonyElement);
            else lbSender.Items.Insert(indeks, przenoszonyElement);
        }
    }
}
