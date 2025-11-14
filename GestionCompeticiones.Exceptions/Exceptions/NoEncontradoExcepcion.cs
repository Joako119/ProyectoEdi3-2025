namespace GestionCompeticiones.WebAPI.Exceptions
{
    public class NoEncontradoExcepcion : ExcepcionDeDominio
    {
        public NoEncontradoExcepcion(string mensaje) : base(mensaje) { }
    }
}
