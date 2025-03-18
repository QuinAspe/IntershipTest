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

        protected async override Task OnInitializedAsync()
        {
            TeamsList = await DataService.GetTeamsAsync();
            await base.OnInitializedAsync();
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
        private async Task SaveAddTeam()
        {
            var result = await TeamService.AddAsync(AddingTeam.MapToTeamAddRequestModel());
            if (result.IsSuccess)
            {
                TeamsList = await DataService.GetTeamsAsync();
            }
            else
            {
                ErrorHandlingService.HandleError(result);
            }
            CloseAddModal();
        }
        private void GoToTeamInfo(int id)
        {
            NavigationManager.NavigateTo($"/teaminfo/{id}");
        }
    }
}
