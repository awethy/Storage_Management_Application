using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Storage_Management_Application.Core.ServicesAbstractions;
using Storage_Management_Application.Models;

namespace Storage_Management_Application.Pages
{
    public class UnitsOfMeasurementModel : PageModel
    {
        private readonly IUnitsOMService _unitsOMService;

        public UnitsOfMeasurementModel(IUnitsOMService unitsOMService)
        {
            _unitsOMService = unitsOMService;
        }

        public List<UnitsOM> ActiveUnits { get; set; } = new List<UnitsOM>();
        public List<UnitsOM> InActiveUnits { get; set; } = new List<UnitsOM>();
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostArchiveAsync(int selectedUnitId)
        {
            //логика архивирования единицы
            try
            {
                await _unitsOMService.InActiveUnit(selectedUnitId);
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                await OnGetAsync();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(int selectedUnitId)
        {
            // Логика удаления единицы измерения
            try
            {
                await _unitsOMService.DeleteUnitAsync(selectedUnitId);
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
            ActiveUnits = await _unitsOMService.GetActiveUnitsAsync();
            InActiveUnits = await _unitsOMService.GetInActiveUnitsAsync();
        }

        public async Task<IActionResult> OnPostAdd()
        {
            // Логика добавления новой единицы измерения
            var name = Request.Form["UnitName"];
            var newUnit = new UnitsOM { Name = name, IsActive = true };
            try
            {
                await _unitsOMService.CreateUnitAsync(newUnit);
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
