using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Storage_Management_Application.Core.Abstractions;
using Storage_Management_Application.Models;
using System.Runtime;

namespace Storage_Management_Application.Pages
{
    public class BalancePageModel : PageModel
    {
        private readonly IBalanceRepository _balanceRepository;

        public BalancePageModel(IBalanceRepository balanceRepository)
        {
            _balanceRepository = balanceRepository ?? throw new ArgumentNullException(nameof(balanceRepository));
        }

        public List<Balance> Balances { get; set; } = new List<Balance>();
        public List<string> ResourceNames { get; set; } = new List<string>();

        public async Task OnGetAsync()
        {
            Balances = await _balanceRepository.GetAllBalancesAsync();
            ResourceNames = Balances.Select(b => b.Resource.Name).Distinct().ToList();
        }

        public async Task<IActionResult> OnPostFilterByResourceAsync(string resourceName)
        {
            if (string.IsNullOrWhiteSpace(resourceName))
            {
                Balances = await _balanceRepository.GetAllBalancesAsync();
            }
            else
            {
                Balances = await _balanceRepository.GetBalanceByResourceNameAsync(resourceName);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostFilterByUnitAsync(string unitName)
        {
            if (string.IsNullOrWhiteSpace(unitName))
            {
                Balances = await _balanceRepository.GetAllBalancesAsync();
            }
            else
            {
                Balances = await _balanceRepository.GetBalanceByUnitNameAsync(unitName);
            }
            return Page();
        }
    }
}
