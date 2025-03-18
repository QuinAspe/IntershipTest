using IntershipTest.Core.Entities;
using IntershipTest.Core.Interfaces.Services;
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
            GetPlayers();
            GetTeams();
            await base.OnInitializedAsync();
        }
        private void GoToPlayerInfo(int id)
        {
            NavigationManager.NavigateTo($"/playerinfo/{id}");
        }
        private async void GetTeams()
        {
            var getTeams = await TeamService.GetAllAsync();
            if (!getTeams.IsSuccess)
            {

            }
            else
            {
                Teams = getTeams.Value.ToList();
                StateHasChanged();
            }
        }
        private async void GetPlayers()
        {
            var getPlayers = await PlayerService.GetAllAsync();
            if (!getPlayers.IsSuccess)
            {

            }
            else
            {
                PlayersList = getPlayers.Value.ToList();
                StateHasChanged();
            }
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
        private async void SaveAddPlayer()
        {
            var result = await PlayerService.AddAsync(AddingPlayer.MapToPlayerAddRequestModel());
            if (result.IsSuccess)
            {
                GetPlayers();
            }
            else
            {
                //errors
            }
            CloseAddModal();
        }
    }
}
