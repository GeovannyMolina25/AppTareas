using System;
using System.Collections.Generic;

namespace AppTareas.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Comentarios = new HashSet<Comentario>();
            Perfiles = new HashSet<Perfile>();
            Pujas = new HashSet<Puja>();
            Servicios = new HashSet<Servicio>();
            TransaccioneIdUsuarioClienteNavigations = new HashSet<Transaccione>();
            TransaccioneIdUsuarioProveedorNavigations = new HashSet<Transaccione>();
        }

        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Contrasena { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Rol { get; set; } = null!;
        public decimal? Calificacion { get; set; }

        public virtual ICollection<Comentario> Comentarios { get; set; }
        public virtual ICollection<Perfile> Perfiles { get; set; }
        public virtual ICollection<Puja> Pujas { get; set; }
        public virtual ICollection<Servicio> Servicios { get; set; }
        public virtual ICollection<Transaccione> TransaccioneIdUsuarioClienteNavigations { get; set; }
        public virtual ICollection<Transaccione> TransaccioneIdUsuarioProveedorNavigations { get; set; }
    }
}
