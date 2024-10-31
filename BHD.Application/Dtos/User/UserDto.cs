using BHD.Application.Dtos.Phone;

namespace BHD.Application.Dtos.User
{
    /// <summary>
    /// Clase que contiene los datos para la creación de un usuario
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Campo que contiene el nombre del usuario, longitud maxima de 64 caracteres
        /// </summary>
        /// <example>John Doe</example>
        public string Name { get; set; }
        /// <summary>
        /// Campo que contiene el nombre del usuario, longitud maxima de 254 caracteres
        /// </summary>
        /// <example>JohnDoe@company.com</example>
        public string Email { get; set; }
        /// <summary>
        /// Campo que contiene la contraseña del usuario, longitud maxima de 64 caracteres
        /// </summary>
        /// <example>JohnDoe@company.com</example>
        public string Password { get; set; }
        /// <summary>
        /// Lista de telefonos asociados al usuario
        /// </summary>
        public IEnumerable<PhoneDto> Phones { get; set; }
    }
}
