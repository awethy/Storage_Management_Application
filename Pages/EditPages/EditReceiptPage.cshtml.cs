using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Storage_Management_Application.Core.ServicesAbstractions;
using Storage_Management_Application.DTO;
using Storage_Management_Application.Models;

namespace Storage_Management_Application.Pages.EditPages
{
    public class EditReceiptPageModel : PageModel
    {
        private readonly IReceiptService _receiptService;
        private readonly IResourceService _resourceService;
        private readonly IUnitsOMService _unitsOMService;

        public EditReceiptPageModel(IResourceService resourceService,
            IUnitsOMService unitsOMService,
            IReceiptService receiptService)
        {
            _resourceService = resourceService ?? throw new ArgumentNullException(nameof(resourceService));
            _unitsOMService = unitsOMService ?? throw new ArgumentNullException(nameof(unitsOMService));
            _receiptService = receiptService ?? throw new ArgumentNullException(nameof(receiptService));
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty]
        public ReceiptDocument Receipt { get; set; }
        public List<Resource> AvailableResources { get; private set; } = new();
        public List<UnitsOM> AvailableUnits { get; private set; } = new();
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                AvailableResources = await _resourceService.GetActiveResourcesAsync();
                AvailableUnits = await _unitsOMService.GetActiveUnitsAsync();
                Receipt = await _receiptService.GetReceiptById(Id);
                if (Receipt == null)
                {
                    return NotFound($"Поступление с ID {Id} не найдено.");
                }
                return Page();
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                ModelState.AddModelError(string.Empty, $"Ошибка при загрузке данных: {ex.Message}");
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState)
                {
                    foreach (var error in modelState.Value.Errors)
                    {
                        ErrorMessage = ErrorMessage + $"{error.ErrorMessage}";
                    }
                }
                return Page();
            }
            try
            {
                var newReceipt = new ReceiptDocumentDTO
                {
                    Number = Receipt.Number,
                    Date = Receipt.Date,
                    ReceiptResources = Receipt.ReceiptResources.Select(rr => new ReceiptResourceDTO
                    {
                        ResourceId = rr.ResourceId,
                        UnitsOMId = rr.UnitsOMId,
                        Quantity = rr.Quantity
                    }).ToList()
                };
                await _receiptService.UpdateReceipt(newReceipt, Id);

                return RedirectToPage("/ReceiptPage");
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                ModelState.AddModelError(string.Empty, $"Ошибка при обновлении данных: {ex.Message}");
                return Page();
            }
        }

        public async Task<IActionResult> OnPostDeleteReceiptAsync()
        {
            try
            {
                await _receiptService.DeleteReceipt(Id);
                return RedirectToPage("/ReceiptPage");
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                ModelState.AddModelError(string.Empty, $"Ошибка при удалении поступления: {ex.Message}");
                return Page();
            }
        }
    }
}
