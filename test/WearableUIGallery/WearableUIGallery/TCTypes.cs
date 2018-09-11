using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace WearableUIGallery
{
    public class TCTypes : ObservableCollection<object>
    {
        public TCTypes()
        {
        }
        public TCTypes(Type type)
        {
            Add(type);
        }

        public static implicit operator Type(TCTypes type)
        {
            if (type.Count > 0)
            {
                return type[0] as Type;
            }
            else
            {
                return null;
            }
        }

        public static implicit operator TCTypes(Type type)
        {
            return new TCTypes(type);
        }
    }
}
