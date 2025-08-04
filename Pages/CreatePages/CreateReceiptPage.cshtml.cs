using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Storage_Management_Application.Core.ServicesAbstractions;
using Storage_Management_Application.Models;
using Storage_Management_Application.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public ReceiptDocument Receipt { get; set; }
        public List<Resource> AvailableResources { get; private set; } = new();
        public List<UnitsOM> AvailableUnits { get; private set; } = new();

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
                // ����������� ������
                ModelState.AddModelError(string.Empty, $"������ ��� �������� ������: {ex.Message}");
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine("==> ���� � OnPostAsync");
            if (!ModelState.IsValid)
            {
                Console.WriteLine("==> ModelState ����������:");
                foreach (var modelState in ModelState)
                {
                    foreach (var error in modelState.Value.Errors)
                    {
                        Console.WriteLine($" - {modelState.Key}: {error.ErrorMessage}");
                    }
                }
                return Page();

            }
            try
            {
                Receipt.Date = DateTime.SpecifyKind(Receipt.Date, DateTimeKind.Utc);
                await _receiptService.CreateReceiptDoc(Receipt);
                // ���������� ������� ����� �������� ���������
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
                // ����������� ������
                ModelState.AddModelError(string.Empty, $"������ ��� ���������� ������: {ex.Message}");
                return Page();
            }
        }
    }
}
