using ApplicationCore.Entities;

namespace ViewModels
{
    public interface ILegacyWorkspaceContext
    {
        bool IsThereUnsavedEntry();

        void SetCurrentStore(Store store);
    }
}
