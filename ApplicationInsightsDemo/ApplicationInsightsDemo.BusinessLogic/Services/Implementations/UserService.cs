using ApplicationInsightsDemo.BusinessLogic.Helpers.Interfaces;
using ApplicationInsightsDemo.BusinessLogic.Models.User;
using ApplicationInsightsDemo.BusinessLogic.Services.Interfaces;
using ApplicationInsightsDemo.Common.Exceptions;
using ApplicationInsightsDemo.DataAccess.DatabaseContexts.Interfaces;
using ApplicationInsightsDemo.DataAccess.DataModels;
using ApplicationInsightsDemo.DataAccess.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApplicationInsightsDemo.BusinessLogic.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IApplicationInsightsDemoDbContext _context;
        private readonly IPasswordHelper _passwordHelper;

        public UserService(IMapper mapper, IApplicationInsightsDemoDbContext context, IPasswordHelper passwordHelper)
        {
            _mapper = mapper;
            _context = context;
            _passwordHelper = passwordHelper;
        }

        public Task<List<UserModel>> GetAllAsync()
        {
            return _mapper.ProjectTo<UserModel>(_context.Users).ToListAsync();
        }

        public async Task<UserModel> GetByIdAsync(int id)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (userEntity == null)
            {
                throw new EntityNotFoundException(typeof(UserEntity), id);
            }

            var userModel = _mapper.Map<UserModel>(userEntity);
            return userModel;
        }

        public async Task<UserLoginInfoModel> GetUserLoginInfoAsync(string email)
        {
            var userLoginInfoDataModel = await _context.Users
                .Select(u => new UserLoginInfoDataModel
                {
                    Id = u.Id,
                    Email = u.Email,
                    PasswordHash = u.PasswordHash,
                    PasswordSalt = u.PasswordSalt
                }).FirstOrDefaultAsync(u => u.Email == email);

            if (userLoginInfoDataModel == null)
            {
                throw new EntityNotFoundException(typeof(UserEntity), $"{nameof(email)}: {email}");
            }

            var userLoginInfoModel = _mapper.Map<UserLoginInfoModel>(userLoginInfoDataModel);
            return userLoginInfoModel;
        }

        public async Task CreateAsync(UserCreateModel createModel)
        {
            var userEntity = _mapper.Map<UserEntity>(createModel);
            var passwordSalt = Guid.NewGuid().ToString();
            var passwordHash = _passwordHelper.GeneratePasswordHash(createModel.Password, passwordSalt);

            userEntity.PasswordHash = passwordHash;
            userEntity.PasswordSalt = passwordSalt;
            userEntity.CreatedAtUtc = DateTime.UtcNow;

            _context.Users.Add(userEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserUpdateModel updateModel)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Id == updateModel.Id);

            if (userEntity == null)
            {
                throw new EntityNotFoundException(typeof(UserEntity), updateModel.Id);
            }

            _mapper.Map(updateModel, userEntity);
            userEntity.UpdatedAtUtc = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (userEntity == null)
            {
                throw new EntityNotFoundException(typeof(UserEntity), id);
            }

            _context.Users.Remove(userEntity);
            await _context.SaveChangesAsync();
        }
    }
}