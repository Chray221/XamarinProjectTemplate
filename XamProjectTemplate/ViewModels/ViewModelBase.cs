using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace XamProjectTemplate.ViewModels
{
    [Preserve(AllMembers = true)]
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible, IInitialize
    {
        protected INavigationService NavigationService { get; private set; }
        protected IPageDialogService PageDialogService { get; private set; }


        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        bool _IsLoading;
        public bool IsLoading { get { return _IsLoading; } set { _IsLoading = value; RaisePropertyChanged(nameof(IsLoading)); } }
        bool _IsClicked;
        public bool IsClicked { get { return _IsClicked; } set { _IsClicked = value; RaisePropertyChanged(nameof(IsClicked)); } }

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public ViewModelBase(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            NavigationService = navigationService;
            PageDialogService = pageDialogService;
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }

        public virtual void OnBackButtonPressed()
        {

        }

        public async Task NavigateToAsync(Type page, INavigationParameters parameters = null, bool isModal = false, bool isAnimated = true)
        {
            if (!IsClicked)
            {
                IsClicked = true;
                App.Log($"Navigate To: {page.Name}");
                var result = await NavigationService?.NavigateAsync(page.Name, parameters: parameters, useModalNavigation: isModal, isAnimated);
                if (!result.Success)
                {
                    HandleNavigationResult(result);
                }
                IsClicked = false;
            }
        }
        protected void HandleNavigationResult(INavigationResult result)
        {
            if (!result.Success)
            {
                switch (result.Exception)
                {
                    case ContainerResolutionException crException:
                        HandleContainerException(crException);
                        break;
                    case NavigationException ne when ne.InnerException is ContainerResolutionException cre:
                        HandleContainerException(cre);
                        break;
                    case NavigationException ne:
                        //HandleNavigationException(ne);
                        App.LogException(ne);
                        break;
                    default:
                        // Report a bug to the Prism Team
                        App.LogException(result.Exception);
                        break;
                }
            }
        }

        private void HandleContainerException(ContainerResolutionException cre)
        {
            // NOTE: This is not a cheap function to execute... This will recurse dependencies...
            // Multiple exceptions may be thrown and caught
#if DEBUG
            // returns IDictionary<Type, Exception>
            var errors = cre.GetErrors();
            errors.ForEach((keyPair) =>
            {
                var type = keyPair.Key;
                var ex = keyPair.Value;
                App.Log($"Could not resolve {type.Name}.\n{ex.GetType()} - {ex.Message}");

            });
#endif
        }

        public async void NavigateTo(Type page, INavigationParameters parameters = null, bool isModal = false)
        {
            await NavigateToAsync(page, parameters, isModal);
        }

        public async void NavigateTo(string path, INavigationParameters parameters = null, bool isModal = false, bool isAnimated = true)
        {
            if (!IsClicked)
            {
                IsClicked = true;
                App.Log($"Navigate To: {path}");
                var result = await NavigationService?.NavigateAsync(path, parameters, useModalNavigation: isModal, isAnimated);
                if (!result.Success)
                {
                    //App.LogException(result.Exception);
                    HandleNavigationResult(result);
                }
                IsClicked = false;
            }
        }

        public async Task GoBackAsync(INavigationParameters parameters = null, bool isModal = false, bool isAnimated = true)
        {
            if (!IsClicked)
            {
                IsClicked = true;
                App.Log($"Go Back ");
                var result = await NavigationService?.GoBackAsync(parameters, useModalNavigation: isModal, isAnimated);
                if (!result.Success)
                {
                    //App.LogException(result.Exception);
                    HandleNavigationResult(result);
                }
                IsClicked = false;
            }
        }

        public async void GoBack(INavigationParameters parameters, bool isModal = false)
        {
            await GoBackAsync(parameters, isModal);
        }

        public async void GoBack(bool isModal = false)
        {
            await GoBackAsync(null, isModal);
        }

        public async Task DisplayAlertAsync(string title, string message, string cancelButton)
        {
            await DisplayAlertAsync(title, message, null, cancelButton);
        }

        public async void DisplayAlert(string title, string message, string cancelButton)
        {
            await DisplayAlertAsync(title, message, null, cancelButton);
        }

        public async Task<bool> DisplayAlertAsync(string title, string message, string acceptButton, string cancelButton)
        {
            if (PageDialogService != null)
            {
                return await PageDialogService.DisplayAlertAsync(title, message, acceptButton, cancelButton);
            }
            else
            {
                App.Log("PageDialogService is null");
            }
            return false;
        }

        public virtual void Initialize(INavigationParameters parameters)
        {

        }

        public async Task OnClickedAsync(Func<Task> action)
        {
            if (!IsClicked)
            {
                IsClicked = true;
                await action.Invoke();
                IsClicked = false;
            }
        }
        public async void OnClicked(Func<Task> action)
        {
            await OnClickedAsync(action);
        }
    }
}
