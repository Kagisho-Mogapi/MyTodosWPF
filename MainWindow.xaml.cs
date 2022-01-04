
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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


            List<string> myList = File.ReadAllLines(FilePath()).ToList();
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
                
                List<string> myList = File.ReadAllLines(FilePath()).ToList();

                myList.Add(todo_input.Text);
                File.WriteAllLines(FilePath(), myList);

                todo_input.Text = "";

                StoreToList(myList); 

                MessageBox.Show("Todo was added to the list successfuly", "Success", MessageBoxButton.OK);

            }
            else
            {
                MessageBox.Show("Empty input", "Empty input", MessageBoxButton.OK);
            }

        }


        private void todos_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show(todos_list.SelectedItem.ToString(), "Todo",MessageBoxButton.OK);

        }

        void StoreToList(List<string> list) 
        {
            todos_list.ItemsSource = list;
            todo_delete.ItemsSource = list;
        }


        private void todo_delete_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            List<string> myList = GetTodoList();

            int selected = todo_delete.SelectedIndex;
            if(selected != -1)
            {

                myList.RemoveAt(selected);
                MessageBox.Show("Todo is removed", "Todo", MessageBoxButton.OK);
                File.WriteAllLines(FilePath(), myList);
                StoreToList(myList);
            }

        }

        List<string> GetTodoList()
        {
            return File.ReadAllLines(FilePath()).ToList();
        }

        string FilePath()
        {

            FileStream todoFile = new FileStream("My Todos.txt", FileMode.OpenOrCreate);
            todoFile.Close();

            return todoFile.Name;
        }
    }
}
