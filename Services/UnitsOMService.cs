using Storage_Management_Application.Core.Abstractions;
using Storage_Management_Application.Core.ServicesAbstractions;
using Storage_Management_Application.Models;

namespace Storage_Management_Application.Services
{
    public class UnitsOMService : IUnitsOMService
    {
        private readonly IUnitsOMRepository _unitRepository;

        public UnitsOMService(IUnitsOMRepository unitRepository)
        {
            _unitRepository = unitRepository;
        }

        public async Task<List<UnitsOM>> GetAllUnitsAsync()
        {
            return await _unitRepository.GetAllAsync();
        }

        public async Task<UnitsOM> GetUnitByIdAsync(int id)
        {
            return await _unitRepository.GetByIdAsync(id);
        }

        public async Task<List<UnitsOM>> GetActiveUnitsAsync()
        {
            return await _unitRepository.GetActiveUnitsAsync();
        }

        public async Task<List<UnitsOM>> GetInActiveUnitsAsync()
        {
            return await _unitRepository.GetInActiveUnitsAsync();
        }

        public async Task CreateUnitAsync(UnitsOM unit)
        {
            // Проверка на уникальность имени
            var allUnits = await _unitRepository.GetAllAsync();
            if (allUnits.Any(u => u.Name == unit.Name))
            {
                throw new InvalidOperationException($"Единица измерения с именем '{unit.Name}' уже существует.");
            }

            await _unitRepository.CreateAsync(unit);
        }

        public async Task UpdateUnitAsync(UnitsOM unit)
        {
            // Проверка на уникальность имени
            var allUnits = await _unitRepository.GetAllAsync();
            if (allUnits.Any(u => u.Name == unit.Name && u.Id != unit.Id))
            {
                throw new InvalidOperationException($"Единица измерения с именем '{unit.Name}' уже существует.");
            }
            await _unitRepository.UpdateAsync(unit);
        }

        public async Task DeleteUnitAsync(int id)
        {
            var unit = await _unitRepository.GetByIdAsync(id);
            if (unit == null)
            {
                throw new KeyNotFoundException($"Единица измерения с ID {id} не найдена.");
            }
            await _unitRepository.DeleteAsync(id);
        }

        public async Task InActiveUnit(int id)
        {
            var unit = await _unitRepository.GetByIdAsync(id);
            if (unit == null)
            {
                throw new KeyNotFoundException($"Единица измерения с ID {id} не найдена.");
            }
            unit.IsActive = false;
            await _unitRepository.UpdateAsync(unit);
        }

        public async Task ActiveUnit(int id)
        {
            var unit = await _unitRepository.GetByIdAsync(id);
            if (unit == null)
            {
                throw new KeyNotFoundException($"Единица измерения с ID {id} не найдена.");
            }
            unit.IsActive = true;
            await _unitRepository.UpdateAsync(unit);
        }
    }
}
