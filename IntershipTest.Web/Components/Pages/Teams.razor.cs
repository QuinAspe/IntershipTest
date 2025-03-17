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
        private bool IsEditModalOpen = false;
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
        private void OpenEditModal(Team team)
        {
            EditingTeam = new Team
            {
                Id = team.Id,
                Name = team.Name,
            };
            IsEditModalOpen = true;
        }
        private void CloseEditModal()
        {
            IsEditModalOpen = false;
            StateHasChanged();
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
        private async void SaveEditTeam()
        {
            var result = await TeamService.UpdateAsync(EditingTeam.MapToPlayerUpdateRequestModel());
            if (result.IsSuccess)
            {
                GetTeams();
            }
            else
            {
                //errors
            }
            CloseEditModal();
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
        private async void DeleteTeam(int id)
        {
            if (await JsRuntime.InvokeAsync<bool>("confirm", "Weet je zeker dat je dit team wilt verwijderen?"))
            {
                var result = await TeamService.DeleteAsync(id);
                if (result.IsSuccess)
                {
                    GetTeams();
                }
                else
                {
                    //errors
                }
            }
        }
    }
}
