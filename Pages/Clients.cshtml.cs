using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Storage_Management_Application.Core.ServicesAbstractions;
using Storage_Management_Application.Models;

namespace Storage_Management_Application.Pages
{
    public class ClientsModel : PageModel
    {
        private readonly IClientService _clientService;

        public ClientsModel(IClientService clientService)
        {
            _clientService = clientService;
        }

        public List<Client> ActiveClients { get; set; } = new List<Client>();
        public List<Client> InActiveClients { get; set; } = new List<Client>();
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostArchiveAsync(int selectedClientId)
        {
            // Логика архивирования ресурса
            await _clientService.InActiveClient(selectedClientId);
            return RedirectToPage();
        }

        public async Task OnGetAsync()
        {
            ActiveClients = await _clientService.GetActiveClients();
            InActiveClients = await _clientService.GetInActiveClients();
        }

        public async Task<IActionResult> OnPostAdd()
        {
            // Логика добавления новой единицы измерения
            var name = Request.Form["ClientName"];
            var address = Request.Form["ClientAddress"];    
            var newClient = new Client { Name = name, Address = address, IsActive = true };
            try
            {
                await _clientService.CreateClient(newClient);
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                await OnGetAsync();
                return Page();
            }
        }
    }
}
