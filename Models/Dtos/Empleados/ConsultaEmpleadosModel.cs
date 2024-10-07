namespace Models.Dtos.Empleados
{
    public class ConsultaEmpleadosModel
    {
        public string NombreEmpleado { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string TipoEmpleado { get; set; }
        public decimal Salario { get; set; }
        public string NombreSupervisor { get; set; }
    }
}
