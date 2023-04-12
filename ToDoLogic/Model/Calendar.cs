using System.Collections;

namespace ToDoLogic.Model
{
    public class Calendar : IEnumerable<TaskModel>
    {
        public List<TaskModel> taskList = new List<TaskModel>();
        public Calendar model;

        public Calendar(Calendar model)
        {
            this.model = model;
        }

        //crud
        public void AddTask(TaskModel model)
        {
            taskList.Add(model);
        }

        public bool RemoveTask(TaskModel model)
        {
            return taskList.Remove(model);
        }

        public int NumOfTasks
        {
            get
            {
                return taskList.Count;
            }
        }

        public TaskModel this[int index]
        {
            get
            {
                return taskList[index];
            }
        }

        public IEnumerator<TaskModel> GetEnumerator()
        {
            return taskList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)this.GetEnumerator();
        }
    }
}