using System;

namespace Common.MessageBox.Enums
{
  public enum Icon
  {
    None,
    Question,
    Info,
    Warning,
    Error
  }

  [Flags]
  public enum ButtonResult
  {
    Ok,
    Yes,
    No,
    Abort,
    Cancel,
    None
  }

  public enum ButtonEnum
  {
    Ok,
    YesNo,
    OkCancel,
    OkAbort,
    YesNoCancel,
    YesNoAbort
  }
}