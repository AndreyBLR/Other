using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcApp
{
    public interface IModel
    {
        void Create();
        void Read(Guid id);
        void Update();
        void Delete();
    }
}
