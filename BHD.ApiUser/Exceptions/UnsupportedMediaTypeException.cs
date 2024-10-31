using System;

namespace BHD.ApiUser.Exceptions
{
    public class UnsupportedMediaTypeException : Exception
    {

        public UnsupportedMediaTypeException()
            : base("Solo se acepta content-type tipo application/json")
        {
        }

        public UnsupportedMediaTypeException(string message)
            : base(message)
        {
        }

        public UnsupportedMediaTypeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
