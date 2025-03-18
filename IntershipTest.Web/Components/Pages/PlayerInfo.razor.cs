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
        private bool ShowError = false;
        private string ErrorMessage = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            var result = await PlayerService.GetByIdAsync(PlayerId);
            if (result.IsSuccess)
            {
                SelectedPlayer = result.Value;
            }
            else
            {
                HandleError(result);
            }
            var result2 = await TeamService.GetAllAsync();
            if (result2.IsSuccess)
            {
                Teams = result2.Value.ToList();
            }
            else
            {
                HandleError(result2);
            }
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
            StateHasChanged();
        }
        private async void SaveEditPlayer()
        {
            var result = await PlayerService.UpdateAsync(EditingPlayer.MapToPlayerUpdateRequestModel());
            if (result.IsSuccess)
            {
                SelectedPlayer = EditingPlayer;
            }
            else
            {
                HandleError(result);
            }
            CloseEditModal();
        }
        private async void DeletePlayer(int id)
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
                    HandleError(result);
                }
            }
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
