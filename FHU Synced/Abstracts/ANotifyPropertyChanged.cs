using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FHU_Synced.Abstracts
{
    public abstract class ANotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        virtual protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null )
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); 
        }

        virtual protected bool AssignAndNotifyPropertyChanged<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if ((field == null && value != null) || (field != null && !field.Equals(value)))
            {
                field = value;
                this.NotifyPropertyChanged(propertyName);

                return true;
            }

            return false;
        }
    }
}
