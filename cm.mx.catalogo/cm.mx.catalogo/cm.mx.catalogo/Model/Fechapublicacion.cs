using System;

namespace cm.mx.catalogo.Model
{
    public class Fechapublicacion
    {
        public virtual int Fechapublicacionid { get; set; }
        public virtual Promocion Promocion { get; set; }
        public virtual string Estado { get; set; }
        public virtual DateTime Fechainicio { get; set; }
        public virtual DateTime Fechafin { get; set; }
        public virtual DateTime Fechaalta { get; set; }
        public virtual int Usuarioalta { get; set; }
        public virtual DateTime Fechabaja { get; set; }
        public virtual int Usuariobaja { get; set; }
    }
}