namespace Northwind.ViewModels
{
    public static class IntExtensions
    {
        public static IntSelectionViewModel ToViewModel(this int? year)
        {
            return new IntSelectionViewModel()
            {
                Value = year,
                DisplayName = year.GetValueOrDefault().ToString(),
            };
        }
        public static int? ToValue(this IntSelectionViewModel viewModel)
        {
            ArgumentNullException.ThrowIfNull(viewModel);
            return viewModel.Value;
        }
    }
}
