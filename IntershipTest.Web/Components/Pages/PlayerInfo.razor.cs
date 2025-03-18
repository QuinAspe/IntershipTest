using IntershipTest.Core.Entities;
using IntershipTest.Core.Services;
using IntershipTest.Core.Services.Models;
using IntershipTest.Web.Mapping;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace IntershipTest.Web.Components.Pages
{
    public partial class PlayerInfo
    {
        [Parameter]
        public int PlayerId { get; set; }

        public Player SelectedPlayer;
        private List<Team> Teams = new();
        private Player EditingPlayer = new();
        private Player AddingPlayer = new();
        private bool IsEditModalOpen = false;

        protected override async Task OnInitializedAsync()
        {
            SelectedPlayer = await DataService.GetPlayerAsync(PlayerId);
            Teams = await DataService.GetTeamsAsync();
            await base.OnInitializedAsync();
        }
        private void GoToTeamInfo(int id)
        {
            NavigationManager.NavigateTo($"/teaminfo/{id}");
        }
        private void OpenEditModal(Player player)
        {
            EditingPlayer = new Player
            {
                Id = player.Id,
                FirstName = player.FirstName,
                LastName = player.LastName,
                DateOfBirth = player.DateOfBirth,
                TeamId = player.TeamId
            };
            IsEditModalOpen = true;
        }
        private void CloseEditModal()
        {
            IsEditModalOpen = false;
        }
        private async Task SaveEditPlayer()
        {
            var result = await PlayerService.UpdateAsync(EditingPlayer.MapToPlayerUpdateRequestModel());
            if (result.IsSuccess)
            {
                SelectedPlayer = null;
                NavigationManager.NavigateTo($"/PlayerInfo/{EditingPlayer.Id}",true);
            }
            else
            {
                ErrorHandlingService.HandleError(result);
            }
            CloseEditModal();
        }
        private async Task DeletePlayer(int id)
        {
            if (await JsRuntime.InvokeAsync<bool>("confirm", "Weet je zeker dat je deze speler wilt verwijderen?"))
            {
                var result = await PlayerService.DeleteAsync(id);
                if (result.IsSuccess)
                {
                    NavigationManager.NavigateTo($"/teaminfo/{SelectedPlayer.TeamId}",true);
                }
                else
                {
                    ErrorHandlingService.HandleError(result);
                }
            }
        }
    }
}
