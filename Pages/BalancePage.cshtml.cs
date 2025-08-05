using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Storage_Management_Application.Core.Abstractions;
using Storage_Management_Application.Core.ServicesAbstractions;
using Storage_Management_Application.Models;
using System.Runtime.CompilerServices;

namespace Storage_Management_Application.Pages
{
    public class BalancePageModel : PageModel
    {
        private readonly IBalanceRepository _balanceRepository;
        private readonly IResourceService _resourceService;
        public readonly IUnitsOMService _unitsOMService;

        public BalancePageModel(IBalanceRepository balanceRepository, IResourceService resourceService, IUnitsOMService unitsOMService)
        {
            _balanceRepository = balanceRepository ?? throw new ArgumentNullException(nameof(balanceRepository));
            _resourceService = resourceService ?? throw new ArgumentNullException(nameof(resourceService));
            _unitsOMService = unitsOMService ?? throw new ArgumentNullException(nameof(unitsOMService));
        }

        [BindProperty(SupportsGet = true)]
        public int? SelectedResourceId { get; set; }
        [BindProperty(SupportsGet = true)]
        public int? SelectedUnitId { get; set; }

        public List<Balance> Balances { get; set; } = new List<Balance>();
        public List<Resource> Resources { get; set; } = new List<Resource>();
        public List<UnitsOM> Units { get; set; } = new List<UnitsOM>();

        public async Task OnGetAsync()
        {
            Resources = await _resourceService.GetAllResourcesAsync();
            Units = await _unitsOMService.GetAllUnitsAsync();

            IEnumerable<Balance> query = await _balanceRepository.GetAllBalancesAsync();

            if (SelectedResourceId.HasValue)
            {
                query = query.Where(b => b.ResourceId == SelectedResourceId.Value);
            }
            if (SelectedUnitId.HasValue)
            {
                query = query.Where(b => b.UnitsOMId == SelectedUnitId.Value);
            }

            Balances = query.ToList();
        }
    }
}
