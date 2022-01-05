
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

            // Getting/creating the todos file then store the file contents to myList
            List<string> myList = GetTodoListFromFile();

            StoreToList(myList); // Populates the lists that view all the todos
        }

        // For when pressing the Clear Button
        private void Clear_btn_Click(object sender, RoutedEventArgs e)
        {
            // Empties the todo input
            todo_input.Text = "";
        }

        // For when pressing the Submit Button
        private void Submit_btn_Click(object sender, RoutedEventArgs e)
        {
            // Checks if the todo input is empty or not
            if (todo_input.Text.Length != 0)
            {
                // If todo input is not empty

                // Getting/creating the todos file then store the file contents to myList
                List<string> myList = GetTodoListFromFile();

                // Add todo input to myList
                myList.Add(todo_input.Text);

                // Replace all lines from the file with the one from myList
                File.WriteAllLines(FilePath(), myList);

                // Empties the todo input
                todo_input.Text = "";

                StoreToList(myList);  // Populates the lists that view all the todos

                // A message box to show that the todo was added successfuly
                MessageBox.Show("Todo was added to the list successfuly", "Success", MessageBoxButton.OK);

            }
            else
            {
                // If todo input is empty

                // A message box to show that todo input empty
                MessageBox.Show("Empty input", "Empty input", MessageBoxButton.OK);
            }

        }

        // For when a todo is selected from a ListBox to view it
        private void Todos_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // A message box that views the todo
            MessageBox.Show(todos_list.SelectedItem.ToString(), "Todo",MessageBoxButton.OK);

        }

        // To update the view and delete ListBox
        void StoreToList(List<string> list) 
        {
            // Updates the ListBoxs with the lates list
            todos_list.ItemsSource = list;
            todo_delete.ItemsSource = list;
        }

        // For when a todo is selected from a ListBox to delete it
        private void Todo_delete_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Getting/creating the todos file then store the file contents to myList
            List<string> myList = GetTodoListFromFile();

            // store the selected todo index to delete
            int selected = todo_delete.SelectedIndex;

            // Checks if selected not -1 if so then do nothing
            if(selected != -1)
            {
                // Removes an element from the at selected
                myList.RemoveAt(selected);

                // A message box to indicated that the todo has been removed
                MessageBox.Show("Todo is removed", "Todo", MessageBoxButton.OK);

                // Replace all lines from the file with the one from myList
                File.WriteAllLines(FilePath(), myList);


                StoreToList(myList); // Populates the lists that view all the todos
            }

        }

        // Gets the todos from a file, convert them to a list then return the list
        List<string> GetTodoListFromFile()
        {
            return File.ReadAllLines(FilePath()).ToList();
        }

        // Returns the path of the file that store's the list
        string FilePath()
        {
            // Creates/gets the My todos.txt file
            FileStream todoFile = new FileStream("My Todos.txt", FileMode.OpenOrCreate);
            todoFile.Close();

            // Returns the path of the todoFile file
            return todoFile.Name;
        }
    }
}
