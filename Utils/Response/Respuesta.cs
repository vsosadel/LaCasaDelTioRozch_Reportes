namespace Utils.Response
{
    public class Respuesta<T> : Resultado
    {
        public List<T> lsCuerpo { get; set; }
        public T? cuerpo { get; set; }

        public Respuesta()
        {
            lsCuerpo = new List<T>();
        }
    }
}
