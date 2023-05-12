using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // Tasks list
        List<string> tasks = new List<string>();
        // Load the list of tasks from the txt file
        if (File.Exists("tasks.txt"))
        {
            string[] lines = File.ReadAllLines("tasks.txt");
            tasks = new List<string>(lines);
        }

        // Main loop 
        while (true)
        {
            DisplayTasks(); // Display current tasks
            Console.WriteLine("1. Add a task");
            Console.WriteLine("2. Edit a task");
            Console.WriteLine("3. Mark a task as completed");
            Console.WriteLine("4. Delete a task");
            Console.WriteLine("5. Exit");
            Console.Write("Option: ");
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                // Add a task
                case "1":
                    Console.Write("Enter a task: ");
                    string task = Console.ReadLine();
                    tasks.Add(task);
                    Console.WriteLine("**************************************");
                    Console.WriteLine("Task added!");
                    // Save the list of tasks to the txt file
                    File.WriteAllText("tasks.txt", string.Join("\n", tasks));
                    break;

                // Edit a task
                case "2":
                    DisplayTasks();
                    Console.Write("Select a task to edit: ");
                    int editIndex;
                    while (true)
                    {
                        // Validate the user input
                        bool isValidEditIndex = int.TryParse(Console.ReadLine(), out editIndex); // Check if the user input is an integer
                        if (!isValidEditIndex || editIndex < 1 || editIndex > tasks.Count) // Check if the user input is equal to the task number
                        {
                            Console.WriteLine("**************************************");
                            Console.WriteLine("Invalid task number!");
                            DisplayTasks();
                            Console.Write("Select a task to edit: ");
                        }
                        else
                        {
                            break;
                        }
                    }
                    // Allow user to edit the task
                    Console.Write("Enter new task name: ");
                    string newTask = Console.ReadLine();
                    tasks[editIndex - 1] = newTask;
                    Console.WriteLine("**************************************");
                    Console.WriteLine("Task edited!");
                    // Update the list of tasks on the txt file
                    File.WriteAllText("tasks.txt", string.Join("\n", tasks));
                    break;

                // Mark a task as completed
                case "3":
                    DisplayTasks();
                    Console.Write("Select a task to mark as completed: ");
                    int completeIndex;
                    while (true)
                    {
                        bool isValidCompleteIndex = int.TryParse(Console.ReadLine(),out completeIndex);
                        if (!isValidCompleteIndex || completeIndex < 1 || completeIndex > tasks.Count)
                        {
                            Console.WriteLine("**************************************");
                            Console.WriteLine("Invalid task number!");
                            DisplayTasks();
                            Console.Write("Select a task to mark as completed: ");
                        }
                        else
                        {
                            break;
                        }
                    }
                    string completedTask = tasks[completeIndex - 1] + " (COMPLETED)";
                    tasks[completeIndex - 1] = completedTask;
                    Console.WriteLine("**************************************");
                    Console.WriteLine("Task marked as completed!");
                    File.WriteAllText("tasks.txt", string.Join("\n", tasks));
                    break;

                // Delete a task
                case "4":
                    DisplayTasks();
                    Console.Write("Select a task to delete: ");
                    int deleteIndex;
                    while (true)
                    {
                        bool isValidDeleteIndex = int.TryParse(Console.ReadLine(), out deleteIndex);
                        if (!isValidDeleteIndex || deleteIndex < 1 || deleteIndex > tasks.Count)
                        {
                            Console.WriteLine("**************************************");
                            Console.WriteLine("Invalid task number!");
                            DisplayTasks();
                            Console.Write("Select a task to delete: ");
                        }
                        else
                        {
                            break;
                        }
                    }
                    tasks.RemoveAt(deleteIndex - 1);
                    Console.WriteLine("**************************************");
                    Console.WriteLine("Task deleted!");
                    File.WriteAllText("tasks.txt", string.Join("\n", tasks));
                    break;

                // Exit the app
                case "5":
                    while (true)
                    {
                        Console.Write("Are you sure? (y/n): ");
                        string confirmExit = Console.ReadLine().ToLower();
                        if (confirmExit != "y" && confirmExit != "n")
                        {
                            Console.WriteLine("Invalid input!");
                        }
                        else if (confirmExit == "y")
                        {
                            Console.WriteLine("**************************************");
                            Console.WriteLine("Exiting...");
                            Console.WriteLine("**************************************");
                            Environment.Exit(0);
                        }
                        else if (confirmExit == "n")
                        {
                            Console.WriteLine("**************************************");
                            break;
                        }
                    }
                    break;
                    
                default:
                    Console.WriteLine("**************************************");
                    Console.WriteLine("Invalid input!");
                    break;
            }
        }
        // Display tasks function
        void DisplayTasks()
        {
            Console.WriteLine("**************************************");
            Console.WriteLine("TASKS:");
            for (int i = 0; i < tasks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {tasks[i]}");
            }
            Console.WriteLine("**************************************");
        }
    }
}