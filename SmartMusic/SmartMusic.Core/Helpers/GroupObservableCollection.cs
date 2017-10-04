using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Windows.UI.Xaml.Data;

namespace SmartMusic.Core.Helpers
{
    [CollectionDataContract]
    public class GroupObservableCollection<T> : ObservableCollection<T>
    {
        /// <summary>
        /// The Group Title
        /// </summary>
        [DataMember]
        public char Key { get; set; }

        /// <summary>
        /// Constructor ensure that a Group Title is included
        /// </summary>
        /// <param name="name">string to be used as the Group Title</param>
        public GroupObservableCollection(char name)
        {
            this.Key = name;
        }
    }
}
