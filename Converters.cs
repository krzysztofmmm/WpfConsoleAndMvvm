using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using ToDoLogic.Model;

namespace WPFToDolist
{
    class Converters : IValueConverter
    {
        public Brush ColorForFalse { get; set; } = Brushes.Black;
        public Brush ColorForTrue { get; set; } = Brushes.Gray;
        public object Convert(object value , Type targetType , object parameter , CultureInfo culture)
        {
            bool b = (bool)value;
            return !b ? ColorForFalse : ColorForTrue;
        }

        public object ConvertBack(object value , Type targetType , object parameter , CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

    public class TaskConverter : IMultiValueConverter
    {
        public object Convert(object[] values , Type targetType , object parameter , CultureInfo culture)
        {
            string duty = (string)values[0];
            DateTime creationDate = DateTime.Now;
            DateTime? date = (DateTime)values[1];
            PriorityLevel priority = (PriorityLevel)(int)values[2];
            if(!string.IsNullOrWhiteSpace(duty) && date.HasValue)

                return new VievModel.TaskViewModel(duty , creationDate , date.Value , priority , false);
            else return null;

        }

        public object[] ConvertBack(object value , Type[] targetTypes , object parameter , CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}