#region 文件和作者信息
/******************************************************************

  * CLR 版本: 4.0.30319.42000
  * 机器名称: SUNXIAN
  * 命名空间: ComboBoxMultipleControl
  * 文 件 名: ComboBoxExViewModel
  * 创建人员: SunXian
  * 创建时间: 2017/7/11 10:34:27
  
//******************************************************************
* Copyright： @青岛凯亚研发部@
******************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ComboBoxMultipleControl.Annotations;

namespace ComboBoxMultipleControl.ViewModel
{
    public class ComboBoxExViewModel:INotifyPropertyChanged
    {
        private static ComboBoxExViewModel _instance;

        public static ComboBoxExViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ComboBoxExViewModel();
                }
                return _instance;
            }
        }


        public ObservableCollection<Student> StudentInfos { get; set; }

        #region 选中的项
        private List<Student> _SelectedItems;
        public List<Student> SelectedItems
        {
            get { return _SelectedItems; }
            set
            {
                if (PropertyChanged != null)
                {
                    _SelectedItems = value;
                    this.PropertyChanged(this, new PropertyChangedEventArgs("SelectedItems"));
                }
            }
        }
        #endregion

        #region 选中内容展示
        private string _selectedContent;
        public string SelectedContent
        {
            get { return _selectedContent; }
            set
            {
                if (_selectedContent != value)
                {
                    _selectedContent = value;
                    OnPropertyChanged(nameof(SelectedContent));
                }
            }
        }
        #endregion
        public ICommand CheckItemsChangedCommand { get; set; }


        public string SelectedStr { get; set; }

        private ComboBoxExViewModel()
        {
            StudentInfos = new ObservableCollection<Student>();
            SelectedItems = new List<Student>();

            CheckItemsChangedCommand = new RelayCommand(UpdateContent);

            StudentInfos.Add(new Student { Id = Guid.NewGuid(),Code="001", Name = "张可", IsChecked = false });
            StudentInfos.Add(new Student { Id = Guid.NewGuid(), Code = "002", Name = "工业股", IsChecked = false });
            StudentInfos.Add(new Student { Id = Guid.NewGuid(), Code = "003", Name = "余量", IsChecked = false });
            StudentInfos.Add(new Student { Id = Guid.NewGuid(), Code = "004", Name = "发热发", IsChecked = false });
            StudentInfos.Add(new Student { Id = Guid.NewGuid(), Code = "005", Name = "吕俊杰", IsChecked = false });
            StudentInfos.Add(new Student { Id = Guid.NewGuid(), Code = "006", Name = "望东方", IsChecked = false });
            StudentInfos.Add(new Student { Id = Guid.NewGuid(), Code = "007", Name = "发热为儿", IsChecked = false });
            StudentInfos.Add(new Student { Id = Guid.NewGuid(), Code = "008", Name = "他让个人头", IsChecked = false });
            StudentInfos.Add(new Student { Id = Guid.NewGuid(), Code = "009", Name = "国旅股份", IsChecked = false });
        }


        private void UpdateContent()
        {
            SelectedContent = string.Empty;
            foreach (var item in SelectedItems)
            {
                SelectedContent = string.Format("{0};{1}/{2}", SelectedContent, item.Code, item.Name);
            }
        }


        #region PropertyChanged属性
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
