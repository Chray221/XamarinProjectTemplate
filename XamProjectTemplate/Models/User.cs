using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace XamProjectTemplate.Models
{
    public class User : ModelBase
    {
        int _Id;
        public int Id
        {
            get { return _Id; }
            set { SetProperty(ref _Id, value, nameof(Id)); }
        }
    }
}
