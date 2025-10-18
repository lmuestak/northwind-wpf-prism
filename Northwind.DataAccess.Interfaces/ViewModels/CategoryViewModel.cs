using Northwind.Data;
using Northwind.Mvvm;
using System.Collections.ObjectModel;

namespace Northwind.ViewModels
{


    public class CategoryViewModel : EntityViewModel<Category>
    {

        private int? _categoryId;
        private string _categoryName = string.Empty;
        private string? _description;
        private byte[]? _picture;
        private byte[]? _icon17;
        private byte[]? _icon25;
        private ObservableCollection<ProductViewModel>? _products;

        public int? CategoryId
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
            get => _picture ?? [];
            set => SetProperty(ref _picture, value);
        }
        public byte[]? Icon17
        {
            get => _icon17 ?? [];
            set => SetProperty(ref _icon17, value);
        }
        public byte[]? Icon25
        {
            get => _icon25 ?? [];
            set => SetProperty(ref _icon25, value);
        }
        public ObservableCollection<ProductViewModel> Products
        {
            get => _products ??= [];
            set => SetProperty(ref _products, value);
        }
    }
}
