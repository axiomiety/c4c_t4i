using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DataModels;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace PathViewModels
{
    public class StudentsViewModel : INotifyPropertyChanged
    {
        ISchoolService _service;
        public event PropertyChangedEventHandler PropertyChanged;
        bool eventRegistered = false;

        public StudentsViewModel(ISchoolService service)
        {
            this._service = service;
        }

        private void StudentsChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged("Students");
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ISchoolService Service
        {
            get
            {
                return _service;
            }
        }

        public IClass Class
        {
            get
            {
                return _service.Class;
            }
        }

        public IList<IStudent> Students
        {
            get
            {
                if (!eventRegistered)
                {
                    (this.Class.Students as ObservableCollection<IStudent>).CollectionChanged += StudentsChanged;
                    eventRegistered = true;
                }
                return Class.Students;
            }
        }

        public IList<string> StudentNames
        {
            get
            {
                return (from IStudent student in this.Students select student.Name).ToList();
            }
        }

    }
}