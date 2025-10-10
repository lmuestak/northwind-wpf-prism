using Northwind.Data;
using Northwind.Mvvm;
using Prism.Ioc;

namespace Northwind.Modules.ViewModels
{
    public class OrderViewModel : EntityViewModel<Order>
    {
        private int _categoryId;
        private string _categoryName;
        private string? _description;
        private byte[]? _picture;
        private byte[]? _icon17;
        private byte[]? _icon25;
        private Category _model;

        public OrderViewModel(IContainerExtension container) : base(container) { }

        public int CategoryId
        {
            get => _categoryId;
            set => SetProperty(ref _categoryId, value);
        }
        public string CategoryName
        {
            get => _categoryName;
            set => SetProperty(ref _categoryName, value);
        }
        public string? Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        public byte[]? Picture
        {
            get => _picture;
            set => SetProperty(ref _picture, value);
        }
        public byte[]? Icon17
        {
            get => _icon17;
            set => SetProperty(ref _icon17, value);
        }
        public byte[]? Icon25
        {
            get => _icon25;
            set => SetProperty(ref _icon25, value);
        }
        public override Category Model
        {
            get => _model;
            set
            {
                if (SetProperty(ref _model, value))
                {
                    if (value != null)
                    {
                        CategoryId = value.CategoryId;
                        CategoryName = value.CategoryName;
                        Description = value.Description;
                        Picture = value.Picture;
                        Icon17 = value.Icon17;
                        Icon25 = value.Icon25;
                    }
                }
            }
        }

    }
}
