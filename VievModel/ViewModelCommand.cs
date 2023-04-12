using System;
using System.Windows.Input;



public class ViewModelCommand : ICommand
{
    //Fields
    private Action<object?> _executeAction;
    private Func<object? , bool> _canExcuteAction;

    //Constuctors
    public ViewModelCommand(Action<object?> executeAction , Func<object? , bool> _canExcuteAction = null)
    {
        //if(_executeAction == null) throw new ArgumentNullException(nameof(executeAction));
        //else
        this._executeAction = executeAction;
        this._canExcuteAction = _canExcuteAction;

    }
    public bool CanExecute(object? parameter)
    {
        if(_canExcuteAction == null) return true;
        else return _canExcuteAction(parameter);
    }

    public void Execute(object? parameter)
    {
        _executeAction(parameter);
    }

    //Events
    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }
}