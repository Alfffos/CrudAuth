using Agenda_api.Models.DTOs;
using CrudAuth.Repository.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;
using CrudAuth.Models.Entities;
using System.IdentityModel.Tokens.Jwt;


namespace CrudAuth.Controllers
{
    [Route("api/Authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;
        public AuthenticationController(IConfiguration config, IUserRepository userRepository)
        {
            _config = config;
            _userRepository=userRepository;
        }



        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate(AutenticationRequestBody autenticationRequestBody)
        {
            var user = await _userRepository.ValidateUser(autenticationRequestBody); // Paso 1 valida las crendenciales del usuario(Usuario y contraseña).

            if (user is null)
            {
                return Unauthorized(); //Si no se valida el usuario retorna un 401.
            }

            // Paso 2 Genera el token.
            var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Jwt:SecretForKey"]));

            var credentials = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

            // aca se acceden a las propiedades del objeto User para agregarlas al token.(En este caso se agrega el id, el nombre y el rol pero puede variar.)
            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", user.Id.ToString()));
            claimsForToken.Add(new Claim("given_name", user.UserName)); //Accedemos a las propiedades del objeto User
            //claimsForToken.Add(new Claim("family_name", user.LastName));
            claimsForToken.Add(new Claim("role", user.Role.ToString()));

            var jwtSecurityToken = new JwtSecurityToken(
              _config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claimsForToken,
              DateTime.UtcNow,
              DateTime.UtcNow.AddHours(1), // Aca se configura para que el token expire en una hora(siempre y cuando el backend este correendo).
              credentials);


            var tokenToReturn = new JwtSecurityTokenHandler()
                .WriteToken(jwtSecurityToken);

            return Ok(tokenToReturn);
        }

    }
}
