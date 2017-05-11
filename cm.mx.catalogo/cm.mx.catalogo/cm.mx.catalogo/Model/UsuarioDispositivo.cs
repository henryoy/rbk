using System;

namespace cm.mx.catalogo.Model
{
    [Serializable()]
    public class UsuarioDispositivo
    {
        public virtual int UsuarioDispositivoId { get; set; }
        public virtual int UsuarioId { get; set; }
        public virtual string Token { get; set; }
        public virtual string Dispositivo { get; set; }
        public virtual string Serie { get; set; }
        public virtual string Plataforma { get; set; }
        public virtual DateTime FechaAlta { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}