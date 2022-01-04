using System;
using System.Collections.Generic;
using System.IO;
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

namespace MyTodosWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();

            FileStream todoFile = new FileStream("My Todos.txt", FileMode.OpenOrCreate);
            todoFile.Close();

            List<string> myList = new List<string>();
            myList = File.ReadAllLines(todoFile.Name).ToList();
            StoreToList(myList);
        }

        private void clear_btn_Click(object sender, RoutedEventArgs e)
        {
            todo_input.Text = "";
        }

        private void submit_btn_Click(object sender, RoutedEventArgs e)
        {
            if (todo_input.Text.Length != 0)
            {
                FileStream todoFile = new FileStream("My Todos.txt", FileMode.OpenOrCreate);
                todoFile.Close();

                List<string> myList = new List<string>();
                myList = File.ReadAllLines(todoFile.Name).ToList();

                myList.Add(todo_input.Text);
                File.WriteAllLines(todoFile.Name, myList);

                todo_input.Text = "";

                StoreToList(myList);

            }
            else
            {
                MessageBox.Show("Empty input", "Empty input", MessageBoxButton.OK);
            }

        }


        private void todos_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show(todos_list.SelectedIndex.ToString()+". "+ todos_list.SelectedItem.ToString(), "Todo",MessageBoxButton.OK, 
                MessageBoxImage.Information);

        }

        void StoreToList(List<string> list) 
        {
            todos_list.ItemsSource = list;
            todo_delete.ItemsSource = list;
        }


        private void todo_delete_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            FileStream todoFile = new FileStream("My Todos.txt", FileMode.OpenOrCreate);
            todoFile.Close();

            List<string> myList = new List<string>();
            myList = File.ReadAllLines(todoFile.Name).ToList();

            int selected = todo_delete.SelectedIndex;
            if(selected != -1)
            {

                myList.RemoveAt(selected);
                MessageBox.Show("Todo is removed", "Todo", MessageBoxButton.OK);
            File.WriteAllLines(todoFile.Name, myList);
            StoreToList(myList);
            }

            //MessageBox.Show(todo_delete.SelectedIndex.ToString(), "Todo", MessageBoxButton.OK);



        }
    }
}
