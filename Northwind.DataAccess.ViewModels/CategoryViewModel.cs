using Northwind.Mvvm;

namespace Northwind.Data
{
    public class CategoryViewModel(IContainerExtension container) : EntityViewModel<Category>(container)
    {

        private int _categoryId;
        private string _categoryName = string.Empty;
        private string? _description;
        private byte[]? _picture;
        private byte[]? _icon17;
        private byte[]? _icon25;

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

    }
}
