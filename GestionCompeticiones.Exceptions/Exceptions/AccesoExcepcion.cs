namespace GestionCompeticiones.WebAPI.Exceptions
{
    public class AccesoExcepcion : ExcepcionDeDominio
    {
        public AccesoExcepcion(string mensaje = "Acceso prohibido") : base(mensaje) { }
    }
}
