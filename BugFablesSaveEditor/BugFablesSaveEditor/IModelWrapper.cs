namespace BugFablesSaveEditor;

public interface IModelWrapper<out T>
{
  public T Model { get; }
}
