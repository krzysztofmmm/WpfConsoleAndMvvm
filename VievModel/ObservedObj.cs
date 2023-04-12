using System.ComponentModel;

public abstract class ObservedObj : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(params string[] propNames)
    {
        if(PropertyChanged != null)
        {
            foreach(string propName in propNames)
                PropertyChanged(this , new PropertyChangedEventArgs(propName));
        }
    }
}