namespace prueba_tecnica_net.Enumerables;

public enum DeletedStatus
{
    //Cuando se elimina correctamente en base de datos
    Deleted,
    //Cuando no se elimina en base de datos porque no hay datos
    Empty,
    //Cuando hay un error al eliminar en base de datos
    Error
}
