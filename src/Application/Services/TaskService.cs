using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;

namespace Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;
        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<(IEnumerable<TaskDto> tasks, int totalCount)> GetAllAsync(int page, int pageSize)
        {
            var items = await _repository.GetAllAsync(page, pageSize);
            var total = await _repository.CountAsync();
            return (items.Select(ToDto), total);
        }

        public async Task<TaskDto?> GetByIdAsync(Guid id)
        {
            var task = await _repository.GetByIdAsync(id);
            return task == null ? null : ToDto(task);
        }

        public async Task<TaskDto> CreateAsync(CreateTaskDto dto)
        {
            var entity = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                CreatedAt = DateTime.UtcNow,
                DueDate = dto.DueDate,
                Status = Domain.Enums.TaskStatus.Pending
            };
            await _repository.AddAsync(entity);
            return ToDto(entity);
        }

        public async Task<TaskDto?> UpdateAsync(Guid id, UpdateTaskDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;
            entity.Title = dto.Title;
            entity.Description = dto.Description;
            entity.DueDate = dto.DueDate;
            entity.Status = dto.Status;
            await _repository.UpdateAsync(entity);
            return ToDto(entity);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;
            await _repository.DeleteAsync(id);
            return true;
        }

        private static TaskDto ToDto(TaskItem task) => new TaskDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            CreatedAt = task.CreatedAt,
            DueDate = task.DueDate,
            Status = task.Status
        };
    }
}
