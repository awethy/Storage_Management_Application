using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Storage_Management_Application.Core.ServicesAbstractions;
using Storage_Management_Application.Models;
using System.Threading.Tasks;

namespace Storage_Management_Application.Pages
{
    public class ReceiptPageModel : PageModel
    {
        private readonly IReceiptService _receiptService;
        private readonly IResourceService _resourceService;
        private readonly IUnitsOMService _unitsOMService;

        public ReceiptPageModel(IReceiptService receiptService, IResourceService resourceService, IUnitsOMService unitsOMService)
        {
            _receiptService = receiptService ?? throw new ArgumentNullException(nameof(receiptService));
            _resourceService = resourceService ?? throw new ArgumentNullException(nameof(resourceService));
            _unitsOMService = unitsOMService ?? throw new ArgumentNullException(nameof(unitsOMService));
        }

        public List<ReceiptDocument> Receipts { get; private set; } = new();
        public List<Resource> AvailableResources { get; private set; } = new();
        public List<UnitsOM> AvailableUnits { get; private set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                Receipts = await _receiptService.GetReceipts();
                AvailableResources = await _resourceService.GetActiveResourcesAsync();
                AvailableUnits = await _unitsOMService.GetActiveUnitsAsync();
                return Page();
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                ModelState.AddModelError(string.Empty, $"Ошибка при загрузке данных: {ex.Message}");
                return Page();
            }
        }
    }
}

