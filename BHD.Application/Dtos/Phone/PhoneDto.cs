namespace BHD.Application.Dtos.Phone
{
    /// <summary>
    /// Clase que representa los datos de un telefono
    /// </summary>
    public class PhoneDto
    {
        /// <summary>
        /// campo que contiene el número del telefono
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// campo que contiene el código de área del telefono
        /// </summary>
        public string CityCode { get; set; }
        /// <summary>
        /// campo que contiene el código de país del telefono
        /// </summary>
        public string CountryCode { get; set; }
        /// <summary>
        /// Campo que contiene el id del usuario al que pertenece el telefono
        /// </summary>
        public Guid? UserId { get; set; }
    }
}
