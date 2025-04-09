namespace Adopet.Exceptions
{
    public class AdocaoException : Exception
    {
        public AdocaoException() { }
        public AdocaoException(string? message) : base(message) {}

        public AdocaoException(string? message, Exception? excecaoInterna) : base(message, excecaoInterna) { }
    }

    public class PetIndisponivelException : AdocaoException
    {
        public PetIndisponivelException() { }
        public PetIndisponivelException(string? message) : base(message) { }
        public PetIndisponivelException(string? message, Exception? excecaoInterna) : base(message, excecaoInterna) { }
    }

    public class PetEstaSendoAdotadoException : AdocaoException
    {
        public PetEstaSendoAdotadoException() { }

        public PetEstaSendoAdotadoException(string? message) : base(message)
        {
        }
    }

    public class TutorComLimiteAtingidoException : AdocaoException
    {
        public TutorComLimiteAtingidoException() { }

        public TutorComLimiteAtingidoException(string? message) : base(message)
        {
        }
    }
}
