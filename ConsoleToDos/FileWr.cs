using Newtonsoft.Json;
using System.Collections.ObjectModel;
using ToDoLogic.Model;

namespace ConsoleToDos
{

    public static class FileWr
    {
        static string filePath = @"tasks.json";

        public static void Serialize(List<TaskModelForConsole> duties)
        {
            string json = JsonConvert.SerializeObject(duties);
            File.WriteAllText(filePath , json);
        }
        public static List<TaskModelForConsole> DeserializeTasks()
        {
            List<TaskModelForConsole> tasks = new List<TaskModelForConsole>();
            if(File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                tasks = JsonConvert.DeserializeObject<List<TaskModelForConsole>>(json);
            }
            return tasks;
        }

    }

    public static class WpfFileWR
    {
        public static void SaveWpf<T>(ObservableCollection<T> taskList , string filePath)
        {
            var list = taskList.ToList();
            var json = JsonConvert.SerializeObject(list);
            File.WriteAllText(filePath , json);
        }

        public static ObservableCollection<T> DeserializeObs<T>(string filePath)
        {
            var json = File.ReadAllText(filePath);
            var list = JsonConvert.DeserializeObject<List<T>>(json);
            return new ObservableCollection<T>(list);
        }
    }
}