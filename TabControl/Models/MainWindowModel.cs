#region 文件和作者信息
/******************************************************************

  * CLR 版本: 4.0.30319.42000
  * 机器名称: SUNXIAN
  * 命名空间: TabControl.Models
  * 文 件 名: MainWindowModel
  * 创建人员: SunXian
  * 创建时间: 2017/5/3 16:43:46
  
//******************************************************************
* Copyright： @青岛凯亚研发部@
******************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TabControl.Annotations;

namespace TabControl.Models
{
    public class MainWindowModel:INotifyPropertyChanged
    {
        public MainWindowModel()
        {
            SelectedTabItem=new TabItem();
        }

        private TabItem _selectedTabItem;
        public TabItem SelectedTabItem
        {
            get { return _selectedTabItem; }
            set
            {
                if (value != _selectedTabItem)
                {
                    _selectedTabItem= value;
                    OnPropertyChanged(nameof(SelectedTabItem));
                }
            }
        }

        //private bool _isMove;

        //public bool IsMove
        //{
        //    get { return _isMove; }
        //    set
        //    {
        //        if (value != _isMove)
        //        {
        //            _isMove = value;
        //            OnPropertyChanged(nameof(IsMove));
        //        }
        //    }
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
