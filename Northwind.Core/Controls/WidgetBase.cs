using Northwind.Mvvm;
using Prism.Ioc;

namespace Northwind.Core.Controls
{
    public class WidgetBase : ViewModelBase
    {

        private string _title = string.Empty;
        private int _widgetId;

        private int _rowIndex;
        private int _columnIndex;

        private WidgetSize _size;

        public WidgetBase(IContainerExtension container) : base(container)
        {
        }

        public WidgetBase(IContainerExtension container, WidgetSize size) : base(container)
        {
            Size = size;
        }
        public WidgetBase(IContainerExtension container, WidgetSize size, string title) : this(container,size)
        {
            Title = title;
        }
        public int WidgetId
        {
            get => _widgetId;
            set => SetProperty(ref _widgetId, value);
        }
        public int RowIndex
        {
            get => _rowIndex;
            set => SetProperty(ref _rowIndex, value);
        }
        public int ColumnIndex
        {
            get => _columnIndex;
            set => SetProperty(ref _columnIndex, value);
        }
        public WidgetSize Size
        {
            get => _size;
            set => SetProperty(ref _size, value);
        }
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

    }
}
