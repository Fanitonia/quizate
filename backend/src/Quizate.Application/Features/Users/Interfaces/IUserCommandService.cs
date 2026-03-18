using Quizate.Application.Common.Result;
using Quizate.Application.Features.Auth.DTOs.Requests;
using Quizate.Application.Features.Users.DTOs.Requests;

namespace Quizate.Application.Features.Users.Interfaces;

public interface IUserCommandService
{
    public Task<Result> UpdateUserInfoAsync(UpdateUserRequest request, Guid userId);
    public Task<Result> UpdateUserRoleAsync(UpdateUserRoleRequest request, Guid userId);
    public Task<Result> DeleteUserAsync(Guid userId);
    public Task<Result> ChangePasswordAsync(PasswordChangeRequest request, Guid userId);
}
