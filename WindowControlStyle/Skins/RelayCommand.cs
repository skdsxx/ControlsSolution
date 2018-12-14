#region 文件和作者信息
/******************************************************************

  * CLR 版本: 4.0.30319.42000
  * 机器名称: DESKTOP-L71O6PR
  * 命名空间: WindowControlStyle.Skins
  * 文 件 名: RelayCommand
  * 创建人员: SunXian
  * 创建时间: 2018/11/30 18:14:19

//******************************************************************
* Copyright： @青岛凯亚研发部@
******************************************************************/
#endregion
using System;
using System.Windows.Input;

namespace WindowControlStyle.Skins
{
    public class RelayCommand<T> : ICommand where T : class
    {
        private readonly Action<T> _execute;

        private readonly Func<bool> _canExecute;

        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<T> execute, Func<bool> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute(parameter as T);
        }
    }
}
