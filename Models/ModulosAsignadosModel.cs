namespace COMBUSTIBLEAESCORE.Models
{
    public class ModulosAsignadosModel
    {
        public int ModuloID { get; set; }
        public string Nombre { get; set; }
        public int Nivel { get; set; }
        public string Codigo { get; set; }
        public string Parent {  get; set; }
        public string GrandParent { get; set; }
        public bool Asiganado { get; set; }
    }
}
