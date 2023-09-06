namespace DAL.Readers.Base;
public interface IReader<T>
{
    IEnumerable<T> Read(string path);
}
