using Dima.Core.Handlers;
using Dima.Core.Requests.Account;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace Dima.Web.Pages.Identity;

public partial class RegisterPage : ComponentBase
{
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;
    [Inject]
    public IAccountHandler AccountHandler { get; set; } = null!;
    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;
    [Inject]
    public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;
    public bool isBusy { get; set; } = false;
    public RegisterRequest RegisterRequest{ get; set; } = new();
    public MudForm _mudForm { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if(user.Identity is {IsAuthenticated : true}) NavigationManager.NavigateTo("");
    }

    public async Task OnValidSubmitAsync()
    {
        isBusy = true;

        try
        {
            var result = await AccountHandler.RegisterAsync(RegisterRequest);

            if(result.isSuccess){
                Snackbar.Add("Cadastro realizado com sucesso!", Severity.Success);
                NavigationManager.NavigateTo("/login");
            } 
            else Snackbar.Add(result.message, Severity.Error);         
        }
        catch (System.Exception)
        {
            Snackbar.Add("Ocorreu um erro inesperado", Severity.Error);
            throw;
        }
        finally
        {

        }
    }
}