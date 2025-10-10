using Prism.Ioc;

namespace Northwind.Mvvm
{
    public abstract class EntityViewModel<T>(IContainerExtension container) : ViewModelBase(container), IEntityViewModel<T> where T : class
    {
        private EntityState _state = EntityState.Unchanged;
        public EntityState State 
        { 
            get => _state; 
            set => SetProperty(ref _state, value); 
        }
    }

}
