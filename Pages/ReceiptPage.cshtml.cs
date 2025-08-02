using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Storage_Management_Application.Core.Abstractions;
using Storage_Management_Application.Core.ServicesAbstractions;
using Storage_Management_Application.Data.Repositories;
using Storage_Management_Application.Models;

namespace Storage_Management_Application.Pages
{
    public class ReceiptPageModel : PageModel
    {
        private readonly IReceiptService _receiptService;
        private readonly IResourceService _resourceService;

        public ReceiptPageModel(IReceiptService receiptService, IResourceService resourceService)
        {
            _receiptService = receiptService ?? throw new ArgumentNullException(nameof(receiptService));
            _resourceService = resourceService ?? throw new ArgumentNullException(nameof(resourceService));
        }

        public List<ReceiptDocument> Receipts { get; set; } = new List<ReceiptDocument>();
        public List<Resource> AvailableResources { get; set; } = new List<Resource>();

        public async Task OnGetAsync()
        {
            Receipts = await _receiptService.GetReceipts();
            AvailableResources = await _resourceService.GetActiveResourcesAsync();
        }
    }
}
