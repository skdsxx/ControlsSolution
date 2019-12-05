using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WindowControlStyle.Resources
{
    public class Language : INotifyPropertyChanged
    {

        private string _languageCode;
        private string _languageName;
        private string _languageDisplayName;
        private string _resourcefile;
        private bool _languageenabled;
        private ResourceDictionary _resource;

        /// <summary>
        /// 语言代码  如en,zh
        /// </summary>
        public string LanguageCode
        {
            get { return _languageCode; }
            set
            {
                _languageCode = value; 
                OnPropertyChanged("LanguageCode");
            }
        }
        /// <summary>
        /// 语言名称  如:中国 ,English
        /// </summary>
        public string LanguageName
        {
            get { return _languageName; }
            set
            { 
                _languageName = value;
                OnPropertyChanged("LanguageName");
            }
        }
        /// <summary>
        /// 语言名称  如:中国 ,English
        /// </summary>
        public string LanguageDisplayName
        {
            get { return _languageDisplayName; }
            set
            {
                _languageDisplayName = value; 
                OnPropertyChanged("LanguageDisplayName");
            }
        }

        /// <summary>
        /// 语言资源文件地址 
        /// </summary>
        public string Resourcefile
        {
            get { return _resourcefile; }
            set
            { 
                _resourcefile = value; 
                OnPropertyChanged("Resourcefile"); 
            }
        }

        /// <summary>
        /// 是否启用此语言配置文件
        /// </summary>
        public bool LanguageEnabled
        {
            get { return _languageenabled; }
            set 
            { 
                _languageenabled = value;
                OnPropertyChanged("LanguageEnabled"); 
            }
        }

        /// <summary>
        /// 系统资源字典
        /// </summary>

        public ResourceDictionary Resource
        {
            get { return _resource; }
            set 
            {
                _resource = value;
                OnPropertyChanged("Resource"); 
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
