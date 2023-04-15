namespace BugFablesSaveEditor;

/// <summary>
/// Allows the model wrapper to be used with a ViewModelCollection
/// </summary>
/// <typeparam name="T">The underlying type to wrap to</typeparam>
public interface IModelWrapper<T>
{
  /// <summary>
  /// The underlying model, should only be used for read operations
  /// </summary>
  public T Model { get; }

  /// <summary>
  /// A factory method to get a wrapper around an existing model
  /// </summary>
  /// <param name="model">The model to wrap</param>
  /// <returns>A wrapper around the model</returns>
  public static abstract IModelWrapper<T> WrapModel(T model);

  /// <summary>
  /// A factory method to get a wrapper around a new copy of a model
  /// </summary>
  /// <param name="model">The model to wrap</param>
  /// <returns>A wrapper around the model</returns>
  public static abstract IModelWrapper<T> WrapNewModel(T model);
}
