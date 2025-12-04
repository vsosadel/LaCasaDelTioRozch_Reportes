namespace Utils.Response
{
    public class Resultado
    {
        public string mensaje { get; set; }
        public bool exitoso { get; set; }
        public Resultado()
        {
            mensaje = string.Empty;
            exitoso = false;
        }
    }
}
