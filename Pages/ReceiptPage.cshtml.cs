using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Storage_Management_Application.Core.ServicesAbstractions;
using Storage_Management_Application.DTO;
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

        public List<Resource> Resources { get; private set; } = new();
        public List<UnitsOM> Units { get; private set; } = new();

        [BindProperty(SupportsGet = true)]
        public List<int> SelectedResourceId { get; set; } = new List<int>();
        [BindProperty(SupportsGet = true)]
        public List<int> SelectedUnitId { get; set; } = new List<int>();
        [BindProperty(SupportsGet = true)]
        public DateTime DateFrom { get; set; } = DateTime.UtcNow.AddDays(-30); // По умолчанию 30 дней назад
        [BindProperty(SupportsGet = true)]
        public DateTime DateTo { get; set; } = DateTime.UtcNow;


        public async Task OnGetAsync()
        {
            try
            {
                Resources = await _resourceService.GetAllResourcesAsync();
                Units = await _unitsOMService.GetAllUnitsAsync();
                IEnumerable<ReceiptDocument> query = await _receiptService.GetReceipts();

                if (SelectedResourceId != null && SelectedResourceId.Any())
                {
                    query = query.Where(b => b.ReceiptResources.Any(r => r.Resource != null && SelectedResourceId.Contains(r.ResourceId)));
                }
                if (SelectedUnitId != null && SelectedUnitId.Any())
                {
                    query = query.Where(b => b.ReceiptResources.Any(r => r.Resource != null && SelectedUnitId.Contains(r.UnitsOMId)));
                }

                query = query.Where(b => b.Date >= DateFrom && b.Date <= DateTo);

                Receipts = query.ToList();
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                ModelState.AddModelError(string.Empty, $"Ошибка при загрузке данных: {ex.Message}");
            }
        }
    }
}

