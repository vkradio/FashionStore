﻿using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using MvvmInfrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class WorkspaceViewModel : PropertyChangeNotifier
    {
        readonly IDialogService dialogService;
        readonly IStoreManagementService storeManagementService;
        readonly ILegacyWorkspaceContext legacyWorkspaceContext;

        #region Private underlying fields
        StoreSelectorViewModel storeSelector;
        Store currentStore;
        #endregion

        #region Public properties and actions
        public StoreSelectorViewModel StoreSelector
        {
            get => storeSelector;

            set
            {
                storeSelector = value;
                OnPropertyChanged(nameof(StoreSelector));
            }
        }

        public Store CurrentStore
        {
            get => currentStore;

            set
            {
                currentStore = value;
                OnPropertyChanged(nameof(CurrentStore));
            }
        }

        public bool IsItAllowedToChangeCurrentStore()
        {
            return false;
        }
        #endregion

        public WorkspaceViewModel(IDialogService dialogService, IStoreManagementService storeManagementService, ILegacyWorkspaceContext legacyWorkspaceContext)
        {
            Guard.Against.Null(dialogService, nameof(dialogService));
            Guard.Against.Null(storeManagementService, nameof(storeManagementService));
            Guard.Against.Null(legacyWorkspaceContext, nameof(legacyWorkspaceContext));

            this.dialogService = dialogService;
            this.storeManagementService = storeManagementService;
            this.legacyWorkspaceContext = legacyWorkspaceContext;

            StoreSelector = new StoreSelectorViewModel(this, this.storeManagementService);
            StoreSelector.PropertyChanged += async (s, e) =>
            {
                if (e.PropertyName == nameof(StoreSelectorViewModel.SelectedStore))
                    CurrentStore = await storeManagementService.GetStore(StoreSelector.SelectedStore?.StoreId ?? -1).ConfigureAwait(false);
            };
        }

        public async Task Initialize() => await StoreSelector.Initialize().ConfigureAwait(false);
    }
}