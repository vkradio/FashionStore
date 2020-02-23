using ApplicationCore.Entities;

namespace ViewModels
{
    public interface ILegacyWorkspaceContext
    {
        bool IsThereUnsavedEntry();

        void SetCurrentWarehouse(Warehouse warehouse);
    }
}
