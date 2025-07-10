using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Interfaces
{
    public interface ITaskService
    {
        Task<(IEnumerable<TaskDto> tasks, int totalCount)> GetAllAsync(int page, int pageSize);
        Task<TaskDto?> GetByIdAsync(Guid id);
        Task<TaskDto> CreateAsync(CreateTaskDto dto);
        Task<TaskDto?> UpdateAsync(Guid id, UpdateTaskDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
