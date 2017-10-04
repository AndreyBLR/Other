using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMusic.Core.DataModel.Interfaces
{
    public interface IFileSystemItem
    {
        string Path { get; set; }
        string Name { get; set; }
        ulong Size { get; set; }
        DateTimeOffset DateModified { get; set; }
    }
}
