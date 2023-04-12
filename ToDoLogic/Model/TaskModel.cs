namespace ToDoLogic.Model
{

    public enum PriorityLevel : byte
    {
        Low,
        Medium,
        High
    }

    public class TaskModel
    {
        public string Duty { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreationDate { get; set; }
        public PriorityLevel Priority { get; set; }
        public bool IsDone { get; set; }

        public TaskModel(string duty , DateTime creationDate , DateTime date , PriorityLevel priority , bool isDone = false)
        {
            this.Duty = duty;
            this.CreationDate = creationDate;
            this.Date = date;
            this.Priority = priority;
            this.IsDone = isDone;
        }

        public override string ToString()
        {
            return Duty + ", Creation Time " + CreationDate.ToString() + ", Date: " + Date.ToString() +
                ", priority: " + Priority.ToString() + ", is task done: " + (IsDone ? "done" : "undone");
        }
    }

    public class TaskModelForConsole
    {
        public int ID { get; set; }
        public string? Duty { get; set; }
        public DateTime Date { get; set; }
        public PriorityLevel Priority { get; set; }
        public bool IsDone { get; set; }
    }
}

