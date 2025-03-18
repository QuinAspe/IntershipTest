using IntershipTest.Core.Entities;
using IntershipTest.Core.Interfaces.Services;
using IntershipTest.Core.Services.Models;
using IntershipTest.Web.Mapping;
using Microsoft.JSInterop;
using static System.Net.WebRequestMethods;

namespace IntershipTest.Web.Components.Pages
{
    public partial class Players
    {
        private List<Player> PlayersList = new();
        private List<Team> Teams = new();
        private Player EditingPlayer = new();
        private Player AddingPlayer = new();
        private bool IsAddModalOpen = false;

        protected async override Task OnInitializedAsync()
        {
            PlayersList = await DataService.GetPlayersAsync();
            Teams = await DataService.GetTeamsAsync();
            await base.OnInitializedAsync();
        }
        private void GoToPlayerInfo(int id)
        {
            NavigationManager.NavigateTo($"/playerinfo/{id}");
        }
        private void OpenAddModal()
        {
            AddingPlayer = new Player();
            IsAddModalOpen = true;
        }
        private void CloseAddModal()
        {
            IsAddModalOpen = false;
            StateHasChanged();
        }
        private async Task SaveAddPlayer()
        {
            var result = await PlayerService.AddAsync(AddingPlayer.MapToPlayerAddRequestModel());
            if (result.IsSuccess)
            {
                PlayersList = await DataService.GetPlayersAsync();
            }
            else
            {
                ErrorHandlingService.HandleError(result);
            }
            CloseAddModal();
        }
    }
}
