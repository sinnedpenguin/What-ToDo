using Spectre.Console;

namespace What_ToDo.Tasks
{
    public class Tasks
    {
        public static List<string> tasks = new();

        public static void DisplayTasks()
        {
            AnsiConsole.WriteLine("What To Do:");
            foreach (var task in tasks)
            {
                AnsiConsole.MarkupLine($"> {task}");
            }
            AnsiConsole.WriteLine(new string('-', Console.WindowWidth));

            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .AddChoices("Add", "Edit", "Mark as completed", "Delete", "Exit")
                );

            switch (selection)
            {
                case "Add":
                    AddTask();
                    break;

                case "Edit":
                    EditTask();
                    break;

                case "Mark as completed":
                    MarkAsCompleted();
                    break;

                case "Delete":
                    DeleteTask();
                    break;

                case "Exit":
                    SaveTasks();
                    Environment.Exit(0);
                    break;
            }
        }

        public static void AddTask()
        {
            var task = AnsiConsole.Ask<string>("Enter task: ");
            tasks.Add(task);
            AnsiConsole.Clear();
            SaveTasks();
            DisplayTasks();
        }

        public static void EditTask()
        {
            AnsiConsole.Clear();
            if (tasks.Count == 0)
            {
                AnsiConsole.WriteLine("No available task/s to edit!");
                DisplayTasks();
            }
            else
            {
                AnsiConsole.Clear();
                var selection = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Edit:")
                        .PageSize(10)
                        .MoreChoicesText("Move up and down to check more tasks")
                        .AddChoices(tasks)
                    );

                if (selection != null)
                {
                    var index = tasks.IndexOf(selection);
                    var task = AnsiConsole.Ask<string>("Edit task: ");
                    tasks[index] = task;
                }
                SaveTasks();
                DisplayTasks();
            }
        }

        public static void MarkAsCompleted()
        {
            AnsiConsole.Clear();
            if (tasks.Count == 0)
            {
                AnsiConsole.WriteLine("No available task/s to mark as completed!");
                DisplayTasks();
            }
            else
            {
                AnsiConsole.Clear();
                var selection = AnsiConsole.Prompt(
                    new MultiSelectionPrompt<string>()
                        .Title("Mark as completed:")
                        .PageSize(10)
                        .MoreChoicesText("Move up and down to check more tasks")
                        .AddChoices(tasks)
                    );
                for (int i = 0; i < tasks.Count; i++)
                {
                    if (selection.Contains(tasks[i]))
                    {
                        tasks[i] = $"[green]{tasks[i]}[/]";
                    }
                }
                SaveTasks();
                DisplayTasks();
            }
        }
            
        public static void DeleteTask()
        {
            AnsiConsole.Clear();
            if (tasks.Count == 0)
            {
                AnsiConsole.WriteLine("No available task/s to delete!");
                DisplayTasks();
            }
            else
            {
                AnsiConsole.Clear();
                var selection = AnsiConsole.Prompt(
                    new MultiSelectionPrompt<string>()
                        .Title("Delete:")
                        .PageSize(10)
                        .MoreChoicesText("Move up and down to check more tasks")
                        .AddChoices(tasks)
                    );
                foreach (var item in selection)
                {
                    tasks.Remove(item);
                }
                SaveTasks();
                DisplayTasks();
            }
        }

        public static void SaveTasks()
        {
            File.WriteAllLines("Tasks.txt", tasks);
        }

        public static void LoadTasks()
        {
            if (File.Exists("Tasks.txt"))
            {
                tasks = File.ReadAllLines("Tasks.txt").ToList();
            }
        }

    }
}