using AutoMapper;
using BHD.Application.Dtos.Security;
using BHD.Application.Dtos.User;
using BHD.Application.Repositories;
using BHD.Application.Security.Authentication;
using BHD.Application.Security.Password;
using BHD.Domain.Models;
using BHD.Models.Models;
using FluentValidation;

namespace BHD.Application.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPhoneRepository _phoneRepository;
        private readonly IJwtFactory _jwtFactory;
        private readonly IPasswordService _passwordService;
        private readonly IValidator<User> _validator;
        private readonly IMapper _mapper;
        public UserService(
            IUserRepository userRepository,
            IPhoneRepository phoneRepository,
            IJwtFactory jwtFactory,
            IPasswordService passwordService,
            IMapper mapper,
            IValidator<User> validator)
        {
            _userRepository = userRepository;
            _phoneRepository = phoneRepository;
            _jwtFactory = jwtFactory;
            _passwordService = passwordService;
            _mapper = mapper;
            _validator = validator;
        }
        public bool Login(LoginModel model)
        {
            bool isValid = false;
            var user = _userRepository.GetByEmail(model.Email);
            if (user == null)
                return isValid;

            isValid = _passwordService.VerifyPassword(user.Password, model.Password);
            return isValid;
        }
        public CreatedUserDto Create(UserDto model)
        {

            var dbModel = _mapper.Map<User>(model);
            var validationResult = _validator.Validate(dbModel);

            if (!validationResult.IsValid)
            {
                var test = validationResult.Errors.Select(e => e.ErrorMessage);
                var errors = string.Join("|", test);
                throw new ValidationException(errors);
            }

            dbModel.Password = _passwordService.HashPassword(dbModel.Password);
            dbModel.Token = _jwtFactory.GenerateEncodedToken(dbModel.Email);

            var createdDbModel = _userRepository.Create(dbModel);

            model.Phones = model.Phones.Select(y =>
            {
                y.UserId = createdDbModel.Id;
                return y;
            });

            var phones = _mapper.Map<IEnumerable<Phone>>(model.Phones);
            _phoneRepository.AddRange(phones);

            var created = _mapper.Map<CreatedUserDto>(dbModel);

            return created;
        }

        public CreatedUserDto Delete(string Id)
        {
            throw new NotImplementedException();
        }

        public CreatedUserDto Get(string Id)
        {
            throw new NotImplementedException();
        }

        public CreatedUserDto Update(UserDto model)
        {
            throw new NotImplementedException();
        }
    }
}
