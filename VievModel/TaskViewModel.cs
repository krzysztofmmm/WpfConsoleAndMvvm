using Newtonsoft.Json;
using System;
using System.Windows.Input;
using ToDoLogic.Model;

namespace WPFToDolist.VievModel
{
    //KLASA TESTOWA DO SPRAWDZENIA JAK TO ZADZIALA
    public class TaskViewModel : ObservedObj
    {
        //TA KLASA JEST DO OBLSUGI POJEDYNCZEGO WYBRANEGO ZADANIA, NP DO EDYCJI!!!!!

        private ToDoLogic.Model.TaskModel model;

        #region properties
        public string Duty
        {

            get
            {
                return model.Duty;
            }
        }

        public DateTime CreationDate
        {
            get
            {
                return model.CreationDate;
            }
        }

        public DateTime Date
        {
            get
            {
                return model.Date;
            }
        }
        public PriorityLevel Priority
        {
            get
            {
                return model.Priority;
            }
            set
            {
                if(model.Priority != value)
                {
                    model.Priority = value;
                    OnPropertyChanged(nameof(Priority));
                }
            }
        }

        public bool IsDone
        {
            get
            {
                return model.IsDone;
            }
        }

        public bool IsStayedUndone
        {
            get
            {
                return !IsDone && (DateTime.Now > Date);
            }
        }
        #endregion
        public TaskViewModel(TaskModel model)
        {
            this.model = model;
        }
        [JsonConstructor]
        public TaskViewModel(string duty , DateTime creationDate , DateTime date , PriorityLevel priority , bool isDone)
        {
            model = new TaskModel(duty , creationDate , date , priority , isDone);
        }

        public TaskViewModel(TaskViewModel taskModel)
        {
            TaskModel = taskModel;
        }

        public TaskModel GetModel()
        {
            return model;
        }

        public override string ToString()
        {
            return model.ToString();
        }

        #region commands

        [JsonIgnore]
        private ICommand markAsDone;
        [JsonIgnore]
        public ICommand MarkAsDone
        {
            get
            {
                if(markAsDone == null) markAsDone = new ViewModelCommand
                (o =>
                {
                    model.IsDone = true;
                    OnPropertyChanged(nameof(MarkAsDone) , nameof(IsStayedUndone));
                } ,
                o =>
                {
                    return !model.IsDone;
                }
                );
                return markAsDone;
            }
        }
        [JsonIgnore]
        private ICommand markAsUnDone;
        [JsonIgnore]
        public ICommand MarkAsUnDone
        {
            get
            {
                if(markAsUnDone == null) markAsUnDone = new ViewModelCommand
                (o =>
                {
                    model.IsDone = false;
                    OnPropertyChanged(nameof(MarkAsUnDone) , nameof(IsStayedUndone));
                } ,
                o =>
                {
                    return model.IsDone;
                }
                );
                return markAsUnDone;
            }
        }

        public TaskViewModel TaskModel { get; }
        #endregion
    }
}
