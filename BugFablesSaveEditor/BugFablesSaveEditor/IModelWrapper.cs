namespace BugFablesSaveEditor;

public interface IModelWrapper<T>
{
  public T Model { get; }
  public static abstract IModelWrapper<T> WrapModel(T model);
}
