using System;
using System.Diagnostics;

namespace SLBenefits.Core
{
  public abstract class DisposableBase : IDisposable
  {
    bool _isDisposed;

    [DebuggerStepThrough]
    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    void Dispose(bool disposing)
    {
      if (!_isDisposed)
      {
        OnDisposing(disposing);
      }
      _isDisposed = true;
    }

    protected abstract void OnDisposing(bool disposing);

    ~DisposableBase()
    {
      Dispose(false);
    }
  }
}