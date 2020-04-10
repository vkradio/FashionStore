using ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using MvvmInfrastructure;
using System.Threading.Tasks;

namespace ViewModels
{
    public class WorkspaceViewModel : PropertyChangeNotifier
    {
        readonly IDialogService dialogService;
        readonly ILocalizationService localizationService;
        readonly IStoreManagementService storeManagementService;
        readonly ILegacyWorkspaceContext legacyWorkspaceContext;

        bool initialized;

        #region Private underlying fields
        StoreSelectorViewModel storeSelector;
        #endregion

        #region Public properties and actions
        public StoreSelectorViewModel StoreSelector
        {
            get => storeSelector;

            set
            {
                if (value != storeSelector)
                {
                    storeSelector = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsItAllowedToChangeCurrentStore()
        {
            if (!initialized)
            {
                return true;
            }

            var unsavedEntry = legacyWorkspaceContext.IsThereUnsavedEntry();
            if (unsavedEntry)
            {
                var userReply = dialogService.PresentDialog(localizationService.AskProceedAndAbandonFormData, DialogOptionsEnum.YesNoCancel);
                return userReply == DialogResultEnum.Yes;
            }
            else
            {
                return true;
            }
        }
        #endregion

        public WorkspaceViewModel(
            IDialogService dialogService,
            ILocalizationService localizationService,
            IStoreManagementService storeManagementService,
            ILegacyWorkspaceContext legacyWorkspaceContext
        )
        {
            Guard.Against.Null(dialogService, nameof(dialogService));
            Guard.Against.Null(localizationService, nameof(localizationService));
            Guard.Against.Null(storeManagementService, nameof(storeManagementService));
            Guard.Against.Null(legacyWorkspaceContext, nameof(legacyWorkspaceContext));

            this.dialogService = dialogService;
            this.localizationService = localizationService;
            this.storeManagementService = storeManagementService;
            this.legacyWorkspaceContext = legacyWorkspaceContext;

            storeSelector = new StoreSelectorViewModel(this, this.storeManagementService);
            OnPropertyChanged(nameof(StoreSelector));
        }

        public async Task Initialize()
        {
            await StoreSelector.Initialize().ConfigureAwait(false);
            initialized = true;
        }
    }
}
