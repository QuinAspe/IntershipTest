using IntershipTest.Core.Entities;
using IntershipTest.Core.Services.Models;
using IntershipTest.Web.Mapping;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace IntershipTest.Web.Components.Pages
{
    public partial class TeamInfo
    {
        [Parameter]
        public int TeamId { get; set; }


        private Team SelectedTeam;
        private Team EditingTeam = new();
        private bool IsEditModalOpen = false;


        protected override async Task OnInitializedAsync()
        {
            SelectedTeam = await DataService.GetTeamAsync(TeamId);
            await base.OnInitializedAsync();
        }
        private void GoToPlayerInfo(int id)
        {
            NavigationManager.NavigateTo($"/playerinfo/{id}");
        }
        private async Task DeleteTeam(int id)
        {
            if (await JsRuntime.InvokeAsync<bool>("confirm", "Weet je zeker dat je dit team wilt verwijderen \n Alle spelers in het team worden ook verwijderd"))
            {
                var result = await TeamService.DeleteAsync(id);
                if (result.IsSuccess)
                {
                    NavigationManager.NavigateTo($"/teams");
                }
                else
                {
                    ErrorHandlingService.HandleError(result);
                }
            }
        }
        private async Task SaveEditTeam()
        {
            var result = await TeamService.UpdateAsync(EditingTeam.MapToPlayerUpdateRequestModel());
            if (result.IsSuccess)
            {
                SelectedTeam = EditingTeam;
                StateHasChanged();
            }
            else
            {
                ErrorHandlingService.HandleError(result);
            }
            CloseEditModal();
        }
        private void OpenEditModal(Team team)
        {
            EditingTeam = new Team
            {
                Id = team.Id,
                Name = team.Name,
                Players = team.Players
            };
            IsEditModalOpen = true;
        }
        private void CloseEditModal()
        {
            IsEditModalOpen = false;
            StateHasChanged();
        }
    }
}
