using Northwind.Core.Controls;
using Prism.Ioc;

namespace Northwind.Modules.ViewModels
{
    public class SalesByCategoryWidgetViewModel : WidgetBase
    {
        public SalesByCategoryWidgetViewModel(IContainerExtension container) : base(container)
        {
        }
        public SalesByCategoryWidgetViewModel(IContainerExtension container, WidgetSize size) : base(container, size)
        {
        }
        public SalesByCategoryWidgetViewModel(IContainerExtension container, WidgetSize size, string title) : base(container, size,title)
        {
        }
    }
}
