using Prism.Mvvm;

namespace Northwind.Mvvm
{
    public abstract class EntityViewModel<T>: BindableBase, IEntityViewModel<T> where T : class
    {

#if DEBUG
        private bool _isInDebugMode = true;
#else
                private bool _isInDebugMode = false;
#endif

        private EntityState _state = EntityState.Unchanged;
        public EntityState State 
        { 
            get => _state; 
            set => SetProperty(ref _state, value); 
        }
        public bool IsInDebugMode
        {
            get => _isInDebugMode;
            set => SetProperty(ref _isInDebugMode, value);
        }

    }

}
