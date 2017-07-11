using System;

namespace cm.mx.catalogo.Model
{
    [Serializable()]
    public class Sesion
    {
        public virtual string Estatus { get; set; }
        public virtual int SesionID { get; set; }
        public virtual string Token { get; set; }
        public virtual int UsuarioID { get; set; }
    }
}