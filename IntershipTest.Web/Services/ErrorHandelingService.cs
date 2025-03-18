using IntershipTest.Core.Services.Models;

namespace IntershipTest.Web.Services
{
    public class ErrorHandelingService
    {
        public bool ShowError { get; private set; }
        public string ErrorMessage { get; private set; }

        public void HandleError<T>(ResultModel<T> resultModel)
        {
            if (resultModel.Errors.Any())
            {
                ShowError = true;
                ErrorMessage = string.Join(", ", resultModel.Errors);
            }
            else
            {
                ShowError = false;
                ErrorMessage = string.Empty;
            }
        }
    }
}
