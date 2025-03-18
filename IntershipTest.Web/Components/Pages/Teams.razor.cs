using IntershipTest.Core.Entities;
using IntershipTest.Core.Services;
using IntershipTest.Core.Services.Models;
using IntershipTest.Web.Mapping;
using Microsoft.JSInterop;

namespace IntershipTest.Web.Components.Pages
{
    public partial class Teams
    {
        private List<Team> TeamsList = new ();
        private Team EditingTeam = new();
        private Team AddingTeam = new();
        private bool IsAddModalOpen = false;
        private bool ShowError = false;
        private string ErrorMessage = string.Empty;
        protected async override Task OnInitializedAsync()
        {
            GetTeams();
            await base.OnInitializedAsync();
        }
        private async void GetTeams()
        {
            var getTeams = await TeamService.GetAllAsync();
            if (!getTeams.IsSuccess)
            {
                HandleError(getTeams);
            }
            else
            {
                TeamsList = getTeams.Value.ToList();
                StateHasChanged();
            }
        }
        private void OpenAddModal()
        {
            AddingTeam = new Team();
            IsAddModalOpen = true;
        }
        private void CloseAddModal()
        {
            IsAddModalOpen = false;
            StateHasChanged();
        }
        private async void SaveAddTeam()
        {
            var result = await TeamService.AddAsync(AddingTeam.MapToTeamAddRequestModel());
            if (result.IsSuccess)
            {
                GetTeams();
            }
            else
            {
                HandleError(result);
            }
            CloseAddModal();
        }
        private void GoToTeamInfo(int id)
        {
            NavigationManager.NavigateTo($"/teaminfo/{id}");
        }
        private void HandleError<T>(ResultModel<T> resultModel)
        {
            if (resultModel.Errors.Any())
            {
                ShowError = true;
                ErrorMessage = resultModel.Errors.FirstOrDefault();
            }
        }
    }
}
