namespace cm.mx.catalogo.Enums
{
    // se pretende no eliminar ningun elemento, sino solo dar de baja por seguiridad el sistema
    internal enum Estatus
    {
        ACTIVO,//alta en general
        PENDIENTE,
        INACTIVO,//inactivo
        BAJA,//Eliminado
    }

    public enum TipoUsuario
    {
        WEB,
        MOBILE
    }

    internal enum TipoNotificacion
    {
        VISITA,
        PROMOCION
    }
}