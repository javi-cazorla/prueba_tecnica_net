namespace prueba_tecnica_net.Enumerables;

public enum SavedStatus
{
    //Cuando se guarda correctamente en base de datos
    Saved,
    //Cuando no se guarda en base de datos porque ya hay datos
    NotSaved,
    //Cuando no se guarda en base de datos porque no hay datos
    Empty,
    //Cuando hay un error al guardar en base de datos
    Error
}
