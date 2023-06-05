using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.IO;
using System.Reflection;



namespace TicTacToe_KB2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page4_Lista : ContentPage
    {
        public Page4_Lista()
        {
            InitializeComponent();

        }
    }
    public class TodoItem
    {
        public string TodoText { get; set; }

        public TodoItem(string TodoText)
        {
            this.TodoText = TodoText;
        }
    }
    public class TodoListViewModel
    {
        string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "KtoWygralv2.txt");
        public ObservableCollection<TodoItem> TodoItems { get; set; }
        public TodoListViewModel()
        {
            TodoItems = new ObservableCollection<TodoItem>();
            string values;
            string[] value;

            try
            {
                values = File.ReadAllText(fileName);
                value = values.Split('\n');

                    for (int i = 0; i < value.Length; i++)
                    {

                    if (value[i] != "")
                    {
                       TodoItems.Add(new TodoItem(value[i]));
                    }

                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd wczytania");
            }


            /*if (!File.Exists(fileName))
            {
                File.WriteAllText(fileName, "");
            }*/
            //File.WriteAllText(fileName, "kkv2");
            //values = File.ReadAllText(fileName);
    

        }

        public ICommand AddTodoCommand => new Command(AddTodoItem);
        public string NewTodoZmianaWartosci { get; set; }

        public void AddTodoItem()
        {
            TodoItems.Add(new TodoItem(NewTodoZmianaWartosci));

            File.WriteAllText(fileName, File.ReadAllText(fileName)+"\n"+NewTodoZmianaWartosci);
        }

        public ICommand UsunTodoCommand => new Command(UsunTodoItem);

        void UsunTodoItem(object o)
        {
            TodoItem itemDo_Usuniecia = o as TodoItem;
            TodoItems.Remove(itemDo_Usuniecia);

            var sb = new StringBuilder();
            foreach (var msg in TodoItems)
            {
                sb.AppendLine(msg.TodoText);
            }
            File.WriteAllText(fileName, sb.ToString());

            

        }
    }
}