namespace GestionCompeticiones.WebAPI.Exceptions
{
    public class ValidacionExcepcion : ExcepcionDeDominio
    {
        public IEnumerable<string> Errores { get; }

        public ValidacionExcepcion(IEnumerable<string> errores)
            : base("Error de validación")
        {
            Errores = errores;
        }
    }
}
