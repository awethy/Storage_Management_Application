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

        public ReceiptPageModel(IReceiptService receiptService, IResourceService resourceService, IUnitsOMService unitsOMService)
        {
            _receiptService = receiptService ?? throw new ArgumentNullException(nameof(receiptService));
        }

        public List<ReceiptDocument> Receipts { get; private set; } = new();


        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                Receipts = await _receiptService.GetReceipts();

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

