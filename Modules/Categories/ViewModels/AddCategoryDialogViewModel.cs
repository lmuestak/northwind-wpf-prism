using Northwind.DataAccess;
using Prism.Commands;
using Prism.Dialogs;
using Prism.Ioc;

namespace Northwind.ViewModels
{
    //public class AddCategoryDialogViewModel : CategoryViewModel, IDialogAware
    //{
    //    public AddCategoryDialogViewModel(IContainerExtension container) : base(container)
    //    {
    //    }

    //    public DialogCloseListener RequestClose { get; }
    //    public bool CanCloseDialog() => true;
    //    public void OnDialogClosed() { }
    //    public void OnDialogOpened(IDialogParameters parameters)
    //    {
    //        if (parameters.ContainsKey("categoryId"))
    //        {
    //            //var categoryId = parameters.GetValue<int>("categoryId");
    //            //var category = UnitOfWork.Find(categoryId);
    //            //if (category != null)
    //            //{
    //            //    Model = category;
    //            //}
    //        }
    //    }
    //    public DelegateCommand CancelCommand { get; }

    //    //RequestClose.Invoke(new DialogResult() { Result = ButtonResult.OK });

    //}
}
