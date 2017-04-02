namespace cm.mx.catalogo.Enums
{
    // se pretende no eliminar ningun elemento, sino solo dar de baja por seguiridad el sistema
    internal enum Estatus
    {
        ACTIVO,//alta en general
        INACTIVO,//inactivo
        BAJA,//Eliminado
    }

    public enum TipoUsuario
    {
        WEB,
        MOBILE
    }
}