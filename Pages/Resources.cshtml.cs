using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Storage_Management_Application.Core.ServicesAbstractions;
using Storage_Management_Application.Models;


namespace Storage_Management_Application.Pages
{
    public class ResourcesModel : PageModel
    {
        private readonly IResourceService _resourceService;

        public ResourcesModel(IResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        public List<Resource> ActiveResources { get; set; } = new List<Resource>();
        public List<Resource> InActiveResources { get; set; } = new List<Resource>();
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostArchiveAsync(int selectedResourceId)
        {
            // Логика архивирования ресурса
            try
            {
                await _resourceService.InActiveResourceAsync(selectedResourceId);
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                await OnGetAsync();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(int selectedResourceId)
        {
            // Логика удаления ресурса
            try
            {
                await _resourceService.DeleteResourceAsync(selectedResourceId);
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                await OnGetAsync();
                return Page();
            }
        }

        public async Task OnGetAsync()
        {
            ActiveResources = await _resourceService.GetActiveResourcesAsync();
            InActiveResources = await _resourceService.GetInActiveResourcesAsync();
        }

        public async Task<IActionResult> OnPostAdd()
        {
            // Логика добавления новой единицы измерения
            var name = Request.Form["ResourceName"];
            var newResource = new Resource { Name = name, IsActive = true };
            try
            {
                await _resourceService.CreateResourceAsync(newResource);
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
