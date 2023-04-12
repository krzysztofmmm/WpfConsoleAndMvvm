using Newtonsoft.Json;
using ToDoLogic.Model;

namespace ConsoleToDos
{
    public class Calend
    {

        public string fileName = @"taskList.json";
        public List<TaskModelForConsole> Duties { get; set; }
        public Calend()
        {
            Duties = new List<TaskModelForConsole>();
        }

        public void AddTask(bool isDone , int id , DateTime date , PriorityLevel priority , string? task  /*TaskModelForConsole taskModel*/)
        {
            var newTask = new TaskModelForConsole
            {
                IsDone = isDone ,
                ID = id ,
                Date = date ,
                Priority = priority ,
                Duty = task
            };
            Duties.Add(newTask);
        }

        public void DeleteTask(int idd)
        {
            var taskToRemove = Duties.FirstOrDefault(t => t.ID == idd);
            if(taskToRemove != null)
            {
                Duties.Remove(taskToRemove);
            }
        }

        public void ShowTasks(bool showDoneTasks = false)
        {


            Duties = FileWr.DeserializeTasks();
            var tasks = Duties;

            if(showDoneTasks)
            {
                tasks = tasks.Where(t => t.IsDone).ToList();
            }

            if(!tasks.Any())
            {
                Console.WriteLine("No tasks found.");
                return;
            }

            Console.WriteLine("ID | Task | Priority | Due Date | Is Done");
            Console.WriteLine("------------------------------------------");
            foreach(var task in tasks)
            {
                Console.WriteLine($"{task.ID} | {task.Duty} | {task.Priority} | {task.Date.ToShortDateString()} | {(task.IsDone ? "Done" : "Undone")}\n" +
                "------------------------------------------");
            }
        }

        public void MoveTask(int taskId)
        {
            //deserializacja listy z pliku
            var json = File.ReadAllText(fileName);
            var taskList = JsonConvert.DeserializeObject<List<TaskModelForConsole>>(json);

            //wybranie taska z odpowienim id4
            var taskToMove = Duties.FirstOrDefault(t => t.ID == taskId);

            //jesli zadanie  id zostalo znalezione wpisanie nowej daty
            if(taskToMove != null)
            {
                Console.Write("Enter the new date for the task (yyyy-mm-dd): ");
                DateTime newDate = DateTime.Parse(Console.ReadLine());

                //dodanie nowej daty
                taskToMove.Date = newDate;

                Console.WriteLine($"Task with ID {taskId} has been moved to {newDate}.");
            }

            else
            {
                Console.WriteLine($"Task with ID {taskId} was not found.");
            }
        }


        public void IsDone(int taskId , bool isDone)
        {
            var taskToUpdate = Duties.FirstOrDefault(t => t.ID == taskId);

            if(taskToUpdate == null)
            {
                return;
            }

            taskToUpdate.IsDone = isDone;
        }

        public void SortTasks(string sortChoice)
        {

            switch(sortChoice)
            {
                case "1":
                    Duties = Duties.OrderBy(t => t.ID).ToList();
                    break;
                case "2":
                    Duties = Duties.OrderByDescending(t => t.ID).ToList();
                    break;
                case "3":
                    Duties = Duties.OrderBy(t => t.Priority).ToList();
                    break;
                case "4":
                    Duties = Duties.OrderByDescending(t => t.Priority).ToList();
                    break;
                case "5":
                    Duties = Duties.OrderBy(t => t.Date).ToList();
                    break;
                case "6":
                    Duties = Duties.OrderByDescending(t => t.Date).ToList();
                    break;
                default:
                    return;
            }
        }

        public void ChangePriority(int taskId , PriorityLevel priority)
        {
            var taskToUpdate = Duties.FirstOrDefault(t => t.ID == taskId);

            if(taskToUpdate == null)
            {
                return;
            }
            int newPriority = int.Parse(Console.ReadLine());

            taskToUpdate.Priority = priority;
        }
    }
}

