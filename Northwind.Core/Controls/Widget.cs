using System;

namespace Northwind.Core.Controls
{
    public class Widget
    {
        private readonly Func<WidgetBase> _createWidget;
        private string Name { get; set; } = string.Empty;
        private string Description { get; set; } = string.Empty;
        public Widget(string name, string description, Func<WidgetBase> createWidget)
        {
            Name = name;
            Description = description;
            _createWidget = createWidget;
        }
        public WidgetBase CreateWidget()
        {
            return _createWidget.Invoke();
        }
    }
}
