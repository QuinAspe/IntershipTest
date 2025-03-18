using IntershipTest.Core.Entities;
using IntershipTest.Core.Services;
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
            GetTeams();
            await base.OnInitializedAsync();
        }
        private async void GetTeams()
        {
            var getTeams = await TeamService.GetAllAsync();
            if (!getTeams.IsSuccess)
            {

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
                //errors
            }
            CloseAddModal();
        }
        private void GoToTeamInfo(int id)
        {
            NavigationManager.NavigateTo($"/teaminfo/{id}");
        }
    }
}
