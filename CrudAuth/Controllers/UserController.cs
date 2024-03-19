using AutoMapper;
using CrudAuth.Models.DTOs;
using CrudAuth.Models.Entities;
using CrudAuth.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace CrudAuth.Controllers
{
    [Route("api/User")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpPost("CreateUser")]
        [AllowAnonymous]
        public async Task<IActionResult> Create(UserCreateDTO userDto)
        {
            try
            {
                if (await _userRepository.GetUserByName(userDto.Username) != null)
                {
                    return BadRequest("The UserName already exists");
                }
                User new_user = _mapper.Map<User>(userDto);

                await _userRepository.CreateUser(new_user);

                return Ok(new_user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetAllUser")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = await _userRepository.GetAllUsers();
                var userDTOs = _mapper.Map<List<UserGetDTO>>(users);
                return Ok(userDTOs);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("DeleteUser")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletUser(int id)
        {
            try
            {
                if (await _userRepository.GetUserById(id) == null)
                {
                    return BadRequest("The user you are trying to delete does not exist");
                }
                await _userRepository.DeleteUser(id);
                return Ok("User successfully deleted");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetByIdUser")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                UserGetDTO userDTO = _mapper.Map<UserGetDTO>(await _userRepository.GetUserById(id));
                return Ok(userDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
