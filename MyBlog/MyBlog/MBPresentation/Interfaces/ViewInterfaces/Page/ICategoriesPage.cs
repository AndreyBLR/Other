using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MBPresentation.ServiceReference;

namespace MBPresentation.Interfaces.ViewInterfaces.Page
{
    public interface ICategoriesPage
    {
        event VoidEventHandler CategoriesReading;
        event VoidEventHandler CategoryCreating;
        event VoidEventHandler CategoryDeleting;
        event VoidEventHandler CategoryReading;
        event VoidEventHandler CategoryUpdating;

        Guid CategoryId { get; set; }
        List<Category> CategoryList { get; set; }
        Category Category { get; set; }
    }
}
