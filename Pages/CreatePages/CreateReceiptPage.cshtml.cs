using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Storage_Management_Application.Core.ServicesAbstractions;
using Storage_Management_Application.DTO;
using Storage_Management_Application.Models;

namespace Storage_Management_Application.Pages.CreatePages
{
    public class CreateReceiptPageModel : PageModel
    {
        private readonly IReceiptService _receiptService;
        private readonly IBalanceService _balanceService;
        private readonly IResourceService _resourceService;
        private readonly IUnitsOMService _unitsOMService;

        public CreateReceiptPageModel(IResourceService resourceService,
            IUnitsOMService unitsOMService,
            IReceiptService receiptService,
            IBalanceService balanceService)
        {
            _resourceService = resourceService ?? throw new ArgumentNullException(nameof(resourceService));
            _balanceService = balanceService ?? throw new ArgumentNullException(nameof(balanceService));
            _unitsOMService = unitsOMService ?? throw new ArgumentNullException(nameof(unitsOMService));
            _receiptService = receiptService ?? throw new ArgumentNullException(nameof(receiptService));
        }

        [BindProperty]
        public ReceiptDocumentDTO Receipt { get; set; }
        public List<Resource> AvailableResources { get; private set; } = new();
        public List<UnitsOM> AvailableUnits { get; private set; } = new();
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState)
                {
                    foreach (var error in modelState.Value.Errors)
                    {
                        ErrorMessage = ErrorMessage + $"" +
                            $"{error.ErrorMessage}";
                    }
                }
                return Page();
            }
            try
            {
                await _receiptService.CreateReceiptDoc(Receipt);
                // Обновление баланса после создания документа
                foreach (var resource in Receipt.ReceiptResources)
                {
                    var balance = new Balance
                    {
                        ResourceId = resource.ResourceId,
                        UnitsOMId = resource.UnitsOMId,
                        Quantity = resource.Quantity
                    };
                    await _balanceService.CreateBalance(balance);
                }

                return RedirectToPage("/ReceiptPage");
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                ErrorMessage = ex.Message;
                await OnGetAsync();
                return Page();
            }
        }
    }
}
