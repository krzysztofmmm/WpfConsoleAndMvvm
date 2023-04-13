using ConsoleToDos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Windows.Input;
using ToDoLogic.Model;

namespace WPFToDolist.VievModel
{
    //TA KLASA JEST DO OBLSUGI OGOLU ZADAN!!!!!
    public class SortOption
    {
        public string? Name { get; set; }
        public string? CommandParameter { get; set; }
    }

    public class TasksViewModels : ObservedObj
    {
        private Calendar model;
        public ObservableCollection<TaskViewModel> TasksList { get; set; } = new ObservableCollection<TaskViewModel>();



        private DateTime startDate = DateTime.Now;
        public DateTime NewDate
        {
            get { return startDate; }
            set { startDate = value; OnPropertyChanged("NewDate"); }
        }

        private string newDuty;
        public string NewDuty
        {
            get { return newDuty; }
            set { newDuty = value; OnPropertyChanged("NewDuty"); }
        }


        private PriorityLevel newPriority;
        public PriorityLevel NewPriority
        {
            get { return newPriority; }
            set { newPriority = value; OnPropertyChanged("NewPriority"); }
        }

        public void Load()
        {
            TasksList = WpfFileWR.DeserializeObs<TaskViewModel>("tasks.json");
        }



        public TasksViewModels()
        {
            if(File.Exists("tasks.json"))
            {
                Load();
            }
            else
            {
                return;
            }
        }
        #region do przemyslenia
        private void CopyTasksFromModel()
        {
            TasksList.Clear();
            TasksList.CollectionChanged -= ModelSync;
            foreach(TaskModel taskModel in model)
            {
                TasksList.Add(new TaskViewModel(taskModel));
            }
            TasksList.CollectionChanged += ModelSync;
        }
        public void ModelSync(object? sender , NotifyCollectionChangedEventArgs e)
        {
            switch(e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    TaskViewModel newTask = (TaskViewModel)e.NewItems[0];
                    if(newTask != null)
                    {
                        model.AddTask(newTask.GetModel());
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    TaskViewModel deletedTask = (TaskViewModel)e.OldItems[0];
                    if(deletedTask != null)
                    {
                        model.RemoveTask(deletedTask.GetModel());
                    }
                    break;
            }
        }
        #endregion

        private ICommand save;
        public ICommand Save
        {
            get
            {
                if(save == null)
                {
                    save = new ViewModelCommand(o => WpfFileWR.SaveWpf(TasksList , "tasks.json"));
                }
                return save;
            }
        }



        private ICommand? deleteTask;

        public ICommand DeleteTask
        {
            get
            {
                if(deleteTask == null) deleteTask = new ViewModelCommand(
                o =>
                {
                    int id = (int)o;
                    TaskViewModel task = TasksList[id];
                    TasksList.Remove(task);
                } ,
                o =>
                {
                    if(o == null) return false;
                    int id = (int)o;
                    return id >= 0;
                }
                );
                return deleteTask;
            }
        }





        private ICommand addTask;

        public ICommand AddTask
        {
            get
            {
                if(addTask == null) addTask = new ViewModelCommand(
                o =>
                {

                    TaskViewModel duty = o as TaskViewModel;
                    if(duty != null) TasksList.Add(duty);

                } ,
                o =>
                {
                    return (o as TaskViewModel) != null;
                }
                );
                return addTask;
            }
        }


        public ObservableCollection<SortOption> SortOptions { get; } = new ObservableCollection<SortOption>
        {
        new SortOption { Name = "Date (Ascending)", CommandParameter = "DateAsc" },
        new SortOption { Name = "Date (Descending)", CommandParameter = "DateDesc" },
        new SortOption { Name = "Priority (Ascending)", CommandParameter = "PriorityAsc" },
        new SortOption { Name = "Priority (Descending)", CommandParameter = "PriorityDesc" },
        };

        private SortOption _selectedSortOption;
        public SortOption SelectedSortOption
        {
            get => _selectedSortOption;
            set
            {
                if(_selectedSortOption != value)
                {
                    _selectedSortOption = value;
                    SortCommand.Execute(_selectedSortOption.CommandParameter);
                }
            }
        }

        private ICommand? sortCommand;
        public ICommand SortCommand
        {
            get
            {
                if(sortCommand == null)
                {
                    sortCommand = new ViewModelCommand(
                        o =>
                        {
                            string sortType = o as string;
                            if(sortType != null)
                            {
                                IEnumerable<TaskViewModel> sortedTasks = null;
                                switch(sortType)
                                {
                                    case "DateAsc":
                                        sortedTasks = TasksList.OrderBy(t => t.Date);
                                        break;
                                    case "DateDesc":
                                        sortedTasks = TasksList.OrderByDescending(t => t.Date);
                                        break;
                                    case "PriorityAsc":
                                        sortedTasks = TasksList.OrderBy(t => t.Priority);
                                        break;
                                    case "PriorityDesc":
                                        sortedTasks = TasksList.OrderByDescending(t => t.Priority);
                                        break;
                                }
                                if(sortedTasks != null)
                                {
                                    var tempTasksList = new ObservableCollection<TaskViewModel>(sortedTasks);
                                    for(int i = 0;i < tempTasksList.Count;i++)
                                    {
                                        int oldIndex = TasksList.IndexOf(tempTasksList[i]);
                                        if(oldIndex != i)
                                            TasksList.Move(oldIndex , i);
                                    }
                                }
                            }
                        } ,
                        o => (o as string) != null
                    );
                }
                return sortCommand;
            }

        }


        private ICommand editTask;
        public ICommand EditTask
        {
            get
            {
                if(editTask == null) editTask = new ViewModelCommand(
                  o =>
                  {
                      int id = (int)o;
                      TaskViewModel task = TasksList[id];
                      task.GetModel().Date = NewDate;
                      task.GetModel().Duty = NewDuty;
                      task.GetModel().Priority = NewPriority;

                  } ,
                  o =>
                  {
                      if(o == null) return false;
                      int taskIndex = (int)o;
                      return taskIndex >= 0;
                  });
                return editTask;

            }
        }
    }
}