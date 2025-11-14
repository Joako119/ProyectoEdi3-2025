namespace GestionCompeticiones.WebAPI.Exceptions
{
    public class ExcepcionDeDominio : Exception
    {
        public ExcepcionDeDominio(string mensaje) : base(mensaje) { }
    }
}
